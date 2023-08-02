using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateNicknameUI : MonoBehaviour
{
    public enum eStateType
    { 
        First,
        Second,
        End,
    }

    readonly Color _redColor = new Color(1, 0.4f, 0.4f);
    readonly Color _greenColor = new Color(0, 0.8f, 0);

    [SerializeField] Button _nextButton;
    [SerializeField] Animator _nextButtonAni;
    [SerializeField] Transform _createNicknameObject;
    [SerializeField] Transform _createNicknameObjectRightPos;
    [SerializeField] Transform _welcomeObject;
    [SerializeField] Transform _zeroPos;
    [SerializeField] Text _stateText;
    [SerializeField] Text _userNicknameText;
    [SerializeField] InputField _nicknameInput;

    bool _isAllow = false;
    string _name = string.Empty;

    eStateType _currentState = eStateType.First;

    IEnumerator MakeDelay(int delayNumber, Action action)
    {
        // 1: 0.5f , 2: 1f, 3: 1.5f, 4: 2f
        switch (delayNumber)
        {
            case 1:
                yield return PublicDefined._05secDelay;
                break;
            case 2:
                yield return PublicDefined._1secDelay;
                break;
            case 3:
                yield return PublicDefined._15secDelay;
                break;
            case 4:
                yield return PublicDefined._2secDelay;
                break;
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }

    public void ClickNextButton()
    {
        if (!_isAllow)
            return;

        switch(_currentState)
        {
            // 오브젝트 갈아치우기 
            case eStateType.First:
                _name = _nicknameInput.text.ToLower();
                DBManager.INSTANCE.SetName(_name);
                _userNicknameText.text = "<color=darkblue>" + _name + "</color> 님!";
                iTween.MoveTo(_createNicknameObject.gameObject, iTween.Hash("x", _createNicknameObjectRightPos.position.x, "time", 0.5f));
                iTween.MoveTo(_welcomeObject.gameObject, iTween.Hash("x", _zeroPos.position.x, "time", 0.5f));
                _nextButton.interactable = false;
                StartCoroutine(MakeDelay(2, () =>
                {
                        _nextButton.interactable = true;
                        _currentState = eStateType.Second;
                }));
                break;
            // 서버에 닉네임 저장하고 창 닫기
            case eStateType.Second:
                DBManager.INSTANCE.SaveNewUserData();
                gameObject.SetActive(false);
                _nextButton.interactable = false;
                _currentState = eStateType.End;
                break;
            case eStateType.End:
                break;
        }
    }

    public void ClickExistButton()
    {
        if (_isAllow)
            return;

        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            TheNameIsAvailable();
        }
        else
        {
            // 닉네임이 조건에 맞는지 확인
            if (_nicknameInput.text.Length < 3 || _nicknameInput.text.Length > 16)
            {
                _stateText.color = _redColor;
                _stateText.text = "이름이 너무 짧거나 길어요!";

            }
            else // 서버에서 같은 닉네임이 있는지 확인
            {
                _name = _nicknameInput.text.ToLower();
                //TheNameIsAvailable();
                StartCoroutine(DBManager.INSTANCE.ExistNickname(_name));
            }
        }
    }



    public void InputFieldUpdate()
    {
        if (_isAllow)
        {
            _isAllow = false;
            _stateText.text = string.Empty;
            _nextButtonAni.SetBool("IsOn", false);
        }
    }

    public void InputFieldEndEdit()
    {
        //Debug.Log("zz");
        _nicknameInput.text = _nicknameInput.text.ToLower();
    }

    public void TheNameAlreadyExists()
    {
        //Debug.Log("들어오나요?2");
        _stateText.color = _redColor;
        _stateText.text = "이미 다른 유저의 이름입니다.";
    }

    public void TheNameIsAvailable()
    {
        _stateText.color = _greenColor;
        _stateText.text = "사용 가능한 이름입니다.";
        
        _nextButtonAni.SetBool("IsOn", true);
        _isAllow = true;
    }
}
