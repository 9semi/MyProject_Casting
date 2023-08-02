using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 캐릭터 선택 씬에서 선택해서 게임씬으로 넘겨주는 클래스
public class DataManager : MonoBehaviour
{ 
    static DataManager _uniqueInstance;
    static public DataManager INSTANCE
    {
        get { return _uniqueInstance; }
    }

    public int _worldTime { set; get; }

    public float _sinkerWeight { set; get; }
    public int _depthLength { set; get; }
    public float _lureWeight { set; get; }
    //public float _rodIntensive { set; get; }
    //public float _reelIntensive { set; get; }

    public bool _isMatch = false;

    public PublicDefined.eMapType _mapType;
    public bool _tutorialIsInProgress ;
    public bool _matchGameIsInProgress;
    public int _matchConditionNumber;
    public bool _isTryLogout = false;
    public bool _isFirstLogin = true;
    public bool _isADBlock = false;


    List<Fish> _prevFishObject_Jeongdongjin = new List<Fish>();
    List<FishSkyway> _prevFishObject_Skyway = new List<FishSkyway>();
    List<FishHomerspit> _prevFishObject_Homerspit = new List<FishHomerspit>();

    GameManager _gameManager = null;
    ObjectManager _objectManager = null;
    FishObjectManager _fishObjectManager;
    FishObjectManagerSkyway _fishObjectManagerSkyway;
    FishObjectManagerHomerspit _fishObjectManagerHomerspit;
    UserData _userData;

    // tutorial 관련
    [HideInInspector] public int _currentBaitNumber_forTutorial;
    [HideInInspector] public int _currentPastebaitNumber_forTutorial;

    // 옵션 관련
    [HideInInspector] public bool _vibration = true;
    [HideInInspector] public bool _landscape;
    [HideInInspector] public bool _tutorialIsDone;

    // 업데이트 관련
    [HideInInspector] public bool _updateCheck = false;

    // 로그인 제거 버전
    public bool _isNoneLoginVersion = true;

    private void Awake()
    {
        _tutorialIsInProgress = false;
        _matchGameIsInProgress = false;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            switch(SceneManager.GetActiveScene().buildIndex)
            {
                case 5:
                case 10:
                    _mapType = PublicDefined.eMapType.jeongdongjin;
                    break;
                case 6:
                case 11:
                    _mapType = PublicDefined.eMapType.skyway;
                    break;
                case 7:
                case 12:
                    _mapType = PublicDefined.eMapType.homerspit;
                    break;
                case 9:
                    _mapType = PublicDefined.eMapType.tutorial;
                    break;
                default:
                    _mapType = PublicDefined.eMapType.lobby;
                    break;
            }
        }
        else
        {
            _mapType = PublicDefined.eMapType.lobby;
        }

        //_mapType = PublicDefined.eMapType.jeongdongjin;

        _worldTime = 12;

        _uniqueInstance = this;
        _sinkerWeight = 0;
        _lureWeight = 0;
    }
    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _userData = DBManager.INSTANCE.GetUserData();
    }

    public void SetUserData(UserData instance)
    {
        _userData = instance;
        //CheckCurrentEquipment();
    }
    public void SetFishObjectManagerInstance(FishObjectManager instance)
    {
        _fishObjectManager = instance;
    }
    public void SetFishObjectManagerInstance(FishObjectManagerSkyway instance)
    {
        _fishObjectManagerSkyway = instance;
    }
    public void SetFishObjectManagerInstance(FishObjectManagerHomerspit instance)
    {
        _fishObjectManagerHomerspit = instance;
    }
    public void CheckBaitProbability()
    {
        switch(_mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                CheckBaitProbability_Jeongdongjin();
                break;
            case PublicDefined.eMapType.skyway:
                CheckBaitProbability_Skyway();
                break;
            case PublicDefined.eMapType.homerspit:
                CheckBaitProbability_Homerspit();
                break;
        }
    }
    void CheckBaitProbability_Jeongdongjin()
    {
        //Debug.Log(_prevFishObject_Jeongdongjin.Count);
        if (_prevFishObject_Jeongdongjin.Count > 0)
        {
            for (int i = 0; i < _prevFishObject_Jeongdongjin.Count; i++)
            {
                _prevFishObject_Jeongdongjin[i].BiteBait = _prevFishObject_Jeongdongjin[i].GetBackBiteBait();
                //Debug.Log(_prevFishObject_Jeongdongjin[i].backBiteBait);
            }

            _prevFishObject_Jeongdongjin.Clear();
        }

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if (_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
            return;


        //switch (_userData.GetCurrentEquipmentDictionary()["bait"])
        //{
        //    case 2:
        //        PlusBaitProbability(_fishObjectManager.commonoctopus, 5);
        //        PlusBaitProbability(_fishObjectManager.blackporgy, 4);
        //        break;
        //    case 3:
        //        PlusBaitProbability(_fishObjectManager.whitespottedconger, 5);
        //        PlusBaitProbability(_fishObjectManager.bigfinsquid, 5);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 4);
        //        break;
        //    case 4:
        //        PlusBaitProbability(_fishObjectManager.pacificherring, 5);
        //        PlusBaitProbability(_fishObjectManager.konosiruspunctatus, 10);
        //        PlusBaitProbability(_fishObjectManager.flatheadgreymullet, 5);
        //        PlusBaitProbability(_fishObjectManager.halfbeak, 10);
        //        //PlusBaitProbability(_fishObjectManager.whitespottedconger, 10);
        //        break;
        //    case 6:
        //        PlusBaitProbability(_fishObjectManager.seabass, 9);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 9);
        //        PlusBaitProbability(_fishObjectManager.commonoctopus, 5);
        //        break;
        //    case 10:
        //        PlusBaitProbability(_fishObjectManager.commonoctopus, 5);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);
        //        break;
        //    case 15:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);
        //        PlusBaitProbability(_fishObjectManager.horsemackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 10);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 4);
        //        PlusBaitProbability(_fishObjectManager.japanesespanishmackerel, 4);
        //        break;
        //    case 20:
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 5);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 5);
        //        break;
        //    case 21:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 5);
        //        PlusBaitProbability(_fishObjectManager.spottybellygreenling, 5);
        //        PlusBaitProbability(_fishObjectManager.sandsmelt, 5);
        //        PlusBaitProbability(_fishObjectManager.indianflathead, 5);
        //        PlusBaitProbability(_fishObjectManager.goldeyerockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.bluefingurnard, 10);
        //        PlusBaitProbability(_fishObjectManager.sandfish, 10);
        //        PlusBaitProbability(_fishObjectManager.blackporgy, 4);
        //        break;
        //    case 23:
        //        PlusBaitProbability(_fishObjectManager.bigfinsquid, 5);
        //        break;
        //    case 27:
        //        PlusBaitProbability(_fishObjectManager.japanesespanishmackerel, 9);
        //        break;
        //    case 35:
        //        PlusBaitProbability(_fishObjectManager.commonoctopus, 5);
        //        PlusBaitProbability(_fishObjectManager.blackporgy, 4);
        //        break;
        //    case 45:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.spottybellygreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.surfperch, 10);
        //        PlusBaitProbability(_fishObjectManager.blackporgy, 9);
        //        break;
        //    case 46:
        //        PlusBaitProbability(_fishObjectManager.finespottedflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.brownsole, 10);
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.spottybellygreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.surfperch, 10);
        //        PlusBaitProbability(_fishObjectManager.spotbellyrockfish, 10);

        //        PlusBaitProbability(_fishObjectManager.sandsmelt, 10);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 10);
        //        PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);

        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 5);
        //        PlusBaitProbability(_fishObjectManager.goldeyerockfish, 5);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 5);
        //        PlusBaitProbability(_fishObjectManager.halfbeak, 5);

        //        PlusBaitProbability(_fishObjectManager.seabass, 2);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 2);
        //        break;
        //    case 48:
        //        PlusBaitProbability(_fishObjectManager.finespottedflounder, 5);
        //        PlusBaitProbability(_fishObjectManager.brownsole, 5);
        //        PlusBaitProbability(_fishObjectManager.surfperch, 5);
        //        PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 5);
        //        PlusBaitProbability(_fishObjectManager.spotbellyrockfish, 5);
        //        PlusBaitProbability(_fishObjectManager.sandfish, 5);
        //        PlusBaitProbability(_fishObjectManager.konosiruspunctatus, 5);
        //        PlusBaitProbability(_fishObjectManager.halfbeak, 5);
        //        PlusBaitProbability(_fishObjectManager.okhotskatkamackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 5);
        //        PlusBaitProbability(_fishObjectManager.pacificherring, 10);
        //        PlusBaitProbability(_fishObjectManager.blackporgy, 2);
        //        PlusBaitProbability(_fishObjectManager.largescaleblackfish, 2);
        //        PlusBaitProbability(_fishObjectManager.horsemackerel, 10);
        //        PlusBaitProbability(_fishObjectManager.mackerel, 10);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 2);
        //        PlusBaitProbability(_fishObjectManager.flatheadgreymullet, 10);
        //        break;
        //    case 51:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 5);
        //        PlusBaitProbability(_fishObjectManager.spottybellygreenling, 5);
        //        PlusBaitProbability(_fishObjectManager.sandsmelt, 5);
        //        PlusBaitProbability(_fishObjectManager.indianflathead, 5);
        //        PlusBaitProbability(_fishObjectManager.bluefingurnard, 10);
        //        PlusBaitProbability(_fishObjectManager.sandfish, 10);
        //        PlusBaitProbability(_fishObjectManager.largescaleblackfish, 9);
        //        break;
        //    case 56:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.spottybellygreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.surfperch, 10);
        //        PlusBaitProbability(_fishObjectManager.largescaleblackfish, 4);
        //        PlusBaitProbability(_fishObjectManager.okhotskatkamackerel, 10);
        //        break;
        //    case 40:
        //        PlusBaitProbability(_fishObjectManager.bigfinsquid, 10);
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 4);
        //        PlusBaitProbability(_fishObjectManager.whitespottedconger, 5);
        //        break;
        //    case 24:
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.indianflathead, 10);
        //        break;
        //    case 39:
        //        PlusBaitProbability(_fishObjectManager.finespottedflounder, 5);
        //        PlusBaitProbability(_fishObjectManager.brownsole, 5);
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 5);
        //        PlusBaitProbability(_fishObjectManager.whitespottedconger, 10);
        //        break;
        //    case 55:
        //        PlusBaitProbability(_fishObjectManager.bluefingurnard, 5);
        //        break;
        //    case 36:
        //        PlusBaitProbability(_fishObjectManager.bigfinsquid, 10);
        //        PlusBaitProbability(_fishObjectManager.commonoctopus, 10);
        //        break;
        //    case 54:
        //        //PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 4);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;
        //    case 52:
        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;
        //    case 53:
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);
        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 9);
        //        break;
        //    case 34:
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);
        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 10);

        //        PlusBaitProbability(_fishObjectManager.japaneseamberjack, 4);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;
        //    case 33:
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 10);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;
        //    case 32:
        //        PlusBaitProbability(_fishObjectManager.fatgreenling, 10);
        //        PlusBaitProbability(_fishObjectManager.babyseabass, 10);

        //        PlusBaitProbability(_fishObjectManager.darkbandedrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);

        //        PlusBaitProbability(_fishObjectManager.mackerel, 5);
        //        PlusBaitProbability(_fishObjectManager.bigscaledredfin, 10);
        //        PlusBaitProbability(_fishObjectManager.japanesespanishmackerel, 4);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;
        //    case 22:
        //        PlusBaitProbability(_fishObjectManager.oliveflounder, 10);
        //        PlusBaitProbability(_fishObjectManager.koreanrockfish, 10);
        //        PlusBaitProbability(_fishObjectManager.seabass, 4);
        //        PlusBaitProbability(_fishObjectManager.spottedseabass, 4);
        //        break;


        //}
    }
    void CheckBaitProbability_Skyway()
    {
        if (_prevFishObject_Skyway.Count > 0)
        {
            for (int i = 0; i < _prevFishObject_Skyway.Count; i++)
            {
                _prevFishObject_Skyway[i].biteBait = _prevFishObject_Skyway[i].backBiteBait;
            }

            _prevFishObject_Skyway.Clear();
        }


        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if (_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
            return;

        switch (_userData.GetCurrentEquipmentDictionary()["bait"])
        {
            case 20:
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticwreckfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.japaneseamberjack, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.hogfish, 10);
                break;
            case 48:
                PlusBaitProbability(_fishObjectManagerSkyway.floridapompano, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.lagoontriggerfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.redlionfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.redsnapper, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.mahimahi, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.blackgrouper, 2);
                PlusBaitProbability(_fishObjectManagerSkyway.atlantictripletail, 2);
                PlusBaitProbability(_fishObjectManagerSkyway.commonsnook, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.mackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.gnomefish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.soinymullet, 10);
                break;
            case 10:
                PlusBaitProbability(_fishObjectManagerSkyway.atlantictripletail, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.cobia, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.blackdrum, 10);
                break;
            case 1:
                PlusBaitProbability(_fishObjectManagerSkyway.kingmackerel, 10);
                break;
            case 16:
                PlusBaitProbability(_fishObjectManagerSkyway.kingmackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.gnomefish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.yellowtailamberjack, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.seabass, 5);
                break;
            case 21:
                PlusBaitProbability(_fishObjectManagerSkyway.gulfflounder, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.cobia, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.weakfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.bonefish, 10);
                break;
            case 45:
                PlusBaitProbability(_fishObjectManagerSkyway.soinymullet, 5);
                break;
            case 46:
                PlusBaitProbability(_fishObjectManagerSkyway.bonefish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.stripedbass, 5);
                break;
            case 13:
                PlusBaitProbability(_fishObjectManagerSkyway.sheepshead, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.blackporgy, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.redporgy, 2);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticcod, 5);
                break;
            case 25:
                PlusBaitProbability(_fishObjectManagerSkyway.redstingray, 5);
                break;
            case 47:
                PlusBaitProbability(_fishObjectManagerSkyway.wahoo, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.halibut, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.stripedbass, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.indopacificsailfish, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.seabass, 10);
                break;
            case 42:
                PlusBaitProbability(_fishObjectManagerSkyway.stripedbass, 10);
                break;
            case 31:
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.indopacificsailfish, 9);
                break;
            case 57:
                PlusBaitProbability(_fishObjectManagerSkyway.hogfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.floridapompano, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.blackporgy, 10);
                break;
            case 5:
                PlusBaitProbability(_fishObjectManagerSkyway.blackporgy, 10);
                break;
            case 12:
                PlusBaitProbability(_fishObjectManagerSkyway.floridapompano, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.sheepshead, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.blackporgy, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.redporgy, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.weakfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.soinymullet, 5);
                break;
            case 43:
                PlusBaitProbability(_fishObjectManagerSkyway.wahoo, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.redstingray, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.indopacificsailfish, 2);
                break;
            case 14:
                PlusBaitProbability(_fishObjectManagerSkyway.blackgrouper, 4);
                break;
            case 29:
                PlusBaitProbability(_fishObjectManagerSkyway.blackgrouper, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.redporgy, 4);
                break;
            case 24:
                PlusBaitProbability(_fishObjectManagerSkyway.redlionfish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.gulfflounder, 10);
                break;
            case 8:
                PlusBaitProbability(_fishObjectManagerSkyway.hogfish, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.commonsnook, 10);
                break;
            case 39:
                PlusBaitProbability(_fishObjectManagerSkyway.kingmackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.swordfish, 2);
                PlusBaitProbability(_fishObjectManagerSkyway.lagoontriggerfish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.redsnapper, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticwreckfish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.japaneseamberjack, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mahimahi, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.yellowtailamberjack, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.redporgy, 9);
                PlusBaitProbability(_fishObjectManagerSkyway.redtilefish, 9);
                break;
            case 55:
                PlusBaitProbability(_fishObjectManagerSkyway.indopacificsailfish, 9);
                PlusBaitProbability(_fishObjectManagerSkyway.swordfish, 9);
                break;
            case 54:
                PlusBaitProbability(_fishObjectManagerSkyway.wahoo, 10);
                break;
            case 15:
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticwreckfish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.japaneseamberjack, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.halibut, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticcod, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.stripedbass, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.seabass, 10);
                break;
            case 34:
                PlusBaitProbability(_fishObjectManagerSkyway.redsnapper, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mahimahi, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.blackgrouper, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.kingmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.seatrout, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.yellowtailamberjack, 10);
                break;
            case 33:
                PlusBaitProbability(_fishObjectManagerSkyway.seatrout, 10);
                break;
            case 32:
                PlusBaitProbability(_fishObjectManagerSkyway.japaneseamberjack, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mahimahi, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.kingmackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.seatrout, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.mackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticcod, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.seabass, 5);
                break;
            case 22:
                PlusBaitProbability(_fishObjectManagerSkyway.gulfflounder, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.wahoo, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.halibut, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.atlantictripletail, 9);
                PlusBaitProbability(_fishObjectManagerSkyway.stripedbass, 5);
                break;
            case 49:
                PlusBaitProbability(_fishObjectManagerSkyway.redtilefish, 4);
                PlusBaitProbability(_fishObjectManagerSkyway.swordfish, 4);
                break;
            case 11:
                PlusBaitProbability(_fishObjectManagerSkyway.blackgrouper, 9);
                break;
            case 44:
                PlusBaitProbability(_fishObjectManagerSkyway.sheepshead, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticcod, 10);
                break;
            case 9:
                PlusBaitProbability(_fishObjectManagerSkyway.halibut, 10);
                break;
            case 3:
                PlusBaitProbability(_fishObjectManagerSkyway.redsnapper, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.redtilefish, 2);
                PlusBaitProbability(_fishObjectManagerSkyway.seabass, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.yellowtailamberjack, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.indopacificsailfish, 2);
                break;
            case 28:
                //PlusBaitProbability(_fishObjectManagerSkyway.sheepshead, 30);
                PlusBaitProbability(_fishObjectManagerSkyway.atlantictripletail, 9);
                PlusBaitProbability(_fishObjectManagerSkyway.seatrout, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.weakfish, 10);
                PlusBaitProbability(_fishObjectManagerSkyway.atlanticspanishmackerel, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.blackdrum, 5);
                PlusBaitProbability(_fishObjectManagerSkyway.sheepshead, 10);
                break;
            case 50:
                PlusBaitProbability(_fishObjectManagerSkyway.redtilefish, 9);
                break;
        }
    }
    void CheckBaitProbability_Homerspit()
    {
        if (_prevFishObject_Homerspit.Count > 0)
        {
            for (int i = 0; i < _prevFishObject_Homerspit.Count; i++)
            {
                _prevFishObject_Homerspit[i].biteBait = _prevFishObject_Homerspit[i].backBiteBait;
                //Debug.Log(_prevFishObject_Homerspit[i].name);
            }

            _prevFishObject_Homerspit.Clear();
        }

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if (_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
            return;

        switch (_userData.GetCurrentEquipmentDictionary()["bait"])
        {
            case 2:
                PlusBaitProbability(_fishObjectManagerHomerspit.lavenderjobfish, 10);
                break;
            case 4:
                PlusBaitProbability(_fishObjectManagerHomerspit.salmonsnailfish, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificsaury, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.spotlinedsardine, 5);
                break;
            case 6:
                PlusBaitProbability(_fishObjectManagerHomerspit.quillbackrockfish, 9);
                break;
            case 48:
                PlusBaitProbability(_fishObjectManagerHomerspit.broadbandedthornyhead, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.salmonsnailfish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificsaury, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.alaskapollack, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfinsole, 5);
                break;
            case 10:
                PlusBaitProbability(_fishObjectManagerHomerspit.bigskate, 2);
                break;
            case 16:
                PlusBaitProbability(_fishObjectManagerHomerspit.lingcod, 2);
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificcod, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.bluefingurnard, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.quillbackrockfish, 2);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 2);
                break;
            case 21:
                PlusBaitProbability(_fishObjectManagerHomerspit.blackfinflounder, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.rocksole, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.dragonpoacher, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.spotlinedsardine, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfinsole, 10);

                PlusBaitProbability(_fishObjectManagerHomerspit.flatheadsole, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.beringwolffish, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.lavenderjobfish, 5);
                break;
            case 45:
                PlusBaitProbability(_fishObjectManagerHomerspit.bigskate, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.lingcod, 9);
                PlusBaitProbability(_fishObjectManagerHomerspit.yelloweyerockfish, 9);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 4);
                break;
            case 46:
                PlusBaitProbability(_fishObjectManagerHomerspit.blackfinflounder, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.rocksole, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.broadbandedthornyhead, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.sailfinpoacher, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.manybandedsole, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.kamchatkaflounder, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.lavenderjobfish, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.splendidalfonsino, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.spotlinedsardine, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfinsole, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.bluefingurnard, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.japanesepufferfish, 5);
                break;
            case 13:
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificsaury, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.sablefish, 5);
                break;
            case 38:
                PlusBaitProbability(_fishObjectManagerHomerspit.bluefingurnard, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificcod, 10);
                break;
            case 25:
                PlusBaitProbability(_fishObjectManagerHomerspit.bigskate, 9);
                PlusBaitProbability(_fishObjectManagerHomerspit.salmonshark, 4);
                break;
            case 0:
                PlusBaitProbability(_fishObjectManagerHomerspit.beringwolffish, 10);
                break;
            case 47:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 2);
                PlusBaitProbability(_fishObjectManagerHomerspit.lingcod, 2);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 4);
                break;
            case 40:
                PlusBaitProbability(_fishObjectManagerHomerspit.redtippedgrouper, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.flatheadsole, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.bigskate, 2);
                PlusBaitProbability(_fishObjectManagerHomerspit.alaskapollack, 10);
                break;
            case 31:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 4);
                break;
            case 57:
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificoceanperch, 10);
                break;
            case 12:
                PlusBaitProbability(_fishObjectManagerHomerspit.manybandedsole, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfinsole, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.dragonpoacher, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.redtippedgrouper, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.kamchatkaflounder, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificoceanperch, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.gnomefish, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.japanesepufferfish, 5);
                break;
            case 43:
                PlusBaitProbability(_fishObjectManagerHomerspit.quillbackrockfish, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 2);
                break;
            case 29:
                PlusBaitProbability(_fishObjectManagerHomerspit.beringwolffish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.yelloweyerockfish, 2);
                break;
            case 8:
                PlusBaitProbability(_fishObjectManagerHomerspit.sailfinpoacher, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.gnomefish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.chumsalmon, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.splendidalfonsino, 10);
                break;
            case 39:
                PlusBaitProbability(_fishObjectManagerHomerspit.kamchatkaflounder, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.lavenderjobfish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.quillbackrockfish, 2);
                break;
            case 30:
                PlusBaitProbability(_fishObjectManagerHomerspit.yelloweyerockfish, 2);
                break;
            case 15:
                PlusBaitProbability(_fishObjectManagerHomerspit.lingcod, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.bluefingurnard, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.quillbackrockfish, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.yelloweyerockfish, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.halibut, 9);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 2);
                break;
            case 53:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 9);
                //PlusBaitProbability(_fishObjectManagerHomerspit.japanesepufferfish, 10);
                break;
            case 34:
                PlusBaitProbability(_fishObjectManagerHomerspit.redtippedgrouper, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.flatheadsole, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.alaskapollack, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.sablefish, 10);
                break;
            case 33:
                PlusBaitProbability(_fishObjectManagerHomerspit.alaskapollack, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.sablefish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.chumsalmon, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 9);
                break;
            case 32:
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificcod, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.chumsalmon, 5);
                PlusBaitProbability(_fishObjectManagerHomerspit.chinooksalmon, 9);
                break;
            case 22:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 4);
                PlusBaitProbability(_fishObjectManagerHomerspit.sablefish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.halibut, 2);
                break;
            case 18:
                PlusBaitProbability(_fishObjectManagerHomerspit.pacificoceanperch, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.chumsalmon, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.japanesepufferfish, 10);
                break;
            case 7:
                PlusBaitProbability(_fishObjectManagerHomerspit.salmonshark, 2);
                break;
            case 17:
                PlusBaitProbability(_fishObjectManagerHomerspit.kamchatkaflounder, 10);
                break;
            case 26:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 2);
                break;
            case 37:
                PlusBaitProbability(_fishObjectManagerHomerspit.halibut, 4);
                break;
            case 9:
                PlusBaitProbability(_fishObjectManagerHomerspit.salmonshark, 9);
                PlusBaitProbability(_fishObjectManagerHomerspit.halibut, 4);
                break;
            case 19:
                PlusBaitProbability(_fishObjectManagerHomerspit.sablefish, 10);
                PlusBaitProbability(_fishObjectManagerHomerspit.halibut, 2);
                break;
            case 41:
                PlusBaitProbability(_fishObjectManagerHomerspit.yelloweyerockfish, 4);
                break;
            case 3:
                PlusBaitProbability(_fishObjectManagerHomerspit.lingcod, 4);
                break;
            case 54:
                PlusBaitProbability(_fishObjectManagerHomerspit.yellowfintuna, 4);
                break;

        }
    }
    void PlusBaitProbability(GameObject[] fishObject, float probability)
    {
        switch(_mapType)
        {
            case PublicDefined.eMapType.lobby:
                return;
            case PublicDefined.eMapType.jeongdongjin:
                for (int i = 0; i < fishObject.Length; i++)
                {
                    if(fishObject[i].activeSelf)
                    {
                        Fish fish = fishObject[i].GetComponent<Fish>();
                        fish.BiteBait += probability;
                        _prevFishObject_Jeongdongjin.Add(fish);
                        //Debug.Log(probability + " , " + probability / 100);
                        //Debug.LogError("[" + fish.fishKoreanName + "]" + fish.BiteBait);
                    }
                }
                break;
            case PublicDefined.eMapType.skyway:
                for(int i = 0; i < fishObject.Length; i++)
                {
                    if (fishObject[i].activeSelf)
                    {
                        FishSkyway fish = fishObject[i].GetComponent<FishSkyway>();
                        fish.BiteBait += probability;
                        _prevFishObject_Skyway.Add(fish);
                        //Debug.LogError("[" + fish.fishKoreanName + "]" + fish.BiteBait);
                    }
                }
                break;
            case PublicDefined.eMapType.homerspit:
                for (int i = 0; i < fishObject.Length; i++)
                {
                    if (fishObject[i].activeSelf)
                    {
                        FishHomerspit fish = fishObject[i].GetComponent<FishHomerspit>();
                        fish.BiteBait += probability;
                        _prevFishObject_Homerspit.Add(fish);
                        //Debug.LogError("[" + fish.fishKoreanName + "]" + fish.BiteBait);
                    }
                }
                break;
        }
    }

    //void CheckCurrentEquipment()
    //{
    //    if (_userData == null)
    //        _userData = DBManager.INSTANCE.GetUserData();

    //    Dictionary<string, int> dic = _userData.GetCurrentEquipmentDictionary();

    //    if (!dic["rod"].Equals(-1))
    //    {
    //        _rodIntensive = ItemData.Instance.rodItemDB[dic["rod"]].intensive;
    //    }
    //    if (!dic["reel"].Equals(-1))
    //    {
    //        _reelIntensive = ItemData.Instance.reelItemDB[dic["reel"]].intensive;
    //    }
    //    if (!dic["sinker"].Equals(-1))
    //    {
    //        _sinkerWeight = ItemData.Instance.sinkerItemDB[dic["sinker"]]._sinkerWeight;
    //    }

    //    _depthLength = dic["depthlength"];
    //}

    public void ResetAllInstance()
    {
        // 게임이 종료될 때 호출해줘서 다음 게임에서도 객체를 잘 가져오게 리셋해두자.
        _gameManager = null;
        _objectManager = null;
    }

    public void UpdateEquipmentRod(Item i)
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }
            //_rodIntensive = i.intensive;
            _gameManager.RodInfoChange(i.intensive, i.rodMaterial);
        }
    }

    public void UpdateEquipmentReel(Item i)
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }

            //_reelIntensive = i.intensive;
            _gameManager.ReelInfoChange(i.intensive, i.serialNumber - 6000);
        }
    }

    public void UpdateEquipmentBaitOn(int baitSerialNumber, int prevBaitSerialNumber = -1)
    {
        //Debug.Log(_mapType);

        if (_mapType != PublicDefined.eMapType.lobby)
        {
            _currentBaitNumber_forTutorial = baitSerialNumber;
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }

            // 해당 미끼 켜기                                 needle이 0번째에 있어서 +1 해줘야 한다.
            _gameManager.ReelPoint3.GetChild(0).GetChild(baitSerialNumber + 1).gameObject.SetActive(true);

            // 바늘 오브젝트 끄기
            _gameManager.ReelPoint3.GetChild(0).GetChild(0).gameObject.SetActive(false);

            // 만약 전에 끼던 미끼가 있다면 그것은 꺼야한다.
            if (!prevBaitSerialNumber.Equals(-1))
            {
                GameObject prev = _gameManager.ReelPoint3.GetChild(0).GetChild(prevBaitSerialNumber + 1).gameObject;

                if (prev.activeSelf)
                    prev.SetActive(false);
            }
        }

        switch (baitSerialNumber)
        {
            // 메탈지그
            case 15:
                _lureWeight = 200;
                break; 
            // 바이브레이션
            case 22:
                _lureWeight = 160;
                break;
            // 스푼
            case 32:
                _lureWeight = 180;
                break;
            // 스핀테일지그
            case 33:
                _lureWeight = 140;
                break;
            // 싱킹미노우
            case 34:
                _lureWeight = 120;
                break;
            // 에기
            case 36:
                _lureWeight = 200;
                break;
            // 타이라바
            case 50:
                _lureWeight = 160;
                break;
            // 펜슬베이트
            case 52:
                _lureWeight = 0;
                break;
            // 폽퍼
            case 53:
                _lureWeight = -1;
                break;
            // 플로팅미노우
            case 54:
                _lureWeight = -1;
                break;
            default:
                _lureWeight = 0;
                break;
        }
    }
    public void UpdateEquipmentBaitOff(int baitSerialNumber)
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }

            // 해당 미끼 끄기
            _gameManager.ReelPoint3.GetChild(0).GetChild(baitSerialNumber - 1999).gameObject.SetActive(false);

            // 바늘 오브젝트 켜기
            _gameManager.ReelPoint3.GetChild(0).GetChild(0).gameObject.SetActive(true);
            CheckBaitProbability();

            switch(_mapType)
            {
                case PublicDefined.eMapType.jeongdongjin:
                    _fishObjectManager.IncreaseProbabilityAccordingToMovement(false);
                    break;
                case PublicDefined.eMapType.skyway:
                    _fishObjectManagerSkyway.IncreaseProbabilityAccordingToMovement(false);
                    break;
                case PublicDefined.eMapType.homerspit:
                    _fishObjectManagerHomerspit.IncreaseProbabilityAccordingToMovement(false);
                    break;
            }
        }
        _lureWeight = 0;
    }

    public void UpdateEquipmentFloatOn()
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }

            _gameManager.ReelPoint2.GetChild(0).gameObject.SetActive(true);
            _gameManager.GameStyleSstate = GameManager.eGameStyle.Bobber;
        }

        if (_depthLength <= 0)
        {
            _userData.GetCurrentEquipmentDictionary()["depthlength"] = 5;
            _depthLength = 5;
        }
    }

    public void UpdateEquipmentFloatOff()
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_gameManager == null)
            {
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }

            _gameManager.ReelPoint2.GetChild(0).gameObject.SetActive(false);
            _gameManager.GameStyleSstate = GameManager.eGameStyle.Onetwo;

        }
    }
    public void UpdateEquipmentSinkerOn(float weight)
    {
        //Debug.LogError(_mapType);
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_objectManager == null)
                _objectManager = GameObject.FindGameObjectWithTag("Object").GetComponent<ObjectManager>();

            if(_gameManager == null)
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            // 봉돌 위치
            _gameManager.SetSinkerObjectActive(true);

        }
        _sinkerWeight = weight;
    }
    public void UpdateEquipmentSinkerOff()
    {
        if (_mapType != PublicDefined.eMapType.lobby)
        {
            // 게임씬인데 객체가 없다면 가져온다.
            if (_objectManager == null)
            {
                _objectManager = GameObject.FindGameObjectWithTag("Object").GetComponent<ObjectManager>();
            }
            _gameManager.SetSinkerObjectActive(false);

        }
        _sinkerWeight = 0;
    }

    public void RemoveBaitWhenBaitUse()
    {
        //Debug.Log("11");
        // 장착하고 있는 미끼가 1개가 아니라 여러개라면 개수만 줄인다.
        // 장착하고 있는 미끼가 마지막 미끼라면 장착 해제한다.
        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        int currentBaitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
        //Debug.LogError(currentBaitNumber);

        if (currentBaitNumber.Equals(-1))
        {
            return;
        }
        else
        {
            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            Dictionary<int, int> baitDic = _userData.GetBaitDictionary();

            baitDic[currentBaitNumber]--;
            updateDic.Add("/bait/" + currentBaitNumber.ToString(), baitDic[currentBaitNumber]) ;

            if(baitDic[currentBaitNumber].Equals(0))
            {
                if (_gameManager == null)
                {
                    _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                }
                //Debug.LogError("미끼 자식 번호: " + currentBaitNumber + 1);
                // 해당 미끼 끄기
                _gameManager.ReelPoint3.GetChild(0).GetChild(currentBaitNumber + 1).gameObject.SetActive(false);

                // 바늘 오브젝트 켜기
                _gameManager.ReelPoint3.GetChild(0).GetChild(0).gameObject.SetActive(true);

                _gameManager.UpdateFishSearchRange(0);

                _userData.GetCurrentEquipmentDictionary()["bait"] = -1;

                switch(_mapType)
                {
                    case PublicDefined.eMapType.jeongdongjin:
                        CheckBaitProbability_Jeongdongjin();
                        break;
                    case PublicDefined.eMapType.skyway:
                        CheckBaitProbability_Skyway();
                        break;
                    case PublicDefined.eMapType.homerspit:
                        CheckBaitProbability_Homerspit();
                        break;
                }

                //Debug.Log(Equipment.INSTANCE);
                if(Equipment.INSTANCE != null)
                {
                    Equipment.INSTANCE.SetCurrentBaitItem(null);
                    Equipment._prevBaitItemSerialNumber = -1;
                }

                updateDic.Add("/equipment/bait/", -1);
            }

            DBManager.INSTANCE.UpdateFirebase(updateDic);
        }
    }
}
