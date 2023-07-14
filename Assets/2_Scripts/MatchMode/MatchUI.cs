using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
    readonly string[] _mapString = { "�� �� ��", "�� ī �� �� ��", "ȣ �� �� ��" };
    readonly string[] _winCoditionString = { "���� ����� ���� ����", "���� ����� ���� ����", "���� ����� ������ ����", "���� ����� ��� ����" };
    readonly string[] _sceneNames = { "Match_EastSea", "Match_Skyway", "Match_Homerspit" };
    readonly WaitForSeconds _delay = new WaitForSeconds(0.05f);
    readonly WaitForSeconds _countDelay = new WaitForSeconds(3f);

    [Header("��")]
    public Sprite[] _mapSprites;
    public Image _mapImage;
    public Text _mapName;

    [Header("�ð�")]
    public string[] _timeStrings;
    public Text _timeText;

    [Header("�¸� ����")]
    public Text _winConditionText;

    [Header("��ư")]
    public Button _matchButton;
    public Button _xButton;

    [Header("ī��Ʈ")]
    public Text _countText;

    [Header("Matching �ؽ�Ʈ")]
    public GameObject _findTextObject;
    public GameObject _matchingTextObject;
    public GameObject _findSuccessObject;

    int _step;
    int _mapNumber;
    int _timeNumber;
    int _conditionNumber;
    bool _isMatching;
    Coroutine _matchingCoroutine;
    Coroutine _timerCoroutine;
    bool _isMatchingSuccess;

    private void OnEnable()
    {
        _findTextObject.SetActive(true);
        _matchingTextObject.SetActive(false);
        _findSuccessObject.SetActive(false);
        _matchButton.gameObject.SetActive(true);
        _countText.gameObject.SetActive(false);
        _step = 0;

        _matchingCoroutine = StartCoroutine(MatchingTextCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_matchingCoroutine);
        _matchingCoroutine = null;
    }

    public void ClickMatch()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();

        _isMatching = true;

        _matchButton.interactable = false;
        _findTextObject.SetActive(false);
        _matchingTextObject.SetActive(true);
        _mapNumber = ReturnRandomIndex(0, 2);
        _timeNumber = ReturnRandomIndex(0, 23);
        _conditionNumber = ReturnRandomIndex(0, 3);

        // �׽�Ʈ
        //_mapNumber = 0;
        //_conditionNumber = 0;

        // ��Ī �ڷ�ƾ �����Ų��.
        _timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void ClickXButton()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        DataManager.INSTANCE._matchGameIsInProgress = false;
        // ��Ī�� ã�� ���̶�� ��Ī�� ����Ѵ�.
        if (_isMatching)
        {
            _matchButton.interactable = true;
            _findTextObject.SetActive(true);
            _matchingTextObject.SetActive(false) ;
            _isMatching = false;

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
        }
        // ��Ī�� ã�� ���� �ƴ϶�� UI�� ����.
        else
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator MatchingTextCoroutine()
    {
        yield return new WaitUntil(() => _isMatchingSuccess.Equals(true));

        _xButton.interactable = false;
        _matchingTextObject.SetActive(false);
        _findSuccessObject.SetActive(true);

        StartCoroutine(MatchCoroutine());
        StartCoroutine(RandomCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        int rand = Random.Range(1, 3);
        float timer = 0;
        while (timer < rand)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _isMatchingSuccess = true;
    }
    IEnumerator RandomCoroutine()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeMatching).GetComponent<AudioPoolObject>().Init();

        yield return new WaitUntil(() => _step > 0);
        _mapName.text = _mapString[_mapNumber];
        _mapImage.sprite = _mapSprites[_mapNumber];

        yield return new WaitUntil(() => _step > 1);
        _timeText.text = _timeStrings[_timeNumber];

        yield return new WaitUntil(() => _step > 2);
        _winConditionText.text = _winCoditionString[_conditionNumber];

        _matchButton.gameObject.SetActive(false);
        _countText.gameObject.SetActive(true);

        yield return _countDelay;

        DataManager.INSTANCE._matchGameIsInProgress = true;
        DataManager.INSTANCE._matchConditionNumber = _conditionNumber;
        DataManager.INSTANCE._worldTime = _timeNumber;

        // ���� ����
        switch(_mapNumber)
        {
            case 0:
                DataManager.INSTANCE._mapType = PublicDefined.eMapType.jeongdongjin;
                break;
            case 1:
                DataManager.INSTANCE._mapType = PublicDefined.eMapType.skyway;
                break;
            case 2:
                DataManager.INSTANCE._mapType = PublicDefined.eMapType.homerspit;
                break;
        }
        DataManager.INSTANCE._isMatch = true;
        DataManager.INSTANCE._matchConditionNumber = _conditionNumber;
        LoadingSceneManager.LoadScene(_sceneNames[_mapNumber]);
        //DataManager.INSTANCE._mapType = PublicDefined.eMapType.skyway;
        //LoadingSceneManager.LoadScene(_sceneNames[1]);
    }
    IEnumerator MatchCoroutine()
    {
        float timer = 0;
        int map = 0, time = 0, condition = 0;

        while (timer < 3)
        {
            if (map > 2)
                map = 0;

            if (time > 23)
                time = 0;

            if (condition > 3)
                condition = 0;

            _mapName.text = _mapString[map];
            _timeText.text = _timeStrings[time];
            _winConditionText.text = _winCoditionString[condition];

            map++;
            time++;
            condition++;

            timer += 0.1f;
            yield return _delay;
        }

        _step = 1;
        timer = 0;

        while (timer < 3)
        {
            if (time > 23)
                time = 0;

            if (condition > 3)
                condition = 0;

            _timeText.text = _timeStrings[time];
            _winConditionText.text = _winCoditionString[condition];

            time++;
            condition++;

            timer += 0.1f;
            yield return _delay;
        }

        _step = 2;
        timer = 0;

        while (timer < 3)
        {
            if (condition > 3)
                condition = 0;

            _winConditionText.text = _winCoditionString[condition];

            condition++;

            timer += 0.1f;
            yield return _delay;
        }

        _step = 3;
    }
    int ReturnRandomIndex(int start, int end)
    {
        int number = Random.Range(start, end + 1);

        return number;
    }
}
