using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    [Header("Aquarium Manager")]
    [SerializeField] AquariumManager _aquariumManamger;

    [Header("스폰 설정")]
    [SerializeField] FlockUnit _fishObject;
    [SerializeField] Vector3 _spawnBounds;

    [Header("속도 설정")]
    [Range(0, 10)] public float _minSpeed;
    [Range(0, 10)] public float _maxSpeed;

    [Header("탐지 거리")]
    [Range(0, 10)] public float _cohesionDistance;
    [Range(0, 10)] public float _avoidanceDistance;
    [Range(0, 10)] public float _aligementDistance;
    [Range(0, 100)] public float _boundsDistance;
    [Range(0, 10)] public float _obstacleDistance;

    [Header("행동 무게")]
    [Range(0, 10)] public float _cohesionWeight;
    [Range(0, 10)] public float _avoidanceWeight;
    [Range(0, 10)] public float _aligementWeight;
    [Range(0, 10)] public float _boundsWeight;
    [Range(0, 100)] public float _obstacleWeight;

    [SerializeField] List<Transform> _parents; public List<Transform> Parents { get { return _parents; } }


    FlockUnit[] _allFishObject; public FlockUnit[] AllFishObject{ get { return _allFishObject; } set { _allFishObject = value; } }

    bool _isFlocking = false; public bool IsFlocking { get { return _isFlocking; }  set { _isFlocking = value; } }

    private void Update()
    {
        if (_isFlocking)
        {
            for (int i = 0; i < _allFishObject.Length; i++)
            {
                _allFishObject[i].MoveUnit();
            }
        }
    }

    public void InitFishs(int aquariumNumber, Dictionary<int, List<PublicDefined.stFishInfo>> dic)
    {
        if (dic.Count.Equals(0))
        {
            _parents[aquariumNumber].gameObject.SetActive(true);
            return;
        }
        int objectSize = 0;
        for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
        {
            objectSize += _parents[aquariumNumber].GetChild(i).childCount;
        }
        int flockSize = 0;
        foreach (List<PublicDefined.stFishInfo> value in dic.Values)
        {
            flockSize += value.Count;
        }
        if (_aquariumManamger.CheckExistFishs[aquariumNumber])
        {
            _parents[aquariumNumber].gameObject.SetActive(true);
            _allFishObject = new FlockUnit[objectSize];
            int fishCnt = 0;
            
            for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
            {
                for (int k = 0; k < _parents[aquariumNumber].GetChild(i).childCount; k++)
                {
                    _allFishObject[fishCnt] = _parents[aquariumNumber].GetChild(i).GetChild(k).GetComponent<FlockUnit>();
                    fishCnt++;
                }
            }

            _isFlocking = true;
            return;
        }

        int cnt = 0;

        _parents[aquariumNumber].gameObject.SetActive(true);
        _allFishObject = new FlockUnit[flockSize];

        foreach (List<PublicDefined.stFishInfo> data in dic.Values)
        {
            if (data.Count <= 0)
                continue;

            int fishNumber = data[0]._fishNumber;

            Transform p = _parents[aquariumNumber].Find(_aquariumManamger.AquariumFishDB[fishNumber]._engName);
            bool isExist = p == null ? false : true;
            
            if (isExist)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
                    randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);
                    Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                    _allFishObject[cnt] = Instantiate(_aquariumManamger.AquariumFishDB[fishNumber]._prefab.GetComponent<FlockUnit>(), randomVector, rotation, p);
                    float scaleValue = data[i]._length * 0.02f;
                    if (scaleValue < 1.2f)
                        scaleValue = 1.2f;
                    _allFishObject[cnt].transform.localScale *= scaleValue;
                    _allFishObject[cnt].AssignFlock(this);
                    _allFishObject[cnt].InitSpeed(UnityEngine.Random.Range(_minSpeed, _maxSpeed));
                    cnt++;
                }
            }
            else
            {
                GameObject parent = new GameObject();
                parent.transform.SetParent(_parents[aquariumNumber]);
                parent.name = _aquariumManamger.AquariumFishDB[fishNumber]._engName;

                for (int i = 0; i < data.Count; i++)
                {
                    Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
                    randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);
                    Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

                    _allFishObject[cnt] = Instantiate(_aquariumManamger.AquariumFishDB[fishNumber]._prefab.GetComponent<FlockUnit>(), randomVector, rotation, parent.transform);
                    float scaleValue = data[i]._length * 0.02f;
                    if (scaleValue < 1.2f)
                        scaleValue = 1.2f;
                    _allFishObject[cnt].transform.localScale *= scaleValue;
                    _allFishObject[cnt].AssignFlock(this);
                    _allFishObject[cnt].InitSpeed(UnityEngine.Random.Range(_minSpeed, _maxSpeed));
                    cnt++;
                }
            }
        }
        _aquariumManamger.CheckExistFishs[aquariumNumber] = true;
        _isFlocking = true;
    }

    public void UpdateAllFishObject(int aquariumNumber)
    {
        int objectSize = 0;
        for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
        {
            objectSize += _parents[aquariumNumber].GetChild(i).childCount;
        }

        _parents[aquariumNumber].gameObject.SetActive(true);
        _allFishObject = new FlockUnit[objectSize];

        int fishCnt = 0;

        for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
        {
            for (int k = 0; k < _parents[aquariumNumber].GetChild(i).childCount; k++)
            {
                _allFishObject[fishCnt] = _parents[aquariumNumber].GetChild(i).GetChild(k).GetComponent<FlockUnit>();
                fishCnt++;
            }
        }
        _isFlocking = true;
    }

    public void ResetPosition()
    {
        _isFlocking = false;

        for (int i = 0; i < _allFishObject.Length; i++)
        {
            Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);

            _allFishObject[i].transform.localPosition = randomVector;
        }

        _isFlocking = true;
    }

    public List<Transform> GetParents()
    {
        return _parents;
    }
}
