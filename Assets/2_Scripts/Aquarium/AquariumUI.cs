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
    
    [Header("수족관 메뉴 UI")]
    [SerializeField] Text _goldText; public void SetGoldText() { _goldText.text = _userData._gold.ToString(); }
    [SerializeField] Text _pearlText;
    [SerializeField] Image _BG;
    [SerializeField] Button[] _aquariumButtons;
    [SerializeField] Text[] _fishCountTexts;
    [SerializeField] Sprite[] _bgSprites; public Sprite GetBackgroundSprite(int aquariumNumber) { return _bgSprites[aquariumNumber]; }
    [SerializeField] Button _enterButton;
    [SerializeField] GameObject _topBarObject;
    [SerializeField] GameObject _aquariumButtonsObject;

    [Header("수족관 안쪽 UI")]
    [SerializeField] Transform _slotParent;
    [SerializeField] GameObject _fishSlotUIObject;
    [SerializeField] GameObject _exitButton;
    [SerializeField] Button _updownButton;
    [SerializeField] Transform _updownButtonTransform;
    [SerializeField] Transform _downPos;
    [SerializeField] Transform _upPos;
    [SerializeField] GameObject _fishInfoUIObject;
    [SerializeField] GameObject _positionResetButtonObject;

    [Header("수족관 구매 UI")]
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
        _aquariumState = _userData.GetAquariumPossessState(); // 수족관 사면 bool형으로 만들어진 이 리스트를 업데이트 해줘야 한다.
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
    public void ClickAquariumButton(int n) // 수족관 선택 버튼
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.profileArrow).GetComponent<AudioPoolObject>().Init();
        // 똑같은 버튼을 눌렀다면 선택을 해제하고 원래의 배경으로 돌려놓는다. EnterButton도 막아둔다.
        if (_currentAquariumNumber.Equals(n))
        {
            _currentAquariumNumber = -1;
            _aquariumButtons[n].transform.GetComponent<Image>().color = _translucenceColor;
            _enterButton.interactable = false;
            _BG.sprite = _originBG;
        }
        // 다른 버튼을 눌렀다면 배경은 바꿔주되 해당 수족관이 있는지 확인하고 있으면 EnterButton을 열어준다.
        else
        {
            _currentAquariumNumber = n;
            EnterButtonsUpdate(n);
            AquariumButtonsUpdate(n);
            ChangeBG(n);
        }
    }
    public void ClickEnterButton() // 수족관으로 들어가는 버튼
    {
        // 위에서 이미 막겠지만 혹시 모르니까 한 번 더 막는다. 현재 버튼의 수족관을 보유하고 있지 않다면 return
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

        // 카메라 회전, FOV 초기화
        _mainCamera.fieldOfView = 90;
        _aquariumManamger._finalFov = 90;

        _aquariumManamger._finalPositionX = 0;
        _aquariumManamger._finalPositionY = 0;

        _mainCamera.transform.position = new Vector3(0, 0, -12);
        _aquariumManamger._run = true;

        // 매니저한테 Dictinary<물고기 번호, 마릿수> 보내서 해당 물고기들을 생성한다.
        //_aquariumMenuUI.SetActive(false); // UI 바꾸기
        _enterButton.gameObject.SetActive(false);
        _topBarObject.SetActive(false);
        _aquariumButtonsObject.SetActive(false);

        //_aquariumInsideUI.SetActive(true);
        _positionResetButtonObject.SetActive(true);
        _exitButton.SetActive(true);
        _fishSlotUIObject.SetActive(true);

        // 해당 번호의 수족관 물고기 현황을 가져온다. (몇번 물고기가 몇마리 있는지)
        Dictionary<int, List<PublicDefined.stFishInfo>> dic = _userData.SelectAquariumDictionary(_currentAquariumNumber);

        // 수족관 데코키고 물고기 생성
        _aquariumManamger.EnterAquarium(_currentAquariumNumber, dic);

        // 수족관 안쪽 UI에 물고기 슬롯을 활성화 시킨다.
        FishSlotUpdate();
    }
    public void ClickExitButton() // 수족관에서 나오는 버튼
    {
        PlayExitEffectAudio();
        _aquariumEffectManager.BGEffectSetActive(true);
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);
        // 해당 수족관 물고기 전부 끄기(o), 해당 수족관 데코 끄기(o), InsideUI 끄기(o), MenuUI 켜기(o)
        _aquariumManamger.ExitAquarium(_currentAquariumNumber);
        _positionResetButtonObject.SetActive(false);
        _exitButton.SetActive(false);
        _fishSlotUIObject.SetActive(false);
        _enterButton.gameObject.SetActive(true);
        _topBarObject.SetActive(true);
        _aquariumButtonsObject.SetActive(true);
        _aquariumManamger._run = false;
    }
    public void ClickUpDownButton() // FishSlot On/Off 버튼
    {
        // 원래는 iTween을 사용했으나 혹시 에러날 수 있으니 그냥 애니메이션을 쓰자.
        // 애니메이션 쓰니까 가끔 버벅거려서 그냥 iTween으로 쓴다.
        if (_currentFishSlotState.Equals(eFishSlotType.On)) // FishSlot이 올라온 상태
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
  
            _currentFishSlotState = eFishSlotType.Off;
            iTween.MoveTo(_fishSlotUIObject, iTween.Hash("y", _downPos.position.y, "time", 0.5f));
            _updownButton.interactable = false;
            StartCoroutine(MakeDelay(1, () => _updownButton.interactable = true));

            _updownButtonTransform.GetChild(0).gameObject.SetActive(true);
            _updownButtonTransform.GetChild(1).gameObject.SetActive(false);
        }
        else // 내려간 상태
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
        // 카메라 회전, FOV 초기화
        _mainCamera.fieldOfView = 90;
        _aquariumManamger._finalFov = 90;

        _aquariumManamger._finalPositionX = 0;
        _aquariumManamger._finalPositionY = 0;

        _mainCamera.transform.position = new Vector3(0, 0, -12);

        // 물고기 리셋
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

    void AquariumButtonsUpdate(int n) // 누른 버튼만 선명하게 
    {
        for (int i = 0; i < _aquariumButtons.Length; i++)
        {
            if (i.Equals(n))
                _aquariumButtons[i].transform.GetComponent<Image>().color = Color.white;
            else
                _aquariumButtons[i].transform.GetComponent<Image>().color = _translucenceColor;
        }
    }
    public void CheckAquariumPossessState() // 어떤 수족관을 가지고 있는지 체크한다.
    {
        _aquariumState = _userData.GetAquariumPossessState();

        for (int i = 0; i < _aquariumState.Count; i++)
        {
            if (_aquariumState[i])
            {
                // 보유 중인 아쿠아리움의 버튼은 열어준다.
                _aquariumButtons[i].interactable = true;
                _aquariumButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                // 구매하기 버튼 끈다.
                _aquariumButtons[i].transform.GetChild(1).gameObject.SetActive(false);
                // 해당 수족관에 몇마리의 물고기가 있는지 확인한다.
                CheckFishCount(i);
            }
            else
            {
                // 보유 중이지 않다면 버튼을 닫고 카운트 텍스트는 끄고 구매하기 버튼을 킨다.
                _aquariumButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                _aquariumButtons[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    void CheckFishCount(int aquariumNumber) // 수족관마다 물고기를 몇마리씩 가지고 있는지 체크한다.
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
    Dictionary<int, List<PublicDefined.stFishInfo>> GetFishCountInEachAquarium(int aquariumNumber) // 해당 수족관에 어떤 물고기가 몇마리 있는지 확인한다.
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
    void ChangeBG(int n) // 배경을 바꾼다.
    {
        _BG.sprite = _bgSprites[n];
    }
    void EnterButtonsUpdate(int n) // EnterButton 활성화/비활성화 체크
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
    void InitFishSlot() // fishSlot 초기화
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
        //                    DB번호            물고기 정보 리스트
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
