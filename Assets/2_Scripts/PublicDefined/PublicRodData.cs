using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PublicRodData : MonoBehaviour
{
    public PublicDefined.eItemType _type;
    public int _serialNumber;
    public string _korName;
    public string _itemInfo;
    public int _goldCost;
    public int _pearlCost;
    public int _intensive;
    public Sprite _costImage;
    public Sprite _itemImage;
    public Material _rodMaterial;
}
