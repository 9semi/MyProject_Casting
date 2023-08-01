using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    readonly string _platinumOverString = "사용하시던 [월간 골드 플레티넘]의 기간이 만료되었습니다.";
    readonly string _diamondOverString = "사용하시던 [월간 골드 다이아몬드]의 기간이 만료되었습니다.";

    [SerializeField] GameObject _mailCountObject;
    [SerializeField] Text _mailCountText;
    [SerializeField] GameObject _mailUI;
    [SerializeField] LobbyTutorial _tutorialUI;

    [Header("Info UI")]
    [SerializeField] Text _nameText;
    [SerializeField] Text _gradeText;
    [SerializeField] GameObject[] _starObject;
    [SerializeField] Image[] _1_3_starImages;
    [SerializeField] Image[] _4_6_starImages;
    [SerializeField] Image[] _7_9_starImages;

    [Header("MyInfo UI")]
    [SerializeField] MyInfoUI _myinfoUI;
    [SerializeField] Text _nameText2;
    [SerializeField] Text _gradeText2;
    [SerializeField] GameObject[] _starObject2;
    [SerializeField] Image[] _1_3_starImages2;
    [SerializeField] Image[] _4_6_starImages2;
    [SerializeField] Image[] _7_9_starImages2;

    [Header("OST")]
    [SerializeField] Sprite _playSprite;
    [SerializeField] Sprite _pauseSprite;
    [SerializeField] Image _ostStateImage;
    [SerializeField] bool _ostOn = false;

    [SerializeField] Lobby_GetPackageRewardUI _lobby_getPackageRewardUI;
    [SerializeField] PackageOverUI _packageOverUI;

    UserData _userData;

    private void Start()
    {
        _userData = DBManager.INSTANCE.GetUserData();

        // 혹시 실행되고 있는 이펙트 소리가 있다면 찾아서 끈다.
        AudioManager.INSTANCE.StopAllEffect();

        if (!AudioManager.INSTANCE.CheckIsSameBGM(PublicDefined.eBGMType.lobbyscene))
            AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);

        // tutorial을 하고 있다면 정보를 갱신할 필요 X
        //if (!DataManager.INSTANCE._tutorialIsDone)
        //{
        //    _tutorialUI.gameObject.SetActive(true);
        //    _mailCountText.text = "1";
        //    _mailCountObject.SetActive(true);
        //}
        //else
        //{
            _tutorialUI.gameObject.SetActive(false);
            InitMyInfo();
            _myinfoUI.Init(_userData);

            if(DataManager.INSTANCE._isFirstLogin)
                CheckPackage();
        //}
    }

    public void ClickButton(int num)
    {
        switch (num)
        {
            case 0: // 수족관
                PlayButtonClickAudio();
                SceneManager.LoadScene("AquariumScene");
                break;
            case 1: // 상점
                PlayButtonClickAudio();
                break;
            case 2: // 장비창
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                break;
            case 3: // 게임시작
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                SceneManager.LoadScene("MapSelectScene");
                break;
            case 4: // 편지
                PlayButtonClickAudio();
                _mailCountObject.SetActive(false);
                _mailUI.SetActive(true);
                break;
            case 5: // 블루투스
                PlayButtonClickAudio();
                break;
            case 6: // 설정
                //PlayButtonClickAudio();
                break;
        }
    }

    void InitMyInfo()
    {
        _nameText.text = _userData._nickname;
        _nameText2.text = _userData._nickname;
        _gradeText.text = _userData._grade.ToString();
        _gradeText2.text = _userData._grade.ToString();
        CheckStar();
    }

    void CheckStar()
    {
        float s = _userData._star;
        float g = _userData._grade;

        // 별이 하나도 없으면 그냥 종료
        if (s <= 0)
        {
            if (g < 4)
            {
                _starObject[0].SetActive(true);
                _starObject[1].SetActive(false);
                _starObject[2].SetActive(false);
                _starObject2[0].SetActive(true);
                _starObject2[1].SetActive(false);
                _starObject2[2].SetActive(false);
            }
            else if (g < 7)
            {
                _starObject[0].SetActive(false);
                _starObject[1].SetActive(true);
                _starObject[2].SetActive(false);
                _starObject2[0].SetActive(false);
                _starObject2[1].SetActive(true);
                _starObject2[2].SetActive(false);
            }
            else
            {
                _starObject[0].SetActive(false);
                _starObject[1].SetActive(false);
                _starObject[2].SetActive(true);
                _starObject2[0].SetActive(false);
                _starObject2[1].SetActive(false);
                _starObject2[2].SetActive(true);
            }

            return;
        }

        float value = s / 1;
        float rest = s % 1;

        // 1 ~ 3 
        if (g < 4)
        {
            _starObject[0].SetActive(true);
            _starObject[1].SetActive(false);
            _starObject[2].SetActive(false);
            _starObject2[0].SetActive(true);
            _starObject2[1].SetActive(false);
            _starObject2[2].SetActive(false);

            for (int i = 0; i < _1_3_starImages.Length; i++)
            {
                // 꽉 찬 별 갯수
                if (i >= value)
                {
                    _1_3_starImages[i].fillAmount = 0;
                    _1_3_starImages2[i].fillAmount = 0;
                }
                else
                {
                    _1_3_starImages[i].fillAmount = 1;
                    _1_3_starImages2[i].fillAmount = 1;
                }
            }

            if (rest > 0)
            {
                _1_3_starImages[(int)value].fillAmount = 0.5f;
                _1_3_starImages2[(int)value].fillAmount = 0.5f;
            }

        }
        // 4 ~ 6
        else if (g < 7)
        {
            _starObject[0].SetActive(false);
            _starObject[1].SetActive(true);
            _starObject[2].SetActive(false);
            _starObject2[0].SetActive(false);
            _starObject2[1].SetActive(true);
            _starObject2[2].SetActive(false);

            for (int i = 0; i < _4_6_starImages.Length; i++)
            {
                // 꽉 찬 별 갯수
                if (i >= value)
                {
                    _4_6_starImages[i].fillAmount = 0;
                    _4_6_starImages2[i].fillAmount = 0;
                }
                else
                {
                    _4_6_starImages[i].fillAmount = 1;
                    _4_6_starImages2[i].fillAmount = 1;
                }
            }

            if (rest > 0)
            {
                _4_6_starImages[(int)value].fillAmount = 0.5f;
                _4_6_starImages2[(int)value].fillAmount = 0.5f;
            }
        }
        else
        {
            _starObject[0].SetActive(false);
            _starObject[1].SetActive(false);
            _starObject[2].SetActive(true);
            _starObject2[0].SetActive(false);
            _starObject2[1].SetActive(false);
            _starObject2[2].SetActive(true);

            for (int i = 0; i < _7_9_starImages.Length; i++)
            {
                // 꽉 찬 별 갯수
                if (i >= value)
                {
                    _7_9_starImages[i].fillAmount = 0;
                    _7_9_starImages2[i].fillAmount = 0;
                }
                else
                {
                    _7_9_starImages[i].fillAmount = 1;
                    _7_9_starImages2[i].fillAmount = 1;
                }
            }

            if (rest > 0)
            {
                _7_9_starImages[(int)value].fillAmount = 0.5f;
                _7_9_starImages2[(int)value].fillAmount = 0.5f;
            }
        }
    }

    public void CheckPackage()
    {
        bool _isGetPlatinumReward = false;
        bool _isGetDiamondReward = false;

        List<string> notices = new List<string>();
        DataManager.INSTANCE._isFirstLogin = false;

        //Debug.Log("플레티넘: " + _userData._havePlatinumPackage + " , " + "다아이몬드: " + _userData._haveDiamondPackage);

        if (_userData._havePlatinumPackage || _userData._haveDiamondPackage)
        {
            if(_userData._havePlatinumPackage)
            {
                // 오늘 날짜가 딕셔너리 안에 없으면 이미 지났다.
                DateTime today = DateTime.Today;
                //DateTime today = DateTime.Today.AddDays(10);
                string todayStr = today.ToString("yyyyMMdd");
                Dictionary<string, bool> platinumDic = _userData.GetPlatinumPackage();

                if (platinumDic.ContainsKey(todayStr))
                {
                    if (platinumDic[todayStr])
                    {
                        // 오늘 이미 받았다.
                        _isGetPlatinumReward = true;
                    }
                    //else
                    //{
                    //    platinumDic[todayStr] = true;
                    //    _userData._gold += 3000;

                    //    // 파이어베이스 갱신
                    //    DBManager.INSTANCE.UpdatePackage(0, todayStr);
                    //}
                }
                else
                {
                    // 기간 만료
                    // 파어이베이스 삭제
                    notices.Add(_platinumOverString);
                    _userData._havePlatinumPackage = false;
                    //platinumDic = new Dictionary<string, bool>();
                    DBManager.INSTANCE.DeletePackage(0);
                }
            }

            if (_userData._haveDiamondPackage)
            {
                // 오늘 날짜가 딕셔너리 안에 없으면 이미 지났다.
                DateTime today = DateTime.Today;
                //DateTime today = DateTime.Today.AddDays(10);
                string todayStr = today.ToString("yyyyMMdd");
                //Debug.Log(todayStr);
                Dictionary<string, bool> diamondDic = _userData.GetDiamondPackage();

                if (diamondDic.ContainsKey(todayStr))
                {
                    if (diamondDic[todayStr])
                    {
                        // 오늘 이미 받았다.
                        //Debug.Log("이미 받았다. 다이아몬드");
                        _isGetDiamondReward = true;
                    }
                    //else
                    //{
                    //    diamondDic[todayStr] = true;
                    //    _userData._gold += 5000;

                    //    DBManager.INSTANCE.UpdatePackage(1, todayStr);
                    //}
                }
                else
                {
                    // 기간 만료
                    // 파어이베이스 삭제
                    notices.Add(_diamondOverString);
                    //diamondDic = new Dictionary<string, bool>();
                    _userData._haveDiamondPackage = false;
                    DBManager.INSTANCE.DeletePackage(1);
                }
            }
        }
        if (notices.Count > 0)
            _packageOverUI.Init(notices);

        _lobby_getPackageRewardUI.Init(_userData, _userData._havePlatinumPackage, _userData._haveDiamondPackage, _isGetPlatinumReward, _isGetDiamondReward);
    }

    public void ClickOSTButton()
    {
        PlayButtonClickAudio();

        _ostOn = !_ostOn;

        if(_ostOn)
        {
            if (AudioManager.INSTANCE.BgmVolume <= 0)
            {
                AudioManager.INSTANCE.BGMSetting(0.5f, false);
                DBManager.INSTANCE.SaveXml(false);
            }

            if (AudioManager.INSTANCE.BgmMute)
            {
                AudioManager.INSTANCE.BGMSetting(AudioManager.INSTANCE.BgmVolume, false);
                DBManager.INSTANCE.SaveXml(false);
            }


            _ostStateImage.sprite = _pauseSprite;
            AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.ost, true);
        }
        else
        {
            _ostStateImage.sprite = _playSprite;
            AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.lobbyscene, true);
        }
    }

    public void PlayButtonClickAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
}