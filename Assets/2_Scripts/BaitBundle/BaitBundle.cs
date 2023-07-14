using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaitBundle : MonoBehaviour
{
    [Header("스크립트")]
    public PetManager _pet;
    public BaitSpatulaControl _baitspatulaControl;
    
    // 물이 튀는 위치
    public ParticleSystem _waterEffect;
    public GameObject _surroundingEffect;

    // 낙하 됐는지
    bool _baitBundleIsFlying = true;

    Vector3 _forceVector;

    Rigidbody _rgd;

    private void Awake()
    {
        _rgd = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        AddforceFunction();
    }

    void AddforceFunction()
    {
        _rgd.useGravity = true;

        if (_pet.progress < 0.3f)
            _pet.progress = 0.35f;

        //powerX = characterTr.rotation.y * progress * 1580;
        //powerY = progress * 600;
        //powerZ = progress * 1150;

        //_forceVector.x = gameMgr.powerX;
        //_forceVector.y = gameMgr.powerY;
        //_forceVector.z = gameMgr.powerZ;
        //needleControl.myRd.AddForce(_forceVector);

        //_pet.progress = 1;

        _forceVector.x = _baitspatulaControl._rotY * _pet.progress * 13.5f;
        _forceVector.y = _pet.progress * 600;
        _forceVector.z = _pet.progress * 780;

        //float powerX = _baitspatulaControl._rotY * _pet.progress * 13.5f;
        //float powerY = _pet.progress * 560;
        //float powerZ = _pet.progress * 655;

        _rgd.AddForce(_forceVector);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            _rgd.isKinematic = true;
            
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.baitbundleReachesTheSurface).GetComponent<AudioPoolObject>().Init();
            
            // 범위에 있는 물고기 중 떡밥에 영향을 받는 물고기는 확률을 증가시킨다.
            if (_baitBundleIsFlying)
            {
                _baitBundleIsFlying = false;
                _pet.IncreaseProbility(transform.position.x - 10f, transform.position.x + 10f, transform.position.z + 10f, transform.position.z - 10f);

                // 물 튀는 이펙트 활성화 
                _waterEffect.Play();
                // 주변으로 퍼지는 이펙트 생성
                Instantiate(_surroundingEffect, new Vector3(transform.position.x, -1.29f, transform.position.z), Quaternion.Euler(-90, 0, 0));
            }

            StartCoroutine(MakeDelay(2, () =>
            {
                _waterEffect.Stop();
                _baitBundleIsFlying = true;
                _rgd.isKinematic = false;
                _rgd.useGravity = false;
                transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
            }));
        }
    }

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
        switch (delayNumber)
        {
            case 2:
                yield return PublicDefined._1secDelay;
                break;
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }
}
