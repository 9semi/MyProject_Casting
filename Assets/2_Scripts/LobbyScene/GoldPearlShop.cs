using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPearlShop : MonoBehaviour
{
    public Sprite _selectedSprite;
    public Sprite _unselectSprite;

    public Image _goldButtonImage;
    public Image _pearlButtonImage;

    public GameObject _goldProductObject;
    public GameObject _pearlProductObject;

    private void OnEnable()
    {
        ClickButton(0);
    }

    public void ClickButton(int number)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        if (number.Equals(0))
        {
            _goldButtonImage.sprite = _selectedSprite;
            _pearlButtonImage.sprite = _unselectSprite;
            _goldProductObject.SetActive(true);
            _pearlProductObject.SetActive(false);
        }
        else
        {
            _goldButtonImage.sprite = _unselectSprite;
            _pearlButtonImage.sprite = _selectedSprite;
            _goldProductObject.SetActive(false);
            _pearlProductObject.SetActive(true);
        }
    }
    public void ClickXButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
}
