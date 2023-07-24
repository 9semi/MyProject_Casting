using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAquariumUI : MonoBehaviour
{
    [SerializeField] GameObject _moneyObject;
    [SerializeField] GameObject _cashObject;

    [SerializeField] GameObject[] _cashBuyButtonObject;

    [SerializeField] Text _costText;
    [SerializeField] Text _currentMoneyText;
    [SerializeField] Text _aquariumNameText;
    [SerializeField] Text _cashCostText;

    [SerializeField] Sprite _goldSprite;
    [SerializeField] Sprite _cashSprite;

    [SerializeField] Image _moneyImage;
    [SerializeField] Image _aquariumImage;

    [SerializeField] Button _buyButton;

    [SerializeField] GameObject _buyCheckUI;

    [SerializeField] FishInfoUI _fishInfoUI;

    UserData _userData;
    AquariumUI _aquariumUI;
    int _aquariumNumber;
    bool _isCash;
    int _cost;

    public void Init(bool isCash, int cost, int aquariumNumber, Sprite aquariumSprite, UserData userData, AquariumUI aquariumUI)
    {
        _aquariumNumber = aquariumNumber;
        _userData = userData;
        _isCash = isCash;
        _cost = cost;
        _aquariumUI = aquariumUI;

        switch (aquariumNumber)
        {
            case 1:
                _aquariumNameText.text = "¸Í±×·Îºê ½£";
                break;
            case 2:
                _aquariumNameText.text = "°í¿äÇÑ ³­ÆÄ¼±";
                break;
            case 3:
                _aquariumNameText.text = "ÇØÁ¶½£";
                _cashBuyButtonObject[0].SetActive(true);
                _cashBuyButtonObject[1].SetActive(false);
                break;
            case 4:
                _aquariumNameText.text = "Çª¸¥ºû µ¿±¼";
                _cashBuyButtonObject[0].SetActive(false);
                _cashBuyButtonObject[1].SetActive(true);
                break;
        }
        _aquariumImage.sprite = aquariumSprite;
        _costText.text = cost.ToString("#,##0");

        // ¼öÁ·°üÀÇ °ªÀÌ Ä³½Ã¶ó¸é
        if (_isCash)
        {
            _moneyObject.SetActive(false);
            _cashObject.SetActive(true);
            _moneyImage.sprite = _cashSprite;
            _cashCostText.text = _cost.ToString("#,##0");
        }
        else
        {
            _moneyObject.SetActive(true);
            _cashObject.SetActive(false);
            _moneyImage.sprite = _goldSprite;
            _currentMoneyText.text = userData._gold.ToString("#,##0");
            _buyButton.interactable = true;
            if (cost > userData._gold)
            {
                _buyButton.interactable = false;
            }
        }
        gameObject.SetActive(true);
    }

    public void ClickBuyButton()
    {
        PlayClickEffectAudio();
        _buyCheckUI.SetActive(true);
    }

    public void ClickOKButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.buyOK).GetComponent<AudioPoolObject>().Init();
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        switch(_aquariumNumber)
        {
            case 1:
                _userData._haveSecondAquarium = true;
                updateDic.Add("/_haveSecondAquarium/", true);
                break;
            case 2:
                _userData._haveThirdAquarium = true;
                updateDic.Add("/_haveThirdAquarium/", true);
                break;
            case 3:
                _userData._haveFourthAquarium = true;
                updateDic.Add("/_haveFourthAquarium/", true);
                break;
            case 4:
                _userData._haveFifthAquarium = true;
                updateDic.Add("/_haveFifthAquarium/", true);
                break;
        }

        if(_isCash)
        {
            //_userData._pearl -= _cost;
            //updateDic.Add("/_pearl/", _userData._pearl);
        }
        else
        {
            _userData._gold -= _cost;
            updateDic.Add("/_gold/", _userData._gold);
        }
        
        DBManager.INSTANCE.UpdateFirebase(updateDic);
        _userData.UpdateCountStateList(true, _aquariumNumber);
        _aquariumUI.CheckAquariumPossessState();
        _aquariumUI.UpdateMoney();
        _fishInfoUI.SelectAquariumUIUpdate();
        gameObject.SetActive(false);
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
