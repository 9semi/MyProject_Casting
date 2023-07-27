using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookAndSeasonPass : MonoBehaviour
{
    // 버튼 색상 translucence
    readonly Color _bookButtonColor = new Color(0.8f, 0.8f, 1f, 1);
    readonly Color _passButtonColor = new Color(0.7f, 0.7f, 0.7f, 1);
    readonly Color _translucenceBookButtonColor = new Color(0.8f, 0.8f, 1f, 0.4f);
    readonly Color _translucencePassButtonColor = new Color(0.7f, 0.7f, 0.7f, 0.4f);

    // 파이어베이스에 보낼 string
    readonly string _jeongdongjinFreeReward = "jeongdongjinPassFreeRewardState";
    readonly string _skywayFreeReward = "skywayPassFreeRewardState";
    readonly string _homerspitFreeReward = "homerspitPassFreeRewardState";
    readonly string _jeongdongjinPreReward = "jeongdongjinPassPremiumRewardState";
    readonly string _skywayPreReward = "skywayPassPremiumRewardState";
    readonly string _homerspitPreReward = "homerspitPassPremiumRewardState";

    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    [Header("도감 관련")]
    [Header("도감과 패스 오브젝트")]
    [SerializeField] GameObject _bookObject, _passObject;
    [Header("도감과 패스 버튼")]
    [SerializeField] Image _bookButtonImage;
    [SerializeField] Image _passButtonImage;
    [Header("물고기 오브젝트")]
    [SerializeField] GameObject[] fishes;
    [SerializeField] Sprite[] fishSprite;
    [Header("물고기 이미지 버튼")]
    [SerializeField] Transform _bookSlotContent;
    BookSlot[] _bookSlots; public BookSlot[] BookSlots { get { return _bookSlots; } }

    [Header("패스 관련")]
    [SerializeField] Transform[] _questButtons;
    [SerializeField] Button[] _freeButtons;
    [SerializeField] Button[] _premiumButtons;
    [SerializeField] Image[] _panels;
    [SerializeField] PassGetRewardUI _getRewardUI;
    [SerializeField] GameObject _getAllRewardUI;
    [SerializeField] PassBuyUI _buyPassUI;

    [Header("패스 버튼 스프라이트")]
    [SerializeField] Sprite _grayButtonSprite;
    [SerializeField] Sprite _greenButtonSprite;
    [SerializeField] Sprite _blueButtonSprite;

    [Header("체크 이미지")]
    [SerializeField] GameObject[] _check_green;
    [SerializeField] GameObject[] _check_yellow;

    [Header("미니맵")]
    [SerializeField] MiniMap _minimap;

    // 패스 미션 정보들을 가지고 있다.
    Pass _pass;
    // 미끼 정보 가지고 오자.
    List<Item> _baitList;
    // 유저 정보 가지고 있어야 편할 듯
    UserData _userData;

    GameObject _plzCheckInGameUI;
    int _mapType_Lobby; public int MapType_Lobby { set { _mapType_Lobby = value; } }

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>().GetSeasonpoassInstance(this);

        _userData = DBManager.INSTANCE.GetUserData();
        _pass = _passObject.GetComponent<Pass>();
        _baitList = ItemData.Instance.baitItemDB;
        _bookSlots = new BookSlot[_bookSlotContent.childCount];

        for(int i = 0; i < _bookSlots.Length; i++)
        {
            _bookSlots[i] = _bookSlotContent.GetChild(i).GetComponent<BookSlot>();
        }
    }
    
    private void OnEnable()
    {
        if(DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.lobby))
        {
            _minimap.CurrentNumber = -1;
            _minimap.CurrentFilterNumber = -1;
            _minimap.IsFiltering = false;
            InitBook_Lobby();
            InitPassButtons_Lobby();
            ClickBookSeasonPassButton(0);
        }
        else
        {
            _minimap.CurrentNumber = -1;
            _minimap.CurrentFilterNumber = -1;
            _minimap.IsFiltering = false;
            InitBook();
            InitPassButtons();
            ClickBookSeasonPassButton(0);
        }
    }

    public void ClickBookSeasonPassButton(int buttonNumber)
    {
        //PlayClickEffectAudio();

        switch (buttonNumber)
        {
            case 0:
                _bookButtonImage.color = _bookButtonColor;
                _passButtonImage.color = _translucencePassButtonColor;
                PlayClickEffectAudio();
                OpenBook();
                break;
            case 1:
                if(DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.lobby))
                {
                    PlayClickEffectAudio();
                    _plzCheckInGameUI.SetActive(true);
                }
                else
                {
                    _bookButtonImage.color = _translucenceBookButtonColor;
                    _passButtonImage.color = _passButtonColor;
                    PlayClickEffectAudio();
                    OpenPass();
                }
                break;
            case 2:
                _bookButtonImage.color = _bookButtonColor;
                _passButtonImage.color = _translucencePassButtonColor;
                OpenBook();
                break;
        }
    }
    public void InitBook()
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
            case PublicDefined.eMapType.tutorial:
                Dictionary<int, PublicDefined.stRankFishInfo> jeongdongjinDic = _userData.GetJeongdongjinRankDictionary();

                // 도감 슬롯 갯수만큼 반복 
                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    // 전역변수로 저장한 해당 맵 물고기 이미지들 삽입
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    // 슬롯 jeongdongjin 물고기 이름을 프리펩에 있는 순서대로 가져옴 - 프리펩 순서대로 놔야함 가나다순
                    Fish fish = fishes[i].GetComponent<Fish>();

                    if (jeongdongjinDic.ContainsKey(fish.GetFishDBNum()))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.GetFishKoreanName();
                    _bookSlots[i].Type = fish.GetStructFishData()._gradeType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.GetStructFishData()._gradeType);
                    _bookSlots[i].gameObject.SetActive(true);

                    _bookSlots[i].SetInfoArray(new string[fish.GetFishInfoArray().Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.GetFishInfoArray().Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.GetFishInfoArray()[j];
                    }
                }
                break;
            case PublicDefined.eMapType.skyway:
                Dictionary<int, PublicDefined.stRankFishInfo> skywayDic = _userData.GetSkywayRankDictionary();

                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    FishSkyway fish = fishes[i].GetComponent<FishSkyway>();

                    if (skywayDic.ContainsKey(fish.fishDBNum))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.fishKoreanName;
                    _bookSlots[i].Type = fish.fishType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.fishType);
                    _bookSlots[i].gameObject.SetActive(true);
                    _bookSlots[i].SetInfoArray(new string[fish.info.Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.info[j];
                    }
                }
                break;
            case PublicDefined.eMapType.homerspit:
                Dictionary<int, PublicDefined.stRankFishInfo> homerspitDic = _userData.GetHomerspitRankDictionary();

                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    FishHomerspit fish = fishes[i].GetComponent<FishHomerspit>();

                    if (homerspitDic.ContainsKey(fish.fishDBNum))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.fishKoreanName;
                    _bookSlots[i].Type = fish.fishType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.fishType);
                    _bookSlots[i].gameObject.SetActive(true);

                    _bookSlots[i].SetInfoArray(new string[fish.info.Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.info[j];
                    }
                }
                break;
        }
    }
    public void InitBook_Lobby()
    {
        switch (_mapType_Lobby)
        {
            case 0:
                Dictionary<int, PublicDefined.stRankFishInfo> jeongdongjinDic = _userData.GetJeongdongjinRankDictionary();

                // 도감 슬롯 갯수만큼 반복 
                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    // 전역변수로 저장한 해당 맵 물고기 이미지들 삽입
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    // 슬롯 jeongdongjin 물고기 이름을 프리펩에 있는 순서대로 가져옴 - 프리펩 순서대로 놔야함 가나다순
                    Fish fish = fishes[i].GetComponent<Fish>();

                    if (jeongdongjinDic.ContainsKey(fish.GetFishDBNum()))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.GetFishKoreanName();
                    _bookSlots[i].Type = fish.GetStructFishData()._gradeType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.GetStructFishData()._gradeType);
                    _bookSlots[i].gameObject.SetActive(true);

                    _bookSlots[i].SetInfoArray(new string[fish.GetFishInfoArray().Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.GetFishInfoArray().Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.GetFishInfoArray()[j];
                    }
                }
                break;
            case 2:
                Dictionary<int, PublicDefined.stRankFishInfo> skywayDic = _userData.GetSkywayRankDictionary();

                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    FishSkyway fish = fishes[i].GetComponent<FishSkyway>();

                    if (skywayDic.ContainsKey(fish.fishDBNum))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.fishKoreanName;
                    _bookSlots[i].Type = fish.fishType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.fishType);
                    _bookSlots[i].gameObject.SetActive(true);
                    _bookSlots[i].SetInfoArray(new string[fish.info.Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.info[j];
                    }
                }
                break;
            case 1:
                Dictionary<int, PublicDefined.stRankFishInfo> homerspitDic = _userData.GetHomerspitRankDictionary();

                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    FishHomerspit fish = fishes[i].GetComponent<FishHomerspit>();

                    if (homerspitDic.ContainsKey(fish.fishDBNum))
                        _bookSlots[i].GetStarObject().SetActive(true);
                    else
                        _bookSlots[i].GetStarObject().SetActive(false);

                    _bookSlots[i].GetNameText().text = fish.fishKoreanName;
                    _bookSlots[i].Type = fish.fishType;
                    _bookSlots[i].GetNameText().color = GetColorAccordingToType(fish.fishType);
                    _bookSlots[i].gameObject.SetActive(true);

                    _bookSlots[i].SetInfoArray(new string[fish.info.Length]);
                    // 물고기 정보 갯수(4줄)만큼 반복
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                        _bookSlots[i].GetInfoArray()[j] = fish.info[j];
                    }
                }
                break;
        }
    }
    public void InitPassButtons()
    {
        if(_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        Dictionary<int, bool> freeRewardState = new Dictionary<int, bool>();
        Dictionary<int, bool> preRewardState = new Dictionary<int, bool>();

        int index = 0;
        bool havePass = false;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
            case PublicDefined.eMapType.tutorial:
                freeRewardState = _userData.GetCheckJeongdongjinPassFreeReward();
                preRewardState = _userData.GetCheckJeongdongjinPassPremiumReward();
                index = _userData._currentJeongdongjinPassIndex;
                havePass = _userData._haveJeongdongjinPass;
                break;
            case PublicDefined.eMapType.skyway:
                freeRewardState = _userData.GetCheckSkywayPassFreeReward();
                preRewardState = _userData.GetCheckSkywayPassPremiumReward();
                index = _userData._currentSkywayPassIndex;
                havePass = _userData._haveSkywayPass;
                break;
            case PublicDefined.eMapType.homerspit:
                freeRewardState = _userData.GetCheckHomerPassFreeReward();
                preRewardState = _userData.GetCheckHomerspitPassPremiumReward();
                index = _userData._currentHomerspitPassIndex;
                havePass = _userData._haveHomerspitPass;
                break;
        }

        for (int i = 0; i < _questButtons.Length; i++)
        {
            // 퀘스트 부분에 체크 표시(클리어: 초록, 진행 중: 노랑)
            if (i < index) // 클리어
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(true);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = false;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                if (freeRewardState[i])
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _freeButtons[i].interactable = false;
                }
                else
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _greenButtonSprite;
                    _freeButtons[i].interactable = true;
                }

                if (preRewardState[i] || !havePass)
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;
                }
                else
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _blueButtonSprite;
                    _premiumButtons[i].interactable = true;
                }

            }
            else if (i.Equals(index)) // 진행 중
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(true);
                _panels[i].enabled = false;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _premiumButtons[i].interactable = false;
            }
            else // 안 열려있다.
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = true;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                if (freeRewardState[i])
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _freeButtons[i].interactable = false;
                }

                if (preRewardState[i])
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;
                }
            }

            // 퀘스트 내용 넣기
            _questButtons[i].GetChild(0).GetComponent<Text>().text = _pass._passInfo[i]._questContent;

            // 보상 그림 넣기
            // 보상이 미끼라면 이 곳
            if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _baitList[_pass._passInfo[i]._freeBaitDBNumber].itemImage;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text =
                    _baitList[_pass._passInfo[i]._freeBaitDBNumber].korName + " " + _pass._passInfo[i]._freeBaitQuantity.ToString() + "개";
            }
            // 보상이 진주라면 이 곳
            else
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._freeGold.ToString() + "개";
            }

            _premiumButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
            _premiumButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._premiumGold.ToString() + "개";
        }
    }
    public void InitPassButtons_Lobby()
    {
        if (_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        Dictionary<int, bool> freeRewardState = new Dictionary<int, bool>();
        Dictionary<int, bool> preRewardState = new Dictionary<int, bool>();

        int index = 0;
        bool havePass = false;

        switch (_mapType_Lobby)
        {
            case 0:
                freeRewardState = _userData.GetCheckJeongdongjinPassFreeReward();
                preRewardState = _userData.GetCheckJeongdongjinPassPremiumReward();
                index = _userData._currentJeongdongjinPassIndex;
                havePass = _userData._haveJeongdongjinPass;
                break;
            case 2:
                freeRewardState = _userData.GetCheckSkywayPassFreeReward();
                preRewardState = _userData.GetCheckSkywayPassPremiumReward();
                index = _userData._currentSkywayPassIndex;
                havePass = _userData._haveSkywayPass;
                break;
            case 1:
                freeRewardState = _userData.GetCheckHomerPassFreeReward();
                preRewardState = _userData.GetCheckHomerspitPassPremiumReward();
                index = _userData._currentHomerspitPassIndex;
                havePass = _userData._haveHomerspitPass;
                break;
        }

        for (int i = 0; i < _questButtons.Length; i++)
        {
            // 퀘스트 부분에 체크 표시(클리어: 초록, 진행 중: 노랑)
            if (i < index) // 클리어
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(true);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = false;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                if (freeRewardState[i])
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _freeButtons[i].interactable = false;
                }
                else
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _greenButtonSprite;
                    _freeButtons[i].interactable = true;
                }

                if (preRewardState[i] || !havePass)
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;
                }
                else
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _blueButtonSprite;
                    _premiumButtons[i].interactable = true;
                }

            }
            else if (i.Equals(index)) // 진행 중
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(true);
                _panels[i].enabled = false;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _premiumButtons[i].interactable = false;
            }
            else // 안 열려있다.
            {
                //Debug.Log(i + "번째 freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = true;

                // 보상을 받은 버튼은 회색으로 바꾸고 막는다.
                if (freeRewardState[i])
                {
                    _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _freeButtons[i].interactable = false;
                }

                if (preRewardState[i])
                {
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;
                }
            }

            // 퀘스트 내용 넣기
            _questButtons[i].GetChild(0).GetComponent<Text>().text = _pass._passInfo[i]._questContent;

            // 보상 그림 넣기
            // 보상이 미끼라면 이 곳
            if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _baitList[_pass._passInfo[i]._freeBaitDBNumber].itemImage;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text =
                    _baitList[_pass._passInfo[i]._freeBaitDBNumber].korName + " " + _pass._passInfo[i]._freeBaitQuantity.ToString() + "개";
            }
            // 보상이 진주라면 이 곳
            else
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._freeGold.ToString() + "개";
            }

            _premiumButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
            _premiumButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._premiumGold.ToString() + "개";
        }
    }
    void OpenPass()
    {
        // 시즌패스 컨텐츠 활성화
        _passObject.SetActive(true);
        _bookObject.SetActive(false);
    }
    void OpenBook()
    {
        // 도감 컨텐츠 활성화
        _bookObject.SetActive(true);
        _passObject.SetActive(false);
    }
    Color GetColorAccordingToType(PublicDefined.eFishType type)
    {
        switch(type)
        {
            case PublicDefined.eFishType.Sundry:
                return _sundryColor;
            case PublicDefined.eFishType.Normal:
                return _normalColor;
            case PublicDefined.eFishType.Rare:
                return _rareColor;
            default:
                return Color.white;
        }
    }
    public void ClickFreeButton(int number)
    {
        PlayClickEffectAudio();

        if(_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        if(_pass == null)
            _pass = _passObject.GetComponent<Pass>();

        if (_baitList == null)
        {
            _baitList = ItemData.Instance.baitItemDB;
        }

        _freeButtons[number].transform.GetComponent<Image>().sprite = _grayButtonSprite;
        _freeButtons[number].interactable = false;

        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        if (_pass._passInfo[number]._type.Equals(PassInfo.eFreeRewardType.Bait))
        {
            int baitDBNumber = _pass._passInfo[number]._freeBaitDBNumber;
            int baitQuantity = _pass._passInfo[number]._freeBaitQuantity;

            // UI 띄우기
            if(gameObject.activeSelf)
                _getRewardUI.Init(_baitList[_pass._passInfo[number]._freeBaitDBNumber].itemImage, _baitList[baitDBNumber].korName + " " + baitQuantity + "개");

            // 정보 갱신
            // 딕셔너리 갱신
            Dictionary<int, int> baitDic = _userData.GetBaitDictionary();
            baitDic[baitDBNumber] += baitQuantity;

            // 파이어베이스 갱신
            updateDic.Add("/bait/" + baitDBNumber.ToString(), baitDic[baitDBNumber]);
           // Debug.Log(number + "번째 Free 보상으로 " + ItemData.Instance.baitItemDB[_pass._passInfo[number]._freeBaitDBNumber]);
        }
        else
        {
            // UI 띄우기
            if (gameObject.activeSelf)
                _getRewardUI.Init(_pass._goldSprite, "골드 " + _pass._passInfo[number]._freeGold + "개");
            // 정보 갱신
            // 유저 정보 갱신
            _userData._gold += _pass._passInfo[number]._freeGold;
            //파이어베이스 갱신
            updateDic.Add("/_gold/", _userData._gold);
           // Debug.Log(number + "번째 Free 보상으로 " + _pass._passInfo[number]._freeGold);
        }

        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _userData.GetCheckJeongdongjinPassFreeReward()[number] = true;
                updateDic.Add("/" + _jeongdongjinFreeReward + "/" + number.ToString(), true);
                break;
            case PublicDefined.eMapType.skyway:
                _userData.GetCheckSkywayPassFreeReward()[number] = true;
                updateDic.Add("/" + _skywayFreeReward + "/" + number.ToString(), true);
                break;
            case PublicDefined.eMapType.homerspit:
                _userData.GetCheckHomerPassFreeReward()[number] = true;
                updateDic.Add("/" + _homerspitFreeReward + "/" + number.ToString(), true);
                break;
            default:
               // Debug.Log("뭔가 이상하다. 맵 선택이 안되어있다.");
                break;
        }
        DBManager.INSTANCE.UpdateFirebase(updateDic);
    }
    public void ClickPremiumButton(int number)
    {
        PlayClickEffectAudio();
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        _premiumButtons[number].transform.GetComponent<Image>().sprite = _grayButtonSprite;
        _premiumButtons[number].interactable = false;

        // UI 띄우기
        if (gameObject.activeSelf)
            _getRewardUI.Init(_pass._goldSprite, "골드 " + _pass._passInfo[number]._premiumGold + "개");

        // 정보 갱신
        // 유저 정보 갱신
        _userData._gold += _pass._passInfo[number]._premiumGold;

        //파이어베이스 갱신
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _userData.GetCheckJeongdongjinPassPremiumReward()[number] = true;
                updateDic.Add("/" + _jeongdongjinPreReward + "/" + number.ToString(), true);
                break;
            case PublicDefined.eMapType.skyway:
                _userData.GetCheckSkywayPassPremiumReward()[number] = true;
                updateDic.Add("/" + _skywayPreReward + "/" + number.ToString(), true);
                break;
            case PublicDefined.eMapType.homerspit:
                _userData.GetCheckHomerspitPassPremiumReward()[number] = true;
                updateDic.Add("/" + _homerspitPreReward + "/" + number.ToString(), true);
                break;
            default:
                Debug.Log("뭔가 이상하다. 맵 선택이 안되어있다.");
                break;
        }

        updateDic.Add("_gold", _userData._gold);
        DBManager.INSTANCE.UpdateFirebase(updateDic);
    }
    public void ClickBuyPremiumPassButton()
    {
        PlayClickEffectAudio();
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                _buyPassUI.Init(_userData._haveJeongdongjinPass, _userData._gold, this);
                break;
            case PublicDefined.eMapType.skyway:
                _buyPassUI.Init(_userData._haveSkywayPass, _userData._gold, this);
                break;
            case PublicDefined.eMapType.homerspit:
                _buyPassUI.Init(_userData._haveHomerspitPass, _userData._gold, this);
                break;
        }
    }
    public void ClickGetAllRewardButton()
    {
        // 현재 맵이 어딘지 확인한다.
        // 현재 패스 인덱스까지 반복한다.
        // RewardList를 확인하면서 false를 체크한다.
        int currentPassIndex = 0;
        int totalGold = 0;
        bool isHavePass = false;
        string freeMapName = string.Empty;
        string preMapName = string.Empty;
        Dictionary<int, bool> freeRewardDic = new Dictionary<int, bool>();
        Dictionary<int, bool> preRewardDic = new Dictionary<int, bool>();
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                currentPassIndex = _userData._currentJeongdongjinPassIndex;
                freeRewardDic = _userData.GetCheckJeongdongjinPassFreeReward();
                preRewardDic = _userData.GetCheckJeongdongjinPassPremiumReward();
                isHavePass = _userData._haveJeongdongjinPass;
                freeMapName = "/" + _jeongdongjinFreeReward + "/";
                preMapName = "/" + _jeongdongjinPreReward + "/";
                break;
            case PublicDefined.eMapType.skyway:
                currentPassIndex = _userData._currentSkywayPassIndex;
                freeRewardDic = _userData.GetCheckSkywayPassFreeReward();
                preRewardDic = _userData.GetCheckSkywayPassPremiumReward();
                isHavePass = _userData._haveSkywayPass;
                freeMapName = "/" + _skywayFreeReward + "/";
                preMapName = "/" + _skywayPreReward + "/";
                break;
            case PublicDefined.eMapType.homerspit:
                currentPassIndex = _userData._currentHomerspitPassIndex;
                freeRewardDic = _userData.GetCheckHomerPassFreeReward();
                preRewardDic = _userData.GetCheckHomerspitPassPremiumReward();
                isHavePass = _userData._haveHomerspitPass;
                freeMapName = "/" + _homerspitFreeReward + "/";
                preMapName = "/" + _homerspitPreReward + "/";
                break;
        }

        // 만약 첫번째 퀘스트 중이라면 리턴
        if (currentPassIndex.Equals(0))
            return;

        // 패스 여부에 따라 한번 더 하는지 안하는지 결정한다.
        for (int i = 0; i < currentPassIndex; i++) 
        {
            // 만약 무료 보상을 안 받았다면 받는다.
            if(!freeRewardDic[i])
            {
                // 데이터 갱신
                freeRewardDic[i] = true;
                updateDic.Add(freeMapName + i.ToString(), true);

                // 버튼 클릭 못하고 회색으로 변경
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                // 보상이 미끼일 경우
                if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
                {
                    int baitDBNumber = _pass._passInfo[i]._freeBaitDBNumber;
                    int baitQuantity = _pass._passInfo[i]._freeBaitQuantity;

                    // 정보 갱신
                    // 딕셔너리 갱신
                    Dictionary<int, int> baitDic = _userData.GetBaitDictionary();
                    baitDic[baitDBNumber] += baitQuantity;

                    // 파이어베이스 갱신
                    if(updateDic.ContainsKey("/bait/" + baitDBNumber.ToString()))
                    {
                        updateDic["/bait/" + baitDBNumber.ToString()] = baitDic[baitDBNumber];
                    }
                    else
                    {
                        updateDic.Add("/bait/" + baitDBNumber.ToString(), baitDic[baitDBNumber]);
                    }
                    
                    //Debug.Log("미끼 번호 : " + baitDBNumber);
                }
                // 보상이 진주일 경우
                else
                {
                    totalGold += _pass._passInfo[i]._freeGold;
                }
            }
        }

        if(isHavePass)
        {
            for (int i = 0; i < currentPassIndex; i++)
            {
                // 만약 무료 보상을 안 받았다면 받는다.
                if (!preRewardDic[i])
                {
                    preRewardDic[i] = true;
                    updateDic.Add(preMapName + i.ToString(), true);

                    // 버튼 클릭 못하고 회색으로 변경
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;

                    totalGold += _pass._passInfo[i]._premiumGold;
                }
            }
        }

        // 변경된 점이 없다면 리턴
        if (updateDic.Count.Equals(0))
            return;

        // 진주 정보 넣기
        // Debug.Log("전: " + _userData._gold);
        _userData._gold += totalGold;
        updateDic.Add("/_gold/", _userData._gold);
        //Debug.Log("후: " + _userData._gold);

        DBManager.INSTANCE.UpdateRewardState(updateDic);
        _getAllRewardUI.SetActive(true);
    }

    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
}