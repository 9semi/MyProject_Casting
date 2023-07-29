using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NeedleControl : MonoBehaviour
{
    readonly int _throwHash = Animator.StringToHash("Throw");

    float _castingPowerX;
    float _castingPowerY;
    float _castingPowerZ;

    Rigidbody _rigidbody;

    // 스크립트 연결
    InGameUIManager _ingameUIManager;
    public CameraManager cameraMgr;
    public GameManager gameMgr;
    public FishControl fishControl;


    private GameObject lureSplash;  // 물 웅덩이 파티클
    public GameObject lureSplashObj;// 물 웅덩이 파티클
    public Transform myTr;
    public bool isWater = false;
    public Coroutine needleCor;
    public Transform _centerPos;

    // 파티클 연결 
    [HideInInspector] public GameObject particleObject;
    // 캐릭터 애니메이션 작업 중
    private CharacterManager characterMgr;
    private PetManager petMgr;

    // 수심 
    int _depthLength;

    UserData _userData;

    // 블루투스 데이터
    public BLETotal bleTotal;

    // 대전 모드일 때 게임이 끝나면 바늘이 물에 닿아도 아무 일이 일어나지 않는다.
    [HideInInspector] public bool _isPause = false;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        myTr = transform;
        _rigidbody = GetComponent<Rigidbody>();

        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        cameraMgr = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameMgr.needleControl = GetComponent<NeedleControl>();

        // 물고기 작업 시작
        fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        fishControl.SetNeedleControlInstance(GetComponent<NeedleControl>());
        fishControl.Target = myTr;
        fishControl.NeedleCenterPos = _centerPos;

        // 파티클 연결 작업
        particleObject = GameObject.FindGameObjectWithTag("Object").GetComponent<ObjectManager>()._splashObject;

        // 캐릭터 애니메이션 작업 중
        characterMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
        petMgr = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>();

        // 블루투스 오브젝트 연결
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();
            bleTotal.needleControl = this;
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if (collider.CompareTag("Water") && !isWater && !_isPause)
        {
            if (characterMgr.GetReelAudioObject() != null)
            {
                characterMgr.GetReelAudioObject().GetComponent<AudioSource>().loop = false;
                characterMgr.GetReelAudioObject().GetComponent<AudioPoolObject>().ReturnThis();
                characterMgr.SetReelAudioObject(null);
            }

            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.sinkerReachesTheSurface).GetComponent<AudioPoolObject>().Init();

            // 파티클 작업
            particleObject.transform.position = new Vector3(myTr.position.x, -1.5f, myTr.position.z);
            particleObject.SetActive(true);

            isWater = true;
            gameMgr.NeedleReset();
            fishControl.IsCatch = false;

            transform.eulerAngles = Vector3.zero;
            transform.GetChild(0).localEulerAngles = Vector3.zero;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.drag = 1;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;


            gameMgr.IsFly = false;
            gameMgr.NeedleInWater = true;

            if (gameMgr._userData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
            {
                gameMgr.SetSinkerObjectActive(false);
            }

            Vibration.Cancel();

            needleCor = StartCoroutine(NeedleMove());
            StartCoroutine(MakeDelay(2, () =>
            {
                particleObject.SetActive(false);
                cameraMgr.ResetCamera();
                _ingameUIManager.DistanceDepthTextOn();

            }));
            
            // 물고기 작업 시작
            fishControl.IsFind = false;
            
            gameMgr.SettingCharacterAnimator(_throwHash, false);

            // #블루투스
            if (bleTotal != null && bleTotal.ConnectedMain && !DataManager.INSTANCE._tutorialIsInProgress)
            {
                // 봉돌을 장착 중이라면
                if(_userData.GetCurrentEquipmentDictionary()["sinker"] != -1)
                {
                    switch(_userData.GetCurrentEquipmentDictionary()["sinker"])
                    {
                        case 0:
                            fishControl._normalBLDC = 10 + 2;
                            break;
                        case 1:
                            fishControl._normalBLDC = 10 + 4;
                            break;
                        case 2:
                            fishControl._normalBLDC = 10 + 6;
                            break;
                        case 3:
                            fishControl._normalBLDC = 10 + 8;
                            break;
                        case 4:
                            fishControl._normalBLDC = 10 + 10;
                            break;

                    }
                }
                else
                {
                    fishControl._normalBLDC = 10;
                }

                fishControl._bldcMax = 25;
                fishControl.dc = 0;
                bleTotal.Motor(fishControl._normalBLDC, fishControl.dc);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sand"))
            gameMgr.NeedleStopMoving();
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
    public IEnumerator NeedleMove()
    {
        WaitForSeconds waitTime = PublicDefined._05secDelay;
        float sinkerWeight = DataManager.INSTANCE._sinkerWeight;
        float lureWeight = DataManager.INSTANCE._lureWeight;
        int currentBait = DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["bait"];
        _depthLength = DataManager.INSTANCE._depthLength * -1;

        // 기본적으로 움직이기 시작하기 때문에.
        gameMgr.NeedleStartMoving();

        while (gameMgr.style == GameManager.GameStyle.Bobber && !fishControl.IsBite)
        {
            if (myTr.position.y > _depthLength - 1.5f)
            {
                _rigidbody.AddForce(new Vector3(0, -5f + (sinkerWeight * -0.1f), 0));
            }
            else
            {
                _rigidbody.AddForce(new Vector3(0, 3.5f /*+ (DataManager._sinkerWeight * 0.05f)*/, 0));

                if (!_ingameUIManager._Reeling._IsReeling && !gameMgr.IsEging && gameMgr.IsNeedleMoving)
                    gameMgr.NeedleStopMoving();
            }

            yield return waitTime;
        }

        while (gameMgr.style == GameManager.GameStyle.Onetwo && !fishControl.IsBite)
        {
            // 찌와 같은 부력
            if(currentBait.Equals(53) || currentBait.Equals(54))
            {
                // 화면상 깊이 : 0.5m 아래로 떨어지면
                if (myTr.position.y < -2)
                {
                    _rigidbody.AddForce(new Vector3(0, 2f, 0));

                    if (!_ingameUIManager._Reeling._IsReeling && !gameMgr.IsEging && gameMgr.IsNeedleMoving)
                        gameMgr.NeedleStopMoving();
                }
                else
                {
                    _rigidbody.AddForce(new Vector3(0, -2f, 0));
                }
            }
            else
            {
                _rigidbody.AddForce(new Vector3(0, -5f + (sinkerWeight * -0.1f) + (lureWeight * -0.1f), 0));
            }
            yield return waitTime;
        }
    }

    public void NeedleMoveRestart()
    {
        needleCor = StartCoroutine(NeedleMove());
    }

    public void CastingPowerSetting(float powerX, float powerY, float powerZ)
    {
        _castingPowerX = powerX;
        _castingPowerY = powerY;
        _castingPowerZ = powerZ;
    }

    public void CastingNeedle()
    {
        AddForceToNeedle(_castingPowerX, _castingPowerY, _castingPowerZ);
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        transform.GetChild(0).localEulerAngles = new Vector3(90, 0, 0);
    }

    public void AddForceToNeedle(float x, float y, float z)
    {
        _rigidbody.AddForce(new Vector3(x, y, z));
    }

    public void NeedleReset()
    {
        // AddForce 초기화
        _rigidbody.velocity = Vector3.zero;
        // 저항 초기화
        _rigidbody.drag = 0;
        // 움직임 금지
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        // 중력 금지
        _rigidbody.useGravity = false;
    }

    public Transform GetNeedleControlTransform()
    {
        return this.transform;
    }
}