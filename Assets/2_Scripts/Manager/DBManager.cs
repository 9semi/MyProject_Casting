using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;

public class DBManager : MonoBehaviour
{
    readonly int _version = 107;

    readonly string _userinfoString = "UserInformation";
    readonly string _NicknameString = "Nickname";
    readonly string _nicknameString = "_nickname";
    readonly string _goldString = "_gold";
    readonly string _pearlString = "_pearl";
    readonly string _gradeString = "_grade";
    readonly string _starString = "_star";
    readonly string _dataString = "data";
    readonly string _aquariumString = "aquarium";
    readonly string _fishNumberString = "_fishNumber";
    readonly string _fishTypeString = "_fishType";
    readonly string _lengthString = "_length";
    readonly string _nameString = "_name";
    readonly string _priceString = "_price";
    readonly string _weightString = "_weight";
    readonly string _keyString = "_key";
    readonly string _passAboutFish_jeongdongjinString = "jeongdongjinPassAboutFish";
    readonly string _passAboutFish_skywayString = "skywayPassAboutFish";
    readonly string _passAboutFish_homerspitString = "homerspitPassAboutFish";
    readonly string _passAboutAction_jeongdongjinString = "jeongdongjinPassAboutAction";
    readonly string _passAboutAction_skywayString = "skywayPassAboutAction";
    readonly string _passAboutAction_homerspitString = "homerspitPassAboutAction";
    readonly string _currentPassIndex_jeongdongjinString = "_currentJeongdongjinPassIndex";
    readonly string _currentPassIndex_skywayString = "_currentSkywayPassIndex";
    readonly string _currentPassIndex_homerspitString = "_currentHomerspitPassIndex";
    readonly string _jeongdongjinFreeReward = "jeongdongjinPassFreeRewardState";
    readonly string _skywayFreeReward = "skywayPassFreeRewardState";
    readonly string _homerspitFreeReward = "homerspitPassFreeRewardState";
    readonly string _jeongdongjinPreReward = "jeongdongjinPassPremiumRewardState";
    readonly string _skywayPreReward = "skywayPassPremiumRewardState";
    readonly string _homerspitPreReward = "homerspitPassPremiumRewardState";
    readonly string _platinumPackage = "platinumPackage";
    readonly string _diamondPackage = "diamondPackage";

    readonly string _editorFirebaseUid = "B9DPmwlqvGaP6zGMNLyjpsXv3JF2"; // 내 파이어 베이스 UID

    static DBManager _uniqueInstance;
    static public DBManager INSTANCE
    {
        get { return _uniqueInstance; }
    }

    UserData _userData;
    StringBuilder _sb = new StringBuilder();
    GoogleManager _googleManager;
    public void GetGoogleManager()
    {
        if (_googleManager == null)
        {
            if(GameObject.FindGameObjectWithTag("GoogleManager"))
                _googleManager = GameObject.FindGameObjectWithTag("GoogleManager").GetComponent<GoogleManager>();
        }
           
    }

    bool _dataLoadSuccess; public bool DataLoadSuccess { set { _dataLoadSuccess = value; } }
    int _dataLoadProgress; public int DataLoadProgress { set { _dataLoadProgress = value; } }
    bool _isTest;

    private void Awake()
    {
        _uniqueInstance = this;
        //Debug.Log("버전: " + _version);
        UserDataInit();
    }
    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
        OptionXmlLoad();
    }

    public void UserDataInit()
    {
        _userData = new UserData();
        _userData.InitData();
    }
    private void Start()
    {
        //임시 테스트
        //Debug.Log(PlayerSettings.bundleVersion);

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            if(DataManager.INSTANCE._isNoneLoginVersion)
                return;

            DataManager.INSTANCE.SetUserData(_userData);
            StartCoroutine(DataLoadCoroutine());
        }
    }
    IEnumerator DataLoadCoroutine()
    {
        WaitForSeconds delay = PublicDefined._2secDelay;
        GetGoogleManager();

        while (!_dataLoadSuccess)
        {
            switch(_dataLoadProgress)
            {
                case 0:
                    LoadPublicData();
                    break;
                case 1:
                    LoadAquariumData();
                    break;
                case 2:
                    LoadPassData();
                    break;
                case 3:
                    LoadItemData();
                    break;
                case 4:
                    LoadRankData();
                    break;
                default:
                    break;
            }

            yield return delay;
        }

        //Debug.Log("씬 이동");
        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            if (!_isTest)
                SceneManager.LoadScene("LobbyScene");

        }
        else
        {
            _googleManager.StateTextOnOff(false);
            _googleManager.EverythingIsReadyUntilStart();

        }
    }

    public UserData GetUserData()
    {
        return _userData;
    }
    public void SetUId(string uid)
    {
        _userData.SetUId(uid);
    }
    public void SetName(string name)
    {
        _userData.SetNickname(name);
    }
    Dictionary<string, object> ConvertToDictionary(string key, object value)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(key, value);
        return dic;
    }
    public IEnumerator VersionCheck(GoogleManager.UserInformation userinfo)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Version");
        GetGoogleManager();
        reference.GetValueAsync().ContinueWith(task =>
        {
            if(task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("실패 이유: " + task.Result.ToString());
                _googleManager.SomethingsWrong();
            }

            if(task.IsCompleted)
            {
                DataSnapshot ss = task.Result;

                int firebaseVersion = int.Parse(ss.Value.ToString());

                Debug.Log(firebaseVersion + " , " + _version);

                if (firebaseVersion == _version)
                {
                    StartCoroutine(ExistUId(userinfo));
                }
                else
                {
                    _googleManager.UpdatePlz();
                }
            }
        });
        yield return null;
    }

    public IEnumerator ExistUId(GoogleManager.UserInformation userinfo)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString);
        GetGoogleManager();
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("실패 이유: " + task.Result.ToString());
                _googleManager.SomethingsWrong();
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.HasChild(userinfo.GetUId()))
                {
                    _googleManager.StateTextUpdate("정보를 확인했습니다.");
                    StartCoroutine(HaveNickname(userinfo));
                }
                else
                {
                    StartCoroutine(CreateNewAddress(userinfo));
                }
            }
        });
        yield return null;
    }
    IEnumerator CreateNewAddress(GoogleManager.UserInformation userinfo)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString);
        string json = JsonUtility.ToJson(userinfo);
        reference.Child(userinfo.GetUId()).SetRawJsonValueAsync(json);
        GetGoogleManager();
        _googleManager.CreateNicknameUI();
        yield return null;
    }
    public IEnumerator HaveNickname(GoogleManager.UserInformation userinfo)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(userinfo.GetUId());
        GetGoogleManager();

        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("실패 이유: " + task.Result.ToString());
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.HasChild("data"))
                {
                    _googleManager.StateTextUpdate("데이터가 확인되었습니다.");
                    StartCoroutine(DataLoadCoroutine());
                }
                else
                {
                    // 닉네임이 없다면
                    _googleManager.StateTextUpdate("데이터가 없습니다.");
                    _googleManager.CreateNicknameUI();
                }
            }
        });
        yield return null;
    }
    public IEnumerator ExistNickname(string nickname)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_NicknameString);
        GetGoogleManager();
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("실패 이유: " + task.Result.ToString());
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.HasChild(nickname))
                { 
                    // 중복되는 닉네임이 있다.
                    _googleManager.TheNameAlreadyExists();
                }
                else
                { 
                    // 중복되는 닉네임이 없다.
                    _googleManager.TheNameIsAvailable();
                }
            }
        });

        yield return null;
    }
    public void OptionXmlLoad()
    {
        string path;

        if(Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            path = Application.dataPath + "/OptionSetting.xml";
        }
        else
        {
            path = Application.persistentDataPath + "/OptionSetting.xml";
        }

        XmlDocument xml = new XmlDocument();

        //Debug.Log("경로: " + path);
        if (File.Exists(path))
        {
            //Debug.Log("파일이 존재합니다:");
            try
            {
                xml.Load(path);
                XmlNodeList xList = xml.SelectNodes("/Option");

                float bgmVolume = 0.5f;
                bool bgmMute = false;
                float effectVolume = 0.5f;
                bool effectMute = false;
                bool vib = true;
                bool land = false;

                foreach (XmlNode recordNode in xList)
                {
                    bgmVolume = float.Parse(recordNode["BgmVolume"].InnerText);
                    bgmMute = bool.Parse(recordNode["BgmMute"].InnerText);
                    effectVolume = float.Parse(recordNode["EffectVolume"].InnerText);
                    effectMute = bool.Parse(recordNode["EffectMute"].InnerText);
                    vib = bool.Parse(recordNode["Vibration"].InnerText);
                    land = bool.Parse(recordNode["Landscape"].InnerText);
                }

                //Debug.Log("배경음 볼륨 : " + bgmVolume + ", 배경음 음소거 : " + bgmMute + ", 효과음 볼륨 : " + effectVolume + ", 효과음 음소거 : " + effectMute);

                AudioManager.INSTANCE.BGMSetting(bgmVolume, bgmMute);
                AudioManager.INSTANCE.EffectSetting(effectVolume, effectMute);
                DataManager.INSTANCE._vibration = vib;
                DataManager.INSTANCE._landscape = land;

                if (land)
                    Screen.orientation = ScreenOrientation.LandscapeRight;
                else
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            catch
            {
                //Debug.Log("파일이 존재하지 않습니다: 캐치문");
                SaveXml(true);
            }
        }
        else
        {
            //Debug.Log("파일이 존재하지 않습니다:");
            SaveXml(true);
        }
    }
    public void SaveXml(bool isFirst)
    {
        XmlDocument xml = new XmlDocument();
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode rootNode = xml.CreateNode(XmlNodeType.Element, "Option", string.Empty);
        xml.AppendChild(rootNode);

        if(isFirst)
        { 
            XmlElement bgmV = xml.CreateElement("BgmVolume");
            bgmV.InnerText = "0.5";
            rootNode.AppendChild(bgmV);

            XmlElement bgmM = xml.CreateElement("BgmMute");
            bgmM.InnerText = "False";
            rootNode.AppendChild(bgmM);

            XmlElement effV = xml.CreateElement("EffectVolume");
            effV.InnerText = "0.5";
            rootNode.AppendChild(effV);

            XmlElement effM = xml.CreateElement("EffectMute");
            effM.InnerText = "False";
            rootNode.AppendChild(effM);

            XmlElement v = xml.CreateElement("Vibration");
            v.InnerText = "True";
            rootNode.AppendChild(v);

            XmlElement l = xml.CreateElement("Landscape");
            l.InnerText = "False";
            rootNode.AppendChild(l);

            AudioManager.INSTANCE.BGMSetting(0.5f, false);
            AudioManager.INSTANCE.EffectSetting(0.5f, false);
            DataManager.INSTANCE._vibration = true;
            DataManager.INSTANCE._landscape = false;

        }
        else
        {
            XmlElement bgmV = xml.CreateElement("BgmVolume");
            bgmV.InnerText = AudioManager.INSTANCE.BgmVolume.ToString();
            rootNode.AppendChild(bgmV);

            XmlElement bgmM = xml.CreateElement("BgmMute");
            bgmM.InnerText = AudioManager.INSTANCE.BgmMute.ToString();
            rootNode.AppendChild(bgmM);

            XmlElement effV = xml.CreateElement("EffectVolume");
            effV.InnerText = AudioManager.INSTANCE.EffectVolume.ToString();
            rootNode.AppendChild(effV);

            XmlElement effM = xml.CreateElement("EffectMute");
            effM.InnerText = AudioManager.INSTANCE.EffectMute.ToString();
            rootNode.AppendChild(effM);

            XmlElement v = xml.CreateElement("Vibration");
            v.InnerText = DataManager.INSTANCE._vibration.ToString();
            rootNode.AppendChild(v);

            XmlElement l = xml.CreateElement("Landscape");
            l.InnerText = DataManager.INSTANCE._landscape.ToString();
            rootNode.AppendChild(l);

            //Debug.Log("배경음 볼륨: " + AudioManager.INSTANCE._bgmVolume + ", 배경음 음소거: " + 
                //AudioManager.INSTANCE._bgmMute + ", 효과음 볼륨: " + AudioManager.INSTANCE._effectVolume + ", 효과음 음소거: " + AudioManager.INSTANCE._effectMute);
        }

        string path;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            path = Application.dataPath + "/OptionSetting.xml";
        }
        else
        {
            path = Application.persistentDataPath + "/OptionSetting.xml";
        }

        xml.Save(path);
    }

    public void UserDataLoad()
    {
        LoadPublicData();
    }

    void LoadPublicData()
    {
        DatabaseReference reference;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }
        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());
            }
            else if (t.IsCompleted)
            {
                DataSnapshot ss = t.Result;
                
                _userData._nickname = ss.Child(_nicknameString).Value.ToString();
                _userData._gold = int.Parse(ss.Child(_goldString).Value.ToString());
                _userData._pearl = int.Parse(ss.Child(_pearlString).Value.ToString());
                _userData._grade = int.Parse(ss.Child(_gradeString).Value.ToString());
                _userData._star = float.Parse(ss.Child(_starString).Value.ToString());
                _userData._win = int.Parse(ss.Child("_win").Value.ToString());
                _userData._lose = int.Parse(ss.Child("_lose").Value.ToString());
                _userData._draw = int.Parse(ss.Child("_draw").Value.ToString());
                _userData._haveRepresentFish = bool.Parse(ss.Child("_haveRepresentFish").Value.ToString());
                _userData._havePlatinumPackage = bool.Parse(ss.Child("_havePlatinumPackage").Value.ToString());
                _userData._haveDiamondPackage = bool.Parse(ss.Child("_haveDiamondPackage").Value.ToString());
                DataManager.INSTANCE._tutorialIsDone = bool.Parse(ss.Child("_isTutorialDone").Value.ToString());
                
                if(ss.HasChild("_isGetUlseomCoupon"))
                {
                    _userData._isGetUlseomCoupon = bool.Parse(ss.Child("_isGetUlseomCoupon").Value.ToString());
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("_isGetUlseomCoupon", false);
                    reference.UpdateChildrenAsync(dic);
                }

                if(ss.HasChild("_haveADBlock"))
                {
                    _userData._haveADBlock = bool.Parse(ss.Child("_haveADBlock").Value.ToString());
                    DataManager.INSTANCE._isADBlock = _userData._haveADBlock;
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("_haveADBlock", false);
                    reference.UpdateChildrenAsync(dic);
                }

                if(ss.HasChild("_isGetGStarCoupon"))
                {
                    _userData._isGetGStarCoupon = bool.Parse(ss.Child("_isGetGStarCoupon").Value.ToString());
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("_isGetGStarCoupon", false);
                    reference.UpdateChildrenAsync(dic);
                }
                
                if (_userData._haveRepresentFish)
                {
                    Dictionary<string, object> d = ObjectToDictionary2(ss.Child("representFish").Value);
                    int number = int.Parse(d[_fishNumberString].ToString());
                    int type = int.Parse(d[_fishTypeString].ToString());
                    int price = int.Parse(d[_priceString].ToString());
                    float length = float.Parse(d[_lengthString].ToString());
                    float weight = float.Parse(d[_weightString].ToString());
                    string name = d[_nameString].ToString();
                    string key = d[_keyString].ToString();

                    _userData.InitRepresentFish(number, name, length, weight, price, type, key);
                }

                if(_userData._havePlatinumPackage)
                {
                    Dictionary<string, bool> packageDic = new Dictionary<string, bool>();

                    packageDic = _userData.GetPlatinumPackage();

                    foreach (DataSnapshot data in ss.Child(_platinumPackage).Children)
                    {
                        packageDic[data.Key] = bool.Parse(data.Value.ToString());
                    }
                }

                if(_userData._haveDiamondPackage)
                {
                    Dictionary<string, bool> packageDic = new Dictionary<string, bool>();

                    packageDic = _userData.GetDiamondPackage();

                    foreach (DataSnapshot data in ss.Child(_diamondPackage).Children)
                    {
                        packageDic[data.Key] = bool.Parse(data.Value.ToString());
                    }
                }
                _dataLoadProgress = 1;
                LoadAquariumData();
            }
        });
    }

    void LoadItemData()
    {
        //Debug.Log("아이템");
        DatabaseReference reference;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());

            }
            else if (t.IsCompleted)
            {

                DataSnapshot ss = t.Result;

                Dictionary<int, int> dic;

                // 낚싯대, 릴, 미끼 등등 장비 아이템 정보 받아오기
                dic = _userData.GetBaitDictionary();
                for (int i = 0; i < ss.Child("bait").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("bait").Child(i.ToString()).Value.ToString());
                }
                dic = _userData.GetRodDictionary();

                for (int i = 0; i < ss.Child("rod").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("rod").Child(i.ToString()).Value.ToString());
                }
                dic = _userData.GetReelDictionary();

                for (int i = 0; i < ss.Child("reel").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("reel").Child(i.ToString()).Value.ToString());
                }
                dic = _userData.GetFloatDictionary();

                for (int i = 0; i < ss.Child("float").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("float").Child(i.ToString()).Value.ToString());
                }
                dic = _userData.GetPasteBaitDictionary();

                for (int i = 0; i < ss.Child("pastebait").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("pastebait").Child(i.ToString()).Value.ToString());
                }
                dic = _userData.GetSinkerDictionary();

                for (int i = 0; i < ss.Child("sinker").ChildrenCount; i++)
                {
                    dic[i] = int.Parse(ss.Child("sinker").Child(i.ToString()).Value.ToString());
                }

                Dictionary<string, int> dic3;
                // 현재 끼고 있는 장비
                dic3 = _userData.GetCurrentEquipmentDictionary();
                foreach (DataSnapshot data in ss.Child("equipment").Children)
                {
                   // Debug.Log("data.Key = " + data.Key + ", data.Value = " + data.Value);
                    dic3[data.Key] = int.Parse(data.Value.ToString());
                }
                DataManager.INSTANCE._depthLength = dic3["depthlength"];
                _dataLoadProgress = 2;
               // Debug.Log("장비 데이터 로드");
                LoadRankData();
            }
        });
    }

    void LoadAquariumData()
    {
        //Debug.Log("아쿠아리움");
        DatabaseReference reference;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());
            }
            else if (t.IsCompleted)
            {
                DataSnapshot ss = t.Result;

                Dictionary<int, List<PublicDefined.stFishInfo>> dic4;

                // 수족관 정보 받아오기
                // 수족관 보유 여부
                _userData._haveFirstAquarium = bool.Parse(ss.Child("_haveFirstAquarium").Value.ToString());
                _userData._haveSecondAquarium = bool.Parse(ss.Child("_haveSecondAquarium").Value.ToString());
                _userData._haveThirdAquarium = bool.Parse(ss.Child("_haveThirdAquarium").Value.ToString());
                _userData._haveFourthAquarium = bool.Parse(ss.Child("_haveFourthAquarium").Value.ToString());
                _userData._haveFifthAquarium = bool.Parse(ss.Child("_haveFifthAquarium").Value.ToString());

                _userData.InitAquarium();

                // 각 수족관에 무슨 물고기가 있는지 로드해야 한다.
                if (_userData._haveFirstAquarium && ss.HasChild(_aquariumString))
                {
                    if (ss.Child(_aquariumString).HasChild("first"))
                    {
                        dic4 = _userData.GetFirstAquariumDictionary();
                        foreach (DataSnapshot data in ss.Child(_aquariumString).Child("first").Children)
                        {
                            Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                            int number = int.Parse(d[_fishNumberString].ToString());
                            int type = int.Parse(d[_fishTypeString].ToString());
                            float length = float.Parse(d[_lengthString].ToString());
                            string name = d[_nameString].ToString();
                            int price = int.Parse(d[_priceString].ToString());
                            float weight = float.Parse(d[_weightString].ToString());
                            string key = d[_keyString].ToString();
                            PublicDefined.stFishInfo fishInfo = new PublicDefined.stFishInfo(number, name, length, weight, price, (PublicDefined.eFishType)type);
                            fishInfo.SetKey(key);

                            if (dic4.ContainsKey(number))
                            {
                                // 만약 해당 번호의 물고기가 이미 있다면 리스트에 추가해야 한다.
                                dic4[number].Add(fishInfo);
                            }
                            else
                            {
                                List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
                                list.Add(fishInfo);
                                dic4.Add(number, list);
                            }
                        }

                    }
                }

                if (_userData._haveSecondAquarium && ss.HasChild(_aquariumString))
                {
                    if (ss.Child(_aquariumString).HasChild("second"))
                    {
                        dic4 = _userData.GetSecondAquariumDictionary();
                        foreach (DataSnapshot data in ss.Child(_aquariumString).Child("second").Children)
                        {
                            Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                            int number = int.Parse(d[_fishNumberString].ToString());
                            int type = int.Parse(d[_fishTypeString].ToString());
                            float length = float.Parse(d[_lengthString].ToString());
                            string name = d[_nameString].ToString();
                            int price = int.Parse(d[_priceString].ToString());
                            float weight = float.Parse(d[_weightString].ToString());
                            string key = d[_keyString].ToString();
                            PublicDefined.stFishInfo fishInfo = new PublicDefined.stFishInfo(number, name, length, weight, price, (PublicDefined.eFishType)type);
                            fishInfo.SetKey(key);

                            if (dic4.ContainsKey(number))
                            {
                                // 만약 해당 번호의 물고기가 이미 있다면 리스트에 추가해야 한다.
                                dic4[number].Add(fishInfo);
                            }
                            else
                            {
                                List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
                                list.Add(fishInfo);
                                dic4.Add(number, list);
                            }
                        }
                    }
                }
                if (_userData._haveThirdAquarium && ss.HasChild(_aquariumString))
                {
                    if (ss.Child(_aquariumString).HasChild("third"))
                    {
                        dic4 = _userData.GetThirdAquariumDictionary();
                        foreach (DataSnapshot data in ss.Child(_aquariumString).Child("third").Children)
                        {
                            Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                            int number = int.Parse(d[_fishNumberString].ToString());
                            int type = int.Parse(d[_fishTypeString].ToString());
                            float length = float.Parse(d[_lengthString].ToString());
                            string name = d[_nameString].ToString();
                            int price = int.Parse(d[_priceString].ToString());
                            float weight = float.Parse(d[_weightString].ToString());
                            string key = d[_keyString].ToString();
                            PublicDefined.stFishInfo fishInfo = new PublicDefined.stFishInfo(number, name, length, weight, price, (PublicDefined.eFishType)type);
                            fishInfo.SetKey(key);
                            if (dic4.ContainsKey(number))
                            {
                                // 만약 해당 번호의 물고기가 이미 있다면 리스트에 추가해야 한다.
                                dic4[number].Add(fishInfo);
                            }
                            else
                            {
                                List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
                                list.Add(fishInfo);
                                dic4.Add(number, list);
                            }
                        }
                    }

                }

                if (_userData._haveFourthAquarium && ss.HasChild(_aquariumString))
                {
                    if (ss.Child(_aquariumString).HasChild("fourth"))
                    {
                        dic4 = _userData.GetFourthAquariumDictionary();
                        foreach (DataSnapshot data in ss.Child(_aquariumString).Child("fourth").Children)
                        {
                            Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                            int number = int.Parse(d[_fishNumberString].ToString());
                            int type = int.Parse(d[_fishTypeString].ToString());
                            float length = float.Parse(d[_lengthString].ToString());
                            string name = d[_nameString].ToString();
                            int price = int.Parse(d[_priceString].ToString());
                            float weight = float.Parse(d[_weightString].ToString());
                            string key = d[_keyString].ToString();
                            PublicDefined.stFishInfo fishInfo = new PublicDefined.stFishInfo(number, name, length, weight, price, (PublicDefined.eFishType)type);
                            fishInfo.SetKey(key);
                            if (dic4.ContainsKey(number))
                            {
                                // 만약 해당 번호의 물고기가 이미 있다면 리스트에 추가해야 한다.
                                dic4[number].Add(fishInfo);
                            }
                            else
                            {
                                List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
                                list.Add(fishInfo);
                                dic4.Add(number, list);
                            }
                        }
                    }
                }

                if (_userData._haveFifthAquarium && ss.HasChild(_aquariumString))
                {
                    if (ss.Child(_aquariumString).HasChild("fifth"))
                    {
                        dic4 = _userData.GetFifthAquariumDictionary();
                        foreach (DataSnapshot data in ss.Child(_aquariumString).Child("fifth").Children)
                        {
                            Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                            int number = int.Parse(d[_fishNumberString].ToString());
                            int type = int.Parse(d[_fishTypeString].ToString());
                            float length = float.Parse(d[_lengthString].ToString());
                            string name = d[_nameString].ToString();
                            int price = int.Parse(d[_priceString].ToString());
                            float weight = float.Parse(d[_weightString].ToString());
                            string key = d[_keyString].ToString();
                            PublicDefined.stFishInfo fishInfo = new PublicDefined.stFishInfo(number, name, length, weight, price, (PublicDefined.eFishType)type);
                            fishInfo.SetKey(key);
                            if (dic4.ContainsKey(number))
                            {
                                // 만약 해당 번호의 물고기가 이미 있다면 리스트에 추가해야 한다.
                                dic4[number].Add(fishInfo);
                            }
                            else
                            {
                                List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
                                list.Add(fishInfo);
                                dic4.Add(number, list);
                            }
                        }
                    }
                }

                _userData.CheckAquariumCount();
               // Debug.Log("아쿠아리움 데이터 로드");
                _dataLoadProgress = 2;
                LoadPassData();
            }
        });
    }

    void LoadPassData()
    {
        
        //Debug.Log("패스");
        DatabaseReference reference;
        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());
            }
            else if (t.IsCompleted)
            {
                DataSnapshot ss = t.Result;

                Dictionary<int, int> dic;
                Dictionary<int, bool> dic2;
                Dictionary<int, Dictionary<int, int>> dic5;

                // 패스 보유 여부
                _userData._haveJeongdongjinPass = bool.Parse(ss.Child("_haveJeongdongjinPass").Value.ToString());
                _userData._haveSkywayPass = bool.Parse(ss.Child("_haveSkywayPass").Value.ToString());
                _userData._haveHomerspitPass = bool.Parse(ss.Child("_haveHomerspitPass").Value.ToString());

                // 각 맵의 패스 인덱스
                _userData._currentJeongdongjinPassIndex = int.Parse(ss.Child("_currentJeongdongjinPassIndex").Value.ToString());
                _userData._currentSkywayPassIndex = int.Parse(ss.Child("_currentSkywayPassIndex").Value.ToString());
                _userData._currentHomerspitPassIndex = int.Parse(ss.Child("_currentHomerspitPassIndex").Value.ToString());


                // 패스 보상 수령 정보 받아오기
                dic2 = _userData.GetCheckJeongdongjinPassFreeReward();

                for (int i = 0; i < ss.Child(_jeongdongjinFreeReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_jeongdongjinFreeReward).Child(i.ToString()).Value.ToString());
                }

                dic2 = _userData.GetCheckSkywayPassFreeReward();

                for (int i = 0; i < ss.Child(_skywayFreeReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_skywayFreeReward).Child(i.ToString()).Value.ToString());
                }

                dic2 = _userData.GetCheckHomerPassFreeReward();

                for (int i = 0; i < ss.Child(_homerspitFreeReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_homerspitFreeReward).Child(i.ToString()).Value.ToString());
                }

                dic2 = _userData.GetCheckJeongdongjinPassPremiumReward();

                for (int i = 0; i < ss.Child(_jeongdongjinPreReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_jeongdongjinPreReward).Child(i.ToString()).Value.ToString());
                }

                dic2 = _userData.GetCheckSkywayPassPremiumReward();

                for (int i = 0; i < ss.Child(_skywayPreReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_skywayPreReward).Child(i.ToString()).Value.ToString());
                }

                dic2 = _userData.GetCheckHomerspitPassPremiumReward();

                for (int i = 0; i < ss.Child(_homerspitPreReward).ChildrenCount; i++)
                {
                    dic2[i] = bool.Parse(ss.Child(_homerspitPreReward).Child(i.ToString()).Value.ToString());
                }
                //Debug.Log("1");
                // 물고기와 관련된 패스 정보 받아오기
                if (ss.HasChild(_passAboutFish_jeongdongjinString))
                {
                    // 자식이 있다면 안에 무슨 자료가 있긴 하니까 받아오자.
                    dic5 = _userData.GetCurrentStateOfJeongdongjinPassAboutFish();
                    Dictionary<string, object> fishDic;
                   //Debug.Log(ss.Child(_passAboutFish_jeongdongjinString).ChildrenCount);

                    foreach (DataSnapshot data in ss.Child(_passAboutFish_jeongdongjinString).Children)
                    {
                        //Debug.Log(data.Key);
                        fishDic = ObjectToDictionary2(data.Value);

                        Dictionary<int, int> dic55 = new Dictionary<int, int>();
                       // Debug.Log("가");
                        foreach (KeyValuePair<string, object> data2 in fishDic)
                        {
                           // Debug.Log("나");
                           // Debug.Log("data2.Key : " + data2.Key + ", " + data2.Key.GetType());
                           // Debug.Log("data2.Value : " + data2.Value + ", " + data2.Value.GetType());
                            dic55.Add(int.Parse(data2.Key), int.Parse(data2.Value.ToString()));
                        }
                        dic5[int.Parse(data.Key)] = dic55;
                    }
                }
               // Debug.Log("2");
                if (ss.HasChild(_passAboutFish_skywayString))
                {
                    // 자식이 있다면 안에 무슨 자료가 있긴 하니까 받아오자.
                    dic5 = _userData.GetCurrentStateOfSkywayPassAboutFish();
                    Dictionary<string, object> fishDic;
                    foreach (DataSnapshot data in ss.Child(_passAboutFish_skywayString).Children)
                    {
                        fishDic = ObjectToDictionary2(data.Value);
                        Dictionary<int, int> dic55 = new Dictionary<int, int>();
                        foreach (KeyValuePair<string, object> data2 in fishDic)
                        {
                            //Debug.Log("data.Key : " + key + ", " + data.Key.GetType());
                            //Debug.Log("data2.Key : " + data2.Key + ", " + data2.Key.GetType());
                            //Debug.Log("data2.Value : " + data2.Value + ", " + data2.Value.GetType());
                            dic55.Add(int.Parse(data2.Key), int.Parse(data2.Value.ToString()));
                        }
                        dic5[int.Parse(data.Key)] = dic55;
                    }
                }
                //Debug.Log("3");
                if (ss.HasChild(_passAboutFish_homerspitString))
                {
                    // 자식이 있다면 안에 무슨 자료가 있긴 하니까 받아오자.
                    dic5 = _userData.GetCurrentStateOfHomerspitPassAboutFish();
                    Dictionary<string, object> fishDic;
                    foreach (DataSnapshot data in ss.Child(_passAboutFish_homerspitString).Children)
                    {
                        fishDic = ObjectToDictionary2(data.Value);
                        Dictionary<int, int> dic55 = new Dictionary<int, int>();
                        foreach (KeyValuePair<string, object> data2 in fishDic)
                        {
                            dic55.Add(int.Parse(data2.Key), int.Parse(data2.Value.ToString()));
                        }
                        dic5[int.Parse(data.Key)] = dic55;
                    }
                }
               // Debug.Log("5");
                // 액션과 관련된 패스 정보 받아오기
                if (ss.HasChild(_passAboutAction_jeongdongjinString))
                {
                    dic = _userData.GetCurrentStateOfJeongdongjinPassAboutAction();
                    Dictionary<string, object> diczz = ObjectToDictionary2(ss.Child(_passAboutAction_jeongdongjinString).Value);

                    foreach (KeyValuePair<string, object> datazz in diczz)
                    {
                        dic[int.Parse(datazz.Key)] = int.Parse(datazz.Value.ToString());
                    }
                }
                //Debug.Log("6");
                if (ss.HasChild(_passAboutAction_skywayString))
                {
                    dic = _userData.GetCurrentStateOfSkywayPassAboutAction();
                    Dictionary<string, object> diczz = ObjectToDictionary2(ss.Child(_passAboutAction_skywayString).Value);
                    foreach (KeyValuePair<string, object> datazz in diczz)
                    {
                        dic[int.Parse(datazz.Key)] = int.Parse(datazz.Value.ToString());
                    }
                }
                //Debug.Log("7");
                if (ss.HasChild(_passAboutAction_homerspitString))
                {
                    dic = _userData.GetCurrentStateOfHomerspitPassAboutAction();
                    Dictionary<string, object> diczz = ObjectToDictionary2(ss.Child(_passAboutAction_homerspitString).Value);

                    foreach (KeyValuePair<string, object> datazz in diczz)
                    {
                        dic[int.Parse(datazz.Key)] = int.Parse(datazz.Value.ToString());
                    }
                }
                _dataLoadProgress = 3;
              //  Debug.Log("패스 데이터 로드");
                LoadItemData();
            }
        });
    }

    void LoadRankData()
    {
        //Debug.Log("랭크");
        DatabaseReference reference;
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }
        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());
            }
            else if (t.IsCompleted)
            {
                DataSnapshot ss = t.Result;

                Dictionary<int, PublicDefined.stRankFishInfo> dic5;

                string key = "jeongdongjinRank";

                // 각 수족관에 무슨 물고기가 있는지 로드해야 한다.
                if (ss.HasChild(key))
                {
                    dic5 = _userData.GetJeongdongjinRankDictionary();

                    foreach (DataSnapshot data in ss.Child(key).Children)
                    {
                        Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                        int number = int.Parse(d[_fishNumberString].ToString());
                        int type = int.Parse(d[_fishTypeString].ToString());
                        float length = float.Parse(d[_lengthString].ToString());
                        string name = d[_nameString].ToString();
                        float weight = float.Parse(d[_weightString].ToString());

                        PublicDefined.stRankFishInfo fishInfo = new PublicDefined.stRankFishInfo(number, name, length, weight, (PublicDefined.eFishType)type);

                        dic5.Add(number, fishInfo);
                    }
                }
                key = "skywayRank";

                if (ss.HasChild(key))
                {
                    dic5 = _userData.GetSkywayRankDictionary();

                    foreach (DataSnapshot data in ss.Child(key).Children)
                    {
                        Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                        int number = int.Parse(d[_fishNumberString].ToString());
                        int type = int.Parse(d[_fishTypeString].ToString());
                        float length = float.Parse(d[_lengthString].ToString());
                        string name = d[_nameString].ToString();
                        float weight = float.Parse(d[_weightString].ToString());

                        PublicDefined.stRankFishInfo fishInfo = new PublicDefined.stRankFishInfo(number, name, length, weight, (PublicDefined.eFishType)type);

                        dic5.Add(number, fishInfo);
                    }
                }

                key = "homerspitRank";

                if (ss.HasChild(key))
                {
                    dic5 = _userData.GetHomerspitRankDictionary();

                    foreach (DataSnapshot data in ss.Child(key).Children)
                    {
                        Dictionary<string, object> d = ObjectToDictionary2(data.Value);

                        int number = int.Parse(d[_fishNumberString].ToString());
                        int type = int.Parse(d[_fishTypeString].ToString());
                        float length = float.Parse(d[_lengthString].ToString());
                        string name = d[_nameString].ToString();
                        float weight = float.Parse(d[_weightString].ToString());

                        PublicDefined.stRankFishInfo fishInfo = new PublicDefined.stRankFishInfo(number, name, length, weight, (PublicDefined.eFishType)type);

                        dic5.Add(number, fishInfo);
                    }
                }



                _dataLoadSuccess = true;
                _dataLoadProgress = 5;
                Debug.Log("데이터 로드 끝");
            }
        });
    }
    Dictionary<int, bool> ObjectToDictionary(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        Dictionary<int, bool> dic = JsonConvert.DeserializeObject<Dictionary<int, bool>>(json);
        return dic;
    }
    Dictionary<string, object> ObjectToDictionary2(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        return dic;
    }
    public void SaveNewUserData()
    {
        // 낚싯대와 릴 3호로 초기화 시켜준다.
        _userData.InitNewUser();

        Dictionary<string, object> dic = new Dictionary<string, object>();

        NicknameRenewal();
        SaveXml(true);

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString);
        reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);

        string json = JsonUtility.ToJson(_userData);
        reference.SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetRodDictionary());
        reference.Child("rod").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetReelDictionary());
        reference.Child("reel").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetBaitDictionary());
        reference.Child("bait").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetPasteBaitDictionary());
        reference.Child("pastebait").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetFloatDictionary());
        reference.Child("float").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetSinkerDictionary());
        reference.Child("sinker").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckJeongdongjinPassFreeReward());
        reference.Child("jeongdongjinPassFreeRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckSkywayPassFreeReward());
        reference.Child("skywayPassFreeRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckHomerPassFreeReward());
        reference.Child("homerspitPassFreeRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckJeongdongjinPassPremiumReward());
        reference.Child("jeongdongjinPassPremiumRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckSkywayPassPremiumReward());
        reference.Child("skywayPassPremiumRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCheckHomerspitPassPremiumReward());
        reference.Child("homerspitPassPremiumRewardState").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCurrentEquipmentDictionary());
        reference.Child("equipment").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfJeongdongjinPassAboutAction());
        reference.Child("jeongdongjinPassAboutAction").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfSkywayPassAboutAction());
        reference.Child("skywayPassAboutAction").SetRawJsonValueAsync(json);

        json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfHomerspitPassAboutAction());
        reference.Child("homerspitPassAboutAction").SetRawJsonValueAsync(json);

        _userData.CheckAquariumCount();
        GetGoogleManager();
        _googleManager.StateTextOnOff(false);
        _googleManager.EverythingIsReadyUntilStart();

    }
    void NicknameRenewal()
    {
        // Nickname 노드에 닉네임 저장 (중복 체크에 사용)
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_NicknameString);
        //string json = JsonConvert.SerializeObject(ToDictionary(_userData.GetNickname(), _userData.GetUId()));
        //reference.SetRawJsonValueAsync(json);
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(_userData.GetNickname(), _userData.GetUId());
        reference.UpdateChildrenAsync(dic);
    }
    public Dictionary<string, object> ToDictionary(string key, object value)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(key, value);
        return dic;
    }

    // 구매한 아이템 정보 갱신한다.
    public void BuyItem(Item item, bool isGold, GameObject checkUI, int quantity = 1)
    {
        if (quantity.Equals(0))
            quantity = 1;

        //Debug.Log(_userData._gold + "원 있었다가");

        if (isGold)
            _userData._gold -= item.goldCost * quantity;
        else
            _userData._pearl -= item.pearlCost * quantity;

        //Debug.Log(_userData._gold + "원으로 바뀜");

        if (Shop.INSTANCE != null)
            Shop.INSTANCE.UpdateGoldPearl();

        if (FishingGear.INSTANCE != null)
            FishingGear.INSTANCE.UpdateGoldPearl();


        Dictionary<int, int> dic = new Dictionary<int, int>();

        // UserData 데이터에 구매한 아이템 정보를 갱신한다.
        switch (item.Type)
        {
            case PublicDefined.eItemType.Pass:
                UpdatePass(item.serialNumber, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Rod:
                dic = _userData.GetRodDictionary();
                dic[item.serialNumber - 1000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Reel:
                dic = _userData.GetReelDictionary();
                dic[item.serialNumber - 6000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Bait:
                dic = _userData.GetBaitDictionary();
                dic[item.serialNumber - 2000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Pastebait:
                dic = _userData.GetPasteBaitDictionary();
                dic[item.serialNumber - 3000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Float:
                dic = _userData.GetFloatDictionary();
                dic[item.serialNumber - 4000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.Sinker:
                dic = _userData.GetSinkerDictionary();
                dic[item.serialNumber - 5000] = 1;
                UpdateItemData(item, isGold, checkUI);
                break;
            case PublicDefined.eItemType.BaitBox:
                if(quantity.Equals(1) || quantity.Equals(0))
                    RandomBait(item.serialNumber - 7000, checkUI);
                else
                    RandomBait(item.serialNumber - 7000, checkUI, quantity);
                break;
            case PublicDefined.eItemType.Gold:
                _userData._gold += item.quantity * quantity;

                if (Shop.INSTANCE != null)
                    Shop.INSTANCE.UpdateGoldPearl();

                if (FishingGear.INSTANCE != null)
                    FishingGear.INSTANCE.UpdateGoldPearl();

                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("/_gold/", _userData._gold);
                updateDic.Add("/_pearl/", _userData._pearl);
                UpdateFirebase(updateDic);
                checkUI.SetActive(false);
                break;
            case PublicDefined.eItemType.Pearl:
                _userData._pearl += item.quantity * quantity;

                if (Shop.INSTANCE != null)
                    Shop.INSTANCE.UpdateGoldPearl();

                if (FishingGear.INSTANCE != null)
                    FishingGear.INSTANCE.UpdateGoldPearl();

                Dictionary<string, object> updateDic2 = new Dictionary<string, object>();
                updateDic2.Add("/_gold/", _userData._gold);
                updateDic2.Add("/_pearl/", _userData._pearl);
                UpdateFirebase(updateDic2);
                checkUI.SetActive(false);
                break;
        }
    }
    // 구매한 아이템 데이터베이스에 올려야한다.
    void UpdateItemData(Item item, bool isGold, GameObject checkUI)
    {
        DatabaseReference reference;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child("data");
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();

        if (isGold)
            dic.Add("/_gold" , _userData._gold);
        else
            dic.Add("/_pearl" , _userData._pearl);

        // 파이어베이스에 데이터 정보를 업데이트한다.
        switch (item.Type)
        {
            case PublicDefined.eItemType.Pass:
                break;
            case PublicDefined.eItemType.Rod:
                //dic.Add((item.serialNumber - 1000).ToString(), 1); 테스트 전부터 원래 주석처리였음 여기는 풀지말도록
                dic.Add("/rod/" + (item.serialNumber - 1000).ToString(), 1);
                //reference.Child("rod").UpdateChildrenAsync(dic);
                break;
            case PublicDefined.eItemType.Reel:
                //dic.Add((item.serialNumber - 6000).ToString(), 1);
                dic.Add("/reel/" + (item.serialNumber - 6000).ToString(), 1);
                //reference.Child("reel").UpdateChildrenAsync(dic);
                break;
            case PublicDefined.eItemType.Bait:
                //dic.Add((item.serialNumber - 2000).ToString(), 1);
                dic.Add("/bait/" + (item.serialNumber - 2000).ToString(), 1);
                //reference.Child("bait").UpdateChildrenAsync(dic);
                break;
            case PublicDefined.eItemType.Pastebait:
                //dic.Add((item.serialNumber - 3000).ToString(), 1);
                dic.Add("/pastebait/" + (item.serialNumber - 3000).ToString(), 1);
                //reference.Child("pastebait").UpdateChildrenAsync(dic);
                break;
            case PublicDefined.eItemType.Float:
                //dic.Add((item.serialNumber - 4000).ToString(), 1);
                dic.Add("/float/" + (item.serialNumber - 4000).ToString(), 1);
                //reference.Child("float").UpdateChildrenAsync(dic);
                break;
            case PublicDefined.eItemType.Sinker:
                //dic.Add((item.serialNumber - 5000).ToString(), 1);
                dic.Add("/sinker/" + (item.serialNumber - 5000).ToString(), 1);
                //reference.Child("sinker").UpdateChildrenAsync(dic);
                break;
        }

        if (!DataManager.INSTANCE._isNoneLoginVersion)
        {
            reference.UpdateChildrenAsync(dic);
        }

        checkUI.SetActive(false);
    }

    void UpdatePass(int serialNumber, bool isGold, GameObject checkUI)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child("data");
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();

        if (isGold)
            dic.Add("/_gold", _userData._gold);
        else
            dic.Add("/_pearl", _userData._pearl);

        switch (serialNumber)
        {
            case (int)PublicDefined.ePassType.jeongdongjin:
                //Debug.Log("jeongdongjin 패스 구입");
                _userData._haveJeongdongjinPass = true;
                if (Application.platform != RuntimePlatform.WindowsEditor)
                {
                    dic.Add("/_haveJeongdongjinPass", true);
                    reference.UpdateChildrenAsync(dic);
                }

                break;
            case (int)PublicDefined.ePassType.skyway:
                //Debug.Log("skyway 패스 구입");
                _userData._haveSkywayPass = true;
                if (Application.platform != RuntimePlatform.WindowsEditor)
                {
                    dic.Add("/_haveSkywayPass", true);
                    reference.UpdateChildrenAsync(dic);
                }

                break;
            case (int)PublicDefined.ePassType.homerspit:
                //Debug.Log("homerspit 패스 구입");
                _userData._haveHomerspitPass = true;
                if (Application.platform != RuntimePlatform.WindowsEditor)
                {
                    dic.Add("/_haveHomerspitPass", true);
                    reference.UpdateChildrenAsync(dic);
                }

                break;
        }
        checkUI.SetActive(false);
    }

    public void UpdateEquipment()
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString);

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic = _userData.GetCurrentEquipmentDictionary2(); 

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference.Child(_editorFirebaseUid).Child(_dataString).Child("equipment").UpdateChildrenAsync(dic);

        }
        else
        {
            reference.Child(_userData.GetUId()).Child(_dataString).Child("equipment").UpdateChildrenAsync(dic);
        }
    }

    // 랜덤된 미끼를 선택한다. 박스 열리는 연출은 아직이다.
    void RandomBait(int number, GameObject checkUI, int quantity = 1)
    {
        List<int> serialList = new List<int>();
        Dictionary<int, int> randomDic = new Dictionary<int, int>();
        switch(number)
        {
            // jeongdongjin 미끼 박스
            case 0:
                serialList = ItemData.Instance._jeongdongjinBaitSerialNumberList;
                randomDic = GetRandomNumberList(serialList, quantity);
                if (_userData._currentJeongdongjinPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl))
                {
                    PassManager.INSTANCE.CheckClearPassAboutAction_Jeongdongjin();
                }
                break;
            // skyway 미끼 박스
            case 1:
                serialList = ItemData.Instance._skywayBaitSerialNumberList;
                randomDic = GetRandomNumberList(serialList, quantity);
                break;
            // homerspit 미끼 박스
            case 2:
                serialList = ItemData.Instance._homerspitBaitSerialNumberList;
                randomDic = GetRandomNumberList(serialList, quantity);
                if (_userData._currentHomerspitPassIndex.Equals((int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl))
                {
                    PassManager.INSTANCE.CheckClearPassAboutAction_Homerspit();
                }
                break;
        }

        OpenBait(randomDic, checkUI);
    }

    Dictionary<int, int> GetRandomNumberList(List<int> serialList, int quantity)
    {
        int count = 0;
        int limit = 10 * quantity;

        Dictionary<int, int> dic = new Dictionary<int, int>();

        while(count < limit)
        {
            int randomNumber = serialList[Random.Range(0, serialList.Count)];

            if (dic.ContainsKey(randomNumber))
                dic[randomNumber]++;
            else
                dic.Add(randomNumber, 1);

            count++;
        }

        return dic;
    }

    void OpenBait(Dictionary<int, int> randomDic, GameObject checkUI)
    {
        Dictionary<int, int> baitDic = _userData.GetBaitDictionary();
        Dictionary<int, int> pastebaitDic = _userData.GetPasteBaitDictionary();

        // 파이어베이스 업데이트 할 Dictionary
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        int totalCount = 0;

        // 상자에서 나온 10개의 미끼들 UserData에 업데이트하고 나열한다.
        foreach(KeyValuePair<int, int> data in randomDic)
        {
            // 미끼
            if(data.Key < 3000)
            {
                //string test;
                int index = data.Key - 2000;
                //test = "전 : " + baitDic[index];
                baitDic[index] += data.Value;
                //test += " // 후 : " + baitDic[index];
                updateDic.Add("/bait/" + index.ToString(), baitDic[index]);
                //Debug.Log(ItemData.Instance.baitItemDB[index].korName + " " + test);
            }
            // 떡밥
            else
            {
                //string test;
                int index = data.Key - 3000;
                //test = "전 : " + pastebaitDic[index];
                pastebaitDic[index] += data.Value;
                //test += " // 후 : " + pastebaitDic[index];
                updateDic.Add("/pastebait/" + index.ToString(), pastebaitDic[index]);
                //Debug.Log(ItemData.Instance.pasetbaitItemDB[index].korName + " " + test);
            }
            totalCount += data.Value;
        }
        //Debug.Log("총 개수: " + totalCount);
        updateDic.Add("/_gold", _userData._gold);

        if (!DataManager.INSTANCE._isNoneLoginVersion)
        {
            UpdateFirebase(updateDic);
        }

        checkUI.SetActive(false);
        Shop.INSTANCE.RandomBaitUIOn(randomDic);
    }

    public void UpdateBaitData(bool isPaste, Dictionary<string, object> updateBaitDic)
    {
        if(isPaste)
        {
            if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
            {
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString).Child("pastebait");
                reference.UpdateChildrenAsync(updateBaitDic);
            }
            else
            {
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString).Child("pastebait");
                reference.UpdateChildrenAsync(updateBaitDic);
            }
        }
        else
        {
            if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
            {
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid)
                        .Child(_dataString).Child("bait");
                reference.UpdateChildrenAsync(updateBaitDic);
            }
            else
            {
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId())
                         .Child(_dataString).Child("bait");
                reference.UpdateChildrenAsync(updateBaitDic);
            }
        }
    }

    public void UpdatePearl()
    {
        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("_pearl", _userData._pearl);
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
            reference.UpdateChildrenAsync(dic);
        }
        else
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("_pearl", _userData._pearl);
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
            reference.UpdateChildrenAsync(dic);
        }
    }

    public void UpdateRewardState(Dictionary<string, object> updateDic)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        }

        reference.UpdateChildrenAsync(updateDic);
    }

    public void UpdateFirebase(Dictionary<string, object> updateDic)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
            reference.UpdateChildrenAsync(updateDic);
        }
        else
        {
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
            reference.UpdateChildrenAsync(updateDic);
        }
    }
    public void SetRawJsonFirebase(string key, string value)
    {
        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
            reference.Child(key).SetRawJsonValueAsync(value);
        }
        else
        {
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
            reference.Child(key).SetRawJsonValueAsync(value);
        }
    }

    public void UpdateRepresentFish()
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;

        if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                Child(_dataString).Child("representFish");
        }
        else
        {
            reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                Child(_dataString).Child("representFish");
        }

        string json = JsonUtility.ToJson(_userData.GetRepresentFish());

        reference.SetRawJsonValueAsync(json);
    }

    // 물고기 수족관에 추가
    public string AddAquariumFish(int number, PublicDefined.stFishInfo fishInfo)
    {
        DatabaseReference reference;
        switch (number)
        {
            case 0:
                // fSzMR6B2rmd479jtMQdyq1gxnps1
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("first");
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("first");
                }
                break;
            case 1:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("second");
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("second");
                }
                break;
            case 2:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("third");
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("third");
                }
                break;
            case 3:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fourth");
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fourth");
                }
                break;
            default:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fifth");
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fifth");
                }
                break;
        }
        string key;
        if (fishInfo._key.Equals(string.Empty))
        {
            key = reference.Push().Key;
            fishInfo._key = key;
        }
        else
        {
            key = fishInfo._key;
        }

        string json = JsonUtility.ToJson(fishInfo);

        //if(Application.platform != RuntimePlatform.WindowsEditor)

        if (!DataManager.INSTANCE._isNoneLoginVersion)
            reference.Child(key).SetRawJsonValueAsync(json);

        return key;
    }

    // 물고기 수족관에서 옮기기
    public void ShiftFish(int current, int after, PublicDefined.stFishInfo stFishInfo)
    {
        DatabaseReference reference;
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(stFishInfo._key, null);
        string json = JsonUtility.ToJson(dic);
        switch (current)
        {
            case 0:
                // fSzMR6B2rmd479jtMQdyq1gxnps1
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("first").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("first").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 1:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("second").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("second").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 2:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("third").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("third").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 3:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fourth").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fourth").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            default:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fifth").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fifth").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
        }
        //if(Application.platform != RuntimePlatform.WindowsEditor)
        reference.RemoveValueAsync();
        //reference.UpdateChildrenAsync(dic);
        //reference.SetRawJsonValueAsync(json);
        AddAquariumFish(after, stFishInfo);
    }

    public void SellFish(int AquariumNumber, PublicDefined.stFishInfo stFishInfo)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("_gold", _userData._gold);
        reference.UpdateChildrenAsync(dic);

        switch (AquariumNumber)
        {
            case 0:
                // fSzMR6B2rmd479jtMQdyq1gxnps1
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("first").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("first").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 1:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("second").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("second").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 2:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("third").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("third").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 3:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fourth").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fourth").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
            case 4:
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).
                        Child(_dataString).Child(_aquariumString).Child("fifth").Child(stFishInfo._key);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).
                        Child(_dataString).Child(_aquariumString).Child("fifth").Child(stFishInfo._key);
                    //Debug.Log(_userData.GetUId());
                }
                break;
        }
        reference.RemoveValueAsync();
        //reference.UpdateChildrenAsync(dic);
        //reference.SetRawJsonValueAsync(json);
    }

    public void UpdatePassAboutFish(PublicDefined.eMapType mapType)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;
        switch (mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutFish_jeongdongjinString).Child(_userData._currentJeongdongjinPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfJeongdongjinPassAboutFish()[_userData._currentJeongdongjinPassIndex]);
                    //Debug.Log(json);
                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutFish_jeongdongjinString).Child(_userData._currentJeongdongjinPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfJeongdongjinPassAboutFish()[_userData._currentJeongdongjinPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
            case PublicDefined.eMapType.skyway:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutFish_skywayString).Child(_userData._currentSkywayPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfSkywayPassAboutFish()[_userData._currentSkywayPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutFish_skywayString).Child(_userData._currentSkywayPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfSkywayPassAboutFish()[_userData._currentSkywayPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
            case PublicDefined.eMapType.homerspit:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutFish_homerspitString).Child(_userData._currentHomerspitPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfHomerspitPassAboutFish()[_userData._currentHomerspitPassIndex]);
                    //Debug.Log(json);
                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutFish_homerspitString).Child(_userData._currentHomerspitPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfHomerspitPassAboutFish()[_userData._currentHomerspitPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
        }
    }
    public void UpdatePassAboutAction(PublicDefined.eMapType mapType)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;
        switch (mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutAction_jeongdongjinString).Child(_userData._currentJeongdongjinPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfJeongdongjinPassAboutAction()[_userData._currentJeongdongjinPassIndex]);

                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutAction_jeongdongjinString).Child(_userData._currentJeongdongjinPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfJeongdongjinPassAboutAction()[_userData._currentJeongdongjinPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
            case PublicDefined.eMapType.skyway:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutAction_skywayString).Child(_userData._currentSkywayPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfSkywayPassAboutAction()[_userData._currentSkywayPassIndex]);

                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutAction_skywayString).Child(_userData._currentSkywayPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfSkywayPassAboutAction()[_userData._currentSkywayPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
            case PublicDefined.eMapType.homerspit:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString)
                        .Child(_passAboutAction_homerspitString).Child(_userData._currentHomerspitPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfHomerspitPassAboutAction()[_userData._currentHomerspitPassIndex]);

                    reference.SetRawJsonValueAsync(json);
                }
                else
                {
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString)
                        .Child(_passAboutAction_homerspitString).Child(_userData._currentHomerspitPassIndex.ToString());
                    string json = JsonConvert.SerializeObject(_userData.GetCurrentStateOfHomerspitPassAboutAction()[_userData._currentHomerspitPassIndex]);
                    reference.SetRawJsonValueAsync(json);
                }
                break;
        }
    }
    public void UpdatePassIndex(PublicDefined.eMapType mapType)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;
        switch (mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_jeongdongjinString, _userData._currentJeongdongjinPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_jeongdongjinString, _userData._currentJeongdongjinPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                break;
            case PublicDefined.eMapType.skyway:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_skywayString, _userData._currentSkywayPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_skywayString, _userData._currentSkywayPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                break;
            case PublicDefined.eMapType.homerspit:
                if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_homerspitString, _userData._currentHomerspitPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                else
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add(_currentPassIndex_homerspitString, _userData._currentHomerspitPassIndex);
                    reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
                    reference.UpdateChildrenAsync(dic);
                }
                break;
        }
    }
    public void BuyPackage(int type) // 플래티넘, 다이아몬드
    {
        DatabaseReference reference;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (type.Equals(0))
            {
                _userData._havePlatinumPackage = true;
                _userData._gold += 33000;

                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("_havePlatinumPackage", true);
                updateDic.Add("_gold", _userData._gold);
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
                reference.UpdateChildrenAsync(updateDic);

                string json = JsonConvert.SerializeObject(_userData.GetPlatinumPackage());
                reference.Child(_platinumPackage).SetRawJsonValueAsync(json);
            }
            else
            {
                _userData._haveDiamondPackage = true;
                _userData._gold += 55000;

                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("_haveDiamondPackage", true);
                updateDic.Add("_gold", _userData._gold);
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString);
                reference.UpdateChildrenAsync(updateDic);

                string json = JsonConvert.SerializeObject(_userData.GetDiamondPackage());
                reference.Child(_diamondPackage).SetRawJsonValueAsync(json);
            }
        }
        else
        {
            if (type.Equals(0))
            {
                _userData._havePlatinumPackage = true;
                _userData._gold += 33000;

                if (DataManager.INSTANCE._isNoneLoginVersion)
                    return;

                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("_havePlatinumPackage", true);
                updateDic.Add("_gold", _userData._gold);
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
                reference.UpdateChildrenAsync(updateDic);

                string json = JsonConvert.SerializeObject(_userData.GetPlatinumPackage());
                reference.Child(_platinumPackage).SetRawJsonValueAsync(json);
            }
            else
            {
                _userData._haveDiamondPackage = true;
                _userData._gold += 55000;

                if (DataManager.INSTANCE._isNoneLoginVersion)
                    return;

                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("_haveDiamondPackage", true);
                updateDic.Add("_gold", _userData._gold);
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString);
                reference.UpdateChildrenAsync(updateDic);

                string json = JsonConvert.SerializeObject(_userData.GetDiamondPackage());
                reference.Child(_diamondPackage).SetRawJsonValueAsync(json);
            }
        }
    }

    public void UpdatePackage(int type, string today) // 플래티넘, 다이아몬드
    {
        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        updateDic.Add("/_gold", _userData._gold);

        _sb.Length = 0;
        _sb.Append("/");

        if (type.Equals(0))
        {
            _sb.Append(_platinumPackage);
        }
        else
        {
            _sb.Append(_diamondPackage);
        }
        _sb.Append("/");
        _sb.Append(today);
        updateDic.Add(_sb.ToString(), true);
        UpdateFirebase(updateDic);
    }
    public void DeletePackage(int type)
    {
        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DatabaseReference reference;
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (type.Equals(0))
            {
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString).Child(_platinumPackage);
                updateDic.Add("/_havePlatinumPackage", false);
            }
            else
            {
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_editorFirebaseUid).Child(_dataString).Child(_diamondPackage);
                updateDic.Add("/_haveDiamondPackage", false);
            }
                
        }
        else
        {
            if (type.Equals(0))
            {
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString).Child(_platinumPackage);
                updateDic.Add("/_havePlatinumPackage", false);
            }
            else
            {
                reference = FirebaseDatabase.DefaultInstance.GetReference(_userinfoString).Child(_userData.GetUId()).Child(_dataString).Child(_diamondPackage);
                updateDic.Add("/_haveDiamondPackage", false);
            }
        }

        UpdateFirebase(updateDic);
        reference.RemoveValueAsync();
    }
}
