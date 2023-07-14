using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{    
    //아이템 타입
    public PublicDefined.eItemType Type;

    // 아이템 파이베이스로 불러올라고 구분지어놓은 시리얼 번호
    public int serialNumber;

    // 같은 종류의 아이템일 때 상세적인 분류할라고 놓은 번호
    public int typeNum;

    // 미끼 장착할 때 쓰는 번호
    public int baitNum;

    // 아이템 갯수(소비아이템만 적용)
    public int quantity;

    // 아이템 한글 이름
    public string korName;

    // 아이템 정보
    public string itemInfo;

    //조개 포인트-게임머니(재화)
    public int goldCost;

    //크리스탈 포인트 - 게임머니,현금(재화)
    public int pearlCost;

    public int cashCost;

    // 낚싯대 강도
    public float intensive;

    //재화의 이미지를 나타냄
    public Sprite costImg;

    //아이템 사진
    public Sprite itemImage;

    // 봉돌 무게
    public float _sinkerWeight;

    //미끼 효과
    //public List<ItemEffect> baitEfts;

    //떡밥 효과
    //public List<ItemEffect> whiteLeadEfts;

    // 낚싯대 재질(색깔)
    public Material rodMaterial;
}