using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoFishSlot : MonoBehaviour
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _lengthText;
    [SerializeField] Text _weightText;

    int _DBNum;
    int _price;
    float _length;
    float _weight;
    string _name;
    string _key;
    int _type;
    Color _typeColor;

    MyInfoUI _myInfoUI;

    public void Init(MyInfoUI instance, Color textColor, int type, string name, float length, float weight, int price, int DBNum, string key)
    {
        _typeColor = textColor;
        _myInfoUI = instance;
        _DBNum = DBNum;
        _price = price;
        _length = length;
        _weight = weight;
        _name = name;
        _key = key;
        _type = type;
        _nameText.color = _typeColor;
        _lengthText.color = _typeColor;
        _weightText.color = _typeColor;

        _nameText.text = name;
        _lengthText.text = length.ToString("N2");
        _weightText.text = weight.ToString("N2");

        gameObject.SetActive(true);
    }

    public void ClickSelectButton()
    {
        _myInfoUI.SetRepresentFish(_typeColor, _type, _DBNum, _length, _weight, _price, _name, _key);
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileSelect).GetComponent<AudioPoolObject>().Init();
    }
}
