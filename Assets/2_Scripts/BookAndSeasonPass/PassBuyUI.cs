using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassBuyUI : MonoBehaviour
{
    readonly int _cost = 7900;

    public GameObject _moneyObject;
    public GameObject _alreadyHaveObject;
    public GameObject _buyButtonObject;
    public GameObject _checkUI;

    int _currentPearl;
    [HideInInspector] public BookAndSeasonPass _seasonPass;

    public void Init(bool isHave, int currentPearl, BookAndSeasonPass seasonPass)
    {
        _currentPearl = currentPearl;
        _seasonPass = seasonPass;
        if (isHave)
        {
            _moneyObject.SetActive(false);
            _buyButtonObject.SetActive(false);
            _alreadyHaveObject.SetActive(true);
        }
        else
        {
            _moneyObject.SetActive(true);
            _buyButtonObject.SetActive(true);
            _alreadyHaveObject.SetActive(false);

        }

        gameObject.SetActive(true);
    }

    public void ClickBuyButton()
    {
        PlayClickEffectAudio();
        _checkUI.SetActive(true);
    }
    public void ClickOKButton()
    {
        //PlayClickEffectAudio();
        //UserData userData = DBManager.INSTANCE.GetUserData();
        //Dictionary<string, object> updateDic = new Dictionary<string, object>();

        // �н� ���� ������ ������Ʈ
        //userData._pearl -= _cost;
        //updateDic.Add("/_pearl/", userData._pearl);

        //switch (DataManager.INSTANCE._mapType)
        //{
        //    case PublicDefined.eMapType.jeongdongjin:
        //        userData._haveJeongdongjinPass = true;
        //        //updateDic.Add("/_haveJeongdongjinPass/", true);
        //        break;
        //    case PublicDefined.eMapType.skyway:
        //        userData._haveSkywayPass = true;
        //        //updateDic.Add("/_haveSkywayPass/", true);
        //        break;
        //    case PublicDefined.eMapType.homerspit:
        //        userData._haveHomerspitPass = true;
        //        //updateDic.Add("/_haveHomerspitPass/", true);
        //        break;
        //}
        //DBManager.INSTANCE.UpdateFirebase(updateDic);

        // ��ư�� ������Ʈ ���� (���� ��)
        //_moneyObject.SetActive(false);
        //_buyButtonObject.SetActive(false);
        //_alreadyHaveObject.SetActive(true);

        // �н��� ���������� ���� �ε��������� �����̾� ��ư�� �� ������� �Ѵ�.
        _seasonPass.InitPassButtons();
    }
    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
}
