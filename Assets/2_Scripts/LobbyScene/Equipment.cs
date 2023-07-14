using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    static Equipment _uniqueInstance;
    static public Equipment INSTANCE
    {
        get { return _uniqueInstance; }
    }
    
    public bool _isBaitChange = false;

    public Image _rodImage;
    public Image _reelImage;
    public Image _baitImage;
    public Text _baitQuantity;
    public Image _pastebaitImage;
    public Text _pastebaitQuantity;
    public Image _floatImage;
    public Image _sinkerImage;
    public FishingGear _fishingGear;
    public Text[] _currentEquipmentText;

    [HideInInspector] public Item _currentRodItem = null;
    [HideInInspector] public Item _currentReelItem = null;
    [HideInInspector] public Item _currentBaitItem = null;
    [HideInInspector] public Item _currentPastebaitItem = null;
    [HideInInspector] public Item _currentFloatItem = null;
    [HideInInspector] public Item _currentSinkerItem = null;

    Dictionary<string, int> _currentEquipmentDictionary;
    UserData _userData = null;
    static public int _prevBaitItemSerialNumber = -1;

    private void Awake()
    {
        //Debug.Log("생성");
        _uniqueInstance = this;
        _userData = DBManager.INSTANCE.GetUserData();
    }

    private void OnEnable()
    {
        CheckCurrentEquipment();
    }

    public void BackToInitialState()
    {
        _currentRodItem = ItemData.Instance.rodItemDB[2];
        _currentReelItem = ItemData.Instance.reelItemDB[2];
        _currentBaitItem = null;
        _currentPastebaitItem = null;
        _currentFloatItem = null;
        _currentSinkerItem = null;
    }

    // 장비를 바꾼다.
    public void ChangeItem(Item i, int num)
    {
        // 같은 장비를 클릭했다면 그냥 리턴

        switch(num)
        {
            case 0:
                if (_userData.GetCurrentEquipmentDictionary()["rod"].Equals(i.serialNumber - 1000))
                    break;

                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipRod).GetComponent<AudioPoolObject>().Init();
                _currentRodItem = i;
                _userData.GetCurrentEquipmentDictionary()["rod"] = _currentRodItem.serialNumber - 1000;
                _rodImage.sprite = _currentRodItem.itemImage;
                _rodImage.enabled = true;
                _currentEquipmentText[num].text = _currentRodItem.korName;
                DataManager.INSTANCE.UpdateEquipmentRod(_currentRodItem);
                break;
            case 1:
                if (_userData.GetCurrentEquipmentDictionary()["reel"].Equals(i.serialNumber - 6000))
                    break;

                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipReel).GetComponent<AudioPoolObject>().Init();
                _currentReelItem = i;
                _userData.GetCurrentEquipmentDictionary()["reel"] = _currentReelItem.serialNumber - 6000;
                _reelImage.sprite = _currentReelItem.itemImage;
                _reelImage.enabled = true;
                _currentEquipmentText[num].text = _currentReelItem.korName;
                DataManager.INSTANCE.UpdateEquipmentReel(_currentReelItem);
                // skyway 패스 확인
                if ((_currentReelItem.serialNumber - 6000).Equals(5) || (_currentReelItem.serialNumber - 6000).Equals(6))
                {
                    if(_userData._currentSkywayPassIndex == (int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl)
                    {
                        PassManager.INSTANCE.CheckClearPassAboutAction_Skyway();
                    }
                }
                break;
            case 2:
            case 3:
                if (_userData.GetCurrentEquipmentDictionary()["bait"].Equals(i.serialNumber - 2000))
                    break;

                // 루어 미끼라면 찌, 봉돌을 제거하고 커버를 씌워야한다.
                if(i.typeNum.Equals(1))
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipLureBait).GetComponent<AudioPoolObject>().Init();
                    ClickEquipmentSlot(4);
                    ClickEquipmentSlot(5);
                    _fishingGear.SinkerFloatCoverOn();

                    //if (DataManager.INSTANCE._mapType != PublicDefined.eMapType.lobby)
                    //{
                    //    // 게임씬인데 객체가 없다면 가져온다.
                    //    if (_fishingGear.gameMgr == null)
                    //    {
                    //        _fishingGear.gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                    //    }

                    //    _fishingGear.gameMgr.reelPoint2.GetChild(0).gameObject.SetActive(false);
                    //    _fishingGear.gameMgr.style = GameManager.GameStyle.Onetwo;
                    //}

                }
                else
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipBait).GetComponent<AudioPoolObject>().Init();
                    _fishingGear.SinkerFloatCoverOff();
                }

                _currentBaitItem = i;
                _userData.GetCurrentEquipmentDictionary()["bait"] = _currentBaitItem.serialNumber - 2000;
                _baitImage.sprite = _currentBaitItem.itemImage;
                _baitImage.enabled = true;
                _baitQuantity.text = _userData.GetBaitDictionary()[_currentBaitItem.serialNumber - 2000].ToString();
                _currentEquipmentText[2].text = _currentBaitItem.korName;

                //Debug.Log("현재 미끼 : " + _currentBaitItem.korName + "(" + (_currentBaitItem.serialNumber - 2000) + ")");

                // 미끼를 처음 낀다.
                if (_prevBaitItemSerialNumber.Equals(-1))
                    DataManager.INSTANCE.UpdateEquipmentBaitOn(_currentBaitItem.serialNumber - 2000);
                else
                    DataManager.INSTANCE.UpdateEquipmentBaitOn(_currentBaitItem.serialNumber - 2000, _prevBaitItemSerialNumber - 2000);

                if (!_prevBaitItemSerialNumber.Equals(_currentBaitItem.serialNumber - 2000))
                    _isBaitChange = true;

                _prevBaitItemSerialNumber = _currentBaitItem.serialNumber;
                break;
            case 4:
                if (_userData.GetCurrentEquipmentDictionary()["pastebait"].Equals(i.serialNumber - 3000))
                    break;

                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipPastebait).GetComponent<AudioPoolObject>().Init();
                DataManager.INSTANCE._currentPastebaitNumber_forTutorial = i.serialNumber - 3000;
                _currentPastebaitItem = i;
                _userData.GetCurrentEquipmentDictionary()["pastebait"] = _currentPastebaitItem.serialNumber - 3000;
                _pastebaitImage.sprite = _currentPastebaitItem.itemImage;
                _pastebaitImage.enabled = true;
                _currentEquipmentText[3].text = _currentPastebaitItem.korName;
                _pastebaitQuantity.text = _userData.GetPasteBaitDictionary()[_currentPastebaitItem.serialNumber - 3000].ToString();
                break;
            case 5:
                if (_userData.GetCurrentEquipmentDictionary()["float"].Equals(i.serialNumber - 4000))
                    break;

                if (_currentBaitItem != null && _currentBaitItem.typeNum.Equals(1))
                    break;

                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipFloat).GetComponent<AudioPoolObject>().Init();
                _currentFloatItem = i;
                _userData.GetCurrentEquipmentDictionary()["float"] = _currentFloatItem.serialNumber - 4000;
                _floatImage.sprite = _currentFloatItem.itemImage;
                _floatImage.enabled = true;
                _currentEquipmentText[4].text = _currentFloatItem.korName;
                _fishingGear._depthLengthInput.interactable = true;
                _fishingGear._depthLengthInput.text = DataManager.INSTANCE._depthLength.ToString();

                DataManager.INSTANCE.UpdateEquipmentFloatOn();
                break;
            case 6:
                if (_userData.GetCurrentEquipmentDictionary()["sinker"].Equals(i.serialNumber - 5000))
                    break;

                if (_currentBaitItem != null && _currentBaitItem.typeNum.Equals(1))
                    break;

                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipSinker).GetComponent<AudioPoolObject>().Init();
                _currentSinkerItem = i;
                _userData.GetCurrentEquipmentDictionary()["sinker"] = _currentSinkerItem.serialNumber - 5000;
                _sinkerImage.sprite = _currentSinkerItem.itemImage;
                _sinkerImage.enabled = true;
                _currentEquipmentText[5].text = _currentSinkerItem.korName;
                DataManager.INSTANCE.UpdateEquipmentSinkerOn(_currentSinkerItem._sinkerWeight);
                //Debug.LogError(_currentSinkerItem._sinkerWeight);
                break;
        }
    }

    // 현재 장비 뭐 장착하고 있는지 확인하고 그 장비를 장착한다.
    public void CheckCurrentEquipment()
    {
        if(_userData == null)
        {
            _userData = DBManager.INSTANCE.GetUserData();
        }

        _currentEquipmentDictionary = new Dictionary<string, int>();
        _currentEquipmentDictionary = _userData.GetCurrentEquipmentDictionary();

        // -1이 아니면 현재 장착 중인 아이템이 있다는 것이다.
        if (!_currentEquipmentDictionary["rod"].Equals(-1))
        {
            _currentRodItem = ItemData.Instance.rodItemDB[_currentEquipmentDictionary["rod"]];
            _rodImage.sprite = _currentRodItem.itemImage;
            _rodImage.enabled = true;
            _currentEquipmentText[0].text = _currentRodItem.korName;
            DataManager.INSTANCE.UpdateEquipmentRod(_currentRodItem);
        }

        if (!_currentEquipmentDictionary["reel"].Equals(-1))
        {
            _currentReelItem = ItemData.Instance.reelItemDB[_currentEquipmentDictionary["reel"]];
            _reelImage.sprite = _currentReelItem.itemImage;
            _reelImage.enabled = true;
            _currentEquipmentText[1].text = _currentReelItem.korName;
            DataManager.INSTANCE.UpdateEquipmentRod(_currentReelItem);
        }

        if (_currentEquipmentDictionary["float"].Equals(-1))
        {
            _fishingGear._depthLengthInput.interactable = false;
        }
        else
        {
            _fishingGear._depthLengthInput.interactable = true;

            if (DataManager.INSTANCE._depthLength > 0)
                _fishingGear._depthLengthInput.text = DataManager.INSTANCE._depthLength.ToString();
            else
                _fishingGear._depthLengthInput.text = 5.ToString();

            _currentFloatItem = ItemData.Instance.floatItemDB[_currentEquipmentDictionary["float"]];
            _floatImage.sprite = _currentFloatItem.itemImage;
            _floatImage.enabled = true;
            _currentEquipmentText[4].text = _currentFloatItem.korName;
            DataManager.INSTANCE.UpdateEquipmentFloatOn();
        }

        if (!_currentEquipmentDictionary["sinker"].Equals(-1))
        {
            _currentSinkerItem = ItemData.Instance.sinkerItemDB[_currentEquipmentDictionary["sinker"]];
            _sinkerImage.sprite = _currentSinkerItem.itemImage;
            _sinkerImage.enabled = true;
            _currentEquipmentText[5].text = _currentSinkerItem.korName;
            float w = ItemData.Instance.sinkerItemDB[_currentEquipmentDictionary["sinker"]]._sinkerWeight;
            DataManager.INSTANCE.UpdateEquipmentSinkerOn(w);
        }

        if (!_currentEquipmentDictionary["bait"].Equals(-1))
        {
            _currentBaitItem = ItemData.Instance.baitItemDB[_currentEquipmentDictionary["bait"]];
            _baitImage.sprite = _currentBaitItem.itemImage;
            _baitImage.enabled = true;
            _baitQuantity.text = _userData.GetBaitDictionary()[_currentBaitItem.serialNumber - 2000].ToString();
            _currentEquipmentText[2].text = _currentBaitItem.korName;
            DataManager.INSTANCE.UpdateEquipmentBaitOn(_currentBaitItem.serialNumber - 2000);
            _prevBaitItemSerialNumber = _currentBaitItem.serialNumber;

            // 루어 미끼라면 찌, 봉돌을 제거하고 커버를 씌워야한다.
            if (_currentBaitItem.typeNum.Equals(1))
            {
                ClickEquipmentSlot(4);
                ClickEquipmentSlot(5);
                _fishingGear.SinkerFloatCoverOn();
            }
            else
            {
                _fishingGear.SinkerFloatCoverOff();
            }
        }
        else
        {
            _prevBaitItemSerialNumber = -1;
            _baitImage.enabled = false;
            _currentEquipmentText[2].text = string.Empty;
            _baitQuantity.text = string.Empty;
            _fishingGear.SinkerFloatCoverOff();
        }

        if (!_currentEquipmentDictionary["pastebait"].Equals(-1))
        {
            _currentPastebaitItem = ItemData.Instance.pasetbaitItemDB[_currentEquipmentDictionary["pastebait"]];
            _pastebaitImage.sprite = _currentPastebaitItem.itemImage;
            _pastebaitImage.enabled = true;
            _currentEquipmentText[3].text = _currentPastebaitItem.korName;
            _pastebaitQuantity.text = _userData.GetPasteBaitDictionary()[_currentPastebaitItem.serialNumber - 3000].ToString();
        }
        else
        {
            _pastebaitImage.enabled = false;
            _currentEquipmentText[3].text = string.Empty;
            _pastebaitQuantity.text = string.Empty;
        }
    }

    // 장비 빼기
    public void ClickEquipmentSlot(int num)
    {
        // 현재 장착 중인 아이템이 있을 때
        // 현재 장착 중인 아이템이 없을 때
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipRemove).GetComponent<AudioPoolObject>().Init();
        switch (num)
        {
            // rod과 reel은 비어있을 수 없다.
            case 0:
                if (_currentRodItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _userData.GetCurrentEquipmentDictionary()["rod"] = -1;
                _currentRodItem = null;
                _rodImage.enabled = false;
                break;
            case 1:
                if (_currentReelItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _userData.GetCurrentEquipmentDictionary()["reel"] = -1;
                _currentReelItem = null;
                _reelImage.enabled = false;
                break;
            case 2:
                if (_currentBaitItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _fishingGear.SinkerFloatCoverOff();
                _userData.GetCurrentEquipmentDictionary()["bait"] = -1;
                DataManager.INSTANCE.UpdateEquipmentBaitOff(_currentBaitItem.serialNumber);
                _currentBaitItem = null;
                _baitImage.enabled = false;
                _baitQuantity.text = string.Empty;
                break;
            case 3:
                if (_currentPastebaitItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _userData.GetCurrentEquipmentDictionary()["pastebait"] = -1;
                _currentPastebaitItem = null;
                _pastebaitImage.enabled = false;
                _pastebaitQuantity.text = string.Empty;
                break;
            case 4:
                if (_currentFloatItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _userData.GetCurrentEquipmentDictionary()["float"] = -1;
                _currentFloatItem = null;
                _floatImage.enabled = false;
                _fishingGear._depthLengthInput.text = string.Empty;
                _fishingGear._depthLengthInput.interactable = false;
                DataManager.INSTANCE.UpdateEquipmentFloatOff();
                break;
            case 5:
                if (_currentSinkerItem == null)
                    return;

                _currentEquipmentText[num].text = string.Empty;
                _userData.GetCurrentEquipmentDictionary()["sinker"] = -1;
                _currentSinkerItem = null;
                _sinkerImage.enabled = false;
                DataManager.INSTANCE.UpdateEquipmentSinkerOff();
                break;
        }
    }
}
