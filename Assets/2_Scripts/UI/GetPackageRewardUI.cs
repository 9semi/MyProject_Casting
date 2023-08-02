using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPackageRewardUI : MonoBehaviour
{
    [SerializeField] GameObject _platinumObject;
    [SerializeField] GameObject _diamondObject;
    
    [SerializeField] Text _noticeText;

    public void Init(int type, DateTime startDate, DateTime endDate)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("기간[");
        sb.Append(startDate.Year);
        sb.Append(".");
        sb.Append(startDate.Month);
        sb.Append(".");
        sb.Append(startDate.Day);
        sb.Append(" ~ ");
        sb.Append(endDate.Year);
        sb.Append(".");
        sb.Append(endDate.Month);
        sb.Append(".");
        sb.Append(endDate.Day);
        sb.Append("]동안 일일");

        // 플래티넘
        if (type.Equals(0))
        {
            _platinumObject.SetActive(true);
            _diamondObject.SetActive(false);
            sb.Append("3천 골드를 획득합니다.");
        }
        // 다이아몬드
        else
        {
            _platinumObject.SetActive(false);
            _diamondObject.SetActive(true);
            sb.Append("5천 골드를 획득합니다.");
        }
        _noticeText.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void ClickOKButton()
    {
        Shop.INSTANCE.UpdateGoldPearl();
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
}
