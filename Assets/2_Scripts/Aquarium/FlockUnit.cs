using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    [HideInInspector] public Transform _myTransform { get; set; }
    [HideInInspector] public bool _stop { get; set; }

    public float _fovAngle;
    public float _smoothDamp;
    public LayerMask _obstacleMask;
    public Vector3[] _directionToCheckWhenAvoidingObstacles;
    public bool _allowRotate = true;

    List<FlockUnit> _cohesionNeighbours = new List<FlockUnit>(); // 주변의 화합, 결합, 응집력
    List<FlockUnit> _avoidenceNeighbours = new List<FlockUnit>(); // 
    List<FlockUnit> _aligementNeighbours = new List<FlockUnit>(); // 
    Flocking _assignedFlock; // 할당된 무리
    Vector3 _currentVelocity;
    Vector3 _currentObstacleAvoidanceVector;
    float _speed;

    private void Awake()
    {
        _myTransform = transform;
        _stop = true;
    }

    public void AssignFlock(Flocking flock)
    {
        _assignedFlock = flock;
    }

    public void StopRun()
    {
        _assignedFlock.IsFlocking = false;
    }

    public void InitSpeed(float speed)
    {
        _speed = speed;
    }

    public void MoveUnit()
    {
        FindNeighbours();
        CalculateSpeed();

        Vector3 cohesionVector = CalculateCohesionVector() * _assignedFlock._cohesionWeight; // 응집
        Vector3 avoidanceVector = CalculateAvoidenceVector() * _assignedFlock._avoidanceWeight; // 기피
        Vector3 aligementVector = CalculateAligementVector() * _assignedFlock._aligementWeight; // 정렬
        Vector3 boundsVector = CalculateBoundsVector() * _assignedFlock._boundsWeight;
        Vector3 obstacleVector = CalculateObstacleVector() * _assignedFlock._obstacleWeight;

        Vector3 moveVector = cohesionVector + avoidanceVector + aligementVector + boundsVector + obstacleVector;

        moveVector = Vector3.SmoothDamp(_myTransform.forward, moveVector, ref _currentVelocity, _smoothDamp);
        moveVector = moveVector.normalized * _speed;

        if (moveVector == Vector3.zero)
            moveVector = transform.forward;

        if(_allowRotate)
            _myTransform.forward = moveVector;

        _myTransform.position += moveVector * Time.deltaTime;
    }

    private Vector3 CalculateObstacleVector()
    {
        Vector3 obstacleVector = Vector3.zero;

        RaycastHit hit;
        // 만약 _assignedFlock._obstacleDistance만큼 내 앞에 장애물이 있다면 최적의 길을 찾는다.
        if (Physics.Raycast(_myTransform.position, _myTransform.forward, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
        {
            obstacleVector = FindBestDircetionToAvoidObstacle();
        }
        else
        {
            _currentObstacleAvoidanceVector = Vector3.zero;
        }
        return obstacleVector;
    }

    private Vector3 FindBestDircetionToAvoidObstacle()
    {
        if(_currentObstacleAvoidanceVector != Vector3.zero)
        { 
            RaycastHit hit;
            if(Physics.Raycast(_myTransform.position, _myTransform.forward, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
            {
                return _currentObstacleAvoidanceVector;
            }
        }
        float maxDistance = int.MinValue;
        Vector3 direction = Vector3.zero;
        for (int i = 0; i < _directionToCheckWhenAvoidingObstacles.Length; i++)
        {
            RaycastHit hit;
            Vector3 currentDirection = _myTransform.TransformDirection(_directionToCheckWhenAvoidingObstacles[i].normalized);

            if(Physics.Raycast(_myTransform.position, currentDirection, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
            {
                float currentDistance = (hit.point - _myTransform.position).sqrMagnitude;
                if(currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    direction = currentDirection;
                }
            }
            else
            {
                direction = currentDirection;
                _currentObstacleAvoidanceVector = currentDirection.normalized;
                return direction; 
            }
        }
        return direction.normalized;
    }

    Vector3 CalculateCohesionVector()
    {
        Vector3 cohesionVector = Vector3.zero;

        if (_cohesionNeighbours.Count.Equals(0))
        {
            return cohesionVector;
        }

        int neighboursInFov = 0;

        for (int i = 0; i < _cohesionNeighbours.Count; i++)
        {
            // 주변에 있는 오브젝트의 각도를 전부 확인해서 내가 정해놓은 각도 안에 있는 오브젝트를 선별한다.
            if (IsInFOV(_cohesionNeighbours[i]._myTransform.position))
            {
                neighboursInFov++;
                cohesionVector += _cohesionNeighbours[i]._myTransform.position;
            }
        }

        cohesionVector /= neighboursInFov; // 평균
        cohesionVector -= _myTransform.position; // 무리 쪽을 바라보게
        cohesionVector = cohesionVector.normalized;
        return cohesionVector;
    }

    private Vector3 CalculateAligementVector()
    {
        Vector3 aligementVector = _myTransform.forward;
        if (_aligementNeighbours.Count.Equals(0))
            return aligementVector;

        int neighbourInFov = 0;

        for (int i = 0; i < _aligementNeighbours.Count; i++) 
        {
            if(IsInFOV(_aligementNeighbours[i]._myTransform.position))
            {
                neighbourInFov++;
                aligementVector += _aligementNeighbours[i]._myTransform.forward;
            }
        }

        aligementVector /= neighbourInFov;
        aligementVector = aligementVector.normalized;
        return aligementVector;
    }

    private Vector3 CalculateAvoidenceVector()
    {
        Vector3 avoidenceVector = Vector3.zero;

        if (_avoidenceNeighbours.Count.Equals(0))
            return Vector3.zero;

        int neighbourInFov = 0;

        for (int i = 0; i < _avoidenceNeighbours.Count; i++)
        {
            if (IsInFOV(_avoidenceNeighbours[i]._myTransform.position))
            {
                neighbourInFov++;
                avoidenceVector += (_myTransform.position - _avoidenceNeighbours[i]._myTransform.position);
            }
        }

        avoidenceVector /= neighbourInFov;
        avoidenceVector = avoidenceVector.normalized;
        return avoidenceVector;
    }

    private Vector3 CalculateBoundsVector()
    {
        Vector3 offsetToCenter = _assignedFlock.transform.position - _myTransform.position;
        bool isNearCenter = (offsetToCenter.magnitude >= _assignedFlock._boundsDistance * 0.9f);
        return isNearCenter ? offsetToCenter.normalized : Vector3.zero;

    }

    void CalculateSpeed()
    {
        //if (_cohesionNeighbours.Count.Equals(0))
        //    return;

        //_speed = 0;

        //for (int i = 0; i< _cohesionNeighbours.Count; i++)
        //{
        //    _speed += _cohesionNeighbours[i]._speed;
        //}
        //_speed /= _cohesionNeighbours.Count;
        _speed = Mathf.Clamp(_speed, _assignedFlock._minSpeed, _assignedFlock._maxSpeed);
    }

    void FindNeighbours()
    {
        _cohesionNeighbours.Clear();
        _avoidenceNeighbours.Clear();
        _aligementNeighbours.Clear();

        FlockUnit[] allUnits = _assignedFlock.AllFishObject;

        for(int i = 0; i < allUnits.Length; i++)
        {
            FlockUnit currentUnit = allUnits[i];

            if(currentUnit != this)
            {
                // 두 트랜스폼 간의 거리 측정
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);

                // 나와 상대의 거리
                if (currentNeighbourDistanceSqr <= _assignedFlock._cohesionDistance * _assignedFlock._cohesionDistance)
                {
                    _cohesionNeighbours.Add(currentUnit);
                }

                if (currentNeighbourDistanceSqr <= _assignedFlock._avoidanceDistance * _assignedFlock._avoidanceDistance)
                {
                    _avoidenceNeighbours.Add(currentUnit);
                }

                if (currentNeighbourDistanceSqr <= _assignedFlock._aligementDistance * _assignedFlock._aligementDistance)
                {
                    _aligementNeighbours.Add(currentUnit);
                }
            }
        }
    }



    bool IsInFOV(Vector3 neighbourPosition)
    {                                                                                           // 감지 영역
        return Vector3.Angle(_myTransform.forward, neighbourPosition - _myTransform.position) <= _fovAngle;
    }
}
