using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouponResultUI : MonoBehaviour
{
    [SerializeField] Text _resultText;

    public void Init(string resultText)
    {
        gameObject.SetActive(true);
        _resultText.text = resultText;

    }
}
