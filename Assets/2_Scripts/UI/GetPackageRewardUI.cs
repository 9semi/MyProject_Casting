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
        sb.Append("�Ⱓ[");
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
        sb.Append("]���� ����");

        // �÷�Ƽ��
        if (type.Equals(0))
        {
            _platinumObject.SetActive(true);
            _diamondObject.SetActive(false);
            sb.Append("3õ ��带 ȹ���մϴ�.");
        }
        // ���̾Ƹ��
        else
        {
            _platinumObject.SetActive(false);
            _diamondObject.SetActive(true);
            sb.Append("5õ ��带 ȹ���մϴ�.");
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
