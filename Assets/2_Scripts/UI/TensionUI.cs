using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TensionUI : MonoBehaviour
{
    readonly int _pointRectTr_Y = 35;
    readonly string _rodStr = "rod";
    readonly string _reelStr = "reel";
    readonly int _raiseHash = Animator.StringToHash("Raise");

    // SerializeField 
    [SerializeField] GameObject _fishBitting_Eng;
    [SerializeField] GameObject _yellowGageBar; public void SetGageObject(bool b) { _yellowGageBar.SetActive(b); }
    [SerializeField] GameObject _gageBarParent;
    [SerializeField] GameObject _gageEffect;
    [SerializeField] RectTransform _gageEffectRectTr;
    [SerializeField] GameObject gagePoint;
    [SerializeField] GameObject reelBreak;
    [SerializeField] RectTransform _redGageBarRectTransform; public RectTransform _RedGageBarRectTransform { get { return _redGageBarRectTransform; } }
    [SerializeField] RectTransform _leftSpecialAttackGage;
    [SerializeField] RectTransform _upSpecialAttackGage;
    [SerializeField] RectTransform _rightSpecialAttackGage;
    public RectTransform GetSpecialAttackGage(int number)
    {
        if (number.Equals(0))       return _leftSpecialAttackGage;
        else if (number.Equals(1))  return _upSpecialAttackGage;
        else                        return _rightSpecialAttackGage;
    }
    [SerializeField] GameObject _leftSpecialAttackImageObject;
    [SerializeField] GameObject _upSpecialAttackImageObject;
    [SerializeField] GameObject _rightSpecialAttackImageObject;
    public GameObject GetSpecialAttackImageObject(int number)
    {
        if (number.Equals(0))       return _leftSpecialAttackImageObject;
        else if (number.Equals(1))  return _upSpecialAttackImageObject;
        else                        return _rightSpecialAttackImageObject;
    }

    [SerializeField] Sprite _jeondongjinBG;
    [SerializeField] Sprite _homerspitBG;
    [SerializeField] Sprite _skywayBG;
    [SerializeField] Image _phoneBG1;
    [SerializeField] Image _phoneBG2;
    [SerializeField] Image _phoneBG3;
    [SerializeField] Image _leftArrow;
    [SerializeField] Image _leftFrame;
    [SerializeField] Image _leftBar;
    [SerializeField] Image _upArrow;
    [SerializeField] Image _upFrame;
    [SerializeField] Image _upBar;
    [SerializeField] Image _rightArrow;
    [SerializeField] Image _rightFrame;
    [SerializeField] Image _rightBar;
    [SerializeField] Image _frame;

    // Script
    FishControl _fishControl;
    InGameUIManager _ingameUIManager;
    BLETotal bleTotal;
    Reeling _reeling; public void GetReelingInstance(Reeling instance) { _reeling = instance; }
    GameManager _gameManager;
    
    // Private
    Transform _characterTransform;
    GameObject[] _gageSegment = new GameObject[60];
    Vector2 _minVec;
    Vector2 _maxVec;
    Color _grayColor = new Color(0.6f, 0.6f, 0.6f);
    Color _greenColor = new Color(0.2f, 0.95f, 0.2f);
    Color _whiteColor = Color.white;
    Coroutine reelOffCor;
    Coroutine reelOnCor;
    Coroutine failCor;
    Vector3 _gageVec = new Vector3(0, -10, -5);
    List<Item> _rodItemDB;
    float _gageCondition = 0; public float _GageCondition { get { return _gageCondition; } }
    float _minX; public float _MinX { get { return _minX; } }
    float _maxX; public float _MaxX { get { return _maxX; } }
    float _timer;
    float _colorValue;
    double angle;
    bool isBreak;
    bool isFail; 
    bool _tutorialReelOffStop;
    bool _isTutorial;
    bool _isConnectedBluetooth = false;

    
    private void Awake()
    {
        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        _ingameUIManager._CharacterGageObject = _gageEffect;
        _fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        _fishControl._TensionUI = this;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _tutorialReelOffStop = false;
        _timer = 0;
        _colorValue = 1;

        if (_yellowGageBar.gameObject.activeSelf)
            _yellowGageBar.gameObject.SetActive(false);
    }
    void Start()
    {
        isBreak = false;
        _isTutorial = DataManager.INSTANCE._tutorialIsInProgress;

        _minVec = new Vector2(0, _pointRectTr_Y);
        _maxVec = new Vector2(900, _pointRectTr_Y);

        ResetColor_SpecialAttack();
        _characterTransform = GameObject.FindGameObjectWithTag("Player").transform;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _phoneBG1.sprite = _jeondongjinBG;
                _phoneBG2.sprite = _jeondongjinBG;
                _phoneBG3.sprite = _jeondongjinBG;
                break;
            case PublicDefined.eMapType.homerspit:
                _phoneBG1.sprite = _homerspitBG;
                _phoneBG2.sprite = _homerspitBG;
                _phoneBG3.sprite = _homerspitBG;
                break;
            case PublicDefined.eMapType.skyway:
                _phoneBG1.sprite = _skywayBG;
                _phoneBG2.sprite = _skywayBG;
                _phoneBG3.sprite = _skywayBG;
                break;
            default:
                _phoneBG1.sprite = _jeondongjinBG;
                _phoneBG2.sprite = _jeondongjinBG;
                _phoneBG3.sprite = _jeondongjinBG;
                break;
        }

        _rodItemDB = ItemData.Instance.rodItemDB;

        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();
            // 블루투스가 연결이 되어 있으면 브레이크 UI X
            // 브레이크 항상 true
            if (bleTotal.ConnectedReel)
            {
                _isConnectedBluetooth = true;
                isBreak = true;
                reelBreak.SetActive(false);
            }
        }
        
        for (int i = 0; i < 60; i++)
        {
            _gageSegment[i] = _gageBarParent.transform.GetChild(i).gameObject;
        }
        
    }
    
    public void OnPointerDown()
    {
        isBreak = true;
    }
    public void OnPointerUp()
    {
        isBreak = false;
    }

    // bitting 시 물고기에 따라 노란색 tension게이지 활성화
    public IEnumerator BittingGage(int second1, int second2)
    {
        second1 *= 10;
        second2 *= 10;
        int gageSpeed = 170;
        _yellowGageBar.SetActive(true);
        _redGageBarRectTransform.sizeDelta = new Vector2(0, _pointRectTr_Y); // 에디터상에서 희미한 빨간 줄

        for (int i = 0; i < 60; i++) // second1 ~ second2 만큼 켜진다.
        {
            if (second1 <= i && second2 > i)
                _gageSegment[i].SetActive(true);
            else
                _gageSegment[i].SetActive(false);
        }

        // 크기 세팅
        MaxMinSegment();

        while (_fishControl.IsStart)
        {
            _gageCondition = _redGageBarRectTransform.sizeDelta.x + gageSpeed * Time.deltaTime;

            if (_gageCondition <= 900) // 에디터에서 늘려보니 900이면 꽉 찬다.
                _redGageBarRectTransform.sizeDelta = new Vector2(_gageCondition, _pointRectTr_Y); // 900보다 적으면 계속 늘리고
            else
                _redGageBarRectTransform.sizeDelta = _maxVec; // 꽉 차면 900으로 고정한다.

            yield return null;
        }
    }
    
    // 텐션게이지 Off
    public void ReelExit()
    {
        if (failCor != null)
        {
            _frame.color = Color.white;
              _colorValue = 1;
            _timer = 0;
            StopCoroutine(failCor);
            failCor = null;
        }

        if (_fishBitting_Eng.activeSelf)
        {
            _fishBitting_Eng.SetActive(false);
            _fishControl.IsStart = false;
        }

        
        if (!_gameManager.UserData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
        {
            _gameManager.SetSinkerObjectActive(true);
        }
        
        _gageEffect.SetActive(false);
        _fishControl.IsFighting = false;
        StopCoroutine(reelOnCor);
        StopCoroutine(reelOffCor);
        _yellowGageBar.SetActive(false);

        // #블루투스
        if (bleTotal != null && bleTotal.ConnectedMain)
        {
            if (_gameManager.NeedleInWater)
            {
                if (_fishControl.DcCoroutine != null)
                {
                    //_fishControl.bldc = 15; _fishControl.dc = 0;
                    //bleTotal.Motor(_fishControl.bldc, _fishControl.dc);
                    _fishControl.StopCoroutine(_fishControl.DcCoroutine);
                    _fishControl.DcCoroutine = null;
                    _fishControl.IsDcing = false;

                    bleTotal.Motor(_fishControl.NormalBLDC, 0);
                }
            }
        }
    }

    // 블루투스용 실패확인
    private IEnumerator Failing()
    {
        while (!isFail)
        {
            //yield return PublicDefined._15secDelay;
            if (_timer < 1.5f)
            {
                if (_colorValue > 0)
                {
                    _colorValue -= Time.deltaTime * 0.7f;
                    _frame.color = new Color(1, _colorValue, _colorValue);
                }
                _timer += Time.deltaTime;
            }
            else
            {
                _frame.color = Color.white;
                isFail = true;
            }

            yield return null;
        }
    }

    // reeling X
    private IEnumerator ReelOff(int gageSpeed)
    {
        float gageProgress;
        float time;

        while (_fishControl.IsBite) // 물고기가 미끼를 물었다.
        {
            gageProgress = 0;

            if (!_reeling.IsReeling && !_tutorialReelOffStop) // 안 누르고 있다.
            {
                time = Time.deltaTime;
                //Debug.Log(time);
                gageProgress -= gageSpeed * time;

                if (_fishControl.GetFishMovementDirection(1))
                {// 물고기가 아래로 간다면 
                    gageProgress += 45 * time;
                }
                else
                {
                    gageProgress -= 45 * time;
                }

                if (!_fishControl.GetFishMovementDirection(2))
                {
                    gageProgress += 45 * time;
                }
                else
                {
                    gageProgress -= 45 * time;
                }

                // 미끼와 캐릭터의 각도가 게이지에 영향을 미친다.
                angle = _gameManager.GetNeedleControlTransform().position.x / Mathf.Pow((Mathf.Pow(_gameManager.GetNeedleControlTransform().position.x, 2) 
                    + Mathf.Pow(_gameManager.GetNeedleControlTransform().position.z, 2)), 0.5f) - Math.Sin(_characterTransform.eulerAngles.y * Mathf.PI / 180);

                if (angle < 0)
                    angle *= -4;
                else
                    angle *= 4;

                gageProgress += 15 * Mathf.RoundToInt((float)angle) * time;

                if (isBreak)
                {
                    if(_isConnectedBluetooth)
                    {
                        if (gageProgress > 0)
                            gageProgress *= 1.4f;
                        else
                            gageProgress *= 0.5f;
                    }

                }

                if (_redGageBarRectTransform.sizeDelta.x + gageProgress <= 0)
                    _redGageBarRectTransform.sizeDelta = _minVec;
                else if (_redGageBarRectTransform.sizeDelta.x + gageProgress >= 900)
                    _redGageBarRectTransform.sizeDelta = _maxVec;
                else
                    _redGageBarRectTransform.sizeDelta = new Vector2(_redGageBarRectTransform.sizeDelta.x + gageProgress, _pointRectTr_Y);
                
                _gageVec.x = _redGageBarRectTransform.sizeDelta.x;
                _gageEffectRectTr.anchoredPosition = _gageVec;

                // 텐션 풀림
                if (_redGageBarRectTransform.sizeDelta.x <= 0 && !_isTutorial)
                {
                    if (failCor == null)
                    {
                        failCor = StartCoroutine(Failing());
                    }

                    if (isFail)
                    {
                        Debug.Log(1);
                        _frame.color = Color.white;
                        _colorValue = 1;
                        _timer = 0;
                        StopCoroutine(failCor);
                        failCor = null;
                        isFail = false;
                        ReelExit(); // 릴 종료
                    }
                }
                // 텐션 터짐
                else if (_redGageBarRectTransform.sizeDelta.x >= 900 && !_isTutorial)
                {
                    if (failCor == null)
                        failCor = StartCoroutine(Failing());

                    if (isFail)
                    {
                        Debug.Log(2);
                        _frame.color = Color.white;
                        _colorValue = 1;
                        _timer = 0;
                        StopCoroutine(failCor);
                        failCor = null;
                        isFail = false;
                        ReelExit();
                    }
                }
                // 텐션 정상
                else /*if (pointRectTr.sizeDelta.x > 0 && pointRectTr.sizeDelta.x < 900)*/
                {
                    if (failCor != null)
                    {
                        _frame.color = Color.white;
                        _colorValue = 1;
                        _timer = 0;
                        StopCoroutine(failCor);
                        failCor = null;
                    }
                }

                if (_fishControl.IsBite)
                {
                    if (_gameManager.GetNeedleControlTransform().position.z < 5f)
                    {
                        _fishControl.IsDeath = true; // 여기까지 실행됐는데 다음줄부터 실행이 안된거다.
                        _ingameUIManager.DistanceDepthTextOff();
                        _gameManager.NeedleInWater = false;
                        ReelExit();

                        if (_fishControl.FishTransform != null && _fishControl.IsCatch)
                        {
                            // 캐릭터가 낚싯대 들어올리는 애니메이션
                            _gameManager.SettingCharacterAnimator(_raiseHash, true);
                        }
                    }
                }
            }

            yield return null;
        }
    }

    // reeling O
    private IEnumerator ReelOn(int gageSpeed)
    {
        float gageProgress;
        float time1 = 0, time2;
        while (_fishControl.IsBite)
        {
            if (_reeling.IsReeling)
            {
                // 활성화된 게이지 안에서만 reeling 함수 On
                if (_redGageBarRectTransform.sizeDelta.x >= _minX && _redGageBarRectTransform.sizeDelta.x <= _maxX)
                {
                    _ingameUIManager.Reeling();
                }

                while (time1 <= 0.2f)
                {
                    gageProgress = 0;
                    time2 = Time.deltaTime;

                    if (time1 + time2 > 0.2f)
                        time2 = 0.2f - time1;

                    time1 += Time.deltaTime;
                    angle = _gameManager.GetNeedleControlTransform().position.x / Mathf.Pow((Mathf.Pow(_gameManager.GetNeedleControlTransform().position.x, 2) + Mathf.Pow(_gameManager.GetNeedleControlTransform().position.z, 2)), 0.5f)
                        - Math.Sin(_characterTransform.eulerAngles.y * Mathf.PI / 180);

                    //Debug.Log(_ingameUIManager.needleX / Mathf.Pow((Mathf.Pow(_ingameUIManager.needleX, 2) + Mathf.Pow(_ingameUIManager.needleZ, 2)), 0.5f));
                    // -> -0.5~0.5 물고기가 가운데 기준으로 왼쪽이면 - , 오른쪽이면 + 이다. 거리가 멀수록 숫자가 커진다. 
                    //Debug.Log(Math.Sin(characterTr.eulerAngles.y * Mathf.PI / 180)); 
                    // -> -0.5~0.5 왼쪽 끝을 볼 때 : -0.5 오른쪽 끝 0.5
                    //Debug.Log("angle : " + angle);
                    // -> [예상: 물고기의 위치와 내 각도의 차를 구하는 느낌?] [확인: 맞는 듯. 위의 연산에서 두 수의 차가 크면 (-9 ~ 9) 게이지가 빨리 찬다.

                    // 신형 릴
                    if(_isConnectedBluetooth)
                    {
                        if (angle < 0)
                            angle *= -3;// -4
                        else
                            angle *= 3;// 4

                        if (angle >= 0 && angle < 1)
                            angle = 1;

                        if (_redGageBarRectTransform.sizeDelta.x < _minX)
                        {
                            gageProgress = 3.5f * gageSpeed * time2 * Mathf.RoundToInt((float)angle);
                        }
                        else if(_redGageBarRectTransform.sizeDelta.x > _maxX)
                        {
                            gageProgress = gageSpeed * time2 * Mathf.RoundToInt((float)angle);
                        }
                        else
                        {
                            gageProgress = 1.5f * gageSpeed * time2 * Mathf.RoundToInt((float)angle);
                        }

                        if (_redGageBarRectTransform.sizeDelta.x + gageProgress >= 900)
                            _redGageBarRectTransform.sizeDelta = _maxVec;
                        else
                            _redGageBarRectTransform.sizeDelta = new Vector2(_redGageBarRectTransform.sizeDelta.x + gageProgress, _pointRectTr_Y);
                    }
                    // 구형 릴
                    else
                    {
                        if (angle < 0)
                            angle *= -3;// -4
                        else
                            angle *= 3;// 4

                        if (angle >= 0 && angle < 1)
                            angle = 1;

                        if (!_fishControl.GetFishMovementDirection(1) && !_fishControl.GetFishMovementDirection(2))
                            gageProgress = gageSpeed * time2 * Mathf.RoundToInt((float)angle);
                        else if (_fishControl.GetFishMovementDirection(1) && _fishControl.GetFishMovementDirection(2))
                            gageProgress = 4 * gageSpeed * time2 * Mathf.RoundToInt((float)angle);
                        else
                            gageProgress = 2 * gageSpeed * time2 * Mathf.RoundToInt((float)angle);

                        if (_redGageBarRectTransform.sizeDelta.x + gageProgress >= 900)
                            _redGageBarRectTransform.sizeDelta = _maxVec;
                        else
                            _redGageBarRectTransform.sizeDelta = new Vector2(_redGageBarRectTransform.sizeDelta.x + gageProgress, _pointRectTr_Y);
                    }

                    _gageVec.x = _redGageBarRectTransform.sizeDelta.x;
                    _gageEffectRectTr.anchoredPosition = _gageVec;
                    yield return null;
                }

                time1 = 0;

                if (_redGageBarRectTransform.sizeDelta.x >= 900 && !_isTutorial)
                {
                    if (failCor == null)
                        failCor = StartCoroutine(Failing());

                    if (isFail)
                    {
                        _frame.color = Color.white;
                        _colorValue = 1;
                        _timer = 0;
                        StopCoroutine(failCor);
                        failCor = null;
                        isFail = false;
                        ReelExit();
                    }
                }
                else /*if (pointRectTr.sizeDelta.x < 0 && !_isTutorial)*/
                {
                    if (failCor != null)
                    {
                        _frame.color = Color.white;
                        _colorValue = 1;
                        _timer = 0;
                        StopCoroutine(failCor);
                        failCor = null;
                    }
                }

                if (_fishControl.IsBite)
                {
                    if (_gameManager.GetNeedleControlTransform().position.z < 5f)
                    {
                        _fishControl.IsDeath = true;
                        _ingameUIManager.DistanceDepthTextOff();
                        _gameManager.NeedleInWater = false;
                        ReelExit();

                        if (_fishControl.FishTransform != null && _fishControl.IsCatch)
                        {
                            _gameManager.SettingCharacterAnimator(_raiseHash, true);

                        }
                    }
                }
            }

            yield return null;
        }
    }

    // 무게에 따른 게이지 활성화
    public void WeightGage(float weight, UserData userData)
    {
        int reelOffSpeed = 0;
        int reelOnSpeed = 0;
        double fishWeight;

        Dictionary<string, int> dic = userData.GetCurrentEquipmentDictionary();
        float rodIntensive = _rodItemDB[dic[_rodStr]].intensive;
        float reelIntensive = _rodItemDB[dic[_reelStr]].intensive;
        fishWeight = weight * rodIntensive * reelIntensive;
        _redGageBarRectTransform.sizeDelta = new Vector2(650, _pointRectTr_Y);

        if (_isConnectedBluetooth)
        {
            if (fishWeight < 3)
            {
                fishWeight = 3;
            }
            else if(fishWeight > 7)
            {
                fishWeight = 7;
            }
        }
        
        if (fishWeight <= 2)  // 2kg 이하
        {
            for (int i = 0; i < 60; i++)
            {
                // 텐션 게이지 가운데 활성화
                if (23 < i && i < 36)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 720;
                reelOnSpeed = 75;
            }
        }
        else if (fishWeight <= 3)   // 3kg 이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (20 < i && i < 39)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 640;
                reelOnSpeed = 75; //75
            }
        }
        else if (fishWeight <= 4)   // 4kg이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (17 < i && i < 42)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 560;
                reelOnSpeed = 75; //75
            }
        }
        else if (fishWeight <= 5)   // 5kg 이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (14 < i && i < 45)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 480;
                reelOnSpeed = 113; //113
            }
        }
        else if (fishWeight <= 6)   // 6kg이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (11 < i && i < 48)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 400;
                reelOnSpeed = 113; //113
            }
        }
        else if (fishWeight <= 7) // 7kg이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (14 < i && i < 45)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 320;
                reelOnSpeed = 150;//150
            }
        }
        else if (fishWeight <= 8) // 8kg이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (17 < i && i < 42)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 240;
                reelOnSpeed = 188;//150
            }
        }
        else if (fishWeight <= 9)  // 9kg이하
        {
            for (int i = 0; i < 60; i++)
            {
                if (20 < i && i < 39)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 160;
                reelOnSpeed = 225;//225
            }
        }
        else /*if (fishWeight <= 10)   10kg이상*/
        {
            for (int i = 0; i < 60; i++)
            {
                if (23 < i && i < 36)
                    _gageSegment[i].SetActive(true);
                else
                    _gageSegment[i].SetActive(false);

                reelOffSpeed = 80;
                reelOnSpeed = 263;//225
            }
        }
        //else    // 10kg이상
        //{
        //    for (int i = 0; i < 60; i++)
        //    {
        //        if (26 < i && i < 36)
        //            _gageSegment[i].SetActive(true);
        //        else
        //            _gageSegment[i].SetActive(false);

        //        reelOffSpeed = 0;
        //        reelOnSpeed = 280;//300
        //    }
        //}

        if (_isConnectedBluetooth)
        {
            // 신형 릴
            reelOnSpeed += 14;
            // 구형 릴
            //reelOnSpeed += 75;
        }

        _gageEffect.SetActive(true);
        reelOffCor = StartCoroutine(ReelOff(reelOffSpeed));
        reelOnCor = StartCoroutine(ReelOn(reelOnSpeed));
        MaxMinSegment();
    }
    
    // 특수공격 성공시 텐션 칸 늘림
    public void AddGageSegment() // 맨 왼쪽에서 왼쪽으로 2개, 맨 오른쪽에서 맨 오른쪽으로 2개 
    {
        // 만약 2번째가 켜져 있다면 꽉 찬거다.
        if (_gageSegment[1].activeSelf)
        {
            _gageSegment[0].SetActive(true);

            // 끝에서 2번째가 켜져 있다면 꽉 찬거다.
            if (_gageSegment[58].activeSelf)
            {
                _gageSegment[59].SetActive(true);
                return;
            }
            else
            {
                for (int i = 59; i >= 0; i--)
                {
                    if (_gageSegment[i].activeSelf)
                    {
                        if (!i.Equals(59) && i <= 57)
                        {
                            _gageSegment[i + 1].SetActive(true);
                            _gageSegment[i + 2].SetActive(true);
                        }
                        else if (i.Equals(58))
                            _gageSegment[i + 1].SetActive(true);
                        break;
                    }
                }
                return;
            }
        }
        if (_gageSegment[58].activeSelf)
        {
            _gageSegment[59].SetActive(true);
            if (_gageSegment[1].activeSelf)
            {
                _gageSegment[0].SetActive(true);
                return;
            }
            else
            {
                for (int i = 0; i < 60; i++)
                {
                    if (_gageSegment[i].activeSelf)
                    {
                        if (!i.Equals(0) && i >= 2)
                        {
                            _gageSegment[i - 1].SetActive(true);
                            _gageSegment[i - 2].SetActive(true);
                        }
                        else if (i.Equals(1))
                            _gageSegment[i - 1].SetActive(true);
                        break;
                    }
                }
                return;
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (_gageSegment[i].activeSelf)
            {
                if (!i.Equals(0) && i >= 2)
                {
                    _gageSegment[i - 1].SetActive(true);
                    _gageSegment[i - 2].SetActive(true);
                }
                else if (i.Equals(1))
                    _gageSegment[i - 1].SetActive(true);
                break;
            }
        }
        for (int i = 59; i >= 0; i--)
        {
            if (_gageSegment[i].activeSelf)
            {
                if (!i.Equals(59) && i <= 57)
                {
                    _gageSegment[i + 1].SetActive(true);
                    _gageSegment[i + 2].SetActive(true);
                }
                else if(i.Equals(58))
                    _gageSegment[i + 1].SetActive(true);
                break;
            }
        }
        _minX -= 30; _maxX += 30;
    }

    // 최대, 최소 게이지값 설정
    private void MaxMinSegment()
    {
        for (int i = 0; i < 60; i++) // 켜진 세그먼트 중에 가장 처음에 있는 x좌표를 가져오는 듯
        {
            if (_gageSegment[i].activeSelf)
            {
                _minX = _gageSegment[i].GetComponent<RectTransform>().anchoredPosition.x;
                break;
            }

        }
        for (int i = 59; i >= 0; i--) // 켜진 세그먼트 중에 가장 끝에 있는 (x좌표 + 15)를 가져오는 듯
        {
            if (_gageSegment[i].activeSelf)
            {
                _maxX = _gageSegment[i].GetComponent<RectTransform>().anchoredPosition.x + 15;
                break;
            }

        }
    }
    public void PlayBGM()
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.jeongdongjinBGM, false);
                break;
            case PublicDefined.eMapType.skyway:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.skywayBGM, false);
                break;
            case PublicDefined.eMapType.homerspit:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.homerspitBGM, false);
                break;
        }
    }

    public void ColorChange_SpecialAttack(int number)
    {
        _leftSpecialAttackGage.sizeDelta = Vector2.zero;
        _upSpecialAttackGage.sizeDelta = Vector2.zero;
        _rightSpecialAttackGage.sizeDelta = Vector2.zero;
        switch (number)
        {
            case 0: // 위
                _upArrow.color = _whiteColor;
                _upFrame.color = _whiteColor;
                _upBar.color = _whiteColor;

                _leftArrow.color = _grayColor;
                _leftBar.color = _grayColor;
                _leftFrame.color = _grayColor;

                _rightArrow.color = _grayColor;
                _rightBar.color = _grayColor;
                _rightFrame.color = _grayColor;
                break;
            case 1: // 오
                _upArrow.color = _grayColor;
                _upFrame.color = _grayColor;
                _upBar.color = _grayColor;

                _leftArrow.color = _grayColor;
                _leftBar.color = _grayColor;
                _leftFrame.color = _grayColor;

                _rightArrow.color = _whiteColor;
                _rightBar.color = _whiteColor;
                _rightFrame.color = _whiteColor;
                break;
            case 2: // 왼
                _upArrow.color = _grayColor;
                _upFrame.color = _grayColor;
                _upBar.color = _grayColor;

                _leftArrow.color = _whiteColor;
                _leftBar.color = _whiteColor;
                _leftFrame.color = _whiteColor;

                _rightArrow.color = _grayColor;
                _rightBar.color = _grayColor;
                _rightFrame.color = _grayColor;
                break;
        }
    }
    public void ResetColor_SpecialAttack()
    {
        _leftSpecialAttackGage.sizeDelta = Vector2.zero;
        _upSpecialAttackGage.sizeDelta = Vector2.zero;
        _rightSpecialAttackGage.sizeDelta = Vector2.zero;

        _upArrow.color = _grayColor;
        _upFrame.color = _grayColor;
        _upBar.color = _grayColor;

        _leftArrow.color = _grayColor;
        _leftBar.color = _grayColor;
        _leftFrame.color = _grayColor;

        _rightArrow.color = _grayColor;
        _rightBar.color = _grayColor;
        _rightFrame.color = _grayColor;
    }
    public void Success_SpecialAttack(int number)
    {
        switch (number)
        {
            case 0: // 위
                _upArrow.color = _greenColor;
                //_upFrame.color = _greenColor;
                //_upBar.color = _greenColor;
                break;
            case 1: // 오
                _rightArrow.color = _greenColor;
                //_rightBar.color = _greenColor;
                //_rightFrame.color = _greenColor;
                break;
            case 2: // 왼
                _leftArrow.color = _greenColor;
                //_leftBar.color = _greenColor;
                //_leftFrame.color = _greenColor;
                break;
        }
    }
    public void ResetGuide()
    {
        _upSpecialAttackImageObject.SetActive(false);
        _rightSpecialAttackImageObject.SetActive(false);
        _leftSpecialAttackImageObject.SetActive(false);
    }
}
