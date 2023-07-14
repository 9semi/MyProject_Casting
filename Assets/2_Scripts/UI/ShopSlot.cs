using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Shop shop;
    //������ ����
    [HideInInspector] public Item _item;

    //������ �̸�
    public Text _itemName;

    //������ ����
    public Text _costText;

    //���Ծȿ� ���Ե� �̹���
    public Image _itemImage;

    // ��ȭ �̹���
    public Image _costImage;

    public BuyPopUP buyPop;

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