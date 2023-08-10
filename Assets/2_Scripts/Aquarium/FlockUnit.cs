using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    bool _stop;

    [SerializeField] float _fovAngle;
    [SerializeField] float _smoothDamp;
    [SerializeField] LayerMask _obstacleMask;
    [SerializeField] Vector3[] _directionToCheckWhenAvoidingObstacles;
    [SerializeField] bool _allowRotate = true;

    List<FlockUnit> _cohesionNeighbours = new List<FlockUnit>();
    List<FlockUnit> _avoidenceNeighbours = new List<FlockUnit>();
    List<FlockUnit> _aligementNeighbours = new List<FlockUnit>(); 
    Flocking _assignedFlock;
    Vector3 _currentVelocity;
    Vector3 _currentObstacleAvoidanceVector;
    float _speed;

    private void Awake()
    {
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

        Vector3 cohesionVector = CalculateCohesionVector() * _assignedFlock._cohesionWeight; 
        Vector3 avoidanceVector = CalculateAvoidenceVector() * _assignedFlock._avoidanceWeight;
        Vector3 aligementVector = CalculateAligementVector() * _assignedFlock._aligementWeight;
        Vector3 boundsVector = CalculateBoundsVector() * _assignedFlock._boundsWeight;
        Vector3 obstacleVector = CalculateObstacleVector() * _assignedFlock._obstacleWeight;

        Vector3 moveVector = cohesionVector + avoidanceVector + aligementVector + boundsVector + obstacleVector;

        moveVector = Vector3.SmoothDamp(transform.forward, moveVector, ref _currentVelocity, _smoothDamp);
        moveVector = moveVector.normalized * _speed;

        if (moveVector == Vector3.zero)
            moveVector = transform.forward;

        if(_allowRotate)
            transform.forward = moveVector;

        transform.position += moveVector * Time.deltaTime;
    }

    private Vector3 CalculateObstacleVector()
    {
        Vector3 obstacleVector = Vector3.zero;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
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
            if(Physics.Raycast(transform.position, transform.forward, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
            {
                return _currentObstacleAvoidanceVector;
            }
        }
        float maxDistance = int.MinValue;
        Vector3 direction = Vector3.zero;
        for (int i = 0; i < _directionToCheckWhenAvoidingObstacles.Length; i++)
        {
            RaycastHit hit;
            Vector3 currentDirection = transform.TransformDirection(_directionToCheckWhenAvoidingObstacles[i].normalized);

            if(Physics.Raycast(transform.position, currentDirection, out hit, _assignedFlock._obstacleDistance, _obstacleMask))
            {
                float currentDistance = (hit.point - transform.position).sqrMagnitude;
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
            if (IsInFOV(_cohesionNeighbours[i].transform.position))
            {
                neighboursInFov++;
                cohesionVector += _cohesionNeighbours[i].transform.position;
            }
        }
    
        cohesionVector /= neighboursInFov;
        cohesionVector -= transform.position; 
        cohesionVector = cohesionVector.normalized;
        return cohesionVector;
    }

    private Vector3 CalculateAligementVector()
    {
        Vector3 aligementVector = transform.forward;
        if (_aligementNeighbours.Count.Equals(0))
            return aligementVector;

        int neighbourInFov = 0;

        for (int i = 0; i < _aligementNeighbours.Count; i++) 
        {
            if(IsInFOV(_aligementNeighbours[i].transform.position))
            {
                neighbourInFov++;
                aligementVector += _aligementNeighbours[i].transform.forward;
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
            if (IsInFOV(_avoidenceNeighbours[i].transform.position))
            {
                neighbourInFov++;
                avoidenceVector += (transform.position - _avoidenceNeighbours[i].transform.position);
            }
        }

        avoidenceVector /= neighbourInFov;
        avoidenceVector = avoidenceVector.normalized;
        return avoidenceVector;
    }

    private Vector3 CalculateBoundsVector()
    {
        Vector3 offsetToCenter = _assignedFlock.transform.position - transform.position;
        bool isNearCenter = (offsetToCenter.magnitude >= _assignedFlock._boundsDistance * 0.9f);
        return isNearCenter ? offsetToCenter.normalized : Vector3.zero;

    }

    void CalculateSpeed()
    {
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
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);
                
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
    {                                                        
        return Vector3.Angle(transform.forward, neighbourPosition - transform.position) <= _fovAngle;
    }
}
