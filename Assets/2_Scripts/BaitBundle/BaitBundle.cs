using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaitBundle : MonoBehaviour
{
    [Header("스크립트")]
    [SerializeField] PetManager _pet;
    [SerializeField] BaitSpatulaControl _baitspatulaControl;

    // 물이 튀는 위치
    [SerializeField] ParticleSystem _waterEffect;
    [SerializeField] GameObject _surroundingEffect;

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

        if (_pet.Progress < 0.3f)
            _pet.Progress = 0.35f;

        _forceVector.x = _baitspatulaControl.RotY * _pet.Progress * 13.5f;
        _forceVector.y = _pet.Progress * 600;
        _forceVector.z = _pet.Progress * 780;
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
