using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyTutorial : MonoBehaviour
{
    public GameObject _step0Object;
    public GameObject _step1Object;
    public GameObject _step2Object;
    public GameObject _step3Object;
    public GameObject _step4Object;
    public GameObject _step5Object;
    public GameObject _step6Object;
    public GameObject _fishinggearUI;
    public GameObject _step7Object;

    public GameObject _skipButton;
    public GameObject _skipUIObject;

    public Button _goldButton;
    public Button _pearlButton;

    public int _progress;

    void OnEnable()
    {
        _progress = 0;
        _goldButton.interactable = false;
        _pearlButton.interactable = false;
        _step0Object.SetActive(true);

        StartCoroutine(LobbyTutorialCoroutine());
    }

    IEnumerator LobbyTutorialCoroutine()
    {
        yield return new WaitUntil(() => _progress > 0);
        Step1();
        yield return new WaitUntil(() => _progress > 1);
        Step2();
        yield return new WaitUntil(() => _progress > 2);
        Step3();
        yield return new WaitUntil(() => _progress > 3);
        Step4();
        yield return new WaitUntil(() => _progress > 4);
        Step5();
        yield return new WaitUntil(() => _progress > 6);
        Step7();
    }

    #region S0
    public void ClickStartButton0()
    {
        _step0Object.SetActive(false);
        _progress = 1;
    }
    #endregion
    #region S1
    void Step1()
    {
        DBManager.INSTANCE.GetUserData().BackToInitialState();
        Dictionary<string, int> dic = DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary();
        dic["rod"] = -1;
        dic["reel"] = -1;
        _step1Object.SetActive(true);
    }
    public void ClickMailButton1()
    {
        _step1Object.SetActive(false);
        _progress = 2;
    }
    #endregion
    #region S2
    void Step2()
    {
        _step2Object.SetActive(true);
    }
    public void ClickGetButton2()
    {
        _step2Object.SetActive(false);
        _progress = 3;
    }
    #endregion
    #region S3
    void Step3()
    {
        _step2Object.SetActive(false);
        _step3Object.SetActive(true);
    }
    public void ClickXButton3()
    {
        _step3Object.SetActive(false);
        _progress = 4;
    }
    #endregion
    #region S4
    void Step4()
    {
        _step4Object.SetActive(true);
    }
    public void ClickFishingGearButton4()
    {
        _fishinggearUI.SetActive(true);
        _skipButton.SetActive(false);
        _step4Object.SetActive(false);

  
        _progress = 5;
    }
    #endregion
    #region S5
    void Step5()
    {
        _step5Object.SetActive(true);
    }
    public void ClickFishingGearSlotButton5()
    {
        if (_step5Object.activeSelf)
        {
            _step5Object.SetActive(false);
            Step6();
        }
    }
    #endregion
    #region S6
    void Step6()
    {
        _step6Object.SetActive(true);
    }
    public void ClickXButton6()
    {
        _step6Object.SetActive(false);

        Dictionary<string, int> dic = DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary();
        if (dic["rod"].Equals(2) && dic["reel"].Equals(2))
        {
            _fishinggearUI.SetActive(false);
            _skipButton.SetActive(true);
            _progress = 7;
        }
        else
            Step5();
    }
    #endregion
    #region S7
    void Step7()
    {
        _step7Object.SetActive(true);
    }
    #endregion

    public void ClickSkipButton()
    {
        PlayEffectButtonClick();
        Time.timeScale = 0;
        _skipUIObject.SetActive(true);
    }
    public void ClickSkipOKButton()
    {
        Time.timeScale = 1;
        DataManager.INSTANCE._tutorialIsInProgress = false;
        Dictionary<string, object> dic = new Dictionary<string, object>();
        Dictionary<string, int> equipDic = DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary();
        equipDic["rod"] = 2;
        equipDic["reel"] = 2;
        dic.Add("/equipment/rod", 2);
        dic.Add("/equipment/reel", 2);
        DataManager.INSTANCE._tutorialIsDone = true;
        dic.Add("/_isTutorialDone/", true);
        DBManager.INSTANCE.UpdateFirebase(dic);
        LoadingSceneManager.LoadScene("LobbyScene");
    }
    public void ClickSkipCancelButton()
    {
        Time.timeScale = 1;
        PlayExitEffectAudio();
        _skipUIObject.SetActive(false);
    }
    void PlayEffectButtonClick()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
    public void PlayExitEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
    }
}
