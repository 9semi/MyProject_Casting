using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
[Serializable]
public class BookArray
{
    public bool[] isCheck;
}

public class Book : MonoBehaviour
{
    public FishPopUp fishPopUp;
    public Text mapNameTxt,currentAmountTxt,fullAmountTxt;
    public Image mapIconSlot;
    public Image[] tab;
    public Sprite jenongdongjinIcon, sunshineSkywayIcon, homerSpitIcon, naWiliWiliIcon;
    public Sprite[] jeongdonjinFish, sunshineSkywayFish, alaskaFish;
    public BookSlot[] bookSlots;

    private void Start()
    {
        SelectMap(0);
    }

    // 도감 여는 함수
    public void ControlBook(bool isSet)
    {
        gameObject.SetActive(isSet);
    }


    // 맵에 따른 물고기 도감 선택하는 함수
    public void SelectMap(int mapNum)
    {
        //SoundManager.instance.EffectPlay("UIClick");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        int tabNum = 0;
        // 탭 이름으로 구분
        switch (mapNum)
        {
            // 패키지일 때 0
            case 0:
                tabNum = 0;
                SetSlot(3,jenongdongjinIcon,"Jeongdongjin",jeongdonjinFish);
                break;

            case 1:
                tabNum = 1;
                SetSlot(2,sunshineSkywayIcon,"SunshineSkyWay",sunshineSkywayFish);
                break;

            case 2:
                tabNum = 2;
                break;

            case 3:
                tabNum = 3;
                break;

            case 4:
                tabNum = 4;
                
                break;
        }

        for (int i = 0; i < tab.Length; i++)
        {
            tab[i].color = i == tabNum ? new Color(64/255f, 91/255f, 144/255f, 255/255f) : new Color(0f, 0f, 0f, 0f);
        }
    }

    void SetSlot(int currentAmount,Sprite icon,string mapName,Sprite[] fishSprites)
    {
        mapIconSlot.sprite = icon;
        mapNameTxt.text = mapName;
        currentAmountTxt.text = currentAmount.ToString();
        fullAmountTxt.text = fishSprites.Length.ToString();
        for (int i = 0; i < bookSlots.Length; i++)
        {
            bool isExist = i < fishSprites.Length;
            bookSlots[i].gameObject.SetActive(isExist);
            if (isExist)
            // 슬롯을 업데이트(사진, 이름표시)
            bookSlots[i].fishImage.sprite = fishSprites[i];
        }
    }
}