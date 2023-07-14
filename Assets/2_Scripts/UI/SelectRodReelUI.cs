using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRodReelUI : MonoBehaviour
{
    public enum eStateType
    {
        SelectRod,
        SelectReel,
        End,
    }

    public Sprite[] _rodSpriteArray;
    public Sprite[] _reelSpriteArray;
    public string[] _rodNameArray;
    public string[] _reelNameArray;
    public GameObject _rodTextObject;
    public GameObject _reelTextObject;
    public GameObject _gameStartObject;
    public GameObject _buttonObject;
    public Button _nextButton;
    public Button _prevButton;
    public Transform _zeroPos;
    public Transform _reelTextObjectRightPos;
    public Transform _gameStartObjectLeftPos;

    public Image _itemImage;
    public Text _itemName;

    int _currentImageNumber = 0;
    int _decideRodImageNumber = 0;
    int _decideReelImageNumber = 0;
    eStateType _currentState = eStateType.SelectRod;

    private void Awake()
    {
        _prevButton.gameObject.SetActive(false);
        ChangeImage();
    }

    void ChangeImage()
    {
        switch(_currentState)
        {
            case eStateType.SelectRod:
                _itemImage.sprite = _rodSpriteArray[_currentImageNumber];
                _itemName.text = _rodNameArray[_currentImageNumber];
                _decideRodImageNumber = _currentImageNumber;
                Debug.Log("ÇöÀç ³¬½Ë´ë ¹øÈ£: " + _decideRodImageNumber);
                break;
            case eStateType.SelectReel:
                _itemImage.sprite = _reelSpriteArray[_currentImageNumber];
                _itemName.text = _reelNameArray[_currentImageNumber];
                _decideReelImageNumber = _currentImageNumber;
                Debug.Log("ÇöÀç ¸± ¹øÈ£: " + _decideReelImageNumber);
                break;
        }
    }

    public void ClickImagePrevButton()
    {
        _currentImageNumber--;

        if (_currentImageNumber < 0)
            _currentImageNumber = 6;

        ChangeImage();
    }

    public void ClickImageNextButton()
    {
        _currentImageNumber++;

        if (_currentImageNumber > 6)
            _currentImageNumber = 0;

        ChangeImage();
    }

    public void ClickPageNextButton()
    {
        switch(_currentState)
        {
            case eStateType.SelectRod:
                _currentState = eStateType.SelectReel;
                _currentImageNumber = 0;
                _rodTextObject.SetActive(false);
                _reelTextObject.SetActive(true);
                // ÆäÀÌÁö ¹öÆ° Àá±ñ ¸ØÃç¾ß ÇÒµí
                _nextButton.interactable = false;
                _prevButton.interactable = false;
                _prevButton.gameObject.SetActive(true);
                StartCoroutine(MakeDelay(1, () =>
                {
                    _nextButton.interactable = true;
                    _prevButton.interactable = true;
                }));
                ChangeImage();
                break;
            case eStateType.SelectReel:
                _currentState = eStateType.End;
                iTween.MoveTo(_buttonObject, iTween.Hash("x", _reelTextObjectRightPos.position.x, "time", 0.5f));
                iTween.MoveTo(_reelTextObject, iTween.Hash("x", _reelTextObjectRightPos.position.x, "time", 0.5f));
                iTween.MoveTo(_gameStartObject, iTween.Hash("x", _zeroPos.position.x, "time", 0.5f));
                // ÆäÀÌÁö ¹öÆ° Àá±ñ ¸ØÃç¾ß ÇÒµí
                _nextButton.interactable = false;
                _prevButton.interactable = false;
                StartCoroutine(MakeDelay(1, () =>
                {
                    _nextButton.interactable = true;
                    _prevButton.interactable = true;
                }));
                break;
            case eStateType.End:
                break;
        }
    }

    public void ClickPagePrevButton()
    {
        switch (_currentState)
        {
            case eStateType.SelectRod:
                break;
            case eStateType.SelectReel:
                _currentState = eStateType.SelectRod;
                _currentImageNumber = 0;
                _rodTextObject.SetActive(true);
                _reelTextObject.SetActive(false);
                _prevButton.gameObject.SetActive(false);
                ChangeImage();
                // ÆäÀÌÁö ¹öÆ° Àá±ñ ¸ØÃç¾ß ÇÒµí
                _nextButton.interactable = false;
                _prevButton.interactable = false;
                StartCoroutine(MakeDelay(1, () =>
                {
                    _nextButton.interactable = true;
                    _prevButton.interactable = true;
                }));
                break;
            case eStateType.End:
                _currentState = eStateType.SelectReel;
                _currentImageNumber = 0;
                _buttonObject.SetActive(true);
                iTween.MoveTo(_gameStartObject, iTween.Hash("x", _gameStartObjectLeftPos.position.x, "time", 0.5f));
                iTween.MoveTo(_reelTextObject, iTween.Hash("x", _zeroPos.position.x, "time", 0.5f));
                iTween.MoveTo(_buttonObject, iTween.Hash("x", _zeroPos.position.x, "time", 0.5f));
                ChangeImage();
                // ÆäÀÌÁö ¹öÆ° Àá±ñ ¸ØÃç¾ß ÇÒµí
                _nextButton.interactable = false;
                _prevButton.interactable = false;
                StartCoroutine(MakeDelay(1, () =>
                {
                    _nextButton.interactable = true;
                    _prevButton.interactable = true;
                }));

                break;
        }
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
}
