using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ItemData : MonoBehaviour
{
    static ItemData _uniqueInstance;
    static public ItemData Instance
    {
        get { return _uniqueInstance; }
    }


    // 패키지 아이템 리스트
    public List<Item> packageItemDB = new List<Item>();
    public List<Item> passItemDB = new List<Item>();

    // 낚싯대 아이템 리스트
    public List<Item> rodItemDB = new List<Item>();

    // 릴 아이템 리스트
    public List<Item> reelItemDB = new List<Item>();

    // 미끼 아이템 리스트
    public List<Item> baitItemDB = new List<Item>();

    // 떡밥
    public List<Item> pasetbaitItemDB = new List<Item>();

    // 찌 아이템 리스트
    public List<Item> floatItemDB = new List<Item>();

    // 봉돌 아이템 리스트
    public List<Item> sinkerItemDB = new List<Item>();

    // 각 맵마다 미끼의 시리얼 넘버만 가지고 있으면 상점에서 미끼 뽑는 것 해결.
    public List<int> _jeongdongjinBaitSerialNumberList = new List<int>();
    public List<int> _skywayBaitSerialNumberList = new List<int>();
    public List<int> _homerspitBaitSerialNumberList = new List<int>();

    public List<Item> _baitBoxItemDB = new List<Item>();

    private void Awake()
    {
        _uniqueInstance = this;
    }

    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }

    public ItemData GetInstance()
    {
        return this;
    }
}