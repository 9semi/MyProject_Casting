using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingGearEquipUI : MonoBehaviour
{
    readonly string _rodText = "rod";
    readonly string _reelText = "reel";
    readonly string _baitText = "bait";
    readonly string _pastebaitText = "pastebait";
    readonly string _sinkerText = "sinker";
    readonly string _floatText = "float";
    readonly string[] _itemTypeTextArray = { "³¬½Ë´ë", "¸±", "ÀÏ¹Ý ¹Ì³¢", "·ç¾î ¹Ì³¢", "¹Ø¹ä", "Âî/ºÀµ¹" };

    [SerializeField] Equipment _eq;
    [SerializeField] BaitSellUI _sellUI;
    [SerializeField] Text _itemTypeText;

    [SerializeField] Image _prevItemImage;
    [SerializeField] Text _prevItemNameText;
    [SerializeField] Text _prevItemInfoText;

    [SerializeField] Image _currentItemImage;
    [SerializeField] Text _currentItemNameText;
    [SerializeField] Text _currentItemInfoText;

    [SerializeField] GameObject _noneObject;
    [SerializeField] GameObject _overlapObject;
    [SerializeField] GameObject _isLureObject;

    [SerializeField] GameObject _normalButtonObject;
    [SerializeField] GameObject _baitButtonObject;
    [SerializeField] Button _sellButton;

    int _currentButtonNumber = 0;
    int _currentItemIndex = -1;
    Item _currentItem;
    UserData _userData;
    Dictionary<string, int> _currentEquipmentDictionary;

    public void Init(Item currentItem, int buttonNumber)
    {
        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        _currentButtonNumber = buttonNumber;
        _currentItem = currentItem;
        _currentEquipmentDictionary = _userData.GetCurrentEquipmentDictionary();

        if (_currentItem.Type.Equals(PublicDefined.eItemType.Float) || _currentItem.Type.Equals(PublicDefined.eItemType.Sinker))
        {
            if (LureCheck())
            {
                _isLureObject.SetActive(true);
                return;
            }
        }

        if (OverlapCheck(_currentItem.Type.Equals(PublicDefined.eItemType.Float)))
        {
            _overlapObject.SetActive(true);
            return;
        }

        if(currentItem.Type.Equals(PublicDefined.eItemType.Bait))
        {
            _normalButtonObject.SetActive(false);
            _baitButtonObject.SetActive(true);

            if (DataManager.INSTANCE._tutorialIsInProgress)
            {
                _sellButton.interactable = false;
            }
            
        }
        else
        {
            _normalButtonObject.SetActive(true);
            _baitButtonObject.SetActive(false);
        }

        _itemTypeText.text = _itemTypeTextArray[_currentButtonNumber];
        Item prevItem;
        
        if (_currentItem.Type.Equals(PublicDefined.eItemType.Float))
            prevItem = GetPrevItem(buttonNumber, true);
        else
            prevItem = GetPrevItem(buttonNumber, false);

        if(prevItem == null)
        {
            _noneObject.SetActive(true);
            _prevItemInfoText.text = string.Empty;
        }
        else
        {
            _noneObject.SetActive(false);
            _prevItemImage.sprite = prevItem.itemImage;
            _prevItemNameText.text = prevItem.korName;
            _prevItemInfoText.text = prevItem.itemInfo;
        }

        _currentItemImage.sprite = _currentItem.itemImage;
        _currentItemNameText.text = _currentItem.korName;
        _currentItemInfoText.text = _currentItem.itemInfo;

        gameObject.SetActive(true);
    }

    Item GetPrevItem(int buttonNumber, bool isFloat)
    {
        if (_currentItemIndex.Equals(-1))
            return null;

        switch (buttonNumber)
        {
            case 0:
                return ItemData.Instance.rodItemDB[_currentEquipmentDictionary[_rodText]];
            case 1:
                return ItemData.Instance.reelItemDB[_currentEquipmentDictionary[_reelText]];
            case 2:
            case 3:
                return ItemData.Instance.baitItemDB[_currentItemIndex];
            case 4:
                return ItemData.Instance.pasetbaitItemDB[_currentItemIndex];
            case 5:
                if(isFloat)
                {
                    return ItemData.Instance.floatItemDB[_currentItemIndex];
                }
                else
                {
                    return ItemData.Instance.sinkerItemDB[_currentItemIndex];
                }
            default:
                return null;
        }
    }

    bool LureCheck()
    {
        int DBNum = _currentEquipmentDictionary["bait"];
        if (DBNum.Equals(-1))
            return false;
        else
        {
            if (ItemData.Instance.baitItemDB[DBNum].typeNum.Equals(1)) // ·ç¾î´Â TypeNumÀÌ 1
                return true;
            else
                return false;
        }
    }

    bool OverlapCheck(bool isFloat)
    {
        switch (_currentButtonNumber)
        {
            case 0:
                _currentItemIndex = _currentEquipmentDictionary[_rodText];
                if (_currentItemIndex.Equals(-1))
                    return false;
                else if (_currentItem.serialNumber.Equals(ItemData.Instance.rodItemDB[_currentItemIndex].serialNumber))
                    return true;
                else
                    return false;
            case 1:
                _currentItemIndex = _currentEquipmentDictionary[_reelText];
                if (_currentItemIndex.Equals(-1))
                    return false;
                else if (_currentItem.serialNumber.Equals(ItemData.Instance.reelItemDB[_currentItemIndex].serialNumber))
                    return true;
                else
                    return false;
            case 2:
            case 3:
                _currentItemIndex = _currentEquipmentDictionary[_baitText];
                if (_currentItemIndex.Equals(-1))
                    return false;
                else if (_currentItem.serialNumber.Equals(ItemData.Instance.baitItemDB[_currentItemIndex].serialNumber))
                    return true;
                else
                    return false;
            case 4:
                _currentItemIndex = _currentEquipmentDictionary[_pastebaitText];
                if (_currentItemIndex.Equals(-1))
                    return false;
                else if (_currentItem.serialNumber.Equals(ItemData.Instance.pasetbaitItemDB[_currentItemIndex].serialNumber))
                    return true;
                else
                    return false;
            case 5:
                if(isFloat)
                {
                    _currentItemIndex = _currentEquipmentDictionary[_floatText];
                    if (_currentItemIndex.Equals(-1))
                        return false;
                    else if (_currentItem.serialNumber.Equals(ItemData.Instance.floatItemDB[_currentItemIndex].serialNumber))
                        return true;
                    else
                        return false;
                }
                else
                {
                    _currentItemIndex = _currentEquipmentDictionary[_sinkerText];
                    if (_currentItemIndex.Equals(-1))
                        return false;
                    else if (_currentItem.serialNumber.Equals(ItemData.Instance.sinkerItemDB[_currentItemIndex].serialNumber))
                        return true;
                    else
                        return false;
                }
            default:
                return false;
        }


    }

    public void ClickEqipButton()
    {
        // ÀÌÆåÆ® ¼Ò¸®

        // ÇöÀç ½½·Ô°ú ¶È°°À» ¶§

        // ÇöÀç ½½·ÔÀÌ ºñ¾îÀÖÁö ¾ÊÀ» ¶§

        // ÇåÀç ½½·ÔÀÌ ºñ¾îÀÖÀ» ¶§

        switch (_currentButtonNumber) // 0: ³¬½Ë´ë ¹öÆ°, 1: ¸± ¹öÆ°, 2: ¹Ì³¢ ¹öÆ°, 3: ¶±¹ä ¹öÆ°, 4: Âî ¹öÆ°, 5: ºÀµ¹ ¹öÆ°À» ´©¸£°í ÀÖ´Ù.
        {
            case 0:
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipRod).GetComponent<AudioPoolObject>().Init();
                if (_eq._currentRodItem == null)
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                else if (_eq._currentRodItem.Equals(_currentItem))
                    return;
                else
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                break;
            case 1:
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipReel).GetComponent<AudioPoolObject>().Init();
                if (_eq._currentReelItem == null)
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                else if (_eq._currentReelItem.Equals(_currentItem))
                    return;
                else
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                break;
            case 2:
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipBait).GetComponent<AudioPoolObject>().Init();
                if (_eq._currentBaitItem == null)
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                else if (_eq._currentBaitItem.Equals(_currentItem))
                    return;
                else
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                break;
            case 3:
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipLureBait).GetComponent<AudioPoolObject>().Init();
                if (_eq._currentBaitItem == null)
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                else if (_eq._currentBaitItem.Equals(_currentItem))
                    return;
                else
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                break;
            case 4:
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipPastebait).GetComponent<AudioPoolObject>().Init();
                if (_eq._currentPastebaitItem == null)
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                else if (_eq._currentPastebaitItem.Equals(_currentItem))
                    return;
                else
                    _eq.ChangeItem(_currentItem, _currentButtonNumber);
                break;
            case 5: // ÇöÀç Âî/ºÀµ¹ ¹öÆ°À» Å¬¸¯ÇÑ »óÅÂ
                if (_currentItem.Type.Equals(PublicDefined.eItemType.Float))
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipFloat).GetComponent<AudioPoolObject>().Init();
                    if (_eq._currentFloatItem == null)
                        _eq.ChangeItem(_currentItem, _currentButtonNumber);
                    else if (_eq._currentFloatItem.Equals(_currentItem))
                        return;
                    else
                        _eq.ChangeItem(_currentItem, _currentButtonNumber);
                    break;
                }
                else
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipSinker).GetComponent<AudioPoolObject>().Init();
                    if (_eq._currentSinkerItem == null)
                        _eq.ChangeItem(_currentItem, 6);
                    else if (_eq._currentSinkerItem.Equals(_currentItem))
                        return;
                    else
                        _eq.ChangeItem(_currentItem, 6);
                    break;
                }
        }
        gameObject.SetActive(false);
    }
    public void ClickCancelButton()
    {
        // ÀÌÆåÆ® ¼Ò¸®
        PlayExitEffectAudio();
        gameObject.SetActive(false);
    }
    public void ClickSellButton()
    {
        if (!DataManager.INSTANCE._tutorialIsDone)
            return;

        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _sellUI.Init(_userData, _currentItem, _userData.GetBaitDictionary()[_currentItem.serialNumber - 2000], gameObject);
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
}
