using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{    
    // 물고기 등급에 따른 이름 색상
    readonly Color _sundryColor = new Color(0, 0.45f, 0.6f);
    readonly Color _normalColor = new Color(0, 0.6f, 0.15f);
    readonly Color _rareColor = new Color(0.4f, 0, 0.5f);

    [Header("jeongdongjin")]
    public Outline[] _jeongdongjinAreaOutlines; // 순서대로 테트라포드, 급심지대, 사니질지대, 해초지대, 암초지대, 물골
    public Text[] _jeongdongjinTexts;
    public BookAndSeasonPass _jeongdongjinBook;

    public Fish[] _xpxmfkvhemFish;
    public Fish[] _rmqtlawleoFish;
    public Fish[] _tkslwlfwleoFish;
    public Fish[] _gochwleoFish;
    public Fish[] _dkachwleoFish;
    public Fish[] _anfrhfwleoFish;

    [Header("skyway")]
    public Outline[] _skywayAreaOutlines;
    public Text[] _skywayTexts;
    public BookAndSeasonPass _skywayBook;

    public FishSkyway[] _xpxmfkvhemFishSkyway;
    public FishSkyway[] _rmqtlawleoFishSkyway;
    public FishSkyway[] _tkslwlfwleoFishSkyway;
    public FishSkyway[] _gochwleoFishSkyway;
    public FishSkyway[] _dkachwleoFishSkyway;
    public FishSkyway[] _anfrhfwleoFishSkyway;

    [Header("homerspit")]
    public Outline[] _homerspitAreaOutlines;
    public Text[] _homerspitTexts;
    public BookAndSeasonPass _homerspitBook;

    public FishHomerspit[] _dkachwleoFishHomerspit;
    public FishHomerspit[] _gochwleoFishHomerspit;
    public FishHomerspit[] _tkslwlfwleoFishHomerspit;
    public FishHomerspit[] _rmqtlawleoFishHomerspit;
    public FishHomerspit[] _anfrhfFishHomerspit;
    public FishHomerspit[] _qkRkxWhranfrhfFishHomerspit;

    public GameObject _parent;
    public int _currentNumber = -1;
    public int _currentFilterNumber = -1;
    public bool _isFiltering = false;

    private void OnEnable()
    {
        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                if (_isFiltering)
                {
                    for (int i = 0; i < _jeongdongjinAreaOutlines.Length; i++)
                    {
                        _jeongdongjinAreaOutlines[i].effectColor = Color.black;
                        _jeongdongjinTexts[i].color = Color.black;
                    }
                    _currentNumber = _currentFilterNumber;
                    _jeongdongjinAreaOutlines[_currentFilterNumber].effectColor = Color.red;
                    _jeongdongjinTexts[_currentFilterNumber].color = Color.red;
                }
                else
                {
                    for (int i = 0; i < _jeongdongjinAreaOutlines.Length; i++)
                    {
                        _jeongdongjinAreaOutlines[i].effectColor = Color.black;
                        _jeongdongjinTexts[i].color = Color.black;
                    }
                }
                break;
            case PublicDefined.eMapType.skyway:
                if (_isFiltering)
                {
                    for (int i = 0; i < _skywayAreaOutlines.Length; i++)
                    {
                        _skywayAreaOutlines[i].effectColor = Color.black;
                        _skywayTexts[i].color = Color.black;
                    }
                    _currentNumber = _currentFilterNumber;
                    _skywayAreaOutlines[_currentFilterNumber].effectColor = Color.red;
                    _skywayTexts[_currentFilterNumber].color = Color.red;
                }
                else
                {
                    for (int i = 0; i < _skywayAreaOutlines.Length; i++)
                    {
                        _skywayAreaOutlines[i].effectColor = Color.black;
                        _skywayTexts[i].color = Color.black;
                    }
                }
                break;
            case PublicDefined.eMapType.homerspit:
                if (_isFiltering)
                {
                    for (int i = 0; i < _homerspitAreaOutlines.Length; i++)
                    {
                        _homerspitAreaOutlines[i].effectColor = Color.black;
                        _homerspitTexts[i].color = Color.black;
                    }

                    if (_currentFilterNumber.Equals(3) || _currentFilterNumber.Equals(5))
                    {
                        _currentNumber = _currentFilterNumber;
                        _homerspitAreaOutlines[3].effectColor = Color.red;
                        _homerspitTexts[3].color = Color.red;
                        _homerspitAreaOutlines[5].effectColor = Color.red;
                        _homerspitTexts[5].color = Color.red;
                    }
                    else
                    {
                        _currentNumber = _currentFilterNumber;
                        _homerspitAreaOutlines[_currentFilterNumber].effectColor = Color.red;
                        _homerspitTexts[_currentFilterNumber].color = Color.red;
                    }
                }
                else
                {
                    for (int i = 0; i < _homerspitAreaOutlines.Length; i++)
                    {
                        _homerspitAreaOutlines[i].effectColor = Color.black;
                        _homerspitTexts[i].color = Color.black;
                    }
                }
                break;
        }
    }

    #region jeongdongjin
    public void ClickJeongdongjinArea(int number)
    {
        // 같은 지역을 눌렀을 경우
        if (_currentNumber.Equals(number))
        {
            PlayExitEffectAudio();

            _jeongdongjinAreaOutlines[number].effectColor = Color.black;
            _jeongdongjinTexts[number].color = Color.black;

            _currentNumber = -1;
        }
        // 다른 지역을 눌렀을 경우
        else
        {
            PlayClickEffectAudio();

            _currentNumber = number;

            for (int i = 0; i < _jeongdongjinAreaOutlines.Length; i++)
            {
                _jeongdongjinAreaOutlines[i].effectColor = Color.black;
                _jeongdongjinTexts[i].color = Color.black;
            }

            _jeongdongjinAreaOutlines[number].effectColor = Color.red;
            _jeongdongjinTexts[number].color = Color.red;
        }
    }
    public void ClickFilterButton_Jeongdongjin()
    {
        if (_currentNumber.Equals(-1) || _currentFilterNumber.Equals(_currentNumber))
            return;

        PlayClickEffectAudio();

        _isFiltering = true;
        _currentFilterNumber = _currentNumber;

        Dictionary<int, PublicDefined.stRankFishInfo> jeongdongjinDic = DBManager.INSTANCE.GetUserData().GetJeongdongjinRankDictionary();

        Fish[] fishArray = GetJeongdongjinFishArray(_currentNumber);

        for (int i = 0; i < fishArray.Length; i++)
        {
            if (jeongdongjinDic.ContainsKey(fishArray[i].fishDBNum))
                _jeongdongjinBook.bookSlots[i]._starObject.SetActive(true);
            else
                _jeongdongjinBook.bookSlots[i]._starObject.SetActive(false);

            _jeongdongjinBook.bookSlots[i].fishImage.sprite = fishArray[i]._mySprite;
            _jeongdongjinBook.bookSlots[i].nameTxt.text = fishArray[i].fishKoreanName;
            _jeongdongjinBook.bookSlots[i]._type = fishArray[i].GetStructFishData()._gradeType;
            _jeongdongjinBook.bookSlots[i].nameTxt.color = GetColorAccordingToType(fishArray[i].GetStructFishData()._gradeType);

            // 물고기 정보 갯수(4줄)만큼 반복
            //for (int j = 0; j < _jeongdongjinBook.bookSlots[i].info.Length; j++)
            //{
            //    // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
            //    _jeongdongjinBook.bookSlots[i].info[j] = fishArray[i].info[j];
            //    _jeongdongjinBook.bookSlots[i].gameObject.SetActive(true);
            //}

            _jeongdongjinBook.bookSlots[i].info = new string[fishArray[i].info.Length];
            _jeongdongjinBook.bookSlots[i].gameObject.SetActive(true);
            for (int j = 0; j < fishArray[i].info.Length; j++)
            {
                // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                _jeongdongjinBook.bookSlots[i].info[j] = fishArray[i].info[j];
            }
        }

        for (int i = fishArray.Length; i < _jeongdongjinBook.bookSlots.Length; i++)
        {
            _jeongdongjinBook.bookSlots[i].gameObject.SetActive(false);
        }

        _parent.SetActive(false);
    }
    public void ClickReturnButton_Jeongdongjin()
    {
        if (_isFiltering)
        {
            PlayClickEffectAudio();

            _isFiltering = false;
            _jeongdongjinBook.InitBook();

            if (!_currentNumber.Equals(-1))
            {
                _jeongdongjinAreaOutlines[_currentNumber].effectColor = Color.black;
                _jeongdongjinTexts[_currentNumber].color = Color.black;
            }

            _jeongdongjinAreaOutlines[_currentFilterNumber].effectColor = Color.black;
            _jeongdongjinTexts[_currentFilterNumber].color = Color.black;

            _currentNumber = -1;
            _currentFilterNumber = -1;

            _parent.SetActive(false);
        }
    }
    Fish[] GetJeongdongjinFishArray(int number)
    {
        switch(number)
        {
            case 0:
                return _xpxmfkvhemFish;
            case 1:
                return _rmqtlawleoFish;
            case 2:
                return _tkslwlfwleoFish;
            case 3:
                return _gochwleoFish;
            case 4:
                return _dkachwleoFish;
            case 5:
                return _anfrhfwleoFish;
            default:
                return null;
        }
    }
    #endregion

    #region skyway
    public void ClickSkywayArea(int number)
    {
        // 같은 지역을 눌렀을 경우
        if (_currentNumber.Equals(number))
        {
            PlayExitEffectAudio();

            _skywayAreaOutlines[number].effectColor = Color.black;
            _skywayTexts[number].color = Color.black;

            _currentNumber = -1;
        }
        // 다른 지역을 눌렀을 경우
        else
        {
            PlayClickEffectAudio();

            _currentNumber = number;

            for (int i = 0; i < _skywayAreaOutlines.Length; i++)
            {
                _skywayAreaOutlines[i].effectColor = Color.black;
                _skywayTexts[i].color = Color.black;
            }

            _skywayAreaOutlines[number].effectColor = Color.red;
            _skywayTexts[number].color = Color.red;
        }
    }
    public void ClickFilterButton_Skyway()
    {
        if (_currentNumber.Equals(-1) || _currentFilterNumber.Equals(_currentNumber))
            return;

        PlayClickEffectAudio();

        _isFiltering = true;
        _currentFilterNumber = _currentNumber;

        Dictionary<int, PublicDefined.stRankFishInfo> skywayDic = DBManager.INSTANCE.GetUserData().GetSkywayRankDictionary();

        FishSkyway[] fishArray = GetSkywayFishArray(_currentNumber);

        for (int i = 0; i < fishArray.Length; i++)
        {
            if (skywayDic.ContainsKey(fishArray[i].fishDBNum))
                _skywayBook.bookSlots[i]._starObject.SetActive(true);
            else
                _skywayBook.bookSlots[i]._starObject.SetActive(false);

            _skywayBook.bookSlots[i].fishImage.sprite = fishArray[i]._mySprite;
            _skywayBook.bookSlots[i].nameTxt.text = fishArray[i].fishKoreanName;
            _skywayBook.bookSlots[i]._type = fishArray[i].fishType;
            _skywayBook.bookSlots[i].nameTxt.color = GetColorAccordingToType(fishArray[i].fishType);

            // 물고기 정보 갯수(4줄)만큼 반복
            //for (int j = 0; j < _skywayBook.bookSlots[i].info.Length; j++)
            //{
            //    // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
            //    _skywayBook.bookSlots[i].info[j] = fishArray[i].info[j];
            //    _skywayBook.bookSlots[i].gameObject.SetActive(true);
            //}

            _skywayBook.bookSlots[i].info = new string[fishArray[i].info.Length];
            _skywayBook.bookSlots[i].gameObject.SetActive(true);
            for (int j = 0; j < fishArray[i].info.Length; j++)
            {
                // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                _skywayBook.bookSlots[i].info[j] = fishArray[i].info[j];
            }
        }

        for (int i = fishArray.Length; i < _skywayBook.bookSlots.Length; i++)
        {
            _skywayBook.bookSlots[i].gameObject.SetActive(false);
        }
        _parent.SetActive(false);
    }
    public void ClickReturnButton_Skyway()
    {
        if (_isFiltering)
        {
            PlayClickEffectAudio();

            _isFiltering = false;
            _skywayBook.InitBook();

            if (!_currentNumber.Equals(-1))
            {
                _skywayAreaOutlines[_currentNumber].effectColor = Color.black;
                _skywayTexts[_currentNumber].color = Color.black;
            }

            _skywayAreaOutlines[_currentFilterNumber].effectColor = Color.black;
            _skywayTexts[_currentFilterNumber].color = Color.black;

            _currentNumber = -1;
            _currentFilterNumber = -1;
            _parent.SetActive(false);
        }
    }
    FishSkyway[] GetSkywayFishArray(int number)
    {
        switch (number)
        {
            case 0:
                return _xpxmfkvhemFishSkyway;
            case 1:    
                return _rmqtlawleoFishSkyway;
            case 2:    
                return _tkslwlfwleoFishSkyway;
            case 3:
                return _gochwleoFishSkyway;
            case 4:
                return _dkachwleoFishSkyway;
            case 5:
                return _anfrhfwleoFishSkyway;
            default:
                return null;
        }
    }
    #endregion

    #region homerspit
    public void ClickHomerspitArea(int number)
    {
        // 같은 지역을 눌렀을 경우(homerspit은 급심지대가 두개여서 3번 5번은 같은 번호 취급한다.)
        if(number.Equals(3) || number.Equals(5))
        {
            if(_currentNumber.Equals(3) || _currentNumber.Equals(5))
            {
                PlayExitEffectAudio();

                _homerspitAreaOutlines[3].effectColor = Color.black;
                _homerspitTexts[3].color = Color.black;

                _homerspitAreaOutlines[5].effectColor = Color.black;
                _homerspitTexts[5].color = Color.black;

                _currentNumber = -1;
            }
            else
            {
                PlayClickEffectAudio();

                _currentNumber = number;

                for (int i = 0; i < _homerspitAreaOutlines.Length; i++)
                {
                    _homerspitAreaOutlines[i].effectColor = Color.black;
                    _homerspitTexts[i].color = Color.black;
                }

                _homerspitAreaOutlines[3].effectColor = Color.red;
                _homerspitTexts[3].color = Color.red;

                _homerspitAreaOutlines[5].effectColor = Color.red;
                _homerspitTexts[5].color = Color.red;
            }
        }
        else if (_currentNumber.Equals(number))
        {
            PlayExitEffectAudio();

            _homerspitAreaOutlines[number].effectColor = Color.black;
            _homerspitTexts[number].color = Color.black;

            _currentNumber = -1;
        }
        // 다른 지역을 눌렀을 경우
        else
        {
            PlayClickEffectAudio();

            _currentNumber = number;

            for (int i = 0; i < _homerspitAreaOutlines.Length; i++)
            {
                _homerspitAreaOutlines[i].effectColor = Color.black;
                _homerspitTexts[i].color = Color.black;
            }

            _homerspitAreaOutlines[number].effectColor = Color.red;
            _homerspitTexts[number].color = Color.red;
        }
    }
    public void ClickFilterButton_Homerspit()
    {
        if (_currentNumber.Equals(-1) || _currentFilterNumber.Equals(_currentNumber)
            || (_currentNumber.Equals(3) && _currentFilterNumber.Equals(5)) || (_currentNumber.Equals(5) && _currentFilterNumber.Equals(3)))
            return;

        PlayClickEffectAudio();

        _isFiltering = true;
        _currentFilterNumber = _currentNumber;

        Dictionary<int, PublicDefined.stRankFishInfo> homerspitDic = DBManager.INSTANCE.GetUserData().GetHomerspitRankDictionary();

        FishHomerspit[] fishArray = GetHomerspitFishArray(_currentNumber);

        for (int i = 0; i < fishArray.Length; i++)
        {
            if (homerspitDic.ContainsKey(fishArray[i].fishDBNum))
                _homerspitBook.bookSlots[i]._starObject.SetActive(true);
            else
                _homerspitBook.bookSlots[i]._starObject.SetActive(false);

            _homerspitBook.bookSlots[i].fishImage.sprite = fishArray[i]._mySprite;
            _homerspitBook.bookSlots[i].nameTxt.text = fishArray[i].fishKoreanName;
            _homerspitBook.bookSlots[i]._type = fishArray[i].fishType;
            _homerspitBook.bookSlots[i].nameTxt.color = GetColorAccordingToType(fishArray[i].fishType);

            //// 물고기 정보 갯수(4줄)만큼 반복
            //for (int j = 0; j < _homerspitBook.bookSlots[i].info.Length; j++)
            //{
            //    // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
            //    _homerspitBook.bookSlots[i].info[j] = fishArray[i].info[j];
            //    _homerspitBook.bookSlots[i].gameObject.SetActive(true);
            //}

            _homerspitBook.bookSlots[i].info = new string[fishArray[i].info.Length];
            _homerspitBook.bookSlots[i].gameObject.SetActive(true);
            // 물고기 정보 갯수(4줄)만큼 반복
            for (int j = 0; j < fishArray[i].info.Length; j++)
            {
                // 슬롯에 저장될 jeongdongjin 물고기 정보를 프리펩에 있는 정보로 가져옴
                _homerspitBook.bookSlots[i].info[j] = fishArray[i].info[j];
            }
        }

        for (int i = fishArray.Length; i < _homerspitBook.bookSlots.Length; i++)
        {
            _homerspitBook.bookSlots[i].gameObject.SetActive(false);
        }

        _parent.SetActive(false);
    }
    public void ClickReturnButton_Homerspit()
    {
        if (_isFiltering)
        {
            PlayClickEffectAudio();

            _isFiltering = false;
            _homerspitBook.InitBook();

            if (!_currentNumber.Equals(-1))
            {
                _homerspitAreaOutlines[_currentNumber].effectColor = Color.black;
                _homerspitTexts[_currentNumber].color = Color.black;
            }

            if(_currentFilterNumber.Equals(3) || _currentFilterNumber.Equals(5))
            {
                _homerspitAreaOutlines[3].effectColor = Color.black;
                _homerspitTexts[3].color = Color.black;
                _homerspitAreaOutlines[5].effectColor = Color.black;
                _homerspitTexts[5].color = Color.black;
            }
            else
            {
                _homerspitAreaOutlines[_currentFilterNumber].effectColor = Color.black;
                _homerspitTexts[_currentFilterNumber].color = Color.black;
            }
            _currentNumber = -1;
            _currentFilterNumber = -1;
            _parent.SetActive(false);
        }
    }
    FishHomerspit[] GetHomerspitFishArray(int number)
    {
        switch (number)
        {
            case 0:
                return _dkachwleoFishHomerspit;
            case 1:
                return _gochwleoFishHomerspit;
            case 2:
                return _tkslwlfwleoFishHomerspit;
            case 3:
            case 5:
                return _rmqtlawleoFishHomerspit;
            case 4:
                return _anfrhfFishHomerspit;
            case 6:
                return _qkRkxWhranfrhfFishHomerspit;
            default:
                return null;
        }
    }
    #endregion


    Color GetColorAccordingToType(PublicDefined.eFishType type)
    {
        switch (type)
        {
            case PublicDefined.eFishType.Sundry:
                return _sundryColor;
            case PublicDefined.eFishType.Normal:
                return _normalColor;
            case PublicDefined.eFishType.Rare:
                return _rareColor;
            default:
                return Color.white;
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