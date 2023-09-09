using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class Shop : MonoBehaviour
{
    static Shop _uniqueInstance;
    static public Shop INSTANCE
    {
        get { return _uniqueInstance; }
    }

    [SerializeField] GameObject _passListObject;
    [SerializeField] GameObject _otherListObject;
    
    [SerializeField] ShopSlot[] slots;

    
    [SerializeField] Image[] tab;
    
    [SerializeField] Sprite tabSeletedSprite, tabUnselectedSprite;
    
    [SerializeField] Text _goldText;
    
    [SerializeField] Text _pearlText;
    
    [SerializeField] GameObject _randomBaitUI;
    [SerializeField] GameObject _randomBaitSlotPrefab;
    [SerializeField] Transform _randomBaitContent;
    List<Item> _baitList;
    List<Item> _pastebaitList;

    UserData _userdata = null;

    private void Awake()
    {
        _uniqueInstance = this;
    }

    private void Start()
    {
        if (_userdata == null)
            _userdata = DBManager.INSTANCE.GetUserData();

        _baitList = ItemData.Instance.baitItemDB;
        _pastebaitList = ItemData.Instance.pasetbaitItemDB;
    }
    private void OnEnable()
    {
        if (_userdata == null)
            _userdata = DBManager.INSTANCE.GetUserData();

        _goldText.text = _userdata._gold.ToString("#,##0");
        _pearlText.text = _userdata._pearl.ToString("#,##0");
        SetSlot(ItemData.Instance.passItemDB);

        _passListObject.SetActive(true);
        _otherListObject.SetActive(false);

        for (int i = 0; i < tab.Length; i++)
        {
            tab[i].sprite = i == 0 ? tabSeletedSprite : tabUnselectedSprite;
        }
    }
    
    void SetSlot(List<Item> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            bool isExist = i < items.Count;
            
            slots[i].gameObject.SetActive(isExist);
            
            if (isExist)
            {
                slots[i].SetItem(items[i]);
                
                slots[i].UpdateSlot();
            }

        }
    }
    void SetSlot2(List<Item> floatList, List<Item> sinkerList)
    {
        for (int i = 0; i < floatList.Count; i++)
        {
            bool isExist = i < floatList.Count;
            
            slots[i].gameObject.SetActive(isExist);
            
            if (isExist)
            {
                slots[i].SetItem(floatList[i]); 
                slots[i].UpdateSlot();
            }
                


        }

        for (int i = floatList.Count; i < slots.Length; i++)
        {
            bool isExist = i < sinkerList.Count + floatList.Count;
            
            slots[i].gameObject.SetActive(isExist);
            
            if (isExist)
            {
                slots[i].SetItem(sinkerList[i - floatList.Count]); 
                slots[i].UpdateSlot();
            }

        }
    }
    public void OnOff(bool isOn)
    {
        if(!isOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        
        gameObject.SetActive(isOn);
    }
    
    public void ClickButton(int contentNum)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        
        int currentTabNumber = 0;
        
        switch (contentNum)
        {
            // 패키지
            case 0:
                // 선택한 번호 0으로 초기화
                currentTabNumber = 0;
                _passListObject.SetActive(true);
                _otherListObject.SetActive(false);
                // 아이템 출력 함수(패키지)
                //SetSlot(ItemData.Instance.passItemDB);
                break;

            // 낚싯대
            case 1:
                currentTabNumber = 1;
                _passListObject.SetActive(false);
                _otherListObject.SetActive(true);
                // 아이템 출력 함수(낚싯대)
                SetSlot(ItemData.Instance.rodItemDB);
                break;

            // 릴
            case 2:
                currentTabNumber = 2;
                _passListObject.SetActive(false);
                _otherListObject.SetActive(true);
                // 아이템 출력 함수(릴)
                SetSlot(ItemData.Instance.reelItemDB);
                break;

            // 미끼
            case 3:
                currentTabNumber = 3;
                _passListObject.SetActive(false);
                _otherListObject.SetActive(true);
                // 아이템 출력 함수(미끼)
                SetSlot(ItemData.Instance._baitBoxItemDB);
                break;

            // 기타( 아이템 아예 없음 )
            case 4:
                currentTabNumber = 4;
                _passListObject.SetActive(false);
                _otherListObject.SetActive(true);
                // 아이템 출력 함수(찌/봉돌)
                SetSlot2(ItemData.Instance.floatItemDB, ItemData.Instance.sinkerItemDB);
                break;
        }
        // 항목(패키지, 낚싯대, 릴, 미끼, 기타)만큼 반복
        for (int i = 0; i < tab.Length; i++)
        {
            // 항목 이미지가 선택한 숫자와 같을 때 선택한 이미지로 넣고 나머지는 선택 안한 이미지
            tab[i].sprite = i == currentTabNumber ? tabSeletedSprite : tabUnselectedSprite;
        }
    }

    public void UpdateGoldPearl()
    {
        //_goldText.text = _userdata._gold.ToString();
        //_pearlText.text = _userdata._pearl.ToString();
        _goldText.text = _userdata._gold.ToString("#,##0");
        _pearlText.text = _userdata._pearl.ToString("#,##0");
    }

    public void RandomBaitUIOn(Dictionary<int, int> randomDic)
    {
        // 아이템 리스트로부터 미끼의 사진과 이름을 설정한다.
        int count = 0;
        int limit = _randomBaitContent.childCount;

        foreach (KeyValuePair<int, int> data in randomDic)
        {
            int index;
            Item item;

            // 미끼
            if (data.Key < 3000)
            {
                index = data.Key - 2000;
                item = _baitList[index];

                if (count >= limit)
                {
                    GameObject go = Instantiate(_randomBaitSlotPrefab, _randomBaitContent);
                    go.GetComponent<RandomBaitSlot>().Init(item.itemImage, item.korName, data.Value.ToString(), item.itemInfo);
                }
                else
                {
                    _randomBaitContent.GetChild(count).GetComponent<RandomBaitSlot>().Init(item.itemImage, item.korName, data.Value.ToString(), item.itemInfo);
                    count++;
                }
            }
            // 떡밥
            else
            {
                index = data.Key - 3000;
                item = _pastebaitList[index];

                if (count >= limit)
                {
                    GameObject go = Instantiate(_randomBaitSlotPrefab, _randomBaitContent);
                    go.GetComponent<RandomBaitSlot>().Init(item.itemImage, item.korName, data.Value.ToString(), item.itemInfo);
                }
                else
                {
                    _randomBaitContent.GetChild(count).GetComponent<RandomBaitSlot>().Init(item.itemImage, item.korName, data.Value.ToString(), item.itemInfo);
                    count++;
                }
            }
        }

        _randomBaitUI.SetActive(true);
    }

    public void RandomBaitUIOff()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        for (int i = 0; i < _randomBaitContent.childCount; i++)
        {
            if (!_randomBaitContent.GetChild(i).gameObject.activeSelf)
                break;
            _randomBaitContent.GetChild(i).gameObject.SetActive(false);
        }
        _randomBaitUI.SetActive(false);
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