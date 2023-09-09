using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AquariumManager : MonoBehaviour
{
    [SerializeField] AquariumUI _aquariumUI;

    List<Transform> _fishObjectParentList = new List<Transform>();
    [SerializeField] Flocking _flock;

    [Header("물고기 정보")]
    [SerializeField] List<AquariumFishDB> _aquariumFishDB = new List<AquariumFishDB>();
    public List<AquariumFishDB> AquariumFishDB { get { return _aquariumFishDB; } }

    [Header("수족관 데코")]
    [SerializeField] GameObject[] _aquariumDecoObject;

    [Header("수족관을 보여줄 카메라")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Camera _cam; public Camera Cam { get { return _cam; } } 
    
    float _rotateY = 0;
    float _rotateX = 0;
    float _moveSpeed = 15f;
    int _fov = 0;
    
    float _befGap;
    float _aftGap;
    int _fovPower = 1;
    Vector2 _firstTouchPos, _secondTouchPos;
    
    [Header("비활성화 물고기")]
    public Transform _garbageObject;

    bool _isCameraRunning = false; public bool IsCameraRunning { set { _isCameraRunning = value; } }
    int _finalFov = 90; public int FinalFov { set { _finalFov = value; } }
    float _finalPositionY = 0; public float FinalPositionY { get { return _finalPositionY; } set { _finalPositionY = value; } }
    float _finalPositionX = 0; public float FinalPositionX { get { return _finalPositionX; } set { _finalPositionX = value; } }
    List<bool> _checkExistFishs = new List<bool>();
    public List<bool> CheckExistFishs { get { return _checkExistFishs; } set { _checkExistFishs = value; } }
    [SerializeField] FishInfoUI _fishInfoUI;
    
    private void Start()
    {
        if (!AudioManager.INSTANCE.CheckIsSameBGM(PublicDefined.eBGMType.lobbyscene))
            AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);

        _fishObjectParentList = _flock.Parents;
    }

    private void Update()
    {
        if (_isCameraRunning && !_fishInfoUI.gameObject.activeSelf)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (_aquariumUI.CurrentFishSlotState.Equals(AquariumUI.eFishSlotType.On))
                {
                    if (touch.position.y < 300)
                        return;
                }

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _firstTouchPos = touch.position - touch.deltaPosition;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _secondTouchPos = touch.position - touch.deltaPosition;
                        if (_firstTouchPos.x - _secondTouchPos.x < 0)
                            _finalPositionX += -_moveSpeed * Time.deltaTime;
                        else            
                            _finalPositionX += _moveSpeed * Time.deltaTime;

                        _finalPositionX = Mathf.Clamp(_finalPositionX, -30, 30);

                        _cameraTransform.position = new Vector3(_finalPositionX, 0, -12);
                        break;
                }
            }

            if(Input.touchCount == 2)
            {
                if (Input.touches[0].phase.Equals(TouchPhase.Moved) || Input.touches[1].phase.Equals(TouchPhase.Moved) || Input.touches
                    [1].phase.Equals(TouchPhase.Stationary))
                {
                    _aftGap = (Input.touches[0].position - Input.touches[1].position).magnitude;
                    
                    if(_aftGap > _befGap)
                        _finalFov -= _fovPower;
                    else
                        _finalFov += _fovPower;

                    _finalFov = Mathf.Clamp(_finalFov, 30, 110);
                    _cam.fieldOfView = _finalFov;
                    _befGap = _aftGap;
                }
            }
        }
    }

    private void CameraMove()
    {
        // 물고기를 확대하고 싶을 때 카메라의 위치를 앞으로 움직이면 이미지가 늘어난다. Field of View를 조절해야 할듯.

        //_finalRotateX += _rotateX * _rotateSpeed * Time.deltaTime;
        //_finalRotateX = Mathf.Clamp(_finalRotateX, -29, 29);
        //_finalRotateY += _rotateY * _rotateSpeed * Time.deltaTime;
        //_finalRotateY = Mathf.Clamp(_finalRotateY, -44, 44);
        //_cameraTransform.rotation = Quaternion.Euler(_finalRotateX, _finalRotateY, 0);

        //_finalFov += _fov;
        //_finalFov = Mathf.Clamp(_finalFov, 30, 110);
        //_cam.fieldOfView = _finalFov;
    }
    public void EnterAquarium(int aquariumNumber, Dictionary<int, List<PublicDefined.stFishInfo>> dic)
    {
        for (int i = 0; i < _aquariumDecoObject.Length; i++)
        {
            if (i.Equals(aquariumNumber))
                _aquariumDecoObject[i].SetActive(true);
            else
                _aquariumDecoObject[i].SetActive(false);
        }

        // 수족관 물고기 생성
        _flock.InitFishs(aquariumNumber, dic);
    }
    public void ExitAquarium(int aquariumNumber)
    {
        _isCameraRunning = false;
        _flock.IsFlocking = false;
        _aquariumDecoObject[aquariumNumber].SetActive(false);
        _fishObjectParentList[aquariumNumber].gameObject.SetActive(false);
    }
    public void ShiftFish(PublicDefined.stFishInfo stFishInfo, int currentAquariumNumber, int AfterAquariumNumber)
    {
        // 시각적으로 해당 물고기의 부모를 옮기는 작업
        int DBNumber = stFishInfo._fishNumber;
        string engName = _aquariumFishDB[DBNumber]._engName;

        Transform shiftTransform = _flock.Parents[currentAquariumNumber].Find(engName).GetChild(0);

        if(!_checkExistFishs[AfterAquariumNumber])
        {
            _fishInfoUI.UpdateInfoSlot(stFishInfo);
            _aquariumUI.FishSlotUpdate();
            shiftTransform.SetParent(_garbageObject);
            shiftTransform.gameObject.SetActive(false);
            return;
        }

        Transform afterFishParent = _flock.Parents[AfterAquariumNumber].Find(engName);
        _flock.Parents[AfterAquariumNumber].gameObject.SetActive(true);
        // 해당 수족관에 물고기가 없다면.. 수족관 버튼을 누른 적이 없다면 굳이 옮기지 않아도 나중에 그 버튼을 누를 때 생성될 것이다.
        // 왜냐하면 데이터는 이미 옮겨져있기 때문에.
        _flock.IsFlocking = false;

        if (afterFishParent == null)
        {
            // 동일한 물고기가 없어서 부모 자체가 없다. 옮길 수족관에 부모를 만든다.
            GameObject go = new GameObject();
            go.name = engName;
            go.transform.SetParent(_flock.Parents[AfterAquariumNumber]);
            shiftTransform.SetParent(go.transform);
        }
        else
        {
            // 동일한 물고기가 있어서 부모가 존재한다.
            shiftTransform.SetParent(afterFishParent);
        }
        _flock.Parents[AfterAquariumNumber].gameObject.SetActive(false);
        // 옮기면 SetActive는 false지만 계속 움직이고 있다. 그래서 Flocking에서 _isCameraRunning을 멈췄다가 다시 실행 시키자.
        // Flocking의 _allFishObject를 새로 갱신해야 하는 듯

        // FishSlot 하고 FishInfoSlot을 업데이트한다.
        _fishInfoUI.UpdateInfoSlot(stFishInfo);
        _aquariumUI.FishSlotUpdate();
        _flock.UpdateAllFishObject(currentAquariumNumber);

    }

    public void SellFish(PublicDefined.stFishInfo stFishInfo, int currentAquariumNumber)
    {
        int DBNumber = stFishInfo._fishNumber;
        string engName = _aquariumFishDB[DBNumber]._engName;
        _flock.IsFlocking = false;

        Transform shiftTransform = _flock.Parents[currentAquariumNumber].Find(engName).GetChild(0);

        _fishInfoUI.UpdateInfoSlot(stFishInfo);
        _aquariumUI.FishSlotUpdate();
        shiftTransform.SetParent(_garbageObject);
        shiftTransform.gameObject.SetActive(false);
        _flock.UpdateAllFishObject(currentAquariumNumber);
    }

    public void ResetFishPosition()
    {
        _flock.ResetPosition();
    }

    public void SetAquariumUIInstance(AquariumUI instance)
    {
        _aquariumUI = instance;
    }
    public void SetFishInfoUIInstance(FishInfoUI instance)
    {
        _fishInfoUI = instance;
    }
    public AquariumUI GetAquariumUIInstance()
    {
        return _aquariumUI;
    }
    // 회전 패드 관련
    public void ClickRotateX(int x)
    {
        _rotateX = x;
    }

    public void ClickRotateY(int y)
    {
        _rotateY = y;
    }

    public void ResetRotateX()
    {
        _rotateX = 0;
    }

    public void ResetRotateY()
    {
        _rotateY = 0;
    }
}