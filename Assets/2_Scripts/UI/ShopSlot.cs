using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    Item _item; public void SetItem(Item item) { _item = item; }

    [SerializeField] Shop shop;
    [SerializeField] Text _itemName;
    [SerializeField] Text _costText;
    [SerializeField] Image _itemImage;
    [SerializeField] Image _costImage;
    [SerializeField] BuyPopUP buyPop;

    //�������� �ֽ�ȭ�ϴ� �Լ� 
    public void UpdateSlot()
    {
        //������ �̹����� �����̹����� ����
        _itemImage.sprite = _item.itemImage;
        //�̹��� ������Ʈ Ȱ��ȭ
        _itemImage.gameObject.SetActive(true);

        //�ڽ�Ʈ���� �̹��� ����
        _costImage.sprite = _item.costImg;

        //������ �̸� �����ؽ�Ʈ ����
        _itemName.text = _item.korName;

        //������ ��ȭ �ؽ�Ʈ ����
        if (_item.goldCost > 0)
        {
            _costText.text = GetThousandCommaText(_item.goldCost).ToString();
        }
        else if (_item.pearlCost > 0)
        {
            _costText.text = GetThousandCommaText(_item.pearlCost).ToString();
        }
    }

    public static string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
    
    public void RemoveSlot()
    {
        //������ �׸��� ���
        _item = null;
        //������ ������Ʈ ��Ȱ��ȭ
        _itemImage.gameObject.SetActive(false);
    }

    public void Click()
    {
        if (_item != null)
        {
            //SoundManager.instance.EffectPlay("UIClick");
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
            buyPop.PopupSetItem(_item);
        }
    }
}
