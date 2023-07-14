using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;

public class Slot : MonoBehaviour
{
    // 아이템 정보( 로컬 전용 아이템 변수)
    [HideInInspector] public Item item;

    // 아이템 수량
    [HideInInspector] public int quantity;

    //슬롯안에 삽입될 이미지
    public Image itemImage;

    // 수량 나타낼 텍스트
    public Text quantityText;

    // 아이템 이름 나타낼 텍스트
    public Text nameText;

    // 장착 중인 슬롯의 스크립트
    public Equipment _eq;
    public FishingGearEquipUI _eqUI;

    // 현재 유저가 누르고 있는 버튼의 번호
    int _currentButtonNumber = 0;

    //아이템을 최신화하는 함수(로컬방식
    public void UpdateSlot(Item i, int currentButtonNumber)
    {
        item = i;
        _currentButtonNumber = currentButtonNumber;

        //아이템 이미지를 슬롯이미지에 삽입
        itemImage.sprite = item.itemImage;

        // 아이템 이름 출력
        nameText.text = item.korName;

        // 아이템 이미지 활성화
        itemImage.enabled = true;

        if(item.Type.Equals(PublicDefined.eItemType.Bait) || item.Type.Equals(PublicDefined.eItemType.Pastebait))
        {
            quantityText.text = quantity.ToString();
        }
        else
        {
            quantityText.text = string.Empty;
        }

        gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        //아이템 항목을 비움
        item = null;

        //아이템 오브젝트 비활성화
        itemImage.enabled = false;
        nameText.text = string.Empty;
        quantityText.text = string.Empty;

        gameObject.SetActive(false);
    }
    public void ClickSlot()
    {
        // 현재 슬롯과 똑같을 때

        // 현재 슬롯이 비어있지 않을 때

        // 헌재 슬롯이 비어있을 때
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _eqUI.Init(item, _currentButtonNumber);

        //switch (_currentButtonNumber) // 0: 낚싯대 버튼, 1: 릴 버튼, 2: 미끼 버튼, 3: 떡밥 버튼, 4: 찌 버튼, 5: 봉돌 버튼을 누르고 있다.
        //{
        //    case 0:
        //        if(_eq._currentRodItem == null)
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        else if (_eq._currentRodItem.Equals(item))
        //            return;
        //        else
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        break;
        //    case 1:
        //        if (_eq._currentReelItem == null)
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        else if (_eq._currentReelItem.Equals(item))
        //            return;
        //        else
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        break;
        //    case 2:
        //    case 3:
        //        if (_eq._currentBaitItem == null)
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        else if (_eq._currentBaitItem.Equals(item))
        //            return;
        //        else
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        break;
        //    case 4:
        //        if (_eq._currentPastebaitItem == null)
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        else if (_eq._currentPastebaitItem.Equals(item))
        //            return;
        //        else
        //            _eq.ChangeItem(item, _currentButtonNumber);
        //        break;
        //    case 5: // 현재 찌/봉돌 버튼을 클릭한 상태
        //        if(item.Type == PublicDefined.eItemType.Float)
        //        {
        //            if (_eq._currentFloatItem == null)
        //                _eq.ChangeItem(item, _currentButtonNumber);
        //            else if (_eq._currentFloatItem.Equals(item))
        //                return;
        //            else
        //                _eq.ChangeItem(item, _currentButtonNumber);
        //            break;
        //        }
        //        else
        //        {
        //            if (_eq._currentSinkerItem == null)
        //                _eq.ChangeItem(item, 6);
        //            else if (_eq._currentSinkerItem.Equals(item))
        //                return;
        //            else
        //                _eq.ChangeItem(item, 6);
        //            break;
        //        }
        //}
    }
}