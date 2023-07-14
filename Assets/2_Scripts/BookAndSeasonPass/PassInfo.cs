using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PassInfo
{
    public enum eFreeRewardType
    {
        Gold,
        Bait,
    }
    public eFreeRewardType _type;
    public string _questContent;
    public int _freeGold;
    public int _freeBaitDBNumber;
    public int _freeBaitQuantity;
    public int _premiumGold;

}
