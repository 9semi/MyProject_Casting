using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishRankSlot : MonoBehaviour
{
    public Image[] _bgImages;
    public Image _fishImage;
    public Text _nameText;
    public Text _lengthText;
    public Text _weightText;

    public GameObject _trueObject;
    public GameObject _falseObject;

    public void InitTrue(Color c, Sprite fishSprite, string n, float l, float w)
    {
        _bgImages[0].color = c;
        _bgImages[1].color = c;
        _bgImages[2].color = c;
        _fishImage.sprite = fishSprite;
        _nameText.text = n;
        _lengthText.text = l.ToString("N2");
        _weightText.text = w.ToString("N2");
        _trueObject.SetActive(true);
    }
    public void InitFalse(Sprite fishSprite, string n)
    {
        _fishImage.sprite = fishSprite;
        _nameText.text = n;
        _falseObject.SetActive(true);
    }
}
