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

    #region jeongdongjin ����� ���� �н�
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
                //    //Ŭ����
                //    //_userData._currentJeongdongjinPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //    //CheckJeongdongjin();
                //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                //}
                break;
            case PublicDefined.eJeongdongjinPass.tjdeowkqrl:
                if(fish._fishNumber.Equals(18))
                {
                    // Ŭ����, ��ųʸ� ���� �ؾ��ϳ� ������ �ϴ� �غ��� ���߿� ���� �Ǹ� ����.
                    dic = new Dictionary<int, int>();
                    dic.Add(18, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(tjdeowkqrl)");

                }
                break;
            case PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl:
                if (fish._fishNumber.Equals(6))
                {
                    if (fish._length >= 35)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(6, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(ehekfl35cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl:
                if (fish._fishNumber.Equals(25))
                {
                    if (fish._length >= 40)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(25, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(whvlqhffkr40cmdltkdwkqrl)");

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
                        //Debug.Log("���� ����� Count : " + dic[15]);
                    }
                    else
                    {
                        dic[15]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("���� ����� Count : " + dic[15]);
                        //if (dic[fish._fishNumber] > 2)
                        //{
                        //    // Ŭ����
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(qndwkddj3akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl:
                if(fish._fishNumber.Equals(1) || fish._fishNumber.Equals(14) || fish._fishNumber.Equals(13))
                {
                    // ���� ����, �Һ���, ������ �� �Ѹ����� �����ٸ� ���´�.
                    _currentStateOfJeongdongjinPassAboutFish.TryGetValue((int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl, out dic);

                    if (dic == null)
                    {
                        dic = new Dictionary<int, int>();
                        dic.Add(fish._fishNumber, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("���� ����,�Һ���,������ Count : " + _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl].Count);

                    }
                    else if(!dic.ContainsKey(fish._fishNumber))
                    {
                        dic.Add(fish._fishNumber, 1);
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //if (dic.Count.Equals(3))
                        //{
                        //    // Ŭ����
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(ī��Ʈ�� 3�� �Ǹ� Ŭ����ȰŴ�.)");

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
                       // Debug.Log("���� ���̿�¡�� Count : " + dic[9]);
                    }
                    else
                    {
                        dic[9]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //Debug.Log("���� ���̿�¡�� Count : " + dic[9]);
                    
                        //if (dic[9] > 4)
                        //{
                        //    // Ŭ����
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(ansmldhwlddj5akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl:
                if (fish._fishNumber.Equals(3))
                {
                    if (fish._weight >= 1.5f)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(3, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(��ġ1.5kg�̻����)");
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
                        //Debug.Log("���� ���� Count : " + dic[2]);
                    }
                    else
                    {
                        dic[2]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                       // Debug.Log("���� ���� Count : " + dic[2]);

                        //if (dic[2] > 19)
                        //{
                        //    // Ŭ����
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(rhemddj20akflwkqrl)");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl:
                if (fish._fishNumber.Equals(21))
                {
                    if (fish._length >= 50)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(21, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(dladustndj50cmdltkdwkqrl)");
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
                        //Debug.Log("���� �а�ġ Count : " + dic[30]);
                    }
                    else
                    {
                        dic[30]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                       // Debug.Log("���� �а�ġ Count : " + dic[30]);

                        //if (dic[30] > 19)
                        //{
                        //    // Ŭ����
                        //    //_userData._currentJeongdongjinPassIndex++;
                        //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //    //CheckJeongdongji();
                        //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                        //}
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(20))
                {
                    if (fish._length >= 70)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(20, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(didxo70cmdltkdwkqrl)");
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
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(31, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(fndjskRtlfhghkddjwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl:
                if (fish._fishNumber.Equals(19))
                {
                    if (fish._weight >= 3)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(19, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                        //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(tnddj3kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl:
                if(fish._fishNumber.Equals(99) || fish._fishNumber.Equals(5) || fish._fishNumber.Equals(10) || fish._fishNumber.Equals(11) || fish._fishNumber.Equals(16) 
                    || fish._fishNumber.Equals(24))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
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
                            //Debug.Log("���� ���̿�¡�� Count : " + dic[9]);
                        }
                        else
                        {
                            dic[9]++;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                            //Debug.Log("���� ���̿�¡�� Count : " + dic[9]);
                            //if (dic[9].Equals(10))
                            //{
                            //    // Ŭ����
                            //    //_userData._currentJeongdongjinPassIndex++;
                            //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                            //    //CheckJeongdongji();
                            //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(dprlfmfdldydgoansmldhwlddj10akflwkqrl)");
                                
                            //}
                        }
                    }
                }
                break;
            case PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl:
                if(fish._weight >= 5)
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(anfrhrl5kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eJeongdongjinPass.rkatjdehawkqrl:
                if(fish._fishNumber.Equals(99))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(99, 1);
                    _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongji();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(rkatjdehawkqrl)");
                }
                break;
            case PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl:
                if(fish._fishNumber.Equals(10))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(53))
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(10, 1);
                        _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.jeongdongjin);
                        //_userData._currentJeongdongjinPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                        //CheckJeongdongji();
                       // Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����(vhqvjfmfdldydgoqkddjwkqrl)");
                        
                    }
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region jeongdongjin �ൿ ���� �н�
    public void CheckClearPassAboutAction_Jeongdongjin()
    {
        switch((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl:
                // Ŭ����: �ش� ��ųʸ� 1�� �ٲٰ� ���̾�̽��� ������Ʈ, �ε����� ������Ʈ
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);

                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl:
                // Ŭ����
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl:
                //if(_userData.GetJeongdongjinRankDictionary().Count > 4)
                //{
                //    // Ŭ����
                //    //_userData._currentJeongdongjinPassIndex= 1;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //    CheckJeongdongjin();
                //    Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                //}

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl:
                // Ŭ����
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 9)
                {
                    // Ŭ����
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 14)
                {
                    // Ŭ����
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                   // Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl:
                // Ŭ����
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex= 1;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 19)
                {
                    // Ŭ����
                    //_userData._currentJeongdongjinPassIndex= 1;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 24)
                {
                    // Ŭ����
                    //_userData._currentJeongdongjinPassIndex= 1;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl:
                // Ŭ����
                _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl]= 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.jeongdongjin);
                //_userData._currentJeongdongjinPassIndex= 1;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                //CheckJeongdongjin();
                //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl:
                if (_userData.GetJeongdongjinRankDictionary().Count > 29)
                {
                    // Ŭ����
                    //_userData._currentJeongdongjinPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.jeongdongjin);
                    //CheckJeongdongjin();
                    //Debug.Log(_userData._currentJeongdongjinPassIndex + "�� ����Ʈ Ŭ����");
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

    #region homerspit ����� ���� �н�
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
                //    //Ŭ����
                //    //_userData._currentHomerspitPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //    Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
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
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(skfrownfrhrlwkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl:
                if (fish._fishNumber.Equals(48))
                {
                    if (fish._length >= 14)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(48, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                       // Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(shfkdrkrtltjeo14cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(55))
                {
                    if (fish._length >= 70)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(55, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(dmseorn70cmdltkdwkqrl)");
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
                            // Ŭ����
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(wkdansqhffkr3akflwkqrl)");
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
                        //Debug.Log("���� ȭ��ġ,����ġ,���� �� Count : " + _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl].Count);

                    }
                    else if (!dic.ContainsKey(fish._fishNumber))
                    {
                        dic.Add(fish._fishNumber, 1);
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        if (dic.Count.Equals(3))
                        {
                            // Ŭ����
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(ī��Ʈ�� 3�� �Ǹ� Ŭ����ȰŴ�.)");
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
                        //Debug.Log("���� �ںӵ� Count : " + dic[46]);

                    }
                    else
                    {
                        dic[46]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("���� �ںӵ� Count : " + dic[46]);

                        if (dic[46] > 4)
                        {
                            // Ŭ����
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(wkqnfreha5akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl:
                if (fish._fishNumber.Equals(59))
                {
                    if (fish._weight >= 1)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(59, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(qlwrmasnseha1kgdltkdwkqrl)");
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
                        //Debug.Log("���� ��� Count : " + dic[60]);
                    }
                    else
                    {
                        dic[60]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("���� ��� Count : " + dic[60]);

                        if (dic[60] > 19)
                        {
                            // Ŭ����
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(wjddjfl20akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(39))
                {
                    if (fish._length >= 70)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(39, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(dusdj70cmdltkdwkqrl)");
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
                        //Debug.Log("���� ��ġ Count : " + dic[51]);
                    }
                    else
                    {
                        dic[51]++; 
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //Debug.Log("���� ��ġ Count : " + dic[51]);

                        if (dic[51] > 19)
                        {
                            //// Ŭ����
                            //_userData._currentHomerspitPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                            //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl:
                if (fish._fishNumber.Equals(49))
                {
                    if (fish._length >= 75)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(49, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(eorn75cmdltkdwkqrl)");
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
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(55, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(fndjskRtlfhdmseornwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl:
                if (fish._fishNumber.Equals(32))
                {
                    if (fish._weight >= 3)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(32, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(audxo3kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl:
                if (fish._fishNumber.Equals(34) || fish._fishNumber.Equals(38) || fish._fishNumber.Equals(43) || fish._fishNumber.Equals(47) || fish._fishNumber.Equals(52)
                    || fish._fishNumber.Equals(57) || fish._fishNumber.Equals(61) || fish._fishNumber.Equals(63))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
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
                            //Debug.Log("���� ���� Count : " + dic[32]);
                        }
                        else
                        {
                            dic[32]++;
                            DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);

                            if (dic[32] > 9)
                            {
                                // Ŭ����
                                //_userData._currentHomerspitPassIndex++;
                                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                                //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(tldzldalshdnfmfdldydgoaudxo10akflwkqrl)");
                            }
                        }
                    }
                }
                break;
            case PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl:
                if (fish._weight >= 10)
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(anfrhrl10kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.dkrtkddjwkqrl:
                if (fish._fishNumber.Equals(57))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(57, 1);
                    _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(rkatjdehawkqrl)");
                }
                break;
            case PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl:
                if (fish._fishNumber.Equals(63))
                {
                    int baitNumber = _userData.GetCurrentEquipmentDictionary()["bait"];
                    if (baitNumber.Equals(53))
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(63, 1);
                        _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.homerspit);
                        //_userData._currentHomerspitPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                        //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����(vhqvjfmfdldydgoghkdekfkddjwkqrl)");
                    }
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region homerspit �ൿ ���� �н�
    public void CheckClearPassAboutAction_Homerspit()
    {
        switch ((PublicDefined.eHomerspitPass)_userData._currentHomerspitPassIndex)
        {
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl:
                // Ŭ����: �ش� ��ųʸ� 1�� �ٲٰ� ���̾�̽��� ������Ʈ, �ε����� ������Ʈ
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex + "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl:
                // Ŭ����
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr5akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 4)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl:
                // Ŭ����
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
               //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr10akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 9)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr15akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 14)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl:
                // Ŭ����
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr20akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 19)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr25akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 24)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl:
                // Ŭ����
                _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.homerspit);
                //_userData._currentHomerspitPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr30akflcodnrl:
                if (_userData.GetHomerspitRankDictionary().Count > 29)
                {
                    // Ŭ����
                    //_userData._currentHomerspitPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.homerspit);
                    //Debug.Log(_userData._currentHomerspitPassIndex+ "�� ����Ʈ Ŭ����");
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

    #region skyway ����� ���� �н�
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
                //    //Ŭ����
                //    //_userData._currentSkywayPassIndex++;
                //    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //    Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                //}
                break;
            case PublicDefined.eSkywayPass.vlzkthvltnlwkqrl:
                if (fish._fishNumber.Equals(82))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(82, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(vlzkthvltnlwkqrl)");
                }
                break;
            case PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl:
                if (fish._fishNumber.Equals(76))
                {
                    if (fish._length >= 35)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(76, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                       // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(rjfvmrkwkal35cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl:
                if (fish._fishNumber.Equals(96))
                {
                    if (fish._weight >= 20)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(96, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(Rhcltkacl20kgdltkdwkqrl)");
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
                            // Ŭ����
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(rhemddj5akflwkqrl)");
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
                            // Ŭ����
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(rhemddj5akflwkqrl)");
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
                       // Debug.Log("���� ��� Count : " + dic[80]);
                    }
                    else
                    {
                        dic[80]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                       //Debug.Log("���� ��� Count : " + dic[80]);

                        if (dic[80] > 1)
                        {
                            // Ŭ����
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                            //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(qkddj2akflwkqrl)");
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
                        //Debug.Log("���� �νø� Count : " + dic[98]);
                    }
                    else
                    {
                        dic[98]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //Debug.Log("���� �νø� Count : " + dic[98]);

                        if (dic[98] > 1)
                        {
                            // Ŭ����
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                           // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(qntlfl2akflwkqrl)");
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
                        //Debug.Log("���� �뼭�緹ũ�ǽ� Count : " + dic[67]);

                    }
                    else
                    {
                        dic[67]++;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                       // Debug.Log("���� �뼭�緹ũ�ǽ� Count : " + dic[67]);

                        if (dic[67] > 2)
                        {
                            // Ŭ����
                            //_userData._currentSkywayPassIndex++;
                            //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                          //  Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(eotjdidfpzmvltnl3akflwkqrl)");
                        }
                    }
                }
                break;
            case PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl:
                if (fish._fishNumber.Equals(70))
                {
                    if (fish._length >= 40)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(70, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(rkatjdeha40cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl:
                if (fish._fishNumber.Equals(92))
                {
                    if (fish._length >= 50)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(92, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(didajfleha50cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl:
                if (fish._fishNumber.Equals(87))
                {
                    if (fish._length >= 70)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(87, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl] = dic;
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(thrdlarmasnseha70cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl:
                if (fish._fishNumber.Equals(84))
                {
                    if (fish._length > 120)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(84, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl] = dic;
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(akstorl120cmdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl:
                if (fish._fishNumber.Equals(81))
                {
                    if (fish._weight >= 10)
                    {
                        // Ŭ����
                        dic = new Dictionary<int, int>();
                        dic.Add(81, 1);
                        _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl] = dic;
                        DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                        //_userData._currentSkywayPassIndex++;
                        //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                        //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(dhkdrhemddj10kgdltkdwkqrl)");
                    }
                }
                break;
            case PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl:
                if (fish._fishNumber.Equals(69) || fish._fishNumber.Equals(66) || fish._fishNumber.Equals(79) || fish._fishNumber.Equals(86) || fish._fishNumber.Equals(89)
                    || fish._fishNumber.Equals(95))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eSkywayPass.wjrehawkqrl:
                if (fish._fishNumber.Equals(86))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(86, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl:
                if (fish._weight >= 30)
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(fish._fishNumber, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����(anfrhrl30kgdltkdwkqrl)");
                }
                break;
            case PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl:
                if (fish._fishNumber.Equals(69))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(69, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                    //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            case PublicDefined.eSkywayPass.ehctoclwkqrl:
                if (fish._fishNumber.Equals(79))
                {
                    // Ŭ����
                    dic = new Dictionary<int, int>();
                    dic.Add(79, 1);
                    _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl] = dic;
                    DBManager.INSTANCE.UpdatePassAboutFish(PublicDefined.eMapType.skyway);
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                }
                break;
            default:
                break;
        }
        SetCurrentQuest();
    }
    #endregion
    #region skyway �ൿ ���� �н�
    public void CheckClearPassAboutAction_Skyway()
    {
        switch ((PublicDefined.eSkywayPass)_userData._currentSkywayPassIndex)
        {
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl:
                // Ŭ����: �ش� ��ųʸ� 1�� �ٲٰ� ���̾�̽��� ������Ʈ, �ε����� ������Ʈ
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
               // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl:
                // Ŭ����
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
               // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr5akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 4)
                {
                    // Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                 //   Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                }
                break;
            case PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl:
                // Ŭ����
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
              // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr10akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 9)
                {
                    // Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                 //   Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                }
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr15akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 14)
                {
                    //// Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl:
                // Ŭ����
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl] = 1;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr20akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 19)
                {
                    // Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                }
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr25akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 24)
                {
                    // Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                  //  Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                }
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl:
                // Ŭ����
                _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl]++;
                DBManager.INSTANCE.UpdatePassAboutAction(PublicDefined.eMapType.skyway);
                //_userData._currentSkywayPassIndex++;
                //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                //Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");

                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr30akflcodnrl:
                if (_userData.GetSkywayRankDictionary().Count > 29)
                {
                    // Ŭ����
                    //_userData._currentSkywayPassIndex++;
                    //DBManager.INSTANCE.UpdatePassIndex(PublicDefined.eMapType.skyway);
                   // Debug.Log(_userData._currentSkywayPassIndex+ "�� ����Ʈ Ŭ����");
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

        //Debug.Log("���� �н� �ε��� : " + _userData._currentJeongdongjinPassIndex);
        switch ((PublicDefined.eJeongdongjinPass)_userData._currentJeongdongjinPassIndex)
        {
            case PublicDefined.eJeongdongjinPass.wjdehdwlsanfrhrl3akflwkqrl:
                content[0] = "������ ����� 3���� ���";
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
                content[0] = "�������� ������ ����� �ֱ�";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl:
                content[0] = "������ ����";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tjdeowkqrl:
                content[0] = "���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tjdeowkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl:
                content[0] = "������ ��� 5���� ä���";
                //content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr5akflcodnrl] + " / 5";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl:
                content[0] = "���ٸ� 35cm �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ehekfl35cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl:
                content[0] = "������ �̳��� ����";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl:
                content[0] = "���Ǻ��� 40cm �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.whvlqhffkr40cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl:
                content[0] = "����� 3���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else 
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.qndwkddj3akflwkqrl][15] + " / 3";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr10akflcodnrl:
                content[0] = "������ ��� 10���� ä���";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count+ " / 10";
                break;
            case PublicDefined.eJeongdongjinPass.qhffkr_qnfqhffkr_roqhffkrgksakflTlrwkqrl:
                content[0] = "����/�Һ���/������ �Ѹ����� ���";

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

                content[1] = "���� " + thirteen + " / 1 , �Һ��� " + fourteen + " / 1 , ������ " + one + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl:
                content[0] = "���̿�¡�� 5���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.ansmldhwlddj5akflwkqrl][9] + " / 5";
                break;
            case PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl:
                content[0] = "��ġ 1.5kg �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.sjqcl1_5kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl:
                content[0] = "���� 20���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rhemddj20akflwkqrl][2] + " / 20";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr15akflcodnrl:
                content[0] = "������ ��� 15���� ä���";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl:
                content[0] = "�ӿ����� 50cm �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dladustndj50cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "���������� ����� �ȱ�";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl:
                content[0] = "�а�ġ 20���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gkrrhdcl20akflwkqrl][30] + " / 20";

                break;
            case PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl:
                content[0] = "���� 70cm �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.didxo70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr20akflcodnrl:
                content[0] = "������ ��� 20���� ä���";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl:
                content[0] = "��� ���÷� Ȳ�� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.fndjskRtlfhghkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl:
                content[0] = "���� 3kg �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.tnddj3kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "��;��� 1���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl:
                content[0] = "���⸦ �̿��� ���̿�¡�� 10���� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl] == null)
                    content[1] = "0 / 10";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.dprlfmfdldydgoansmldhwlddj10akflwkqrl][9] + " / 10";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr25akflcodnrl:
                content[0] = "������ ��� 25���� ä���";
                content[1] = _userData.GetJeongdongjinRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl:
                content[0] = "����� 5kg �̻� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.anfrhrl5kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.rkatjdehawkqrl:
                content[0] = "������ ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.rkatjdehawkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl:
                content[0] = "���۸� �̿��� ��� ���";
                if (_currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfJeongdongjinPassAboutFish[(int)PublicDefined.eJeongdongjinPass.vhqvjfmfdldydgoqkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl:
                content[0] = "�������� ������ ��� ����� �ֱ�";
                content[1] = _currentStateOfJeongdongjinPassAboutAction[(int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eJeongdongjinPass.wjdehdwlsrlfhr30akflcodnrl:
                content[0] = "������ ��� 30���� ä���";
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
                content[0] = "��ī�̿��� ����� 3���� ���";
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
                content[0] = "�������� ��ī�̿��� ����� �ֱ�";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl:
                content[0] = "������ ����";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.vlzkthvltnlwkqrl:
                content[0] = "��ī���ǽ� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vlzkthvltnlwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr5akflcodnrl:
                content[0] = "��ī�̿��� ��� 5���� ä���";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl:
                content[0] = "�������ڹ� 35cm �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rjfvmrkwkal35cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl:
                content[0] = "�屸�� �����ϱ�";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.wkdrnflfwkdckrgkrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl:
                content[0] = "��ġ��ġ 20kg �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.Rhcltkacl20kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.rhemddj5akflwkqrl:
                content[0] = "���� 5���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rhemddj5akflwkqrl][83] + " / 5";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr10akflcodnrl:
                content[0] = "��ī�̿��� ��� 10���� ä���";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 10";
                break;
            case PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl:
                content[0] = "�÷��ù̳�츦 �̿��� ��ġ��ġ 5���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.vmffhxldalshdnfmfdldydgoRhcltkacl5akflwkqrl][96] + " / 5";
                break;
            case PublicDefined.eSkywayPass.qkddj2akflwkqrl:
                content[0] = "��� 2���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qkddj2akflwkqrl] == null)
                    content[1] = "0 / 2";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qkddj2akflwkqrl][80] + " / 2";
                break;
            case PublicDefined.eSkywayPass.qntlfl2akflwkqrl:
                content[0] = "�νø� 2���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl] == null)
                    content[1] = "0 / 2";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qntlfl2akflwkqrl][98] + " / 2";
                break;
            case PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl:
                content[0] = "�뼭�緹ũ�ǽ� 3���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.eotjdidfpzmvltnl3akflwkqrl][67] + " / 3";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr15akflcodnrl:
                content[0] = "��ī�̿��� ��� 15���� ä���";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl:
                content[0] = "������ 40cm �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.rkatjdeha40cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "���������� ����� �ȱ�";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl:
                content[0] = "��Ӹ��� 50cm �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.didajfleha50cmdltkdwkqrl][92] + " / 1";
                break;
            case PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl:
                content[0] = "���ӱݴ��� 70cm �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.thrdlarmasnseha70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr20akflcodnrl:
                content[0] = "��ī�̿��� ��� 20���� ä���";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl:
                content[0] = "������ 120cm �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.akstorl120cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl:
                content[0] = "�հ��� 10kg �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.dhkdrhemddj10kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "��;��� 1���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.wjrehawkqrl:
                content[0] = "���� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.wjrehawkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr25akflcodnrl:
                content[0] = "��ī�̿��� ��� 25���� ä���";
                content[1] = _userData.GetSkywayRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl:
                content[0] = "����� 30kg �̻� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.anfrhrl30kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl:
                content[0] = "���׷��� ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.qmfforrmfnvjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.ehctoclwkqrl:
                content[0] = "����ġ ���";
                if (_currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfSkywayPassAboutFish[(int)PublicDefined.eSkywayPass.ehctoclwkqrl].Count + " / 1";
                break;
            case PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl:
                content[0] = "�������� ��ī�̿��� ��� ����� �ֱ�";
                content[1] = _currentStateOfSkywayPassAboutAction[(int)PublicDefined.eSkywayPass.tnwhrrhksdptmzkdldnpdlgmlrnldjwhdsjgrl] + " / 1";
                break;
            case PublicDefined.eSkywayPass.tmzkdldnpdlrlfhr30akflcodnrl:
                content[0] = "��ī�̿��� ��� 30���� ä���";
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
                content[0] = "ȣ�ӽ��� ����� 3���� ���";
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
                content[0] = "�������� ȣ�ӽ��� ����� �ֱ�";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl:
                content[0] = "������ ����";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksemfdjrkrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl:
                content[0] = "�����ٰ�� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.skfrownfrhrlwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr5akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 5���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 5";
                break;
            case PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl:
                content[0] = "������ü��� 14cm �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.shfkdrkrtltjeo14cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl:
                content[0] = "ȣ�ӽ��� �̳��� ����";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.ghajtmvltalRlvordufrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl:
                content[0] = "���뱸 70cm �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dmseorn70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl:
                content[0] = "�幮���� 3���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl] == null)
                    content[1] = "0 / 3";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkdansqhffkr3akflwkqrl][50] + " / 3";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr10akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 10���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 10";
                break;
            case PublicDefined.eHomerspitPass.ghktkfcl_akthcl_rkrtlrkwkalgksakflTlrwkqrl:
                content[0] = "ȭ��ġ, ����ġ, ���ð��ڹ� �Ѹ����� ���";

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
                content[0] = "�ںӵ� 5���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl] == null)
                    content[1] = "0 / 5";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wkqnfreha5akflwkqrl][46] + " / 5";
                break;
            case PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl:
                content[0] = "���ݴ��� 1kg �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.qlwrmasnseha1kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl:
                content[0] = "��� 20���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.wjddjfl20akflwkqrl][60] + " / 20";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr15akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 15���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 15";
                break;
            case PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl:
                content[0] = "���� 70cm �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dusdj70cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl:
                content[0] = "���������� ����� �ȱ�";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdptjanfrhrlvkfrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl:
                content[0] = "��ġ 20���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl] == null)
                    content[1] = "0 / 20";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.Rhdcl20akflwkqrl][51] + " / 20";

                break;
            case PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl:
                content[0] = "�뱸 75cm �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.eorn75cmdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr20akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 20���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 20";
                break;
            case PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl:
                content[0] = "��� ���÷� ���뱸 ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.fndjskRtlfhdmseornwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl:
                content[0] = "���� 3kg �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.audxo3kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl:
                content[0] = "��;��� 1���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.gmlrnldjwhd1akflwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl:
                content[0] = "��ŷ�̳�츦 �̿��� ���� 10���� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl] == null)
                    content[1] = "0 / 10";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.tldzldalshdnfmfdldydgoaudxo10akflwkqrl][32] + " / 10";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr25akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 25���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 25";
                break;
            case PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl:
                content[0] = "����� 10kg �̻� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.anfrhrl10kgdltkdwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.dkrtkddjwkqrl:
                content[0] = "�ǻ�� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.dkrtkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl:
                content[0] = "���۸� �̿��� Ȳ�ٶ��� ���";
                if (_currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl] == null)
                    content[1] = "0 / 1";
                else
                    content[1] = _currentStateOfHomerspitPassAboutFish[(int)PublicDefined.eHomerspitPass.vhqvjfmfdldydgoghkdekfkddjwkqrl].Count + " / 1";
                break;
            case PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl:
                content[0] = "�������� ȣ�ӽ��� ��� ����� �ֱ�";
                content[1] = _currentStateOfHomerspitPassAboutAction[(int)PublicDefined.eHomerspitPass.tnwhrrhksdpghajtmvltgmlrnlanfrhrlsjgrl] + " / 1";
                break;
            case PublicDefined.eHomerspitPass.ghajtmvltrlfhr30akflcodnrl:
                content[0] = "ȣ�ӽ��� ��� 30���� ä���";
                content[1] = _userData.GetHomerspitRankDictionary().Count + " / 30";
                break;
        }
        return content;
    }

    // �����, �׼� ��θ� üũ�Ѵ�. �ΰ��� �Ŵ������� ���ʷ� ������ �� Ȯ���ϱ� ����
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
                        //Ŭ����
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
            // ============================================================�׼� ����==================================================================
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
                        //Ŭ����
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
