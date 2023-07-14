using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBase : MonoBehaviour
{ 
    public struct stRareFish
    {
        public Transform transform;
        public Sprite image;
    }

    public struct stFishData
    {
        public GameObject _fishObject;
        public Transform _fishTransform;
        public int _activityType;
        public int _motorType;
        public float _lenth;
        public float _weight;
        public int _price;
        public string _name;
        public string[] _info;
        public PublicDefined.eFishType _gradeType;
        public int _fishDBNumber;

        public stFishData(GameObject fishObj, Transform fishTr, int activityType, int motorType, float lenth, float weight, int price, string name,
                            string[] info, PublicDefined.eFishType gradeType, int fishDBNum)
        {
            _fishObject = fishObj;
            _fishTransform = fishTr;
            _activityType = activityType;
            _motorType = motorType;
            _lenth = lenth;
            _weight = weight;
            _price = price;
            _name = name;
            _info = info;
            _gradeType = gradeType;
            _fishDBNumber = fishDBNum;
        }
    }
}
