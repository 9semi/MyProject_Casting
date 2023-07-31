using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PassBuyPopup : MonoBehaviour
{
    Item _jeondongjin;
    Item _homerspit;
    Item _skyway;
    Item _platinumPackage;
    Item _diamondPackage;
    Item _adblockPackage;

    [SerializeField] GetPackageRewardUI _getRewardUI;

    [SerializeField] GameObject[] _buyButtons;
    [SerializeField] Sprite[] _passSprites;
    [SerializeField] GameObject _infoObjects;

    [SerializeField] Image _passImage;
    [SerializeField] Text _passNameText;
    [SerializeField] Text _infoText;
    [SerializeField] Text _costText;
    [SerializeField] GameObject _alreadyHaveObject;

    bool _isInfoOn;
    UserData _userData;

    // 패스와 월정액 상품
    public void InitPassBuyPopup(int number)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if (_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        _passImage.sprite = _passSprites[number];
        _alreadyHaveObject.SetActive(false);

        if (number.Equals(0))
        {
            if (_jeondongjin == null)
            {
                _jeondongjin = ItemData.Instance.passItemDB[0];
            }
            _passNameText.text = _jeondongjin.korName;
            _infoText.text = _jeondongjin.itemInfo;
            _costText.text = _jeondongjin.cashCost.ToString("#,##0");
            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._haveJeongdongjinPass)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }

        else if (number.Equals(1))
        {
            if (_homerspit == null)
            {
                _homerspit = ItemData.Instance.passItemDB[2];
            }

            _passNameText.text = _homerspit.korName;
            _infoText.text = _homerspit.itemInfo;
            _costText.text = _homerspit.cashCost.ToString("#,##0");
            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._haveHomerspitPass)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }

        else if(number.Equals(2))
        {
            if (_skyway == null)
            {
                _skyway = ItemData.Instance.passItemDB[1];
            }

            _passNameText.text = _skyway.korName;
            _infoText.text = _skyway.itemInfo;
            _costText.text = _skyway.cashCost.ToString("#,##0");
            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._haveSkywayPass)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }

        else if(number.Equals(3))
        {
            if (_platinumPackage == null)
            {
                _platinumPackage = ItemData.Instance.packageItemDB[0];
            }

            _passNameText.text = _platinumPackage.korName;
            _infoText.text = _platinumPackage.itemInfo;
            _costText.text = _platinumPackage.cashCost.ToString("#,##0");
            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._havePlatinumPackage)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }

        else if(number.Equals(4))
        {
            if (_diamondPackage == null)
            {
                _diamondPackage = ItemData.Instance.packageItemDB[1];
            }

            _passNameText.text = _diamondPackage.korName;
            _infoText.text = _diamondPackage.itemInfo;
            _costText.text = _diamondPackage.cashCost.ToString("#,##0");
            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._haveDiamondPackage)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }
        else if(number.Equals(5))
        {
            if (_adblockPackage == null)
            {
                _adblockPackage = ItemData.Instance.packageItemDB[2];
            }

            _passNameText.text = _adblockPackage.korName;
            _infoText.text = _adblockPackage.itemInfo;
            _costText.text = _adblockPackage.cashCost.ToString("#,##0");

            for (int i = 0; i < _buyButtons.Length; i++)
            {
                _buyButtons[i].SetActive(false);
            }

            _buyButtons[number].gameObject.SetActive(true);

            if (_userData._haveADBlock)
            {
                _alreadyHaveObject.SetActive(true);
                _buyButtons[number].gameObject.SetActive(false);
            }
        }

        _infoObjects.SetActive(false);
        _isInfoOn = false;
        gameObject.SetActive(true);
    }

    public void ClickInfoButton()
    {
        _isInfoOn = !_isInfoOn;
        _infoObjects.SetActive(_isInfoOn);

        if(_isInfoOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        else
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }

    public void SuccessBuyPackage(int type)
    {
        _getRewardUI.Init(type, DateTime.Today, DateTime.Today.AddDays(29));
        gameObject.SetActive(false);
    }
}
