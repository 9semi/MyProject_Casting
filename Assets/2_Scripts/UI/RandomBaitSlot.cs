using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBaitSlot : MonoBehaviour
{
    public Image _itemImage;
    public GameObject _itemInfoObject;
    public Text _itemText;
    public Text _itemInfoText;

    public void Init(Sprite sprite, string name, string quantity, string info)
    {
        _itemImage.sprite = sprite;
        _itemText.text = name + " X " + quantity;
        _itemInfoText.text = info;

        InitItemInfo();

        gameObject.SetActive(true);
    }

    public void ClickItemImage()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _itemImage.gameObject.SetActive(false);
        _itemInfoObject.SetActive(true);
    }

    public void ClickItemInfo()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        _itemImage.gameObject.SetActive(true);
        _itemInfoObject.SetActive(false);
    }

    void InitItemInfo()
    {
        _itemImage.gameObject.SetActive(true);
        _itemInfoObject.SetActive(false);
    }
}
