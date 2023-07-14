using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PassManager : MonoBehaviour
{
    static PassManager _uniqueInstance;
    static public PassManager INSTANCE 
    { 
        get { return _uniqueInstance; }
    }
    UserData _userData;
    // jeongdongjin
    Dictionary<int, Dictionary<int, int>> _currentStateOfJeongdongjinPassAboutFish;
    Dictionary<int, int> _currentStateOfJeongdongjinPassAboutAction;

    // homerspit
    Dictionary<int, Dictionary<int, int>> _currentStateOfHomerspitPassAboutFish;
    Dictionary<int, int> _currentStateOfHomerspitPassAboutAction;

    // skyway
    Dictionary<int, Dictionary<int, int>> _currentStateOfSkywayPassAboutFish;
    Dictionary<int, int> _currentStateOfSkywayPassAboutAction;

    InGameUIManager _ingameUI;

    private void Awake()
    {
        _uniqueInstance = this;
    }
    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _userData = DBManager.INSTANCE.GetUserData();

        _currentStateOfJeongdongjinPassAboutFish = _userData.GetCurrentStateOfJeongdongjinPassAboutFish();
        _currentStateOfJeongdongjinPassAboutAction = _userData.GetCurrentStateOfJeongdongjinPassAboutAction();

        _currentStateOfSkywayPassAboutFish = _userData.GetCurrentStateOfSkywayPassAboutFish();
        _currentStateOfSkywayPassAboutAction = _userData.GetCurrentStateOfSkywayPassAboutAction();

        _currentStateOfHomerspitPassAboutFish = _userData.GetCurrentStateOfHomerspitPassAboutFish();
        _currentStateOfHomerspitPassAboutAction = _userData.GetCurrentStateOfHomerspitPassAboutAction();
    }

    public void SetIngameUIManager(InGameUIManager instance, bool isClear)
    {
        _ingameUI = instance;

        if (DataManager.INSTANCE._matchGameIsInProgress || DataManager.INSTANCE._tutorialIsInProgress)
            return;

        if (isClear)
            return;
        else
            SetCurrentQuest();
    }
    void SetCurrentQuest()
    {
        if(_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        _currentStateOfJeongdongjinPassAboutFish = _userData.GetCurrentStateOfJeongdongjinPassAboutFish();
        _currentStateOfJeongdongjinPassAboutAction = _userData.GetCurrentStateOfJeongdongjinPassAboutAction();

        _currentStateOfSkywayPassAboutFish = _userData.GetCurrentStateOfSkywayPassAboutFish();
        _currentStateOfSkywayPassAboutAction = _userData.GetCurrentStateOfSkywayPassAboutAction();

        _currentStateOfHomerspitPassAboutFish = _userData.GetCurrentStateOfHomerspitPassAboutFish();
        _currentStateOfHomerspitPassAboutAction = _userData.GetCurrentStateOfHomerspitPassAboutAction();

        string[] content;
        int index;

        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                index = _userData._currentJeongdongjinPassIndex;

                if (index.Equals((int)PublicDefined.eJeongdongjinPass.Count))
                {
                    _ingameUI.AllClearPassQuest();
                    return;
                }

                content = SetCurrentQuest_Jeongdongjin();
                _ingameUI.SetQuestContent(content[0], content[1]);

                CheckJeongdongjin();

                break;
            case PublicDefined.eMapType.skyway:
                index = _userData._currentSkywayPassIndex;

                if (index.Equals((int)PublicDefined.eSkywayPass.Count))
                {
                    _ingameUI.AllClearPassQuest();
                    return;
                }

                content = SetCurrentQuest_Skyway();
                _ingameUI.SetQuestContent(content[0], content[1]);

                CheckSkyway();

                break;
            case PublicDefined.eMapType.homerspit:
                index = _userData._currentHomerspitPassIndex;

                if (index.Equals((int)PublicDefined.eHomerspitPass.Count))
                {
                    _ingameUI.AllClearPassQuest();
                    return;
                }

                content = SetCurrentQuest_Homerspit();
                _ingameUI.SetQuestContent(content[0], content[1]);

                CheckHomerspit();
                break;
        }
    }

    public void ToPassManagerAboutFish(PublicDefined.stFishInfo fish)
    {
        switch(DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                CheckClearPassAboutFish_Jeongdongjin(fish);
                break;
            case PublicDefined.eMapType.skyway:
                CheckClearPassAboutFish_Skyway(fish);
                break;
            case PublicDefined.eMapType.homerspit:
                CheckClearPassAboutFish_Homerspit(fish);
                break;
        }
    }
    public void ToPassManagerAboutAction()
    {
        switch (DataManager.INSTANCE._mapType)
        {
            case PublicDefined.eMapType.jeongdongjin:
                CheckClearPassAboutAction_Jeongdongjin();
                break;
            case PublicDefined.eMapType.skyway:
                CheckClearPassAboutAction_Skyway();
                break;
            case PublicDefined.eMapType.homerspit:
                CheckClearPassAboutAction_Homerspit();
                break;
        }
    }
    public void ToPassManagerAboutCommon()
    {
        PublicDefined.eJeongdongjinPass jeongdongjinIndex = (PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex;
        PublicDefined.eSkywayPass skywayIndex = (PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex;
        PublicDefined.eHomerspitPass homerspitIndex = (PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex;

        if(jeongdongjinIndex.Equals(PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl) || jeongdongjinIndex.Equals(PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl))
        {
            CheckClearPassAboutAction_Jeongdongjin();
        }

        if (skywayIndex.Equals(PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl) || skywayIndex.Equals(PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl))
        {
            CheckClearPassAboutAction_Skyway();
        }

        if (homerspitIndex.Equals(PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl) || homerspitIndex.Equals(PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl))
        {
            CheckClearPassAboutAction_Homerspit();
        }
    }

    #region jeongdongjin 물고기 관련 패스
    public void CheckClearPassAboutFish_Jeongdongjin(PublicDefined.stFishInfo fish)
    { 
        Dictionary<int, int> dic;

        switch ((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl, out dic);
                if (dic == null)
                {
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                }
                else if (dic.ContainsKey(fish._fishNumber))
                {
                    dic[fish._fishNumber]++;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                }
                else
                {
                    dic.Add(fish._fishNumber, 1);
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                }

                //int count = 0;
                //foreach (int data in dic.Values)
                //{
                //    count += data;
                //}
                //if (count > 2)
                //{
                //    //클리어
                //    //_userData._currentJeongdongjinPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //    //CheckJeongdongjin();
                //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                //}
                break;
            case PublicDefined.eJeongdongjinPass.tjdeowkqrl:
                if(fish._fishNumber.Equals(18))
                {
                    // 클리어, 딕셔너리 굳이 해야하나 싶은데 일단 해보고 나중에 빼도 되면 빼자.
                    dic = new Dictionary<int, int>();
                    dic.Add(18, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(tjdeowkqrl)");

                }
                break;
            case PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl:
                if (fish._fishNumber.Equals(6))
                {
                    if (fish._length >= 35)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(6, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(ehekfl35cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl:
                if (fish._fishNumber.Equals(25))
                {
                    if (fish._length >= 40)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(25, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(whvlqhffkr40cmdltkdwkqrl)");

                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl:
                if (fish._fishNumber.Equals(15))
                {
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(15, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("현재 붕장어 Count : " + dic[15]);
                    }
                    else
                    {
                        dic[15]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("현재 붕장어 Count : " + dic[15]);
                        //if (dic[fish._fishNumber] > 2)
                        //{
                        //    // 클리어
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(qndwkddj3akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl:
                if(fish._fishNumber.Equals(1) || fish._fishNumber.Equals(14) || fish._fishNumber.Equals(13))
                {
                    // 만약 볼락, 불볼락, 개볼락 중 한마리가 잡혔다면 들어온다.
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(fish._fishNumber, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("현재 볼락,불볼락,개볼락 Count : " + _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].Count);

                    }
                    else if(!dic.ContainsKey(fish._fishNumber))
                    {
                        dic.Add(fish._fishNumber, 1);
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //if (dic.Count.Equals(3))
                        //{
                        //    // 클리어
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(카운트가 3이 되면 클리어된거다.)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl:
                if (fish._fishNumber.Equals(9))
                {
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl, out dic);
                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(9, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                       // Debug.Log("현재 무늬오징어 Count : " + dic[9]);
                    }
                    else
                    {
                        dic[9]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("현재 무늬오징어 Count : " + dic[9]);
                    
                        //if (dic[9] > 4)
                        //{
                        //    // 클리어
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(ansmldhwlddj5akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl:
                if (fish._fishNumber.Equals(3))
                {
                    if (fish._weight >= 1.5f)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(3, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(넙치1.5kg이상잡기)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl:
                if (fish._fishNumber.Equals(2))
                {
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(2, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl] = dic;
                        //Debug.Log("현재 고등어 Count : " + dic[2]);
                    }
                    else
                    {
                        dic[2]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                       // Debug.Log("현재 고등어 Count : " + dic[2]);

                        //if (dic[2] > 19)
                        //{
                        //    // 클리어
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(rhemddj20akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl:
                if (fish._fishNumber.Equals(21))
                {
                    if (fish._length >= 50)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(21, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(dladustndj50cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl:
                if (fish._fishNumber.Equals(30))
                {
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(30, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("현재 학공치 Count : " + dic[30]);
                    }
                    else
                    {
                        dic[30]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                       // Debug.Log("현재 학공치 Count : " + dic[30]);

                        //if (dic[30] > 19)
                        //{
                        //    // 클리어
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(20))
                {
                    if (fish._length >= 70)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(20, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(didxo70cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl:
                if(fish._fishNumber.Equals(31))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if(baitNumber.Equals(15) || baitNumber.Equals(22) || baitNumber.Equals(32) || baitNumber.Equals(33) || baitNumber.Equals(34) || baitNumber.Equals(50)
                         || baitNumber.Equals(36) || baitNumber.Equals(52) || baitNumber.Equals(53) || baitNumber.Equals(54))
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(31, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(fndjskRtlfhghkddjwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl:
                if (fish._fishNumber.Equals(19))
                {
                    if (fish._weight >= 3)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(19, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(tnddj3kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl:
                if(fish._fishNumber.Equals(99) || fish._fishNumber.Equals(5) || fish._fishNumber.Equals(10) || fish._fishNumber.Equals(11) || fish._fishNumber.Equals(16) 
                    || fish._fishNumber.Equals(24))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl:
                if (fish._fishNumber.Equals(9))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(36))
                    {
                        _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl, out dic);

                        if (dic == null)
                        {
                            dic = new Dictionary<int, int>();
                            dic.Add(9, 1);
                            _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl] = dic;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                            //Debug.Log("현재 무늬오징어 Count : " + dic[9]);
                        }
                        else
                        {
                            dic[9]++;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                            //Debug.Log("현재 무늬오징어 Count : " + dic[9]);
                            //if (dic[9].Equals(10))
                            //{
                            //    // 클리어
                            //    //_userData._currentJeongdongjinPassIndex++;
                            //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                            //    //CheckJeongdongji();
                            //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(dprlfmfdldydgoansmldhwlddj10akflwkqrl)");
                                
                            //}
                        }
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl:
                if(fish._weight >= 5)
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(anfrhrl5kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eJeongdongjinPass.rkatjdehawkqrl:
                if(fish._fishNumber.Equals(99))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(99, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(rkatjdehawkqrl)");
                }
                break;
            case PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl:
                if(fish._fishNumber.Equals(10))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(53))
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(10, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                       // Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어(vhqvjfmfdldydgoqkddjwkqrl)");
                        
                    }
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region jeongdongjin 행동 관련 패스
    public void CheckClearPassAboutAction_Jeongdongjin()
    {
        switch((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl:
                // 클리어: 해당 딕셔너리 1로 바꾸고 파이어베이스에 업데이트, 인덱스도 업데이트
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);

                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl:
                // 클리어
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl:
                //if(_userData.GetJeongdongjinRankDictionary().Count > 4)
                //{
                //    // 클리어
                //    //_userData._currentJeongdongjinPassIndex= 1;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //    CheckJeongdongjin();
                //    Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                //}

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl:
                // 클리어
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 9)
                {
                    // 클리어
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 14)
                {
                    // 클리어
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                   // Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl:
                // 클리어
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex= 1;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 19)
                {
                    // 클리어
                    //_userData._currentJeongdongjinPassIndex= 1;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 24)
                {
                    // 클리어
                    //_userData._currentJeongdongjinPassIndex= 1;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl:
                // 클리어
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex= 1;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 29)
                {
                    // 클리어
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "번 퀘스트 클리어");
                }
                break;
        }

        if(DataManager.INSTANCE._mapType == PublicDefined.eMapType.jeongdongjin || DataManager.INSTANCE._mapType == PublicDefined.eMapType.homerspit ||
            DataManager.INSTANCE._mapType == PublicDefined.eMapType.skyway)
        {
            SetCurrentQuest();
        }
    }
    #endregion

    #region homerspit 물고기 관련 패스
    public void CheckClearPassAboutFish_Homerspit(PublicDefined.stFishInfo fish)
    {
        Dictionary<int, int> dic;
        
        switch ((PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex)
        {
            case PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl, out dic);
                if (dic == null)
                {
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                }
                else if (dic.ContainsKey(fish._fishNumber))
                {
                    dic[fish._fishNumber]++;

                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                }
                else
                {
                    dic.Add(fish._fishNumber, 1);
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                }

                //int count = 0;
                //foreach(int data in dic.Values)
                //{
                //    count += data;
                //}

                //if (count > 2)
                //{
                //    //클리어
                //    //_userData._currentHomerspitPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //    Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                //}
                break;
            case PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl:
                if (fish._fishNumber.Equals(56))
                {
                    dic = new Dictionary<int, int>();
                    dic.Add(56, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(skfrownfrhrlwkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl:
                if (fish._fishNumber.Equals(48))
                {
                    if (fish._length >= 14)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(48, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                       // Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(shfkdrkrtltjeo14cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(55))
                {
                    if (fish._length >= 70)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(55, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(dmseorn70cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl:
                if (fish._fishNumber.Equals(50))
                {
                    _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(50, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    }
                    else
                    {
                        dic[50]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        if (dic[fish._fishNumber] > 2)
                        {
                            // 클리어
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(wkdansqhffkr3akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl:
                if (fish._fishNumber.Equals(41) || fish._fishNumber.Equals(45) || fish._fishNumber.Equals(62))
                {
                    _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(fish._fishNumber, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 화살치,마소치,각시 중 Count : " + _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].Count);

                    }
                    else if (!dic.ContainsKey(fish._fishNumber))
                    {
                        dic.Add(fish._fishNumber, 1);
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        if (dic.Count.Equals(3))
                        {
                            // 클리어
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(카운트가 3이 되면 클리어된거다.)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl:
                if (fish._fishNumber.Equals(46))
                {
                    _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl, out dic);
                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(46, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 자붉돔 Count : " + dic[46]);

                    }
                    else
                    {
                        dic[46]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 자붉돔 Count : " + dic[46]);

                        if (dic[46] > 4)
                        {
                            // 클리어
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(wkqnfreha5akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl:
                if (fish._fishNumber.Equals(59))
                {
                    if (fish._weight >= 1)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(59, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(qlwrmasnseha1kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl:
                if (fish._fishNumber.Equals(60))
                {
                    _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(60, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl] = dic;
                        //Debug.Log("현재 정어리 Count : " + dic[60]);
                    }
                    else
                    {
                        dic[60]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 정어리 Count : " + dic[60]);

                        if (dic[60] > 19)
                        {
                            // 클리어
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(wjddjfl20akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(39))
                {
                    if (fish._length >= 70)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(39, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(dusdj70cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl:
                if (fish._fishNumber.Equals(51))
                {
                    _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(51, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 꽁치 Count : " + dic[51]);
                    }
                    else
                    {
                        dic[51]++; 
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("현재 꽁치 Count : " + dic[51]);

                        if (dic[51] > 19)
                        {
                            //// 클리어
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl:
                if (fish._fishNumber.Equals(49))
                {
                    if (fish._length >= 75)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(49, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(eorn75cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl:
                if (fish._fishNumber.Equals(55))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(15) || baitNumber.Equals(22) || baitNumber.Equals(32) || baitNumber.Equals(33) || baitNumber.Equals(34) || baitNumber.Equals(50)
                         || baitNumber.Equals(36) || baitNumber.Equals(52) || baitNumber.Equals(53) || baitNumber.Equals(54))
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(55, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(fndjskRtlfhdmseornwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl:
                if (fish._fishNumber.Equals(32))
                {
                    if (fish._weight >= 3)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(32, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(audxo3kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl:
                if (fish._fishNumber.Equals(34) || fish._fishNumber.Equals(38) || fish._fishNumber.Equals(43) || fish._fishNumber.Equals(47) || fish._fishNumber.Equals(52)
                    || fish._fishNumber.Equals(57) || fish._fishNumber.Equals(61) || fish._fishNumber.Equals(63))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl:
                if (fish._fishNumber.Equals(32))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(34))
                    {
                        _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl, out dic);

                        if (dic == null)
                        {
                            dic = new Dictionary<int, int>();
                            dic.Add(32, 1);
                            _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl] = dic;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                            //Debug.Log("현재 명태 Count : " + dic[32]);
                        }
                        else
                        {
                            dic[32]++;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);

                            if (dic[32] > 9)
                            {
                                // 클리어
                                //_userData._currentHomerspitPassIndex++;
                                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                                //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(tldzldalshdnfmfdldydgoaudxo10akflwkqrl)");
                            }
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl:
                if (fish._weight >= 10)
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(anfrhrl10kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.dkrtkddjwkqrl:
                if (fish._fishNumber.Equals(57))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(57, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(rkatjdehawkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl:
                if (fish._fishNumber.Equals(63))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(53))
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(63, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어(vhqvjfmfdldydgoghkdekfkddjwkqrl)");
                    }
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region homerspit 행동 관련 패스
    public void CheckClearPassAboutAction_Homerspit()
    {
        switch ((PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex)
        {
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl:
                // 클리어: 해당 딕셔너리 1로 바꾸고 파이어베이스에 업데이트, 인덱스도 업데이트
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex + "번 퀘스트 클리어");
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl:
                // 클리어
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr5akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 4)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl:
                // 클리어
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
               //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr10akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 9)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr15akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 14)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl:
                // 클리어
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr20akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 19)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr25akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 24)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl:
                // 클리어
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr30akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 29)
                {
                    // 클리어
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "번 퀘스트 클리어");
                }
                break;
        }
        if (DataManager.INSTANCE._mapType == PublicDefined.eMapType.jeongdongjin || DataManager.INSTANCE._mapType == PublicDefined.eMapType.homerspit ||
            DataManager.INSTANCE._mapType == PublicDefined.eMapType.skyway)
        {
            SetCurrentQuest();
        }
    }
    #endregion

    #region skyway 물고기 관련 패스
    public void CheckClearPassAboutFish_Skyway(PublicDefined.stFishInfo fish)
    {
        Dictionary<int, int> dic;

        switch ((PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex)
        {
            case PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl, out dic);

                if (dic == null)
                {
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                }
                else if (dic.ContainsKey(fish._fishNumber))
                {
                    dic[fish._fishNumber]++;

                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                }
                else
                {
                    dic.Add(fish._fishNumber, 1);

                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                }

                //int count = 0; 
                //foreach(int data in dic.Values)
                //{
                //    count += data;
                //}

                //if (count > 2)
                //{
                //    //클리어
                //    //_userData._currentSkywayPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //    Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                //}
                break;
            case PublicDefined.eSkywayPass.vlzkthvltnlwkqrl:
                if (fish._fishNumber.Equals(82))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(82, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(vlzkthvltnlwkqrl)");
                }
                break;
            case PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl:
                if (fish._fishNumber.Equals(76))
                {
                    if (fish._length >= 35)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(76, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                       // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(rjfvmrkwkal35cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl:
                if (fish._fishNumber.Equals(96))
                {
                    if (fish._weight >= 20)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(96, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(Rhcltkacl20kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rhemddj5akflwkqrl:
                if (fish._fishNumber.Equals(83))
                {
                    _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(83, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    }
                    else
                    {
                        dic[83]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        if (dic[83] > 4)
                        {
                            // 클리어
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(rhemddj5akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl:
                if (fish._fishNumber.Equals(96) && _userData.GetCurrentEquipmentDictionary()["bait"].Equals(54))
                {
                    _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(96, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    }
                    else
                    {
                        dic[96]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        if (dic[96] > 4)
                        {
                            // 클리어
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(rhemddj5akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.qkddj2akflwkqrl:
                if (fish._fishNumber.Equals(80))
                {
                    _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.qkddj2akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(80, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qkddj2akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                       // Debug.Log("현재 방어 Count : " + dic[80]);
                    }
                    else
                    {
                        dic[80]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                       //Debug.Log("현재 방어 Count : " + dic[80]);

                        if (dic[80] > 1)
                        {
                            // 클리어
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(qkddj2akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.qntlfl2akflwkqrl:
                if (fish._fishNumber.Equals(98))
                {
                    _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(98, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //Debug.Log("현재 부시리 Count : " + dic[98]);
                    }
                    else
                    {
                        dic[98]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //Debug.Log("현재 부시리 Count : " + dic[98]);

                        if (dic[98] > 1)
                        {
                            // 클리어
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                           // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(qntlfl2akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl:
                if (fish._fishNumber.Equals(67))
                {
                    _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(67, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl] = dic;
                        //Debug.Log("현재 대서양레크피쉬 Count : " + dic[67]);

                    }
                    else
                    {
                        dic[67]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                       // Debug.Log("현재 대서양레크피쉬 Count : " + dic[67]);

                        if (dic[67] > 2)
                        {
                            // 클리어
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                          //  Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(eotjdidfpzmvltnl3akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl:
                if (fish._fishNumber.Equals(70))
                {
                    if (fish._length >= 40)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(70, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(rkatjdeha40cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl:
                if (fish._fishNumber.Equals(92))
                {
                    if (fish._length >= 50)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(92, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(didajfleha50cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(87))
                {
                    if (fish._length >= 70)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(87, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl] = dic;
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(thrdlarmasnseha70cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl:
                if (fish._fishNumber.Equals(84))
                {
                    if (fish._length > 120)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(84, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl] = dic;
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(akstorl120cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl:
                if (fish._fishNumber.Equals(81))
                {
                    if (fish._weight >= 10)
                    {
                        // 클리어
                        dic = new Dictionary<int, int>();
                        dic.Add(81, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(dhkdrhemddj10kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl:
                if (fish._fishNumber.Equals(69) || fish._fishNumber.Equals(66) || fish._fishNumber.Equals(79) || fish._fishNumber.Equals(86) || fish._fishNumber.Equals(89)
                    || fish._fishNumber.Equals(95))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eSkywayPass.wjrehawkqrl:
                if (fish._fishNumber.Equals(86))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(86, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl:
                if (fish._weight >= 30)
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어(anfrhrl30kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl:
                if (fish._fishNumber.Equals(69))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(69, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                }
                break;
            case PublicDefined.eSkywayPass.ehctoclwkqrl:
                if (fish._fishNumber.Equals(79))
                {
                    // 클리어
                    dic = new Dictionary<int, int>();
                    dic.Add(79, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region skyway 행동 관련 패스
    public void CheckClearPassAboutAction_Skyway()
    {
        switch ((PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex)
        {
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl:
                // 클리어: 해당 딕셔너리 1로 바꾸고 파이어베이스에 업데이트, 인덱스도 업데이트
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
               // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl:
                // 클리어
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
               // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr5akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 4)
                {
                    // 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                 //   Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                }
                break;
            case PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl:
                // 클리어
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
              // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr10akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 9)
                {
                    // 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                 //   Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                }
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr15akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 14)
                {
                    //// 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl:
                // 클리어
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr20akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 19)
                {
                    // 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                }
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr25akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 24)
                {
                    // 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                  //  Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl:
                // 클리어
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl]++;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr30akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 29)
                {
                    // 클리어
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "번 퀘스트 클리어");
                }
                break;
        }
        if (DataManager.INSTANCE._mapType == PublicDefined.eMapType.jeongdongjin || DataManager.INSTANCE._mapType == PublicDefined.eMapType.homerspit ||
            DataManager.INSTANCE._mapType == PublicDefined.eMapType.skyway)
        {
            SetCurrentQuest();
        }
    }
    #endregion

    string[] SetCurrentQuest_Jeongdongjin()
    {
        string[] content = new string[2];

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        //Debug.Log("현재 패스 인덱스 : " + _userData._currentJeongdongjinPassIndex);
        switch ((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl:
                content[0] = "정동진 물고기 3마리 잡기";
                if(_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                {
                    int count = 0;
                    foreach (int data in _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl].Values)
                    {
                        count += data;
                    }
                    content[1] = count + " / 3";
                }
                    
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl:
                content[0] = "수족관에 정동진 물고기 넣기";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl:
                content[0] = "수족관 들어가기";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tjdeowkqrl:
                content[0] = "성대 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl:
                content[0] = "정동진 기록 5마리 채우기";
                //content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl] + " / 5";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl:
                content[0] = "도다리 35cm 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl:
                content[0] = "정동진 미끼팩 열기";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl:
                content[0] = "조피볼락 40cm 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl:
                content[0] = "붕장어 3마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else 
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl][15] + " / 3";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl:
                content[0] = "정동진 기록 10마리 채우기";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count+ " / 10";
                break;
            case PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl:
                content[0] = "볼락/불볼락/개볼락 한마리씩 잡기";

                int thirteen = 0;
                int fourteen = 0;
                int one = 0;

                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl] == null)
                    thirteen = 0;
                else if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(13))
                    thirteen = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][13];
                else
                    thirteen = 0;

                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl] == null)
                    fourteen = 0;
                else if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(14))
                    fourteen = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][14];
                else
                    fourteen = 0;

                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl] == null)
                    one = 0;
                else if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(1))
                    one = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][1];
                else
                    one = 0;
                
                //if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(13))
                //    thirteen = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][13];
                //if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(14))
                //    fourteen = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][14];
                //if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].ContainsKey(1))
                //    one = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl][1];

                content[1] = "볼락 " + thirteen + " / 1 , 불볼락 " + fourteen + " / 1 , 개볼락 " + one + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl:
                content[0] = "무늬오징어 5마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl][9] + " / 5";
                break;
            case PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl:
                content[0] = "넙치 1.5kg 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl:
                content[0] = "고등어 20마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl][2] + " / 20";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl:
                content[0] = "정동진 기록 15마리 채우기";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl:
                content[0] = "임연수어 50cm 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "수족관에서 물고기 팔기";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl:
                content[0] = "학공치 20마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl][30] + " / 20";

                break;
            case PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl:
                content[0] = "양태 70cm 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl:
                content[0] = "정동진 기록 20마리 채우기";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl:
                content[0] = "루어 낚시로 황어 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl:
                content[0] = "숭어 3kg 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "희귀어종 1마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl:
                content[0] = "에기를 이용해 무늬오징어 10마리 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl] == null)
                    content[1] = "0 / 10";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl][9] + " / 10";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl:
                content[0] = "정동진 기록 25마리 채우기";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl:
                content[0] = "물고기 5kg 이상 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.rkatjdehawkqrl:
                content[0] = "감성돔 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl:
                content[0] = "폽퍼를 이용해 방어 잡기";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl:
                content[0] = "수족관에 정동진 희귀 물고기 넣기";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl:
                content[0] = "정동진 기록 30마리 채우기";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 30";
                break;
        }

        return content;
    }
    string[] SetCurrentQuest_Skyway()
    {
        string[] content = new string[2];

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        switch ((PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex)
        {
            case PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl:
                content[0] = "스카이웨이 물고기 3마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                {
                    int count = 0;
                    foreach (int data in _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl].Values)
                    {
                        count += data;
                    }
                    content[1] = count + " / 3";
                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl:
                content[0] = "수족관에 스카이웨이 물고기 넣기";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl:
                content[0] = "수족관 들어가기";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.vlzkthvltnlwkqrl:
                content[0] = "피카소피쉬 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr5akflcodnrl:
                content[0] = "스카이웨이 기록 5마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl:
                content[0] = "걸프가자미 35cm 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl:
                content[0] = "장구릴 장착하기";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl:
                content[0] = "꼬치삼치 20kg 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.rhemddj5akflwkqrl:
                content[0] = "고등어 5마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl][83] + " / 5";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr10akflcodnrl:
                content[0] = "스카이웨이 기록 10마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 10";
                break;
            case PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl:
                content[0] = "플로팅미노우를 이용해 꼬치삼치 5마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl][96] + " / 5";
                break;
            case PublicDefined.eSkywayPass.qkddj2akflwkqrl:
                content[0] = "방어 2마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qkddj2akflwkqrl] == null)
                    content[1] = "0 / 2";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qkddj2akflwkqrl][80] + " / 2";
                break;
            case PublicDefined.eSkywayPass.qntlfl2akflwkqrl:
                content[0] = "부시리 2마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl] == null)
                    content[1] = "0 / 2";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl][98] + " / 2";
                break;
            case PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl:
                content[0] = "대서양레크피쉬 3마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl][67] + " / 3";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr15akflcodnrl:
                content[0] = "스카이웨이 기록 15마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl:
                content[0] = "감성돔 40cm 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "수족관에서 물고기 팔기";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl:
                content[0] = "양머리돔 50cm 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl][92] + " / 1";
                break;
            case PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl:
                content[0] = "속임금눈돔 70cm 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr20akflcodnrl:
                content[0] = "스카이웨이 기록 20마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl:
                content[0] = "만새기 120cm 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl:
                content[0] = "왕고등어 10kg 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "희귀어종 1마리 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.wjrehawkqrl:
                content[0] = "적돔 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr25akflcodnrl:
                content[0] = "스카이웨이 기록 25마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl:
                content[0] = "물고기 30kg 이상 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl:
                content[0] = "블랙그루퍼 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.ehctoclwkqrl:
                content[0] = "돛새치 잡기";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl:
                content[0] = "수족관에 스카이웨이 희귀 물고기 넣기";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr30akflcodnrl:
                content[0] = "스카이웨이 기록 30마리 채우기";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 30";
                break;
        }
        return content;
    }
    string[] SetCurrentQuest_Homerspit()
    {
        string[] content = new string[2];

        if (_userData == null)
            _userData = DBManager.INSTANCE.GetUserData();

        switch ((PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex)
        {
            case PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl:
                content[0] = "호머스핏 물고기 3마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                {
                    int count = 0;
                    foreach (int data in _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl].Values)
                    {
                        count += data;
                    }
                    content[1] = count + " / 3";
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl:
                content[0] = "수족관에 호머스핏 물고기 넣기";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl:
                content[0] = "수족관 들어가기";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl:
                content[0] = "날개줄고기 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr5akflcodnrl:
                content[0] = "호머스핏 기록 5마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl:
                content[0] = "노랑각시서대 14cm 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl:
                content[0] = "호머스핏 미끼팩 열기";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl:
                content[0] = "은대구 70cm 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl:
                content[0] = "장문볼락 3마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl][50] + " / 3";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr10akflcodnrl:
                content[0] = "호머스핏 기록 10마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 10";
                break;
            case PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl:
                content[0] = "화살치, 마소치, 각시가자미 한마리씩 잡기";

                int fourtyfive = 0;
                int fourtyone = 0;
                int sixtytwo = 0;

                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl] == null)
                    fourtyfive = 0;
                else if(_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(45))
                    fourtyfive = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][45];
                else
                    fourtyfive = 0;
                
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl] == null)
                    fourtyone = 0;
                else if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(41))
                    fourtyone = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][41];
                else
                    fourtyone = 0;

                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl] == null)
                    sixtytwo = 0;
                else if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(62))
                    sixtytwo = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][62];
                else
                    sixtytwo = 0;

                //if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(45))
                //    fourtyfive = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][45];
                //if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(41))
                //    fourtyone = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][41];
                //if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].ContainsKey(62))
                //    sixtytwo = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl][62];

                content[1] = fourtyfive + " / 1 , " + fourtyone + " / 1 , " + sixtytwo + " / 1";
                break;
            case PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl:
                content[0] = "자붉돔 5마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl][46] + " / 5";
                break;
            case PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl:
                content[0] = "빛금눈돔 1kg 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl:
                content[0] = "정어리 20마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl][60] + " / 20";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr15akflcodnrl:
                content[0] = "호머스핏 기록 15마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl:
                content[0] = "연어 70cm 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "수족관에서 물고기 팔기";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl:
                content[0] = "꽁치 20마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl][51] + " / 20";

                break;
            case PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl:
                content[0] = "대구 75cm 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr20akflcodnrl:
                content[0] = "호머스핏 기록 20마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl:
                content[0] = "루어 낚시로 은대구 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl:
                content[0] = "명태 3kg 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "희귀어종 1마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl:
                content[0] = "싱킹미노우를 이용해 명태 10마리 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl] == null)
                    content[1] = "0 / 10";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl][32] + " / 10";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr25akflcodnrl:
                content[0] = "호머스핏 기록 25마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl:
                content[0] = "물고기 10kg 이상 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.dkrtkddjwkqrl:
                content[0] = "악상어 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl:
                content[0] = "폽퍼를 이용해 황다랑어 잡기";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl:
                content[0] = "수족관에 호머스핏 희귀 물고기 넣기";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr30akflcodnrl:
                content[0] = "호머스핏 기록 30마리 채우기";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 30";
                break;
        }
        return content;
    }

    // 물고기, 액션 모두를 체크한다. 인게임 매니저에서 최초로 들어왔을 때 확인하기 위함
    void CheckJeongdongjin()
    {
        Dictionary<int, int> dic;
        bool isClear = false;

        switch ((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl, out dic);

                if (dic != null)
                {
                    int count = 0;
                    foreach (int data in dic.Values)
                    {
                        count += data;
                    }
                    if (count > 2)
                    {
                        //클리어
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.tjdeowkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.tjdeowkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl:

                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(15))
                    {
                        if (dic[15] >= 3)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count >= 3)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(9))
                    {
                        if (dic[9] >= 5)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(2))
                    {
                        if (dic[2] >= 20)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(30))
                    {
                        if (dic[30] >= 20)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(9))
                    {
                        if (dic[9] >= 10)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.rkatjdehawkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl:
                _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }

                }
                isClear = false;
                break;
            // ============================================================액션 관련==================================================================
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl:
                if (_currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl:
                if (_currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 4)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl:
                if (_currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 9)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 14)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl:
                if (_currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 19)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 24)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl:
                if (_currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 29)
                    isClear = true;
                break;
            default:
                break;
        }

        _ingameUI.QuestClear(isClear);
    }
    void CheckSkyway()
    {
        Dictionary<int, int> dic;
        bool isClear = false;

        switch ((PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex)
        {
            case PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.tmzkdldnpdlanfrhrl3akflwkqrl, out dic);

                if (dic != null)
                {
                    int count = 0;
                    foreach (int data in dic.Values)
                    {
                        count += data;
                    }
                    if (count > 2)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.vlzkthvltnlwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rhemddj5akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(83))
                    {
                        if(dic[83] >= 5)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(96))
                    {
                        if (dic[96] >= 5)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.qkddj2akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.qkddj2akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(80))
                    {
                        if (dic[80] >= 2)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.qntlfl2akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(98))
                    {
                        if (dic[98] >= 2)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(67))
                    {
                        if (dic[67] >= 3)
                        {
                            isClear = true;
                            break;
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.wjrehawkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.wjrehawkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.ehctoclwkqrl:
                _currentStateOfSkywayPassAboutFish.TryGetValue((int)PublicDefined.eSkywayPass.ehctoclwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl:
                if (_currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl:
                if (_currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr5akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 4)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl:
                if (_currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr10akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 9)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr15akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 14)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl:
                if (_currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr20akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 19)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr25akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 24)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl:
                if (_currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr30akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 29)
                    isClear = true;
                break;
            default:
                break;
        }
        _ingameUI.QuestClear(isClear);
    }
    void CheckHomerspit()
    {
        Dictionary<int, int> dic;
        bool isClear = false;

        switch ((PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex)
        {
            case PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.ghajtmvltanfrhrl3akflwkqrl, out dic);

                if (dic != null)
                {
                    int count = 0;
                    foreach (int data in dic.Values)
                    {
                        count += data;
                    }
                    if (count > 2)
                    {
                        //클리어
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(50))
                    {
                        if(dic[50] >= 3)
                        {
                            isClear = true;
                            break;
                        }

                    }
                }
                break;
            case PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count >= 3)
                    {
                        isClear = true;
                        break;
                    }
                }
                //if (fish._fishNumber.Equals(41) || fish._fishNumber.Equals(45) || fish._fishNumber.Equals(62))
                break;
            case PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(46))
                    {
                        if (dic[46] >= 5)
                        {
                            isClear = true;
                            break;
                        }

                    }
                }
                break;
            case PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(60))
                    {
                        if (dic[60] >= 20)
                        {
                            isClear = true;
                            break;
                        }

                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(51))
                    {
                        if (dic[51] >= 20)
                        {
                            isClear = true;
                            break;
                        }

                    }
                }
                break;
            case PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                isClear = false;
                break;
            case PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.ContainsKey(32))
                    {
                        if (dic[32] >= 10)
                        {
                            isClear = true;
                            break;
                        }

                    }
                }
                break;
            case PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dkrtkddjwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl:
                _currentStateOfHomerspitPassAboutFish.TryGetValue((int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl, out dic);

                if (dic != null)
                {
                    if (dic.Count > 0)
                    {
                        isClear = true;
                        break;
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl:
                if (_currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl:
                if (_currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr5akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 4)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl:
                if (_currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr10akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 9)
                {
                    isClear = true;
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr15akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 14)
                {
                    isClear = true;
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl:
                if (_currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr20akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 19)
                {
                    isClear = true;
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr25akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 24)
                {
                    isClear = true;
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl:
                if (_currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl] > 0)
                    isClear = true;
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr30akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 29)
                {
                    isClear = true;
                }
                break;
            default:
                break;
        }
        _ingameUI.QuestClear(isClear);
    }
}
