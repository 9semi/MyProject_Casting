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

    [Header("������ ����")]
    [SerializeField] Sprite _blueButtonSprite;
    [SerializeField] Sprite _redButtonSprite;
    [SerializeField] GameObject _selectAquariumUI;
    [SerializeField] GameObject _sellConfirmUI;
    [SerializeField] Image[] _aquariumButtonImages;
    [SerializeField] Text[] _aquariumButtonCountTexts;
    [SerializeField] Text[] _aquariumButtonPossessTexts;
    
    List<int> _aquariumCountList;
    UserData _userData;

    // FishInfoSlot�� ����
    List<PublicDefined.stFishInfo> _fishInfoLIst;
    AquariumFishInfoSlot[] _slots;

    // ����⸦ ���������� �ű� �� �ʿ��� ����
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
            //Debug.Log(i + "��° ������ ��ü �� : " + _aquariumCountList[i]);
            if (_aquariumCountList[i].Equals(-1))
            {
                // ������ ��ü�� ������ ���� �ʴ�.

                _aquariumButtonImages[i].sprite = _redButtonSprite;
                _aquariumButtonCountTexts[i].text = "�����ϱ�";
                _aquariumButtonPossessTexts[i].text = string.Empty;
            }
            else if (_aquariumCountList[i].Equals(30))
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "���� ��";
                _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "���� ��";
            }
        }
    }

    public void SelectAquariumUIUpdate()
    {
        _aquariumCountList = DBManager.INSTANCE.GetUserData().GetAquariumCountState();

        for (int i = 0; i < _aquariumCountList.Count; i++)
        {
            _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = true;
            //Debug.Log(i + "��° ������ ��ü �� : " + _aquariumCountList[i]);
            if (_aquariumCountList[i].Equals(-1))
            {
                // ������ ��ü�� ������ ���� �ʴ�.

                _aquariumButtonImages[i].sprite = _redButtonSprite;
                _aquariumButtonCountTexts[i].text = "�����ϱ�";
                _aquariumButtonPossessTexts[i].text = string.Empty;
            }
            else if (_aquariumCountList[i].Equals(30))
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "���� ��";
                _aquariumButtonImages[i].transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                _aquariumButtonImages[i].sprite = _blueButtonSprite;
                _aquariumButtonCountTexts[i].text = _aquariumCountList[i].ToString() + " / 30";
                _aquariumButtonPossessTexts[i].text = "���� ��";
            }
        }
    }

    public void ClickFishSave(int aquariumNum) // SelectAquariumUI���� ������ ���� ������ �Լ�
    {
        // _currentAquariumNumber�� ���������� aquariumNum�� ���������� �ű��.
        if (_currentAquariumNumber.Equals(aquariumNum))
        {
            // �������� �����ϰ� ���� �ʾƼ� �����ϱ� ��ư�� ���� ������� ���´�.

            return;
        }

        if (_aquariumCountList[aquariumNum].Equals(-1))
        {
            // �������� �����ϰ� ���� �ʾƼ� �����ϱ� ��ư�� ���� ������� ���´�.
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
        // ������ ����� ī��Ʈ ����Ʈ ������Ʈ
        _userData.UpdateCountStateList(_currentAquariumNumber, aquariumNum);

        // ���� ���������� ����� ����
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);

        List<PublicDefined.stFishInfo> list = dic[_stFishInfo._fishNumber];

        // ���� ������ ��ųʸ��� ����Ʈ���� �����Ѵ�.
        list.RemoveAt(_currentFishListIndex);

        dic = _userData.SelectAquariumDictionary(aquariumNum);
        // ���� �������� ����� �߰�
        if (dic.ContainsKey(_stFishInfo._fishNumber))
        {
            // ���� ���� ��ȣ�� ����Ⱑ �ִٸ�
            dic[_stFishInfo._fishNumber].Add(_stFishInfo);
        }
        else
        {
            // ���� ���� ��ȣ�� ����Ⱑ ���ٸ� ������ ����⸦ ����Ѵ�.
            List<PublicDefined.stFishInfo> tempList = new List<PublicDefined.stFishInfo>();
            tempList.Add(_stFishInfo);
            dic.Add(_stFishInfo._fishNumber, tempList);
        }

        // ������ ������ ������Ʈ
        // PC�׽�Ʈ: ���̾�̽� ������Ʈ�� ��� ���´�.

        DBManager.INSTANCE.ShiftFish(_currentAquariumNumber, aquariumNum, _stFishInfo);

        // �ش� ����� �θ� �ű���
        _aquariumManager.ShiftFish(_stFishInfo, _currentAquariumNumber, aquariumNum);

        // AquariumUI���� ������ ��ư ��ü �� ������Ʈ
        _aquariumManager.GetAquariumUIInstance().CheckAquariumPossessState();

        // ������ ���� UI ����
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

        // ������ ����� ī��Ʈ ����Ʈ ������Ʈ
        _userData.UpdateCountStateList(false, _currentAquariumNumber);

        // ���� ���������� ����� ����
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);
        List<PublicDefined.stFishInfo> list = dic[_stFishInfo._fishNumber];

        // ���� ��ǥ����⸦ �ȾҴٸ� ������Ʈ ������Ѵ�.
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

        //Debug.Log("���� ���: " + _userData._gold + "�� " + list[_currentFishListIndex]._price + "�� �߰��Ѵ�.");

        _userData._gold += list[_currentFishListIndex]._price;
        _aquariumUI.SetGoldText();
        DBManager.INSTANCE.SellFish(_currentAquariumNumber, _stFishInfo);
        list.RemoveAt(_currentFishListIndex);

        // AquariumUI���� ������ ��ư ��ü �� ������Ʈ
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
