using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishInfoUI : MonoBehaviour
{
    [SerializeField] Transform _slotParent;
    [SerializeField] Text _nameText;
    [SerializeField] AquariumManager _aquariumManager;
    [SerializeField] BuyAquariumUI _buyUI;

    [Header("수족관 관련")]
    [SerializeField] Sprite _blueButtonSprite;
    [SerializeField] Sprite _redButtonSprite;
    [SerializeField] GameObject _selectAquariumUI;
    [SerializeField] GameObject _sellConfirmUI;
    [SerializeField] Image[] _aquariumButtonImages;
    [SerializeField] Text[] _aquariumButtonCountTexts;
    [SerializeField] Text[] _aquariumButtonPossessTexts;
    
    List<int> _aquariumCountList;
    UserData _userData;

    // FishInfoSlot용 정보
    List<PublicDefined.stFishInfo> _fishInfoLIst;
    AquariumFishInfoSlot[] _slots;

    // 물고기를 수족관에서 옮길 때 필요한 정보
    PublicDefined.stFishInfo _stFishInfo;
    int _currentAquariumNumber;

    AquariumUI _aquariumUI;
    int _currentFishListIndex;

    private void Start()
    {
        _aquariumManager.SetFishInfoUIInstance(this);
        _userData = DBManager.INSTANCE.GetUserData();
    }
    public void SetAquariumUIInstance(AquariumUI instance)
    {
        _aquariumUI = instance;
    }
    public void InitSlot()
    {
        _slots = new AquariumFishInfoSlot[_slotParent.childCount];

        for (int i = 0; i < _slotParent.childCount; i++)
        {
            _slots[i] = _slotParent.GetChild(i).GetComponent<AquariumFishInfoSlot>();
        }
    }

    public void OnFishInfoSlot(List<PublicDefined.stFishInfo> list, int currentAquariumNumber)
    {
        int cnt = 0;
        _currentAquariumNumber = currentAquariumNumber;
        _fishInfoLIst = list;
        _nameText.text = list[0]._name;
        for (int i = 0; i < _fishInfoLIst.Count; i++)
        {
            _slots[i].InitFishInfoSlot(list[i], this);
            cnt++;
        }

        for (int i = cnt; i < _slots.Length; i++)
        {
            _slots[i].OffSlot();
        }
    }
    public void UpdateInfoSlot(PublicDefined.stFishInfo stFishInfo)
    {
        int cnt = 0;
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);
        _fishInfoLIst = dic[stFishInfo._fishNumber];

        if(_fishInfoLIst.Count.Equals(0))
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i].OffSlot();
            }
        }
        else
        {
            _nameText.text = _fishInfoLIst[0]._name;
            for (int i = 0; i < _fishInfoLIst.Count; i++)
            {
                _slots[i].InitFishInfoSlot(_fishInfoLIst[i], this);
                cnt++;
            }

            for (int i = cnt; i < _slots.Length; i++)
            {
                _slots[i].OffSlot();
            }
        }
    }

    public void ClickShiftButton(int listIndex, PublicDefined.stFishInfo stFishInfo)
    {
        PlayClickEffectAudio();
        _currentFishListIndex = listIndex;
        _stFishInfo = stFishInfo;

        _selectAquariumUI.SetActive(true);

        _aquariumCountList = DBManager.INSTANCE.GetUserData().GetAquariumCountState();

        for (int i = 0; i < _aquariumCountList.Count; i++)
        {
            _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = true;
            //Debug.Log(i + "번째 수족관 개체 수 : " + _aquariumCountList[i]);
            if (_aquariumCountList[i].Equals(-1))
            {
                // 수족관 자체를 가지고 있지 않다.

                _aquariumButtonImages[i].sprite = _redButtonSprite;
                _aquariumButtonCountTexts[i].text = "구매하기";
                _aquariumButtonPossessTexts[i].text = string.Empty;
            }
            else if (_aquariumCountList[i].Equals(30))
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
                _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
            }
        }
    }

    public void SelectAquariumUIUpdate()
    {
        _aquariumCountList = DBManager.INSTANCE.GetUserData().GetAquariumCountState();

        for (int i = 0; i < _aquariumCountList.Count; i++)
        {
            _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = true;
            //Debug.Log(i + "번째 수족관 개체 수 : " + _aquariumCountList[i]);
            if (_aquariumCountList[i].Equals(-1))
            {
                // 수족관 자체를 가지고 있지 않다.

                _aquariumButtonImages[i].sprite = _redButtonSprite;
                _aquariumButtonCountTexts[i].text = "구매하기";
                _aquariumButtonPossessTexts[i].text = string.Empty;
            }
            else if (_aquariumCountList[i].Equals(30))
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
                _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "보유 중";
            }
        }
    }

    public void ClickFishSave(int aquariumNum) // SelectAquariumUI에서 수족관 고르면 들어오는 함수
    {
        // _currentAquariumNumber번 수족관에서 aquariumNum번 수족관으로 옮긴다.
        if (_currentAquariumNumber.Equals(aquariumNum))
        {
            // 수족관을 보유하고 있지 않아서 구매하기 버튼을 누른 사람들이 들어온다.

            return;
        }

        if (_aquariumCountList[aquariumNum].Equals(-1))
        {
            // 수족관을 보유하고 있지 않아서 구매하기 버튼을 누른 사람들이 들어온다.
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

            switch (aquariumNum)
            {
                case 1:
                    _buyUI.Init(false, 80000, aquariumNum, _aquariumUI.GetBackgroundSprite(aquariumNum), _userData, _aquariumUI);
                    break;
                case 2:
                    _buyUI.Init(false, 150000, aquariumNum, _aquariumUI.GetBackgroundSprite(aquariumNum), _userData, _aquariumUI);
                    break;
                case 3:
                    _buyUI.Init(true, 7900, aquariumNum, _aquariumUI.GetBackgroundSprite(aquariumNum), _userData, _aquariumUI);
                    break;
                case 4:
                    _buyUI.Init(true, 14900, aquariumNum, _aquariumUI.GetBackgroundSprite(aquariumNum), _userData, _aquariumUI);
                    break;
            }

            return;
        }



        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        // 수족관 물고기 카운트 리스트 업데이트
        _userData.UpdateCountStateList(_currentAquariumNumber, aquariumNum);

        // 이전 수족관에서 물고기 삭제
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);

        List<PublicDefined.stFishInfo> list = dic[_stFishInfo._fishNumber];

        // 이전 수족관 딕셔너리의 리스트에서 제거한다.
        list.RemoveAt(_currentFishListIndex);

        dic = _userData.SelectAquariumDictionary(aquariumNum);
        // 다음 수족관에 물고기 추가
        if (dic.ContainsKey(_stFishInfo._fishNumber))
        {
            // 만약 같은 번호의 물고기가 있다면
            dic[_stFishInfo._fishNumber].Add(_stFishInfo);
        }
        else
        {
            // 만약 같은 번호의 물고기가 없다면 최초의 물고기를 등록한다.
            List<PublicDefined.stFishInfo> tempList = new List<PublicDefined.stFishInfo>();
            tempList.Add(_stFishInfo);
            dic.Add(_stFishInfo._fishNumber, tempList);
        }

        // 수족관 데이터 업데이트
        // PC테스트: 파이어베이스 업데이트는 잠시 막는다.

        DBManager.INSTANCE.ShiftFish(_currentAquariumNumber, aquariumNum, _stFishInfo);

        // 해당 물고기 부모를 옮기자
        _aquariumManager.ShiftFish(_stFishInfo, _currentAquariumNumber, aquariumNum);

        // AquariumUI에서 수족관 버튼 개체 수 업데이트
        _aquariumManager.GetAquariumUIInstance().CheckAquariumPossessState();

        // 수족관 고르기 UI 끄기
        _selectAquariumUI.SetActive(false);

    }
    public void ClickSellButton(int index, PublicDefined.stFishInfo stFishInfo)
    {
        PlayClickEffectAudio();
        _currentFishListIndex = index;
        _stFishInfo = stFishInfo;
        _sellConfirmUI.SetActive(true);
    }

    public void ClickSellOKButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.coin).GetComponent<AudioPoolObject>().Init();

        if (_userData._currentJeongdongjinPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl)
            || _userData._currentSkywayPassIndex.Equals((int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl) 
            || _userData._currentHomerspitPassIndex.Equals((int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl))
        {
            PassManager.INSTANCE.ToPassManagerAboutCommon();
        }

        // 수족관 물고기 카운트 리스트 업데이트
        _userData.UpdateCountStateList(false, _currentAquariumNumber);

        // 이전 수족관에서 물고기 삭제
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);
        List<PublicDefined.stFishInfo> list = dic[_stFishInfo._fishNumber];

        // 만약 대표물고기를 팔았다면 업데이트 해줘야한다.
        if (_userData._haveRepresentFish)
        {
            if (list[_currentFishListIndex]._key.Equals(_userData.GetRepresentFish()._key))
            {
                _userData._haveRepresentFish = false;
                Dictionary<string, object> updateDic = new Dictionary<string, object>();
                updateDic.Add("/_haveRepresentFish/", false);
                DBManager.INSTANCE.UpdateFirebase(updateDic);
            }
        }

        //Debug.Log("현재 골드: " + _userData._gold + "에 " + list[_currentFishListIndex]._price + "을 추가한다.");

        _userData._gold += list[_currentFishListIndex]._price;
        _aquariumUI.SetGoldText();
        DBManager.INSTANCE.SellFish(_currentAquariumNumber, _stFishInfo);
        list.RemoveAt(_currentFishListIndex);

        // AquariumUI에서 수족관 버튼 개체 수 업데이트
        _aquariumManager.SellFish(_stFishInfo, _currentAquariumNumber);
        _aquariumManager.GetAquariumUIInstance().CheckAquariumPossessState();
        _sellConfirmUI.SetActive(false);
    }

    public void ClickXButton()
    {
        //_uiCamera.depth = -1;
        PlayClickEffectAudio();
        gameObject.SetActive(false);
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
