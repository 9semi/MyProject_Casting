using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int _gold;
    public int _pearl;
    public int _grade;
    public float _star;
    public string _nickname;
    public bool _haveJeongdongjinPass;
    public bool _haveSkywayPass;
    public bool _haveHomerspitPass;
    public bool _haveFirstAquarium;
    public bool _haveSecondAquarium;
    public bool _haveThirdAquarium;
    public bool _haveFourthAquarium;
    public bool _haveFifthAquarium;
    public bool _havePlatinumPackage;
    public bool _haveDiamondPackage;
    public bool _haveADBlock;
    public int _currentJeongdongjinPassIndex; // 30�̸� ��� ����Ʈ�� �Ϸ�� ���̴�.
    public int _currentSkywayPassIndex;
    public int _currentHomerspitPassIndex;
    public int _win;
    public int _lose;
    public int _draw;
    public bool _haveRepresentFish;
    public bool _isTutorialDone;
    public bool _isGetUlseomCoupon;
    public bool _isGetGStarCoupon;

    string _uid;

    // ���
    Dictionary<int, int> _rodDictionary = new Dictionary<int, int>(); // PublicintDefined.eRodType
    Dictionary<int, int> _reelDictionary = new Dictionary<int, int>();
    Dictionary<int, int> _baitDictionary = new Dictionary<int, int>();
    Dictionary<int, int> _pastebaitDictionary = new Dictionary<int, int>();
    Dictionary<int, int> _floatDictionary = new Dictionary<int, int>(); // ��
    Dictionary<int, int> _sinkerDictionary = new Dictionary<int, int>(); // ����

    // ���˴�, ��, �̳�, ����, ��, ����
    Dictionary<string, int> _currentEquipmentDictionary = new Dictionary<string, int>()
    { { "rod", 2 }, { "reel", 2 }, {"bait", -1 }, {"pastebait", -1 }, {"float", -1 }, {"sinker", -1 }, {"depthlength", 5 } };

    // �н�
    Dictionary<int, bool> _checkGetFreeReward_Jeongdongjin = new Dictionary<int, bool>();
    Dictionary<int, bool> _checkGetFreeReward_Skyway = new Dictionary<int, bool>();
    Dictionary<int, bool> _checkGetFreeReward_Homerspit = new Dictionary<int, bool>();
    Dictionary<int, bool> _checkGetPremiumReward_Jeongdongjin = new Dictionary<int, bool>();
    Dictionary<int, bool> _checkGetPremiumReward_Skyway = new Dictionary<int, bool>();
    Dictionary<int, bool> _checkGetPremiumReward_Homerspit = new Dictionary<int, bool>();

    // ���̾�̽��� ������ �� jeongdongjinPassStateAboutFish
    // ���̾�̽��� ������ �� jeongdongjinPassStateAboutAction
    Dictionary<int, int> _currentStateOfJeongdongjinPassAboutAction = new Dictionary<int, int>();
    Dictionary<int, Dictionary<int, int>> _currentStateOfJeongdongjinPassAboutFish = new Dictionary<int, Dictionary<int, int>>();

    Dictionary<int, int> _currentStateOfSkywayPassAboutAction = new Dictionary<int, int>();
    Dictionary<int, Dictionary<int, int>> _currentStateOfSkywayPassAboutFish = new Dictionary<int, Dictionary<int, int>>();

    Dictionary<int, int> _currentStateOfHomerspitPassAboutAction = new Dictionary<int, int>();
    Dictionary<int, Dictionary<int, int>> _currentStateOfHomerspitPassAboutFish = new Dictionary<int, Dictionary<int, int>>();

    // ������ <DB��ȣ, List<FishInfo(�̸�, ����, ����, ����, DB��ȣ)>>
    List<int> _aquariumCountList = new List<int>() { -1, -1, -1, -1, -1 }; // ���������� ������ �ִ� ������ üũ
    Dictionary<int, List<PublicDefined.stFishInfo>> _firstAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
    Dictionary<int, List<PublicDefined.stFishInfo>> _secondAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
    Dictionary<int, List<PublicDefined.stFishInfo>> _thirdAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
    Dictionary<int, List<PublicDefined.stFishInfo>> _fourthAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
    Dictionary<int, List<PublicDefined.stFishInfo>> _fifthAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();

    // �ʼ��þ��� ����� ��ũ
    Dictionary<int, PublicDefined.stRankFishInfo> _jeongdongjinRank = new Dictionary<int, PublicDefined.stRankFishInfo>();
    Dictionary<int, PublicDefined.stRankFishInfo> _skywayRank = new Dictionary<int, PublicDefined.stRankFishInfo>();
    Dictionary<int, PublicDefined.stRankFishInfo> _homerspitRank = new Dictionary<int, PublicDefined.stRankFishInfo>();

    PublicDefined.stFishInfo _representFish;

    Dictionary<string, bool> _platinumPackage = new Dictionary<string, bool>();
    Dictionary<string, bool> _diamondPackage = new Dictionary<string, bool>();

    // ��� ������ false�� �ʱ�ȭ
    public void InitData()
    {
        _uid = string.Empty;
        _nickname = "Guest";
        _gold = 200000;
        _pearl = 0;
        _grade = 1;
        _star = 0;
        _win = 0;
        _lose = 0;
        _draw = 0;

        // PC �׽�Ʈ
        {
            //List<PublicDefined.stFishInfo> list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(9, "���̿�¡��", 33.1561f, 0.355f, 113, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(9, "���̿�¡��", 32.1f, 0.2f, 92, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(9, "���̿�¡��", 15.12f, 0.15f, 43, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(9, "���̿�¡��", 64.23f, 0.895f, 155, PublicDefined.eFishType.Normal));
            //_firstAquarium.Add(9, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(23, "����", 18.75f, 17.184f, 88, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(23, "����", 3.45f, 4.171f, 248, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(23, "����", 28.73f, 5.386f, 131, PublicDefined.eFishType.Sundry));
            //_firstAquarium.Add(23, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(35, "�⸧���ڹ�", 12.2f, 1.1f, 101, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(35, "�⸧���ڹ�", 48.75f, 5.184f, 477, PublicDefined.eFishType.Normal));
            //_secondAquarium.Add(35, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(48, "������ü���", 23.151f, 0.345f, 94, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(48, "������ü���", 51.151f, 0.785f, 124, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(48, "������ü���", 73.151f, 5.34f, 978, PublicDefined.eFishType.Rare));
            //_secondAquarium.Add(48, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(53, "ȫ�صչٸ�", 64.23f, 7.8455f, 437, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(53, "ȫ�صչٸ�", 11.4f, 3.455f, 537, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(53, "ȫ�صչٸ�", 27.58f, 1.295f, 122, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(53, "ȫ�صչٸ�", 65.91f, 5.895f, 67, PublicDefined.eFishType.Rare));
            //_secondAquarium.Add(53, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(72, "������", 11.56f, 7.816f, 92, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(72, "������", 4.88f, 2.61f, 22, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(72, "������", 9.12f, 3.46f, 13, PublicDefined.eFishType.Sundry));
            //_secondAquarium.Add(72, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(65, "�뼭����ġ", 33.1561f, 0.355f, 113, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(65, "�뼭����ġ", 32.1f, 0.2f, 92, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(65, "�뼭����ġ", 15.12f, 0.15f, 43, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(65, "�뼭����ġ", 64.23f, 0.895f, 155, PublicDefined.eFishType.Normal));
            //_thirdAquarium.Add(65, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(42, "�Ը�ġ", 75.16f, 78.6f, 92, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(42, "�Ը�ġ", 47.38f, 12.2f, 22, PublicDefined.eFishType.Sundry));
            //list.Add(new PublicDefined.stFishInfo(42, "�Ը�ġ", 39.82f, 35.47f, 13, PublicDefined.eFishType.Sundry));
            //_thirdAquarium.Add(42, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(19, "����", 88.75f, 7.34f, 347, PublicDefined.eFishType.Rare));
            //_thirdAquarium.Add(19, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(59, "���ݴ���", 5.7f, 11.4f, 34, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(59, "���ݴ���", 8.1f, 55.47f, 224, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(59, "���ݴ���", 75.75f, 12.54f, 51, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(59, "���ݴ���", 35.75f, 91.2f, 477, PublicDefined.eFishType.Normal));
            //_thirdAquarium.Add(59, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(57, "�ǻ��", 245.7f, 553.34f, 3375, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(57, "�ǻ��", 111.75f, 457.34f, 1054, PublicDefined.eFishType.Rare));
            //_fourthAquarium.Add(57, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(95, "Ȳ��ġ", 74.74f, 33.17f, 307, PublicDefined.eFishType.Rare));
            //list.Add(new PublicDefined.stFishInfo(95, "Ȳ��ġ", 13.15f, 45.84f, 104, PublicDefined.eFishType.Rare));
            //_fifthAquarium.Add(95, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(69, "���׷���", 25.75f, 51.34f, 3375, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(69, "���׷���", 11.47f, 77.34f, 1054, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(69, "���׷���", 25.75f, 51.34f, 3375, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(69, "���׷���", 11.47f, 77.34f, 1054, PublicDefined.eFishType.Normal));
            //_fifthAquarium.Add(69, list);

            //list = new List<PublicDefined.stFishInfo>();
            //list.Add(new PublicDefined.stFishInfo(29, "û��", 48.75f, 12.54f, 12, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(29, "û��", 13.5f, 735.5f, 354, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(29, "û��", 77.75f, 44.51f, 778, PublicDefined.eFishType.Normal));
            //list.Add(new PublicDefined.stFishInfo(29, "û��", 1.75f, 1.1f, 943, PublicDefined.eFishType.Normal));
            //_fifthAquarium.Add(29, list);
        }

        // �н�
        _currentJeongdongjinPassIndex = 0;
        _currentSkywayPassIndex = 0;
        _currentHomerspitPassIndex = 0;
        _haveJeongdongjinPass = false;
        _haveSkywayPass = false;
        _haveHomerspitPass = false;

        // ������ ��Ű��
        _havePlatinumPackage = false;
        _haveDiamondPackage = false;

        // ������
        _haveFirstAquarium = true;
        _haveSecondAquarium = false;
        _haveThirdAquarium = false;
        _haveFourthAquarium = false;
        _haveFifthAquarium = false;

        _haveRepresentFish = false;
        _isTutorialDone = false;

        _isGetUlseomCoupon = false;
        _isGetGStarCoupon = false;

        //�ӽ� �׽�Ʈ
        CheckAquariumCount();

        for (int i = 0; i < (int)PublicDefined.eRodType.Count; i++)
        {
            if (i.Equals(2))
                _rodDictionary.Add(i, 1);
            else
                _rodDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.eReelType.Count; i++)
        {
            if (i.Equals(2))
                _reelDictionary.Add(i, 1);
            else
                _reelDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.eBaitType.Count; i++)
        {
            _baitDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.ePasteBaitType.Count; i++)
        {

            _pastebaitDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.eFloatType.Count; i++)
        {
            _floatDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.eSinkerType.Count; i++)
        {
            _sinkerDictionary.Add(i, 0);
        }

        for (int i = 0; i < (int)PublicDefined.eJeongdongjinPass.Count; i++)
        {
            _checkGetFreeReward_Jeongdongjin.Add(i, false);
            _checkGetFreeReward_Skyway.Add(i, false);
            _checkGetFreeReward_Homerspit.Add(i, false);

            _checkGetPremiumReward_Jeongdongjin.Add(i, false);
            _checkGetPremiumReward_Skyway.Add(i, false);
            _checkGetPremiumReward_Homerspit.Add(i, false);
        }

        //_checkGetFreeReward_Jeongdongjin[3] = true;
        //_checkGetFreeReward_Jeongdongjin[5] = true;
        //_checkGetFreeReward_Jeongdongjin[11] = true;
        //_checkGetFreeReward_Jeongdongjin[2] = true;
        //_checkGetFreeReward_Jeongdongjin[17] = true;
        //_checkGetFreeReward_Jeongdongjin[10] = true;

        //_checkGetPremiumReward_Jeongdongjin[1] = true;
        //_checkGetPremiumReward_Jeongdongjin[2] = true;
        //_checkGetPremiumReward_Jeongdongjin[3] = true;
        //_checkGetPremiumReward_Jeongdongjin[4] = true;
        //_checkGetPremiumReward_Jeongdongjin[5] = true;


        // �н� �ʱ�ȭ
        for (int i = 0; i < (int)PublicDefined.eJeongdongjinPass.Count; i++)
        {
            if (i.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsanfrhrlsjgrl)
                || i.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksemfdjrkrl)
                || i.Equals((int)PublicDefined.eJeongdongjinPass.wjdehdwlsalRlvordufrl)
                || i.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdptjanfrhrlvkfrl)
                || i.Equals((int)PublicDefined.eJeongdongjinPass.tnwhrrhksdpwjdehdwlsgmlrnlanfrhrlsjgrl))
            {
                _currentStateOfJeongdongjinPassAboutAction.Add(i, 0);
                _currentStateOfSkywayPassAboutAction.Add(i, 0);
                _currentStateOfHomerspitPassAboutAction.Add(i, 0);
                //Debug.LogWarning(i);
            }
            else
            {
                _currentStateOfJeongdongjinPassAboutFish.Add(i, null);
                _currentStateOfSkywayPassAboutFish.Add(i, null);
                _currentStateOfHomerspitPassAboutFish.Add(i, null);
                //Debug.LogWarning(i);
            }
        }
    }

    // ��ǥ ����� �ʱ�ȭ
    public void InitRepresentFish(int DBNumber, string n, float l, float w, int g, int t, string key)
    {
        _representFish = new PublicDefined.stFishInfo(DBNumber, n, l, w, g,(PublicDefined.eFishType)t);
        _representFish.SetKey(key);
    }
    public PublicDefined.stFishInfo GetRepresentFish()
    {
        return _representFish;
    }


    // UID �ʱ�ȭ
    public void SetUId(string uid)
    {
        _uid = uid;
    }
    // UID ��ȯ
    public string GetUId()
    {
        return _uid;
    }
    // �г��� �ʱ�ȭ
    public void SetNickname(string nickname)
    {
        _nickname = nickname;
    }
    // �г��� ��ȯ
    public string GetNickname()
    {
        return _nickname;
    }
    // ���˴� ��ųʸ�
    public Dictionary<int, int> GetRodDictionary()
    {
        return _rodDictionary;
    }
    public void SetRodDictionary(Dictionary<int, int> dic)
    {
        _rodDictionary = dic;
    }
    // �� ��ųʸ�
    public Dictionary<int, int> GetReelDictionary()
    {
        return _reelDictionary;
    }
    // �̳� ��ųʸ�
    public Dictionary<int, int> GetBaitDictionary()
    {
        return _baitDictionary;
    }
    public void SetBaitDictionary(Dictionary<int, int> dic)
    {
        _baitDictionary = dic;
    }
    // ���� ��ųʸ�
    public Dictionary<int, int> GetPasteBaitDictionary()
    {
        return _pastebaitDictionary;
    }
    public void SetPasteBaitictionary(Dictionary<int, int> dic)
    {
        _pastebaitDictionary = dic;
    }
    // �� ��ųʸ�
    public Dictionary<int, int> GetFloatDictionary()
    {
        return _floatDictionary;
    }
    public void SetFloatDictionary(Dictionary<int, int> dic)
    {
        _floatDictionary = dic;
    }
    // ���� ��ųʸ�
    public Dictionary<int, int> GetSinkerDictionary()
    {
        return _sinkerDictionary;
    }
    public void SetSinkerDictionary(Dictionary<int, int> dic)
    {
        _sinkerDictionary = dic;
    }

    // ���� ���� �ִ� ���
    public Dictionary<string, int> GetCurrentEquipmentDictionary()
    {
        return _currentEquipmentDictionary;
    }

    // �ʱ� ���·� ������
    public void BackToInitialState()
    {
        _currentEquipmentDictionary = new Dictionary<string, int>()
        { { "rod", 2 }, { "reel", 2 }, {"bait", -1 }, {"pastebait", -1 }, {"float", -1 }, {"sinker", -1 }, {"depthlength", 5 } };

    }

    // �� ���� �н� ������ �޾Ҵ��� ���� �ȹ޾Ҵ��� üũ�ϴ� ��ųʸ�
    public Dictionary<int, bool> GetCheckJeongdongjinPassFreeReward()
    {
        return _checkGetFreeReward_Jeongdongjin;
    }
    public Dictionary<int, bool> GetCheckSkywayPassFreeReward()
    {
        return _checkGetFreeReward_Skyway;
    }
    public Dictionary<int, bool> GetCheckHomerPassFreeReward()
    {
        return _checkGetFreeReward_Homerspit;
    }
    public Dictionary<int, bool> GetCheckJeongdongjinPassPremiumReward()
    {
        return _checkGetPremiumReward_Jeongdongjin;
    }
    public Dictionary<int, bool> GetCheckSkywayPassPremiumReward()
    {
        return _checkGetPremiumReward_Skyway;
    }
    public Dictionary<int, bool> GetCheckHomerspitPassPremiumReward()
    {
        return _checkGetPremiumReward_Homerspit;
    }


    // ������ ���õ� �н� ��ȯ
    public Dictionary<int, Dictionary<int, int>> GetCurrentStateOfJeongdongjinPassAboutFish()
    {
        // ���̾�̽��� ������ �� jeongdongjinPassStateAboutFish
        return _currentStateOfJeongdongjinPassAboutFish;
    }
    public Dictionary<int, Dictionary<int, int>> GetCurrentStateOfSkywayPassAboutFish()
    {
        return _currentStateOfSkywayPassAboutFish;
    }
    public Dictionary<int, Dictionary<int, int>> GetCurrentStateOfHomerspitPassAboutFish()
    {
        return _currentStateOfHomerspitPassAboutFish;
    }

    // �׼ǰ� ���õ� �н� ��ȯ
    public Dictionary<int, int> GetCurrentStateOfJeongdongjinPassAboutAction()
    {
        return _currentStateOfJeongdongjinPassAboutAction;
    }
    public Dictionary<int, int> GetCurrentStateOfSkywayPassAboutAction()
    {
        return _currentStateOfSkywayPassAboutAction;
    }
    public Dictionary<int, int> GetCurrentStateOfHomerspitPassAboutAction()
    {
        return _currentStateOfHomerspitPassAboutAction;
    }
    public Dictionary<string, object> GetCurrentEquipmentDictionary2()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();

        foreach(KeyValuePair<string, int> data in _currentEquipmentDictionary)
        {
            dic.Add(data.Key, data.Value);
        }
        return dic;
    }

    public Dictionary<int, List<PublicDefined.stFishInfo>> GetFirstAquariumDictionary()
    {
        return _firstAquarium;
    }

    public Dictionary<int, List<PublicDefined.stFishInfo>> GetSecondAquariumDictionary()
    {
        return _secondAquarium;
    }

    public Dictionary<int, List<PublicDefined.stFishInfo>> GetThirdAquariumDictionary()
    {
        return _thirdAquarium;
    }

    public Dictionary<int, List<PublicDefined.stFishInfo>> GetFourthAquariumDictionary()
    {
        return _fourthAquarium;
    }

    public Dictionary<int, List<PublicDefined.stFishInfo>> GetFifthAquariumDictionary()
    {
        return _fifthAquarium;
    }
    public Dictionary<int, List<PublicDefined.stFishInfo>> SelectAquariumDictionary(int n)
    {
        switch(n)
        {
            case 0:
                return _firstAquarium;
            case 1:
                return _secondAquarium;
            case 2:
                return _thirdAquarium;
            case 3:
                return _fourthAquarium;
            default:
                return _fifthAquarium;
        }
    }

    public void CheckAquariumCount()
    {
        int cnt = 0;
        
        if (_haveFirstAquarium)
        {
            _aquariumCountList[cnt] = 0;
            foreach (List<PublicDefined.stFishInfo> data in _firstAquarium.Values)
            {
                _aquariumCountList[cnt] += data.Count;
            }
        }
        cnt++;
        if (_haveSecondAquarium)
        {
            _aquariumCountList[cnt] = 0;
            foreach (List<PublicDefined.stFishInfo> data in _secondAquarium.Values)
            {
                _aquariumCountList[cnt] += data.Count;
            }
        }
        cnt++;
        if (_haveThirdAquarium)
        {
            _aquariumCountList[cnt] = 0;
            foreach (List<PublicDefined.stFishInfo> data in _thirdAquarium.Values)
            {
                _aquariumCountList[cnt] += data.Count;
            }
        }
        cnt++;
        if (_haveFourthAquarium)
        {
            _aquariumCountList[cnt] = 0;
            foreach (List<PublicDefined.stFishInfo> data in _fourthAquarium.Values)
            {
                _aquariumCountList[cnt] += data.Count;
            }
        }
        cnt++;
        if (_haveFifthAquarium)
        {
            _aquariumCountList[cnt] = 0;
            foreach (List<PublicDefined.stFishInfo> data in _fifthAquarium.Values)
            {
                _aquariumCountList[cnt] += data.Count;
            }
        }
    }
    public void InitAquarium()
    {
        _firstAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
        _secondAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
        _thirdAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
        _fourthAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
        _fifthAquarium = new Dictionary<int, List<PublicDefined.stFishInfo>>();
    }
    public void UpdateCountStateList(int beforeIndex, int afterIndex)
    {
        _aquariumCountList[beforeIndex]--;
        _aquariumCountList[afterIndex]++;
    }
    public void UpdateCountStateList(bool isPlus, int index)
    {
        if (isPlus)
            _aquariumCountList[index]++;
        else
        {
            if (_aquariumCountList[index].Equals(0))
                return;

            _aquariumCountList[index]--;
        }
    }
    public List<int> GetAquariumCountState()
    {
        return _aquariumCountList;
    }
    public void AquariumUpdate(int aquariumNumber)
    {
        _aquariumCountList[aquariumNumber]++;
    }
    public List<bool> GetAquariumPossessState()
    {
        List<bool> _aquariumState = new List<bool>();
        _aquariumState.Add(_haveFirstAquarium);
        _aquariumState.Add(_haveSecondAquarium);
        _aquariumState.Add(_haveThirdAquarium);
        _aquariumState.Add(_haveFourthAquarium);
        _aquariumState.Add(_haveFifthAquarium);
        return _aquariumState;
    }

    public void SetEquipmentDictionary(Dictionary<string, int> dic)
    {
        _currentEquipmentDictionary = dic;
    }

    public Dictionary<int, PublicDefined.stRankFishInfo> GetJeongdongjinRankDictionary()
    {
        return _jeongdongjinRank;
    }
    public Dictionary<int, PublicDefined.stRankFishInfo> GetSkywayRankDictionary()
    {
        return _skywayRank;
    }
    public Dictionary<int, PublicDefined.stRankFishInfo> GetHomerspitRankDictionary()
    {
        return _homerspitRank;
    }


    public void InitNewUser()
    {
        _currentEquipmentDictionary["rod"] = 2;
        _currentEquipmentDictionary["reel"] = 2;
        _rodDictionary[2] = 1;
        _reelDictionary[2] = 1;
    }
    public void InitRodReel(int rod, int reel)
    {
        _currentEquipmentDictionary["rod"] = rod;
        _currentEquipmentDictionary["reel"] = reel;
        _rodDictionary[rod] = 1;
        _reelDictionary[reel] = 1;
    }

    public void GiveBaitTutorialStep19()
    {
        _baitDictionary[45]++;
        _pastebaitDictionary[2]++;

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("/bait/45", _baitDictionary[45]);
        dic.Add("/pastebait/2", _pastebaitDictionary[2]);
        DBManager.INSTANCE.UpdateFirebase(dic);
    }
    public void CleanUpPlatinumPackage()
    {
        _platinumPackage = new Dictionary<string, bool>();
    }

    public void CleanUpDiamondPackage()
    {
        _diamondPackage = new Dictionary<string, bool>();
    }

    public Dictionary<string, bool> GetPlatinumPackage()
    {
        return _platinumPackage;
    }

    public Dictionary<string, bool> GetDiamondPackage()
    {
        return _diamondPackage;
    }

    public void ShowMyData()
    {
        //Debug.Log("���� ���: " + _gold);
        //Debug.Log("���� ����: " + _pearl);
        //Debug.Log("���� �г���: " + _nickname);

        //for (int i = 0; i < _baitDictionary.Count; i++)
        //{
        //    Debug.Log(i + "��° bait : " + _baitDictionary[i].ToString());
        //}

        //for (int i = 0; i < _rodDictionary.Count; i++)
        //{
        //    Debug.Log(i + "��° rod : " + _rodDictionary[i].ToString());
        //}

        //for (int i = 0; i < _pastebaitDictionary.Count; i++)
        //{
        //    Debug.Log(i + "��° pastebait : " + _pastebaitDictionary[i].ToString());
        //}

        //for (int i = 0; i < _sinkerDictionary.Count; i++)
        //{
        //    Debug.Log(i + "��° sinker : " + _sinkerDictionary[i].ToString());
        //}

        //for (int i = 0; i < _floatDictionary.Count; i++)
        //{
        //    Debug.Log(i + "��° float : " + _floatDictionary[i].ToString());
        //}

        //Debug.Log("���� jeongdongjin �н� �ε��� : " + _currentJeongdongjinPassIndex + " , jeongdongjin �н� ����: " + _haveJeongdongjinPass);
        //Debug.Log("���� skyway �н� �ε��� : " + _currentSkywayPassIndex + " , skyway �н� ����: " + _haveSkywayPass);
        //Debug.Log("���� homerspit �н� �ε��� : " + _currentHomerspitPassIndex + " , homerspit �н� ����: " + _haveHomerspitPass);

        //Debug.Log("ù��° ������ DB��ȣ ����");
        //foreach (KeyValuePair<int, List<PublicDefined.stFishInfo>> data in _firstAquarium)
        //{
        //    Debug.Log(data.Key + "�� ����� " + data.Value.Count + "����");
        //    //for (int i = 0; i < data.Value.Count; i++)
        //    //{
        //    //    Debug.Log(data.Value[i]._length);
        //    //}
        //}
        //Debug.Log("�ι�° ������ DB��ȣ ����");
        //foreach (KeyValuePair<int, List<PublicDefined.stFishInfo>> data in _secondAquarium)
        //{
        //    Debug.Log(data.Key + "�� ����� " + data.Value.Count + "����");
        //    //for (int i = 0; i < data.Value.Count; i++)
        //    //{
        //    //    Debug.Log(data.Value[i]._length);
        //    //}
        //}
        //Debug.Log("����° ������ DB��ȣ ����");
        //foreach (KeyValuePair<int, List<PublicDefined.stFishInfo>> data in _thirdAquarium)
        //{
        //    Debug.Log(data.Key + "�� ����� " + data.Value.Count + "����");
        //    //for (int i = 0; i < data.Value.Count; i++)
        //    //{
        //    //    Debug.Log(data.Value[i]._length);
        //    //}
        //}
        //Debug.Log("�׹�° ������ DB��ȣ ����");
        //foreach (KeyValuePair<int, List<PublicDefined.stFishInfo>> data in _fourthAquarium)
        //{
        //    Debug.Log(data.Key + "�� ����� " + data.Value.Count + "����");
        //    //for (int i = 0; i < data.Value.Count; i++)
        //    //{
        //    //    Debug.Log(data.Value[i]._length);
        //    //}
        //}
        //Debug.Log("�ټ���° ������ DB��ȣ ����");
        //foreach (KeyValuePair<int, List<PublicDefined.stFishInfo>> data in _fifthAquarium)
        //{
        //    Debug.Log(data.Key + "�� ����� " + data.Value.Count + "����");
        //    //for (int i = 0; i < data.Value.Count; i++)
        //    //{
        //    //    Debug.Log(data.Value[i]._length);
        //    //}
        //}

        //Debug.Log("jeongdongjin ����� �н�");
        //foreach (KeyValuePair<int, Dictionary<int, int>> data in _currentStateOfJeongdongjinPassAboutFish)
        //{
        //    if (data.Value != null)
        //    {
        //        foreach (KeyValuePair<int, int> data2 in data.Value)
        //        {

        //                Debug.Log(data.Key + "��° ����Ʈ " + data2.Key + "  :  " + data2.Value);
        //        }
        //    }
        //}

        //Debug.Log("skyway ����� �н�");
        //foreach (KeyValuePair<int, Dictionary<int, int>> data in _currentStateOfSkywayPassAboutFish)
        //{
        //    if (data.Value != null)
        //    {
        //        foreach (KeyValuePair<int, int> data2 in data.Value)
        //        {

        //                Debug.Log(data.Key + "��° ����Ʈ " + data2.Key + "  :  " + data2.Value);
        //        }
        //    }
        //}

        //Debug.Log("homerspit ����� �н�");
        //foreach (KeyValuePair<int, Dictionary<int, int>> data in _currentStateOfHomerspitPassAboutFish)
        //{
        //    if (data.Value != null)
        //    {
        //        foreach (KeyValuePair<int, int> data2 in data.Value)
        //        {

        //                Debug.Log(data.Key + "��° ����Ʈ " + data2.Key + "  :  " + data2.Value);
        //        }
        //    }
        //}

        //Debug.Log("jeongdongjin �׼� �н�");
        //foreach (KeyValuePair<int, int> data in _currentStateOfJeongdongjinPassAboutAction)
        //{
        //    Debug.Log(data.Key + "  :  " + data.Value);
        //}

        //Debug.Log("skyway �׼� �н�");
        //foreach (KeyValuePair<int, int> data in _currentStateOfSkywayPassAboutAction)
        //{
        //    Debug.Log(data.Key + "  :  " + data.Value);
        //}

        //Debug.Log("homerspit �׼� �н�");
        //foreach (KeyValuePair<int, int> data in _currentStateOfHomerspitPassAboutAction)
        //{
        //    Debug.Log(data.Key + "  :  " + data.Value);
        //}

        //Debug.Log("jeongdongjin free ����");
        //foreach (KeyValuePair<int, bool> data in _checkGetFreeReward_Jeongdongjin)
        //{
        //    Debug.Log(data.Key + "  :  " + data.Value);
        //}

        Debug.Log("skyway free ����");
        foreach (KeyValuePair<int, bool> data in _checkGetFreeReward_Skyway)
        {
            Debug.Log(data.Key + "  :  " + data.Value);
        }

        //Debug.Log("homerspit free ����");
        //foreach (KeyValuePair<int, bool> data in _checkGetFreeReward_Homerspit)
        //{
        //    Debug.Log(data.Key + "  :  " + data.Value);
        //}
    }
}
