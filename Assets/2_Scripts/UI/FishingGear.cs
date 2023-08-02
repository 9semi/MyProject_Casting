using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FishingGear : MonoBehaviour
{
    static FishingGear _uniqueInstance;
    static public FishingGear INSTANCE
    {
        get { return _uniqueInstance; }
    }

    // 낚싯대 껴야된다는 메세지 오브젝트 
    [SerializeField] GameObject rodEssentialMessage;

    // 현재 골드, 조개 나타낼 텍스트
    [SerializeField] Text _goldText;
    [SerializeField] Text _pearlText;

    // 탭 이미지, 아이템 이미지
    [SerializeField] Image[] _buttonImages;

    // 장착 중인 아이템 슬롯 빨간 테두리 오브젝트
    [SerializeField] GameObject[] _frames;

    // 루어 미끼를 끼면 봉돌과 찌는 장착 불가능하게 만든다.
    [SerializeField] GameObject _floatCover;
    [SerializeField] GameObject _sinkerCover;

    // 선택안된 탭 이미지 스프라이트, 선택된 탭 이미지 스프라이트
    [SerializeField] Sprite tabUnselectedSprite, tabSeletedSprite;

    // 채비 슬롯(현재 가지고 있는 아이템 나타낼 슬롯)
    [SerializeField] Transform _content;
    Slot[] _slots;

    // 바늘이 물에 들어갔는지 판별하기 위한 게임매니저
    [SerializeField] GameManager _gameManager;

    // 면사매듭 길이 입력받는 창
    [SerializeField] InputField _depthLengthInput; public InputField GetDepthLengthInput() { return _depthLengthInput; }

    // equipment 스크립트 가지고 있어야 장비를 바꿀 때마다 초기화 계속 할듯
    [SerializeField] Equipment _eq;

    int _currentButtonNumber;

    UserData _userData = null;

    List<Item> _rodList = new List<Item>();
    List<Item> _reelList = new List<Item>();
    List<Item> _baitList = new List<Item>();
    List<Item> _pastebaitList = new List<Item>();
    List<Item> _floatList = new List<Item>();
    List<Item> _sinkerList = new List<Item>();

    Dictionary<int, int> _userDataDic = new Dictionary<int, int>();

    private void Awake()
    {
        _uniqueInstance = this;
        // 아이템 리스트에서 모든 리스트 가져오기
        GetAllItemDataList();
    }

    private void OnEnable()
    {
        // 가방여는 효과음 재생
        //SoundManager.instance.EffectPlay("Bag");
        //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.장비클릭);

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        // 골드, 진주 초기화
        UpdateGoldPearl();

        // 슬롯 초기화
        _slots = new Slot[_content.childCount];

        for (int i = 0; i < _content.childCount; i++)
        {
            _slots[i] = _content.GetChild(i).GetComponent<Slot>();
        }

        // 장비 칸 아이템 불러오기
        ButtonSelcet(0);
        _eq.CheckCurrentEquipment();
    }

    public void UpdateGoldPearl()
    {
        //_goldText.text = _userdata._gold.ToString();
        //_pearlText.text = _userdata._pearl.ToString();
        _goldText.text = _userData._gold.ToString("#,##0");
        _pearlText.text = _userData._pearl.ToString("#,##0");
    }

    public void UpdateBait()
    {
        ButtonSelcet(_currentButtonNumber);
    }

    void GetAllItemDataList()
    {
        _rodList = ItemData.Instance.rodItemDB;
        _reelList = ItemData.Instance.reelItemDB;
        _baitList = ItemData.Instance.baitItemDB;
        _pastebaitList = ItemData.Instance.pasetbaitItemDB;
        _floatList = ItemData.Instance.floatItemDB;
        _sinkerList = ItemData.Instance.sinkerItemDB;
    }
    /// <summary>
    /// 채비창 켰다 껐다 하는 함수
    /// </summary>
    /// <param name="isOn"></param>
    public void FishingGearOff()
    {
        // 가방여는 효과음 재생
        //SoundManager.instance.EffectPlay("Bag");
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();

        // 채비창을 끄는데 낚싯대 슬롯에 아이템이 없으면
        if (_eq.GetCurrentRodItem() == null || _eq.GetCurrentReelItem() == null)
        {
            // 낚싯대 끼라는 메세지 활성화
            SetRodEssentialMessage(true);
        }
        else
        {
            //Debug.LogError(ItemData.Instance.rodItemDB[_userData.GetCurrentEquipmentDictionary()["rod"]].rodMaterial);

            if (!DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.lobby))
            {
                _gameManager.RodMeshOn(ItemData.Instance.rodItemDB[_userData.GetCurrentEquipmentDictionary()["rod"]].rodMaterial);

                if(_userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
                {
                    _gameManager.UpdateFishSearchRange(0);
                }
                else
                {
                    _gameManager.UpdateFishSearchRange(3);
                }

                if (Equipment.INSTANCE.IsBaitChange)
                {
                    Equipment.INSTANCE.IsBaitChange = false;
                    DataManager.INSTANCE.CheckBaitProbability();
                }
            }

            _userData.GetCurrentEquipmentDictionary()["depthlength"] = DataManager.INSTANCE._depthLength;

            // 잘 꺼진다면 파이어베이스에 현재 장착 중인 아이템들 업데이트 해줘야 한다.
            DBManager.INSTANCE.UpdateEquipment();
            gameObject.SetActive(false);
        }
    }
    public void SetRodEssentialMessage(bool isOn)
    {
        rodEssentialMessage.SetActive(isOn);
    }
    public void ButtonSelcet(int n)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        
        _currentButtonNumber = n;
        _userDataDic = new Dictionary<int, int>();
        // 탭 이름으로 구분
        switch (n)
        {
            // 패키지일 때 0
            case 0:
                _userDataDic = _userData.GetRodDictionary();
                //_currentButtonNumber = 0;
                SetSlot(_rodList);
                break;
            case 1:
                _userDataDic = _userData.GetReelDictionary();
                //_currentButtonNumber = 1;
                SetSlot(_reelList);
                break;
            case 2:
                _userDataDic = _userData.GetBaitDictionary();
                //_currentButtonNumber = 2;
                SetBaitSlot(_baitList, false);
                break;
            case 3:
                _userDataDic = _userData.GetBaitDictionary();
                //_currentButtonNumber = 2;
                SetBaitSlot(_baitList, true);
                break;
            case 4:
                _userDataDic = _userData.GetPasteBaitDictionary();
                //_currentButtonNumber = 3;
                SetSlot(_pastebaitList);
                break;
            case 5:
                //_userDataDic = _userData.GetSinkerDictionary();
                //_currentButtonNumber = 4;
                SetSlot2(_floatList, _sinkerList);
                break;
        }

        for (int i = 0; i < _buttonImages.Length; i++)
        {
            _buttonImages[i].sprite = i.Equals(_currentButtonNumber) ? tabSeletedSprite : tabUnselectedSprite;
        }

        SlotFrameUpdate(_currentButtonNumber);
    }


    public void SetSlot(List<Item> items)
    {
        int cnt = 0;

        // 현재 가지고 있는 아이템들을 슬롯에 뿌리고
        for (int i = 0; i < _userDataDic.Count; i++)
        {
            if (!_userDataDic[i].Equals(0))
            {
                // 한개 이상 있다면 그 아이템의 정보를 슬롯으로 넘겨준다.
                _slots[cnt].Quantity = _userDataDic[i];
                _slots[cnt].UpdateSlot(items[i], _currentButtonNumber);
                cnt++;
            }
        }

        // 빈 슬롯은 sprite를 지운다.
        for (int i = cnt; i < _slots.Length; i++)
        {
            _slots[i].RemoveSlot();
        }
    }

    public void SetBaitSlot(List<Item> baitList, bool isLure)
    {
        int cnt = 0;

        // 루어 미끼 버튼을 눌렀다면
        if (isLure)
        {
            for (int i = 0; i < _userDataDic.Count; i++)
            {
                int n = _userDataDic[i];
                if (!n.Equals(0) && baitList[i].typeNum.Equals(1))
                {
                    // 한개 이상 있다면 그 아이템의 정보를 슬롯으로 넘겨준다.
                    _slots[cnt].Quantity = n;
                    _slots[cnt].UpdateSlot(baitList[i], _currentButtonNumber);
                    cnt++;
                }
            }
        }
        else
        {
            for (int i = 0; i < _userDataDic.Count; i++)
            {
                int n = _userDataDic[i];
                if (!n.Equals(0) && baitList[i].typeNum.Equals(0))
                {
                    // 한개 이상 있다면 그 아이템의 정보를 슬롯으로 넘겨준다.
                    _slots[cnt].Quantity = n;
                    _slots[cnt].UpdateSlot(baitList[i], _currentButtonNumber);
                    cnt++;
                }
            }
        }

        // 빈 슬롯은 sprite를 지운다.
        for (int i = cnt; i < _slots.Length; i++)
        {
            _slots[i].RemoveSlot();
        }
    }

    public void SetSlot2(List<Item> floatList, List<Item> sinkerList)
    {
        int cnt = 0;

        _userDataDic = _userData.GetFloatDictionary();

        // 현재 가지고 있는 아이템들을 슬롯에 뿌리고
        for (int i = 0; i < _userDataDic.Count; i++)
        {
            if (!_userDataDic[i].Equals(0))
            {
                // 한개 이상 있다면 그 아이템의 정보를 슬롯으로 넘겨준다.
                _slots[cnt].Quantity = _userDataDic[i];
                _slots[cnt].UpdateSlot(floatList[i], _currentButtonNumber);
                cnt++;
            }
        }

        _userDataDic = _userData.GetSinkerDictionary();

        // 현재 가지고 있는 아이템들을 슬롯에 뿌리고
        for (int i = 0; i < _userDataDic.Count; i++)
        {
            if (!_userDataDic[i].Equals(0))
            {
                // 한개 이상 있다면 그 아이템의 정보를 슬롯으로 넘겨준다.
                _slots[cnt].Quantity = _userDataDic[i];
                _slots[cnt].UpdateSlot(sinkerList[i], _currentButtonNumber);
                cnt++;
            }
        }

        // 빈 슬롯은 sprite를 지운다.
        for (int i = cnt; i < _slots.Length; i++)
        {
            _slots[i].RemoveSlot();
        }
    }


    void SlotFrameUpdate(int num)
    {
        //for(int i = 0; i < _frames.Length; i++)
        //{
        //    if (i.Equals(num))
        //        _frames[i].SetActive(true);
        //    else
        //        _frames[i].SetActive(false);
        //}
        switch(num)
        {
            case 0:
            case 1:
            case 4:
                for (int i = 0; i < _frames.Length; i++)
                {
                    if (i.Equals(num))
                        _frames[i].SetActive(true);
                    else
                        _frames[i].SetActive(false);
                }
                break;
            case 2:
            case 3:
                for (int i = 0; i < _frames.Length; i++)
                {
                    if (i.Equals(2))
                        _frames[i].SetActive(true);
                    else
                        _frames[i].SetActive(false);
                }
                break;
            case 5:
                for (int i = 0; i < _frames.Length; i++)
                {
                    if (i.Equals(3) || i.Equals(5))
                        _frames[i].SetActive(true);
                    else
                        _frames[i].SetActive(false);
                }
                break;
        }
    }
    public void UpdateDepthLength()
    {
        if (_depthLengthInput.text == string.Empty)
        {
            DataManager.INSTANCE._depthLength = 5;
            _depthLengthInput.text = 5.ToString();
            return;
        }
        int length = int.Parse(_depthLengthInput.text);

        if(length < 1)
        {
            DataManager.INSTANCE._depthLength = 5;
            _depthLengthInput.text = 5.ToString();
        }
        else
        {
            DataManager.INSTANCE._depthLength = length;
            _depthLengthInput.text = length.ToString();
        }
    }

    public void SinkerFloatCoverOn()
    {
        _sinkerCover.SetActive(true);
        _floatCover.SetActive(true);
    }
    public void SinkerFloatCoverOff()
    {
        if(_sinkerCover.activeSelf)
        {
            _sinkerCover.SetActive(false);
            _floatCover.SetActive(false);
        }
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