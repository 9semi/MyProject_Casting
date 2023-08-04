using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using SplineMesh;
using System;

public class GameManager : MonoBehaviour
{
    public enum eGameStyle
    {
        Bobber,
        Onetwo
    }
    eGameStyle _gameStyleState; public eGameStyle GameStyleSstate { get { return _gameStyleState; } set { _gameStyleState = value; } }
    public enum eIngameState
    {
        idle = 0,
        casting,
        fighting
    }
    eIngameState _currentState = eIngameState.idle; public eIngameState CurrentState { get { return _currentState; } set { _currentState = value; } }

    readonly int _throwHash = Animator.StringToHash("Throw");
    readonly int _egingHash = Animator.StringToHash("Eging");
    readonly Vector3 _needleFlyingPos = new Vector3(0.007f, 0.273f, -0.15f);


    // Private
    Vector3 _reelPoint3ResetRot = Vector3.zero;
    Vector3 _needleResetRot = Vector3.zero;
    Vector3 _needleResetPos = Vector3.zero;
    Vector3 _angleGyro; public Vector3 AngleGyro { get { return _angleGyro; } }
    Vector3 resetPointPos0, resetPointPos1, resetPointPos2;
    Vector3 fish;    // 잡힌 물고기 포지션
    Vector3 _sinkerOriginPos;
    Vector3[] _lineRendererPositions = new Vector3[10];  // reelNumPoints위치 저장
    Vector3[] _lineRendererPositions2 = new Vector3[50]; // reelNumPoints2위치 저장
    Vector3[] rodPositions = new Vector3[15];  // 라인 포지션 
    Transform reelMiddleObj;
    Transform reelEndObj;
    Transform _reelPoint1;
    Transform _reelPoint2; public Transform ReelPoint2 { get { return _reelPoint2; } }
    Transform _reelPoint3; public Transform ReelPoint3 { get { return _reelPoint3; } }
    Transform bobberPos;    // 찌가 붙을 위치
    Transform needlePos;    // 바늘이 붙을 위치
    Transform _reelPos123;    // 릴이 붙을 위치
    Transform rodPoint0, rodPoint1, rodPoint2;    // 베지어 꺽임 포인트  
    Transform[] rodborn = new Transform[15];   // 낚시대 위치 트랜스폼
    Transform _fishCaught; public Transform FishCaught { set { _fishCaught = value; } }
    Transform[] reelPosition = new Transform[10];
    GameObject reel;   // 릴 오브젝트
    GameObject _fakeNeedle;
    GameObject _sinkerObject; public void SetSinkerObjectActive(bool active) { _sinkerObject.gameObject.SetActive(active); }
    LineRenderer lineRenderer;  // 낚시대 릴
    LineRenderer lineRenderer2; // 낚시대 릴2
    Gyroscope gyroscope;    // 자이로 센서
    Spline spline;
    
    PublicDefined.eMapType _mapType;

    int reelNumPoints = 10; // 릴 : 낚시대 처음 -> 낚시대 끝부분(reelPositions.Lenth)
    int reelNumPoints2 = 50;    // 릴 : 낚시대 끝부분 -> 바늘(reelPositions2.Lenth)
    float _rotY = 0;// 캐릭터 회전 관련 
    float _inputY = 0;// 캐릭터 회전 관련 
    float _minRotY = -32f;// 캐릭터 회전 관련 
    float _maxRotY = 35f;// 캐릭터 회전 관련 
    float x, y, z;// 낚시대 꺽는 고정변수
    float rod1X, rod1Y, rod1Z;// 낚시대 포인트1의 위치(지금은 사용X)
    float rod2X, rod2Y, rod2Z;// 낚시대 포인트2의 위치
    float _progress; public float Progress { set { _progress = value; } }
    bool _tutorialThrowStop;
    bool _tutorialRotateStop;
    bool _tutorialGageStop;
    bool _isSpinning;
    bool _isMatch;
    bool _isTutorial;
    bool _isPause; public bool IsPause { get { return _isPause; } set { _isPause = value; } }
    bool _isFly; public bool IsFly { get { return _isFly; } set { _isFly = value; } }
    bool _isReset; public bool IsReset { get { return _isReset; } set { _isReset = value; } }
    bool _needleInWater; public bool NeedleInWater { get { return _needleInWater; } set { _needleInWater = value; } }
    bool _baitThrowMode; public bool BaitThrowMode { get { return _baitThrowMode; } set { _baitThrowMode = value; } }
    bool _isNoBite; public bool IsNoBite { get { return _isNoBite; } set { _isNoBite = value; } }
    bool isTurn;
    bool isHook;
    bool _isNeedleMoving; public bool IsNeedleMoving { get { return _isNeedleMoving; } set { _isNeedleMoving = value; } }
    bool _isEging; public bool IsEging { get { return _isEging; } set { _isEging = value; } }
    bool _rotateStop; public bool RotateStop { get { return _rotateStop; } set { _rotateStop = value; } }
    bool _isPlayingBGM; public bool IsPlayingBGM { set { _isPlayingBGM = value; } }
    bool _isConnectedToBluetooth_Main; public bool IsConnectedToBluetooth_Main { get { return _isConnectedToBluetooth_Main; } }
    bool _isConnectedToBluetooth_Reel; public bool IsConnectedToBluetooth_Reel { get { return _isConnectedToBluetooth_Reel; } }
    bool _castingPermission = false;

    [Header("베이트 릴")]
    [SerializeField] GameObject _reel_45;
    Transform _reelPos45;    // 릴이 붙을 위치

    [Header("장구 릴")]
    [SerializeField] GameObject _reel_67;
    Transform _reelPos67;    // 릴이 붙을 위치

    [Header("스피닝 릴")]
    [SerializeField] GameObject _reel_123;

    [SerializeField] GameObject line;    // 줄 오브젝트
    [SerializeField] GameObject bobber;   // 찌 오브젝트  
    [SerializeField] GameObject needle;   // 바늘 오브젝트
    [SerializeField] GameObject[] baitNeedle; // 미끼달린 바늘 오브젝트
    [SerializeField] GameObject fishRodOriginal;  // 낚시 뼈대
    [SerializeField] ConfigurableJoint[] joints;  // 조인트
    [SerializeField] GameObject rodJoint; // 조인트위한 오브젝트
    [SerializeField] GameObject fishPopUp;   // 물고기 정보창
    [SerializeField] Coroutine reelCor; // 캐스팅전 릴디렉션 코루틴 함수
    [SerializeField] Coroutine hookCor;   // 낚시 중 행동(에깅)코루틴
    [SerializeField] Transform _catchPos; public Transform CatchPosition { get { return _catchPos; } }
    [SerializeField] Transform characterTr;   // 캐릭터 트랜스폼
    [SerializeField] Transform handR; // 캐릭터의 오른손 매쉬
    [SerializeField] Transform rodPos; // 낚시대가 붙을 위치(transform)이 컴포넌트로 들어가 있는 오브젝트
    [SerializeField] Transform _rodMeshPos; // 낚싯대 메쉬가 붙을 오브젝트
    [SerializeField] Transform _reellinePos;

    // 스크립트 연결
    CameraManager _cameraManager; public void SetCameraManagerInstance(CameraManager instance) { _cameraManager = instance; }
    InGameUIManager _ingameUIManager; public void SetIngameUIManagerInstance(InGameUIManager instance) { _ingameUIManager = instance; }
    FishObjectManager _jeongdongjinFishManager;
    public FishObjectManager GetJeongdongjinFishManager() { return _jeongdongjinFishManager; }
    FishObjectManagerSkyway _skywayFishManager;
    public FishObjectManagerSkyway GetSkywayFishManager() { return _skywayFishManager; }
    FishObjectManagerHomerspit _homerspitFishManager;
    public FishObjectManagerHomerspit GetHomerspitFishManager() { return _homerspitFishManager; }
    NeedleControl _needleControl; public NeedleControl NeedleControl { get { return _needleControl; } set { _needleControl = value;} }
    FishControl _fishControl;
    CharacterManager _characterManager; public void SetCharacterManagerInstance(CharacterManager instance) { _characterManager = instance; }
    Reeling _reeling; public void SetReelingInstance(Reeling instance) { _reeling = instance; }
    BLETotal _bleTotal;
    Reel _reel;
    UserData _userData; public UserData UserData { get { return _userData; } set { _userData = value; } }
    ReelBlueToothData _reelData; public ReelBlueToothData ReelData { get { return _reelData; } set { _reelData = value; } }



    
    void Awake()
    {
        _tutorialRotateStop = false;
        _tutorialThrowStop = false;
        _tutorialGageStop = false;
        _baitThrowMode = false;
    }
    private void Start()
    {       
        AudioManager.INSTANCE.StopAllEffect();

        if(_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        _rotateStop = false;
        _isMatch = DataManager.INSTANCE._isMatch;
        _isTutorial = DataManager.INSTANCE._tutorialIsInProgress;

        _mapType = DataManager.INSTANCE._mapType;

        if (DataManager.INSTANCE._tutorialIsInProgress)
        {
            _userData.BackToInitialState();
            if(Equipment.INSTANCE != null)
                Equipment.INSTANCE.BackToInitialState();
        }

        InitializeInstances();
        CheckBLE();
        DoEquip();
        ReadyRodReel();
        CheckCurrentEquipment();
        CheckCurrentBait();

        // 게이지 시작
        reelCor = StartCoroutine(ReelDirection());

        // 자이로스코프 센서 활성화
        gyroscope = Input.gyro;
        gyroscope.enabled = true;
        GameStyleSstate = eGameStyle.Onetwo; // 게임 스타일

        // 낚싯대 꺾임 관련 변수
        x = 0.034f; y = 0.025f; z = 0.005f;

        InvokeRepeating("InGameRandomEffectPlay", 2, 60); // 고양이와 seagull 소리를 2초 후에 60초마다 실행 시킨다.

        PlayBGM(true);
    }
    void Update()
    {
        if(Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_isMatch && !_isTutorial)
            {
                _ingameUIManager.Pause();
            }

            _angleGyro = gyroscope.rotationRate; // 회전율 받아온다.
        }
        
        // 낚시대 휨
        DrawQuadraticCurve(); // 이차 곡선을 그린다.(낚싯대) 여기서 rodborn의 위치를 잡고

        // 여기서 rodborn의 위치를 넘겨주면서 곡선을 그리는 것 같다.
        for (int i = 0; i < rodborn.Length; i++)
        {
            spline.nodes[i].Position = new Vector3(rodborn[i].localPosition.x, rodborn[i].localPosition.y, rodborn[i].localPosition.z);
            spline.nodes[i].Direction = new Vector3(rodborn[i].localPosition.x, rodborn[i].localPosition.y, rodborn[i].localPosition.z);
        }

        // 물고기 뭄? 낚시대 휨(많이) : 낚시대 휨(약간) 
        if (_fishControl.IsBite)
        {
            fish = _fishCaught.position;
            Point2();
        }
        else if(_isNoBite)
        {
            NoBiteRod();
        }

        //캐스팅 후(물 속)? -> 게임스타일에 맞게
        if (!_needleInWater)
        {
            NonWaterLine();
        }
        else if (_needleInWater && GameStyleSstate == eGameStyle.Bobber)
        {
            BobberLine();
        }
        else if (_needleInWater && GameStyleSstate == eGameStyle.Onetwo)
        {
            OnetwoLine();
        }

        if (!_baitThrowMode)
        {
            // 신형 릴
            {
                if (_isConnectedToBluetooth_Reel)
                    BluetoothRotate();
                else
                    CharacterRotate();
            }
            // 구형 릴
            {
                //CharacterRotate();
            }
        }
            
    }

    void CheckBLE()
    {
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            _bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();

            if (_bleTotal.ConnectedMain)
            {
                _bleTotal.SetGameManager(this);
                _bleTotal.IsInGame_Main = true;
                _isConnectedToBluetooth_Main = true;
            }

            if (_bleTotal.ConnectedReel)
            {
                _reelData = new ReelBlueToothData();
                _bleTotal.SetGameManager(this);
                _bleTotal.IsInGame_Reel = true;
                _isConnectedToBluetooth_Reel = true;
            }
        }
        else
            _bleTotal = null;
    }

    void InitializeInstances()
    {
        _fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManager>();
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManagerSkyway>();
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManagerHomerspit>();
                break;
        }
    }


    void DoEquip() // 장비를 갖추는 함수
    {
        // 낚시대
        //public에 설정되어 있는 fishRod를 rodPos의 위치값, 회전값에 인스턴스 생성 
        //fishRodOriginal = Instantiate(fishRodOriginal, new Vector3(rodPos.transform.position.x,
        //    rodPos.transform.position.y, rodPos.transform.position.z),
        //    rodPos.transform.rotation);

        fishRodOriginal = Instantiate(fishRodOriginal, rodPos.transform.position, rodPos.transform.rotation);

        // 부모를 rodMeshPos로 지정해보자.
        rodJoint = Instantiate(rodJoint, rodPos.transform.position, rodJoint.transform.rotation, _rodMeshPos);

        //rodPos를 오른손 매쉬의 위치값의 자식으로 설정 (없어도 됨)
        //rodPos.transform.parent = handR;

        //아까 생성한 fishRod 오브젝트를 rodPos 오브젝트의 자식으로 설정
        fishRodOriginal.transform.parent = rodPos.transform;

        // 조인트
        joints = rodJoint.GetComponents<ConfigurableJoint>(); // ConfigurableJoint 컴포넌트가 2개여서 2개 들어가나보다.
        joints[0].connectedBody = fishRodOriginal.GetComponent<Rigidbody>();
        joints[1].connectedBody = fishRodOriginal.GetComponent<Rigidbody>();

        // 낚시대 관련, 베지어 곡선 꺽임 포인트
        rodPoint0 = fishRodOriginal.transform.GetChild(17); // 낚싯대 합쳤을 때 맨 아래
        rodPoint1 = fishRodOriginal.transform.GetChild(18); // 낚싯대 합쳤을 때 3/2 지점
        // FishRodOriginal에 19를 추가했는데 실수로 안 바꾼 것 같다. 일단 내가 바꿔서 테스트 한다. 되는 듯?
        rodPoint2 = fishRodOriginal.transform.GetChild(19); // 낚싯대 합쳤을 때 맨 위
        //rodPoint2 = rodJoint.transform.GetChild(17); // 낚싯대랑 합쳤을 때 맨 위

        // 낚시대 포인트 임시저장소
        //p1X = rodPoint1.position.x;
        //p1Y = rodPoint1.position.y;
        //p1Z = rodPoint1.position.z;
        //p2X = rodPoint2.position.x;
        //p2Y = rodPoint2.position.y;
        //p2Z = rodPoint2.position.z;

        // 낚시대 포인트 첫 트랜스폼
        resetPointPos0 = rodPoint0.localPosition;
        resetPointPos1 = rodPoint1.localPosition;
        resetPointPos2 = rodPoint2.localPosition;

        _reelPos123 = rodJoint.transform.GetChild(2).GetChild(0);
        _reelPos45 = rodJoint.transform.GetChild(2).GetChild(1);
        _reelPos67 = rodJoint.transform.GetChild(2).GetChild(2);

        _reel_123 = Instantiate(_reel_123, new Vector3(_reelPos123.position.x, _reelPos123.position.y, _reelPos123.position.z),
                        _reelPos123.rotation);
        _reel_123.transform.parent = _reelPos123;
        _reel_123.SetActive(false);

        _reel_45 = Instantiate(_reel_45, new Vector3(_reelPos45.position.x, _reelPos45.position.y, _reelPos45.position.z),
                        _reelPos45.rotation);
        _reel_45.transform.parent = _reelPos45;
        _reel_45.SetActive(false);

        _reel_67 = Instantiate(_reel_67, new Vector3(_reelPos67.position.x, _reelPos67.position.y, _reelPos67.position.z),
                        _reelPos67.rotation);
        _reel_67.transform.parent = _reelPos67;
        _reel_67.SetActive(false);

        switch (_userData.GetCurrentEquipmentDictionary()["reel"])
        {
            case 0:
            case 1:
            case 2:
                _reel_123.SetActive(true);
                reel = _reel_123;
                _rodMeshPos.eulerAngles = new Vector3(0, 0, -90);
                _isSpinning = true;
                break;
            case 3:
            case 4:
                _reel_45.SetActive(true);
                reel = _reel_45;
                _rodMeshPos.eulerAngles = new Vector3(0, 0, 90);
                _isSpinning = false;
                break;
            case 5:
            case 6:
                _reel_67.SetActive(true);
                reel = _reel_67;
                _rodMeshPos.eulerAngles = new Vector3(0, 0, 90);
                _isSpinning = false;
                break;
        }

        _reel = reel.GetComponent<Reel>();

        //_rodMeshPos.eulerAngles = new Vector3(0, 0, -90
        //_reelPos123 = rodJoint.transform.GetChild(2).GetChild(0);
        //reel = Instantiate(_reel_123, new Vector3(_reelPos123.position.x, _reelPos123.position.y, _reelPos123.position.z),
        //    _reelPos123.rotation);
        //reel.transform.parent = _reelPos123;
        //_reel = reel.GetComponent<Reel>();

        lineRenderer = reel.GetComponent<LineRenderer>();
        lineRenderer.positionCount = reelNumPoints;

        // 낚싯대 끝부터 물고기까지 낚싯줄
        line = Instantiate(line, new Vector3(_reellinePos.position.x, _reellinePos.position.y, _reellinePos.position.z),
            line.transform.rotation, _reellinePos);
        reelMiddleObj = line.transform.GetChild(1);
        reelEndObj = line.transform.GetChild(2);
        _reelPoint1 = line.transform.GetChild(0);
        _reelPoint2 = line.transform.GetChild(1);
        _reelPoint3 = line.transform.GetChild(2);
        _sinkerObject = _reelPoint2.transform.GetChild(2).gameObject;
        _reelPoint3ResetRot = _reelPoint3.eulerAngles;

        lineRenderer2 = line.GetComponent<LineRenderer>();

        lineRenderer2.positionCount = reelNumPoints2;
        bobberPos = _reelPoint2.transform.GetChild(0);

        // 찌
        //CreateEquipmentSetPos(bobberPos, reelMiddleObj, bobber, true);
        bobber = Instantiate(bobber, bobberPos.position, Quaternion.identity);
        bobberPos.parent = reelMiddleObj;
        bobber.transform.parent = bobberPos;
        bobber.transform.localEulerAngles = Vector3.zero;
        bobber.SetActive(true);

        needlePos = _reelPoint3.transform.GetChild(0);
       // _needleResetRot = needlePos.localEulerAngles;
        _needleResetPos = needlePos.localPosition;
        _fakeNeedle = _reelPoint3.transform.GetChild(3).gameObject;

        // 바늘 - 미끼
        CreateEquipmentSetPos(needlePos, reelEndObj, needle, true);

        for (int i = 0; i < baitNeedle.Length; i++)
        {
            CreateEquipmentSetPos(needlePos, reelEndObj, baitNeedle[i], false);
        }
        _cameraManager.SettingTarget(GetNeedleControlTransform());

        _reelPoint2.GetChild(0).gameObject.SetActive(false);
        _ingameUIManager._ReelPoint2Pos = _reelPoint2;
    }

    private void CheckCurrentEquipment()
    {
        Dictionary<string, int> dic = _userData.GetCurrentEquipmentDictionary();

        //Debug.Log("낚싯대: " + dic["rod"] + ", 릴: " + dic["reel"]);

        Item rod = ItemData.Instance.rodItemDB[dic["rod"]];
        RodInfoChange(rod.intensive, rod.rodMaterial);

        if (_reel == null)
            _reel = reel.GetComponent<Reel>();

        int currentReelNumber = dic["reel"];
        _reel.ChangeMaterial(currentReelNumber);

        //Item currentReel = ItemData.Instance.reelItemDB[currentReelNumber];
        //DataManager.INSTANCE._reelIntensive = currentReel.intensive;

        if (!dic["float"].Equals(-1))
        {
            _reelPoint2.GetChild(0).gameObject.SetActive(true);
            GameStyleSstate = eGameStyle.Bobber;
        }

        int currentSinkerNumber = dic["sinker"];
        //Debug.Log(currentSinkerNumber);
        if (!currentSinkerNumber.Equals(-1))
        {
            Item currentSinker = ItemData.Instance.sinkerItemDB[currentSinkerNumber];
            DataManager.INSTANCE._sinkerWeight = currentSinker._sinkerWeight;
            //reelPoint3.transform.GetChild(4).gameObject.SetActive(true);
            _sinkerObject.SetActive(true);
        
        }
    }

    void CheckCurrentBait()
    {
        Dictionary<string, int> dic = _userData.GetCurrentEquipmentDictionary();
        int currentBait = dic["bait"];

        if (!currentBait.Equals(-1))
        {
            // 해당 미끼 켜기                                
            _reelPoint3.GetChild(0).GetChild(currentBait + 1).gameObject.SetActive(true);
            // 바늘 오브젝트 끄기
            _reelPoint3.GetChild(0).GetChild(0).gameObject.SetActive(false);

            CheckLure(currentBait);

            DataManager.INSTANCE.CheckBaitProbability();
        }
    }

    void CheckLure(int currentBait)
    {
        // 해당 미끼 켜기                                
        _reelPoint3.GetChild(0).GetChild(currentBait + 1).gameObject.SetActive(true);
        // 바늘 오브젝트 끄기
        _reelPoint3.GetChild(0).GetChild(0).gameObject.SetActive(false);

        switch (currentBait)
        {
            case 15:
                DataManager.INSTANCE._lureWeight = 200;
                break;
            case 22:
                DataManager.INSTANCE._lureWeight = 160;
                break;
            case 32:
                DataManager.INSTANCE._lureWeight = 180;
                break;
            case 33:
                DataManager.INSTANCE._lureWeight = 140;
                break;
            case 34:
                DataManager.INSTANCE._lureWeight = 120;
                break;
            case 36:
                DataManager.INSTANCE._lureWeight = 200;
                break;
            case 50:
                DataManager.INSTANCE._lureWeight = 160;
                break;
            case 51:
                DataManager.INSTANCE._lureWeight = 0;
                break;
            case 52:
                DataManager.INSTANCE._lureWeight = -1;
                break;
            case 53:
                DataManager.INSTANCE._lureWeight = -1;
                break;
            default:
                DataManager.INSTANCE._lureWeight = 0;
                break;
        }
    }

    void ReadyRodReel()
    {
        int[] num = new int[] { 2, 3, 5, 7, 8, 9, 10, 12, 13, 14 };

        // 낚시대 메쉬
        spline = rodJoint.GetComponent<Spline>();

        for (int i = 0; i < num.Length; i++)
        {
            reelPosition[i] = rodJoint.transform.GetChild(num[i]).transform;
        }

        for (int i = 0; i < rodborn.Length; i++)
        {
            rodborn[i] = rodJoint.transform.GetChild(i);
        }

        for (int i = 1; i < rodborn.Length; i++)
        {
            rodborn[i].position = rodJoint.transform.GetChild(i).position;
        }
    }

    void CharacterRotate()
    {
        if (!_isPause && !_tutorialRotateStop && !_rotateStop)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                _inputY = Input.GetAxis("Horizontal");
                _rotY += _inputY * 0.7f;
                _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                characterTr.eulerAngles = new Vector3(0, _rotY, 0);
            }
            else
            {
                _inputY = Input.acceleration.x;
                _rotY += _inputY * 1.2f;
                _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                characterTr.eulerAngles = new Vector3(0, _rotY, 0);
            }
        }
    }

    // 낚시대 교체
    public void RodInfoChange(float intensive, Material material)
    {
        //DataManager.INSTANCE._rodIntensive = intensive;
        //Debug.Log(DataManager.INSTANCE._rodIntensive);
        rodJoint.GetComponent<SplineMeshTiling>().toUpdate = true;
        rodJoint.GetComponent<SplineMeshTiling>().material = material;
    }

    // 릴 교체
    public void ReelInfoChange(float intensive, int number)
    {
        //Debug.Log(intensive);
        switch(number)
        {
            case 0:
            case 1:
            case 2:
                _reel_123.SetActive(true);
                _reel_45.SetActive(false);
                _reel_67.SetActive(false);
                
                reel = _reel_123;

                _rodMeshPos.eulerAngles = new Vector3(_rodMeshPos.eulerAngles.x, _rodMeshPos.eulerAngles.y, -90);
                _isSpinning = true;
                break;
            case 3:
            case 4:
                _reel_123.SetActive(false);
                _reel_45.SetActive(true);
                _reel_67.SetActive(false);

                reel = _reel_45;
                _rodMeshPos.eulerAngles = new Vector3(_rodMeshPos.eulerAngles.x, _rodMeshPos.eulerAngles.y, 90);
                _isSpinning = false;
                break;
            case 5:
            case 6:
                _reel_123.SetActive(false);
                _reel_45.SetActive(false);
                _reel_67.SetActive(true);

                reel = _reel_67;
                _rodMeshPos.eulerAngles = new Vector3(_rodMeshPos.eulerAngles.x, _rodMeshPos.eulerAngles.y,90);
                _isSpinning = false;
                break;
        }

        lineRenderer = reel.GetComponent<LineRenderer>();
        lineRenderer.positionCount = reelNumPoints;

        _reel = null;
        _reel = reel.GetComponent<Reel>();

        //DataManager.INSTANCE._reelIntensive = intensive;

        _reel.ChangeMaterial(number);
    }
    
    public void RodMeshOn(Material material)
    {
        rodJoint.GetComponent<SplineMeshTiling>().toUpdate = true;
        rodJoint.GetComponent<SplineMeshTiling>().material = material;
    }

    // 맨 위 포인트만 휨
    private void Point2()
    {
        // Update 안에 있는 함수(물고기가 바늘을 물면 활성화 됨)
        float posX = Mathf.Pow(fish.z, -1); // 멀면 0.02 ~ 0.04정도 가까우면 0.1 ~ 0.2정도 (이 코드가 없으면 멀 때 낚싯대가 과하게 휜다.)

        if (float.IsNaN(posX) || float.IsInfinity(posX))
        {
            rod2X = fish.x + characterTr.rotation.y;
        }
        else
        {
            rod2X = fish.x * posX + characterTr.rotation.y;
        }

        rod2Y = 1.3f + fish.y * y;
        rod2Z = 1.3f + (fish.z - 3) * z;

        rodPoint2.position = new Vector3(rod2X + rodPos.position.x, rod2Y + rodPos.position.y, rod2Z + rodPos.position.z);
    }


    public void CreateEquipmentSetPos(Transform pos,Transform objTr, GameObject equipMent, bool isSetActive)
    {
        equipMent = Instantiate(equipMent, pos.position, equipMent.transform.rotation);
        pos.parent = objTr;
        equipMent.transform.parent = pos;
        equipMent.SetActive(isSetActive);
    }

    // 물고기가 물지않은 상태의 낚시대 휨처리
    private void NoBiteRod()
    {// Update 안에 있는 함수

        _isNoBite = false;
        rodPoint0.localPosition = resetPointPos0;
        rodPoint1.localPosition = resetPointPos1;
        rodPoint2.localPosition = resetPointPos2;
    }

    // 낚시대 초기화
    public void ResetAction()
    {
        //Debug.Log("리셋액션" + isReset);
        if (_isReset)
        {
            if(_isPlayingBGM)
            {
                PlayBGM(false);
            }

            // 물속 움직임 중지
            _needleInWater = false;
            _isReset = false;
            // 훅셋 코루틴 중지
            StopHookSet();
            // 낚시대 방향 첫위치로 되돌려 놓기
            rodPos.rotation = new Quaternion(0, 0, 0, 0);
            // 시간 더해졌으니 reset하면 0으로 초기화
            _progress = 0;
            // 낚시바늘 포지션 초기화
            _needleControl.myTr.eulerAngles = _reelPoint3ResetRot;

            //needleControl.myTr.GetChild(0).GetChild(0).localEulerAngles = _needleResetRotate;
            _needleControl.myTr.GetChild(0).localEulerAngles = _needleResetRot;

            needlePos.localPosition = _needleResetPos;

            if (!_userData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
            {
                _sinkerObject.SetActive(true);
            }

            _needleControl.NeedleReset();
            _cameraManager.SettingTarget(null);
            reelCor = StartCoroutine(ReelDirection());
            _fishControl.IsStart = false;
            _fishControl.Catching();
            rodPoint0.localPosition = resetPointPos0;
            rodPoint1.localPosition = resetPointPos1;
            rodPoint2.localPosition = resetPointPos2;
            _needleControl.isWater = false;

            _reeling.IsReeling = false;

            if (GameStyleSstate.Equals(eGameStyle.Bobber))
            {
                _reelPoint2.GetChild(0).gameObject.SetActive(true);
                _reelPoint2.transform.GetChild(1).gameObject.SetActive(false);
                bobberPos.gameObject.SetActive(true);
            }

            // 수심 움직임 끄기
            if(_needleControl.needleCor != null)
            {
                _needleControl.StopCoroutine(_needleControl.needleCor);
                _needleControl.needleCor = null;
            }
            
            // 현재 상태 idle로 
            _currentState = eIngameState.idle;

            // 블루투스
            BluetoothReset();
        }
    }

    public void NeedleReset()
    {
        needlePos.localPosition = Vector3.zero;
        _needleControl.myTr.eulerAngles = _reelPoint3ResetRot;
        _needleControl.myTr.GetChild(0).localEulerAngles = _needleResetRot;
    }

    //블루투스 초기화
    public void BluetoothReset()
    {
        // 블루투스
        if (_bleTotal != null && _bleTotal.ConnectedMain)
        {
            if (_fishControl.DcCoroutine != null)
            {
                StopCoroutine(_fishControl.DcCoroutine);
                _fishControl.DcCoroutine = null;
            }

            if(_fishControl._MotorStopCoroutine != null)
                _fishControl.MotorStop();
        }
    }
    public IEnumerator ReelDirection() // 캐스팅 전
    {
        // 게이지 스피드
        float powerSpeed = 0.5f;
        
        while (true)
        {
            while (_progress <= 1f) // 게이지가 1에서 내려갈 때
            {
                if (_tutorialGageStop)
                    powerSpeed = 0;

                // 에디터 조작
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    // 파워 게이지
                    _progress += powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetCharacterGage(Mathf.Lerp(0, 1, _progress));
                    
                    if (ReadyForCasting())
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            _currentState = eIngameState.casting;
                            needlePos.localPosition = _needleFlyingPos;
                            _rotateStop = true;
                            StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                            _characterManager.SettingAnimator(_throwHash, true);

                            if (_progress < 0.3f)
                                _progress = 0.35f;

                            _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);
                            StopCoroutine(reelCor);

                        }
                    }
                    yield return null;
                }
                // 안드로이드 조작
                else
                {
                    // 파워 게이지
                    _progress += powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetCharacterGage(Mathf.Lerp(0, 1, _progress));

                    if (ReadyForCasting())
                    {
                        if (_isConnectedToBluetooth_Reel) // 블루투스 O
                        {
                            if (_reelData.Zg < -30000 && !_isPause)
                            {
                                // 캐스팅 조건2
                                _currentState = eIngameState.casting;
                                needlePos.localPosition = _needleFlyingPos;
                                _rotateStop = true;
                                StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                                _characterManager.SettingAnimator(_throwHash, true);

                                if (_progress < 0.3f)
                                    _progress = 0.35f;

                                _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);

                                if (DataManager.INSTANCE._vibration)
                                    Vibration.Vibrate(2500);

                                StopCoroutine(reelCor);

                            }
                        }
                        else
                        {
                            if (_angleGyro.x < -9) // 블루투스 X
                            {
                                if (!_baitThrowMode && !fishPopUp.activeSelf)
                                {
                                    _currentState = eIngameState.casting;
                                    needlePos.localPosition = _needleFlyingPos;
                                    _rotateStop = true;
                                    StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                                    _characterManager.SettingAnimator(_throwHash, true);

                                    if (_progress < 0.3f)
                                        _progress = 0.35f;

                                    _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);

                                    if (DataManager.INSTANCE._vibration)
                                        Vibration.Vibrate(2500);

                                    StopCoroutine(reelCor);
                                }
                            }
                        }
                    }
                    yield return null;
                }
            }
            // =================================================================================================
            while (_progress >= 0)   // 게이지가 0에서 올라갈 때
            {
                if (_tutorialGageStop)
                    powerSpeed = 0;

                // 에디터 조작
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    // 파워 게이지
                    _progress -= powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetCharacterGage(Mathf.Lerp(0, 1, _progress));

                    if (ReadyForCasting())
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            _currentState = eIngameState.casting;
                            needlePos.localPosition = _needleFlyingPos;
                            _rotateStop = true;
                            StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                            _characterManager.SettingAnimator(_throwHash, true);

                            if (_progress < 0.3f)
                                _progress = 0.35f;

                            _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);
                            StopCoroutine(reelCor);

                        }
                    }
                    yield return null;
                }
                // 안드로이드 조작
                else
                {
                    // 파워 게이지
                    _progress -= powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetCharacterGage(Mathf.Lerp(0, 1, _progress));

                    if (ReadyForCasting())
                    {
                        if (_isConnectedToBluetooth_Reel) // 블루투스 O
                        {
                            if (_reelData.Zg < -30000 && !_isPause)
                            {
                                _currentState = eIngameState.casting;
                                needlePos.localPosition = _needleFlyingPos;
                                _rotateStop = true;
                                StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                                _characterManager.SettingAnimator(_throwHash, true);
                                if (_progress < 0.3f)
                                    _progress = 0.35f;

                                _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);

                                if (DataManager.INSTANCE._vibration)
                                    Vibration.Vibrate(2500);

                                StopCoroutine(reelCor);

                            }
                        }
                        else
                        {
                            if (_angleGyro.x < -9) // 블루투스 X
                            {
                                // 캐스팅 조건2 
                                if (!_baitThrowMode && !fishPopUp.activeSelf)
                                {
                                    _currentState = eIngameState.casting;
                                    needlePos.localPosition = _needleFlyingPos;
                                    _rotateStop = true;
                                    StartCoroutine(MakeDelay(2, () => _ingameUIManager.SetCharacterGage(false)));
                                    _characterManager.SettingAnimator(_throwHash, true);
                                    if (_progress < 0.3f)
                                        _progress = 0.35f;

                                    _needleControl.CastingPowerSetting(characterTr.rotation.y * _progress * 1580, _progress * 600, _progress * 1150);

                                    if (DataManager.INSTANCE._vibration)
                                        Vibration.Vibrate(2500);
                                    StopCoroutine(reelCor);
                                }
                            }
                        }
                    }
                    yield return null;
                }
            }
        }
    }

    public void StopHookSet()
    {
        if (hookCor != null)
        {
            isHook = false;
            StopCoroutine(hookCor);
            hookCor = null;
        }
    }

    public void NeedleStopMoving()
    {
        //Debug.Log("바늘이 멈췄다.");
        _isNeedleMoving = false;

        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
        }
    }

    public void NeedleStartMoving()
    {
        //Debug.Log("바늘이 움직인다.");
        _isNeedleMoving = true;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager.IncreaseProbabilityAccordingToMovement(_isNeedleMoving);
                break;
        }
    }

    // 물 밖의 릴 라인, 물안에서 라인 X(찌 스타일)
    private void BobberLine()
    {
        if (_isSpinning)
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                if (i.Equals(0))
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.1f,
                        reelPosition[i].position.z);
                }
                else
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.03f + (i * 0.0015f),
                            reelPosition[i].position.z);
                }
            }
        }
        else
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y + 0.02f - (i * 0.0015f),
                        reelPosition[i].position.z);
            }
        }
        // reelPositions2.Lenth만큼
        for (int i = 1; i < reelNumPoints2; i++)
        {
            float t = i / (float)reelNumPoints2;
            _lineRendererPositions2[i] = CalculateQuadraticBezierPoint(t, _reelPoint2.position, _reelPoint1.position, _lineRendererPositions[9]);
        }
        // 물고기가 물었을 때 or 물지 않았을 때의 릴 포인트 위치설정
        if (_fishControl.IsBite)
        {            
            _reelPoint2.position = new Vector3(_reelPoint3.position.x * 0.8f, -1.5f, _reelPoint3.position.z * 0.8f);
            _reelPoint1.position = new Vector3(_reelPoint2.position.x, _reelPoint2.position.y, _reelPoint2.position.z);
            _lineRendererPositions2[0] = _reelPoint2.position;
        }
        else
        {
            float moveX = 0.1f * _reelPoint3.position.x * 0.8f / Mathf.Sqrt(Mathf.Pow(_reelPoint3.position.x * 0.8f, 2) + Mathf.Pow(_reelPoint3.position.z * 0.8f, 2));
            float moveZ = 0.1f * _reelPoint3.position.z * 0.8f / Mathf.Sqrt(Mathf.Pow(_reelPoint3.position.x * 0.8f, 2) + Mathf.Pow(_reelPoint3.position.z * 0.8f, 2));
            if (_reelPoint2.position.z > _reelPoint3.position.z)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x, -1.5f, _reelPoint2.position.z - moveZ);
            }
            // 오른쪽
            if (_reelPoint3.position.x > 0 && _reelPoint2.position.x > _reelPoint3.position.x * 0.9f)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x - moveX, -1.5f, _reelPoint2.position.z);
            }
            //왼쪽
            else if (_reelPoint3.position.x <= 0 && _reelPoint2.position.x < _reelPoint3.position.x * 0.9f)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x - moveX, -1.5f, _reelPoint2.position.z);
            }
            if (_reelPoint1.position.y > -2.5f)
            {
                _reelPoint1.position = new Vector3(_reelPoint2.position.x * 0.9f, _reelPoint2.position.y - 0.07f, _reelPoint2.position.z - 1f);
            }
            _lineRendererPositions2[0] = _reelPoint3.position;
        }
        // reelPositions2의 마지막과 reelPosition의 마지막은 같은 위치
        _lineRendererPositions2[49] = _lineRendererPositions[9];
        lineRenderer.SetPositions(_lineRendererPositions);
        lineRenderer2.SetPositions(_lineRendererPositions2);
    }

    // 물 밖의 릴 라인, 물안에서 라인 X(원투 스타일)
    private void OnetwoLine()
    {
        for (int i = 0; i < reelNumPoints; i++)
        {
            if (i.Equals(0) && !_fishControl.IsSpecialAttack)
                _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.1f,
                    reelPosition[i].position.z);
            else
                _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.03f + (i * 0.0015f),
                        reelPosition[i].position.z);
        }

        if (_isSpinning)
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                if (i.Equals(0))
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.1f,
                        reelPosition[i].position.z);
                }
                else
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.03f + (i * 0.0015f),
                            reelPosition[i].position.z);
                }
            }
        }
        else
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y + 0.02f - (i * 0.0015f),
                        reelPosition[i].position.z);
            }
        }

        for (int i = 1; i < reelNumPoints2; i++)
        {
            float t = i / (float)reelNumPoints2;
            _lineRendererPositions2[i] = CalculateQuadraticBezierPoint(t, _reelPoint2.position, _reelPoint1.position, _lineRendererPositions[9]);
        }

        if (_fishControl.IsBite)
        {
            _reelPoint2.position = new Vector3(_reelPoint3.position.x * 0.8f, -1.5f, _reelPoint3.position.z * 0.8f);
            _reelPoint1.position = new Vector3(_reelPoint2.position.x, _reelPoint2.position.y, _reelPoint2.position.z);
            _lineRendererPositions2[0] = _reelPoint2.position;
        }
        else
        {            
            float moveX = 0.1f * _reelPoint3.position.x * 0.8f / Mathf.Sqrt(Mathf.Pow(_reelPoint3.position.x * 0.8f, 2) + Mathf.Pow(_reelPoint3.position.z * 0.8f, 2));
            float moveZ = 0.1f * _reelPoint3.position.z * 0.8f / Mathf.Sqrt(Mathf.Pow(_reelPoint3.position.x * 0.8f, 2) + Mathf.Pow(_reelPoint3.position.z * 0.8f, 2));           
            if (_reelPoint2.position.z > _reelPoint3.position.z * 0.8f)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x, -1.5f, _reelPoint2.position.z - moveZ);
            }
            // 오른쪽
            if (_reelPoint3.position.x > 0 && _reelPoint2.position.x > _reelPoint3.position.x * 0.8f)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x - moveX, -1.5f, _reelPoint2.position.z);
            }
            //왼쪽
            else if (_reelPoint3.position.x <= 0 && _reelPoint2.position.x < _reelPoint3.position.x * 0.8f)
            {
                _reelPoint2.position = new Vector3(_reelPoint2.position.x - moveX, -1.5f, _reelPoint2.position.z);
            }
            if (_reelPoint1.position.y > -2.5f)
            {
                _reelPoint1.position = new Vector3(_reelPoint2.position.x * 0.9f, _reelPoint2.position.y - 0.1f, _reelPoint2.position.z - 1f);                
            }
            _lineRendererPositions2[0] = _reelPoint3.position;
        }
        // reelPositions2의 마지막과 reelPosition의 마지막은 같은 위치
        _lineRendererPositions2[49] = _lineRendererPositions[9];
        lineRenderer.SetPositions(_lineRendererPositions);
        lineRenderer2.SetPositions(_lineRendererPositions2);
    }

    // 물속이 아닐 때
    private void NonWaterLine()
    {
        // 캐스팅 중인가?
        if (!_isFly)
        {
            _reelPoint3.position = new Vector3(reelPosition[9].position.x, reelPosition[9].position.y - 0.5f, reelPosition[9].position.z);
            _reelPoint2.position = new Vector3(_reelPoint3.position.x, _reelPoint3.position.y + 0.2f, _reelPoint3.position.z);
            _reelPoint1.localPosition = new Vector3(_reelPoint2.localPosition.x, _reelPoint2.localPosition.y, _reelPoint2.localPosition.z);
        }
        else if (_isFly)
        {
            _reelPoint2.position = new Vector3(_reelPoint3.position.x, _reelPoint3.position.y, _reelPoint3.position.z - 0.1f);

        }

        if (_isSpinning)
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                if (i.Equals(0))
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.1f,
                        reelPosition[i].position.z);
                }
                else
                {
                    _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y - 0.03f + (i * 0.0015f),
                            reelPosition[i].position.z);
                }
            }
        }
        else
        {
            for (int i = 0; i < reelNumPoints; i++)
            {
                _lineRendererPositions[i] = new Vector3(reelPosition[i].position.x, reelPosition[i].position.y + 0.02f - (i * 0.0015f),
                        reelPosition[i].position.z);
            }
        }

        for (int i = 1; i < reelNumPoints2 - 1; i++)
        {
            float t = i / (float)reelNumPoints2;
            _lineRendererPositions2[i] = CalculateQuadraticBezierPoint(t, _reelPoint2.position, _reelPoint1.position, _lineRendererPositions[9]);
        }

        // reelPositions2의 첫번째는 낚시대의 릴장비 부분과 같은 위치
        _lineRendererPositions2[0] = _reelPoint3.position;
        _lineRendererPositions2[49] = _lineRendererPositions[9];
        lineRenderer.SetPositions(_lineRendererPositions);
        lineRenderer2.SetPositions(_lineRendererPositions2);
    }

    // 낚시대 휘는 함수
    private void DrawQuadraticCurve()
    {
        //                  뼈대: 15
        for (int i = 0; i < rodborn.Length; i++)
        {
            float t = i / (float)rodborn.Length;

            // rodPoint0,1,2는 낚싯대 맨 아래, 중간, 맨 위
            rodborn[i].position = CalculateQuadraticBezierPoint(t, rodPoint0.position, rodPoint1.position, rodPoint2.position);
        }
    }
    // 베지에공식
    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        //Lerp로 
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(a, b, t);

        return c;
    }

    void InGameRandomEffectPlay() // seagull소리, 고양이 소리 랜덤 재생
    {
        int random = UnityEngine.Random.Range(0, 2);

        switch (random)
        {
            case 0:
                //SoundManager.instance.EffectPlay("The_Shriek_of_the_Seagulls");
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.seagull).GetComponent<AudioPoolObject>().Init();
                break;
            case 1:
                //SoundManager.instance.EffectPlay("Cat1");
                //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.고양이그르릉).GetComponent<AudioPoolObject>().Init();
                break;
        }
    }

    public void TurnOrientaion()
    {
        //SoundManager.instance.EffectPlay("UIClick");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        isTurn = isTurn == false ? true : false;
        Screen.orientation = isTurn ? ScreenOrientation.LandscapeRight : ScreenOrientation.LandscapeLeft;
    }

    public void TutorialCharacterRotateSetting()
    {
        _rotY = 40;
        characterTr.eulerAngles = new Vector3(0, _rotY, 0);
    }

    public void HideBait()
    {
        needlePos.gameObject.SetActive(false);
        _fakeNeedle.SetActive(true);
    }

    public void ShowBait()
    {
        needlePos.gameObject.SetActive(true);
        _fakeNeedle.SetActive(false);
    }

    public void PlayBGM(bool isBeginning)
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _tutorialRotateStop = false;
                _tutorialThrowStop = false;
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.jeongdongjinBGM, isBeginning);
                break;
            case PublicDefined.eMapType.skyway:
                _tutorialRotateStop = false;
                _tutorialThrowStop = false;
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.skywayBGM, isBeginning);
                break;
            case PublicDefined.eMapType.homerspit:
                _tutorialRotateStop = false;
                _tutorialThrowStop = false;
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.homerspitBGM, isBeginning);
                break;
            case PublicDefined.eMapType.tutorial:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.jeongdongjinBGM, isBeginning);
                break;
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

    public void BluetoothRotate()
    {
        //_reelData = _bleTotal._reelData;

        if (!_isPause && !_tutorialRotateStop && !_rotateStop)
        {
            _inputY = _reelData.Za * -0.0007f;
            _rotY += _inputY;
            _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

            characterTr.eulerAngles = new Vector3(0, _rotY, 0);
        }
    }

    public void UpdateFishSearchRange(int range)
    {
        switch(_mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager.UpdateFishSearchRange(range);
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager.UpdateFishSearchRange(range);
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager.UpdateFishSearchRange(range);
                break;
        }
    }

    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }

    public void HideButtons()
    {
        _ingameUIManager.HideButtons();
    }

    public void CastingNeedle()
    {
        _needleControl.CastingNeedle();
    }
    public Transform GetNeedleControlTransform()
    {
        return _needleControl.GetNeedleControlTransform();
    }

    public void AddForceToNeedle(float x, float y, float z)
    {
        _needleControl.AddForceToNeedle(x, y, z);
    }

    public float GettingCharacterAnimator(int animatorStringHash)
    {
        return _characterManager.GettingAnimator(animatorStringHash);
    }
    public void SettingCharacterAnimator(int animatorStringHash)
    {
        _characterManager.SettingAnimator(animatorStringHash);
    }
    public void SettingCharacterAnimator(int animatorStringHash, bool b)
    {
        _characterManager.SettingAnimator(animatorStringHash, b);
    }
    public void SettingCharacterAnimator(int animatorStringHash, float f)
    {
        _characterManager.SettingAnimator(animatorStringHash, f);
    }

    public void SettingTargetOfCamera()
    {
        _cameraManager.SettingTarget(_needleControl.transform);
    }
    public void CameraPositionSettingWhenCatchFish()
    {
        _cameraManager.CatchFish();
    }
    public void PickupFish()
    {
        if (!_userData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
        {
            _sinkerObject.SetActive(true);
        }

        _rotateStop = true;
        HideButtons();
        HideBait();
        CameraPositionSettingWhenCatchFish();
        _characterManager.CharacterTransformReset();
    }
    public bool ReadyForCasting()
    {
        return !_baitThrowMode && !_tutorialThrowStop && !_isPause && !fishPopUp.activeSelf;
    }
}