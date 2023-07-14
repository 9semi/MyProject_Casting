using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_GetPackageRewardUI : MonoBehaviour
{
    public GameObject _platinumRewardObject;
    public Text _platinumPeriodText;
    public Text _platinumRewardText;
    public GameObject _platinumGetRewardObject;
    public GameObject _platinumAlreadyGetObject;
    public GameObject _platinumADObject;
    public GameObject _platinumEffect;

    public GameObject _diamondRewardObject;
    public Text _diamondPeriodText;
    public Text _diamondRewardText;
    public GameObject _diamondGetRewardObject;
    public GameObject _diamondAlreadyGetObject;
    public GameObject _diamondADObject;
    public GameObject _diamondEffect;

    public void Init(UserData userData, bool havePlatinum, bool haveDiamond, bool isGetPlatinum, bool isGetDiamond)
    {
        DateTime startDay;
        DateTime endDay;

        if(havePlatinum)
        {
            startDay = DateTime.MaxValue;
            endDay = DateTime.MinValue;  
            _platinumRewardObject.SetActive(true);
            _platinumADObject.SetActive(false);

            StringBuilder sb = new StringBuilder();
            foreach (string key in userData.GetPlatinumPackage().Keys)
            {
                DateTime empty = DateTime.ParseExact(key, "yyyyMMdd", null);

                if(DateTime.Compare(startDay, empty) > 0)
                    startDay = empty;

                if (DateTime.Compare(endDay, empty) < 0)
                    endDay = empty;
            }

            //Debug.Log("플래티넘 시작일 : " + startDay.ToString("yyyyMMdd"));
            //Debug.Log("플래티넘 종료일 : " + endDay.ToString("yyyyMMdd"));

            sb.Append("[");
            sb.Append(startDay.ToString("yyyy"));
            sb.Append(".");
            sb.Append(startDay.ToString("MM"));
            sb.Append(".");
            sb.Append(startDay.ToString("dd"));
            sb.Append(" ~ ");
            sb.Append(endDay.ToString("yyyy"));
            sb.Append(".");
            sb.Append(endDay.ToString("MM"));
            sb.Append(".");
            sb.Append(endDay.ToString("dd"));
            sb.Append("]");
            _platinumPeriodText.text = sb.ToString();
            _platinumRewardText.text = "3,000";

            if(isGetPlatinum)
            {
                _platinumGetRewardObject.SetActive(false);
                _platinumAlreadyGetObject.SetActive(true);
            }
            else
            {
                _platinumGetRewardObject.SetActive(true);
                _platinumAlreadyGetObject.SetActive(false);
            }
        }
        else
        {
            _platinumRewardObject.SetActive(false);
            _platinumADObject.SetActive(true);

            _platinumPeriodText.text = string.Empty;
        }

        if(haveDiamond)
        {
            startDay = DateTime.MaxValue;
            endDay = DateTime.MinValue;   
            _diamondRewardObject.SetActive(true);
            _diamondADObject.SetActive(false);

            StringBuilder sb = new StringBuilder();
            foreach (string key in userData.GetDiamondPackage().Keys)
            {
                DateTime empty = DateTime.ParseExact(key, "yyyyMMdd", null);

                if (DateTime.Compare(startDay, empty) > 0)
                    startDay = empty;

                if (DateTime.Compare(endDay, empty) < 0)
                    endDay = empty;
            }

            //Debug.Log("다이아몬드 시작일 : " + startDay.ToString("yyyyMMdd"));
            //Debug.Log("다이아몬드 종료일 : " + endDay.ToString("yyyyMMdd"));

            sb.Append("[");
            sb.Append(startDay.ToString("yyyy"));
            sb.Append(".");
            sb.Append(startDay.ToString("MM"));
            sb.Append(".");
            sb.Append(startDay.ToString("dd"));
            sb.Append(" ~ ");
            sb.Append(endDay.ToString("yyyy"));
            sb.Append(".");
            sb.Append(endDay.ToString("MM"));
            sb.Append(".");
            sb.Append(endDay.ToString("dd"));
            sb.Append("]");
            _diamondPeriodText.text = sb.ToString();
            _diamondRewardText.text = "5,000";

            if (isGetDiamond)
            {
                _diamondGetRewardObject.SetActive(false);
                _diamondAlreadyGetObject.SetActive(true);
            }
            else
            {
                _diamondGetRewardObject.SetActive(true);
                _diamondAlreadyGetObject.SetActive(false);
            }
        }
        else
        {
            _diamondRewardObject.SetActive(false);
            _diamondADObject.SetActive(true);

            _diamondPeriodText.text = string.Empty;
        }

        gameObject.SetActive(true);
    }


    public void ClickOKButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }

    public void ClickRewardButton(int type)
    {
        DateTime today = DateTime.Today;
        string todayStr = today.ToString("yyyyMMdd");

        if (type.Equals(0))
        {
            UserData userData = DBManager.INSTANCE.GetUserData();
            Dictionary<string, bool> platinumDic = userData.GetPlatinumPackage();
            platinumDic[todayStr] = true;
            userData._gold += 3000;
            _platinumEffect.SetActive(true);
            _platinumGetRewardObject.SetActive(false);
            _platinumAlreadyGetObject.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.coin).GetComponent<AudioPoolObject>().Init();
            DBManager.INSTANCE.UpdatePackage(0, todayStr);
        }
        else
        {
            UserData userData = DBManager.INSTANCE.GetUserData();
            Dictionary<string, bool> diamondDic = userData.GetDiamondPackage();
            diamondDic[todayStr] = true;
            userData._gold += 5000;
            _diamondEffect.SetActive(true);
            _diamondGetRewardObject.SetActive(false);
            _diamondAlreadyGetObject.SetActive(true);
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.coin).GetComponent<AudioPoolObject>().Init();
            DBManager.INSTANCE.UpdatePackage(1, todayStr);
        }
    }
}
