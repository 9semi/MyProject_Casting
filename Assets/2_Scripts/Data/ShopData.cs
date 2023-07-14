using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopData : MonoBehaviour
{
    [HideInInspector] public List<Item> passStocks = new List<Item>();
    [HideInInspector] public List<Item> rodStocks = new List<Item>();
    [HideInInspector] public List<Item> reelStocks = new List<Item>();
    [HideInInspector] public List<Item> baitStocks = new List<Item>();
    [HideInInspector] public List<Item> pastebaitStocks = new List<Item>();
    [HideInInspector] public List<Item> float_sinkerStocks = new List<Item>();

    private void Start()
    {
        int i;
        //패스 아이템 데이터 받아옴
        for (i = 0; i < ItemData.Instance.passItemDB.Count; i++)
        {
            //패스 아이템 리스트 추가
            passStocks.Add(ItemData.Instance.passItemDB[i]);
        }

        //낚싯대 아이템 데이터 받아옴
        for (i = 0; i < ItemData.Instance.rodItemDB.Count; i++)
        {
            //낚싯대 아이템 리스트 추가
            rodStocks.Add(ItemData.Instance.rodItemDB[i]);
        }

        //릴 아이템 데이터 받아옴
        for (i = 0; i < ItemData.Instance.reelItemDB.Count; i++)
        {
            //릴 아이템 리스트 추가
            reelStocks.Add(ItemData.Instance.reelItemDB[i]);
        }

        //미끼 아이템 데이터 받아옴
        for (i = 0; i < ItemData.Instance.baitItemDB.Count; i++)
        {
            //미끼 아이템 리스트 추가
            baitStocks.Add(ItemData.Instance.baitItemDB[i]);
        }

        for(i = 0; i< ItemData.Instance.pasetbaitItemDB.Count; i++)
        {
            pastebaitStocks.Add(ItemData.Instance.pasetbaitItemDB[i]);
        }

        //찌, 봉돌 아이템 데이터 받아옴
        //for (i = 0; i < ItemData.Instance._float_sinkerItemDB.Count; i++)
        //{
        //    //찌, 봉돌 아이템 리스트 추가
        //    float_sinkerStocks.Add(ItemData.Instance._float_sinkerItemDB[i]);
        //}       
    }
}