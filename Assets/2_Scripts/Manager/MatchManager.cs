using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    readonly string[] _winConditionString = { "잡은 물고기 길이 총합", "잡은 물고기 무게 총합", "잡은 물고기 마릿수 총합", "잡은 물고기 골드 총합" };
    readonly float _limitTime = 300;
    readonly int _centerHash = Animator.StringToHash("Center");

    [Header("상대 이름 리스트")]
    [SerializeField] string[] _opponentNameList;

    [Header("스크립트")]
    [SerializeField] MatchResultUI _resultUI;
    [SerializeField] GameManager _gameManager;
    [SerializeField] PetManager _petManager;
    [SerializeField] FishControl _fishControl;
    [SerializeField] InGameUIManager _ingameUIManager;

    [SerializeField] Text _conditionText;

    [SerializeField] Text _myNameText;
    [SerializeField] Image _myBar;
    [SerializeField] Text _myScoreText;
    float _myScore;

    [SerializeField] Text _opponentNameText;
    [SerializeField] Image _opponentBar;
    [SerializeField] Text _opponentScoreText;
    float _opponentScore;

    [SerializeField] Image _timerImage;
    [SerializeField] Text _timerText;

    [SerializeField] GameObject _resultObject;

    [SerializeField] GameObject _equipmentUI;
    [SerializeField] GameObject _exitUI;

    [SerializeField] GameObject _noticeObject;
    [SerializeField] Text _noticeText;

    // 물고기 팝업창이 있을 때는 장비창 안켜지게 막아야한다.
    [SerializeField] GameObject _fishPopupObject;

    UserData _userData;
    int _conditionNumber; // 대결 종목 번호
    int _beforeGrade = 0; // 이전 등급
    float _beforeStar = 0; // 이전 별 갯수

    string _opponentName; // 상대 이름 - 결과창에 띄워야 해서 가지고 있어야 한다.

    StringBuilder _sb = new StringBuilder();
    StringBuilder _sb2 = new StringBuilder();

    // 블루투스 데이터 관련
    BLETotal bleTotal;

    Coroutine _timerAudioCoroutine = null;
    float _timer;

    private void Start()
    {
        int rand = UnityEngine.Random.Range(0, _opponentNameList.Length);
        _opponentName = _opponentNameList[rand];
        _opponentNameText.text = _opponentName;

        _userData = DBManager.INSTANCE.GetUserData();
        _conditionNumber = DataManager.INSTANCE._matchConditionNumber;
        _conditionText.text = _winConditionString[_conditionNumber];
        _myNameText.text = _userData._nickname;

        _myScoreText.text = "0";
        _opponentScoreText.text = "0";

        _myBar.fillAmount = 0.5f;
        _opponentBar.fillAmount = 0.5f;

        _beforeGrade = _userData._grade;
        _beforeStar = _userData._star;

        // 블루투스 오브젝트 연결
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();
        }

        //Debug.Log("대전 매치 시작 (등급: " + _userData._grade + ", 별: " + _userData._star + ")");

        StartCoroutine(TimerCoroutine());

        //
        //_resultUI.Init(8, 3, 8, 4, "잡은 물고기 마릿수 총합", 1, "sssmmm", "FishingBot", 123456, 123456);
    }

    IEnumerator TimerCoroutine()
    {
        _timer = _limitTime;

        while(_timer > 0)
        {
            if (_timer <= 10.2f && _timerAudioCoroutine == null)
            {
                _timerAudioCoroutine = StartCoroutine(TimerAudioCoroutine());
            }

            _timerText.text = _timer.ToString("N0");
            _timerImage.fillAmount = 1 - (_timer / _limitTime);
            _timer -= Time.deltaTime;

            yield return null;
        }

        GameEnd();
    }

    IEnumerator TimerAudioCoroutine()
    {
        _timerText.color = Color.red;
        while(true)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeCount).GetComponent<AudioPoolObject>().Init();
            yield return PublicDefined._1secRealDelay;
        }
    }

    void GameEnd()
    {
        if (_timerAudioCoroutine != null)
        {
            StopCoroutine(_timerAudioCoroutine);
            _timerAudioCoroutine = null;
        }
            
        if (_equipmentUI.activeSelf)
            _equipmentUI.SetActive(false);

        if (_exitUI.activeSelf)
            _exitUI.SetActive(false);

        if (bleTotal != null && bleTotal.ConnectedMain && _gameManager.NeedleInWater)
        {
            _fishControl.MotorStop();
            //bleTotal.Motor(0, 0);
        }

        StartCoroutine(ResultUICoroutine());
    }
    void StopAllAction()
    {
        if (_fishControl.IsSpecialAttack)
        {
            _gameManager.SettingCharacterAnimator(_centerHash, false);
            _fishControl.IsSpecialAttack = false;
        }

        AudioManager.INSTANCE.StopAllEffect();

        if (AudioManager.INSTANCE.CheckBGMPlaying())
            AudioManager.INSTANCE.StopBGM();

        _ingameUIManager._ingameUIManager_isPause = true;
        _fishControl.IsPause = true;
        _gameManager.NeedleControl._isPause = true;
        _gameManager.IsPause = true;
        _gameManager.IsReset = true;
        _gameManager.IsPlayingBGM = false;
        _gameManager.ResetAction();
        _gameManager.StopAllCoroutines();
        _fishControl.StopAllCoroutines();
        _petManager.StopAllCoroutines();
    }

    IEnumerator ResultUICoroutine()
    {
        int isVictory = CheckVictory();

        CheckStar(isVictory);
        CheckGrade(isVictory);
        UpdateData(isVictory);

        StopAllAction();

        Time.timeScale = 0.5f;

        yield return PublicDefined._05secDelay;
        yield return PublicDefined._02secDelay;

        AudioManager.INSTANCE.StopBGM();
        AudioManager.INSTANCE.StopAllEffect();

        if (_fishControl.FishResisteAudioObject != null)
        {
            _fishControl.FishResisteAudioObject.GetComponent<AudioSource>().loop = false;
            _fishControl.FishResisteAudioObject.GetComponent<AudioPoolObject>().ReturnThis();
            _fishControl.FishResisteAudioObject = null;
        }

        _resultUI.Init(_beforeGrade, _beforeStar, _userData._grade, _userData._star, _winConditionString[_conditionNumber], isVictory, _userData._nickname, _opponentName, _myScore, _opponentScore);

        Time.timeScale = 1;
        _gameManager.ResetAction();

    }
    public void UpdateMyScore(float length, float weight, int count, int gold)
    {
        switch(_conditionNumber)
        {
            // 길이
            case 0:
                _myScore += length;
                break;
            // 무게
            case 1:
                _myScore += weight;
                break;
            // 마릿수
            case 2:
                _myScore += count;
                break;
            // 골드
            case 3:
                _myScore += gold;
                break;
            default:
                break;
        }

        UpdateBothScore();
    }
    public void UpdateOpponentScore(float length, float weight, float count, float gold, string name)
    {
        //Debug.Log("매치 번호-" + _conditionNumber +  " [" + name + "] 길이: " + length + ", 무게: " + weight + ", 골드: " + gold);
        _sb.Length = 0;
        _sb.Append("상대방이 [");
        _sb.Append(name);
        _sb.Append("] 을(를) 잡았습니다.");

        _noticeText.text = _sb.ToString();
        _noticeObject.SetActive(true);
        StartCoroutine(MakeDelay(()=> _noticeObject.SetActive(false)));
        
        switch (_conditionNumber)
        {
            // 길이
            case 0:
                _opponentScore += length;
                break;
            // 무게
            case 1:
                _opponentScore += weight;
                break;
            // 마릿수
            case 2:
                _opponentScore += count;
                break;
            // 골드
            case 3:
                _opponentScore += gold;
                break;
            default:
                break;
        }

        UpdateBothScore();
    }
    void UpdateBothScore()
    {
        switch (_conditionNumber)
        {
            // 길이
            case 0:
                _sb2.Length = 0;
                _sb2.Append(_myScore.ToString("N1"));
                _sb2.Append("cm");
                _myScoreText.text = _sb2.ToString();

                _sb2.Length = 0;
                _sb2.Append(_opponentScore.ToString("N1"));
                _sb2.Append("cm");
                _opponentScoreText.text = _sb2.ToString();

                if (_myScore.Equals(0) || _opponentScore.Equals(0))
                {
                    if (_myScore.Equals(0))
                    {
                        _myBar.fillAmount = 0;
                        _opponentBar.fillAmount = 1;
                    }
                    else
                    {
                        _myBar.fillAmount = 1;
                        _opponentBar.fillAmount = 0;
                    }
                }
                else
                {
                    float f_sum = _myScore + _opponentScore;
                    _myBar.fillAmount = _myScore / f_sum;
                    _opponentBar.fillAmount = _opponentScore / f_sum;
                }
                break;
            // 무게
            case 1:
                _sb2.Length = 0;
                _sb2.Append(_myScore.ToString("N1"));
                _sb2.Append("kg");
                _myScoreText.text = _sb2.ToString();

                _sb2.Length = 0;
                _sb2.Append(_opponentScore.ToString("N1"));
                _sb2.Append("kg");
                _opponentScoreText.text = _sb2.ToString();

                if (_myScore.Equals(0) || _opponentScore.Equals(0))
                {
                    if (_myScore.Equals(0))
                    {
                        _myBar.fillAmount = 0;
                        _opponentBar.fillAmount = 1;
                    }
                    else
                    {
                        _myBar.fillAmount = 1;
                        _opponentBar.fillAmount = 0;
                    }
                }
                else
                {
                    float f_sum = _myScore + _opponentScore;
                    _myBar.fillAmount = _myScore / f_sum;
                    _opponentBar.fillAmount = _opponentScore / f_sum;
                }
                break;
            // 마릿수
            case 2:
                _sb2.Length = 0;
                _sb2.Append(_myScore.ToString("N0"));
                _sb2.Append("마리");
                _myScoreText.text = _sb2.ToString();

                _sb2.Length = 0;
                _sb2.Append(_opponentScore.ToString("N0"));
                _sb2.Append("마리");
                _opponentScoreText.text = _sb2.ToString();

                //_myScoreText.text = _myScore.ToString("N0");
                //_opponentScoreText.text = _opponentScore.ToString("N0");

                if (_myScore.Equals(0) || _opponentScore.Equals(0))
                {
                    if (_myScore.Equals(0))
                    {
                        _myBar.fillAmount = 0;
                        _opponentBar.fillAmount = 1;
                    }
                    else
                    {
                        _myBar.fillAmount = 1;
                        _opponentBar.fillAmount = 0;
                    }
                }
                else
                {
                    float i_sum = _myScore + _opponentScore;
                    _myBar.fillAmount = _myScore / i_sum;
                    _opponentBar.fillAmount = _opponentScore / i_sum;

                    //Debug.Log(_myScore / i_sum + ", " + _opponentScore / i_sum);
                }
                break;
            // 골드
            case 3:
                //Debug.Log("나: " + _myScore + " , 상대: " + _opponentScore);
                _sb2.Length = 0;
                _sb2.Append(_myScore.ToString("N0"));
                _sb2.Append("Gold");
                _myScoreText.text = _sb2.ToString();

                _sb2.Length = 0;
                _sb2.Append(_opponentScore.ToString("N0"));
                _sb2.Append("Gold");
                _opponentScoreText.text = _sb2.ToString();

                //_myScoreText.text = _myScore.ToString("N0");
                //_opponentScoreText.text = _opponentScore.ToString("N0");

                if (_myScore.Equals(0) || _opponentScore.Equals(0))
                {
                    //Debug.Log(_myIntScore + ", " + _opponentIntScore);

                    if (_myScore.Equals(0))
                    {
                        _myBar.fillAmount = 0;
                        _opponentBar.fillAmount = 1;
                    }
                    else
                    {
                        _myBar.fillAmount = 1;
                        _opponentBar.fillAmount = 0;
                    }
                }
                else
                {
                    float i_sum = _myScore + _opponentScore;
                    _myBar.fillAmount = _myScore / i_sum;
                    _opponentBar.fillAmount = _opponentScore / i_sum;

                    //Debug.Log(_myScore / i_sum + ", " + _opponentScore / i_sum);
                }
                break;
            default:
                break;
        }
    }

    int CheckVictory()
    {
        if (_myScore > _opponentScore)
        {
            return 1;
        }
        else if (_myScore < _opponentScore)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    void CheckStar(int isVictory)
    {
        if (_userData._grade.Equals(9) && _userData._star.Equals(5))
            return;

        //Debug.Log("현재 별 : " + _userData._star);

        // 7단부터는 0.5를 올려야 한다.
        if (isVictory > 0)
        {
            if (_userData._grade < 7)
            {
                //Debug.Log("1점 오름: " + _userData._grade);
                _userData._star += 1;
            }
            else
            {
               // Debug.Log("0.5점 오름: " + _userData._grade);
                _userData._star += 0.5f;
            }
        }
        else if(isVictory < 0)
        {
            if (_userData._star <= 0)
                return;

            if (_userData._grade > 3)
            {
                //Debug.Log("1점 떨어짐: " + _userData._grade);
                _userData._star -= 1;
            }
            else
            {
                //Debug.Log("0.5점 떨어짐: " + _userData._grade);
                _userData._star -= 0.5f;
            }

            if (_userData._star < 0)
                _userData._star = 0;
        }
    }
    void CheckGrade(int isVictory)
    {
        if (_userData._grade.Equals(9) && _userData._star.Equals(5))
            return;

        //Debug.Log("현재 등급 : " + _userData._grade);

        if (isVictory > 0)
        {
            switch (_userData._grade)
            {
                case 1:
                case 2:
                case 3:
                    if (_userData._star >= 3)
                    {
                        _userData._grade++;
                        _userData._star = 0;
                    }
                    break;
                case 4:
                case 5:
                case 6:
                    if (_userData._star >= 4)
                    {
                        _userData._grade++;
                        _userData._star = 0;
                    }
                    break;
                case 7:
                case 8:
                case 9:
                    if (_userData._star >= 5)
                    {
                        _userData._grade++;
                        _userData._star = 0;
                    }
                    break;
                default:
                    Debug.Log("사용자 등급이 잘못되었다.");
                    break;
            }
        }
    }
    public void ClickExitButton()
    {
        CheckStar(-1);
        CheckGrade(-1);
        UpdateData(-1);

        DataManager.INSTANCE._matchGameIsInProgress = false;
        LoadingSceneManager.LoadScene("MapSelectScene");
    }
    
    public void ClickEquipmentButton()
    {
        if (_gameManager.CurrentState.Equals(GameManager.eIngameState.casting) || _gameManager.NeedleInWater || _fishPopupObject.activeSelf)
            return;

        PlayClickEffectAudio();
        _equipmentUI.SetActive(true);
    }

    

    void UpdateData(int result)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("/_grade/", _userData._grade);
        dic.Add("/_star/", _userData._star);

        if (result > 0) 
        {
            _userData._win++;
            _userData._gold += 1000;
            dic.Add("_win", _userData._win);
            dic.Add("_gold", _userData._gold);
        }
        else if (result < 0)
        {
            _userData._lose++;
            dic.Add("_lose", _userData._lose);
        }
        else
        {
            _userData._draw++;
            dic.Add("_draw", _userData._draw);
        }
        DBManager.INSTANCE.UpdateFirebase(dic);
    }
    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }

    IEnumerator MakeDelay(Action action)
    {
        yield return PublicDefined._25secDelay;
        action();
    }
}
