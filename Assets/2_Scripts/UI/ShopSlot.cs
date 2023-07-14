using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Shop shop;
    //아이템 정보
    [HideInInspector] public Item _item;

    //아이템 이름
    public Text _itemName;

    //아이템 가격
    public Text _costText;

    //슬롯안에 삽입될 이미지
    public Image _itemImage;

    // 재화 이미지
    public Image _costImage;

    public BuyPopUP buyPop;

    //아이템을 최신화하는 함수 
    public void UpdateSlot()
    {
        //아이템 이미지를 슬롯이미지에 삽입
        _itemImage.sprite = _item.itemImage;
        //이미지 오브젝트 활성화
        _itemImage.gameObject.SetActive(true);

        //코스트단위 이미지 삽입
        _costImage.sprite = _item.costImg;

        //아이템 이름 슬롯텍스트 삽입
        _itemName.text = _item.korName;

        //아이템 재화 텍스트 삽입
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
        //아이템 항목을 비움
        _item = null;
        //아이템 오브젝트 비활성화
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
