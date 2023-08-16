using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class PetManager : MonoBehaviour
{
    readonly int _throwHash = Animator.StringToHash("Throw");
    readonly int isOnHash = Animator.StringToHash("isOn");

    public enum eItemUIState
    {
        _on,
        _off,
    }
    eItemUIState _itemUIState = eItemUIState._off; public eItemUIState ItemUIState { get { return _itemUIState; } }

    FishObjectManager _jeongdongjinFishManager;
    FishObjectManagerSkyway _skywayFishManager;
    FishObjectManagerHomerspit _homerspitFishManager;
    GameManager _gameManager;
    InGameUIManager _ingameUIManager;
    FishControl _fishControl;
    UserData _userData;

    bool _isPause;
    bool _isLightOn; public bool IsLightOn { get { return _isLightOn; } }
    bool _isCatch; public bool IsCatch { set { _isCatch = value; } }
    bool _tutorialThrowStop;
    bool _radarInUse;
    int _currentPasteBaitIndex = 0;
    float _progress; public float Progress { get { return _progress; } set { _progress = value; } }

    Animator _petAni; public Animator PetAni { get { return _petAni; } }
    Animator _itemOptionAni; 
    Coroutine baitCor;
    Vector3 _angleGyro = Vector3.zero;
    Gyroscope _gyroscope;
    
    [Header("jeongdongjin 레어 물고기 이미지(레이더용)")]
    [SerializeField] Sprite _blackporgySprite;
    [SerializeField] Sprite _largescaleblackfishSprite;
    [SerializeField] Sprite _japaneseamberjackSprite;
    [SerializeField] Sprite _japanesespanishmackerelSprite;
    [SerializeField] Sprite _seabassSprite;
    [SerializeField] Sprite _spottedseabassSprite;

    [Header("skyway 레어 물고기 이미지(레이더용")]
    [SerializeField] Sprite _blackgrouper;
    [SerializeField] Sprite _atlantictrpletail;
    [SerializeField] Sprite _redporgy;
    [SerializeField] Sprite _redtilefish;
    [SerializeField] Sprite _indopacificsailfish;
    [SerializeField] Sprite _swordfish;

    [Header("homerspit 레어 물고기 이미지(레이더용)")]
    [SerializeField] Sprite _bigskate;
    [SerializeField] Sprite _yellowfintuna;
    [SerializeField] Sprite _lingcod;
    [SerializeField] Sprite _yelloweyerockfish;
    [SerializeField] Sprite _quillbackrockfish;
    [SerializeField] Sprite _salmonshark;
    [SerializeField] Sprite _halibut;
    [SerializeField] Sprite _chinooksalmon;
    [Header("------------------------------------")]
    [SerializeField] BaitSpatulaControl _baitspatulaControl;
    [SerializeField] Sprite _nullSprite;
    
    [SerializeField] GameObject landingNet;
    [SerializeField] GameObject baitSpatulaula;
    [SerializeField] GameObject _baitBundle;
    [SerializeField] GameObject _itemOption;
    [SerializeField] Button _itemOptionButton;
    [SerializeField] Button _lampButton;
    [SerializeField] GameObject _pastebaitIsNullObject;
    [SerializeField] Light fishingLamp;
    [SerializeField] ParticleSystem radarParticle;
    [SerializeField] RectTransform[] _fishIcon;
    [SerializeField] RectTransform _canvasRect;

    private void Awake()
    {
        _petAni = GetComponent<Animator>();
        _tutorialThrowStop = false;
        _isLightOn = false;
    }
    private void Start()
    {
        InitializeInstance();
        
        _gyroscope = Input.gyro;
        _gyroscope.enabled = true;

        _userData = DBManager.INSTANCE.GetUserData();
        _itemOptionAni = _itemOption.GetComponent<Animator>();
        
        _petAni = GetComponent<Animator>();

        baitCor = null;
    }
    void InitializeInstance()
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManager>();
                return;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManagerSkyway>();
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager = GameObject.FindGameObjectWithTag("FishObjectManager").GetComponent<FishObjectManagerHomerspit>();
                break;
        }

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        _fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
    }

    public void Fishfinder()
    {
        if (!_radarInUse)
        {
            _radarInUse = true;
            radarParticle.Play();
            StartCoroutine(RadarCoroutine());
        }
    }

    IEnumerator RadarCoroutine()
    {
        int cnt = 0;

        yield return PublicDefined._05secDelay;
        
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.radar).GetComponent<AudioPoolObject>().Init();

        yield return PublicDefined._1secDelay;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
            case PublicDefined.eMapType.tutorial:
                _jeongdongjinFishManager.RadarSort();
                for (int i = 0; i < _jeongdongjinFishManager._RareFishList.Count; i++)
                {
                    Vector3 temp = _jeongdongjinFishManager._RareFishList[i].transform.position;
                    temp.y = (temp.z * 0.05f) - 1f;
                    
                    _fishIcon[cnt].sizeDelta = new Vector2(temp.z + 1.5f, temp.z + 1.5f);

                    Vector3 fishPos = Camera.main.WorldToViewportPoint(temp);

                    Vector3 iconPos = Camera.main.ViewportToWorldPoint(fishPos);

                    _fishIcon[cnt].GetComponent<Image>().sprite = GetJeongdongjinRareFishImage(_jeongdongjinFishManager._RareFishList[i].GetComponent<Fish>().GetFishDBNum());
                    _fishIcon[cnt].transform.position = iconPos;
                    _fishIcon[cnt].gameObject.SetActive(true);

                    cnt++;

                    yield return PublicDefined._02secDelay;
                }

                yield return PublicDefined._2secDelay;

                for (int k = 0; k < _fishIcon.Length; k++)
                {
                    if (_fishIcon[k].gameObject.activeSelf)
                    {
                        _fishIcon[k].gameObject.SetActive(false);
                    }
                }
                _radarInUse = false;
                radarParticle.Stop();
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager.RadarSort();
                for (int i = 0; i < _skywayFishManager._rareFishList.Count; i++)
                {
                    Vector3 temp = _skywayFishManager._rareFishList[i].transform.position;
                    temp.y = (temp.z * 0.05f) - 1f;
                    
                    _fishIcon[cnt].sizeDelta = new Vector2(temp.z + 1.5f, temp.z + 1.5f);

                    Vector3 fishPos = Camera.main.WorldToViewportPoint(temp);

                    Vector3 iconPos = Camera.main.ViewportToWorldPoint(fishPos);

                    _fishIcon[cnt].GetComponent<Image>().sprite = GetSkywayRareFishImage(_skywayFishManager._rareFishList[i].GetComponent<FishSkyway>().fishDBNum);
                    _fishIcon[cnt].transform.position = iconPos;
                    _fishIcon[cnt].gameObject.SetActive(true);
                    cnt++;

                    yield return PublicDefined._02secDelay;
                }

                yield return PublicDefined._2secDelay;

                for (int k = 0; k < _fishIcon.Length; k++)
                {
                    if (_fishIcon[k].gameObject.activeSelf)
                    {
                        _fishIcon[k].gameObject.SetActive(false);
                    }
                }
                _radarInUse = false;
                radarParticle.Stop();
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager.RadarSort();
                for (int i = 0; i < _homerspitFishManager._rareFishList.Count; i++)
                {
                    Vector3 temp = _homerspitFishManager._rareFishList[i].transform.position;
                    temp.y = (temp.z * 0.05f) - 1f;
                    
                    _fishIcon[cnt].sizeDelta = new Vector2(temp.z + 1.5f, temp.z + 1.5f);

                    Vector3 fishPos = Camera.main.WorldToViewportPoint(temp);

                    Vector3 iconPos = Camera.main.ViewportToWorldPoint(fishPos);

                    _fishIcon[cnt].GetComponent<Image>().sprite = GetHomerspitRareFishImage(_homerspitFishManager._rareFishList[i].GetComponent<FishHomerspit>().fishDBNum);
                    _fishIcon[cnt].transform.position = iconPos;
                    _fishIcon[cnt].gameObject.SetActive(true);
                    cnt++;

                    yield return PublicDefined._02secDelay;
                }

                yield return PublicDefined._2secDelay;

                for (int k = 0; k < _fishIcon.Length; k++)
                {
                    if (_fishIcon[k].gameObject.activeSelf)
                    {
                        _fishIcon[k].gameObject.SetActive(false);
                    }
                }
                _radarInUse = false;
                radarParticle.Stop();
                break;
        }
        yield return null;
    }
    
    public void ClickBaitThrow()
    {
        if (_gameManager.CurrentState.Equals(GameManager.eIngameState.fighting) || _radarInUse || _fishControl.IsFind || _gameManager.IsFly)
            return;

        if (_gameManager.BaitThrowMode)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
            
            _baitspatulaControl.StopRotateCoroutine();
            StopCoroutine(baitCor);
            
            baitSpatulaula.SetActive(false);

            _gameManager.BaitThrowMode = false;

            _ingameUIManager._Reeling.enabled = true;
            
            _ingameUIManager.SetPetGage(false);
            _ingameUIManager.ResetCharacterGage();
            _gameManager.Progress = 0;
            return;
        }

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        
        if (DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"].Equals(-1))
        {
            _pastebaitIsNullObject.SetActive(true);
            StartCoroutine(MakeDelay(2, () => _pastebaitIsNullObject.SetActive(false)));
            return;
        }
        else
        {
            _ingameUIManager._Reeling.enabled = false;
            _gameManager.BaitThrowMode = true;
            
            _baitspatulaControl.StartRotateCoroutine();
            
            baitCor = StartCoroutine(BaitDirection());
            
            baitSpatulaula.SetActive(true);
            
            _ingameUIManager.ResetPetGage();
            _ingameUIManager.SetCharacterGage(false);
            _progress = 0;
        }
    }

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        switch(delayNumber)
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

    public void BaitThrow()
    {
        _currentPasteBaitIndex = _userData.GetCurrentEquipmentDictionary()["pastebait"];
        
        _ingameUIManager.ResetCharacterGage();
        _ingameUIManager.SetPetGage(false);

        _gameManager.Progress = 0;
        _petAni.SetBool(_throwHash, true);

        if(!DataManager.INSTANCE._tutorialIsInProgress)
            _ingameUIManager._Reeling.enabled = true;

        _baitspatulaControl.StopRotateCoroutine();
        StopCoroutine(baitCor);
        
        StartCoroutine(MakeDelay(3, () =>
        {
            _gameManager.BaitThrowMode = false;
            baitSpatulaula.SetActive(false);
            _petAni.SetBool(_throwHash, false);
        }));
    }
    
    public void BaitThrowOn()
    {
        if (DataManager.INSTANCE._vibration)
            Vibration.Vibrate(2200);

        _baitBundle.SetActive(true);
    }
    
    public void OnLamp()
    {
        _lampButton.interactable = false;

        StartCoroutine(MakeDelay(1, () => _lampButton.interactable = true));

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.lamp).GetComponent<AudioPoolObject>().Init();
        Light();
        
        landingNet.SetActive(false);
    }
    
    void Light()
    {
        _isLightOn = !_isLightOn;
        fishingLamp.enabled = _isLightOn;
        
        if (_isLightOn)
        {
            switch (DataManager.INSTANCE._mapType)
            {
                case PublicDefined.eMapType.jeongdongjin:
                    _jeongdongjinFishManager.UpdateProbilityWhenLampOn();
                    break;
                case PublicDefined.eMapType.skyway:
                    _skywayFishManager.UpdateProbilityWhenLampOn();
                    break;
                case PublicDefined.eMapType.homerspit:
                    _homerspitFishManager.UpdateProbilityWhenLampOn();
                    break;
            }
        }
        else
        {
            switch (DataManager.INSTANCE._mapType)
            {
                case PublicDefined.eMapType.jeongdongjin:
                    _jeongdongjinFishManager.UpdateProbilityWhenLampOff();
                    break;
                case PublicDefined.eMapType.skyway:
                    _skywayFishManager.UpdateProbilityWhenLampOff();
                    break;
                case PublicDefined.eMapType.homerspit:
                    _homerspitFishManager.UpdateProbilityWhenLampOff();
                    break;
            }
        }
        
    }
    
    public IEnumerator BaitDirection() 
    {
        float powerSpeed = 0.5f;

        while (_gameManager.BaitThrowMode && !_isPause)
        {
            // 안드로이드
            #region 
            if (Application.platform.Equals(RuntimePlatform.Android))
            {
                if (_gameManager.IsConnectedToBluetooth_Reel)
                {
                    // 신형 릴
                    {
                        while (_progress <= 1f) 
                        {                 
                            _progress += powerSpeed * Time.deltaTime;
                            _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                            _angleGyro = _gyroscope.rotationRate;
                            if (_gameManager.BaitThrowMode && _gameManager.ReelData.Zg < -30000 && !_tutorialThrowStop && !_tutorialThrowStop)
                            {
                                BaitThrow();
                            }
                            yield return null;
                        }


                        while (_progress >= 0)
                        {
                            _progress -= powerSpeed * Time.deltaTime;
                            _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                            _angleGyro = _gyroscope.rotationRate;
                            if (_gameManager.BaitThrowMode && _gameManager.ReelData.Zg < -30000 && !_tutorialThrowStop && !_tutorialThrowStop)
                            {
                                BaitThrow();
                            }
                            yield return null;
                        }
                    }
                }
                else
                {
                    while (_progress <= 1f)
                    {                   
                        _progress += powerSpeed * Time.deltaTime;
                        _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                        _angleGyro = _gyroscope.rotationRate;
                        if (_gameManager.BaitThrowMode && _angleGyro.x < -9 && !_tutorialThrowStop && !_tutorialThrowStop)
                        {
                            BaitThrow();
                        }
                        yield return null;
                    }


                    while (_progress >= 0)
                    {
                        _progress -= powerSpeed * Time.deltaTime;
                        _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                        _angleGyro = _gyroscope.rotationRate;
                        if (_gameManager.BaitThrowMode && _angleGyro.x < -9 && !_tutorialThrowStop && !_tutorialThrowStop)
                        {
                            BaitThrow();
                        }
                        yield return null;
                    }
                }
            }
            #endregion
            // 에디터
            #region
            if(Application.platform.Equals(RuntimePlatform.WindowsEditor))
            {
                while (_progress <= 1f)
                {
                    _progress += powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                    if (Input.GetButtonDown("Jump") && _gameManager.BaitThrowMode && !_tutorialThrowStop)
                    {
                        BaitThrow();
                    }

                    yield return null;
                }
                while (_progress >= 0)
                {
                    _progress -= powerSpeed * Time.deltaTime;
                    _ingameUIManager.ResetPetGage(Mathf.Lerp(0, 1, _progress));

                    if (Input.GetButtonDown("Jump") && _gameManager.BaitThrowMode && !_tutorialThrowStop)
                    {
                        BaitThrow();
                    }

                    yield return null;
                }
            }
            #endregion
        }
    }

    public void ClickItemButton()
    {
        if (_gameManager == null)
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        
        if (_gameManager.CurrentState.Equals(GameManager.eIngameState.fighting) && _itemUIState.Equals(eItemUIState._off))
            return;
        
        if (_itemUIState.Equals(eItemUIState._on))
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

            _itemUIState = eItemUIState._off;

            _itemOptionAni.SetBool(isOnHash, false);
            _itemOptionButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _itemOptionButton.interactable = true));
        }
        else
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

            _itemUIState = eItemUIState._on;
            _itemOptionAni.SetBool(isOnHash, true);
            _itemOptionButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _itemOptionButton.interactable = true));
        }
    }
    
    public void CatchFish()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishRaise).GetComponent<AudioPoolObject>().Init();
    }
    
    public void IncreaseProbility(float leftX, float rightX, float upZ, float downZ)
    {
        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _jeongdongjinFishManager.UpdateRangeWhenThrowPastebait(leftX, rightX, upZ, downZ);
                break;
            case PublicDefined.eMapType.skyway:
                _skywayFishManager.UpdateRangeWhenThrowPastebait(leftX, rightX, upZ, downZ);
                break;
            case PublicDefined.eMapType.homerspit:
                _homerspitFishManager.UpdateRangeWhenThrowPastebait(leftX, rightX, upZ, downZ);
                break;
        }
        
        int currentIndex = _userData.GetCurrentEquipmentDictionary()["pastebait"];
        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        Dictionary<int, int> pastebaitDic = _userData.GetPasteBaitDictionary();
        
        pastebaitDic[currentIndex]--;

        if (pastebaitDic[currentIndex].Equals(0))
        {
            if(Equipment.INSTANCE != null)
            {
                Equipment.INSTANCE.SetCurrentPastebaitItem(null);
            }
            
            _userData.GetCurrentEquipmentDictionary()["pastebait"] = -1;
            updateDic.Add("/equipment/pastebait", -1);
        }

        updateDic.Add("/pastebait/" + currentIndex.ToString(), pastebaitDic[currentIndex]);

        DBManager.INSTANCE.UpdateFirebase(updateDic);
    }

    public Sprite GetJeongdongjinRareFishImage(int DBNumber)
    {
        switch (DBNumber)
        {
            case 0:
                return _blackporgySprite;
            case 5:
                return _seabassSprite;
            case 10:
                return _japaneseamberjackSprite;
            case 11:
                return _largescaleblackfishSprite;
            case 16:
                return _japanesespanishmackerelSprite;
            case 24:
                return _spottedseabassSprite;
            case 99:
                return _blackporgySprite;
            default:
                return _nullSprite;
        }
    }
    public Sprite GetSkywayRareFishImage(int DBNumber)
    {
        //Debug.Log(DBNumber);
        switch (DBNumber)
        {
            case 66:
                return _atlantictrpletail;
            case 69:
                return _blackgrouper;
            case 79:
                return _indopacificsailfish;
            case 86:
                return _redporgy;
            case 89:
                return _redtilefish;
            case 95:
                return _swordfish;
            default:
                return _nullSprite;
        }
    }
    public Sprite GetHomerspitRareFishImage(int DBNumber)
    {
        switch (DBNumber)
        {
            case 34:
                return _bigskate;
            case 38:
                return _chinooksalmon;
            case 43:
                return _halibut;
            case 47:
                return _lingcod;
            case 52:
                return _quillbackrockfish;
            case 57:
                return _salmonshark;
            case 61:
                return _yelloweyerockfish;
            case 63:
                return _yellowfintuna;
            default:
                return _nullSprite;
        }
    }
}