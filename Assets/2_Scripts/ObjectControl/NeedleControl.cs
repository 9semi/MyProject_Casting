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
    
    InGameUIManager _ingameUIManager;
    CameraManager _cameraManager;
    GameManager _gameManager;
    CharacterManager _characterManager;
    PetManager _petManager;
    FishControl _fishControl;
    UserData _userData;
    BLETotal _bleTotal;

    Coroutine _needleCoroutine; public Coroutine NeedleCoroutine { get { return _needleCoroutine; } set { _needleCoroutine = value; } }
    Transform _centerPos;
    GameObject _particleObject;
    Rigidbody _rigidbody;
    bool _isWater = false; public bool IsWater { set { _isWater = value; } }
    bool _isPause = false; public bool IsPause { set { _isPause = value; } }
    int _depthLength;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        _cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.NeedleControl = GetComponent<NeedleControl>();

        _fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        _fishControl.SetNeedleControlInstance(GetComponent<NeedleControl>());
        _fishControl.Target = transform;
        _fishControl.NeedleCenterPos = _centerPos;
        _particleObject = GameObject.FindGameObjectWithTag("Object").GetComponent<ObjectManager>()._splashObject;

        _characterManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
        _petManager = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>();

        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            _bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();
            _bleTotal.SetNeedleControl(this);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if (collider.CompareTag("Water") && !_isWater && !_isPause)
        {
            if (_characterManager.GetReelAudioObject() != null)
            {
                _characterManager.GetReelAudioObject().GetComponent<AudioSource>().loop = false;
                _characterManager.GetReelAudioObject().GetComponent<AudioPoolObject>().ReturnThis();
                _characterManager.SetReelAudioObject(null);
            }

            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.sinkerReachesTheSurface).GetComponent<AudioPoolObject>().Init();

            _particleObject.transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
            _particleObject.SetActive(true);

            _isWater = true;
            _gameManager.NeedleReset();
            _fishControl.IsCatch = false;

            transform.eulerAngles = Vector3.zero;
            transform.GetChild(0).localEulerAngles = Vector3.zero;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.drag = 1;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;


            _gameManager.IsFly = false;
            _gameManager.NeedleInWater = true;

            if (_gameManager.UserData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
            {
                _gameManager.SetSinkerObjectActive(false);
            }

            Vibration.Cancel();

            _needleCoroutine = StartCoroutine(NeedleMove());
            StartCoroutine(MakeDelay(2, () =>
            {
                _particleObject.SetActive(false);
                _cameraManager.ResetCamera();
                _ingameUIManager.DistanceDepthTextOn();

            }));
            

            _fishControl.IsFind = false;
            
            _gameManager.SettingCharacterAnimator(_throwHash, false);


            if (_bleTotal != null && _bleTotal.ConnectedMain && !DataManager.INSTANCE._tutorialIsInProgress)
            {

                if(_userData.GetCurrentEquipmentDictionary()["sinker"] != -1)
                {
                    switch(_userData.GetCurrentEquipmentDictionary()["sinker"])
                    {
                        case 0:
                            _fishControl.NormalBLDC = 10 + 2;
                            break;
                        case 1:
                            _fishControl.NormalBLDC = 10 + 4;
                            break;
                        case 2:
                            _fishControl.NormalBLDC = 10 + 6;
                            break;
                        case 3:
                            _fishControl.NormalBLDC = 10 + 8;
                            break;
                        case 4:
                            _fishControl.NormalBLDC = 10 + 10;
                            break;

                    }
                }
                else
                {
                    _fishControl.NormalBLDC = 10;
                }

                _fishControl.BldcMax = 25;
                _fishControl.DcValue = 0;
                _bleTotal.Motor(_fishControl.NormalBLDC, _fishControl.DcValue);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sand"))
            _gameManager.NeedleStopMoving();
    }
    IEnumerator MakeDelay(int delayNumber, Action action)
    {

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
        
        _gameManager.NeedleStartMoving();

        while (_gameManager.GameStyleSstate == GameManager.eGameStyle.Bobber && !_fishControl.IsBite)
        {
            if (transform.position.y > _depthLength - 1.5f)
            {
                _rigidbody.AddForce(new Vector3(0, -5f + (sinkerWeight * -0.1f), 0));
            }
            else
            {
                _rigidbody.AddForce(new Vector3(0, 3.5f, 0));

                if (!_ingameUIManager._Reeling.IsReeling && !_gameManager.IsEging && _gameManager.IsNeedleMoving)
                    _gameManager.NeedleStopMoving();
            }

            yield return waitTime;
        }

        while (_gameManager.GameStyleSstate == GameManager.eGameStyle.Onetwo && !_fishControl.IsBite)
        {
            if(currentBait.Equals(53) || currentBait.Equals(54))
            {
                if (transform.position.y < -2)
                {
                    _rigidbody.AddForce(new Vector3(0, 2f, 0));

                    if (!_ingameUIManager._Reeling.IsReeling && !_gameManager.IsEging && _gameManager.IsNeedleMoving)
                        _gameManager.NeedleStopMoving();
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
        _needleCoroutine = StartCoroutine(NeedleMove());
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
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.drag = 0;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.useGravity = false;
    }

    public Transform GetNeedleControlTransform()
    {
        return this.transform;
    }
}