using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchResultUI : MonoBehaviour
{
    public Sprite[] _bgSprites;
    public Text _gradeText;
    public Text _conditionText;
    public Text _myNameText;
    public Text _myScoreText;
    public Text _opponentNameText;
    public Text _opponentScoreText;

    public GameObject _winObject;
    public GameObject _loseObject;
    public GameObject _drawObject;

    public GameObject[] _starObject;
    public Image[] _1_3_starImages;
    public Image[] _4_6_starImages;
    public Image[] _7_9_starImages;


    public GameObject _exitButton;

    public GameObject[] _victoryEffects;

    public Animator _gradeTextAni;

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
            case 5:
                yield return PublicDefined._25secDelay;
                break;
            default:
                yield return PublicDefined._1secDelay;
                break;
        }
        action();
    }
    IEnumerator EffectCoroutine()
    {
        WaitForSeconds delay1 = PublicDefined._1secDelay;
        WaitForSeconds delay2 = PublicDefined._05secDelay;

        //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeGradeUp).GetComponent<AudioPoolObject>().Init();

        for (int i = 0; i < _victoryEffects.Length; i++)
        {
            _victoryEffects[i].SetActive(true);

            if (i > 1)
                yield return delay2;
            else
                yield return delay1;
        }
        yield return null;
    }

    public void Init(int beforeGrade, float beforeStar, int grade, float star, string condition, int isVictory, string myName, string opponentName, float myScore, float opponentScore)
    {
        transform.GetComponent<Image>().sprite = _bgSprites[UnityEngine.Random.Range(0, 3)];
        gameObject.SetActive(true);
        _exitButton.SetActive(false);

        CheckGrade(beforeGrade);
        OnStarObject(grade);

        _conditionText.text = condition;

        if (isVictory > 0)
        {
            StartCoroutine(EffectCoroutine());

            _winObject.SetActive(true);
            _loseObject.SetActive(false);
            _drawObject.SetActive(false);

            if (beforeGrade < grade)
            {
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeGradeUp).GetComponent<AudioPoolObject>().Init();
                StartCoroutine(GradeUpCoroutine(grade, star));
            }
            else
            {
                AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.fishCatchSuccess).GetComponent<AudioPoolObject>().Init();
                CheckStar(grade, star);
            }
        }
        else if (isVictory < 0)
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeLose).GetComponent<AudioPoolObject>().Init();
            _winObject.SetActive(false);
            _loseObject.SetActive(true);
            _drawObject.SetActive(false);

            CheckStar(grade, star);
        }
        else
        {
            AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.matchModeDraw).GetComponent<AudioPoolObject>().Init();
            _winObject.SetActive(false);
            _loseObject.SetActive(false);
            _drawObject.SetActive(true);

            CheckStar(grade, star);
        }


        _myNameText.text = myName;
        _opponentNameText.text = opponentName;
        _myScoreText.text = myScore.ToString("N1");
        _opponentScoreText.text = opponentScore.ToString("N1");
    }

    IEnumerator ShowStarCoroutine(Image[] images)
    {
        float fill = 0;
        Color c = new Color(1, 1, 1, 0);

        while(fill < 1)
        {
            if (fill <= 1)
            {
                fill += Time.deltaTime * 0.5f;
                c.a = fill;

                for (int i = 0; i < images.Length; i++)
                {
                    images[i].color = c;
                }
            }

            _exitButton.SetActive(true);
            yield return null;
        }
    }
    IEnumerator GradeUpCoroutine(int g, float s)
    {


        WaitForSeconds delay = PublicDefined._25secDelay;

        _gradeTextAni.SetTrigger("GradeUp");

        yield return delay;

        CheckGrade(g);
        CheckStar(g, s);
    }
    void CheckGrade(int g)
    {
        switch(g)
        {
            case 1:
                _gradeText.text = "1´Ü";
                break;
            case 2:
                _gradeText.text = "2´Ü";
                break;
            case 3:
                _gradeText.text = "3´Ü";
                break;
            case 4:
                _gradeText.text = "4´Ü";
                break;
            case 5:
                _gradeText.text = "5´Ü";
                break;
            case 6:
                _gradeText.text = "6´Ü";
                break;
            case 7:
                _gradeText.text = "7´Ü";
                break;
            case 8:
                _gradeText.text = "8´Ü";
                break;
            case 9:
                _gradeText.text = "9´Ü";
                break;
        }
    }
    void OnStarObject(int g)
    {
        if(g <4)
        {
            _starObject[0].SetActive(true);
            _starObject[1].SetActive(false);
            _starObject[2].SetActive(false);
        }
        else if(g < 7)
        {
            _starObject[0].SetActive(false);
            _starObject[1].SetActive(true);
            _starObject[2].SetActive(false);
        }
        else
        {
            _starObject[0].SetActive(false);
            _starObject[1].SetActive(false);
            _starObject[2].SetActive(true);
        }
    }
    void CheckStar(int g, float s)
    {
        float value = s / 1;
        float rest = s % 1;

        //Debug.LogError(value);

        if (s.Equals(0))
            _exitButton.SetActive(true);
        else
        {
            // 1 ~ 3 
            if (g < 4)
            {
                for (int i = 0; i < _1_3_starImages.Length; i++)
                {
                    // ²Ë Âù º° °¹¼ö
                    if (i >= value)
                        _1_3_starImages[i].fillAmount = 0;
                    else
                        _1_3_starImages[i].fillAmount = 1;
                }

                if (rest > 0)
                    _1_3_starImages[(int)value].fillAmount = 0.5f;

                StartCoroutine(ShowStarCoroutine(_1_3_starImages));

            }
            // 4 ~ 6
            else if (g < 7)
            {
                for (int i = 0; i < _4_6_starImages.Length; i++)
                {
                    // ²Ë Âù º° °¹¼ö
                    if (i >= value)
                        _4_6_starImages[i].fillAmount = 0;
                    else
                        _4_6_starImages[i].fillAmount = 1;
                }

                if (rest > 0)
                    _4_6_starImages[(int)value].fillAmount = 0.5f;

                StartCoroutine(ShowStarCoroutine(_4_6_starImages));
            }
            else
            {
                for (int i = 0; i < _7_9_starImages.Length; i++)
                {
                    // ²Ë Âù º° °¹¼ö
                    if (i >= value)
                        _7_9_starImages[i].fillAmount = 0;
                    else
                        _7_9_starImages[i].fillAmount = 1;
                }

                if (rest > 0)
                    _7_9_starImages[(int)value].fillAmount = 0.5f;

                StartCoroutine(ShowStarCoroutine(_7_9_starImages));
            }
        }
    }

    public void ClickExitButton()
    {
        PlayExitEffectAudio();
        DataManager.INSTANCE._matchGameIsInProgress = false;
        LoadingSceneManager.LoadScene("MapSelectScene");
    }

    public void ClickRestartButton()
    {
        PlayExitEffectAudio();
        DataManager.INSTANCE._matchGameIsInProgress = true;
        LoadingSceneManager.LoadScene("MapSelectScene");
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
