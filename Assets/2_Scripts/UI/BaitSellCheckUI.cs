using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaitSellCheckUI : MonoBehaviour
{
    public Text _titleText;
    public Text _beforeQuantityText;
    public Text _afterQuantityText;

    StringBuilder _sb;
    UserData _userData;

    int _saleVolume;
    int _beforeQuantity;
    int _dbNumber;

    // 만약 판매를 끝까지 완료한다면 모든 창을 꺼야한다.
    GameObject _sellUI;
    GameObject _fishingGearEquipUIObject;
    public void Init(UserData userData, string baitName, int saleVolume, int before, int DBNumber, GameObject sellUI, GameObject equipUIObject)
    {
        if (_userData == null)
            _userData = userData;

        if (_sb == null)
            _sb = new StringBuilder();

        //_saleVolume = saleVolume;
        //_beforeQuantity = before;
        //_dbNumber = DBNumber;
        _sellUI = sellUI;
        _fishingGearEquipUIObject = equipUIObject;

        _sb.Length = 0;

        //_sb.Append(baitName);
        //_sb.Append(" <color=red>");
        //_sb.Append(saleVolume);
        //_sb.Append("</color>개를 파시겠습니까?");

        //_titleText.text = _sb.ToString();

        //_beforeQuantityText.text = before.ToString();
        //_afterQuantityText.text = (before - saleVolume).ToString();

        Dictionary<int, int> dic = _userData.GetBaitDictionary();
        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        _userData._gold += saleVolume * 20;
        dic[DBNumber] -= saleVolume;

        if (dic[DBNumber] < 0)
            dic[DBNumber] = 0;

        _sb.Length = 0;
        _sb.Append("/bait/");
        _sb.Append(DBNumber);

        updateDic.Add(_sb.ToString(), dic[DBNumber]);
        updateDic.Add("/_gold", _userData._gold);

        //_sellUI.SetActive(false);
        //_fishingGearEquipUIObject.SetActive(false);

        FishingGear.INSTANCE.UpdateBait();
        FishingGear.INSTANCE.UpdateGoldPearl();

        DBManager.INSTANCE.UpdateFirebase(updateDic);

        gameObject.SetActive(true);

        StartCoroutine(OffCoroutine());
    }

    IEnumerator OffCoroutine()
    {
        yield return PublicDefined._05secRealDelay;
        _sellUI.SetActive(false);
        _fishingGearEquipUIObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ClickOKButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        Dictionary<int, int> dic = _userData.GetBaitDictionary();
        Dictionary<string, object> updateDic = new Dictionary<string, object>();
        _userData._gold += _saleVolume * 20;
        dic[_dbNumber] -= _saleVolume;

        if (dic[_dbNumber] < 0)
            dic[_dbNumber] = 0;

        _sb.Length = 0;
        _sb.Append("/bait/");
        _sb.Append(_dbNumber);

        updateDic.Add(_sb.ToString(), dic[_dbNumber]);
        updateDic.Add("/_gold", _userData._gold);

        _sellUI.SetActive(false);
        _fishingGearEquipUIObject.SetActive(false);

        FishingGear.INSTANCE.UpdateBait();
        FishingGear.INSTANCE.UpdateGoldPearl();

        DBManager.INSTANCE.UpdateFirebase(updateDic);

        gameObject.SetActive(false);
    }

    public void ClickCancelButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        gameObject.SetActive(false);
    }
}
