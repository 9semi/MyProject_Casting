using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquariumEffectManager : MonoBehaviour
{
    [SerializeField] Camera _bgCamera;
    [SerializeField] GameObject _bgEffect;
    [SerializeField] GameObject _touchEffect;

    Queue<GameObject> _touchEffectQueue = new Queue<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(_touchEffect, transform);
            go.SetActive(false);
            _touchEffectQueue.Enqueue(go);
        }

        StartCoroutine(TouchCoroutine());
    }

    IEnumerator TouchCoroutine()
    {
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateTouchEffect(_bgCamera.ScreenToWorldPoint(Input.mousePosition));
                yield return PublicDefined._05secDelay;
            }

            yield return null;
        }
    }

    void CreateTouchEffect(Vector3 pos)
    {
        if (_touchEffectQueue.Count > 0)
        {
            GameObject go = _touchEffectQueue.Dequeue();
            go.SetActive(true);
            go.transform.position = pos;
            go.transform.GetComponent<TouchEffect>().Play(this);
        }
    }

    public void ReturnObject(GameObject go)
    {
        go.SetActive(false);
        _touchEffectQueue.Enqueue(go);
    }

    public void BGEffectSetActive(bool active)
    {
        _bgEffect.SetActive(active);
    }
}
