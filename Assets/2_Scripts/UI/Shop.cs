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

    public GameObject _passListObject;
    public GameObject _otherListObject;

    // 아이템 나타낼 슬롯들
    //public ShopSlot[] slots; // 미끼가 너무 많아서 2줄로 처리했다. 그래서 Content를 따로 둔다.
    public ShopSlot[] slots; // 미끼가 낱개로 파는 것이 아니고 상자 형식으로 팔아서 1줄로 해도 된다.


    // 아이템 항목들(장비, 소비, 기타)
    public Image[] tab;

    // 선택된 항목, 비 선택 항목 나타낼 스프라이트 이미지
    public Sprite tabSeletedSprite, tabUnselectedSprite;

    // 보유 조개 표시할 텍스트
    public Text _goldText;

    // 보유 크리스탈 표시할 텍스트
    public Text _pearlText;

    // 미끼 상자를 구매했을 때 미끼들 나열하기
    public GameObject _randomBaitUI;
    public GameObject _randomBaitSlotPrefab;
    public Transform _randomBaitContent;
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

        //SetSlot(ItemData.Instance.passItemDB);

        //for (int i = 0; i < tab.Length; i++)
        //{
        //    tab[i].sprite = i == 0 ? tabSeletedSprite : tabUnselectedSprite;
        //}
    }
    private void OnEnable()
    {
        if (_userdata == null)
            _userdata = DBManager.INSTANCE.GetUserData();

        _goldText.text = _userdata._gold.ToString("#,##0");
        _pearlText.text = _userdata._pearl.ToString("#,##0");

        ////현재 보유 조개 양 표시
        //_goldText.text = string.Format("{0:#,0}", _userdata._gold);

        ////현재 보유 크리스탈 양 표시
        //_pearlText.text = string.Format("{0:#,0}", _userdata._pearl);

        // 패키지 항목 아이템 불러옴
        SetSlot(ItemData.Instance.passItemDB);

        _passListObject.SetActive(true);
        _otherListObject.SetActive(false);

        for (int i = 0; i < tab.Length; i++)
        {
            tab[i].sprite = i == 0 ? tabSeletedSprite : tabUnselectedSprite;
        }
    }

    /// <summary>
    /// 슬롯을 정렬하고 아이템 출력하는 함수
    /// </summary>
    /// <param name="items"></param>
    void SetSlot(List<Item> items)
    {
        // 슬롯 갯수 만큼 반복(25)
        for (int i = 0; i < slots.Length; i++)
        {
            // 아이템 존재 갯수 만큼 bool형으로 치환
            bool isExist = i < items.Count;

            // 슬롯을 치환한 아이템 갯수 만큼 활성화
            slots[i].gameObject.SetActive(isExist);

            // 활성화가 됐다면
            if (isExist)
                slots[i]._item = items[i]; // 슬롯 아이템에 아이템을 넣음

            // 슬롯을 업데이트(사진, 가격표시, 재화이밎, 아이템이미지)함
            slots[i].UpdateSlot();
        }
    }
    void SetSlot2(List<Item> floatList, List<Item> sinkerList)
    {
        // 슬롯 갯수 만큼 반복(25)
        for (int i = 0; i < floatList.Count; i++)
        {
            // 아이템 존재 갯수 만큼 bool형으로 치환
            bool isExist = i < floatList.Count;

            // 슬롯을 치환한 아이템 갯수 만큼 활성화
            slots[i].gameObject.SetActive(isExist);

            // 활성화가 됐다면
            if (isExist)
                slots[i]._item = floatList[i]; // 슬롯 아이템에 아이템을 넣음

            // 슬롯을 업데이트(사진, 가격표시, 재화이밎, 아이템이미지)함
            slots[i].UpdateSlot();
        }

        for (int i = floatList.Count; i < slots.Length; i++)
        {
            // 아이템 존재 갯수 만큼 bool형으로 치환
            bool isExist = i < sinkerList.Count + floatList.Count;

            // 슬롯을 치환한 아이템 갯수 만큼 활성화
            slots[i].gameObject.SetActive(isExist);

            // 활성화가 됐다면
            if (isExist)
                slots[i]._item = sinkerList[i - floatList.Count]; // 슬롯 아이템에 아이템을 넣음

            // 슬롯을 업데이트(사진, 가격표시, 재화이밎, 아이템이미지)함
            slots[i].UpdateSlot();
        }
    }
    /// <summary>
    /// 상점 켰다 껐다 하는 함수
    /// </summary>
    /// <param name="isOn"></param>
    public void OnOff(bool isOn)
    {
        if(!isOn)
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

        // bool형에 따라 켰다가 껐다가 함
        gameObject.SetActive(isOn);
    }

    /// <summary>
    /// 항목 선택하는 함수
    /// </summary>
    /// <param name="contentNum"></param>
    public void ClickButton(int contentNum)
    {
        // 클릭 효과음 재생
        //SoundManager.instance.EffectPlay("UIClick");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        // 선택한 번호 지역변수 초기화
        int currentTabNumber = 0;

        // 탭 번호로 구분
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