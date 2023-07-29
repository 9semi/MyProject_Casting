using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

public class FishPopUp : MonoBehaviour
{
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);
    readonly Color _alphaColor = new Color(1, 1, 1, 0);
    readonly int _flopHash = Animator.StringToHash("isFlop");
    readonly WaitForSeconds _textDelay = new WaitForSeconds(0.1f);

    [Header("0 : 이름, 무게, 뱃지 /  1: 물고기 정보")]
    public GameObject[] fishInfoPanel;

    [Header("등급 뱃지 오브젝트(이미지) 0 : 잡어, 1 : 일반, 2 : 희귀")]
    public GameObject[] gradeBadges;

    [Header("새로운 뱃지 오브젝트(이미지)")]
    public GameObject newBadge;

    [Header("신기록 뱃지 오브젝트(이미지)")]
    public GameObject recordBadge;

    [Header("판매 팝업(이미지)")]
    public GameObject sellPopup;

    [Header("물고기 이름 텍스트")]
    public Text fishName;

    [Header("물고기 사이즈 텍스트")]
    public Text sizeTxt;
    public Text _sizeMeasureText;
    float _length;

    [Header("물고기 무게 텍스트")]
    public Text weightTxt;
    public Text _weightMeasureText;
    float _weight;

    [Header("물고기 가격 텍스트")]
    public Text priceTxt;
    int _price = 0;

    [Header("물고기 정보 4줄")]
    public Text[] infoTxt;

    [Header("낚시하는 캐릭터")]
    public CharacterManager characterMgr;

    [Header("물고기 연동 클래스")]
    public FishControl fishControl;

    [Header("팔기, 보관 버튼")]
    public Button _sellButton;
    public Button _saveButton;
    public GameObject _commonModeButton;
    public GameObject _matchModeButton;

    // 획득이나 방생이후에 리셋
    GameManager _gameManager;
    InGameUIManager _ingameUIManager;

    [Header("수족관 관련")]
    public Sprite _blueButtonSprite;
    public Sprite _redButtonSprite;
    public Sprite _grayButtonSprite;
    public GameObject _aquariumUI;
    public GameObject _aquariumFullUI;
    public Image[] _aquariumButtonImages;
    public Button[] _aquariumButtons;
    public Text[] _aquariumButtonCountTexts;
    public Text[] _aquariumButtonPossessTexts;
    public Button _fishTouchButton;
    public Sprite[] _bgSprites;
    public IngameBuyAquariumUI _buyUI;
    [SerializeField] Transform _catchFishPos;

    [Header("매치 관련")]
    public MatchManager _matchManager;

    List<int> _aquariumCountList;
    UserData _userData;
    bool _STOP = true;
    // 수족관에 넣을 물고기인지 대비하자.
    PublicDefined.stFishInfo _fishInfo;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
    }

    private void Start()
    {
        _userData = DBManager.INSTANCE.GetUserData();
    }

    private void OnEnable()
    {
        _commonModeButton.SetActive(false);
        _matchModeButton.SetActive(false);
    }

    public void SetUpInfo(PublicDefined.stFishInfo fishInfo, int DBNumber, string name, float size, float weight, int price, string[] info, PublicDefined.eFishType fishType, Animator fishAni) 
    {
        if(!DBNumber.Equals(28) && !DBNumber.Equals(9))
            fishControl.FishResisteAudioObject = AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishFlap, true);

        if(_gameManager == null)
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        }

        _gameManager.HideBait();
        _gameManager.IsPause = true;
        _fishInfo = fishInfo;
        fishAni.SetBool(_flopHash, true);
        
        if(!DataManager.INSTANCE._tutorialIsInProgress)
            CheckRank(new PublicDefined.stRankFishInfo(DBNumber, name, size, weight, fishType));

        switch (fishType)
        {
            case PublicDefined.eFishType.Sundry:
                fishName.color = _sundryColor;
                break;
            case PublicDefined.eFishType.Normal:
                fishName.color = _normalColor;
                break;
            case PublicDefined.eFishType.Rare:
                fishName.color = _rareColor;
                break;
            default:
                break;
        }
        // 물고기 이름 받아오기
        fishName.text = name;

        // 물고기 사이즈 받아오기
        sizeTxt.text = size.ToString("N2");

        // 물고기 무게 받아오기
        //Debug.Log(weight);
        if (weight < 1)
        {
            weight *= 1000;
            weightTxt.text = weight.ToString("N0");
            _weightMeasureText.text = "g";
        }
        else
        {
            weightTxt.text = weight.ToString("N2");
            _weightMeasureText.text = "kg";
        }
        //Debug.Log(weightTxt.color);
        // 가격 받아오기
        //priceTxt.text = price.ToString();
        _price = price;

        // 팝업 키기
        gameObject.SetActive(true);
        StartCoroutine(FishPopupCoroutine());

        _gameManager.IsPause = true;

        _gameManager.BluetoothReset();

        // 게이지 잠깐 끈다.
        _ingameUIManager.SetCharacterGage(false);
        _ingameUIManager.PassQuestOff();

        _commonModeButton.SetActive(true);
    }
    public void SetUpInfo_Match(PublicDefined.stFishInfo fishInfo, int DBNumber, string name, float size, float weight, int price, string[] info, PublicDefined.eFishType fishType, Animator fishAni)
    {
        if (!DBNumber.Equals(28) && !DBNumber.Equals(9))
            fishControl.FishResisteAudioObject = AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishFlap, true);

        _gameManager.HideBait();
        _gameManager.IsPause = true;
        _fishInfo = fishInfo;
        fishAni.SetBool(_flopHash, true);

        switch (fishType)
        {
            case PublicDefined.eFishType.Sundry:
                fishName.color = _sundryColor;
                break;
            case PublicDefined.eFishType.Normal:
                fishName.color = _normalColor;
                break;
            case PublicDefined.eFishType.Rare:
                fishName.color = _rareColor;
                break;
            default:
                break;
        }
        // 물고기 이름 받아오기
        fishName.text = name;

        // 물고기 사이즈 받아오기
        sizeTxt.text = size.ToString("N2");
        sizeTxt.color = Color.white;
        _sizeMeasureText.color = Color.white;
        _length = size;

        // 물고기 무게 받아오기
        if(weight < 1)
        {
            weightTxt.text = (weight * 1000).ToString("N0");
            _weightMeasureText.text = "g";
        }
        else
        {
            weightTxt.text = weight.ToString("N2");
            _weightMeasureText.text = "kg";
        }

        weightTxt.color = Color.white;
        _weightMeasureText.color = Color.white;
        _weight = weight;

        // 가격 받아오기
        priceTxt.text = price.ToString();
        _price = price;

        // 팝업 키기
        gameObject.SetActive(true);

        _gameManager.IsPause = true;

        _gameManager.BluetoothReset();

        // 게이지 잠깐 끈다.
        _ingameUIManager.SetCharacterGage(false);
        _ingameUIManager.PassQuestOff();

        _matchManager.UpdateMyScore(_length, _weight, 1, _price);
        _matchModeButton.SetActive(true);
    }

    public void ClickOKButton_Match()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        PlayBGM();

        // 게이지를 다시 킨다.
        _ingameUIManager.ResetCharacterGage();

        // 재화 들어오는거 추가
        VacataInfo();

        _ingameUIManager.ResetCharacterGage();
        _gameManager.ShowBait();

        if (fishControl.FishResisteAudioObject != null)
        {
            fishControl.FishResisteAudioObject.GetComponent<AudioSource>().loop = false;
            fishControl.FishResisteAudioObject.GetComponent<AudioPoolObject>().ReturnThis();
            fishControl.FishResisteAudioObject = null;
        }
        
        _gameManager.IsPause = false;

    }
    IEnumerator FishPopupCoroutine()
    {
        _sellButton.interactable = false;
        _saveButton.interactable = false;
        _sizeMeasureText.color = Color.white;
        _weightMeasureText.color = Color.white;
        float timer = 0;
        while (timer <= 10)
        {
            sizeTxt.color = new Color(1, 1, 1, timer * 0.1f);
            
            timer += 2f;
            yield return _textDelay;
        }

        timer = 0; 
        while (timer <= 10)
        {
            weightTxt.color = new Color(1, 1, 1, timer * 0.1f);

            timer += 2f;
            yield return _textDelay;
        }

        yield return _textDelay;

        priceTxt.text = _price.ToString();

        yield return _textDelay;

        _sellButton.interactable = true;
        _saveButton.interactable = true;
    }
    // 랭크에 들어가는지 확인
    public void CheckRank(PublicDefined.stRankFishInfo fishInfo)
    {
        _userData = DBManager.INSTANCE.GetUserData();
        Dictionary<int, PublicDefined.stRankFishInfo> dic;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                dic = _userData.GetJeongdongjinRankDictionary();
                break;
            case PublicDefined.eMapType.skyway:
                dic = _userData.GetSkywayRankDictionary();
                break;
            case PublicDefined.eMapType.homerspit:
                dic = _userData.GetHomerspitRankDictionary();
                break;
            default:
                dic = new Dictionary<int, PublicDefined.stRankFishInfo>();
                break;
        }

        // 이미 해당 물고기의 랭킹이 등록되어 있다면 비교해서 큰 물고기로 갱신한다.
        if (dic.ContainsKey(fishInfo._fishNumber))
        {
            if (dic[fishInfo._fishNumber]._length < fishInfo._length)
            {
                //Debug.LogError("키 있음");
                dic[fishInfo._fishNumber] = fishInfo;
                SendFishRankToFirebase(fishInfo);
            }
        }
        else
        {
            //Debug.LogError("키 없음");
            dic.Add(fishInfo._fishNumber, fishInfo);
            SendFishRankToFirebase(fishInfo);
        }

    }
    public void SendFishRankToFirebase(PublicDefined.stRankFishInfo fishInfo)
    {
        string key = string.Empty;
        string json = JsonUtility.ToJson(fishInfo);

        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                key = "/jeongdongjinRank/" + fishInfo._fishNumber;
                break;
            case PublicDefined.eMapType.skyway:
                key = "/skywayRank/" + fishInfo._fishNumber;
                break;
            case PublicDefined.eMapType.homerspit:
                key = "/homerspitRank/" + fishInfo._fishNumber;
                break;
        }

        if (DataManager.INSTANCE._isNoneLoginVersion)
            return;

        DBManager.INSTANCE.SetRawJsonFirebase(key, json);
    }
    // 수족관 목록이 뜬다.
    public void AquariumOn(bool isOn)
    { // 여기서 버튼 색상과 마릿수도 설정한다.

        if(isOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        else
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

        _aquariumCountList = DBManager.INSTANCE.GetUserData().GetAquariumCountState();
        
        if (isOn)
        {
            for (int i = 0; i < _aquariumCountList.Count; i++)
            {

                _aquariumButtons[i].interactable = true;

                if (_aquariumCountList[i].Equals(-1))
                {
                    // 수족관 자체를 가지고 있지 않다.
                    _aquariumButtonImages[i].sprite = _redButtonSprite;
                    _aquariumButtonCountTexts[i].text = "구매하기";
                    _aquariumButtonPossessTexts[i].text = string.Empty;
                }
                else if(_aquariumCountList[i] < 30)
                {
                    _aquariumButtonImages[i].sprite = _blueButtonSprite;
                    _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                    _aquariumButtonPossessTexts[i].text = "보유 중";
                }
                else
                {
                    _aquariumButtons[i].interactable = false;
                    _aquariumButtonImages[i].sprite = _grayButtonSprite;
                    _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                    _aquariumButtonPossessTexts[i].text = "보유 중";
                }
            }
        }
        _aquariumUI.SetActive(isOn);
    }
    //-------------
    #region 수족관 물고기 저장
    public void ResetOption()
    {
        // 게이지를 다시 킨다.
        _ingameUIManager.ResetCharacterGage();
        _ingameUIManager.SetBackState();
        _ingameUIManager.ShowButtons();
        _gameManager.cameraMgr.ResetCamera();
        characterMgr.ResetPos();

        // 재화 들어오는거 추가
        VacataInfo();
        
        // 파닥거리는 소리 반복 끔
        if (fishControl.FishResisteAudioObject != null)
        {
            fishControl.FishResisteAudioObject.GetComponent<AudioSource>().loop = false;
            fishControl.FishResisteAudioObject.GetComponent<AudioPoolObject>().ReturnThis();
            fishControl.FishResisteAudioObject = null;
        }
        sellPopup.SetActive(false);
        _gameManager.IsPause = false;
        _ingameUIManager.ShowButtons();
    }
    public void UpdateAquariumState()
    {
        _aquariumCountList = DBManager.INSTANCE.GetUserData().GetAquariumCountState();

        for (int i = 0; i < _aquariumCountList.Count; i++)
        {
            _aquariumButtons[i].interactable = true;

            if (_aquariumCountList[i].Equals(-1))
            {
                // 수족관 자체를 가지고 있지 않다.
                _aquariumButtonImages[i].sprite = _redButtonSprite;
                _aquariumButtonCountTexts[i].text = "구매하기";
                _aquariumButtonPossessTexts[i].text = string.Empty;
            }
            else if (_aquariumCountList[i] < 30)
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
            }
            else
            {
                _aquariumButtons[i].interactable = false;
                _aquariumButtonImages[i].sprite = _grayButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
            }
        }

    }
    public void ClickFishSave(int aquariumNum)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if (_aquariumCountList[aquariumNum].Equals(-1))
        {
            // 수족관을 보유하고 있지 않아서 구매하기 버튼을 누른 사람들이 들어온다.
            switch (aquariumNum)
            {
                case 1:
                    _buyUI.Init(false, 80000, aquariumNum, _bgSprites[aquariumNum], _userData, this);
                    break;
                case 2:
                    _buyUI.Init(false, 150000, aquariumNum, _bgSprites[aquariumNum], _userData,this);
                    break;
                case 3:
                    _buyUI.Init(true, 7900, aquariumNum, _bgSprites[aquariumNum], _userData,this);
                    break;
                case 4:
                    _buyUI.Init(true, 14900, aquariumNum, _bgSprites[aquariumNum], _userData,this);
                    break;
            }

            return;
        }

        if(_aquariumCountList[aquariumNum] >= 30)
        {
            // 수족관이 꽉 찼으면 들어온다.
            _aquariumFullUI.SetActive(true);
            StartCoroutine(MakeDelay(2, () => _aquariumFullUI.SetActive(false)));
            return;
        }

        _userData.UpdateCountStateList(true, aquariumNum);

        // 패스 퀘스트 확인
        int index = 0;
        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                index = _userData._currentJeongdongjinPassIndex;
                break;
            case PublicDefined.eMapType.skyway:
                index = _userData._currentSkywayPassIndex;
                break;
            case PublicDefined.eMapType.homerspit:
                index = _userData._currentHomerspitPassIndex;
                break;
        }

        //Debug.Log("현재 패스 인덱스 : " + index);
        if (index.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl)|| index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl)
            || index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl) || index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl)
            || index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl) || index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl)
            || index.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl))
        {
            PassManager.INSTANCE.ToPassManagerAboutAction();
        }

        // 패스 퀘스트 확인
        if (index.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl) && fishControl.GetStructFishData()._gradeType.Equals(PublicDefined.eFishType.Rare)
            /*&& _fishInfo._fishType.Equals((int)PublicDefined.eFishType.Rare)*/)
        {
            PassManager.INSTANCE.ToPassManagerAboutAction();
        }

        // 패스 확인
        PassManager.INSTANCE.ToPassManagerAboutFish(_fishInfo);

        // DB에 추가
         string key = DBManager.INSTANCE.AddAquariumFish(aquariumNum, _fishInfo);

        //string key = " ";
        // 받아온 키값 저장
        _fishInfo._key = key;
        
        // 딕셔너리에 저장
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = DBManager.INSTANCE.GetUserData().SelectAquariumDictionary(aquariumNum);

        if (dic.ContainsKey(_fishInfo._fishNumber))
        {
            // 만약 같은 번호의 물고기가 있다면
            dic[_fishInfo._fishNumber].Add(_fishInfo);
        }
        else
        {
            // 만약 같은 번호의 물고기가 없다면 최초의 물고기를 등록한다.
            List<PublicDefined.stFishInfo> tempList = new List<PublicDefined.stFishInfo>();
            tempList.Add(_fishInfo);
            dic.Add(_fishInfo._fishNumber, tempList);
        }

        ResetOption();

        _ingameUIManager.ResetCharacterGage();
        _gameManager.ShowBait();
        _gameManager.IsPause = false;
        PlayBGM();
        _aquariumUI.SetActive(false);
    }
    #endregion

    public void ConfirmClick()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.coin).GetComponent<AudioPoolObject>().Init();
        PlayBGM();

        _userData._gold += _price;
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("/_gold/", _userData._gold);
        DBManager.INSTANCE.UpdateFirebase(dic);

        // 게이지를 다시 킨다.
        _ingameUIManager.ResetCharacterGage();
        _ingameUIManager.SetBackState();
        characterMgr.ResetPos();

        // 재화 들어오는거 추가
        VacataInfo();
        
        _gameManager.ShowBait();

        if (fishControl.FishResisteAudioObject != null)
        {
            fishControl.FishResisteAudioObject.GetComponent<AudioSource>().loop = false;
            fishControl.FishResisteAudioObject.GetComponent<AudioPoolObject>().ReturnThis();
            fishControl.FishResisteAudioObject = null;
        }
        
        sellPopup.SetActive(false);
        _gameManager.IsPause = false;
        _ingameUIManager.ShowButtons();
        _gameManager.cameraMgr.ResetCamera();

        // 패스 확인
        PassManager.INSTANCE.ToPassManagerAboutFish(_fishInfo);
    }

    public void VacataInfo()
    {
        if(fishControl.FishMoveToNeedleCoroutine != null)
        {
            fishControl.StopCoroutine(fishControl.FishMoveToNeedleCoroutine);
            fishControl.FishMoveToNeedleCoroutine = null;
        }

        fishName.text = null;
        sizeTxt.text = null;
        sizeTxt.color = _alphaColor;
        weightTxt.text = null;
        weightTxt.color = _alphaColor;
        priceTxt.text = string.Empty;

        if(DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin))
        {
            //_gameManager._jeongdongjinFishManager._caughtFishs.Add(fishControl.GetStructFishData()._fishObject.GetComponent<Fish>());
            _gameManager._jeongdongjinFishManager.AddCaughtFishList(fishControl.GetStructFishData()._fishObject.GetComponent<Fish>());
        }
        else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.skyway))
        {
            _gameManager._skywayFishManager._caughtFishs.Add(fishControl.GetStructFishData()._fishObject.GetComponent<FishSkyway>());
        }
        else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
        {
            _gameManager._homerspitFishManager._caughtFishs.Add(fishControl.GetStructFishData()._fishObject.GetComponent<FishHomerspit>());
        }

        fishControl.GetStructFishData()._fishTransform.SetParent(_gameManager._catchPos);
        fishControl.GetStructFishData()._fishTransform.localPosition = Vector3.zero;
        fishControl.GetStructFishData()._fishObject.SetActive(false);
        fishControl.FishTransform = null;
        fishControl.IsFind = false;
        characterMgr.ResetPos();
        gameObject.SetActive(false);
    }

    public void SellClick(bool isSet)
    {
        //SoundManager.instance.EffectPlay("UIClick");
        if(isSet)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        else
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

        sellPopup.SetActive(isSet);
    }
    public void CloseBtn()
    {
        //SoundManager.instance.EffectPlay("CashRegister");
        if (fishControl.FishResisteAudioObject != null)
        {
            fishControl.FishResisteAudioObject.GetComponent<AudioSource>().loop = false;
            fishControl.FishResisteAudioObject.GetComponent<AudioPoolObject>().ReturnThis();
            fishControl.FishResisteAudioObject = null;
        }
        fishInfoPanel[1].SetActive(false);
        fishInfoPanel[0].SetActive(true);
    }

    public void PlayBGM()
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

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
        switch (delayNumber)
        {
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