using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    readonly Vector3 _whenCatchFishPos_Jeongdongjin = new Vector3(-1.8f, 0.5f, 2);
    readonly Vector3 _whenCatchFishRot_Jeongdongjin = Vector3.up * 47;

    readonly Vector3 _whenCatchFishPos_Homerspit = new Vector3(-1.55f, 0.5f, 1.9f);
    readonly Vector3 _whenCatchFishRot_Homerspit = Vector3.up * 44;

    readonly Vector3 _whenCatchFishPos = new Vector3(-1.7f, 0.5f, 2f);
    readonly Vector3 _whenCatchFishRot = Vector3.up * 44;

    readonly float _shakeRange = 0.02f;
    [SerializeField] GameManager gameMgr;
    [SerializeField] InGameUIManager inGameUiMgr;

    Transform _needleTransform;
    Vector3 _shakeVector;
   
    // 초기 값
    Vector3 _resetPos;
    Quaternion _resetRot;

    private void Awake()
    {
        // 모바일 해상도 설정 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        _resetPos = transform.position;
        _resetRot = transform.rotation;
    }

    private void LateUpdate()
    {
        if (gameMgr.IsFly && !gameMgr.IsPause)
        {
            //transform.position = new Vector3(transform.position.x, target.position.y + 1f, target.position.z - 1.5f);
            transform.position = new Vector3(_needleTransform.position.x - 2f, _needleTransform.position.y + 1f, _needleTransform.position.z - 1.5f);
            transform.LookAt(_needleTransform);
        }
    }

    public void SettingTarget(Transform needleTransform)
    {
        _needleTransform = needleTransform;
    }

    // 메인 및 포스트카메라(조건 변경 요망)
    public void ResetCamera() // 카메라가 원점으로 돌아오는 함수
    {
        transform.position = _resetPos;
        transform.rotation = _resetRot;
        gameMgr.IsReset = true;
        gameMgr.RotateStop = false;
        
    }

    public void CatchFish()
    {
        if(DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin))
        {
            transform.position = _whenCatchFishPos_Jeongdongjin;
            transform.eulerAngles = _whenCatchFishRot_Jeongdongjin;
        }
        else
        {
            transform.position = _whenCatchFishPos_Homerspit; ;
            transform.eulerAngles = _whenCatchFishRot_Homerspit;
        }
    }

    public void ShakeCamera(float duration)
    {
        StartCoroutine(ShakeCoroutine(duration));
    }

    IEnumerator ShakeCoroutine(float duration)
    {
        WaitForSeconds delay = new WaitForSeconds(0.03f);

        while(duration > 0)
        {
            float randomValue = Random.Range(-0.2f, 0.2f) * _shakeRange;

            _shakeVector = transform.position;
            _shakeVector.x += randomValue;
            _shakeVector.y += randomValue;
            transform.position = _shakeVector;
            duration -= 0.03f;

            yield return delay;
        }
        transform.position = _resetPos;
    }
}
