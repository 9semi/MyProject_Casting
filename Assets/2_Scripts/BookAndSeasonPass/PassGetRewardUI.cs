using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassGetRewardUI : MonoBehaviour
{
    public Image _itemImage;
    public Text _itemText;

    public void Init(Sprite itemSprite, string text)
    {
        _itemImage.sprite = itemSprite;
        _itemText.text = text;
        gameObject.SetActive(true);
    }
}
