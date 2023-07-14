using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageOverUI : MonoBehaviour
{
    public GameObject _firstObject;
    public Text _firstNotice;
    public GameObject _secondObject;
    public Text _secondNotice;

    public void Init(List<string> list)
    {
        _firstObject.SetActive(true);
        _firstNotice.text = list[0];

        if (list.Count > 1)
        {
            _secondObject.SetActive(true);
            _secondNotice.text = list[1];
        }
        //Debug.Log(list[0]);
        gameObject.SetActive(true);
        StartCoroutine(ON());
    }

    IEnumerator ON()
    {
        yield return PublicDefined._2secDelay;
        gameObject.SetActive(false);
    }
}
