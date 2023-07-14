using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public class BuyPopUP : MonoBehaviour
{
    [Header("캐시인지 아닌지에 따른 필요 금액 오브젝트")]
    public GameObject _cashObject;
    public GameObject _goldpearlObject;

    [Header("아이템 이름과 이미지")]
    public Text _itemNameText; // 아이템 이름
    public Image _itemImage; //아이템 이미지
    public Text _itemInfoText;

    [Header("필요한 금액과 보유 금액")]
    public Image costImage; // 재화 이미지
    public Text _costText; // 필요 금액
    public Text _haveMoneyText; // 현재 보유 금액

    [Header("보유 여부에 따른 텍스트")]
    public GameObject _haveMoneyObject;
    public GameObject _alreadHaveObject;

    // 드롭다운과 키보드 사용하지 않아도 된다.
    //[Header("Dropdown")]
    [SerializeField] Dropdown _dropdown;
    [SerializeField] Text _dropdownText;

    [Header("키보드")]
    [SerializeField] GameObject _keyboard;

    [Header("구매 확인 UI")]
    public GameObject _buyCheckUI;
    public GameObject _baitBuyCheckUI;

    [Header("--------------------")]
    public Button _buyButton;
    public Button _baitBuyButton;
    public GameObject _baitBuyObject;
    public GameObject _itemInfoObject;

    int _currentCost;
    int _currentHaveGold;
    int _currentHavePearl;
    bool _isFirstPad = true;
    bool _isGold;
    bool _alreadyHave;
    bool _isInfoOn = false;

    //아이템 정보를 받아올 변수
    [HideInInspector] public Item item;

    public void PopupSetItem(Item i)
    {
        //Debug.Log("아이템의 타입: " + i.Type.ToString());

        item = i;

        // 미끼 상자는 항상 열 수 있다.
        if (i.Type.Equals(PublicDefined.eItemType.BaitBox) || i.Type.Equals(PublicDefined.eItemType.Gold) || i.Type.Equals(PublicDefined.eItemType.Pearl))
        {
            _haveMoneyObject.SetActive(true);
            _alreadHaveObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
            _baitBuyObject.SetActive(true);
            CreateDropdownOption();
        }
        else // 그 외 상품들은 가지고 있는지 체크해야 한다.
        {
            if (IsHaveCheck(i))
            {
                _haveMoneyObject.SetActive(false);
                _alreadHaveObject.SetActive(true);
            }
            else
            {
                _haveMoneyObject.SetActive(true);
                _alreadHaveObject.SetActive(false);
            }

            _baitBuyObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
        }

        //아이템 이미지를 팝업 이미지에 삽입
        _itemImage.sprite = i.itemImage;

        _itemNameText.text = i.korName;

        _itemInfoText.text = i.itemInfo;

        //이미지 활성화
        _itemImage.gameObject.SetActive(true);

        item = i;

        if (_haveMoneyObject.activeSelf)
        {
            //재화단위 이미지 삽입
            costImage.sprite = i.costImg;

            _isGold = item.goldCost > 0 ? true : false;
            _costText.text = item.goldCost > 0 ? item.goldCost.ToString() : item.pearlCost.ToString();


            _buyButton.interactable = true;
        }
        else
        {
            _buyButton.interactable = false;
        }

        // 팝업 활성화
        gameObject.SetActive(true);
    }


    private void OnEnable()
    {
        _currentHaveGold = DBManager.INSTANCE.GetUserData()._gold;
        _currentHavePearl = DBManager.INSTANCE.GetUserData()._pearl;
        _itemInfoObject.SetActive(false);
        _isInfoOn = false;

        if (_isGold)
        {
            _haveMoneyText.text = _currentHaveGold.ToString();

            if (item.goldCost > _currentHaveGold)
            {
                _buyButton.interactable = false;
                _baitBuyButton.interactable = false;
            }
        }
        else
        {
            _haveMoneyText.text = _currentHavePearl.ToString();

            if (item.pearlCost > _currentHavePearl)
            {
                _buyButton.interactable = false;
                _baitBuyButton.interactable = false;
            }
        }


        //_haveMoneyText.text = _isGold ? _currentHaveGold.ToString() : _currentHavePearl.ToString();

        _dropdownText.text = 1.ToString();

        //Debug.Log(item.korName + "의 타입과 시리얼 넘버 : " + item.Type + ", " + item.serialNumber);
    }

    bool IsHaveCheck(Item i)
    {
        bool result = true;

        UserData data = DBManager.INSTANCE.GetUserData();
        switch (i.Type)
        {
            case PublicDefined.eItemType.Pass:
                if (i.serialNumber.Equals(0))
                    result = data._haveJeongdongjinPass;
                else if (i.serialNumber.Equals(1))
                    result = data._haveSkywayPass;
                else
                    result = data._haveHomerspitPass;
                break;
            case PublicDefined.eItemType.Rod:
                if (data.GetRodDictionary()[i.serialNumber - 1000].Equals(1))
                    result = true;
                else
                    result = false;
                break;
            case PublicDefined.eItemType.Reel:
                if (data.GetReelDictionary()[i.serialNumber - 6000].Equals(1))
                    result = true;
                else
                    result = false;
                break;
            case PublicDefined.eItemType.Float:
                if (data.GetFloatDictionary()[i.serialNumber - 4000].Equals(1))
                    result = true;
                else
                    result = false;
                break;
            case PublicDefined.eItemType.Sinker:
                if (data.GetSinkerDictionary()[i.serialNumber - 5000].Equals(1))
                    result = true;
                else
                    result = false;
                break;
            case PublicDefined.eItemType.Gold:
                result = false;
                break;
            case PublicDefined.eItemType.Pearl:
                result = false;
                break;
        }

        return result;
    }
    public void OnOffPopUp(bool isOn)
    {
        if (_keyboard.activeSelf)
            _keyboard.SetActive(false);

        _dropdown.value = 0;
        _isFirstPad = true;

        if (!isOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

        gameObject.SetActive(isOn);

        //countSlider.value = 1;
    }
    
    public void Cancel()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }

    public void ClickBuyButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _buyCheckUI.SetActive(true);
    }

    public void ClickBaitBuyButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _baitBuyCheckUI.SetActive(true);
    }

    // 일반 아이템용
    public void ClickYesButton()
    {

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.buyOK).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
        DBManager.INSTANCE.BuyItem(item, _isGold, _buyCheckUI);
    }

    // 미끼 박스용
    public void ClickYesButton2()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.buyOK).GetComponent<AudioPoolObject>().Init();
        int quantity;

        //Debug.Log(_currentCost);
        //Debug.Log(item.goldCost);
        
        if (item.Type == PublicDefined.eItemType.Gold)
            quantity = _currentCost / item.pearlCost;
        else
            quantity = _currentCost / item.goldCost;
        
        //Debug.Log("현재 개수: " + quantity);

        gameObject.SetActive(false);
        DBManager.INSTANCE.BuyItem(item, _isGold, _baitBuyCheckUI, quantity);
    }

    public void ClickNoButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        _buyCheckUI.SetActive(false);
    }

    void CreateDropdownOption()
    {
        _dropdown.options.Clear();

        for (int i = 0; i < 7; i++)
        {
            if (i.Equals(0))
            {
                Dropdown.OptionData op = new Dropdown.OptionData();
                op.text = "1";
                _dropdown.options.Add(op);
            }
            else
            {
                Dropdown.OptionData op = new Dropdown.OptionData();
                int n = i * 5;
                op.text = n.ToString();
                _dropdown.options.Add(op);
            }
        }
        _dropdown.value = 0;
        _baitBuyButton.interactable = true;
        //_currentCost = item.goldCost;
        _currentCost = _isGold ? item.goldCost : item.pearlCost;
        _costText.text = _currentCost.ToString();
    }

    public void ChangeDropdownValue()
    {
        if (_isGold)
        {
            if (_dropdown.value.Equals(0))
                _currentCost = item.goldCost;
            else
                _currentCost = _dropdown.value * 5 * item.goldCost;

            _costText.text = _currentCost.ToString();

            if (_currentHaveGold < _currentCost)
            {
                _baitBuyButton.interactable = false;
            }
            else
            {
                _baitBuyButton.interactable = true;
            }
        }
        else
        {
            if (_dropdown.value.Equals(0))
                _currentCost = item.pearlCost;
            else
                _currentCost = _dropdown.value * 5 * item.pearlCost;

            _costText.text = _currentCost.ToString();

            if (_currentHavePearl < _currentCost)
            {
                _baitBuyButton.interactable = false;
            }
            else
            {
                _baitBuyButton.interactable = true;
            }
        }
    }

    public void ClickKeyboardPad(int num)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if ((_isFirstPad || int.Parse(_dropdownText.text).Equals(0)) && !num.Equals(10))
        {
            _dropdown.value = 0;
            _dropdownText.text = num.ToString();
            _isFirstPad = false;
        }
        else
        {
            _dropdown.value = 0;
            if (num.Equals(10)) // backspace는 10이다.
            {
                if (_dropdownText.text.Length >= 2)
                {
                    char[] ch = _dropdownText.text.ToCharArray();

                    for (int i = 0; i < ch.Length - 1; i++)
                    {
                        if (i.Equals(0))
                            _dropdownText.text = ch[i].ToString();
                        else
                            _dropdownText.text += ch[i].ToString();
                    }
                }
                else
                {
                    _dropdownText.text = 0.ToString();
                }
            }
            else
            {
                _dropdownText.text += num;

                if (int.Parse(_dropdownText.text) > 255)
                    _dropdownText.text = 255.ToString();
            }
        }
        _currentCost = _isGold ? int.Parse(_dropdownText.text) * item.goldCost : int.Parse(_dropdownText.text) * item.pearlCost;
        _costText.text = _currentCost.ToString();

        if (_isGold)
        {
            if (_currentHaveGold < _currentCost)
            {
                _baitBuyButton.interactable = false;
            }
            else
            {
                _baitBuyButton.interactable = true;

                if (_currentCost <= 0)
                    _baitBuyButton.interactable = false;
                else
                    _baitBuyButton.interactable = true;
            }
        }
        else
        {
            if (_currentHavePearl < _currentCost)
            {
                _baitBuyButton.interactable = false;
            }
            else
            {
                _baitBuyButton.interactable = true;

                if (_currentCost <= 0)
                    _baitBuyButton.interactable = false;
                else
                    _baitBuyButton.interactable = true;
            }
        }
    }

    public void ClickKeyboardButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if (_keyboard.activeSelf)
            _keyboard.SetActive(false);
        else
        {
            _isFirstPad = true;
            _keyboard.SetActive(true);
        }
    }

    public void KeyboardOff()
    {
        _isFirstPad = true;
        _keyboard.SetActive(false);
    }

    public void ClickItemInfo()
    {
        //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if(_isInfoOn)
        {
            _itemInfoObject.SetActive(false);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        }
        else
        {
            _itemInfoObject.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        }

        _isInfoOn = !_isInfoOn;
    }
}