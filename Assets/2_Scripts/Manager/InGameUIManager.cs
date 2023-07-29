using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using System.Text;

public class InGameUIManager : MonoBehaviour
{
    WaitForSeconds delay = PublicDefined._01secDelay;

    readonly int _raiseHash = Animator.StringToHash("Raise");
    readonly int _catchHash = Animator.StringToHash("Catch");
    readonly int _reelingHash = Animator.StringToHash("Reeling");
    readonly int _reelingspeedHash = Animator.StringToHash("ReelingSpeed");
    readonly string _questClearText = "패스 퀘스트 완료! <보상 받기>";

    readonly Vector3 _downVector = new Vector3(0, 0, 0);
    readonly Vector3 _upVector = new Vector3(180, 0, 0);

    // Script
    Reeling _reeling; public Reeling _Reeling { get { return _reeling; } set { _reeling = value; } }
    BookAndSeasonPass _seasonpass; public void GetSeasonpoassInstance(BookAndSeasonPass instance) { _seasonpass = instance; }
    GameManager _gameManager;
    BLETotal bleTotal;
    FishControl fishControl;
    UserData _userData;

    // SerializeField
    [Header("UI")]
    [SerializeField] Image _characterGage;
    public void SetCharacterGage(bool b) { _characterGage.gameObject.SetActive(b); }
    public void ResetCharacterGage(float fillamount = 0) { _characterGage.fillAmount = fillamount; SetCharacterGage(true); }

    [SerializeField] Image _petGage;
    public void SetPetGage(bool b) { _petGage.gameObject.SetActive(b); }
    public void ResetPetGage(float fillamount = 0) { _petGage.fillAmount = fillamount; SetPetGage(true); }

    [SerializeField] GameObject fishSuccess_Eng;
    [SerializeField] GameObject fishFail_Eng;
    [SerializeField] GameObject fishBitting_Eng;
    [SerializeField] GameObject fishBiteSuccess_Eng;
    [SerializeField] GameObject fishBiteFail_Eng;
    [SerializeField] GameObject menuPanel_Kor;
    [SerializeField] GameObject _fishingGearButtonInMenu;
    [SerializeField] GameObject _fishPopup;
    [SerializeField] RectTransform _informationUI;
    [SerializeField] GameObject _gameGuideUI;

    [Header("-----PassQuest 관련-----")]
    [SerializeField] GameObject _questContentObject;
    [SerializeField] Text _currentContentText;
    [SerializeField] Text _currentStateText;
    [SerializeField] GameObject _currentPassObject;
    [SerializeField] GameObject _allClearObject;
    [SerializeField] Sprite _notClearButtonSprite;
    [SerializeField] Sprite _clearButtonSprite;
    [SerializeField] Sprite _goldSprite;

    [Header("물고기 잡았을 때 숨기려는 버튼들")]
    [SerializeField] GameObject[] _buttonObjects;



    // Private
    GameObject _characterGageObject; public GameObject _CharacterGageObject { set { _characterGageObject = value; } }
    Text _depthText_distanceText; public void GetDepthDistanceText(Text text) { _depthText_distanceText = text; }
    RectTransform _depthText_distanceText_RectTransform;
    Coroutine _depth_distance_Coroutine = null;
    Transform _reelPoint2Pos; public Transform _ReelPoint2Pos { set { _reelPoint2Pos = value; } }
    float needleX, needleZ, distance;
    bool _isPassOpen;
    bool _currentQuestState;
    bool _questState = true;
    bool _isPause = false; public bool _ingameUIManager_isPause { get { return _isPause; } set { _isPause = value; } } 
    Image _passObjectImage;
    Button _passObjectButton;
    StringBuilder _sb = new StringBuilder();
    Coroutine _bldcResumeCoroutine = null;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.SetIngameUIManagerInstance(this);

        // 캐릭터 파워게이지 활성화
        _characterGage.gameObject.SetActive(true);
        _userData = DBManager.INSTANCE.GetUserData();

        if (DataManager.INSTANCE._matchGameIsInProgress || DataManager.INSTANCE._tutorialIsInProgress || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.tutorial))
        {
            _isPassOpen = false;
        }
        else
        {
            _isPassOpen = true;
        }
        _questContentObject.SetActive(_isPassOpen);
        _passObjectImage = _questContentObject.GetComponent<Image>();
        _passObjectButton = _questContentObject.GetComponent<Button>();
    }

    void Start()
    {
        // 피쉬컨트롤 컴포넌트 가져옴
        fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        _depthText_distanceText_RectTransform = _depthText_distanceText.GetComponent<RectTransform>();

        // 블루투스 오브젝트 연결
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();
            bleTotal.inGameUIMgr = this;
        }

        if (DataManager.INSTANCE._matchGameIsInProgress || DataManager.INSTANCE._tutorialIsInProgress)
            return;

        // 패스를 전부 다 깬 사람들은 다른 텍스트가 나가야한다.
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                if(_userData._currentJeongdongjinPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.Count))
                {
                    _currentPassObject.SetActive(false);
                    _allClearObject.SetActive(true);
                    PassManager.INSTANCE.SetIngameUIManager(this, true);
                }
                else
                    PassManager.INSTANCE.SetIngameUIManager(this, false);
                break;
            case PublicDefined.eMapType.homerspit:
                if (_userData._currentHomerspitPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.Count))
                {
                    _currentPassObject.SetActive(false);
                    _allClearObject.SetActive(true);
                    PassManager.INSTANCE.SetIngameUIManager(this, true);
                }
                else
                    PassManager.INSTANCE.SetIngameUIManager(this,false);
                break;
            case PublicDefined.eMapType.skyway:
                if (_userData._currentSkywayPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.Count))
                {
                    _currentPassObject.SetActive(false);
                    _allClearObject.SetActive(true);
                    PassManager.INSTANCE.SetIngameUIManager(this, true);
                }
                else
                    PassManager.INSTANCE.SetIngameUIManager(this,false);
                break;
        }
    }


    public void AllClearPassQuest()
    {
        _currentPassObject.SetActive(false);
        _allClearObject.SetActive(true);
        _passObjectImage.sprite = _notClearButtonSprite;
    }

    public void GageBarSet()
    {
        if (_gameManager.NeedleInWater && !_fishPopup.activeSelf)
        {
            StartCoroutine(MakeDelay(4, () => _characterGage.gameObject.SetActive(!_gameManager.NeedleInWater))); 
        }
    }
    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
        switch (delayNumber)
        {
            case 1:
                yield return PublicDefined._05secDelay;
                break;
            case 2:
                yield return PublicDefined._1secDelay;
                break;
            case 3:
                yield return PublicDefined._15secDelay;
                break;
            case 4:
                yield return PublicDefined._2secDelay;
                break;
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }

    // reeling
    public void Reeling()
    {
        int forceY = -30;
        int forceZ;
        float powerX;
        float powerY;
        float powerZ;
        float tempX = _gameManager.GetNeedleControlTransform().position.x;
        float tempY = _gameManager.GetNeedleControlTransform().position.y + 1.5f;
        float tempZ = _gameManager.GetNeedleControlTransform().position.z;

        if (_gameManager.NeedleInWater && !_isPause) // 찌가 물 속이라면
        {
            // reeling 애니메이션을 위해서 추가
            if (distance < 15 && fishControl.IsBite)
            {
                // 캐릭터 애니메이션(Reeling:Trigger)
                _gameManager.SettingCharacterAnimator(_reelingHash);
            }

            if (bleTotal != null && bleTotal.ConnectedMain)
            {
                // 물고기가 바늘에 걸려있고 꽤 멀리 있으면 빨리 당겨진다.
                if (fishControl.IsBite || _gameManager.GetNeedleControlTransform().position.z > 15)
                {
                    forceZ = -48;
                    //forceY = -23;
                }
                else
                {
                    forceZ = -43;
                    //forceY = -30;
                }
            }
            else
            {
                if (fishControl.IsBite || _gameManager.GetNeedleControlTransform().position.z > 15)
                {
                    forceZ = -38;
                    //forceY = -23;
                }
                else
                {
                    forceZ = -33;
                    //forceY = -30;
                }
            }

            powerX = forceZ * tempX / Mathf.Sqrt(Mathf.Pow(tempX, 2) + Mathf.Pow(tempZ, 2)); // 음수 
            powerY = forceY * tempY / Mathf.Sqrt(Mathf.Pow(tempX, 2) + Mathf.Pow(tempY, 2) + Mathf.Pow(tempZ, 2)); // 양수
            powerZ = forceZ * tempZ / Mathf.Sqrt(Mathf.Pow(tempX, 2) + Mathf.Pow(tempZ, 2)); // 음수

            //Debug.Log(powerZ);

            //SoundManager.instance.EffectPlay("Reeling");
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.reeling).GetComponent<AudioPoolObject>().Init();

            if (_gameManager.GetNeedleControlTransform().position.y > -1.8f)
                _gameManager.AddForceToNeedle(powerX, 0, powerZ);
            else
                _gameManager.AddForceToNeedle(powerX, powerY, powerZ);

            // 바늘에 물고기가 물려있는 상황 -> TensionUI에 ReelOn, ReelOff로 옮긴다.
            if (!fishControl.IsBite && !fishControl.IsFighting)
            {
                if (_gameManager.GetNeedleControlTransform().position.z < 5f)
                {
                    //Debug.Log("IngameUIManager/Reeling");
                    if (fishBitting_Eng.activeSelf)
                    {
                        fishBitting_Eng.SetActive(false);
                        fishControl.IsStart = false;
                    }
                    //SoundManager.instance.EffectPlay("Reeling");
                    // 물고기 크기 설정

                    _gameManager.IsPlayingBGM = false;
                    _gameManager.IsReset = true;
                    _gameManager.ResetAction();
                    ResetCharacterGage();
                    DistanceDepthTextOff();

                    // 블루투스가 연결되어있다.
                    if (_gameManager._isConnectedToBluettooth_Main) 
                    {
                        fishControl.MotorStop();
                    }

                    if (fishControl._fishMoveToNeedleCoroutine != null)
                    {
                        fishControl.StopCoroutine(fishControl._fishMoveToNeedleCoroutine);
                        fishControl._fishMoveToNeedleCoroutine = null;
                    }
                }
            }

            // 캐릭터 애니메이션(Reeling:Trigger) 
            _gameManager.SettingCharacterAnimator(_reelingHash);

            if (_gameManager.GettingCharacterAnimator(_reelingspeedHash) <= 1.5f)
            {
                _gameManager.SettingCharacterAnimator(_reelingspeedHash, _gameManager.GettingCharacterAnimator(_reelingspeedHash) + 0.1f);
            }
        }
    }

    public float DistTx()
    {
        needleX = _gameManager.GetNeedleControlTransform().position.x;
        needleZ = _gameManager.GetNeedleControlTransform().position.z;
        distance = _gameManager.GetNeedleControlTransform().position.z - 3.5f;
        return distance;
    }

    public float DepthTx()
    {
        float depth = (_gameManager.GetNeedleControlTransform().position.y + 1.5f) * -1;
        if(depth < 0)
        {
            return 0;
        }
        return depth;
    }

    // 거리, 깊이 체크(바늘이 물 속으로 들어가면 활성화)
    public IEnumerator DDInformation()
    {
        while (true)
        {
            // 거리, 깊이 텍스트
            _sb.Length = 0;
            _sb.Append("거리: ");
            _sb.Append(DistTx().ToString("N2"));
            _sb.Append("m");
            _sb.Append(System.Environment.NewLine);
            _sb.Append("깊이: ");
            _sb.Append(DepthTx().ToString("N2"));
            _sb.Append("m").ToString();

            _depthText_distanceText.text = _sb.ToString();
            
            Vector3 temp = _reelPoint2Pos.transform.position;
            temp.y = (temp.z * 0.07f) - 0.5f;

            Vector3 cross = Vector3.Cross(Vector3.forward.normalized, _reelPoint2Pos.transform.position.normalized);

            //// 왼쪽
            if (cross.y < -0.05f)
            {
                temp.x += (-temp.z * 1.5f) - 0.05f; 
                //Debug.LogError("왼쪽 : " + temp.x);
            }
            else
            {
                temp.x += (temp.z * 1.5f) - 0.05f;
                //Debug.LogError("오른쪽 : " + temp.x);
            }

            // 거리에 따른 크기 설정
            _depthText_distanceText_RectTransform.localScale = new Vector2(temp.z * 0.015f , temp.z * 0.015f);

            Vector3 fishPos = Camera.main.WorldToViewportPoint(temp);

            Vector3 iconPos = Camera.main.ViewportToWorldPoint(fishPos);

            _informationUI.transform.position = fishPos;

            yield return delay;
        }
    }

    public void FishingState(int state)
    {
        //Debug.Log(state);
        switch (state)
        {
            case 0: // 낚시 성공
                fishSuccess_Eng.SetActive(true);
                break;
            case 1: // 낚시 실패
                fishFail_Eng.SetActive(true);
                break;
            case 2: // bitting 중
                fishBitting_Eng.SetActive(true);
                break;
            case 3: // bitting 종료
                fishBitting_Eng.GetComponent<BiteAnim>().OnReset();
                break;
            case 4: // 챔질 성공
                fishBitting_Eng.GetComponent<BiteAnim>().OnReset();
                fishBiteSuccess_Eng.SetActive(true);
                break;
            case 5: // 챔질 실패
                fishBitting_Eng.GetComponent<BiteAnim>().OnReset();
                fishBiteFail_Eng.SetActive(true);
                break;
            
            case 6: // 챔질 성공 끄기
                fishBiteSuccess_Eng.SetActive(false);
                break;
            case 7: // 챔질 실패 끄기
                fishBiteFail_Eng.SetActive(false);
                break;
        }
    }

    public void MoveLobby()
    {
        if (bleTotal != null && bleTotal.ConnectedMain)
        {
            //fishControl.StartCoroutine(fishControl.MotorStop());
            //AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);
            Time.timeScale = 1;
            DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;
            StartCoroutine(HoldOnMoveToLobby());
        }
        else
        {
            //AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene);
            Time.timeScale = 1;
            DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;
            LoadingSceneManager.LoadScene("MapSelectScene");
        }
    }

    IEnumerator HoldOnMoveToLobby()
    {
        yield return PublicDefined._05secDelay;

        if (fishControl._motorStopCoroutine != null)
        {
            fishControl.StopCoroutine(fishControl._motorStopCoroutine);
            fishControl._motorStopCoroutine = null;
        }

        yield return PublicDefined._05secDelay;

        bleTotal.Motor(0, 0);

        LoadingSceneManager.LoadScene("MapSelectScene");
    }

    public void Pause()
    {
        if( _gameManager.IsPause)
        {
            return;
        }

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        AudioManager.INSTANCE.StopReelEffect();

        menuPanel_Kor.SetActive(true);
        _gameManager.IsPause = true;
        fishControl._isPause = true;

        if (_gameManager._currentState.Equals(PublicDefined.IngameState.idle))
        {
            _fishingGearButtonInMenu.SetActive(true);
        }
        else
        {
            _fishingGearButtonInMenu.SetActive(false);
        }

        if(_gameManager._currentState.Equals(PublicDefined.IngameState.fighting))
        {
            if (_characterGageObject.activeSelf)
                _characterGageObject.SetActive(false);
        }

        if (bleTotal != null && bleTotal.ConnectedMain && _gameManager.NeedleInWater)
        {
            fishControl.MotorStop();
        }

        Time.timeScale = 0;
    }

    public void ClickGameGuideButton()
    {
        if (_gameManager.NeedleInWater || _gameManager._currentState != PublicDefined.IngameState.idle)
            return;

        if (_gameGuideUI.activeSelf)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
            _gameGuideUI.SetActive(false);
        }
        else
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
            _gameGuideUI.SetActive(true);
        }
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1;

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        menuPanel_Kor.SetActive(false);
        _gameManager.IsPause = false;
        fishControl._isPause = false;

        if(fishControl._motorStopCoroutine != null)
        {
            fishControl.StopCoroutine(fishControl._motorStopCoroutine);
            fishControl._motorStopCoroutine = null;
        }

        if (_gameManager._currentState.Equals(PublicDefined.IngameState.fighting))
        {
            if (!_characterGageObject.activeSelf)
                _characterGageObject.SetActive(true);
        }

        if (bleTotal != null && bleTotal.ConnectedMain)
        {
            _bldcResumeCoroutine = StartCoroutine(BLDCResumeCoroutine());
        }
    }

    IEnumerator BLDCResumeCoroutine()
    {
        yield return PublicDefined._05secDelay;

        if(fishControl._motorStopCoroutine != null)
        {
            fishControl.StopCoroutine(fishControl._motorStopCoroutine);
            fishControl._motorStopCoroutine = null;
        }

        // 파이팅 상태
        if (_gameManager._currentState.Equals(PublicDefined.IngameState.fighting))
        {
            //Debug.Log("1");
            fishControl.RestartDCCoroutine();
        }
        // 찌가 물 속에 있고 물고기가 문 상태
        else if (_gameManager.NeedleInWater && fishControl.IsStart)
        {
            //Debug.Log("2");
            fishControl.RestartDCCoroutine();
        }
        // 찌가 물 속에 있지만 물고기가 아직 물지 않은 상태
        else if (_gameManager.NeedleInWater)
        {
            //Debug.Log("3");

            // #블루투스
            if (!DataManager.INSTANCE._tutorialIsInProgress)
            {
                fishControl.dc = 0;
                Debug.Log(fishControl._normalBLDC);
                bleTotal.Motor(fishControl._normalBLDC, fishControl.dc);
            }
        }
        // 캐스팅 전 상태는 이미 정지할 때 모터가 멈추기 때문에 그대로
    }

    public void DistanceDepthTextOn()
    {
        _depthText_distanceText.gameObject.SetActive(true);
        _informationUI.gameObject.SetActive(true);

        if(_depth_distance_Coroutine != null)
        {
            StopCoroutine(_depth_distance_Coroutine);
            _depth_distance_Coroutine = null;
        }

        _depth_distance_Coroutine = StartCoroutine(DDInformation());
    }

    public void DistanceDepthTextOff()
    {
        _depthText_distanceText.text = string.Empty;
        _depthText_distanceText.gameObject.SetActive(false);
        _informationUI.gameObject.SetActive(false);

        if(_depth_distance_Coroutine != null)
            StopCoroutine(_depth_distance_Coroutine);

        _depth_distance_Coroutine = null;
    }

    public void SetQuestContent(string content, string currentState)
    {
        _currentContentText.text = content;
        _currentStateText.text = currentState;
    }

    public void QuestClear(bool isClear)
    {
        // isClear에 따라 다른 패턴

        _currentQuestState = isClear; //25 35 410 130 / 25 -60 410 60

        if (isClear)
        {
            // 버튼 활성화
            _passObjectButton.enabled = true;
            
            // 버튼 닫혀있다면 열기
            if (!_isPassOpen)
                _questContentObject.SetActive(true);

            // Sprite 교체
            _passObjectImage.sprite = _clearButtonSprite;

            _currentStateText.text = _questClearText;
        }
        else
        {
            _passObjectButton.enabled = false;

            _questContentObject.SetActive(_isPassOpen);

            _passObjectImage.sprite = _notClearButtonSprite;
        }
    }
    
    public void ClickPassQuestButton()
    {
        if (_gameManager._currentState == PublicDefined.IngameState.fighting)
            return;

        if (_questContentObject.activeSelf)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
            _isPassOpen = false;
            _questContentObject.SetActive(false);
            //_arrowImage.eulerAngles = _downVector;
        }
        else
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
            _isPassOpen = true;
            _questContentObject.SetActive(true);
            //_arrowImage.eulerAngles = _upVector;
        }
    }
    public void PassQuestOff()
    {
        _questState = _questContentObject.activeSelf;

        _questContentObject.SetActive(false);
        //_arrowImage.eulerAngles = _downVector;
    }
    public void SetBackState()
    {
        if(_questState)
        {
            _questContentObject.SetActive(true);
        }
        else
        {
            _questContentObject.SetActive(false);
        }
    }

    // 물고기 잡았을 때를 대비하는 버튼 숨기기, 드러내기 함수
    public void HideButtons()
    {
        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttonObjects[i].SetActive(false);
        }
    }
    
    public void ShowButtons()
    {
        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttonObjects[i].SetActive(true);
        }

        if (!_isPassOpen)
            _questContentObject.SetActive(false);
    }

    // 파이팅 중일 때는 패스 퀘스트 창을 끈다.
    public void HidePassContent()
    {
        _questContentObject.SetActive(false);
    }

    public void ShowPassContent()
    {
        _questContentObject.SetActive(_isPassOpen);
    }

    public void HideGageObject()
    {
        _characterGageObject.SetActive(false);
    }
}