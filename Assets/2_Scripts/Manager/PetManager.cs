using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
//using Boo.Lang.Environments;

public class PetManager : MonoBehaviour
{
    readonly int _throwHash = Animator.StringToHash("Throw");
    public enum eItemUIState
    {
        _on,
        _off,
    }

    readonly int isOnHash = Animator.StringToHash("isOn");

    public bool _isPause;

    [Header("스크립트")]
    public FishObjectManager _jeongdongjinFishManager;
    public FishObjectManagerSkyway _skywayFishManager;
    public FishObjectManagerHomerspit _homerspitFishManager;
    public BaitSpatulaControl _baitspatulaControl;

    public Sprite _nullSprite;
    [Header("jeongdongjin 레어 물고기 이미지(레이더용)")]
    public Sprite _blackporgySprite;
    public Sprite _largescaleblackfishSprite;
    public Sprite _japaneseamberjackSprite;
    public Sprite _japanesespanishmackerelSprite;
    public Sprite _seabassSprite;
    public Sprite _spottedseabassSprite;

    [Header("skyway 레어 물고기 이미지(레이더용")]
    public Sprite _blackgrouper;
    public Sprite _atlantictrpletail;
    public Sprite _redporgy;
    public Sprite _redtilefish;
    public Sprite _indopacificsailfish;
    public Sprite _swordfish;

    [Header("homerspit 레어 물고기 이미지(레이더용)")]
    public Sprite _bigskate;
    public Sprite _yellowfintuna;
    public Sprite _lingcod;
    public Sprite _yelloweyerockfish;
    public Sprite _quillbackrockfish;
    public Sprite _salmonshark;
    public Sprite _halibut;
    public Sprite _chinooksalmon;

    // 미끼던지기, 플래시를 쏘는 애니메이터
    public Animator petAnimator;

    // 현재 낚시 상태를 가져오기 위한 클래스
    public GameManager gameMgr;

    // 게이지 참조하기 위한 클래스
    public InGameUIManager _ingameUI;

    // 물고기 스크립트 
    public FishControl fishControl;

    public CharacterManager characterMgr;

    // 램프 오브젝트    
    public GameObject landingNet;

    // 뜰채 오브젝트
    public GameObject baitSpatulaula;

    UserData _userData;

    // 램프, 떡밥 던지기 옵션
    [SerializeField] GameObject _baitBundle;

    // 아이템 옵션 
    public GameObject _itemOption;
    public Button _itemOptionButton;
    Animator _itemOptionAni;
    public Button _lampButton;

    // 라이트 켜졌을 때
    public bool isLightOn;

    // 물고기 잡았을 때
    public bool isCatch;

    // 파워 게이지 값
    [HideInInspector] public float progress;

    public GameObject _pastebaitIsNullObject;
    public Light fishingLamp;
    

    public ParticleSystem radarParticle;

    public Coroutine baitCor;

    // 레이더 관련 레어 물고기 위치 찾기
    private bool _radarInUse = false;
    public RectTransform[] _fishIcon;
    public RectTransform _canvasRect;

    // 회전 관련 (떡밥 던지기)
    Vector3 _angleGyro = Vector3.zero;
    Gyroscope _gyroscope;

    int _currentPasteBaitIndex = 0;
     
    [HideInInspector] public eItemUIState _itemUIState = eItemUIState._off;
    [HideInInspector] public bool _tutorialThrowStop;

    private void Awake()
    {
        _tutorialThrowStop = false;
        isLightOn = false;
    }
    private void Start()
    {
        _gyroscope = Input.gyro;
        _gyroscope.enabled = true;

        _userData = DBManager.INSTANCE.GetUserData();
        _itemOptionAni = _itemOption.GetComponent<Animator>();

        // 회전을 위해 스크립트가 보유한 트랜스폼 참조
        petAnimator = GetComponent<Animator>();

        baitCor = null;
    }
    // 레이더 이미지 클릭
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

        //SoundManager.instance.EffectPlay("Rader");
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

                    // 거리에 따른 크기 설정
                    _fishIcon[cnt].sizeDelta = new Vector2(temp.z + 1.5f, temp.z + 1.5f);

                    Vector3 fishPos = Camera.main.WorldToViewportPoint(temp);

                    Vector3 iconPos = Camera.main.ViewportToWorldPoint(fishPos);

                    _fishIcon[cnt].GetComponent<Image>().sprite = GetJeongdongjinRareFishImage(_jeongdongjinFishManager._RareFishList[i].GetComponent<Fish>().fishDBNum);
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

                    // 거리에 따른 크기 설정
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

                    // 거리에 따른 크기 설정
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

    // 떡밥 이미지 클릭
    public void ClickBaitThrow()
    {
        if (gameMgr._currentState.Equals(PublicDefined.IngameState.fighting) || _radarInUse || fishControl.isFind || gameMgr.IsFly)
            return;

        if (gameMgr.BaitThrowMode)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

            // 관련 코루틴 정지
            _baitspatulaControl.StopRotateCoroutine();
            StopCoroutine(baitCor);

            // 떡밥 주걱 off
            baitSpatulaula.SetActive(false);

            gameMgr.BaitThrowMode = false;

            _ingameUI._Reeling.enabled = true;

            // 게이지 바꾸고 초기화
            _ingameUI.SetPetGage(false);
            _ingameUI.ResetCharacterGage();
            gameMgr.Progress = 0;
            return;
        }

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();


        // 떡밥이 없다면 없다고 알려주고 리턴
        if (DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"].Equals(-1))
        {
            _pastebaitIsNullObject.SetActive(true);
            StartCoroutine(MakeDelay(2, () => _pastebaitIsNullObject.SetActive(false)));
            return;
        }
        else
        {
            _ingameUI._Reeling.enabled = false;
            // bool형 변수 변경(사람 캐릭터가 못던지게 하기 위함)
            gameMgr.BaitThrowMode = true;

            // 떡밥 주걱 회전 코루틴 시작
            _baitspatulaControl.StartRotateCoroutine();

            // 떡밥 던지기위한 코루틴 함수 실행
            baitCor = StartCoroutine(BaitDirection());
            
            // 떡밥 주걱 on
            baitSpatulaula.SetActive(true);

            // 파워게이지 변경(펫 파워 활성, 사람 파워 비활성)
            _ingameUI.ResetPetGage();
            _ingameUI.SetCharacterGage(false);
            progress = 0;
        }
        //Debug.LogError(gameMgr.BaitThrowMode);
    }

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
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
        //Debug.LogError(1);

        _currentPasteBaitIndex = _userData.GetCurrentEquipmentDictionary()["pastebait"];

        // 게이지 바꾸고 초기화
        _ingameUI.ResetCharacterGage();
        _ingameUI.SetPetGage(false);

        gameMgr.Progress = 0;
        petAnimator.SetBool(_throwHash, true);

        if(!DataManager.INSTANCE._tutorialIsInProgress)
            _ingameUI._Reeling.enabled = true;

        _baitspatulaControl.StopRotateCoroutine();
        StopCoroutine(baitCor);
        
        StartCoroutine(MakeDelay(3, () =>
        {
            gameMgr.BaitThrowMode = false;
            baitSpatulaula.SetActive(false);
            petAnimator.SetBool(_throwHash, false);
        }));
    }

    // 애니메이션 이벤트로 있는 함수
    public void BaitThrowOn()
    {
        if (DataManager.INSTANCE._vibration)
            Vibration.Vibrate(2200);

        _baitBundle.SetActive(true);
    }

    // 램프 버튼 클릭
    public void OnLamp()
    {
        _lampButton.interactable = false;

        StartCoroutine(MakeDelay(1, () => _lampButton.interactable = true));

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.lamp).GetComponent<AudioPoolObject>().Init();
        Light();

        // 뜰채 Off
        landingNet.SetActive(false);
    }

    // 불빛이 켜진다.
    void Light()
    {
        isLightOn = !isLightOn;
        fishingLamp.enabled = isLightOn;

        // 라이트가 온/오프일 때에 따른 물고기들 서칭 범위 변경
        if (isLightOn)
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

    // 캐스팅 전
    public IEnumerator BaitDirection() 
    {
        float powerSpeed = 0.5f;

        while (gameMgr.BaitThrowMode && !_isPause)
        {
            // 안드로이드
            #region 
            if (Application.platform.Equals(RuntimePlatform.Android))
            {
                if (gameMgr._isConnectedToBluettooth_Reel)
                {
                    // 신형 릴
                    {
                        while (progress <= 1f) // 게이지가 1.0보다 작을때 반복
                        {                    // 파워 게이지
                            progress += powerSpeed * Time.deltaTime;
                            _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                            _angleGyro = _gyroscope.rotationRate;
                            if (gameMgr.BaitThrowMode && gameMgr._reelData.Zg < -30000 && !_tutorialThrowStop && !_tutorialThrowStop)
                            {
                                BaitThrow();
                            }
                            yield return null;
                        }


                        while (progress >= 0)
                        {
                            progress -= powerSpeed * Time.deltaTime;
                            _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                            _angleGyro = _gyroscope.rotationRate;
                            if (gameMgr.BaitThrowMode && gameMgr._reelData.Zg < -30000 && !_tutorialThrowStop && !_tutorialThrowStop)
                            {
                                BaitThrow();
                            }
                            yield return null;
                        }
                    }
                }
                else
                {
                    while (progress <= 1f) // 게이지가 1.0보다 작을때 반복
                    {                    // 파워 게이지
                        progress += powerSpeed * Time.deltaTime;
                        _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                        _angleGyro = _gyroscope.rotationRate;
                        if (gameMgr.BaitThrowMode && _angleGyro.x < -9 && !_tutorialThrowStop && !_tutorialThrowStop)
                        {
                            BaitThrow();
                        }
                        yield return null;
                    }


                    while (progress >= 0)
                    {
                        progress -= powerSpeed * Time.deltaTime;
                        _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                        _angleGyro = _gyroscope.rotationRate;
                        if (gameMgr.BaitThrowMode && _angleGyro.x < -9 && !_tutorialThrowStop && !_tutorialThrowStop)
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
                while (progress <= 1f) // 게이지가 1.0보다 작을때 반복
                {
                    // 파워 게이지
                    progress += powerSpeed * Time.deltaTime;
                    _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                    if (Input.GetButtonDown("Jump") && gameMgr.BaitThrowMode && !_tutorialThrowStop)
                    {
                        BaitThrow();
                    }

                    yield return null;
                }
                while (progress >= 0)
                {
                    progress -= powerSpeed * Time.deltaTime;
                    _ingameUI.ResetPetGage(Mathf.Lerp(0, 1, progress));

                    if (Input.GetButtonDown("Jump") && gameMgr.BaitThrowMode && !_tutorialThrowStop)
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
        if (gameMgr._currentState.Equals(PublicDefined.IngameState.fighting) && _itemUIState.Equals(eItemUIState._off))
            return;

        // On -> Off
        if (_itemUIState.Equals(eItemUIState._on))
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

            _itemUIState = eItemUIState._off;

            _itemOptionAni.SetBool(isOnHash, false);
            _itemOptionButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _itemOptionButton.interactable = true));
        }
        // Off -> On
        else
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

            _itemUIState = eItemUIState._on;
            _itemOptionAni.SetBool(isOnHash, true);
            _itemOptionButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _itemOptionButton.interactable = true));
        }
    }

    // 낚시를 성공해서 뜰채를 들어올린다.
    public void CatchFish()
    {
        //landingNet.SetActive(true);
        //fishControl.fishTr.localScale = new Vector3(fishControl.fishData.lenth * 0.5f, fishControl.fishData.lenth * 0.5f, fishControl.fishData.lenth * 0.5f);
        //fishControl.fishTr.parent = landingNet.transform.GetChild(0);
        //fishControl.fishTr.localPosition = Vector3.zero;
        //fishControl.fishTr.localRotation = Quaternion.Euler(0f, -90f, 0f);
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishRaise).GetComponent<AudioPoolObject>().Init();
        //fishControl.fishTr.GetChild(0).GetChild(0).localPosition = Vector3.zero;

        //StartCoroutine(MakeDelay(4, () => landingNet.SetActive(false)));
    }

    // 떡밥에 의한 확률 증가
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
                Equipment.INSTANCE._currentPastebaitItem = null;
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