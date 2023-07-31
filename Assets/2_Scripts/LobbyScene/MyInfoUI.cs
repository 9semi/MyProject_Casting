using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoUI : MonoBehaviour
{
    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);
    readonly string[] _aquariumNames = { "산호초", "맹그로브 숲", "고요한 난파선", "해조숲", "푸른빛 동굴" };

    [Header("정보 메뉴")]
    [SerializeField] Text _goldText;
    [SerializeField] Text _pearlText;
    [SerializeField] Text _winText;
    [SerializeField] Text _loseText;
    [SerializeField] Text _drawText;
    [SerializeField] Text _jeongdongjinFishRecord;
    [SerializeField] Text _skywayFishRecord;
    [SerializeField] Text _homerspitFishRecord;

    [Header("장비 메뉴")]
    [SerializeField] Sprite _uimaskSprite;
    [SerializeField] Image _rodImage;
    [SerializeField] Text _rodText;
    [SerializeField] Image _reelImage;
    [SerializeField] Text _reelText;
    [SerializeField] Image _baitImage;
    [SerializeField] Text _baitText;
    [SerializeField] Image _pastebaitImage;
    [SerializeField] Text _pastebaitText;
    [SerializeField] Image _floatImage;
    [SerializeField] Text _floatText;
    [SerializeField] Image _sinkerImage;
    [SerializeField] Text _sinkerText;

    [Header("대표 물고기 메뉴")]
    [Header("물고기 이미지")]
    [SerializeField] Sprite[] _fishSprites;
    [SerializeField] Sprite[] _aquariumBGSprites;
    [SerializeField] Text _aquariumName;
    [SerializeField] GameObject _aquariumNullObject;
    [SerializeField] Image _aquariumBG;

    [Header("--------------")]
    [Header("버튼 이미지")]
    [SerializeField] Sprite _selectButtonSprite;
    [SerializeField] Sprite _unselectButtonSprite;

    [Header("대표 물고기 UI")]
    [SerializeField] Image _fishImage;
    [SerializeField] Text _fishNameText;
    [SerializeField] Text _fishLengthText;
    [SerializeField] Text _fishWeightText;
    [SerializeField] Text _fishPriceText;
    [SerializeField] Sprite _noneSprite;

    string _name;
    int _price;
    float _l;
    float _w;
    int _type;
    string _key;
    int _dbnumber;

    public Transform _content;
    int _aquariumNumber = 0;

    public Image[] _buttons;
    public GameObject[] _menus;

    UserData _userData;

    // 물고기 슬롯을 눌렀을 경우 true
    bool _isRemove = false; // 삭제인지 구별
    bool _isRenewal = false; // 맨 처음 들어갔을 때인지 구별

    bool _isFirst = true; // 맨처음 OnEnable이 실행될 때 버튼 소리가 안 나게 막기 위해

    int _currentButton = -1;

    PublicDefined.stFishInfo _previousRepresentFish;

    void OnEnable()
    {
        _goldText.text = _userData._gold.ToString("#,##0");
        _pearlText.text = _userData._pearl.ToString("#,##0");

        _isRenewal = false;

        if (_userData._haveRepresentFish)
            _isRemove = false;
        else
            _isRemove = true;

        if (_userData._haveRepresentFish)
        {
            _previousRepresentFish = _userData.GetRepresentFish();
            _fishImage.sprite = _fishSprites[_previousRepresentFish._fishNumber];
            _fishLengthText.text = _previousRepresentFish._length.ToString("N2");
            _fishWeightText.text = _previousRepresentFish._weight.ToString("N2");
            _fishPriceText.text = _previousRepresentFish._price.ToString();
            _fishNameText.text = _previousRepresentFish._name;

            Color c = GetColorAccordingToType(_previousRepresentFish._fishType);
            _fishLengthText.color = c;
            _fishWeightText.color = c;
            _fishPriceText.color = c;
            _fishNameText.color = c;
        }
        else
        {
            _fishImage.sprite = _noneSprite;
            _fishLengthText.text = "?";
            _fishWeightText.text = "?";
            _fishPriceText.text = "?";
            _fishNameText.text = "?";

            _fishLengthText.color = Color.white;
            _fishWeightText.color = Color.white;
            _fishPriceText.color = Color.white;
            _fishNameText.color = Color.white;
        }


        // 메뉴 버튼 이미지 초기화
        ClickButton(0);
    }

    public void Init(UserData userData)
    {
        _userData = userData;

        if(_userData._haveRepresentFish)
        {
            _previousRepresentFish = _userData.GetRepresentFish();
            _fishImage.sprite = _fishSprites[_previousRepresentFish._fishNumber];
            _fishLengthText.text = _previousRepresentFish._length.ToString("N2");
            _fishWeightText.text = _previousRepresentFish._weight.ToString("N2");
            _fishPriceText.text = _previousRepresentFish._price.ToString();
            _fishNameText.text = _previousRepresentFish._name;

            Color c = GetColorAccordingToType(_previousRepresentFish._fishType);
            _fishLengthText.color = c;
            _fishWeightText.color = c;
            _fishPriceText.color = c;
            _fishNameText.color = c;
        }
        else
        {
            _fishImage.sprite = _noneSprite;
            _fishLengthText.text = "?";
            _fishWeightText.text = "?";
            _fishPriceText.text = "?";
            _fishNameText.text = "?";

            _fishLengthText.color = Color.white;
            _fishWeightText.color = Color.white;
            _fishPriceText.color = Color.white;
            _fishNameText.color = Color.white;
        }

        // 정보 메뉴 초기화(UserData에서 가져옴)
        InfomationMenu();
        // 장비 메뉴 초기화(UserData에서 가져옴)
        EquipmentMenu();
        // 아쿠아리움 메뉴 초기화(번호)(UserData에서 가져옴)
        AquariumFishMenu(0);

    }
    public void ClickButton(int num)
    {
        if (_isFirst)
            _isFirst = false;
        else
            PlayClickEffectAudio();

        if (num.Equals(_currentButton))
            return;

        _currentButton = num;
        MenuOnOff(num);

        // 장비는 업데이트 해줘야한다.
        if (num.Equals(1))
            EquipmentMenu();
    }
    void MenuOnOff(int num)
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            _menus[i].SetActive(false);
            _buttons[i].sprite = _unselectButtonSprite;
        }

        _menus[num].SetActive(true);
        _buttons[num].sprite = _selectButtonSprite;
    }

    void InfomationMenu()
    {
        // 정보 메뉴 초기화
        _winText.text = _userData._win.ToString();
        _loseText.text = _userData._lose.ToString();
        _drawText.text = _userData._draw.ToString();
        _jeongdongjinFishRecord.text = _userData.GetJeongdongjinRankDictionary().Count.ToString();
        _skywayFishRecord.text = _userData.GetSkywayRankDictionary().Count.ToString();
        _homerspitFishRecord.text = _userData.GetHomerspitRankDictionary().Count.ToString();
    }
    void EquipmentMenu()
    {
        Dictionary<string, int> dic = _userData.GetCurrentEquipmentDictionary();
        ItemData instance = ItemData.Instance;
        Item item;

        item = instance.rodItemDB[dic["rod"]];
        _rodImage.sprite = item.itemImage;
        _rodText.text = item.korName;

        item = instance.reelItemDB[dic["reel"]];
        _reelImage.sprite = instance.reelItemDB[dic["reel"]].itemImage;
        _reelText.text = item.korName;

        if (!dic["bait"].Equals(-1))
        {
            item = instance.baitItemDB[dic["bait"]];
            _baitImage.sprite = item.itemImage;
            _baitText.text = item.korName;
        }
        else
        {
            _baitImage.sprite = _uimaskSprite;
            _baitText.text = string.Empty;
        }

        if (!dic["pastebait"].Equals(-1))
        {
            item = instance.pasetbaitItemDB[dic["pastebait"]];
            _pastebaitImage.sprite = item.itemImage;
            _pastebaitText.text = item.korName;
        }
        else
        {
            _pastebaitImage.sprite = _uimaskSprite;
            _pastebaitText.text = string.Empty;
        }

        if (!dic["float"].Equals(-1))
        {
            item = instance.floatItemDB[dic["float"]];
            _floatImage.sprite = instance.floatItemDB[dic["float"]].itemImage;
            _floatText.text = item.korName;
        }
        else
        {
            _floatImage.sprite = _uimaskSprite;
            _floatText.text = string.Empty;
        }

        if (!dic["sinker"].Equals(-1))
        {
            item = instance.sinkerItemDB[dic["sinker"]];
            _sinkerImage.sprite = item.itemImage;
            _sinkerText.text = item.korName;
        }
        else
        {
            _sinkerImage.sprite = _uimaskSprite;
            _sinkerText.text = string.Empty;
        }
    }
    void AquariumFishMenu(int num)
    {
        int count = 0;
        Dictionary<int, List<PublicDefined.stFishInfo>> dic;
        _aquariumName.text = _aquariumNames[num];
        _aquariumBG.sprite = _aquariumBGSprites[num];
        switch (num)
        {
            case 0:
                dic = _userData.GetFirstAquariumDictionary();
                break;
            case 1:
                dic = _userData.GetSecondAquariumDictionary();
                break;
            case 2:
                dic = _userData.GetThirdAquariumDictionary();
                break;
            case 3:
                dic = _userData.GetFourthAquariumDictionary();
                break;
            case 4:
                dic = _userData.GetFifthAquariumDictionary();
                break;
            default:
                dic = _userData.GetFirstAquariumDictionary();
                break;
        }

        if(dic.Count <= 0)
            _aquariumNullObject.SetActive(true);
        else
            _aquariumNullObject.SetActive(false);

        foreach (List<PublicDefined.stFishInfo> list in dic.Values)
        {
            for (int i = 0; i < list.Count; i++)
            {
                PublicDefined.stFishInfo fish = list[i];
                _content.GetChild(count).GetComponent<MyInfoFishSlot>().Init(this,GetColorAccordingToType(fish._fishType), fish._fishType, fish._name, fish._length, fish._weight, fish._price, fish._fishNumber, fish._key);
                count++;
            }
        }

        for (int i = count; i < _content.childCount; i++)
        {
            _content.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ClickLeftAquariumButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileArrow).GetComponent<AudioPoolObject>().Init();
        if (_aquariumNumber <= 0)
        {
            _aquariumNumber = 4;
        }
        else
        {
            _aquariumNumber--;
        }

        AquariumFishMenu(_aquariumNumber);
    }
    public void ClickRightAquariumButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileArrow).GetComponent<AudioPoolObject>().Init();
        if (_aquariumNumber >= 4)
        {
            _aquariumNumber = 0;
        }
        else
        {
            _aquariumNumber++;
        }
        AquariumFishMenu(_aquariumNumber);
    }

    public void SetRepresentFish(Color typeColor, int type, int DBNumber, float l, float w, int g, string name, string key)
    {
        _isRemove = false;
        _isRenewal = true;

        _fishLengthText.color = typeColor;
        _fishWeightText.color = typeColor;
        _fishPriceText.color = typeColor;
        _fishNameText.color = typeColor;

        _dbnumber = DBNumber;

        _l = l;
        _w = w;
        _type = 
        _price = g;
        _name = name;
        _key = key;
        _type = type;

        _fishImage.sprite = _fishSprites[DBNumber];

        _fishLengthText.text = l.ToString("N2");
        _fishWeightText.text = w.ToString("N2");
        _fishPriceText.text = g.ToString();
        _fishNameText.text = name;
    }

    public void ClickRemoveButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileRemove).GetComponent<AudioPoolObject>().Init();
        _isRemove = true;
        _fishImage.sprite = _noneSprite;

        _fishLengthText.text = "?";
        _fishWeightText.text = "?";
        _fishPriceText.text = "?";
        _fishNameText.text = "?";
    }
    public void ClickSaveButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileSave).GetComponent<AudioPoolObject>().Init();

        if (_isRemove)
        {
            if(_userData._haveRepresentFish)
            {
                _userData._haveRepresentFish = false;
                _previousRepresentFish = new PublicDefined.stFishInfo();
                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("/_haveRepresentFish/", false);
                DBManager.INSTANCE.UpdateFirebase(updateDic);
            }
        }
        else
        {
            //Debug.Log(_previousRepresentFish);
            //Debug.Log(_previousRepresentFish._key);
            //Debug.Log(_key);
            if (!_isRenewal || _previousRepresentFish._key == _key)
            {
                //Debug.Log("저장 막음");
                return;
            }

            Dictionary<string, object> updateDic = new Dictionary<string, object>();
            _userData.InitRepresentFish(_dbnumber, _name, _l, _w, _price, _type, _key);
            _previousRepresentFish = new PublicDefined.stFishInfo(_dbnumber, _name, _l, _w, _price, (PublicDefined.eFishType)_type);
            _previousRepresentFish.SetKey(_key);

            _userData._haveRepresentFish = true;

            updateDic.Add("/_haveRepresentFish/", true);
            DBManager.INSTANCE.UpdateFirebase(updateDic);
            DBManager.INSTANCE.UpdateRepresentFish();
        }

    }
    Color GetColorAccordingToType(int type)
    {
        switch (type)
        {
            case 0:
                return _sundryColor;
            case 1:
                return _normalColor;
            case 2:
                return _rareColor;
            default:
                return Color.white;
        }
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
