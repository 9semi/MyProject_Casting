﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControl : FishBase
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

    [SerializeField] InGameUIManager _ingameUIManager;
    [SerializeField] FishPopUp _fishPopUp;
    [SerializeField] GameObject _attackSuccessObject;
    [SerializeField] GameObject _attackFailObject;
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject arrowLeft;
    [SerializeField] GameObject arrowRight;
    [SerializeField] GameObject arrowUp;
    [SerializeField] ParticleSystem reelEft;
    [Header("FishObjectManager")]
    [SerializeField] FishObjectManager _jeongdongjinFishManager;
    [SerializeField] FishObjectManagerSkyway _skywayFishManager;
    [SerializeField] FishObjectManagerHomerspit _homerspitFishManager;
    [Header("매치 모드일 때")]
    [SerializeField] GameObject _currentMatchStateObject;

    List<stRareFish> _rareFishList = new List<stRareFish>(); public List<stRareFish> GetRareFishList() { return _rareFishList; }
    PetManager _petManager;
    UserData _userData;
    BLETotal _bleTotal;
    TensionUI _tensionUI; public TensionUI _TensionUI { set { _tensionUI = value; } }
    CharacterManager _characterManager;
    NeedleControl _needleControl; public void SetNeedleControlInstance(NeedleControl instance) { _needleControl = instance; }
    ReelBlueToothData _reelData; public void SetReelData(ReelBlueToothData reelData) { _reelData = reelData; }

    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }
    PublicDefined.stFishInfo _fishDataForPassCheck;
    Animator _fishAni;
    Transform _target; public Transform Target { get { return _target; } set { _target = value; } }
    Transform _needleCenterPos; public Transform NeedleCenterPos { set { _needleCenterPos = value; } }
    Transform _fishTransform; public Transform FishTransform { get { return _fishTransform; } set { _fishTransform = value; } }
    bool _isFind = false; public bool IsFind { get { return _isFind; } set { _isFind = value; } }
    bool _isBite = false; public bool IsBite { get { return _isBite; } }
    bool _isCatch = false; public bool IsCatch { get { return _isCatch; } set { _isCatch = value; } }
    bool _isStart = false; public bool IsStart { get { return _isStart; } set { _isStart = value; } }
    bool _isFighting = false; public bool IsFighting { get { return _isFighting; } set { _isFighting = value; } }
    bool _isOver = false;
    bool _isDeath = false; public bool IsDeath { get { return _isDeath; } set { _isDeath = value; } }
    bool _isCenter = false; 
    bool _isLeft = false;  
    bool _isRight = false; 
    bool _isSpecialAttack = false; public bool IsSpecialAttack { get { return _isSpecialAttack; } set { _isSpecialAttack = value; } }
    bool _firstVib;
    bool _secondVib;
    bool _thirdVib;
    bool _fourthVib;
    int _specialAttackPointRectTr_Y = 38;

    Coroutine _bittingGageCoroutine;
    Coroutine _specialAttackCoroutine;
    Coroutine biteCor;
    Coroutine fightCor;
    Coroutine dieCor;
    Coroutine _shutCoroutine;
    Coroutine _motorStopCoroutine; public Coroutine _MotorStopCoroutine
    { get { return _motorStopCoroutine; } set { _motorStopCoroutine = value; } }
    Coroutine _fishMoveToNeedleCoroutine; public Coroutine FishMoveToNeedleCoroutine
    { get { return _fishMoveToNeedleCoroutine; } set { _fishMoveToNeedleCoroutine = value; } }
    Coroutine _dcCoroutine; public Coroutine DcCoroutine
    { get { return _dcCoroutine; } set { _dcCoroutine = value; } }
    GameObject fishSkin;
    GameObject _fishObject;
    GameObject _fishResisteAudioObject; public GameObject FishResisteAudioObject
    { get { return _fishResisteAudioObject; } set { _fishResisteAudioObject = value; } }
    int _normalBLDC; public int NormalBLDC { get { return _normalBLDC; } set { _normalBLDC = value; } }
    int _fishBLDC; public int FishBLDC { get { return _fishBLDC; } set { _fishBLDC = value; } }
    int _dcValue; public int DcValue { get { return _dcValue; } set { _dcValue = value; } }
    int _bldcMax = 0; public int BldcMax { get { return _bldcMax; } set { _bldcMax = value; } }
    int _powerCount = 0;
    float rand;
    float term;
    bool _isDCing; public bool IsDcing { get { return _isDCing; } set { _isDCing = value; } }
    bool _isTutorial;
    bool _tutorialBittingStop;
    bool _isConnectedToBluetooth; public bool IsConnectedToBluetooth { get { return _isConnectedToBluetooth; } }
    bool _bittingPermission;
    bool _isMatchMode;
    bool _isPause; public bool IsPause { get { return _isPause; } set { _isPause = value; } }
    bool isX = false, isY = false, isZ = false;
    public bool GetFishMovementDirection(int xyzNumber)
    {
        switch (xyzNumber)
        {
            case 0: return isX;
            case 1: return isY;
            case 2: return isZ;
            default: return false;
        }
    }
    

    private void Awake()
    {
        _tutorialBittingStop = false;
    }

    void Start()
    {
        _isTutorial = DataManager.INSTANCE._tutorialIsInProgress;
        _isMatchMode = DataManager.INSTANCE._matchGameIsInProgress;
        _characterManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
        _petManager = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>();
        fightCor = null;
        biteCor = null;
        _shutCoroutine = null;
        _userData = DBManager.INSTANCE.GetUserData();
        
        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            _bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();

            if (_bleTotal.ConnectedMain)
            {
                _isConnectedToBluetooth = true;
                _reelData = new ReelBlueToothData();
                _bleTotal.SetFishControl(this);
            }
        }
    }

    public IEnumerator FishMovetoNeedle()
    {
        while (true)
        {
            _fishTransform.position = _needleCenterPos.position;
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
    
    public IEnumerator DC()
    {
        switch (_fishData._motorType)
        {
            case 0:
                _dcValue = 0;
                break;
            case 1:
                while (_isStart || _isBite || _isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 10f);
                    _dcValue = 0;
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    _dcValue = UnityEngine.Random.Range(50, 71);
                    rand = UnityEngine.Random.Range(1f, 4f);
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);
                }
                break;
            case 2:
                while (_isStart || _isBite || _isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 8f);
                    _dcValue = 0;
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    _dcValue = UnityEngine.Random.Range(65, 86);
                    rand = UnityEngine.Random.Range(1f, 4f);
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);

                }
                break;
            case 3:
                while (_isStart || _isBite || _isCatch)
                {
                    rand = UnityEngine.Random.Range(1f, 5f);
                    _dcValue = 0;
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = false;
                    yield return new WaitForSeconds(rand);
                    _dcValue = UnityEngine.Random.Range(80, 100);
                    rand = UnityEngine.Random.Range(1, 4);
                    _bleTotal.Motor(_fishBLDC, _dcValue);
                    _isDCing = true;
                    yield return new WaitForSeconds(rand);
                }
                break;
        }
    }
    
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
    
    public void SetFish(GameObject fishObject, GameObject _fishSkin, Transform __fishTransform, stFishData fishData)
    {
        _fishObject = fishObject;
        fishSkin = _fishSkin;
        _fishTransform = __fishTransform;
        _fishData = fishData;
        _fishAni = __fishTransform.GetComponent<Animator>();

        _fishDataForPassCheck = new PublicDefined.stFishInfo(_fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._gradeType);
    }
    public IEnumerator ShutInEdge()
    {
        while(!_isDeath)
        {
            if (_target.position.z < 7 && _powerCount < 4 && !_isTutorial) // 물고기가 나랑 가까울 때
            {
                _gameManager.AddForceToNeedle(0, 0, 200);
            }
            else if (_target.position.x < -_target.position.z)
            {
                _gameManager.AddForceToNeedle(120, 0, 0);
            }
            else if (_target.position.x > _target.position.z)
            {
                _gameManager.AddForceToNeedle(-120, 0, 0);
            }
            else if (_target.position.z > 60)
            {
                _gameManager.AddForceToNeedle(0, 0, -200);
            }
            else if (_target.position.y > -3f)
            {
                _gameManager.AddForceToNeedle(0, -180, 0);
            }
            yield return PublicDefined._02secDelay;
        }
    }
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

        _gameManager.CurrentState = GameManager.eIngameState.fighting;
        _gameManager.IsNoBite = true; 
        _petManager.IsCatch = true;

        if (_petManager.ItemUIState.Equals(PetManager.eItemUIState._on))
        {
            _petManager.ClickItemButton();
        }
        _isDeath = false;
        _isFighting = true;
        fishSkin.SetActive(true);
        _gameManager.FishCaught = _fishTransform.GetChild(0).GetChild(0).transform;
        _needleControl.StopCoroutine(_needleControl.NeedleCoroutine); 
        _needleControl.NeedleCoroutine = null;
        _tensionUI.WeightGage(_fishData._weight, _userData);

        OnDie();
        
        if (_bleTotal != null && _bleTotal.ConnectedMain)
        { 
            _fishBLDC = (int)(_fishData._weight * 10) + 20;

            if (_fishBLDC > 85)
                _fishBLDC = 85;

            _dcCoroutine = StartCoroutine(DC());
        }

        while (!_isDeath) 
        {
            term = UnityEngine.Random.Range(3f, 4.5f);

            FightPower(_fishData._activityType, term);
            
            if (_bleTotal != null && _bleTotal.ConnectedMain && !_isTutorial)
            {
                _bleTotal.Motor(BLDC(isY, isZ), _dcValue);
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
        if (_isTutorial)
            return;

        int randX, randY, randZ;
        int x, y, z; 
        int yPower, zPower;

        randX = UnityEngine.Random.Range(0, 2);
        randY = UnityEngine.Random.Range(0, 10);
        randZ = UnityEngine.Random.Range(0, 10);

        x = 0; y = 0; z = 0;

        _fishTransform.eulerAngles = new Vector3(0, 180, 0);
        
        if (randX.Equals(0))
        {
            x = UnityEngine.Random.Range(-35, -25);
        }
        else
        {
            x = UnityEngine.Random.Range(25, 35);
        }

        yPower = UnityEngine.Random.Range(5, 7);
        zPower = UnityEngine.Random.Range(25, 45);

        switch (type)
        {
            case 0: 
                if (randY < 4)
                {
                    y = yPower - 2;
                    isY = false;
                    _fishTransform.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    _fishTransform.Rotate(0, 0, -30);
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
                    _fishTransform.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    _fishTransform.Rotate(0, 0, -30);
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
                    _fishTransform.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    _fishTransform.Rotate(0, 0, -30);
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
                    _fishTransform.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    _fishTransform.Rotate(0, 0, -30);
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
                    _fishTransform.Rotate(0, 0, 15);
                }
                else
                {
                    y = -yPower;
                    isY = true;
                    _fishTransform.Rotate(0, 0, -30);
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
        
        Vector3 cross = Vector3.Cross(Vector3.forward.normalized, _needleControl.transform.position.normalized);

        float xPower = 0;
        
        if (randX.Equals(0))
            xPower = x * power - _target.position.z;
        else
            xPower = x * power + _target.position.z;

        if (!z.Equals(0))
            xPower *= 0.75f;

        if(_powerCount > 2)
            _gameManager.AddForceToNeedle(xPower, y * power, z * power * 0.8f);
        else
            _gameManager.AddForceToNeedle(xPower, y * power, z * power);

        _powerCount++;
        
        if (z > 0 || _isSpecialAttack || _needleControl.transform.position.z < 9.5f)
            return;
    
        if ((y < 0) || (_needleControl.transform.position.x > -0.25f && _needleControl.transform.position.x < 0.25f))
        {
            SpecialAttack(0);
        }
        else if (cross.y > 0)
        {
            if(_needleControl.transform.position.z < 20)
            {
                if (_needleControl.transform.position.x < 1.5f && x < 0)
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
                if (_needleControl.transform.position.x < 0.5f && x < 0)
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
        else if(cross.y < 0)
        {
            if (_needleControl.transform.position.z < 20)
            {
                if (_needleControl.transform.position.x > -1.5f && x > 0)
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
                if (_needleControl.transform.position.x > -0.5f && x > 0)
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
        _specialAttackCoroutine = StartCoroutine(_specialAttackCoroutineoutine(direct));
    }
    
    public IEnumerator _specialAttackCoroutineoutine(int direct)
    {
        float specialAttackGageX = 0;
        float time = 0;
        float successCount = 0;
        float successStandard;
        bool _isSpecialAttackSuccess = false;
        _isSpecialAttack = true;
        _ingameUIManager.FishingState(6);
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
                                if (successCount.Equals(0))
                                {
                                    _gameManager.SettingCharacterAnimator(_centerHash, true);
                                    _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                    _isCenter = true;
                                }

                                if (successCount >= 2f)
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
                            if (successCount.Equals(0))
                            {
                                _gameManager.SettingCharacterAnimator(_centerHash, true);
                                _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                _isCenter = true;
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
                                _gameManager.IsPause = true;

                                successCount += Time.deltaTime;

                                if (successCount.Equals(0))
                                {
                                    _gameManager.SettingCharacterAnimator(_leftHash, true);
                                    _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                    _isLeft = true;
                                }

                                if (successCount >= 2f)
                                {
                                    _isSpecialAttackSuccess = true;
                                    _tensionUI.Success_SpecialAttack(direct);
                                }
                            }
                            else
                            {
                                _gameManager.IsPause = false;
                            }
                        }
                    }
                    else
                    {
                        if ((Input.acceleration.x > successStandard || Input.GetKey(KeyCode.D)) && !_isPause)
                        {
                            _gameManager.IsPause = true;

                            if (successCount.Equals(0))
                            {
                                _gameManager.SettingCharacterAnimator(_leftHash, true);
                                _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                _isLeft = true;
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
                            _gameManager.IsPause = false;
                        }
                    }
                    time += Time.deltaTime;

                    specialAttackGageX = successCount * 98f;

                    if (specialAttackGageX > 196)
                        specialAttackGageX = 196f;

                    _tensionUI.GetSpecialAttackGage(2).sizeDelta = new Vector2(specialAttackGageX, _specialAttackPointRectTr_Y);
                    yield return null;
                }

                _gameManager.IsPause = false;
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
                                _gameManager.IsPause = true;
                                if (successCount.Equals(0))
                                {
                                    _gameManager.SettingCharacterAnimator(_rightHash, true);
                                    _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                    _isRight = true;
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
                                _gameManager.IsPause = false;
                            }
                        }
                    }
                    else
                    {
                        if ((Input.acceleration.x < successStandard || Input.GetKey(KeyCode.A)) && !_isPause)
                        {
                            _gameManager.IsPause = true;

                            if (successCount.Equals(0))
                            {
                                _gameManager.SettingCharacterAnimator(_rightHash, true);
                                _gameManager.SettingCharacterAnimator(_blockspeedHash, 1);
                                _isRight = true;
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
                            _gameManager.IsPause = false;
                        }
                    }
                    time += Time.deltaTime;

                    specialAttackGageX = successCount * 98f;

                    if (specialAttackGageX > 196)
                        specialAttackGageX = 196f;

                    _tensionUI.GetSpecialAttackGage(0).sizeDelta = new Vector2(specialAttackGageX, _specialAttackPointRectTr_Y);
                    yield return null;
                }

                _gameManager.IsPause = false;
                break;
        }

        _gameManager.IsPause = false;

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
            _attackSuccessObject.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
        }
        // 특수 공격 실패
        else
        {
            _tensionUI.ResetColor_SpecialAttack();
            _isSpecialAttack = false;
            _attackFailObject.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
        }

        switch (direct)
        {
            case 0:
                _gameManager.SettingCharacterAnimator(_centerHash, false);
                _isCenter = false;
                arrowUp.SetActive(false);
                break;
            case 1:
                _gameManager.SettingCharacterAnimator(_leftHash, false);
                _isLeft = false;
                arrowRight.SetActive(false);
                break;
            case 2:
                _gameManager.SettingCharacterAnimator(_rightHash, false);
                _isRight = false;
                arrowLeft.SetActive(false);
                break;
        }

        StopCoroutine(_specialAttackCoroutine);
    }

    IEnumerator SpecialAttackSuccessCoroutine()
    {
        yield return PublicDefined._05secDelay;

        _isSpecialAttack = false;
        _tensionUI.ResetColor_SpecialAttack();

        yield return null;
    }
    
    public IEnumerator Bitting(int second1, int second2, int chance1, int chance2)
    {
        float vib = 0;
        int randChance;

        _gameManager.StopHookSet();

        _isStart = true;
        _isOver = false;
        _ingameUIManager.FishingState(2);
        _ingameUIManager.HidePassContent();
        
        if(_petManager.ItemUIState.Equals(PetManager.eItemUIState._on))
        {
            _petManager.ClickItemButton();
        }

        if (_isMatchMode)
            _currentMatchStateObject.SetActive(false);

        AudioManager.INSTANCE.SaveBGMPlayerCurrentTime();
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.bitting, true);
        _bittingGageCoroutine = StartCoroutine(_tensionUI.BittingGage(second1, second2));
        _tensionUI.ResetColor_SpecialAttack();
        _tensionUI.ResetGuide();
        
        if (_bleTotal != null && _bleTotal.ConnectedMain)
        {
            _fishBLDC = (int)(_fishData._weight * 10) + 20;

            if (_fishBLDC > 85)
                _fishBLDC = 85;

            _bleTotal.Motor(_fishBLDC, 0);
        }
        while (_tensionUI._RedGageBarRectTransform.sizeDelta.x < 900 && _isStart && !_isPause)
        {
            // 블루투스 O
            if (_isConnectedToBluetooth)
            {
                // 신형 릴
                {
                    if (_reelData.Zg > 20000 && !_tutorialBittingStop)
                    {
                        _gameManager.SettingCharacterAnimator(_hooksetHash);
                        randChance = UnityEngine.Random.Range(0, 10);
                        
                        if (_tensionUI._RedGageBarRectTransform.sizeDelta.x >= _tensionUI._MinX && _tensionUI._RedGageBarRectTransform.sizeDelta.x <= _tensionUI._MaxX)
                        {
                            // 성공
                            if (randChance < chance1 && _gameManager.NeedleControl.GetNeedleControlTransform().position.z > 5.5f)
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                                _ingameUIManager.FishingState(3);
                                _ingameUIManager.FishingState(4);
                                _isStart = false;
                                _isBite = true;
                                _fishMoveToNeedleCoroutine = StartCoroutine(FishMovetoNeedle());
                                OnFight();
                                OnShutInEdge();
                                
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
                                _isStart = false;

                                if (_bleTotal != null && _bleTotal.ConnectedMain)
                                {
                                    _dcValue = 0;
                                    _bleTotal.Motor(_normalBLDC, _dcValue);
                                }

                            }
                        }
                        // 게이지 밖
                        else
                        {
                            if (randChance < chance2 && _gameManager.NeedleControl.GetNeedleControlTransform().position.z > 5.5f)
                            {
                                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                                _ingameUIManager.FishingState(3);
                                _ingameUIManager.FishingState(4);
                                _isStart = false;
                                _isBite = true;
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
                                _isStart = false;
                                _dcValue = 0;
                                _bleTotal.Motor(_normalBLDC, _dcValue);

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
                if ((_gameManager.AngleGyro.x > 7 || (_bleTotal != null && _gameManager.AngleGyro.x > 4) || Input.GetButtonDown("Jump")) && !_tutorialBittingStop)
                {
                    _gameManager.SettingCharacterAnimator(_hooksetHash);
                    randChance = UnityEngine.Random.Range(0, 10);
                    
                    if (_tensionUI._RedGageBarRectTransform.sizeDelta.x >= _tensionUI._MinX && _tensionUI._RedGageBarRectTransform.sizeDelta.x <= _tensionUI._MaxX)
                    {
                        // 성공
                        if (randChance < chance1 && _gameManager.NeedleControl.GetNeedleControlTransform().position.z > 5.5f)
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                            _ingameUIManager.FishingState(3);
                            _ingameUIManager.FishingState(4);
                            _isStart = false;
                            _isBite = true;
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
                            _isStart = false;

                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(300);
                        }
                    }
                    // 게이지 밖
                    else 
                    {
                        if (randChance < chance2 && _gameManager.NeedleControl.GetNeedleControlTransform().position.z > 5.5f)
                        {
                            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackSuccess).GetComponent<AudioPoolObject>().Init();
                            _ingameUIManager.FishingState(3);
                            _ingameUIManager.FishingState(4);
                            _isStart = false;
                            _isBite = true;
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
                            _isStart = false;

                            if (DataManager.INSTANCE._vibration)
                                Vibration.Vibrate(300);
                        }
                    }
                }
            }
            if (_tensionUI._GageCondition >= vib && DataManager.INSTANCE._vibration)
            {
                Vibration.Vibrate(100); 
                vib += 64f;
            }
            yield return null;
        }
        
        if (!_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
        {
            DataManager.INSTANCE.RemoveBaitWhenBaitUse();
        }
        
        StopCoroutine(_bittingGageCoroutine);
        _bittingGageCoroutine = null;
        _isStart = false;
        PlayBGMWhenBittingFail();
        _isOver = false;
        _isBite = false;

        if (!_isBite)
        {
            if (_bleTotal != null && _bleTotal.ConnectedMain)
            {
                _bleTotal.Motor(_normalBLDC, _dcValue);
            }

            _ingameUIManager.FishingState(3);
            _ingameUIManager.FishingState(5);
           
            switch(DataManager.INSTANCE._mapType)
            {
                case PublicDefined.eMapType.jeongdongjin:
                    _fishTransform.GetComponent<Fish>().Restart();
                    break;
                case PublicDefined.eMapType.skyway:
                    _fishTransform.GetComponent<FishSkyway>().Restart();
                    break;
                case PublicDefined.eMapType.homerspit:
                    _fishTransform.GetComponent<FishHomerspit>().Restart();
                    break;
            }

            if (_fishMoveToNeedleCoroutine != null)
            {
                StopCoroutine(_fishMoveToNeedleCoroutine);
                _fishMoveToNeedleCoroutine = null;
            }

            _isOver = false;

            yield return PublicDefined._2secRealDelay;

            if (_isMatchMode)
                _currentMatchStateObject.SetActive(true);
            else
                _ingameUIManager.ShowPassContent();

            _tensionUI.SetGageObject(false);
            _isFind = false;
        }
        StopCoroutine(biteCor);
    }

    public IEnumerator Dying()
    {
        while (_isBite)
        {
            if (_isDeath)
            {
                _gameManager.CurrentState = GameManager.eIngameState.idle;
                _gameManager.HideBait();
                _gameManager.IsPause = true;
                
                if (_bleTotal != null && _bleTotal.ConnectedMain)
                {
                     MotorStop();
                }
                if (_isSpecialAttack)
                {
                    StopSpecialAttack();
                }
                StopCoroutine(fightCor);
                StopCoroutine(_shutCoroutine);

                _isBite = false;
                _isCatch = true;

                if(_fishData._fishDBNumber.Equals(28))
                    _fishObject.transform.eulerAngles = _diedFishRotate_octopus;
                else
                    _fishObject.transform.eulerAngles = _diedFishRotate;
                _gameManager.IsPlayingBGM = false;
                
                _gameManager.IsReset = true;
                _gameManager.ResetAction();
            }
            else if (!_isFighting)
            {
                _gameManager.CurrentState = GameManager.eIngameState.casting;
                
                if (_bleTotal != null && _bleTotal.ConnectedMain)
                {
                    if (_dcCoroutine != null)
                    {
                        StopCoroutine(_dcCoroutine);
                        _dcCoroutine = null;
                    }
                    _dcValue = 0;
                    _bleTotal.Motor(_normalBLDC, _dcValue);
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
                    _ingameUIManager.ShowPassContent();

                _ingameUIManager.FishingState(1); 
                _gameManager.IsPause = false;
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.specialAttackFail).GetComponent<AudioPoolObject>().Init();
                _isBite = false;
                _isStart = false;
                _isFind = false;

                if (_needleControl.NeedleCoroutine == null)
                {
                    _needleControl.NeedleMoveRestart();
                    
                }
                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        _fishTransform.GetComponent<Fish>().Restart();
                        break;
                    case PublicDefined.eMapType.skyway:
                        _fishTransform.GetComponent<FishSkyway>().Restart();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        _fishTransform.GetComponent<FishHomerspit>().Restart();
                        break;
                }

                if (_fishMoveToNeedleCoroutine != null)
                {
                    StopCoroutine(_fishMoveToNeedleCoroutine);
                    _fishMoveToNeedleCoroutine = null;
                }

                fishSkin.SetActive(false); 
                _gameManager.IsPlayingBGM = false;
                PlayBGMWhenBittingFail();
            }
            yield return null;
        }
        _powerCount = 0;
        StopCoroutine(dieCor);

    }

    public void MotorStop()
    {
        _motorStopCoroutine = StartCoroutine(MotorStopCoroutine());
    }

    IEnumerator MotorStopCoroutine()
    {
        if (_dcCoroutine != null)
        {
            StopCoroutine(_dcCoroutine);
            _dcCoroutine = null;
        }

        float timer = 0;
        
        if (_isDCing)
        {
            int temp = _dcValue;

            while (_dcValue > 0)
            {
                _dcValue -= temp / 3;
                timer += 1;

                if (_dcValue < 0)
                    _dcValue = 0;

                _bleTotal.Motor(15, _dcValue);

                if (timer > 4)
                    break;

                yield return PublicDefined._05secRealDelay;

                _dcValue = 0;
                _bleTotal.Motor(0, _dcValue);
            }
            _isDCing = false;
        }
        else
        {
            _dcValue = 0;
            _bleTotal.Motor(15, _dcValue);

            yield return PublicDefined._09secRealDelay;

            _dcValue = 0;
            _bleTotal.Motor(0, _dcValue);

        }

        _dcValue = 0;
        _bleTotal.Motor(0, _dcValue);
        _motorStopCoroutine = null;
    }

    public void RestartDCCoroutine()
    {
        if(_dcCoroutine == null)
        {
            _dcCoroutine = StartCoroutine(DC());
        }
    }
    public void Catching()
    {
        if (_isCatch && _isDeath)
        {
            _gameManager.SettingCharacterAnimator(_raiseHash);
            _gameManager.PickupFish();

            if (_isMatchMode)
            {
                if (_fishData._fishDBNumber.Equals(28))
                {
                    _fishTransform.localPosition = _caughtFIshPos;
                    _fishTransform.position += Vector3.down * 0.2f;
                    _fishTransform.GetChild(0).localPosition = Vector3.zero;
                    _fishTransform.localRotation = Quaternion.Euler(60, 0, 0);
                }
                else
                {
                    _fishTransform.localPosition = _caughtFIshPos;
                    _fishTransform.GetChild(0).localPosition = Vector3.zero;
                    _fishTransform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
                }

                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        _fishTransform.GetComponent<Fish>().StopCor();
                        break;
                    case PublicDefined.eMapType.skyway:
                        _fishTransform.GetComponent<FishSkyway>().StopCor();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        _fishTransform.GetComponent<FishHomerspit>().StopCor();
                        break;
                    default:
                        break;
                }

                _currentMatchStateObject.SetActive(true);
            }
            else
            {
                if (_fishData._fishDBNumber.Equals(28))
                {
                    _fishTransform.localPosition = _caughtFIshPos;
                    _fishTransform.GetChild(0).localPosition = Vector3.zero;
                }
                else
                {
                    _fishTransform.localPosition = _caughtFIshPos;
                    _fishTransform.GetChild(0).localPosition = Vector3.zero;
                    _fishTransform.GetChild(0).GetChild(0).localPosition = Vector3.zero;
                }

                switch (DataManager.INSTANCE._mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        _fishTransform.GetComponent<Fish>().StopCor();
                        break;
                    case PublicDefined.eMapType.skyway:
                        _fishTransform.GetComponent<FishSkyway>().StopCor();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        _fishTransform.GetComponent<FishHomerspit>().StopCor();
                        break;
                    default:
                        break;
                }
            }



            StartCoroutine(MakeDelay(2, () =>
            {
                _ingameUIManager.FishingState(0);
                
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishCatchSuccess).GetComponent<AudioPoolObject>().Init();

                AudioManager.INSTANCE.StopBGM();

                _gameManager.SettingCharacterAnimator(_raiseHash, false);

                _isCatch = false;
                _petManager.PetAni.SetBool(_catchHash, false);

                _fishData._lenth = (_fishData._lenth * 10000) * 0.01f;

                _fishData._weight = (_fishData._weight * 100) * 0.01f;


                if (_isMatchMode)
                    _fishPopUp.SetUpInfo_Match(_fishDataForPassCheck, _fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._info, _fishData._gradeType, _fishAni);
                else
                    _fishPopUp.SetUpInfo(_fishDataForPassCheck, _fishData._fishDBNumber, _fishData._name, _fishData._lenth, _fishData._weight, _fishData._price, _fishData._info, _fishData._gradeType, _fishAni);

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

            _isOver = true;
            
        }
    }
    public void PlayBGMWhenBittingFail()
    {
        _gameManager.IsPlayingBGM = true;
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
        _gameManager.SettingCharacterAnimator(_centerHash, false);
        arrowUp.SetActive(false);
        _gameManager.SettingCharacterAnimator(_leftHash, false);
        arrowLeft.SetActive(false);
        _gameManager.SettingCharacterAnimator(_rightHash, false);
        arrowRight.SetActive(false);
        StopCoroutine(_specialAttackCoroutine);
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
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }
}