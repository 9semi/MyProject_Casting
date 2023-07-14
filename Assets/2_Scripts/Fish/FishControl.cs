using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControl :  FishBase
{
    readonly int _centerHash = Animator.StringToHash("Center");
    readonly int _blockspeedHash = Animator.StringToHash("BlockSpeed");
    readonly int _leftHash = Animator.StringToHash("Left");
    readonly int _rightHash = Animator.StringToHash("Right");
    readonly int _raiseHash = Animator.StringToHash("Raise");
    readonly int _hooksetHash = Animator.StringToHash("HookSet");
    readonly int _catchHash = Animator.StringToHash("Catch");
    readonly Vector3 _diedFishRotate = new Vector3(-90, -180, -30);
    readonly Vector3 _diedFishRotate_octopus = new Vector3(55, 0, 0);
    readonly Vector3 _caughtFIshPos = new Vector3(0.13f, -0.02f, 0.05f);

    PetManager petMgr;
    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }
    PublicDefined.stFishInfo _fishDataForPassCheck;

    public InGameUIManager inGameUIMgr;
    public FishPopUp fishPopUp;

    public GameObject attackSuccess;   // 공격성공 그래픽
    public GameObject attackFail;  // 공격실패 그래픽

    public GameManager gameMgr;
    [HideInInspector] public CharacterManager characterMgr;

    // 바늘
    [HideInInspector] public NeedleControl needleControl;
    [HideInInspector] public Transform target; //NeedleControl에서 Needle의 Transform을 넘겨줬다.
    [HideInInspector] public Transform _needleCenterPos;
    [HideInInspector] public bool isFind = false; // 물고기 한마리라도 찾았으면 나머지 안타게끔
    [HideInInspector] public bool isBite = false; // 물고나서 체력이 0되기전까지
    [HideInInspector] public bool isCatch = false;    // 잡고 나서 팝업으로
    [HideInInspector] public bool isStart = false;    // 챔질 시작 true, 챔질 끝나면 false
    [HideInInspector] public bool isBlue = false; // 블루투스 통신을 위한 것
    [HideInInspector] public bool _isFighting = false; // 텐션게이지 범위 안?
    [HideInInspector] public bool isOver = false;
    [HideInInspector] public bool isDeath = false;    // 잡았을 때

    // 특수 공격 처리
    [HideInInspector] public bool isCenter = false;   // 애니메이션, 특수 공격할 때 낚싯대를 위로 당길 때 true
    [HideInInspector] public bool isLeft = false;   // 애니메이션 특수 공격할 때 낚싯대를 왼쪽으로 당길 때 true
    [HideInInspector] public bool isRight = false;   // 애니메이션 특수 공격할 때 낚싯대를 오른쪽으로 당길 때 true
    [HideInInspector] public bool _isSpecialAttack = false;  // 특수공격 진행중인지
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject arrowUp;
    private Coroutine specialAttackCor;
    bool _firstVib;
    bool _secondVib;
    bool _thirdVib;
    bool _fourthVib;
    int _specialAttackPointRectTr_Y = 38;
    // 텐션 게이지 관련
    [SerializeField] public ParticleSystem reelEft;

    public Coroutine biteCor;
    public Coroutine fightCor;
    public Coroutine dieCor;
    public Coroutine _shutCoroutine;
    public Coroutine _fishMoveToNeedleCoroutine;
    private GameObject fishSkin;
    GameObject _fishObject;
    [HideInInspector] public Transform fishTr;

    public Option option;

    // 희귀 물고기 리스트
    public List<stRareFish> rareFishData = new List<stRareFish>();

    // 블루투스 데이터 관련
    [HideInInspector] public BLETotal bleTotal;
    [HideInInspector] public Coroutine _dcCoroutine;
    [HideInInspector] public bool isX = false, isY = false, isZ = false;
    [HideInInspector] public int _normalBLDC, _fishBLDC, dc;
    [HideInInspector] public float rand;
    float term;
    [HideInInspector] public bool _isDCing;
    [HideInInspector] public int _bldcMax = 0;

    // 바이팅 관련
    public Coroutine bittingGageCor;

    // 파이팅 관련
    TensionUI _tensionUI; public TensionUI _TensionUI { set { _tensionUI = value; } }
    [HideInInspector] public int powerCount = 0;

    Animator _fishAni;

    [HideInInspector] public GameObject _fishResisteAudioObject = null;
    bool isTutorial;
    // tutorial 중에는 기능을 막는다.
    [HideInInspector] public bool _tutorialBittingStop;
    
    [Header("FishObjectManager")]
    public FishObjectManager _jeongdongjinFishManager;
    public FishObjectManagerSkyway _skywayFishManager;
    public FishObjectManagerHomerspit _homerspitFishManager;

    // 자이로 블루투스 
    [HideInInspector] public bool _isConnectedToBluetooth = false;
    [HideInInspector] public bool _bittingPermission;
    [HideInInspector] public ReelBlueToothData _reelData;

    [Header("매치 모드일 때")]
    public GameObject _currentMatchStateObject;
    [HideInInspector] bool _isMatchMode;
    [HideInInspector] public bool _isPause; // 매치모드 끝날 때 전부 멈춰야 한다. 특수 공격 소리가 계속 나는 상황이 발생해서 막아야 한다.

    UserData _userData;
    public Coroutine _motorStopCoroutine;

    private void Awake()
    {
        _tutorialBittingStop = false;
    }

    void Start()
    {
        isTutorial = DataManager.INSTANCE._tutorialIsInProgress;
        _isMatchMode = DataManager.INSTANCE._matchGameIsInProgress;
        characterMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
        petMgr = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>();
        fightCor = null;
        biteCor = null;
        _shutCoroutine = null;
        _userData = DBManager.INSTANCE.GetUserData();

        // 블루투스 오브젝트 연결
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();

            if (bleTotal._connectedMain)
            {
                _isConnectedToBluetooth = true;
                _reelData = new ReelBlueToothData();
                bleTotal.fishControl = this;
            }
        }
    }

    public IEnumerator FishMovetoNeedle()
    {
        while (true)
        {
            fishTr.position = _needleCenterPos.position;
            yield return null;
        }
    }
    private int BLDC(bool isY, bool isZ)
    {
        int add = 0;

        if (isY)
            add += 7;
        else
            add -= 7;

        if (isZ)
            add += 7;

        if (_fishBLDC + add > 99)
            return 99;
        else
            return _fishBLDC + add;
    }

    // 활성도는 0 ~ 3
    public IEnumerator DC()
    {
        switch (_fishData._motorType)
        {
            case 0:
                dc = 0;
                break;
            case 1:
                while (isStart || isBite || isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 10f);
                    dc = 0;
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    dc = UnityEngine.Random.Range(50, 71);
                    rand = UnityEngine.Random.Range(1f, 4f);
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);
                }
                break;
            case 2:
                while (isStart || isBite || isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 8f);
                    dc = 0;
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    dc = UnityEngine.Random.Range(65, 86);
                    rand = UnityEngine.Random.Range(1f, 4f);
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);

                }
                break;
            case 3:
                while (isStart || isBite || isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 5f);
                    dc = 0;
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    dc = UnityEngine.Random.Range(80, 100);
                    rand = UnityEngine.Random.Range(1, 4);
                    bleTotal.Motor(_fishBLDC, dc);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);
                }
                break;
        }
    }

    #region bitting
    public void OnBite(int second1, int second2, int chance1, int chance2)
    {
        if(!_isPause)
            biteCor = StartCoroutine(Bitting(second1, second2, chance1, chance2));
    }
    public void OnFight()
    {
        fightCor = StartCoroutine(Fighting());
    }
    private void OnDie()
    {
        dieCor = StartCoroutine(Dying());
    }
    public void OnShutInEdge()
    {
        _shutCoroutine = StartCoroutine(ShutInEdge());
    }

    // 잡은 물고기 데이터 가져오는 함수
    public void SetFish(GameObject fishObject, GameObject _fishSkin, Transform _fishTr, stFishData fishData)
    {
        _fishObject = fishObject;
        fishSkin = _fishSkin;
        fishTr = _fishTr;
        _fishData = fishData;
        _fishAni = _fishTr.GetComponent<Animator>();

        _fishDataForPassCheck = new PublicDefined.stFishInfo(_fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._gradeType);
    }
    public IEnumerator ShutInEdge()
    {
        while(!isDeath)
        {
            if (target.position.z < 7 && powerCount < 4 && !isTutorial) // 물고기가 나랑 가까울 때
            {
                gameMgr.AddForceToNeedle(0, 0, 200);
            }
            else if (target.position.x < -target.position.z)
            {
                gameMgr.AddForceToNeedle(120, 0, 0);
            }
            else if (target.position.x > target.position.z)
            {
                gameMgr.AddForceToNeedle(-120, 0, 0);
            }
            else if (target.position.z > 60)
            {
                gameMgr.AddForceToNeedle(0, 0, -200);
            }
            else if (target.position.y > -3f) // 물고기가 해수면이랑 가까울 때
            {
                gameMgr.AddForceToNeedle(0, -180, 0);
            }
            yield return PublicDefined._02secDelay;
        }
    }
    // 찌를 물었고 챔질까지 성공한 상태
    public IEnumerator Fighting()
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
            case PublicDefined.eMapType.tutorial:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.jeongdongjinFighting, true);
                break;
            case PublicDefined.eMapType.skyway:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.skywayFighting, true);
                break;
            case PublicDefined.eMapType.homerspit:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.homerspitFighting, true);
                break;
        }
        gameMgr._currentState = PublicDefined.IngameState.fighting;
        gameMgr.isNobite = true; // 물고기가 물었다.
        petMgr.isCatch = true;

        if (petMgr._itemUIState.Equals(PetManager.eItemUIState._on))
        {
            petMgr.ClickItemButton();
        }
        isDeath = false;
        _isFighting = true;
        fishSkin.SetActive(true);   // 물고기 그래픽 On
        gameMgr.fishObj = fishTr.GetChild(0).GetChild(0).transform;   // 낚시대 휘어지게 만들어줄려고
        //needleControl.myRd.constraints = RigidbodyConstraints.None;
        needleControl.StopCoroutine(needleControl.needleCor); // 바늘 가라앉기 중지
        needleControl.needleCor = null;
        _tensionUI.WeightGage(_fishData._weight, _userData);

        OnDie();

        // #블루투스
        if (bleTotal != null && bleTotal.ConnectedMain) // 블루투스와 연결이 되어있는지 확인한다.
        { // 블루투스와 연결이 잘 되어 있다면 기기?의 명령을 수행한다.
            _fishBLDC = (int)(_fishData._weight * 10) + 20;

            if (_fishBLDC > 85)
                _fishBLDC = 85;

            _dcCoroutine = StartCoroutine(DC());
        }

        while (!isDeath)    // 죽기전까지 계속 반복
        {
            term = UnityEngine.Random.Range(3f, 4.5f);

            FightPower(_fishData._activityType, term);

            // #블루투스
            if (bleTotal != null && bleTotal.ConnectedMain && !isTutorial)
            {
                bleTotal.Motor(BLDC(isY, isZ), dc);
            }

            yield return ReturnRandomDelay();
        }
    }

    WaitForSeconds ReturnRandomDelay()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        if (rand.Equals(0)) return PublicDefined._3secDelay;
        else if(rand.Equals(1)) return PublicDefined._35secDelay;
        else return PublicDefined._4secDelay;
    }

    private void FightPower(int type, float power)
    {
        if (isTutorial)
            return;

        int randX, randY, randZ;
        int x, y, z; 
        int yPower, zPower;

        randX = UnityEngine.Random.Range(0, 2);
        randY = UnityEngine.Random.Range(0, 10);
        randZ = UnityEngine.Random.Range(0, 10);

        x = 0; y = 0; z = 0;

        fishTr.eulerAngles = new Vector3(0, 180, 0);

        // 물고기가 가는 방향의 파워와 물고기의 각도 설정
        if (randX.Equals(0))
        {
            x = UnityEngine.Random.Range(-35, -25);
        }
        else
        {
            x = UnityEngine.Random.Range(25, 35);
        }

        yPower = UnityEngine.Random.Range(5, 7); // 5 ~ 20이었음
        zPower = UnityEngine.Random.Range(25, 45);

        switch (type)
        {
            case 0: // 기본
                if (randY < 4)
                {
                    y = yPower - 2;
                    isY = false;
                    fishTr.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    fishTr.Rotate(0, 0, -30);
                }
                if (randZ < 6)
                {
                    z = zPower;
                    isZ = true;
                }
                else
                {
                    z = 0;
                    isZ = false;
                }
                break;
            case 1: // O
                if (randY < 5)
                {
                    y = yPower - 2;
                    isY = false;
                    fishTr.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    fishTr.Rotate(0, 0, -30);
                }
                if (randZ < 2)
                {
                    z = zPower;
                    isZ = true;
                }
                else
                {
                    z = 0;
                    isZ = false;
                }
                break;
            case 2: // z+
                if (randY < 4)
                {
                    y = yPower - 2;
                    isY = false;
                    fishTr.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    fishTr.Rotate(0, 0, -30);
                }
                if (randZ < 8)
                {
                    z = zPower;
                    isZ = true;
                }
                else
                {
                    z = 0;
                    isZ = false;
                }
                break;
            case 3:  // y+
                if (randY < 8)
                {
                    y = yPower - 2;
                    isY = false;
                    fishTr.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    fishTr.Rotate(0, 0, -30);
                }
                if (randZ < 5)
                {
                    z = zPower;
                    isZ = true;
                }
                else
                {
                    z = 0;
                    isZ = false;
                }
                break;
            case 4: // y-
                if (randY < 2)
                {
                    y = yPower - 2;
                    isY = false;
                    fishTr.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    fishTr.Rotate(0, 0, -30);
                }
                if (randZ < 5)
                {
                    z = zPower;
                    isZ = true;
                }
                else
                {
                    z = 0;
                    isZ = false;
                }
                break;
        }

        // 특수공격 처리
        Vector3 cross = Vector3.Cross(Vector3.forward.normalized, needleControl.myTr.position.normalized);

        float xPower = 0;

        // 왼쪽으로 간다면
        if (randX.Equals(0))
            xPower = x * power - target.position.z;
        else
            xPower = x * power + target.position.z;

        if (!z.Equals(0))
            xPower *= 0.75f;

        if(powerCount > 2)
            gameMgr.AddForceToNeedle(xPower, y * power, z * power * 0.8f);
        else
            gameMgr.AddForceToNeedle(xPower, y * power, z * power);

        powerCount++;

        // 물고기가 앞으로 나아간다면 특수공격은 하지 않는다.
        // 물고기가 너무 가까우면 안한다.
        if (z > 0 || _isSpecialAttack || needleControl.myTr.position.z < 9.5f)
            return;

        // 물고기가 아래로 내려가거나 물고기가 기준선 가운데에 있다면 위로
        if ((y < 0) || (needleControl.myTr.position.x > -0.25f && needleControl.myTr.position.x < 0.25f))
        {
            SpecialAttack(0);
        }
        // 물고기가 오른쪽에 있다면
        else if (cross.y > 0)
        {
            // 오른쪽에 있지만 기준선에 가깝고 왼쪽으로 움직인다면
            if(needleControl.myTr.position.z < 20)
            {
                if (needleControl.myTr.position.x < 1.5f && x < 0)
                {
                    // 오
                    SpecialAttack(1);
                }
                else
                {
                    // 왼
                    SpecialAttack(2);
                }
            }
            else
            {
                if (needleControl.myTr.position.x < 0.5f && x < 0)
                {
                    // 오
                    SpecialAttack(1);
                }
                else
                {
                    // 왼
                    SpecialAttack(2);
                }
            }
        }
        // 물고기가 왼쪽에 있다면
        else if(cross.y < 0)
        {
            // 왼쪽에 있지만 기준선에 가깝고 오른쪽으로 움직인다면
            if (needleControl.myTr.position.z < 20)
            {
                if (needleControl.myTr.position.x > -1.5f && x > 0)
                {
                    // 왼

                    SpecialAttack(2);
                }
                else
                {
                    // 오

                    SpecialAttack(1);
                }
            }
            else
            {
                if (needleControl.myTr.position.x > -0.5f && x > 0)
                {
                    // 왼

                    SpecialAttack(2);
                }
                else
                {
                    // 오
                    SpecialAttack(1);
                }
            }
        }
    }

    public void SpecialAttack(int direct)
    {
        specialAttackCor = StartCoroutine(SpecialAttackCoroutine(direct));
    }

    // 특수공격 코루틴(가속도 센서로 구현되어 있음) 
    public IEnumerator SpecialAttackCoroutine(int direct)
    {
        float specialAttackGageX = 0;
        float time = 0;
        float successCount = 0;
        float successStandard;
        bool _isSpecialAttackSuccess = false;
        _isSpecialAttack = true;
        inGameUIMgr.FishingState(6);
        _tensionUI.ColorChange_SpecialAttack(direct);

        switch (direct)
        {
            case 0:
                successStandard = -0.6f;

                if (DataManager.INSTANCE._vibration)
                    Vibration.Vibrate(900);

                _tensionUI.GetSpecialAttackImageObject(1).SetActive(true);

                arrowUp.SetActive(true);
                // 위로 움직여야 한다.
                while (time <= 4f)
                {
                    if(_isConnectedToBluetooth)
                    {
                        // 신형 릴
                        {
                            if (_reelData.Xa > 700 && !_isPause)
                            {
                                // 캐릭터 애니메이션(Center:true)
                                if (successCount.Equals(0))
                                {
                                    gameMgr.SettingCharacterAnimator(_centerHash, true);
                                    gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                    isCenter = true;
                                }

                                if (successCount >= 2f) // 특수 공격 성공
                                {
                                    _isSpecialAttackSuccess = true;
                                    _tensionUI.Success_SpecialAttack(direct);
                                }
                                successCount += Time.deltaTime;
                            }
                        }
                    }
                    else
                    {
                        if ((Input.acceleration.y < successStandard || Input.GetKey(KeyCode.W)) && !_isPause)
                        {
                            // 캐릭터 애니메이션(Center:true)
                            if (successCount.Equals(0))
                            {
                                gameMgr.SettingCharacterAnimator(_centerHash, true);
                                gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                isCenter = true;
                            }

                            successCount += Time.deltaTime;

                            if (successCount >= 2f) // 특수 공격 성공
                            {
                                _isSpecialAttackSuccess = true;
                                _tensionUI.Success_SpecialAttack(direct);
                            }
                            if (!_firstVib)
                            {
                                if (successCount >= 0.5f)
                                {
                                    _firstVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_secondVib)
                            {
                                if (successCount >= 1f)
                                {
                                    _secondVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_thirdVib)
                            {
                                if (successCount >= 1.5f)
                                {
                                    _thirdVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_fourthVib)
                            {
                                if (successCount >= 2f)
                                {
                                    _fourthVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }

                        }
                    }
                    
                    time += Time.deltaTime;

                    specialAttackGageX = successCount * 98f;

                    if (specialAttackGageX > 196)
                        specialAttackGageX = 196f;

                    _tensionUI.GetSpecialAttackGage(1).sizeDelta = new Vector2(specialAttackGageX, _specialAttackPointRectTr_Y);
                    yield return null;
                }
                break;
            case 1: // 오른쪽
                successStandard = 0.2f;

                if (DataManager.INSTANCE._vibration)
                    Vibration.Vibrate(900);

                _tensionUI.GetSpecialAttackImageObject(2).SetActive(true);

                arrowRight.SetActive(true);

                while (time <= 4f)
                {
                    if(_isConnectedToBluetooth)
                    {
                        // 신형 릴
                        {
                            if (_reelData.Za < -500 && !_isPause)
                            {
                                //Debug.Log("600보다 커야한다. " + bleTotal._reelData.Za);
                                gameMgr._isPause = true;

                                successCount += Time.deltaTime;

                                if (successCount.Equals(0))
                                {
                                    gameMgr.SettingCharacterAnimator(_leftHash, true);
                                    gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                    isLeft = true;
                                }

                                if (successCount >= 2f)
                                {
                                    _isSpecialAttackSuccess = true;
                                    _tensionUI.Success_SpecialAttack(direct);
                                }
                            }
                            else
                            {
                                gameMgr._isPause = false;
                            }
                        }
                    }
                    else
                    {
                        if ((Input.acceleration.x > successStandard || Input.GetKey(KeyCode.D)) && !_isPause)
                        {
                            gameMgr._isPause = true;

                            if (successCount.Equals(0))
                            {
                                gameMgr.SettingCharacterAnimator(_leftHash, true);
                                gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                isLeft = true;
                            }

                            successCount += Time.deltaTime;

                            if (successCount >= 2f)
                            {
                                _isSpecialAttackSuccess = true;
                                _tensionUI.Success_SpecialAttack(direct);
                            }
                            if (!_firstVib)
                            {
                                if (successCount >= 0.5f)
                                {
                                    _firstVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_secondVib)
                            {
                                if (successCount >= 1f)
                                {
                                    _secondVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_thirdVib)
                            {
                                if (successCount >= 1.5f)
                                {
                                    _thirdVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }
                            else if (!_fourthVib)
                            {
                                if (successCount >= 2f)
                                {
                                    _fourthVib = true;
                                    Vibration.Vibrate(200);
                                }
                            }

                        }
                        else
                        {
                            gameMgr._isPause = false;
                        }
                    }
                    time += Time.deltaTime;

                    specialAttackGageX = successCount * 98f;

                    if (specialAttackGageX > 196)
                        specialAttackGageX = 196f;

                    _tensionUI.GetSpecialAttackGage(2).sizeDelta = new Vector2(specialAttackGageX, _specialAttackPointRectTr_Y);
                    yield return null;
                }

                gameMgr._isPause = false;
                break;
            case 2: // 왼쪽
                successStandard = -0.2f;

                if (DataManager.INSTANCE._vibration)
                    Vibration.Vibrate(900);

                _tensionUI.GetSpecialAttackImageObject(0).SetActive(true);

                arrowLeft.SetActive(true);
                while (time <= 4f)
                {
                    if(_isConnectedToBluetooth)
                    {
                        // 신형 릴
                        {
                            if (_reelData.Za > 500 && !_isPause)
                            {
                                //Debug.Log("-650보다 작아야한다. " + bleTotal._reelData.Za);
                                gameMgr._isPause = true;
                                if (successCount.Equals(0))
                                {
                                    gameMgr.SettingCharacterAnimator(_rightHash, true);
                                    gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                    isRight = true;
                                }

                                if (successCount >= 2f)
                                {
                                    _isSpecialAttackSuccess = true;
                                    _tensionUI.Success_SpecialAttack(direct);
                                }

                                successCount += Time.deltaTime;
                            }
                            else
                            {
                                gameMgr._isPause = false;
                            }
                        }
                    }
                    else
                    {
                        if ((Input.acceleration.x < successStandard || Input.GetKey(KeyCode.A)) && !_isPause)
                        {
                            gameMgr._isPause = true;

                            if (successCount.Equals(0))
                            {
                                gameMgr.SettingCharacterAnimator(_rightHash, true);
                                gameMgr.SettingCharacterAnimator(_blockspeedHash, 1);
                                isRight = true;
                            }

                            successCount += Time.deltaTime;

                            if (successCount >= 2f)
                            {
                                _isSpecialAttackSuccess = true;
                                _tensionUI.Success_SpecialAttack(direct);
                            }

                            if (DataManager.INSTANCE._vibration)
                            {
                                if (!_firstVib)
                                {
                                    if (successCount >= 0.5f)
                                    {
                                        _firstVib = true;
                                        Vibration.Vibrate(200);
                                    }
                                }
                                else if (!_secondVib)
                                {
                                    if (successCount >= 1f)
                                    {
                                        _secondVib = true;
                                        Vibration.Vibrate(200);
                                    }
                                }
                                else if (!_thirdVib)
                                {
                                    if (successCount >= 1.5f)
                                    {
                                        _thirdVib = true;
                                        Vibration.Vibrate(200);
                                    }
                                }
                                else if (!_fourthVib)
                                {
                                    if (successCount >= 2f)
                                    {
                                        _fourthVib = true;
                                        Vibration.Vibrate(200);
                                    }
                                }
                            }
                        }
                        else
                        {
                            gameMgr._isPause = false;
                        }
                    }
                    time += Time.deltaTime;

                    specialAttackGageX = successCount * 98f;

                    if (specialAttackGageX > 196)
                        specialAttackGageX = 196f;

                    _tensionUI.GetSpecialAttackGage(0).sizeDelta = new Vector2(specialAttackGageX, _specialAttackPointRectTr_Y);
                    yield return null;
                }

                gameMgr._isPause = false;
                break;
        }

        gameMgr._isPause = false;

        _tensionUI.GetSpecialAttackImageObject(1).SetActive(false);
        _tensionUI.GetSpecialAttackImageObject(2).SetActive(false);
        _tensionUI.GetSpecialAttackImageObject(0).SetActive(false);
        _firstVib = false;
        _secondVib = false;
        _thirdVib = false;
        _fourthVib = false;

        // 특수 공격 성공
        if (_isSpecialAttackSuccess)
        {
            _tensionUI.AddGageSegment();
            _tensionUI.Success_SpecialAttack(direct);
            StartCoroutine(SpecialAttackSuccessCoroutine());
            //_isSpecialAttack = false;
            attackSuccess.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
        }
        // 특수 공격 실패
        else
        {
            _tensionUI.ResetColor_SpecialAttack();
            _isSpecialAttack = false;
            attackFail.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
        }

        switch (direct)
        {
            case 0:
                // 캐릭터 애니메이션(Center:false)
                gameMgr.SettingCharacterAnimator(_centerHash, false);
                isCenter = false;
                arrowUp.SetActive(false);
                break;
            case 1:
                gameMgr.SettingCharacterAnimator(_leftHash, false);
                isLeft = false;
                arrowRight.SetActive(false);
                break;
            case 2:
                gameMgr.SettingCharacterAnimator(_rightHash, false);
                isRight = false;
                arrowLeft.SetActive(false);
                break;
        }

        StopCoroutine(specialAttackCor);
    }

    IEnumerator SpecialAttackSuccessCoroutine()
    {
        yield return PublicDefined._05secDelay;

        _isSpecialAttack = false;
        _tensionUI.ResetColor_SpecialAttack();

        yield return null;
    }

    // bitting 단계(챔질 chance1 나머지 chance2, 6초 지나면 놓침)
    public IEnumerator Bitting(int second1, int second2, int chance1, int chance2)
    {
        float vib = 0;  // 진동
        int randChance;

        gameMgr.StopHookSet();

        isStart = true; // 챔질 시작
        isOver = false; // 챔질 끝
        inGameUIMgr.FishingState(2);
        inGameUIMgr.HidePassContent();
        
        if(petMgr._itemUIState.Equals(PetManager.eItemUIState._on))
        {
            petMgr.ClickItemButton();
        }

        if (_isMatchMode)
            _currentMatchStateObject.SetActive(false);

        AudioManager.INSTANCE.SaveBGMPlayerCurrentTime();
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.bitting, true);
        bittingGageCor = StartCoroutine(_tensionUI.BittingGage(second1, second2));
        //_tensionUI._gageEffect.SetActive(false);
        _tensionUI.ResetColor_SpecialAttack();
        _tensionUI.ResetGuide();


        // #블루투스
        if (bleTotal != null && bleTotal.ConnectedMain) // 블루투스와 연결이 되어있는지 확인한다.
        { // 블루투스와 연결이 잘 되어 있다면 기기?의 명령을 수행한다.
            _fishBLDC = (int)(_fishData._weight * 10) + 20;

            if (_fishBLDC > 85)
                _fishBLDC = 85;

            bleTotal.Motor(_fishBLDC, 0);
        }
        while (_tensionUI._RedGageBarRectTransform.sizeDelta.x < 900 && isStart && !_isPause) // 빨간색 게이지가 꽉 차지 않았다면
        {
            // 블루투스 O
            if (_isConnectedToBluetooth)
            {
                // 신형 릴
                {
                    if (_reelData.Zg > 20000 && !_tutorialBittingStop)
                    {
                        gameMgr.SettingCharacterAnimator(_hooksetHash);
                        randChance = UnityEngine.Random.Range(0, 10);

                        // 빨간색 텐션게이지가 노란색 텐션게이지 안에 있는지 확인
                        if (_tensionUI._RedGageBarRectTransform.sizeDelta.x >= _tensionUI._MinX && _tensionUI._RedGageBarRectTransform.sizeDelta.x <= _tensionUI._MaxX)
                        {
                            // 성공
                            if (randChance < chance1 && gameMgr.needleControl.GetNeedleControlTransform().position.z > 5.5f)
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                                inGameUIMgr.FishingState(3);
                                inGameUIMgr.FishingState(4);
                                isStart = false;
                                isBite = true;
                                _fishMoveToNeedleCoroutine = StartCoroutine(FishMovetoNeedle());
                                OnFight();
                                OnShutInEdge();

                                // 미끼 제거
                                if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
                                {
                                    DataManager.INSTANCE.RemoveBaitWhenBaitUse();
                                }

                                StopCoroutine(biteCor);
                                yield return null;
                            }
                            // 실패
                            else
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                                isStart = false;

                                if (bleTotal != null && bleTotal.ConnectedMain)
                                {
                                    dc = 0;
                                    bleTotal.Motor(_normalBLDC, dc);
                                }

                            }
                        }
                        // 게이지 밖
                        else
                        {
                            if (randChance < chance2 && gameMgr.needleControl.GetNeedleControlTransform().position.z > 5.5f)
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                                inGameUIMgr.FishingState(3);
                                inGameUIMgr.FishingState(4);
                                isStart = false;
                                isBite = true;
                                OnFight();
                                OnShutInEdge();
                                _fishMoveToNeedleCoroutine = StartCoroutine(FishMovetoNeedle());
                                if (DataManager.INSTANCE._vibration)
                                    Vibration.Vibrate(1000);

                                // 미끼 제거
                                if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
                                {
                                    DataManager.INSTANCE.RemoveBaitWhenBaitUse();
                                }

                                StopCoroutine(biteCor);
                                yield return null;
                            }
                            else
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                                isStart = false;
                                dc = 0;
                                bleTotal.Motor(_normalBLDC, dc);

                                if (DataManager.INSTANCE._vibration)
                                    Vibration.Vibrate(300);
                            }
                        }
                    }
                }
            }
            // 블루투스 X
            else
            {
                if ((gameMgr.angleGyro.x > 7 || (bleTotal != null && gameMgr.angleGyro.x > 4) || Input.GetButtonDown("Jump")) && !_tutorialBittingStop)
                {
                    gameMgr.SettingCharacterAnimator(_hooksetHash);
                    randChance = UnityEngine.Random.Range(0, 10);

                    // 빨간색 텐션게이지가 노란색 텐션게이지 안에 있는지 확인
                    if (_tensionUI._RedGageBarRectTransform.sizeDelta.x >= _tensionUI._MinX && _tensionUI._RedGageBarRectTransform.sizeDelta.x <= _tensionUI._MaxX)
                    {
                        // 성공
                        if (randChance < chance1 && gameMgr.needleControl.GetNeedleControlTransform().position.z > 5.5f)
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                            inGameUIMgr.FishingState(3);
                            inGameUIMgr.FishingState(4);
                            isStart = false;
                            isBite = true;
                            _fishMoveToNeedleCoroutine = StartCoroutine(FishMovetoNeedle());
                            OnFight();
                            OnShutInEdge();

                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(1000);

                            // 미끼 제거
                            if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
                            {
                                DataManager.INSTANCE.RemoveBaitWhenBaitUse();
                            }

                            StopCoroutine(biteCor);
                            yield return null;
                        }
                        // 실패
                        else
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                            isStart = false;

                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(300);
                        }
                    }
                    // 게이지 밖
                    else 
                    {
                        if (randChance < chance2 && gameMgr.needleControl.GetNeedleControlTransform().position.z > 5.5f)
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                            inGameUIMgr.FishingState(3);
                            inGameUIMgr.FishingState(4);
                            isStart = false;
                            isBite = true;
                            OnFight();
                            OnShutInEdge();
                            _fishMoveToNeedleCoroutine = StartCoroutine(FishMovetoNeedle());
                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(1000);

                            // 미끼 제거
                            if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
                            {
                                DataManager.INSTANCE.RemoveBaitWhenBaitUse();
                            }

                            StopCoroutine(biteCor);
                            yield return null;
                        }
                        else
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                            isStart = false;

                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(300);
                        }
                    }
                }
            }
            if (_tensionUI._GageCondition >= vib && DataManager.INSTANCE._vibration)
            {
                Vibration.Vibrate(100); // drehzr.tistory.com/751 유니티 진동 관련된 함수 사용법
                vib += 64f;
            }
            yield return null;
        }

        // 미끼 제거
        if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
        {
            DataManager.INSTANCE.RemoveBaitWhenBaitUse();
        }

        // 시간이 지나서 물고기를 놓쳤다.
        StopCoroutine(bittingGageCor);
        bittingGageCor = null;
        isStart = false;
        PlayBGMWhenBittingFail();
        isOver = false;
        isBite = false;

        if (!isBite)
        {
            // #블루투스
            if (bleTotal != null && bleTotal.ConnectedMain)
            {
                bleTotal.Motor(_normalBLDC, dc);
            }

            inGameUIMgr.FishingState(3);
            inGameUIMgr.FishingState(5);
           
            switch(DataManager.INSTANCE._mapType)
            {
                case PublicDefined.eMapType.jeongdongjin:
                    fishTr.GetComponent<Fish>().Restart();
                    break;
                case PublicDefined.eMapType.skyway:
                    fishTr.GetComponent<FishSkyway>().Restart();
                    break;
                case PublicDefined.eMapType.homerspit:
                    fishTr.GetComponent<FishHomerspit>().Restart();
                    break;
            }

            if (_fishMoveToNeedleCoroutine != null)
            {
                StopCoroutine(_fishMoveToNeedleCoroutine);
                _fishMoveToNeedleCoroutine = null;
            }

            isOver = false;

            yield return PublicDefined._2secRealDelay;

            if (_isMatchMode)
                _currentMatchStateObject.SetActive(true);
            else
                inGameUIMgr.ShowPassContent();

            _tensionUI.SetGageObject(false);
            isFind = false;
        }
        StopCoroutine(biteCor);
    }

    public IEnumerator Dying()
    {
        while (isBite)
        {
            if (isDeath)
            {
                //Debug.Log("FishControl/Dying/isDeath : " + isDeath);
                gameMgr._currentState = PublicDefined.IngameState.idle;
                gameMgr.HideBait();
                gameMgr._isPause = true;

                // #블루투스
                if (bleTotal != null && bleTotal.ConnectedMain)
                {
                     MotorStop();
                }
                // 특수공격 중이였다면 종료
                if (_isSpecialAttack)
                {
                    StopSpecialAttack();
                }
                StopCoroutine(fightCor);
                StopCoroutine(_shutCoroutine);

                isBite = false;
                isCatch = true;

                if(_fishData._fishDBNumber.Equals(28))
                    _fishObject.transform.eulerAngles = _diedFishRotate_octopus;
                else
                    _fishObject.transform.eulerAngles = _diedFishRotate;
                gameMgr._bgm = false;

                // 게이지는 안 켠다.
                gameMgr.isReset = true;
                gameMgr.ResetAction();
            }
            //잡기 실패했을 때(파이팅 중이 아니라면)
            else if (!_isFighting)
            {
                gameMgr._currentState = PublicDefined.IngameState.casting;
                // #블루투스
                if (bleTotal != null && bleTotal.ConnectedMain)
                {
                    if (_dcCoroutine != null)
                    {
                        StopCoroutine(_dcCoroutine);
                        _dcCoroutine = null;
                    }
                    // DC: 물고기 흔들림
                    // BLDC: 낚싯줄 땡김
                    dc = 0;
                    bleTotal.Motor(_normalBLDC, dc);
                }

                if (_isSpecialAttack)
                {
                    StopSpecialAttack();
                }
                StopCoroutine(fightCor);
                StopCoroutine(_shutCoroutine);

                if (_isMatchMode)
                    _currentMatchStateObject.SetActive(true);
                else
                    inGameUIMgr.ShowPassContent();

                inGameUIMgr.FishingState(1);   // 낚시실패 UI 활성화
                gameMgr._isPause = false;
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                isBite = false;
                isStart = false;
                isFind = false;

                if (needleControl.needleCor == null)
                {
                    needleControl.NeedleMoveRestart();
                    
                }
                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        fishTr.GetComponent<Fish>().Restart();
                        break;
                    case PublicDefined.eMapType.skyway:
                        fishTr.GetComponent<FishSkyway>().Restart();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        fishTr.GetComponent<FishHomerspit>().Restart();
                        break;
                }

                if (_fishMoveToNeedleCoroutine != null)
                {
                    StopCoroutine(_fishMoveToNeedleCoroutine);
                    _fishMoveToNeedleCoroutine = null;
                }

                fishSkin.SetActive(false);  // 물고기 비활성화
                gameMgr._bgm = false;
                PlayBGMWhenBittingFail();  // 게임 재시작 할 수 있도록 리셋
            }
            yield return null;
        }
        powerCount = 0;
        StopCoroutine(dieCor);

    }
    // DC, BLDC모터 동시에 멈추기 X
    public void MotorStop()
    {
        _motorStopCoroutine = StartCoroutine(MotorStopCoroutine());
    }

    IEnumerator MotorStopCoroutine()
    {
        // DC: 물고기 흔들림
        // BLDC: 낚싯줄 땡김

        if (_dcCoroutine != null)
        {
            StopCoroutine(_dcCoroutine);
            _dcCoroutine = null;
        }

        float timer = 0;

        // 모터가 흔들리는 중에 멈춘다.
        if (_isDCing)
        {
            int temp = dc;

            while (dc > 0)
            {
                dc -= temp / 3;
                timer += 1;

                if (dc < 0)
                    dc = 0;

                bleTotal.Motor(15, dc);

                if (timer > 4)
                    break;

                yield return PublicDefined._05secRealDelay;

                dc = 0;
                bleTotal.Motor(0, dc);
            }
            _isDCing = false;
        }
        else
        {
            dc = 0;
            bleTotal.Motor(15, dc);

            yield return PublicDefined._09secRealDelay;

            dc = 0;
            bleTotal.Motor(0, dc);

        }

        dc = 0;
        bleTotal.Motor(0, dc);
        _motorStopCoroutine = null;
    }

    public void RestartDCCoroutine()
    {
        //bldc = (int)(fishData.weight * 10) + 20;
        if(_dcCoroutine == null)
        {
            _dcCoroutine = StartCoroutine(DC());
        }
    }
    public void Catching()
    {
        // 물 밖으로 건져낼 때
        if (isCatch && isDeath)
        {
            gameMgr.SettingCharacterAnimator(_raiseHash);
            gameMgr.PickupFish();

            if (_isMatchMode)
            {
                if (_fishData._fishDBNumber.Equals(28))
                {
                    fishTr.localPosition = _caughtFIshPos;
                    fishTr.position += Vector3.down * 0.2f;
                    fishTr.GetChild(0).localPosition = Vector3.zero;
                    fishTr.localRotation = Quaternion.Euler(60, 0, 0);
                }
                else
                {
                    fishTr.localPosition = _caughtFIshPos;
                    fishTr.GetChild(0).localPosition = Vector3.zero;
                    fishTr.GetChild(0).GetChild(0).localPosition = Vector3.zero;
                }

                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        fishTr.GetComponent<Fish>().StopCor();
                        break;
                    case PublicDefined.eMapType.skyway:
                        fishTr.GetComponent<FishSkyway>().StopCor();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        fishTr.GetComponent<FishHomerspit>().StopCor();
                        break;
                    default:
                        break;
                }

                _currentMatchStateObject.SetActive(true);
            }
            else
            {
                // 낚시 바늘로 이동
                if (_fishData._fishDBNumber.Equals(28))
                {
                    fishTr.localPosition = _caughtFIshPos;
                    fishTr.GetChild(0).localPosition = Vector3.zero;
                }
                else
                {
                    fishTr.localPosition = _caughtFIshPos;
                    fishTr.GetChild(0).localPosition = Vector3.zero;
                    fishTr.GetChild(0).GetChild(0).localPosition = Vector3.zero;
                }

                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        fishTr.GetComponent<Fish>().StopCor();
                        break;
                    case PublicDefined.eMapType.skyway:
                        fishTr.GetComponent<FishSkyway>().StopCor();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        fishTr.GetComponent<FishHomerspit>().StopCor();
                        break;
                    default:
                        break;
                }
            }



            StartCoroutine(MakeDelay(2, () =>
            {
                // 낚시 성공 UI 활성화
                inGameUIMgr.FishingState(0);

                // 성공 효과음 재생
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishCatchSuccess).GetComponent<AudioPoolObject>().Init();

                AudioManager.INSTANCE.StopBGM();
                // 물고기 손으로 드는 애니메이션 재생

                gameMgr.SettingCharacterAnimator(_raiseHash, false);

                isCatch = false;
                petMgr.petAnimator.SetBool(_catchHash, false);

                _fishData._lenth = (_fishData._lenth * 10000) * 0.01f;

                _fishData._weight = (_fishData._weight * 100) * 0.01f;


                if (_isMatchMode)
                    fishPopUp.SetUpInfo_Match(_fishDataForPassCheck, _fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._info, _fishData._gradeType, _fishAni);
                else
                    fishPopUp.SetUpInfo(_fishDataForPassCheck, _fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._info, _fishData._gradeType, _fishAni);

                if (_fishData._gradeType.Equals(PublicDefined.eFishType.Rare))
                {
                    switch (DataManager.INSTANCE._mapType)
                    {
                        case PublicDefined.eMapType.jeongdongjin:
                            _jeongdongjinFishManager.RemoveRareFishList(_fishObject);
                            break;
                        case PublicDefined.eMapType.skyway:
                            _skywayFishManager.RemoveRareFishList(_fishObject);
                            break;
                        case PublicDefined.eMapType.homerspit:
                            _homerspitFishManager.RemoveRareFishList(_fishObject);
                            break;
                    }
                }
            }));

            isOver = true;
            
        }
    }
    #endregion
    public void PlayBGMWhenBittingFail()
    {
        gameMgr._bgm = true;
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

    private void StopSpecialAttack()
    {
        _isSpecialAttack = false;
        gameMgr.SettingCharacterAnimator(_centerHash, false);
        arrowUp.SetActive(false);
        gameMgr.SettingCharacterAnimator(_leftHash, false);
        arrowLeft.SetActive(false);
        gameMgr.SettingCharacterAnimator(_rightHash, false);
        arrowRight.SetActive(false);
        StopCoroutine(specialAttackCor);
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
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }
}