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
            // ������Ʈ ����ġ��� 
            case eStateType.First:
                _name = _nicknameInput.text.ToLower();
                DBManager.INSTANCE.SetName(_name);
                _userNicknameText.text = "<color=darkblue>" + _name + "</color> ��!";
                iTween.MoveTo(_createNicknameObject.gameObject, iTween.Hash("x", _createNicknameObjectRightPos.position.x, "time", 0.5f));
                iTween.MoveTo(_welcomeObject.gameObject, iTween.Hash("x", _zeroPos.position.x, "time", 0.5f));
                _nextButton.interactable = false;
                StartCoroutine(MakeDelay(2, () =>
                {
                        _nextButton.interactable = true;
                        _currentState = eStateType.Second;
                }));
                break;
            // ������ �г��� �����ϰ� â �ݱ�
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
            // �г����� ���ǿ� �´��� Ȯ��
            if (_nicknameInput.text.Length < 3 || _nicknameInput.text.Length > 16)
            {
                _stateText.color = _redColor;
                _stateText.text = "�̸��� �ʹ� ª�ų� ����!";

            }
            else // �������� ���� �г����� �ִ��� Ȯ��
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
        //Debug.Log("��������?2");
        _stateText.color = _redColor;
        _stateText.text = "�̹� �ٸ� ������ �̸��Դϴ�.";
    }

    public void TheNameIsAvailable()
    {
        _stateText.color = _greenColor;
        _stateText.text = "��� ������ �̸��Դϴ�.";
        
        _nextButtonAni.SetBool("IsOn", true);
        _isAllow = true;
    }
}
