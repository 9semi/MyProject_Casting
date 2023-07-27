using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookAndSeasonPass : MonoBehaviour
{
    // ��ư ���� translucence
    readonly Color _bookButtonColor = new Color(0.8f, 0.8f, 1f, 1);
    readonly Color _passButtonColor = new Color(0.7f, 0.7f, 0.7f, 1);
    readonly Color _translucenceBookButtonColor = new Color(0.8f, 0.8f, 1f, 0.4f);
    readonly Color _translucencePassButtonColor = new Color(0.7f, 0.7f, 0.7f, 0.4f);

    // ���̾�̽��� ���� string
    readonly string _jeongdongjinFreeReward = "jeongdongjinPassFreeRewardState";
    readonly string _skywayFreeReward = "skywayPassFreeRewardState";
    readonly string _homerspitFreeReward = "homerspitPassFreeRewardState";
    readonly string _jeongdongjinPreReward = "jeongdongjinPassPremiumRewardState";
    readonly string _skywayPreReward = "skywayPassPremiumRewardState";
    readonly string _homerspitPreReward = "homerspitPassPremiumRewardState";

    // ����� ��޿� ���� �̸� ����
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    [Header("���� ����")]
    [Header("������ �н� ������Ʈ")]
    [SerializeField] GameObject _bookObject, _passObject;
    [Header("������ �н� ��ư")]
    [SerializeField] Image _bookButtonImage;
    [SerializeField] Image _passButtonImage;
    [Header("����� ������Ʈ")]
    [SerializeField] GameObject[] fishes;
    [SerializeField] Sprite[] fishSprite;
    [Header("����� �̹��� ��ư")]
    [SerializeField] Transform _bookSlotContent;
    BookSlot[] _bookSlots; public BookSlot[] BookSlots { get { return _bookSlots; } }

    [Header("�н� ����")]
    [SerializeField] Transform[] _questButtons;
    [SerializeField] Button[] _freeButtons;
    [SerializeField] Button[] _premiumButtons;
    [SerializeField] Image[] _panels;
    [SerializeField] PassGetRewardUI _getRewardUI;
    [SerializeField] GameObject _getAllRewardUI;
    [SerializeField] PassBuyUI _buyPassUI;

    [Header("�н� ��ư ��������Ʈ")]
    [SerializeField] Sprite _grayButtonSprite;
    [SerializeField] Sprite _greenButtonSprite;
    [SerializeField] Sprite _blueButtonSprite;

    [Header("üũ �̹���")]
    [SerializeField] GameObject[] _check_green;
    [SerializeField] GameObject[] _check_yellow;

    [Header("�̴ϸ�")]
    [SerializeField] MiniMap _minimap;

    // �н� �̼� �������� ������ �ִ�.
    Pass _pass;
    // �̳� ���� ������ ����.
    List<Item> _baitList;
    // ���� ���� ������ �־�� ���� ��
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

                // ���� ���� ������ŭ �ݺ� 
                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    // ���������� ������ �ش� �� ����� �̹����� ����
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    // ���� jeongdongjin ����� �̸��� �����鿡 �ִ� ������� ������ - ������ ������� ������ �����ټ�
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.GetFishInfoArray().Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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

                // ���� ���� ������ŭ �ݺ� 
                for (int i = 0; i < _bookSlots.Length; i++)
                {
                    // ���������� ������ �ش� �� ����� �̹����� ����
                    _bookSlots[i].GetFishImage().sprite = fishSprite[i];
                    // ���� jeongdongjin ����� �̸��� �����鿡 �ִ� ������� ������ - ������ ������� ������ �����ټ�
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.GetFishInfoArray().Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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
                    // ����� ���� ����(4��)��ŭ �ݺ�
                    for (int j = 0; j < fish.info.Length; j++)
                    {
                        // ���Կ� ����� jeongdongjin ����� ������ �����鿡 �ִ� ������ ������
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
            // ����Ʈ �κп� üũ ǥ��(Ŭ����: �ʷ�, ���� ��: ���)
            if (i < index) // Ŭ����
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(true);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = false;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
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
            else if (i.Equals(index)) // ���� ��
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(true);
                _panels[i].enabled = false;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _premiumButtons[i].interactable = false;
            }
            else // �� �����ִ�.
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = true;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
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

            // ����Ʈ ���� �ֱ�
            _questButtons[i].GetChild(0).GetComponent<Text>().text = _pass._passInfo[i]._questContent;

            // ���� �׸� �ֱ�
            // ������ �̳���� �� ��
            if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _baitList[_pass._passInfo[i]._freeBaitDBNumber].itemImage;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text =
                    _baitList[_pass._passInfo[i]._freeBaitDBNumber].korName + " " + _pass._passInfo[i]._freeBaitQuantity.ToString() + "��";
            }
            // ������ ���ֶ�� �� ��
            else
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._freeGold.ToString() + "��";
            }

            _premiumButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
            _premiumButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._premiumGold.ToString() + "��";
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
            // ����Ʈ �κп� üũ ǥ��(Ŭ����: �ʷ�, ���� ��: ���)
            if (i < index) // Ŭ����
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(true);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = false;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
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
            else if (i.Equals(index)) // ���� ��
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(true);
                _panels[i].enabled = false;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _premiumButtons[i].interactable = false;
            }
            else // �� �����ִ�.
            {
                //Debug.Log(i + "��° freebutton: " + freeRewardState[i] + " , premiumbutton: " + preRewardState[i]);
                _check_green[i].SetActive(false);
                _check_yellow[i].SetActive(false);
                _panels[i].enabled = true;

                // ������ ���� ��ư�� ȸ������ �ٲٰ� ���´�.
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

            // ����Ʈ ���� �ֱ�
            _questButtons[i].GetChild(0).GetComponent<Text>().text = _pass._passInfo[i]._questContent;

            // ���� �׸� �ֱ�
            // ������ �̳���� �� ��
            if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _baitList[_pass._passInfo[i]._freeBaitDBNumber].itemImage;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text =
                    _baitList[_pass._passInfo[i]._freeBaitDBNumber].korName + " " + _pass._passInfo[i]._freeBaitQuantity.ToString() + "��";
            }
            // ������ ���ֶ�� �� ��
            else
            {
                _freeButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
                _freeButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._freeGold.ToString() + "��";
            }

            _premiumButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _pass._goldSprite;
            _premiumButtons[i].transform.GetChild(1).GetComponent<Text>().text = _pass._passInfo[i]._premiumGold.ToString() + "��";
        }
    }
    void OpenPass()
    {
        // �����н� ������ Ȱ��ȭ
        _passObject.SetActive(true);
        _bookObject.SetActive(false);
    }
    void OpenBook()
    {
        // ���� ������ Ȱ��ȭ
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

            // UI ����
            if(gameObject.activeSelf)
                _getRewardUI.Init(_baitList[_pass._passInfo[number]._freeBaitDBNumber].itemImage, _baitList[baitDBNumber].korName + " " + baitQuantity + "��");

            // ���� ����
            // ��ųʸ� ����
            Dictionary<int, int> baitDic = _userData.GetBaitDictionary();
            baitDic[baitDBNumber] += baitQuantity;

            // ���̾�̽� ����
            updateDic.Add("/bait/" + baitDBNumber.ToString(), baitDic[baitDBNumber]);
           // Debug.Log(number + "��° Free �������� " + ItemData.Instance.baitItemDB[_pass._passInfo[number]._freeBaitDBNumber]);
        }
        else
        {
            // UI ����
            if (gameObject.activeSelf)
                _getRewardUI.Init(_pass._goldSprite, "��� " + _pass._passInfo[number]._freeGold + "��");
            // ���� ����
            // ���� ���� ����
            _userData._gold += _pass._passInfo[number]._freeGold;
            //���̾�̽� ����
            updateDic.Add("/_gold/", _userData._gold);
           // Debug.Log(number + "��° Free �������� " + _pass._passInfo[number]._freeGold);
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
               // Debug.Log("���� �̻��ϴ�. �� ������ �ȵǾ��ִ�.");
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

        // UI ����
        if (gameObject.activeSelf)
            _getRewardUI.Init(_pass._goldSprite, "��� " + _pass._passInfo[number]._premiumGold + "��");

        // ���� ����
        // ���� ���� ����
        _userData._gold += _pass._passInfo[number]._premiumGold;

        //���̾�̽� ����
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
                Debug.Log("���� �̻��ϴ�. �� ������ �ȵǾ��ִ�.");
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
        // ���� ���� ����� Ȯ���Ѵ�.
        // ���� �н� �ε������� �ݺ��Ѵ�.
        // RewardList�� Ȯ���ϸ鼭 false�� üũ�Ѵ�.
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

        // ���� ù��° ����Ʈ ���̶�� ����
        if (currentPassIndex.Equals(0))
            return;

        // �н� ���ο� ���� �ѹ� �� �ϴ��� ���ϴ��� �����Ѵ�.
        for (int i = 0; i < currentPassIndex; i++) 
        {
            // ���� ���� ������ �� �޾Ҵٸ� �޴´�.
            if(!freeRewardDic[i])
            {
                // ������ ����
                freeRewardDic[i] = true;
                updateDic.Add(freeMapName + i.ToString(), true);

                // ��ư Ŭ�� ���ϰ� ȸ������ ����
                _freeButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                _freeButtons[i].interactable = false;

                // ������ �̳��� ���
                if (_pass._passInfo[i]._type.Equals(PassInfo.eFreeRewardType.Bait))
                {
                    int baitDBNumber = _pass._passInfo[i]._freeBaitDBNumber;
                    int baitQuantity = _pass._passInfo[i]._freeBaitQuantity;

                    // ���� ����
                    // ��ųʸ� ����
                    Dictionary<int, int> baitDic = _userData.GetBaitDictionary();
                    baitDic[baitDBNumber] += baitQuantity;

                    // ���̾�̽� ����
                    if(updateDic.ContainsKey("/bait/" + baitDBNumber.ToString()))
                    {
                        updateDic["/bait/" + baitDBNumber.ToString()] = baitDic[baitDBNumber];
                    }
                    else
                    {
                        updateDic.Add("/bait/" + baitDBNumber.ToString(), baitDic[baitDBNumber]);
                    }
                    
                    //Debug.Log("�̳� ��ȣ : " + baitDBNumber);
                }
                // ������ ������ ���
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
                // ���� ���� ������ �� �޾Ҵٸ� �޴´�.
                if (!preRewardDic[i])
                {
                    preRewardDic[i] = true;
                    updateDic.Add(preMapName + i.ToString(), true);

                    // ��ư Ŭ�� ���ϰ� ȸ������ ����
                    _premiumButtons[i].transform.GetComponent<Image>().sprite = _grayButtonSprite;
                    _premiumButtons[i].interactable = false;

                    totalGold += _pass._passInfo[i]._premiumGold;
                }
            }
        }

        // ����� ���� ���ٸ� ����
        if (updateDic.Count.Equals(0))
            return;

        // ���� ���� �ֱ�
        // Debug.Log("��: " + _userData._gold);
        _userData._gold += totalGold;
        updateDic.Add("/_gold/", _userData._gold);
        //Debug.Log("��: " + _userData._gold);

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