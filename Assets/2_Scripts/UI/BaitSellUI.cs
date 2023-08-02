using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaitSellUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button _sellButton;
    [SerializeField] InputField _inputField;
    [SerializeField] Text _baitNameText;
    [SerializeField] Image _baitImage;
    [SerializeField] Text _beforeQuantityText;
    [SerializeField] Text _priceText;

    [Header("확인/취소 오브젝트")]
    [SerializeField] BaitSellCheckUI _checkUI;

    int _currentQuantity;
    int _maxQuantity; // 팔 수 있는 최대 수량

    Item _baitItem;
    StringBuilder _sb;

    UserData _userData;

    GameObject _fishingGearEquipUIObject;

    public void Init(UserData userData, Item baitItem, int quantity, GameObject equipUIObject)
    {
        if (_userData == null)
            _userData = userData;

        if (_sb == null)
            _sb = new StringBuilder();

        _sb.Length = 0;
        _sb.Append("현재 ");
        //_sb.Append(_userData.GetBaitDictionary()[baitItem.serialNumber - 2000]);
        _sb.Append(quantity);
        _sb.Append("를 보유 중입니다.");
        _beforeQuantityText.text = _sb.ToString();

        //_inputField.text = "0";
        //_priceText.text = "0";
        //_currentQuantity = 0;
        _inputField.text = quantity.ToString();
        _priceText.text = (quantity * 20).ToString();
        _currentQuantity = quantity;

        _maxQuantity = quantity;

        _fishingGearEquipUIObject = equipUIObject;
        _baitItem = baitItem;
        _baitNameText.text = baitItem.korName;
        _baitImage.sprite = baitItem.itemImage;

        gameObject.SetActive(true);
    }

    public void ClickLeftButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if (_currentQuantity <= 0)
            return;
        else
        {
            _currentQuantity--;
            _inputField.text = _currentQuantity.ToString();
            _priceText.text = (_currentQuantity * 20).ToString("#,##0");
        }
        
    }
    public void ClickRightButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        if (_currentQuantity >= _maxQuantity)
            return;
        else
        {
            _currentQuantity++;
            _inputField.text = _currentQuantity.ToString();
            _priceText.text = (_currentQuantity * 20).ToString("#,##0");
        }
    }

    public void ClickSellButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.coin).GetComponent<AudioPoolObject>().Init();

        if (_currentQuantity <= 0)
            return;

        int DBNumber = _baitItem.serialNumber - 2000;
        _checkUI.Init(_userData, _baitItem.korName, _currentQuantity, _userData.GetBaitDictionary()[DBNumber], DBNumber, gameObject, _fishingGearEquipUIObject);
        //gameObject.SetActive(false);
    }

    public void ClickXButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
    public void UpdateInputField()
    {
        if(_inputField.text == string.Empty)
        {
            _currentQuantity = 0;
        }
        else
        {
            _currentQuantity = int.Parse(_inputField.text);

            if (_currentQuantity < 0)
            {
                _currentQuantity = 0;
            }
            else if (_currentQuantity > _maxQuantity)
            {
                _currentQuantity = _maxQuantity;

            }
        }

        _inputField.text = _currentQuantity.ToString();
        _priceText.text = (_currentQuantity * 20).ToString("#,##0");
    }
}
