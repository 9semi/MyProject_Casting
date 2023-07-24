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

    // ������ Ŭ���ϸ� �ش��ϴ� ������ ���� �̸� �������� ������ �ѷ���� �Ѵ�. InitSlot���� �ʱ�ȭ�Ѵ�.
    List<PublicDefined.stFishInfo> _fishInfoList;
    public void ClickSlotButton()
    {
        _aquariumUI.ClickAquariumFishSlot(_fishInfoList);
    }
    public void InitFishSlot(AquariumUI instance, List<PublicDefined.stFishInfo> fishInfoList)
    {
        // list ���������� ���´�.(Ȯ��)
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
