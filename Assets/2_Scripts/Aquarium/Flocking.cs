﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    [Header("Aquarium Manager")]
    public AquariumManager _aquariumManamger;

    [Header("스폰 설정")]
    [HideInInspector] public FlockUnit _fishObject;
    public Vector3 _spawnBounds;

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

    public List<Transform> _parents;


    [HideInInspector] public FlockUnit[] _allFishObject { set; get; }

    [HideInInspector] public bool _run = false;

    private void Update()
    {
        if (_run)
        {
            for (int i = 0; i < _allFishObject.Length; i++)
            {
                _allFishObject[i].MoveUnit();
            }
        }
    }

    public void InitFishs(int aquariumNumber, Dictionary<int, List<PublicDefined.stFishInfo>> dic)
    {
        // Dictionary에 아무것도 없다면 배경만 보여준다.
        if (dic.Count.Equals(0))
        {
            _parents[aquariumNumber].gameObject.SetActive(true);
            return;
        }
        // 현재 오브젝트로 생성된 오브젝트 카운트
        int objectSize = 0;
        for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
        {
            objectSize += _parents[aquariumNumber].GetChild(i).childCount;
        }
        // 딕셔너리에 저장된 물고기 카운트
        int flockSize = 0;
        foreach (List<PublicDefined.stFishInfo> value in dic.Values)
        {
            flockSize += value.Count;
        }
        // 만약 이미 물고기들이 있다면 활성화 시키고 return
        if (_aquariumManamger._checkExistFishs[aquariumNumber])
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

            _run = true;
            return;
        }

        int cnt = 0;

        _parents[aquariumNumber].gameObject.SetActive(true);
        _allFishObject = new FlockUnit[flockSize];

        foreach (List<PublicDefined.stFishInfo> data in dic.Values)
        {
            // PublicDefined.stFishInfo> : 번호, 이름, 길이, 무게, 타입
            if (data.Count <= 0)
                continue;

            int fishNumber = data[0]._fishNumber;

            Transform p = _parents[aquariumNumber].Find(_aquariumManamger._aquariumFishDB[fishNumber]._engName);
            bool isExist = p == null ? false : true;

            // 물고기 부모가 없다면 만든다.
            if (isExist)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
                    randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);
                    Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                    _allFishObject[cnt] = Instantiate(_aquariumManamger._aquariumFishDB[fishNumber]._prefab.GetComponent<FlockUnit>(), randomVector, rotation, p);
                    float scaleValue = data[i]._length * 0.02f;
                    if (scaleValue < 1.2f)
                        scaleValue = 1.2f;
                    _allFishObject[cnt].transform.localScale *= scaleValue;
                    _allFishObject[cnt].AssignFlock(this);
                    _allFishObject[cnt].InitSpeed(UnityEngine.Random.Range(_minSpeed, _maxSpeed));
                    cnt++;
                }
            }
            // 물고기 부모가 이미 있다면 부모 아래에 생성한다.
            else
            {
                GameObject parent = new GameObject();
                parent.transform.SetParent(_parents[aquariumNumber]);
                parent.name = _aquariumManamger._aquariumFishDB[fishNumber]._engName;

                for (int i = 0; i < data.Count; i++)
                {
                    Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
                    randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);
                    Quaternion rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

                    _allFishObject[cnt] = Instantiate(_aquariumManamger._aquariumFishDB[fishNumber]._prefab.GetComponent<FlockUnit>(), randomVector, rotation, parent.transform);
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
        _aquariumManamger._checkExistFishs[aquariumNumber] = true;
        _run = true;
    }

    public void UpdateAllFishObject(int aquariumNumber)
    {
        // 현재 오브젝트로 생성된 오브젝트 카운트
        int objectSize = 0;
        for (int i = 0; i < _parents[aquariumNumber].childCount; i++)
        {
            objectSize += _parents[aquariumNumber].GetChild(i).childCount;
        }
        //Debug.Log(" objectSize = " + objectSize);
        // 만약 이미 물고기들이 있다면 활성화 시키고 return
        //if (_parents[aquariumNumber].childCount > 0)

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
        _run = true;
    }

    public void ResetPosition()
    {
        _run = false;

        for (int i = 0; i < _allFishObject.Length; i++)
        {
            Vector3 randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * _spawnBounds.x, randomVector.y * _spawnBounds.y, randomVector.z * _spawnBounds.z);

            _allFishObject[i].transform.localPosition = randomVector;
        }

        _run = true;
    }

    public List<Transform> GetParents()
    {
        return _parents;
    }
}