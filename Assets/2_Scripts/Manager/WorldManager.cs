using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    readonly string _jeongdongjin = "EastSea_Jeongdongjin";
    readonly string _skyway = "SkyWayFishingPier";
    readonly string _homerspit = "Homerspit";
    readonly string _lobby = "LobbyScene";

    public MapSelectScene_TimeUI _timeUI;
    public MapSelectScene_RankUI[] _rankUI;
    public GameObject _tutorialUI;
    
    public GameObject nullPopup;

    public BookListUI _booklistUI;

    public MatchUI _matchUI;

    public RectTransform _mapContent;

    public Image _bookArrowImage;
    public Sprite _upArrowSprite;
    public Sprite _downArrowSprite;

    [Header("tutorial")]
    int _currentProgressState;
    [Header("Step9")]
    public GameObject _step9Object;
    [Header("Step10")]
    public GameObject _step10Object;

    public GameObject _step0Object;
    public GameObject _step1Object;

    [Header("tutorial 스킵 기능")]
    public GameObject _skipButton;
    public GameObject _skipUIObject;

    int _currentMapNumber = -1;

    [Header("물고기 마릿수에 따른 맵 오픈/클로즈")]
    public GameObject _skywayLockUI;
    public GameObject _homerspitLockUI;
    public Button _skywayMapButton;
    public Button _homerspitMapButton;
    public GameObject _skywayLockImage;
    public GameObject _homerspitLockImage;
    bool _isSkywayLock;
    bool _isHomerspitLock;
    bool _isTutorial;
    bool _isBookOn;

    private void Start()
    {
        if (!AudioManager.INSTANCE.CheckIsSameBGM(PublicDefined.eBGMType.lobbyscene))
            AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);

        if(DataManager.INSTANCE._matchGameIsInProgress)
        {
            _isTutorial = DataManager.INSTANCE._matchGameIsInProgress;
            StartCoroutine(MatchRestart());
        }

        DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;

        CheckFishQuantity();
        CheckLockMap();

        GameObject go = GameObject.FindGameObjectWithTag("Bluetooth");

        if(go != null)
        {
            BLETotal bleTotal = go.transform.GetComponent<BLETotal>();
            bleTotal._isInGame_Main = false;
            bleTotal._isInGame_Reel = false;
            bleTotal.MotorResetCheck();
        }

        if (DataManager.INSTANCE._tutorialIsInProgress)
        {
            _currentProgressState = 9;
            StartCoroutine(TutorialMain());
            _skipButton.SetActive(true);
        }
        else if (!DataManager.INSTANCE._tutorialIsDone)
        {
            _currentProgressState = 0;
            StartCoroutine(TutorialFirst());
            _skipButton.SetActive(true);
        }
    }
    private void Update()
    {
        if (Application.platform.Equals(RuntimePlatform.Android))
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_isTutorial && !_matchUI.gameObject.activeSelf)
            {
                //LoadingSceneManager.LoadScene("LobbyScene");
                SceneManager.LoadScene("LobbyScene");
            }
        }
    }

    IEnumerator MatchRestart()
    {
        _matchUI.gameObject.SetActive(true);
        yield return new WaitUntil(() => _matchUI.gameObject.activeSelf);
        _matchUI.ClickMatch();
        yield return null;
    }

    void CheckFishQuantity()
    {
        // jeongdongjin에서 잡은 물고기가 10마리 이상이면
        if (_rankUI[0].Init(0) > 9)
        {
            // 호머스핏 체크
            if (_rankUI[2].Init(2) > 9)
            {
                _rankUI[1].Init(1);
            }
            else
            {
                _isSkywayLock = true;
            }
        }
        else
        {
            _isHomerspitLock = true;
            _isSkywayLock = true;
        }
    }

    void CheckLockMap()
    {
        if(_isSkywayLock)
        {
            _skywayLockImage.SetActive(true);
            _skywayMapButton.interactable = false;
        }

        if(_isHomerspitLock)
        {
            _homerspitLockImage.SetActive(true);
            _homerspitMapButton.interactable = false;
        }
    }

    public void ClickLockButton(int mapNumber)
    {
        PlayClickEffectAudio();

        if (mapNumber.Equals(1))
        {
            _skywayLockUI.SetActive(true);
        }
        else
        {
            _homerspitLockUI.SetActive(true);
        }
    }

    IEnumerator TutorialFirst()
    {
        Step0();
        yield return new WaitUntil(() => _currentProgressState > 0);
        Step1();
    }
    #region S0
    void Step0()
    {
        _step0Object.SetActive(true);
    }
    public void ClickTutorialButton0()
    {
        _step0Object.SetActive(false);
        _currentProgressState = 1;
    }
    #endregion
    #region S1
    void Step1()
    {
        _step1Object.SetActive(true);
    }
    public void ClickOKButtn1()
    {
        _step1Object.SetActive(false);
    }
    #endregion
    IEnumerator TutorialMain()
    {
        DataManager.INSTANCE._worldTime = 12;
        Step9();
        yield return new WaitUntil(() => _currentProgressState > 9);
        Step10();
        yield return null;
    }
    #region S9
    void Step9()        
    {
        _step9Object.SetActive(true);
    }
    public void ClickJeongdongjinButton()
    {
        PlayClickEffectAudio();
        _step9Object.SetActive(false);
        _currentProgressState = 10;
    }
    #endregion

    #region S10
    void Step10()
    {
        _step10Object.SetActive(true);
    }
    #endregion

    public void ClickMap(int mapNumber)
    {
        PlayClickEffectAudio();
        _currentMapNumber = mapNumber;
        _timeUI.Init(_currentMapNumber);
    }
    public void ClickRecord()
    {
        PlayClickEffectAudio();

        _rankUI[_currentMapNumber].gameObject.SetActive(true);
    }
    public void MoveMap()
    {
        switch(_currentMapNumber)
        {
            case 0:
                if (DataManager.INSTANCE._tutorialIsInProgress)
                {
                    DataManager.INSTANCE._mapType = PublicDefined.eMapType.tutorial;
                    LoadingSceneManager.LoadScene("Tutorial_Jeongdongjin");
                }
                else
                {
                    DataManager.INSTANCE._mapType = PublicDefined.eMapType.jeongdongjin;
                    LoadingSceneManager.LoadScene(_jeongdongjin);
                }
                break;
            case 1:
                DataManager.INSTANCE._mapType = PublicDefined.eMapType.skyway;
                LoadingSceneManager.LoadScene(_skyway);
                break;
            case 2:
                DataManager.INSTANCE._mapType = PublicDefined.eMapType.homerspit;
                LoadingSceneManager.LoadScene(_homerspit);
                break;
        }
    }
    public void ClickXButton()
    {
        PlayExitEffectAudio();

        DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;
        //AudioManager.INSTANCE.SaveBGMPlayerCurrentTime();
        SceneManager.LoadScene(_lobby);
    }

    public void ClickTutorialButton()
    {
        PlayClickEffectAudio();
        _tutorialUI.SetActive(true);
    }
    public void ClickTutorialOKButton()
    {
        PlayClickEffectAudio();
        DataManager.INSTANCE._mapType = PublicDefined.eMapType.tutorial;
        LoadingSceneManager.LoadScene("Tutorial_Jeongdongjin");
    }

    public void NullDevelopment(bool isOn)
    {
        PlayClickEffectAudio();
        nullPopup.SetActive(isOn);
    }
    public void ClickSkipButton()
    {
        PlayClickEffectAudio();
        Time.timeScale = 0;
        _skipUIObject.SetActive(true);
    }
    public void ClickSkipOKButton()
    {
        Time.timeScale = 1;
        DataManager.INSTANCE._tutorialIsInProgress = false;

        if (!DataManager.INSTANCE._tutorialIsDone)
        {
            DataManager.INSTANCE._tutorialIsDone = true;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("/_isTutorialDone/", true);
            DBManager.INSTANCE.UpdateFirebase(dic);
        }

        LoadingSceneManager.LoadScene("MapSelectScene");
    }
    public void ClickSkipCancelButton()
    {
        Time.timeScale = 1;
        PlayExitEffectAudio();
        _skipUIObject.SetActive(false);
    }
    public void ClickBookButton()
    {
        _isBookOn = true;
        _bookArrowImage.sprite = _upArrowSprite;
        _booklistUI.gameObject.SetActive(true);
    }
    public void CloseBookList()
    {
        if (_booklistUI._jeongdongjinBookObject.activeSelf || _booklistUI._homerspitBookObject.activeSelf ||
            _booklistUI._skywayBookObject.activeSelf)
            return;

        PlayClickEffectAudio();
        _isBookOn = false;
        _bookArrowImage.sprite = _downArrowSprite;
        _booklistUI.gameObject.SetActive(false);
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
