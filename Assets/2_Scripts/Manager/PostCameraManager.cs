using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostCameraManager : MonoBehaviour
{
    readonly Vector3 _whenCatchFishPos = new Vector3(-1.7f, 0.5f, 2f);
    readonly Vector3 _whenCatchFishRot = Vector3.up * 44;

    [HideInInspector] public Transform target;

    [SerializeField] GameManager gameMgr;

    Transform postCameraTr;

    // 초기 값
    Vector3 _resetPos;
    Quaternion _resetRot;

    void Start()
    {
        postCameraTr = GetComponent<Transform>();
        _resetPos = transform.position;
        _resetRot = transform.rotation;
    }

    private void LateUpdate()
    {
        if (gameMgr.isFly)
        {
            //postCameraTr.position = new Vector3(postCameraTr.position.x, target.position.y + 1f, target.position.z - 1.5f);
            postCameraTr.position = new Vector3(target.position.x -2f, target.position.y + 1f, target.position.z - 1.5f);
            postCameraTr.LookAt(target);
        }
    }

    public void ResetCamera() // 카메라가 원점으로 돌아오는 함수
    {
        postCameraTr.position = _resetPos;
        postCameraTr.rotation = _resetRot;
    }

    public void CatchFish()
    {
        postCameraTr.position = _whenCatchFishPos;
        postCameraTr.eulerAngles = _whenCatchFishRot;

    }
}
