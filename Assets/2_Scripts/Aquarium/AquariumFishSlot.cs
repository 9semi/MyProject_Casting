using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AquariumFishSlot : MonoBehaviour
{
    [SerializeField] Text _fishName;
    [SerializeField] Text _fishCount;

    AquariumUI _aquariumUI;
    string _name;
    int _count;

    // 슬롯을 클릭하면 해당하는 슬롯의 같은 이름 물고기들의 정보를 뿌려줘야 한다. InitSlot에서 초기화한다.
    List<PublicDefined.stFishInfo> _fishInfoList;
    public void ClickSlotButton()
    {
        _aquariumUI.ClickAquariumFishSlot(_fishInfoList);
    }
    public void InitFishSlot(AquariumUI instance, List<PublicDefined.stFishInfo> fishInfoList)
    {
        // list 정상적으로 들어온다.(확인)
        _aquariumUI = instance;
        _fishInfoList = fishInfoList;
        _name = fishInfoList[0]._name;
        _count = fishInfoList.Count;
        _fishName.text = _name;
        _fishCount.text = _count.ToString();
        //Debug.LogError(_name);
        gameObject.SetActive(true);
    }
    public void OffSlot()
    {
        if (_aquariumUI != null)
            _aquariumUI = null;

        gameObject.SetActive(false);
    }
}
