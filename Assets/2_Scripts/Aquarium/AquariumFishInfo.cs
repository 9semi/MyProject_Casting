using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumFishInfo : MonoBehaviour
{
    public int _fishNumber; // DB ¹øÈ£
    public string _name;
    public float _length;
    public float _weight;
    public int _price;
    public PublicDefined.eFishType _fishType;

    public AquariumFishInfo(int DBNumber, string name, float length, float weight, int price, PublicDefined.eFishType fishType)
    {
        _fishNumber = DBNumber;
        _name = name;
        _length = length;
        _weight = weight;
        _price = price;
        _fishType = fishType;
    }
}
