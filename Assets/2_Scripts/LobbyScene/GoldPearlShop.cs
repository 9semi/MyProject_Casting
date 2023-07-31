using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPearlShop : MonoBehaviour
{
    [SerializeField] Sprite _selectedSprite;
    [SerializeField] Sprite _unselectSprite;

    [SerializeField] Image _goldButtonImage;
    [SerializeField] Image _pearlButtonImage;

    [SerializeField] GameObject _goldProductObject;
    [SerializeField] GameObject _pearlProductObject;

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
