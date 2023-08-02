using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishRankSlot : MonoBehaviour
{
    [SerializeField] Image[] _bgImages;
    [SerializeField] Image _fishImage;
    [SerializeField] Text _nameText;
    [SerializeField] Text _lengthText;
    [SerializeField] Text _weightText;
    [SerializeField] GameObject _trueObject;
    [SerializeField] GameObject _falseObject;

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
