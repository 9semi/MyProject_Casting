using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AquariumFishInfoSlot : MonoBehaviour
{    
    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    [Header("텍스트")]
    public Text _gradeText;
    public Text _lengthText;
    public Text _weightText;
    public Text _priceText;

    FishInfoUI _fishInfoUI;

    PublicDefined.stFishInfo _stFishInfo;

    public void InitFishInfoSlot(PublicDefined.stFishInfo stFishInfo, FishInfoUI instance)
    {
        _fishInfoUI = instance;
        _stFishInfo = stFishInfo;
        FishTypeToColor(stFishInfo._fishType);
        FishTypeToKorean(stFishInfo._fishType);
        _lengthText.text = stFishInfo._length.ToString("N2");
        _weightText.text = stFishInfo._weight.ToString("N2");
        _priceText.text = stFishInfo._price.ToString();
        gameObject.SetActive(true);
    }

    public void OffSlot()
    {
        gameObject.SetActive(false);
    }

    void FishTypeToColor(int type)
    {
        switch(type)
        {
            case 0:
                _gradeText.color = _sundryColor;
                _lengthText.color = _sundryColor;
                _weightText.color = _sundryColor;
                _priceText.color = _sundryColor;
                break;
            case 1:
                _gradeText.color = _normalColor;
                _lengthText.color = _normalColor;
                _weightText.color = _normalColor;
                _priceText.color = _normalColor;
                break;
            case 2:
                _gradeText.color = _rareColor;
                _lengthText.color = _rareColor;
                _weightText.color = _rareColor;
                _priceText.color = _rareColor;
                break;
        }
    }
    void FishTypeToKorean(int type)
    {
        switch(type)
        {
            case 0:
                _gradeText.text = "흔한";
                break;
            case 1:
                _gradeText.text = "보통";
                break;
            case 2:
                _gradeText.text = "희귀한";
                break;
        }    
    }

    public void ClickShiftButton(int listIndex) // 여기서 물고기 정보를 보내줘야 하므로 시작은 이곳이다.
    {
        _fishInfoUI.ClickShiftButton(listIndex, _stFishInfo);
    }

    public void ClickSellButton(int listIndex)
    {
        _fishInfoUI.ClickSellButton(listIndex, _stFishInfo);
    }

}
