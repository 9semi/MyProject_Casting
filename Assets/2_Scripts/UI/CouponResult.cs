using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouponResult : MonoBehaviour
{
    [SerializeField] Text _noticeText;

    public void Init(string text)
    {
        gameObject.SetActive(true);
        _noticeText.text = text;
    }
}
