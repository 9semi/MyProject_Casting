using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AquariumUI : MonoBehaviour
{
    public enum eFishSlotType
    {
        On,
        Off,
    }
    
    public enum eCameraType
    {
        Move,
        Fix,
    }

    readonly Color _translucenceColor = new Color(1, 1, 1, 0.5f);
    readonly int _isOnHash = Animator.StringToHash("isOn");

    [Header("Aquarium Manager")]
    [SerializeField] AquariumManager _aquariumManamger;
    [SerializeField] AquariumEffectManager _aquariumEffectManager;
    
    [Header("������ �޴� UI")]
    [SerializeField] Text _goldText; public void SetGoldText() { _goldText.text = _userData._gold.ToString(); }
    [SerializeField] Text _pearlText;
    [SerializeField] Image _BG;
    [SerializeField] Button[] _aquariumButtons;
    [SerializeField] Text[] _fishCountTexts;
    [SerializeField] Sprite[] _bgSprites; public Sprite GetBackgroundSprite(int aquariumNumber) { return _bgSprites[aquariumNumber]; }
    [SerializeField] Button _enterButton;
    [SerializeField] GameObject _topBarObject;
    [SerializeField] GameObject _aquariumButtonsObject;

    [Header("������ ���� UI")]
    [SerializeField] Transform _slotParent;
    [SerializeField] GameObject _fishSlotUIObject;
    [SerializeField] GameObject _exitButton;
    [SerializeField] Button _updownButton;
    [SerializeField] Transform _updownButtonTransform;
    [SerializeField] Transform _downPos;
    [SerializeField] Transform _upPos;
    [SerializeField] GameObject _fishInfoUIObject;
    [SerializeField] GameObject _positionResetButtonObject;

    [Header("������ ���� UI")]
    [SerializeField] BuyAquariumUI _buyAquariumUI;

    Sprite _originBG;
    UserData _userData;
    FishInfoUI _fishInfoUI;
    AquariumFishSlot[] _fishSlot;

    List<bool> _aquariumState = new List<bool>();
    int _currentAquariumNumber = -1;
    Camera _mainCamera;
    eFishSlotType _currentFishSlotState = eFishSlotType.On; public eFishSlotType CurrentFishSlotState { get { return _currentFishSlotState; } }

    private void Start()
    {
        _userData = DBManager.INSTANCE.GetUserData();
        _mainCamera = _aquariumManamger.Cam;
        _goldText.text = _userData._gold.ToString("#,##0");
        _pearlText.text = _userData._pearl.ToString("#,##0");
        _aquariumState = _userData.GetAquariumPossessState(); // ������ ��� bool������ ������� �� ����Ʈ�� ������Ʈ ����� �Ѵ�.
        _originBG = _BG.sprite;
        _enterButton.interactable = false;
        _fishInfoUI = _fishInfoUIObject.GetComponent<FishInfoUI>();
        _fishInfoUI.SetAquariumUIInstance(this);
        _fishInfoUI.InitSlot();
        _aquariumManamger.SetAquariumUIInstance(this);
        CheckAquariumPossessState();
        InitFishSlot();
    }

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
        switch (delayNumber)
        {
            case 1:
                yield return PublicDefined._05secDelay;
                break;
            default:
                yield return PublicDefined._05secDelay;
                break;
        }
        action();
    }

    public void ClickXButton()
    {
        PlayExitEffectAudio();
        DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;
        //LoadingSceneManager.LoadScene("LobbyScene");
        SceneManager.LoadScene("LobbyScene");
    }
    public void ClickAquariumButton(int n) // ������ ���� ��ư
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileArrow).GetComponent<AudioPoolObject>().Init();
        // �Ȱ��� ��ư�� �����ٸ� ������ �����ϰ� ������ ������� �������´�. EnterButton�� ���Ƶд�.
        if (_currentAquariumNumber.Equals(n))
        {
            _currentAquariumNumber = -1;
            _aquariumButtons[n].transform.GetComponent<Image>().color = _translucenceColor;
            _enterButton.interactable = false;
            _BG.sprite = _originBG;
        }
        // �ٸ� ��ư�� �����ٸ� ����� �ٲ��ֵ� �ش� �������� �ִ��� Ȯ���ϰ� ������ EnterButton�� �����ش�.
        else
        {
            _currentAquariumNumber = n;
            EnterButtonsUpdate(n);
            AquariumButtonsUpdate(n);
            ChangeBG(n);
        }
    }
    public void ClickEnterButton() // ���������� ���� ��ư
    {
        // ������ �̹� �������� Ȥ�� �𸣴ϱ� �� �� �� ���´�. ���� ��ư�� �������� �����ϰ� ���� �ʴٸ� return
        if (!_aquariumState[_currentAquariumNumber] || _currentAquariumNumber.Equals(-1))
            return;

        PlayClickEffectAudio();
        PlayBGM();

        _aquariumEffectManager.BGEffectSetActive(false);

        if (_userData._currentJeongdongjinPassIndex.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl)
            || _userData._currentSkywayPassIndex.Equals((int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl)
            || _userData._currentHomerspitPassIndex.Equals((int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl))
        {
            PassManager.INSTANCE.ToPassManagerAboutCommon();
        }

        // ī�޶� ȸ��, FOV �ʱ�ȭ
        _mainCamera.fieldOfView = 90;
        _aquariumManamger._finalFov = 90;

        _aquariumManamger._finalPositionX = 0;
        _aquariumManamger._finalPositionY = 0;

        _mainCamera.transform.position = new Vector3(0, 0, -12);
        _aquariumManamger._run = true;

        // �Ŵ������� Dictinary<����� ��ȣ, ������> ������ �ش� �������� �����Ѵ�.
        //_aquariumMenuUI.SetActive(false); // UI �ٲٱ�
        _enterButton.gameObject.SetActive(false);
        _topBarObject.SetActive(false);
        _aquariumButtonsObject.SetActive(false);

        //_aquariumInsideUI.SetActive(true);
        _positionResetButtonObject.SetActive(true);
        _exitButton.SetActive(true);
        _fishSlotUIObject.SetActive(true);

        // �ش� ��ȣ�� ������ ����� ��Ȳ�� �����´�. (��� ����Ⱑ ��� �ִ���)
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);

        // ������ ����Ű�� ����� ����
        _aquariumManamger.EnterAquarium(_currentAquariumNumber, dic);

        // ������ ���� UI�� ����� ������ Ȱ��ȭ ��Ų��.
        FishSlotUpdate();
    }
    public void ClickExitButton() // ���������� ������ ��ư
    {
        PlayExitEffectAudio();
        _aquariumEffectManager.BGEffectSetActive(true);
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);
        // �ش� ������ ����� ���� ����(o), �ش� ������ ���� ����(o), InsideUI ����(o), MenuUI �ѱ�(o)
        _aquariumManamger.ExitAquarium(_currentAquariumNumber);
        _positionResetButtonObject.SetActive(false);
        _exitButton.SetActive(false);
        _fishSlotUIObject.SetActive(false);
        _enterButton.gameObject.SetActive(true);
        _topBarObject.SetActive(true);
        _aquariumButtonsObject.SetActive(true);
        _aquariumManamger._run = false;
    }
    public void ClickUpDownButton() // FishSlot On/Off ��ư
    {
        // ������ iTween�� ��������� Ȥ�� ������ �� ������ �׳� �ִϸ��̼��� ����.
        // �ִϸ��̼� ���ϱ� ���� �����ŷ��� �׳� iTween���� ����.
        if (_currentFishSlotState.Equals(eFishSlotType.On)) // FishSlot�� �ö�� ����
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
  
            _currentFishSlotState = eFishSlotType.Off;
            iTween.MoveTo(_fishSlotUIObject, iTween.Hash("y", _downPos.position.y, "time", 0.5f));
            _updownButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _updownButton.interactable = true));

            _updownButtonTransform.GetChild(0).gameObject.SetActive(true);
            _updownButtonTransform.GetChild(1).gameObject.SetActive(false);
        }
        else // ������ ����
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

            _currentFishSlotState = eFishSlotType.On;
            iTween.MoveTo(_fishSlotUIObject, iTween.Hash("y", _upPos.position.y, "time", 0.5f));
            _updownButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _updownButton.interactable = true));
            _updownButtonTransform.GetChild(0).gameObject.SetActive(false);
            _updownButtonTransform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void ClickAquariumFishSlot(List<PublicDefined.stFishInfo> list)
    {
        PlayClickEffectAudio();
        _fishInfoUI.OnFishInfoSlot(list, _currentAquariumNumber);
        _fishInfoUIObject.SetActive(true);
    }

    public void ClickPositionResetButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.aquariumRenewal).GetComponent<AudioPoolObject>().Init();
        // ī�޶� ȸ��, FOV �ʱ�ȭ
        _mainCamera.fieldOfView = 90;
        _aquariumManamger._finalFov = 90;

        _aquariumManamger._finalPositionX = 0;
        _aquariumManamger._finalPositionY = 0;

        _mainCamera.transform.position = new Vector3(0, 0, -12);

        // ����� ����
        _aquariumManamger.ResetFishPosition();
    }

    public void ClickBuyAquariumButton(int number)
    {
        PlayClickEffectAudio();

        switch (number)
        {
            case 1:
                _buyAquariumUI.Init(false, 80000, number, _bgSprites[number], _userData, this);
                break;
            case 2:
                _buyAquariumUI.Init(false, 150000, number, _bgSprites[number], _userData, this);
                break;
            case 3:
                _buyAquariumUI.Init(true, 7900, number, _bgSprites[number], _userData, this);
                break;
            case 4:
                _buyAquariumUI.Init(true, 14900, number, _bgSprites[number], _userData, this);
                break;
        }
    }

    void AquariumButtonsUpdate(int n) // ���� ��ư�� �����ϰ� 
    {
        for (int i = 0; i < _aquariumButtons.Length; i++)
        {
            if (i.Equals(n))
                _aquariumButtons[i].transform.GetComponent<Image>().color = Color.white;
            else
                _aquariumButtons[i].transform.GetComponent<Image>().color = _translucenceColor;
        }
    }
    public void CheckAquariumPossessState() // � �������� ������ �ִ��� üũ�Ѵ�.
    {
        _aquariumState = _userData.GetAquariumPossessState();

        for (int i = 0; i < _aquariumState.Count; i++)
        {
            if (_aquariumState[i])
            {
                // ���� ���� ����Ƹ����� ��ư�� �����ش�.
                _aquariumButtons[i].interactable = true;
                _aquariumButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                // �����ϱ� ��ư ����.
                _aquariumButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                // �ش� �������� ����� ����Ⱑ �ִ��� Ȯ���Ѵ�.
                CheckFishCount(i);
            }
            else
            {
                // ���� ������ �ʴٸ� ��ư�� �ݰ� ī��Ʈ �ؽ�Ʈ�� ���� �����ϱ� ��ư�� Ų��.
                _aquariumButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                _aquariumButtons[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    void CheckFishCount(int aquariumNumber) // ���������� ����⸦ ����� ������ �ִ��� üũ�Ѵ�.
    {
        Dictionary<int, List<PublicDefined.stFishInfo>> dic;

        switch(aquariumNumber)
        {
            case 0:
                dic = _userData.GetFirstAquariumDictionary();
                break;
            case 1:
                dic = _userData.GetSecondAquariumDictionary();
                break;
            case 2:
                dic = _userData.GetThirdAquariumDictionary();
                break;
            case 3:
                dic = _userData.GetFourthAquariumDictionary();
                break;
            case 4:
                dic = _userData.GetFifthAquariumDictionary();
                break;
            default:
                dic = new Dictionary<int, List<PublicDefined.stFishInfo>>();
                break;
        }



        if (dic.Count > 0)
        {
            int cnt = 0;

            foreach (List<PublicDefined.stFishInfo> value in dic.Values)
            {
                cnt += value.Count;
            }

            _fishCountTexts[aquariumNumber].text = cnt.ToString();
        }
        else
        {
            _fishCountTexts[aquariumNumber].text = 0.ToString();
        }
    }
    Dictionary<int, List<PublicDefined.stFishInfo>> GetFishCountInEachAquarium(int aquariumNumber) // �ش� �������� � ����Ⱑ ��� �ִ��� Ȯ���Ѵ�.
    {
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = new Dictionary<int, List<PublicDefined.stFishInfo>>();

        switch (aquariumNumber)
        {
            case 0:
                dic = _userData.GetFirstAquariumDictionary();
                break;
            case 1:
                dic = _userData.GetSecondAquariumDictionary();
                break;
            case 2:
                dic = _userData.GetThirdAquariumDictionary();
                break;
            case 3:
                dic = _userData.GetFourthAquariumDictionary();
                break;
            case 4:
                dic = _userData.GetFifthAquariumDictionary();
                break;
        }

        return dic;
    }
    void ChangeBG(int n) // ����� �ٲ۴�.
    {
        _BG.sprite = _bgSprites[n];
    }
    void EnterButtonsUpdate(int n) // EnterButton Ȱ��ȭ/��Ȱ��ȭ üũ
    {
        if (_aquariumState[n])
        {
            _enterButton.interactable = true;
            _enterButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        else
        {
            _enterButton.interactable = false;
            _enterButton.transform.GetChild(0).GetComponent<Image>().color = _translucenceColor;
        }
    }
    void InitFishSlot() // fishSlot �ʱ�ȭ
    {
        _fishSlot = new AquariumFishSlot[_slotParent.childCount];

        for (int i = 0; i < _slotParent.childCount; i++)
        {
            _fishSlot[i] = _slotParent.GetChild(i).GetComponent<AquariumFishSlot>();
        }
    }
    public void FishSlotUpdate()
    {
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = GetFishCountInEachAquarium(_currentAquariumNumber);

        int cnt = 0;
        //                    DB��ȣ            ����� ���� ����Ʈ
        foreach (List<PublicDefined.stFishInfo> data in dic.Values)
        {
            if (!data.Count.Equals(0))
            {
                //Debug.LogError(cnt + " , " + data.Count);
                _fishSlot[cnt++].InitFishSlot(this, data);
            }
        }

        for (int i = cnt; i < _fishSlot.Length; i++)
        {
            _fishSlot[i].OffSlot();
        }
    }
    
    public void UpdateMoney()
    {
        _goldText.text = _userData._gold.ToString("#,##0");
        _pearlText.text = _userData._pearl.ToString("#,##0");
    }
    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayBGM()
    {
        //Debug.Log(_currentAquariumNumber);
        switch(_currentAquariumNumber)
        {
            case 0:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.firstAquarium, true);
                break;
            case 1:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.secondAquarium, true);
                break;
            case 2:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.thirdAquarium, true);
                break;
            case 3:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.fourthAquarium, true);
                break;
            case 4:
                AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.fifthAquarium, true);
                break;
        }

    }
}
