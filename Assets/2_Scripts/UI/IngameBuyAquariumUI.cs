using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameBuyAquariumUI : MonoBehaviour
{
    public GameObject _moneyObject;
    public GameObject _cashObject;

    public GameObject[] _cashBuyButtonObject;

    public Text _costText;
    public Text _cashCostText;
    public Text _currentMoneyText;
    public Text _aquariumNameText;

    public Sprite _goldSprite;
    public Sprite _cashSprite;

    public Image _moneyImage;
    public Image _aquariumImage;

    public Button _buyButton;

    public GameObject _buyCheckUI;

    UserData _userData;
    int _aquariumNumber;
    bool _isCash;
    int _cost;
    FishPopUp _fishpop;
    public void Init(bool isCash, int cost, int aquariumNumber, Sprite aquariumSprite, UserData userData, FishPopUp fishpop)
    {
        _aquariumNumber = aquariumNumber;
        _userData = userData;
        _isCash = isCash;
        _cost = cost;
        _fishpop = fishpop;
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

        // ¼öÁ·°üÀÇ °ªÀÌ ÁøÁÖ¶ó¸é
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
        _buyCheckUI.SetActive(true);
    }

    public void ClickOKButton()
    {
        Dictionary<string, object> updateDic = new Dictionary<string, object>();

        switch (_aquariumNumber)
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

        if (_isCash)
        {

        }
        else
        {
            _userData._gold -= _cost;
            updateDic.Add("/_gold/", _userData._gold);
        }

        DBManager.INSTANCE.UpdateFirebase(updateDic);
        _userData.UpdateCountStateList(true, _aquariumNumber);
        _fishpop.UpdateAquariumState();
        gameObject.SetActive(false);
    }

    public void ClickXButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
}
