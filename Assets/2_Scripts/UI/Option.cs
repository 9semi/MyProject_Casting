using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class Option : MonoBehaviour 
{
    readonly string _couponString = "Coupon";
    readonly string _ulseomCouponString = "ulseomlove";
    readonly string _gstarCouponString = "seizetheday";

    [Header("게임설정, 소리설정, 알림설정, 계정설정 버튼")]
    // 게임 설정(진동, 화면회전), 소리 설정, 알림 설정, 계정설점
    public GameObject[] settingWindows;

    // 설정 창 이미지
    [Header("설정창 이미지")]
    public Image[] tab;

    [Header("소리 관련")]
    public bool _isMute_BGM;
    public bool _isMute_EFFECT;
    // 배경음 조절 슬라이더
    public Slider bgmSlider;
    public Image _bgmImage;

    // 효과음 조절 슬라이더
    public Slider effectSlider;
    public Image _effectImage;

    public Sprite _onSprite;
    public Sprite _offSprite;

    //게임 설정 관련
    [Header("게임 설정 관련")]
    public GameObject _vibrationCheck;
    bool _isVibrationOn;
    public GameObject _landscapeCheck;
    bool _isLandscapeOn;
    public GameObject _noticeCheck;
    bool _isNoticeOn=true;
    public GameObject _mailCheck;
    bool _isMailOn=true;
    public GameObject _eventCheck;
    bool _isEventOn=true;


    [Header("계정 설정 관련")]
    public GameObject _couponObject;
    public InputField _couponInputField;
    public GameObject _couponResultUI;
    public Text _couponResultText;
    int _couponResultInt = 0;
    string _getGold;


    // 쿠폰이 있다면 받아온다.
    Dictionary<string, object> _couponDictionary;

    private void OnEnable()
    {
        ButtonSelcet(0);

        _isVibrationOn = DataManager.INSTANCE._vibration;
        _isLandscapeOn = DataManager.INSTANCE._landscape;

        _vibrationCheck.SetActive(_isVibrationOn);
        _landscapeCheck.SetActive(_isLandscapeOn);

        _isMute_BGM = AudioManager.INSTANCE._bgmMute;
        _isMute_EFFECT = AudioManager.INSTANCE._effectMute;
        bgmSlider.value = AudioManager.INSTANCE._bgmVolume * 10;
        effectSlider.value = AudioManager.INSTANCE._effectVolume * 10;

        //Debug.Log(AudioManager.INSTANCE._bgmVolume);
        //Debug.Log(AudioManager.INSTANCE._bgmVolume * 10);
        //Debug.Log(bgmSlider.value);

        if (_isMute_BGM)
            _bgmImage.sprite = _offSprite;
        else
            _bgmImage.sprite = _onSprite;

        if (_isMute_EFFECT)
            _effectImage.sprite = _offSprite;
        else
            _effectImage.sprite = _onSprite;

    }

    Dictionary<string, object> ObjectToDictionary2(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        return dic;
    }

    // 설정 창
    public void ButtonSelcet(int contentNum)
    {
        // 클릭 효과음 재생
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        int tabNum = 0;

        for(int i=0;i<settingWindows.Length;i++)
        {
            settingWindows[i].SetActive(false);
        }
        // 탭 숫자로 구분
        switch (contentNum)
        {
            // 게임 설정
            case 0:
                tabNum = 0;
                settingWindows[0].SetActive(true);
                break;
            // 소리 설정
            case 1:
                tabNum = 1;
                settingWindows[1].SetActive(true);
                break;
            // 알림 설정
            case 2:
                tabNum = 2;
                settingWindows[2].SetActive(true);
                break;
            // 계정 설정
            case 3:
                tabNum = 3;
                settingWindows[3].SetActive(true);
                break;
        }

        for (int i = 0; i < tab.Length; i++)
        {
            // 반복하여 선택 번호와 같다면 알파값 255, 아니면 알파값 절반
            tab[i].color = i == tabNum  ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.4f);
        }
    }
    public void ClickXButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        DBManager.INSTANCE.SaveXml(false);
    }

    // 게임 설정
    public void ClickButton(int number)
    {
        PlayClickEffectAudio();

        switch (number)
        {
            // 진동 클릭
            case 0:
                _isVibrationOn = !_isVibrationOn;

                if(_isVibrationOn)
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                }
                else
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
                }
                _vibrationCheck.SetActive(_isVibrationOn);
                DataManager.INSTANCE._vibration = _isVibrationOn;
                break;
            // 화면 회전 클릭
            case 1:
                _isLandscapeOn = !_isLandscapeOn;
                _landscapeCheck.SetActive(_isLandscapeOn);

                if (_isLandscapeOn)
                {
                    Screen.orientation = ScreenOrientation.LandscapeRight;
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                }
                else
                {
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
                }

                DataManager.INSTANCE._landscape = _isLandscapeOn;
                break;
        }
    }

    // 소리 설정
    public void ClickVolumImage(int num)
    {
        switch (num)
        {
            case 0:
                // 현재 음소거 모드라면
                if (_isMute_BGM)
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                    _isMute_BGM = false;
                    _bgmImage.sprite = _onSprite;
                }
                else
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
                    _isMute_BGM = true;
                    _bgmImage.sprite = _offSprite;
                }
                AudioManager.INSTANCE.BGMSetting((bgmSlider.value * 0.1f), _isMute_BGM);
                break;
            case 1:
                if (_isMute_EFFECT)
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
                    _isMute_EFFECT = false;
                    _effectImage.sprite = _onSprite;
                }
                else
                {
                    AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
                    _isMute_EFFECT = true;
                    _effectImage.sprite = _offSprite;
                }
                AudioManager.INSTANCE.EffectSetting((effectSlider.value * 0.1f), _isMute_EFFECT);
                break;
        }
    }
    public void ChangeVolum(int num)
    {
        switch (num)
        {
            case 0:
                AudioManager.INSTANCE.BGMSetting((bgmSlider.value * 0.1f), _isMute_BGM);
                break;
            case 1:
                AudioManager.INSTANCE.EffectSetting((effectSlider.value * 0.1f), _isMute_EFFECT);
                break;
        }
    }

    // 알림 설정
    public void ClickButton2(int num)
    {
        switch(num)
        {
            case 0:
                _isNoticeOn = !_isNoticeOn;
                _noticeCheck.SetActive(_isNoticeOn);

                if (_isNoticeOn)
                    PlayClickEffectAudio();
                else
                    PlayExitEffectAudio();
                break;
            case 1:
                _isMailOn = !_isMailOn;
                _mailCheck.SetActive(_isMailOn);

                if (_isMailOn)
                    PlayClickEffectAudio();
                else
                    PlayExitEffectAudio();
                break;
            case 2:
                _isEventOn = !_isEventOn;
                _eventCheck.SetActive(_isEventOn);

                if (_isEventOn)
                    PlayClickEffectAudio();
                else
                    PlayExitEffectAudio();
                break;
        }
    }

    // 계정 설정
    public void ClickCouponButton()
    {
        PlayClickEffectAudio();

        _couponObject.SetActive(true);
    }

    public void ClickCouponOKButton()
    {
        PlayClickEffectAudio();

        _couponDictionary = new Dictionary<string, object>();
        string text = _couponInputField.text;
        _couponResultInt = 0;
        _getGold = string.Empty;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference.Child(_couponString);

        reference.GetValueAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.Log("실패 이유: " + t.Result.ToString());
            }
            else if (t.IsCompleted)
            {
                DataSnapshot ss = t.Result;

                _couponDictionary = ObjectToDictionary2(ss.Value);

                //foreach(KeyValuePair<string, object> data in _couponDictionary)
                //{
                //    Debug.Log(data.Key + " , " + data.Value);
                //}

                bool isExist = _couponDictionary.ContainsKey(text);

                // 입력한 텍스트가 딕셔너리 키에 있다면 해당 키의 값을 준다. (골드)
                if (isExist)
                {
                    // 보상 지급
                    UserData userData = DBManager.INSTANCE.GetUserData();

                    _getGold = _couponDictionary[text].ToString();

                    // 유엘섬러브 쿠폰이라면
                    if (text.Equals(_ulseomCouponString))
                    {
                        // 이미 받았다.
                        if (userData._isGetUlseomCoupon)
                        {
                            _couponResultInt = 3;
                        }
                        // 최초로 받는다.
                        else
                        {
                            userData._gold += int.Parse(_couponDictionary[_ulseomCouponString].ToString());
                            userData._isGetUlseomCoupon = true;

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("/_gold/", userData._gold);
                            dic.Add("_isGetUlseomCoupon", true);

                            DBManager.INSTANCE.UpdateFirebase(dic);
                            _couponResultInt = 1;
                        }
                        
                    }
                    // 지스타 쿠폰이라면
                    else if(text.Equals(_gstarCouponString))
                    {
                        // 이미 받았다.
                        if (userData._isGetGStarCoupon)
                        {
                            _couponResultInt = 3;
                        }
                        // 최초로 받는다.
                        else
                        {
                            userData._gold += int.Parse(_couponDictionary[_gstarCouponString].ToString());
                            userData._isGetGStarCoupon = true;

                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("/_gold/", userData._gold);
                            dic.Add("_isGetGStarCoupon", true);

                            DBManager.INSTANCE.UpdateFirebase(dic);
                            _couponResultInt = 1;
                        }
                    }
                    // 해당 쿠폰의 보상을 지급하고 파이어베이스에서 해당 쿠폰을 지운다.
                    else
                    {
                        userData._gold += int.Parse(_couponDictionary[text].ToString());

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("/_gold/", userData._gold);
                        DBManager.INSTANCE.UpdateFirebase(dic);

                        reference.Child(text).RemoveValueAsync();
                        _couponResultInt = 1;
                    }
                }
                // 해당 쿠폰이 없다.
                else
                {
                    _couponResultInt = 2;
                }
            }
        });

        StartCoroutine(CouponCoroutine());
    }
    IEnumerator CouponCoroutine()
    {
        yield return new WaitUntil( () =>_couponResultInt > 0);

        // 골드 획득한다.
        if(_couponResultInt.Equals(1))
        {
            _couponObject.SetActive(false);
            _couponResultUI.SetActive(true);
            _couponResultText.text = _getGold + "골드를 획득합니다.";
        }
        else if(_couponResultInt.Equals(2))
        {
            _couponObject.SetActive(false);
            _couponResultUI.SetActive(true);
            _couponResultText.text = "잘못된 쿠폰 번호입니다.";
        }
        else if(_couponResultInt.Equals(3))
        {
            _couponObject.SetActive(false);
            _couponResultUI.SetActive(true);
            _couponResultText.text = "이미 보상을 받았습니다.";
        }
        _couponInputField.text = string.Empty;
        _couponResultInt = 0;
        _getGold = string.Empty;
        yield return null;
    }

    public void ClickCouponXButton()
    {
        PlayExitEffectAudio();
        _couponInputField.text = string.Empty;
    }

    public void ClickOfficialCafeButton()
    {
        PlayClickEffectAudio();
        Application.OpenURL("https://cafe.naver.com/casting0406");
    }
    public void ClickLogoutButton()
    {
        PlayClickEffectAudio();
        DataManager.INSTANCE._isTryLogout = true;
        LoadingSceneManager.LoadScene("LoginScene");
    }
    public void ClickYoutubeButton()
    {
        PlayClickEffectAudio();
        Application.OpenURL("https://www.youtube.com/channel/UCw5rgqFrYcZWhqCOijKM5zw");
    }

    public void ClickDefaultSetting()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.equipRemove).GetComponent<AudioPoolObject>().Init();
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