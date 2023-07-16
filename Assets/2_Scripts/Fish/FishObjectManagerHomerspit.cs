using System.Collections.Generic;
using UnityEngine;

public class FishObjectManagerHomerspit : MonoBehaviour
{
    public PetManager _petManager;
    public GameManager _gameManager;

    #region ����� ������ ����
    // ��ȸ������ - ���(8) 17 15 131 221
    public GameObject blackfinflounderPrefab;   // �⸧���ڹ�
    public GameObject rocksolePrefab;  // �������ڹ�
    public GameObject broadbandedthornyheadPrefab;   // ȫ��ġ
    public GameObject salmonsnailfishPrefab;    // ��ȫ��ġ
    public GameObject sailfinpoacherPrefab;   // �����ٰ��
    public GameObject manybandedsolePrefab;   // ������ü���
    public GameObject dragonpoacherPrefab; // ���ٰ��
    public GameObject redtippedgrouperPrefab;  // ȫ�صչٸ�
    // ��ȸ������ - �Ϲ�(6)
    public GameObject kamchatkaflounderPrefab;  // ȭ��ġ���ڹ�
    public GameObject flatheadsolePrefab;  // ����ġ���ڹ�
    public GameObject pacificoceanperchPrefab;    // �幮����
    public GameObject beringwolffishPrefab;  // �̸�ġ
    public GameObject lavenderjobfishPrefab;  // �ںӵ�
    public GameObject splendidalfonsinoPrefab; // ���ݴ���
    // ��ȸ������ - ���(3)
    public GameObject bigskatePrefab;   // �δ�������
    public GameObject yellowfintunaPrefab; // Ȳ�ٶ���
    public GameObject lingcodPrefab;   // ���뷡��
    // ȸ������ - ���(3)
    public GameObject pacificsauryPrefab;   // ��ġ
    public GameObject gnomefishPrefab;   // �Ը�ġ
    public GameObject spotlinedsardinePrefab;  // ���
    // ȸ������ - �Ϲ�(7)
    public GameObject alaskapollackPrefab;   // ����
    public GameObject pacificcodPrefab;  // �뱸
    public GameObject sablefishPrefab;   // ���뱸
    public GameObject yellowfinsolePrefab;   // ���ð��ڹ�
    public GameObject bluefingurnardPrefab; // ����
    public GameObject chumsalmonPrefab;   // ����
    public GameObject japanesepufferfishPrefab;    // ���ֺ�
    // ȸ������ - ���(5)
    public GameObject yelloweyerockfishPrefab;    // ������췰
    public GameObject quillbackrockfishPrefab;    // ��ħ�췰
    public GameObject salmonsharkPrefab;    // �ǻ��
    public GameObject halibutPrefab;    // �������ڹ�
    public GameObject chinooksalmonPrefab;    // �տ���
    #endregion
    #region ����� ������ ����
    // ��ȸ������ - ���(8)
    [HideInInspector] public GameObject[] blackfinflounder = new GameObject[26]; // �⸧���ڹ�
    [HideInInspector] public GameObject[] rocksole = new GameObject[26];  // �������ڹ�
    [HideInInspector] public GameObject[] broadbandedthornyhead = new GameObject[18]; // ȫ��ġ
    [HideInInspector] public GameObject[] salmonsnailfish = new GameObject[18];  // ��ȫ��ġ
    [HideInInspector] public GameObject[] sailfinpoacher = new GameObject[18];  // �����ٰ��
    [HideInInspector] public GameObject[] manybandedsole = new GameObject[18];  // ������ü���
    [HideInInspector] public GameObject[] dragonpoacher = new GameObject[18];   // ���ٰ��
    [HideInInspector] public GameObject[] redtippedgrouper = new GameObject[18];   // ȫ�صչٸ�
    // ��ȸ������ - �Ϲ�(6)
    [HideInInspector] public GameObject[] kamchatkaflounder = new GameObject[13];   // ȭ��ġ���ڹ�
    [HideInInspector] public GameObject[] flatheadsole = new GameObject[19];   // ����ġ���ڹ�
    [HideInInspector] public GameObject[] pacificoceanperch = new GameObject[13];  // �幮����
    [HideInInspector] public GameObject[] beringwolffish = new GameObject[18];   // �̸�ġ
    [HideInInspector] public GameObject[] lavenderjobfish = new GameObject[13];   // �ںӵ�
    [HideInInspector] public GameObject[] splendidalfonsino = new GameObject[19];   // ���ݴ���
    // ��ȸ������ - ���(3)
    [HideInInspector] public GameObject[] bigskate = new GameObject[5];  // �δ�������
    [HideInInspector] public GameObject[] yellowfintuna = new GameObject[5];    // Ȳ�ٶ���
    [HideInInspector] public GameObject[] lingcod = new GameObject[5];  // ���뷡��
    // ȸ������ - ���(3)
    [HideInInspector] public GameObject[] pacificsaury = new GameObject[30];  // ��ġ
    [HideInInspector] public GameObject[] gnomefish = new GameObject[30]; // �Ը�ġ
    [HideInInspector] public GameObject[] spotlinedsardine = new GameObject[30];    // ���
    // ȸ������ - �Ϲ�(7)
    [HideInInspector] public GameObject[] alaskapollack = new GameObject[30]; // ����
    [HideInInspector] public GameObject[] pacificcod = new GameObject[30];    // �뱸
    [HideInInspector] public GameObject[] sablefish = new GameObject[30]; // ���뱸
    [HideInInspector] public GameObject[] yellowfinsole = new GameObject[30]; // ���ð��ڹ�
    [HideInInspector] public GameObject[] bluefingurnard = new GameObject[30];   // ����
    [HideInInspector] public GameObject[] chumsalmon = new GameObject[30]; // ����
    [HideInInspector] public GameObject[] japanesepufferfish = new GameObject[30];  // ���ֺ�
    // ȸ������ - ���(5)    
    [HideInInspector] public GameObject[] yelloweyerockfish = new GameObject[10];    // ������췰
    [HideInInspector] public GameObject[] quillbackrockfish = new GameObject[10];  // ��ħ�췰
    [HideInInspector] public GameObject[] salmonshark = new GameObject[10];    // �ǻ��
    [HideInInspector] public GameObject[] halibut = new GameObject[10];  // �������ڹ�
    [HideInInspector] public GameObject[] chinooksalmon = new GameObject[10];  // �տ���
    #endregion

    public Transform[] distinctionObject; // �������� ���� ����⺰ �θ������Ʈ(�������Ʈ) ����

    // ���̴��� ���� ���� ����� ����Ʈ
    [HideInInspector] public List<GameObject> _rareFishList = new List<GameObject>();

    // �� ���信 ������ �޴� ����� ����Ʈ
    // ũ��
    [HideInInspector] public List<GameObject> _influencedFishListByKrill = new List<GameObject>();

    // �����ӿ� ���� Ȯ���� �����ϴ� ����� ����Ʈ
    // ������
    [HideInInspector] public List<FishHomerspit> _homerspitfishListIncreaseProbabilityWhenStill;
    // ������
    [HideInInspector] public List<FishHomerspit> _homerspitfishListIncreaseProbabilityWhenMove;

    bool _isIncreaseWhenMove = false;
    bool _isIncreaseWhenStill = false;

    [HideInInspector] public bool _isFirst;
    [HideInInspector] public List<FishHomerspit> _caughtFishs = new List<FishHomerspit>();
    // ���� ����� �� FishingBot�� ����Ⱑ �� �����Ǿ����� �˷��ֱ� ����
    FishingBot _fishingBot;

    private void Awake()
    {
        _isFirst = true;
        AssignmentFishs();
        CreateFish(); //���۽� Ǯ���� ���� ����� ���� �Լ�
        DataManager.INSTANCE.SetFishObjectManagerInstance(this);
    }

    public void InitializeFishSearchRange()
    {
        if (_gameManager._userData == null)
            _gameManager._userData = DBManager.INSTANCE.GetUserData();

        if (_gameManager._userData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
        {
            UpdateFishSearchRange(0);
        }
        else
        {
            UpdateFishSearchRange(3);
        }
    }
    public void ReActiveFish(int worldTime)
    {
        if (_caughtFishs.Count > 0)
        {
            for (int i = 0; i < _caughtFishs.Count; i++)
            {
                _caughtFishs[i].transform.SetParent(_caughtFishs[i]._originParent);
                _caughtFishs[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            _caughtFishs = new List<FishHomerspit>();
        }

        for (int i = 0; i < 5; i++)
        {
            bigskate[i].SetActive(false);  // �δ�������
            yellowfintuna[i].SetActive(false);    // Ȳ�ٶ���
            lingcod[i].SetActive(false);  // ���뷡��
        }
        for (int i = 0; i < 10; i++)
        {
            yelloweyerockfish[i].SetActive(false);     // ������췰
            quillbackrockfish[i].SetActive(false);   // ��ħ�췰
            salmonshark[i].SetActive(false);     // �ǻ��
            halibut[i].SetActive(false);   // �������ڹ�
            chinooksalmon[i].SetActive(false);  // �տ���
        }

        for (int i = 0; i < 13; i++)
        {
            kamchatkaflounder[i].SetActive(false);  // ȭ��ġ���ڹ�
            pacificoceanperch[i].SetActive(false); // �幮����
            lavenderjobfish[i].SetActive(false);  // �ںӵ�
        }

        for (int i = 0; i < 18; i++)
        {
            broadbandedthornyhead[i].SetActive(false); // ȫ��ġ
            salmonsnailfish[i].SetActive(false);  // ��ȫ��ġ
            sailfinpoacher[i].SetActive(false);  // �����ٰ��
            manybandedsole[i].SetActive(false); // ������ü���
            dragonpoacher[i].SetActive(false);  // ���ٰ��
            redtippedgrouper[i].SetActive(false);   // ȫ�صչٸ�
            beringwolffish[i].SetActive(false);   // �̸�ġ
        }
        for (int i = 0; i < 19; i++)
        {
            flatheadsole[i].SetActive(false);   // ����ġ���ڹ�
            splendidalfonsino[i].SetActive(false);  // ���ݴ���
        }
        for (int i = 0; i < 26; i++)
        {
            blackfinflounder[i].SetActive(false);  // �⸧���ڹ�
            rocksole[i].SetActive(false);   // �������ڹ�
        }
        for (int i = 0; i < 30; i++)
        {
            pacificsaury[i].SetActive(false);   // ��ġ
            gnomefish[i].SetActive(false);  // �Ը�ġ
            spotlinedsardine[i].SetActive(false);     // ���
            alaskapollack[i].SetActive(false);  // ����
            pacificcod[i].SetActive(false);     // �뱸
            sablefish[i].SetActive(false);  // ���뱸
            yellowfinsole[i].SetActive(false);  // ���ð��ڹ�
            bluefingurnard[i].SetActive(false);   // ����
            chumsalmon[i].SetActive(false);  // ����
            japanesepufferfish[i].SetActive(false);  // ���ֺ�
        }
        ActiveFish(worldTime);
    }
    // ������ ���� 0 ~ 10
    private int RandomCount(int number)
    {
        int count;
        switch (number)
        {
            case 0:
                count = Random.Range(0, 2);
                break;
            case 1:
                count = Random.Range(1, 4);
                break;
            case 2:
                count = Random.Range(1, 6);
                break;
            case 3:
                count = Random.Range(1, 8);
                break;
            case 4:
                count = Random.Range(5, 11);
                break;
            case 5:
                count = Random.Range(7, 14);
                break;
            case 6:
                count = Random.Range(9, 18);
                break;
            case 7:
                count = Random.Range(13, 20);
                break;
            case 10:
                count = Random.Range(20, 31);
                break;
            // default�� �ʿ����
            default:
                count = 1;
                break;
        }
        return count;
    }
    void CreateFish() // ����� �����Լ�
    {
        int index;
        // �⸧���ڹ�(�ϼ�, �糪��)
        for (index = 0; index < blackfinflounder.Length; index++)
        {
            blackfinflounder[index] = Instantiate(blackfinflounderPrefab);
            blackfinflounder[index].transform.parent = distinctionObject[0].transform;
            blackfinflounder[index].SetActive(false);
        }
        // �������ڹ�(�ϼ�, �糪��)
        for (index = 0; index < rocksole.Length; index++)
        {
            rocksole[index] = Instantiate(rocksolePrefab);
            rocksole[index].transform.parent = distinctionObject[1].transform;
            rocksole[index].SetActive(false);
        }
        // ȫ��ġ(������Ʈ, �糪��)
        for (index = 0; index < broadbandedthornyhead.Length; index++)
        {
            broadbandedthornyhead[index] = Instantiate(broadbandedthornyheadPrefab);
            broadbandedthornyhead[index].transform.parent = distinctionObject[2].transform;
            broadbandedthornyhead[index].SetActive(false);
        }
        // ��ȫ��ġ(�糪��)
        for (index = 0; index < salmonsnailfish.Length; index++)
        {
            salmonsnailfish[index] = Instantiate(salmonsnailfishPrefab);
            salmonsnailfish[index].transform.parent = distinctionObject[3].transform;
            salmonsnailfish[index].SetActive(false);
        }
        // �����ٰ��(�糪��)
        for (index = 0; index < sailfinpoacher.Length; index++)
        {
            sailfinpoacher[index] = Instantiate(sailfinpoacherPrefab);
            sailfinpoacher[index].transform.parent = distinctionObject[4].transform;
            sailfinpoacher[index].SetActive(false);
        }
        // ������ü���(�糪��, ������Ʈ)
        for (index = 0; index < manybandedsole.Length; index++)
        {
            manybandedsole[index] = Instantiate(manybandedsolePrefab);
            manybandedsole[index].transform.parent = distinctionObject[5].transform;
            manybandedsole[index].SetActive(false);
        }
        // ���ٰ��(�ϼ�, ������)
        for (index = 0; index < dragonpoacher.Length; index++)
        {
            dragonpoacher[index] = Instantiate(dragonpoacherPrefab);
            dragonpoacher[index].transform.parent = distinctionObject[6].transform;
            dragonpoacher[index].SetActive(false);
        }
        // ȫ�صչٸ�(����, ������)
        for (index = 0; index < redtippedgrouper.Length; index++)
        {
            redtippedgrouper[index] = Instantiate(redtippedgrouperPrefab);
            redtippedgrouper[index].transform.parent = distinctionObject[7].transform;
            redtippedgrouper[index].SetActive(false);
        }
        // ȭ��ġ���ڹ�(�޽�����, �糪��)
        for (index = 0; index < kamchatkaflounder.Length; index++)
        {
            kamchatkaflounder[index] = Instantiate(kamchatkaflounderPrefab);
            kamchatkaflounder[index].transform.parent = distinctionObject[8].transform;
            kamchatkaflounder[index].SetActive(false);
        }
        // ����ġ���ڹ�(�糪��)
        for (index = 0; index < flatheadsole.Length; index++)
        {
            flatheadsole[index] = Instantiate(flatheadsolePrefab);
            flatheadsole[index].transform.parent = distinctionObject[9].transform;
            flatheadsole[index].SetActive(false);
        }
        // �幮����(������Ʈ)
        for (index = 0; index < pacificoceanperch.Length; index++)
        {
            pacificoceanperch[index] = Instantiate(pacificoceanperchPrefab);
            pacificoceanperch[index].transform.parent = distinctionObject[10].transform;
            pacificoceanperch[index].SetActive(false);
        }
        // �̸�ġ(�ϼ�, ������Ʈ)
        for (index = 0; index < beringwolffish.Length; index++)
        {
            beringwolffish[index] = Instantiate(beringwolffishPrefab);
            beringwolffish[index].transform.parent = distinctionObject[11].transform;
            beringwolffish[index].SetActive(false);
        }
        // �ںӵ�(�ϼ�, ������)
        for (index = 0; index < lavenderjobfish.Length; index++)
        {
            lavenderjobfish[index] = Instantiate(lavenderjobfishPrefab);
            lavenderjobfish[index].transform.parent = distinctionObject[12].transform;
            lavenderjobfish[index].SetActive(false);
        }
        // ���ݴ���(�ϼ�)
        for (index = 0; index < splendidalfonsino.Length; index++)
        {
            splendidalfonsino[index] = Instantiate(splendidalfonsinoPrefab);
            splendidalfonsino[index].transform.parent = distinctionObject[13].transform;
            splendidalfonsino[index].SetActive(false);
        }
        // �δ�������(�糪��)
        for (index = 0; index < bigskate.Length; index++)
        {
            bigskate[index] = Instantiate(bigskatePrefab);
            bigskate[index].transform.parent = distinctionObject[14].transform;
            bigskate[index].SetActive(false);
        }
        // Ȳ�ٶ���(����)
        for (index = 0; index < yellowfintuna.Length; index++)
        {
            yellowfintuna[index] = Instantiate(yellowfintunaPrefab);
            yellowfintuna[index].transform.parent = distinctionObject[15].transform;
            yellowfintuna[index].SetActive(false);
        }
        // ���뷡��(����)
        for (index = 0; index < lingcod.Length; index++)
        {
            lingcod[index] = Instantiate(lingcodPrefab);
            lingcod[index].transform.parent = distinctionObject[16].transform;
            lingcod[index].SetActive(false);
        }
        // ��ġ(������Ʈ)
        for (index = 0; index < pacificsaury.Length; index++)
        {
            pacificsaury[index] = Instantiate(pacificsauryPrefab);
            pacificsaury[index].transform.parent = distinctionObject[17].transform;
            pacificsaury[index].SetActive(false);
        }
        // �Ը�ġ(�ϼ�, �糪��)
        for (index = 0; index < gnomefish.Length; index++)
        {
            gnomefish[index] = Instantiate(gnomefishPrefab);
            gnomefish[index].transform.parent = distinctionObject[18].transform;
            gnomefish[index].SetActive(false);
        }
        // ���(������Ʈ, �ϼ�)
        for (index = 0; index < spotlinedsardine.Length; index++)
        {
            spotlinedsardine[index] = Instantiate(spotlinedsardinePrefab);
            spotlinedsardine[index].transform.parent = distinctionObject[19].transform;
            spotlinedsardine[index].SetActive(false);
        }
        // ����(�޽�����, ������Ʈ)
        for (index = 0; index < alaskapollack.Length; index++)
        {
            alaskapollack[index] = Instantiate(alaskapollackPrefab);
            alaskapollack[index].transform.parent = distinctionObject[20].transform;
            alaskapollack[index].SetActive(false);
        }
        // �뱸(�ϼ�, ������)
        for (index = 0; index < pacificcod.Length; index++)
        {
            pacificcod[index] = Instantiate(pacificcodPrefab);
            pacificcod[index].transform.parent = distinctionObject[21].transform;
            pacificcod[index].SetActive(false);
        }
        // ���뱸(�޽�����, ����)
        for (index = 0; index < sablefish.Length; index++)
        {
            sablefish[index] = Instantiate(sablefishPrefab);
            sablefish[index].transform.parent = distinctionObject[22].transform;
            sablefish[index].SetActive(false);
        }
        // ���ð��ڹ�(�糪��)
        for (index = 0; index < yellowfinsole.Length; index++)
        {
            yellowfinsole[index] = Instantiate(yellowfinsolePrefab);
            yellowfinsole[index].transform.parent = distinctionObject[23].transform;
            yellowfinsole[index].SetActive(false);
        }
        // ����(�޽�����, ����)
        for (index = 0; index < bluefingurnard.Length; index++)
        {
            bluefingurnard[index] = Instantiate(bluefingurnardPrefab);
            bluefingurnard[index].transform.parent = distinctionObject[24].transform;
            bluefingurnard[index].SetActive(false);
        }
        // ����(������Ʈ)
        for (index = 0; index < chumsalmon.Length; index++)
        {
            chumsalmon[index] = Instantiate(chumsalmonPrefab);
            chumsalmon[index].transform.parent = distinctionObject[25].transform;
            chumsalmon[index].SetActive(false);
        }
        // ���ֺ�(�ϼ�, �糪��, ������Ʈ)
        for (index = 0; index < japanesepufferfish.Length; index++)
        {
            japanesepufferfish[index] = Instantiate(japanesepufferfishPrefab);
            japanesepufferfish[index].transform.parent = distinctionObject[26].transform;
            japanesepufferfish[index].SetActive(false);
        }
        // ������췰(�ϼ�, ������)
        for (index = 0; index < yelloweyerockfish.Length; index++)
        {
            yelloweyerockfish[index] = Instantiate(yelloweyerockfishPrefab);
            yelloweyerockfish[index].transform.parent = distinctionObject[27].transform;
            yelloweyerockfish[index].SetActive(false);
        }
        // ��ħ�췰(������Ʈ, �糪��)
        for (index = 0; index < quillbackrockfish.Length; index++)
        {
            quillbackrockfish[index] = Instantiate(quillbackrockfishPrefab);
            quillbackrockfish[index].transform.parent = distinctionObject[28].transform;
            quillbackrockfish[index].SetActive(false);
        }
        // �ǻ��(����)
        for (index = 0; index < salmonshark.Length; index++)
        {
            salmonshark[index] = Instantiate(salmonsharkPrefab);
            salmonshark[index].transform.parent = distinctionObject[29].transform;
            salmonshark[index].SetActive(false);
        }
        // �������ڹ�(�糪��)
        for (index = 0; index < halibut.Length; index++)
        {
            halibut[index] = Instantiate(halibutPrefab);
            halibut[index].transform.parent = distinctionObject[30].transform;
            halibut[index].SetActive(false);
        }
        // �տ���(������Ʈ)
        for (index = 0; index < chinooksalmon.Length; index++)
        {
            chinooksalmon[index] = Instantiate(chinooksalmonPrefab);
            chinooksalmon[index].transform.parent = distinctionObject[31].transform;
            chinooksalmon[index].SetActive(false);
        }
    }
    private void ActiveFish(int worldTime)
    {
        int i; // for���� ����
        int count; // �������� �޾ƿ� ����
        int choiceType; // ȸ���� ����� Ÿ�� ����
        int choice; // ȸ���� ����� ���ÿ� ����
        int choiceTotal; // ȸ���� ����� �� ���� ����
        // ȸ����(���) Ȯ�ο� ����
        bool isPacificsaury, isGnomefish, isSpotlinedsardine;
        // ȸ����(�Ϲ�) Ȯ�ο� ����
        bool isAlaskapollack, isPacificcod, isSablefish, isYellowfinsole, isBluefingurnard, isChumsalmon, isJapanesepufferfish;

        //bool isRockfish, isQuillback, isSalmonshark, isHalibut, isKingsalmon;
        int rand;
        switch (worldTime)
        {
            #region #6�� ������
            case 6:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 15 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 30 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 33 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 37 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 67 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 97 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 10); //Debug.Log("���" + rand);
                        //Debug.Log("���" + rand);
                        switch (rand)
                        {
                            case 0:
                                // ������췰
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                                }
                                break;
                            case 1:
                            case 2:
                                // ��ħ�췰
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                                }
                                break;
                            case 3:
                            case 4:
                                // �������ڹ�
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                                }
                                break;
                            default:
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                                }
                                break;
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for(choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                                //Debug.Log("��ġ");
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 15 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 30 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 33 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 37 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 67 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 97 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 10); //Debug.Log("���" + rand);
                        //Debug.Log("���" + rand);
                        switch (rand)
                        {
                            case 0:
                                // ������췰
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                                }
                                break;
                            case 1:
                            case 2:
                                // ��ħ�췰
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                                }
                                break;
                            case 3:
                            case 4:
                                // �������ڹ�
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                                }
                                break;
                            default:
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #7�� ������
            case 7:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #8�� ������
            case 8:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #9�� ������
            case 9:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #10�� ������
            case 10:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #11�� ������
            case 11:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #12�� ������
            case 12:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #13�� ������
            case 13:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #14�� ������
            case 14:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #15�� ������
            case 15:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #16�� ������
            case 16:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #17�� ������
            case 17:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 36 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 63 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 68 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 73 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #18�� ������
            case 18:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 20 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 30 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 60 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 20)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 55)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 95)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 5 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 10 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 20 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 30 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 60 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 20)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 55)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 95)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #19�� ������
            case 19:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #20�� ������
            case 20:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #21�� ������
            case 21:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #22�� ������
            case 22:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #23�� ������
            case 23:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #0�� ������
            case 0:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion           
            #region #1�� ������
            case 1:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #2�� ������
            case 2:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #3�� ������
            case 3:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #4�� ������
            case 4:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #5�� ������
            case 5:
                // ��ȸ����(���)
                // �⸧���ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // �������ڹ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // ��ȫ��ġ
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // �����ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // ������ü���
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // ���ٰ��
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // ȫ�صչٸ�
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // ��ȸ����(�Ϲ�)
                // ȭ��ġ���ڹ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // ����ġ���ڹ�
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // �幮����
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // �̸�ġ
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // �ںӵ�
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // ���ݴ���
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // ��ȸ����(���)
                // �δ�������
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // Ȳ�ٶ���
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // ���뷡��
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // ���1 �Ϲ�3 ���1
                    case 0:
                        // ȸ����(���)
                        choice = Random.Range(0, 10);
                        // ��ġ
                        if (choice < 3)
                        {
                            //Debug.Log("��ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // �Ը�ġ
                        else if (choice < 6)
                        {
                            //Debug.Log("�Ը�ġ");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // ���
                        else
                        {
                            //Debug.Log("���");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // ���2 �Ϲ�2 ���1
                    case 1:
                        // ȸ����(���)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // ��ġ
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("��ġ");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // �Ը�ġ
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("�Ը�ġ");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // ���
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("���");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // ȸ����(�Ϲ�)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("�Ϲ�" + choice);
                            // ����
                            if (choice < 30 && !isAlaskapollack)
                            {
                                choiceTotal++;
                                isAlaskapollack = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    alaskapollack[i].SetActive(true); alaskapollack[i].GetComponent<FishHomerspit>().SetData(32);
                                }
                            }
                            // �뱸
                            else if (choice < 60 && !isPacificcod)
                            {
                                choiceTotal++;
                                isPacificcod = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificcod[i].SetActive(true); pacificcod[i].GetComponent<FishHomerspit>().SetData(49);
                                }
                            }
                            // ���뱸
                            else if (choice < 70 && !isSablefish)
                            {
                                choiceTotal++;
                                isSablefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    sablefish[i].SetActive(true); sablefish[i].GetComponent<FishHomerspit>().SetData(55);
                                }
                            }
                            // ���ð��ڹ�
                            else if (choice < 80 && !isYellowfinsole)
                            {
                                choiceTotal++;
                                isYellowfinsole = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowfinsole[i].SetActive(true); yellowfinsole[i].GetComponent<FishHomerspit>().SetData(62);
                                }
                            }
                            // ����
                            else if (choice < 85 && !isBluefingurnard)
                            {
                                choiceTotal++;
                                isBluefingurnard = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<FishHomerspit>().SetData(36);
                                }
                            }
                            // ����
                            else if (choice < 90 && !isChumsalmon)
                            {
                                choiceTotal++;
                                isChumsalmon = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    chumsalmon[i].SetActive(true); chumsalmon[i].GetComponent<FishHomerspit>().SetData(39);
                                }
                            }
                            // ���ֺ�
                            else if (choice < 100 && !isJapanesepufferfish)
                            {
                                choiceTotal++;
                                isJapanesepufferfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    japanesepufferfish[i].SetActive(true); japanesepufferfish[i].GetComponent<FishHomerspit>().SetData(44);
                                }
                            }
                        }
                        // ȸ����(���)
                        rand = Random.Range(0, 100); //Debug.Log("���" + rand);
                        if (rand < 30)
                        {
                            // ������췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // ��ħ�췰
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // �ǻ��
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // �������ڹ�
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // �տ���
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                }
                break;
            #endregion
            default:
                worldTime = 0;
                break;
        }


        InfluencedPastebaitFishCheck();
        CheckFishAccordingToMovement();
        RareFishCheck();
        DataManager.INSTANCE.CheckBaitProbability();
        
        InitializeFishSearchRange();

        _gameManager.IsNeedleMoving = false;
        _isIncreaseWhenMove = false;
        _isIncreaseWhenStill = false;

        if (DataManager.INSTANCE._matchGameIsInProgress)
        {
            if (_fishingBot == null)
            {
                _fishingBot = GameObject.FindGameObjectWithTag("FishingBot").GetComponent<FishingBot>();
                _fishingBot.CheckFish();
            }
        }
    }
    void RareFishCheck()
    {
        _rareFishList = new List<GameObject>();

        // �δ�������
        for (int i = 0; i < bigskate.Length; i++)
        {
            if (!bigskate[i].activeSelf)
                continue;

            _rareFishList.Add(bigskate[i]);
        }
        // Ȳ�ٶ���
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (!yellowfintuna[i].activeSelf)
                continue;

            _rareFishList.Add(yellowfintuna[i]);
        }
        // ���뷡��
        for (int i = 0; i < lingcod.Length; i++)
        {
            if (!lingcod[i].activeSelf)
                continue;

            _rareFishList.Add(lingcod[i]);
        }
        // ������췰
        for (int i = 0; i < yelloweyerockfish.Length; i++)
        {
            if (!yelloweyerockfish[i].activeSelf)
                continue;

            _rareFishList.Add(yelloweyerockfish[i]);
        }
        // ��ħ�췰
        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (!quillbackrockfish[i].activeSelf)
                continue;

            _rareFishList.Add(quillbackrockfish[i]);
        }
        // �ǻ��
        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (!salmonshark[i].activeSelf)
                continue;

            _rareFishList.Add(salmonshark[i]);
        }
        // �������ڹ�
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            _rareFishList.Add(halibut[i]);
        }
        // �տ���
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (!chinooksalmon[i].activeSelf)
                continue;

            _rareFishList.Add(chinooksalmon[i]);
        }


    }

    public void RadarSort()
    {
        // z �Ÿ��� ���� �����صд�.
        _rareFishList.Sort(delegate (GameObject f1, GameObject f2)
        {
            return f1.transform.position.z.CompareTo(f2.transform.position.z);
        });
    }
    public void RemoveRareFishList(GameObject fishObject)
    {
        int index = _rareFishList.IndexOf(fishObject);

        if (index.Equals(-1))
            return;

        _rareFishList.RemoveAt(index);
    }
    void InfluencedPastebaitFishCheck()
    {
        _influencedFishListByKrill = new List<GameObject>();

        for (int i = 0; i < broadbandedthornyhead.Length; i++)
        {
            if (broadbandedthornyhead[i].activeSelf)
                _influencedFishListByKrill.Add(broadbandedthornyhead[i]);
            else
                continue;
        }
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (yellowfintuna[i].activeSelf)
                _influencedFishListByKrill.Add(yellowfintuna[i]);
            else
                continue;
        }
        for (int i = 0; i < pacificsaury.Length; i++)
        {
            if (pacificsaury[i].activeSelf)
                _influencedFishListByKrill.Add(pacificsaury[i]);
            else
                continue;
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (gnomefish[i].activeSelf)
                _influencedFishListByKrill.Add(gnomefish[i]);
            else
                continue;
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (spotlinedsardine[i].activeSelf)
                _influencedFishListByKrill.Add(spotlinedsardine[i]);
            else
                continue;
        }
        for (int i = 0; i < alaskapollack.Length; i++)
        {
            if (alaskapollack[i].activeSelf)
                _influencedFishListByKrill.Add(alaskapollack[i]);
            else
                continue;
        }
        for (int i = 0; i < pacificcod.Length; i++)
        {
            if (pacificcod[i].activeSelf)
                _influencedFishListByKrill.Add(pacificcod[i]);
            else
                continue;
        }
        for (int i = 0; i < sablefish.Length; i++)
        {
            if (sablefish[i].activeSelf)
                _influencedFishListByKrill.Add(sablefish[i]);
            else
                continue;
        }
        for (int i = 0; i < yellowfinsole.Length; i++)
        {
            if (yellowfinsole[i].activeSelf)
                _influencedFishListByKrill.Add(yellowfinsole[i]);
            else
                continue;
        }
        for (int i = 0; i < chumsalmon.Length; i++)
        {
            if (chumsalmon[i].activeSelf)
                _influencedFishListByKrill.Add(chumsalmon[i]);
            else
                continue;
        }
        for (int i = 0; i < japanesepufferfish.Length; i++)
        {
            if (japanesepufferfish[i].activeSelf)
                _influencedFishListByKrill.Add(japanesepufferfish[i]);
            else
                continue;
        }
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (chinooksalmon[i].activeSelf)
                _influencedFishListByKrill.Add(chinooksalmon[i]);
            else
                continue;
        }
    }

    void CheckFishAccordingToMovement()
    {
        _homerspitfishListIncreaseProbabilityWhenStill = new List<FishHomerspit>();
        _homerspitfishListIncreaseProbabilityWhenMove = new List<FishHomerspit>();

        // �����ӿ� ���� Ȯ���� �����ϴ� ������
        for (int i = 0; i < broadbandedthornyhead.Length; i++)
        {
            if (broadbandedthornyhead[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(broadbandedthornyhead[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < salmonsnailfish.Length; i++)
        {
            if (salmonsnailfish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(salmonsnailfish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < sailfinpoacher.Length; i++)
        {
            if (sailfinpoacher[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(sailfinpoacher[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < manybandedsole.Length; i++)
        {
            if (manybandedsole[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(manybandedsole[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < dragonpoacher.Length; i++)
        {
            if (dragonpoacher[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(dragonpoacher[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < redtippedgrouper.Length; i++)
        {
            if (redtippedgrouper[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(redtippedgrouper[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < kamchatkaflounder.Length; i++)
        {
            if (kamchatkaflounder[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(kamchatkaflounder[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < flatheadsole.Length; i++)
        {
            if (flatheadsole[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(flatheadsole[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < pacificoceanperch.Length; i++)
        {
            if (pacificoceanperch[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(pacificoceanperch[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < beringwolffish.Length; i++)
        {
            if (beringwolffish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(beringwolffish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < lavenderjobfish.Length; i++)
        {
            if (lavenderjobfish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(lavenderjobfish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < splendidalfonsino.Length; i++)
        {
            if (splendidalfonsino[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(splendidalfonsino[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < bigskate.Length; i++)
        {
            if (bigskate[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(bigskate[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (yellowfintuna[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(yellowfintuna[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < lingcod.Length; i++)
        {
            if (lingcod[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(lingcod[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < pacificsaury.Length; i++)
        {
            if (pacificsaury[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(pacificsaury[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (gnomefish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(gnomefish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (spotlinedsardine[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(spotlinedsardine[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < alaskapollack.Length; i++)
        {
            if (alaskapollack[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(alaskapollack[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < pacificcod.Length; i++)
        {
            if (pacificcod[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(pacificcod[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < sablefish.Length; i++)
        {
            if (sablefish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(sablefish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < yellowfinsole.Length; i++)
        {
            if (yellowfinsole[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(yellowfinsole[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (bluefingurnard[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(bluefingurnard[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < chumsalmon.Length; i++)
        {
            if (chumsalmon[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(chumsalmon[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < japanesepufferfish.Length; i++)
        {
            if (japanesepufferfish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(japanesepufferfish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < yelloweyerockfish.Length; i++)
        {
            if (yelloweyerockfish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(yelloweyerockfish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (quillbackrockfish[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(quillbackrockfish[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (salmonshark[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(salmonshark[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < halibut.Length; i++)
        {
            if (halibut[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(halibut[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (chinooksalmon[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenMove.Add(chinooksalmon[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        // ������ ���� �� Ȯ���� �����ϴ� ������
        for (int i = 0; i < blackfinflounder.Length; i++)
        {
            if (blackfinflounder[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenStill.Add(blackfinflounder[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        for (int i = 0; i < rocksole.Length; i++)
        {
            if (rocksole[i].activeSelf)
                _homerspitfishListIncreaseProbabilityWhenStill.Add(rocksole[i].GetComponent<FishHomerspit>());
            else
                continue;
        }

        //IncreaseProbabilityAccordingToMovement(true);
        _isFirst = false;
    }

    public void UpdateProbilityWhenLampOn() // ���� ON
    {
        // ���ٸ�
        for (int i = 0; i < blackfinflounder.Length; i++)
        {
            if (!blackfinflounder[i].activeSelf)
                continue;

            if(blackfinflounder[i].GetComponent<FishHomerspit>().SearchRange > 0)
                blackfinflounder[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < rocksole.Length; i++)
        {
            if (!rocksole[i].activeSelf)
                continue;

            if (rocksole[i].GetComponent<FishHomerspit>().SearchRange > 0)
                rocksole[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < broadbandedthornyhead.Length; i++)
        {
            if (!broadbandedthornyhead[i].activeSelf)
                continue;

            if (broadbandedthornyhead[i].GetComponent<FishHomerspit>().SearchRange > 0)
                broadbandedthornyhead[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < salmonsnailfish.Length; i++)
        {
            if (!salmonsnailfish[i].activeSelf)
                continue;

            if (salmonsnailfish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                salmonsnailfish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < sailfinpoacher.Length; i++)
        {
            if (!sailfinpoacher[i].activeSelf)
                continue;

            if (sailfinpoacher[i].GetComponent<FishHomerspit>().SearchRange > 0)
                sailfinpoacher[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < manybandedsole.Length; i++)
        {
            if (!manybandedsole[i].activeSelf)
                continue;

            if (manybandedsole[i].GetComponent<FishHomerspit>().SearchRange > 0)
                manybandedsole[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < dragonpoacher.Length; i++)
        {
            if (!dragonpoacher[i].activeSelf)
                continue;

            if (dragonpoacher[i].GetComponent<FishHomerspit>().SearchRange > 0)
                dragonpoacher[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < redtippedgrouper.Length; i++)
        {
            if (!redtippedgrouper[i].activeSelf)
                continue;

            if (redtippedgrouper[i].GetComponent<FishHomerspit>().SearchRange > 0)
                redtippedgrouper[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < kamchatkaflounder.Length; i++)
        {
            if (!kamchatkaflounder[i].activeSelf)
                continue;

            if (kamchatkaflounder[i].GetComponent<FishHomerspit>().SearchRange > 0)
                kamchatkaflounder[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < flatheadsole.Length; i++)
        {
            if (!flatheadsole[i].activeSelf)
                continue;

            if (flatheadsole[i].GetComponent<FishHomerspit>().SearchRange > 0)
                flatheadsole[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < pacificoceanperch.Length; i++)
        {
            if (!pacificoceanperch[i].activeSelf)
                continue;

            if (pacificoceanperch[i].GetComponent<FishHomerspit>().SearchRange > 0)
                pacificoceanperch[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < beringwolffish.Length; i++)
        {
            if (!beringwolffish[i].activeSelf)
                continue;

            if (beringwolffish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                beringwolffish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < lavenderjobfish.Length; i++)
        {
            if (!lavenderjobfish[i].activeSelf)
                continue;

            if (lavenderjobfish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                lavenderjobfish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < bigskate.Length; i++)
        {
            if (!bigskate[i].activeSelf)
                continue;

            if (bigskate[i].GetComponent<FishHomerspit>().SearchRange > 0)
                bigskate[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (!yellowfintuna[i].activeSelf)
                continue;

            if (yellowfintuna[i].GetComponent<FishHomerspit>().SearchRange > 0)
                yellowfintuna[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < lingcod.Length; i++)
        {
            if (!lingcod[i].activeSelf)
                continue;

            if (lingcod[i].GetComponent<FishHomerspit>().SearchRange > 0)
                lingcod[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < pacificsaury.Length; i++)
        {
            if (!pacificsaury[i].activeSelf)
                continue;

            if (pacificsaury[i].GetComponent<FishHomerspit>().SearchRange > 0)
                pacificsaury[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;

            if (gnomefish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                gnomefish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (!spotlinedsardine[i].activeSelf)
                continue;

            if (spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange > 0)
                spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange -= 1; ;
        }
        for (int i = 0; i < alaskapollack.Length; i++)
        {
            if (!alaskapollack[i].activeSelf)
                continue;

            if (alaskapollack[i].GetComponent<FishHomerspit>().SearchRange > 0)
                alaskapollack[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < pacificcod.Length; i++)
        {
            if (!pacificcod[i].activeSelf)
                continue;

            if (pacificcod[i].GetComponent<FishHomerspit>().SearchRange > 0)
                pacificcod[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < sablefish.Length; i++)
        {
            if (!sablefish[i].activeSelf)
                continue;

            if (sablefish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                sablefish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < yellowfinsole.Length; i++)
        {
            if (!yellowfinsole[i].activeSelf)
                continue;

            if (yellowfinsole[i].GetComponent<FishHomerspit>().SearchRange > 0)
                yellowfinsole[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;

            if (bluefingurnard[i].GetComponent<FishHomerspit>().SearchRange > 0)
                bluefingurnard[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < chumsalmon.Length; i++)
        {
            if (!chumsalmon[i].activeSelf)
                continue;

            if (chumsalmon[i].GetComponent<FishHomerspit>().SearchRange > 0)
                chumsalmon[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < japanesepufferfish.Length; i++)
        {
            if (!japanesepufferfish[i].activeSelf)
                continue;

            if (japanesepufferfish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                japanesepufferfish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (!quillbackrockfish[i].activeSelf)
                continue;

            if (quillbackrockfish[i].GetComponent<FishHomerspit>().SearchRange > 0)
                quillbackrockfish[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (!salmonshark[i].activeSelf)
                continue;

            if (salmonshark[i].GetComponent<FishHomerspit>().SearchRange > 0)
                salmonshark[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            if (halibut[i].GetComponent<FishHomerspit>().SearchRange > 0)
                halibut[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (!chinooksalmon[i].activeSelf)
                continue;

            if (chinooksalmon[i].GetComponent<FishHomerspit>().SearchRange > 0)
                chinooksalmon[i].GetComponent<FishHomerspit>().SearchRange -= 1;
        }
    }
    public void UpdateProbilityWhenLampOff() // ���� OFF
    {
        for (int i = 0; i < blackfinflounder.Length; i++)
        {
            if (!blackfinflounder[i].activeSelf)
                continue;

            blackfinflounder[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < rocksole.Length; i++)
        {
            if (!rocksole[i].activeSelf)
                continue;

            rocksole[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < broadbandedthornyhead.Length; i++)
        {
            if (!broadbandedthornyhead[i].activeSelf)
                continue;

            broadbandedthornyhead[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < salmonsnailfish.Length; i++)
        {
            if (!salmonsnailfish[i].activeSelf)
                continue;

            salmonsnailfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < sailfinpoacher.Length; i++)
        {
            if (!sailfinpoacher[i].activeSelf)
                continue;

            sailfinpoacher[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < manybandedsole.Length; i++)
        {
            if (!manybandedsole[i].activeSelf)
                continue;

            manybandedsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < dragonpoacher.Length; i++)
        {
            if (!dragonpoacher[i].activeSelf)
                continue;

            dragonpoacher[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < redtippedgrouper.Length; i++)
        {
            if (!redtippedgrouper[i].activeSelf)
                continue;

            redtippedgrouper[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < kamchatkaflounder.Length; i++)
        {
            if (!kamchatkaflounder[i].activeSelf)
                continue;

            kamchatkaflounder[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < flatheadsole.Length; i++)
        {
            if (!flatheadsole[i].activeSelf)
                continue;

            flatheadsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < pacificoceanperch.Length; i++)
        {
            if (!pacificoceanperch[i].activeSelf)
                continue;

            pacificoceanperch[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < beringwolffish.Length; i++)
        {
            if (!beringwolffish[i].activeSelf)
                continue;

            beringwolffish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < lavenderjobfish.Length; i++)
        {
            if (!lavenderjobfish[i].activeSelf)
                continue;

            lavenderjobfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < bigskate.Length; i++)
        {
            if (!bigskate[i].activeSelf)
                continue;

            bigskate[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (!yellowfintuna[i].activeSelf)
                continue;

            yellowfintuna[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < lingcod.Length; i++)
        {
            if (!lingcod[i].activeSelf)
                continue;

            lingcod[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < pacificsaury.Length; i++)
        {
            if (!pacificsaury[i].activeSelf)
                continue;

            pacificsaury[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;

            gnomefish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (!spotlinedsardine[i].activeSelf)
                continue;

            spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (!spotlinedsardine[i].activeSelf)
                continue;

            spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < alaskapollack.Length; i++)
        {
            if (!alaskapollack[i].activeSelf)
                continue;

            alaskapollack[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < pacificcod.Length; i++)
        {
            if (!pacificcod[i].activeSelf)
                continue;

            pacificcod[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < sablefish.Length; i++)
        {
            if (!sablefish[i].activeSelf)
                continue;

            sablefish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < yellowfinsole.Length; i++)
        {
            if (!yellowfinsole[i].activeSelf)
                continue;

            yellowfinsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;

            bluefingurnard[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < chumsalmon.Length; i++)
        {
            if (!chumsalmon[i].activeSelf)
                continue;

            chumsalmon[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < japanesepufferfish.Length; i++)
        {
            if (!japanesepufferfish[i].activeSelf)
                continue;

            japanesepufferfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (!quillbackrockfish[i].activeSelf)
                continue;

            quillbackrockfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (!salmonshark[i].activeSelf)
                continue;

            salmonshark[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            halibut[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (!chinooksalmon[i].activeSelf)
                continue;

            chinooksalmon[i].GetComponent<FishHomerspit>().SearchRange += 1;
        }
    }
    public void UpdateRangeWhenThrowPastebait(float leftX, float rightX, float upZ, float downZ) // ����
    {
        GameObject[] fishs;
        int cnt = 0;
        switch (DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"])
        {
            case 1: // ũ������
                fishs = new GameObject[_influencedFishListByKrill.Count];
                for (int i = 0; i < _influencedFishListByKrill.Count; i++)
                {
                    if (_influencedFishListByKrill[i].transform.position.x > leftX && _influencedFishListByKrill[i].transform.position.x < rightX
                        && _influencedFishListByKrill[i].transform.position.z < upZ && _influencedFishListByKrill[i].transform.position.z > downZ)
                    {
                        fishs[cnt] = _influencedFishListByKrill[i].gameObject;
                        cnt++;
                    }
                }
                if (fishs.Length > 0)
                    PlusBaitProbability(fishs, 1);
                break;
        }
    }
    void PlusBaitProbability(GameObject[] fishObject, int addRate) // �̳� �� Ȯ�� ++
    {
        //Debug.LogError(fishObject.Length);
        for (int i = 0; i < fishObject.Length; i++)
        {
            if (fishObject[i] != null)
            {
                fishObject[i].GetComponent<FishHomerspit>().StartIncreaseSearchRangeOneMinute(addRate);
            }
            else
                break;
        }
    }
    public void IncreaseProbabilityAccordingToMovement(bool isMoving)
    {
        if (isMoving)
        {
            if (!_isIncreaseWhenMove)
            {
                for (int i = 0; i < _homerspitfishListIncreaseProbabilityWhenMove.Count; i++)
                {

                    _homerspitfishListIncreaseProbabilityWhenMove[i].biteBait += 10f;
                }
                _isIncreaseWhenMove = true;
            }

            if (_isIncreaseWhenStill)
            {
                for (int i = 0; i < _homerspitfishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _homerspitfishListIncreaseProbabilityWhenStill[i].biteBait -= 10f;
                }
                _isIncreaseWhenStill = false;
            }
        }
        else
        {
            if (_isIncreaseWhenMove)
            {
                for (int i = 0; i < _homerspitfishListIncreaseProbabilityWhenMove.Count; i++)
                {
                    _homerspitfishListIncreaseProbabilityWhenMove[i].biteBait -= 10f;
                }
                _isIncreaseWhenMove = false;
            }

            if (!_isIncreaseWhenStill)
            {
                for (int i = 0; i < _homerspitfishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _homerspitfishListIncreaseProbabilityWhenStill[i].biteBait += 10f;
                }
                _isIncreaseWhenStill = true;
            }
        }
    }

    void AssignmentFishs()
    {
        blackfinflounder = new GameObject[26]; // �⸧���ڹ�
        rocksole = new GameObject[26];  // �������ڹ�
        broadbandedthornyhead = new GameObject[18]; // ȫ��ġ
        salmonsnailfish = new GameObject[18];  // ��ȫ��ġ
        sailfinpoacher = new GameObject[18];  // �����ٰ��
        manybandedsole = new GameObject[18];  // ������ü���
        dragonpoacher = new GameObject[18];   // ���ٰ��
        redtippedgrouper = new GameObject[18];   // ȫ�صչٸ�

        kamchatkaflounder = new GameObject[13];   // ȭ��ġ���ڹ�
        flatheadsole = new GameObject[19];   // ����ġ���ڹ�
        pacificoceanperch = new GameObject[13];  // �幮����
        beringwolffish = new GameObject[18];   // �̸�ġ
        lavenderjobfish = new GameObject[13];   // �ںӵ�
        splendidalfonsino = new GameObject[19];   // ���ݴ���

        bigskate = new GameObject[5];  // �δ�������
        yellowfintuna = new GameObject[5];    // Ȳ�ٶ���
        lingcod = new GameObject[5];  // ���뷡��

        pacificsaury = new GameObject[30];  // ��ġ
        gnomefish = new GameObject[30]; // �Ը�ġ
        spotlinedsardine = new GameObject[30];    // ���

        alaskapollack = new GameObject[30]; // ����
        pacificcod = new GameObject[30];    // �뱸
        sablefish = new GameObject[30]; // ���뱸
        yellowfinsole = new GameObject[30]; // ���ð��ڹ�
        bluefingurnard = new GameObject[30];   // ����
        chumsalmon = new GameObject[30]; // ����
        japanesepufferfish = new GameObject[30];  // ���ֺ�

        yelloweyerockfish = new GameObject[10];    // ������췰
        quillbackrockfish = new GameObject[10];  // ��ħ�췰
        salmonshark = new GameObject[10];    // �ǻ��
        halibut = new GameObject[10];  // �������ڹ�
        chinooksalmon = new GameObject[10];  // �տ���
    }

    public void UpdateFishSearchRange(int range)
    {
        for (int i = 0; i < blackfinflounder.Length; i++)
        {
            if (!blackfinflounder[i].activeSelf)
                continue;

            blackfinflounder[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
            //Debug.Log("blackfinflounder : " + blackfinflounder[i].GetComponent<FishHomerspit>().SearchRange);
        }
        
        for (int i = 0; i < rocksole.Length; i++)
        {
            if (!rocksole[i].activeSelf)
                continue;

            rocksole[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < broadbandedthornyhead.Length; i++)
        {
            if (!broadbandedthornyhead[i].activeSelf)
                continue;

            broadbandedthornyhead[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < salmonsnailfish.Length; i++)
        {
            if (!salmonsnailfish[i].activeSelf)
                continue;

            salmonsnailfish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < sailfinpoacher.Length; i++)
        {
            if (!sailfinpoacher[i].activeSelf)
                continue;

            sailfinpoacher[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < manybandedsole.Length; i++)
        {
            if (!manybandedsole[i].activeSelf)
                continue;

            manybandedsole[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < dragonpoacher.Length; i++)
        {
            if (!dragonpoacher[i].activeSelf)
                continue;

            dragonpoacher[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < redtippedgrouper.Length; i++)
        {
            if (!redtippedgrouper[i].activeSelf)
                continue;

            redtippedgrouper[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < kamchatkaflounder.Length; i++)
        {
            if (!kamchatkaflounder[i].activeSelf)
                continue;

            kamchatkaflounder[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < flatheadsole.Length; i++)
        {
            if (!flatheadsole[i].activeSelf)
                continue;

            flatheadsole[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < pacificoceanperch.Length; i++)
        {
            if (!pacificoceanperch[i].activeSelf)
                continue;

            pacificoceanperch[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < beringwolffish.Length; i++)
        {
            if (!beringwolffish[i].activeSelf)
                continue;

            beringwolffish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < lavenderjobfish.Length; i++)
        {
            if (!lavenderjobfish[i].activeSelf)
                continue;

            lavenderjobfish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < bigskate.Length; i++)
        {
            if (!bigskate[i].activeSelf)
                continue;

            bigskate[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (!yellowfintuna[i].activeSelf)
                continue;

            yellowfintuna[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < lingcod.Length; i++)
        {
            if (!lingcod[i].activeSelf)
                continue;

            lingcod[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < pacificsaury.Length; i++)
        {
            if (!pacificsaury[i].activeSelf)
                continue;

            pacificsaury[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;

            gnomefish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (!spotlinedsardine[i].activeSelf)
                continue;

            spotlinedsardine[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < spotlinedsardine.Length; i++)
        {
            if (!spotlinedsardine[i].activeSelf)
                continue;

            spotlinedsardine[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < alaskapollack.Length; i++)
        {
            if (!alaskapollack[i].activeSelf)
                continue;

            alaskapollack[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < pacificcod.Length; i++)
        {
            if (!pacificcod[i].activeSelf)
                continue;

            pacificcod[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < sablefish.Length; i++)
        {
            if (!sablefish[i].activeSelf)
                continue;

            sablefish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < yellowfinsole.Length; i++)
        {
            if (!yellowfinsole[i].activeSelf)
                continue;

            yellowfinsole[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;

            bluefingurnard[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < chumsalmon.Length; i++)
        {
            if (!chumsalmon[i].activeSelf)
                continue;

            chumsalmon[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < japanesepufferfish.Length; i++)
        {
            if (!japanesepufferfish[i].activeSelf)
                continue;

            japanesepufferfish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (!quillbackrockfish[i].activeSelf)
                continue;

            quillbackrockfish[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (!salmonshark[i].activeSelf)
                continue;

            salmonshark[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            halibut[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (!chinooksalmon[i].activeSelf)
                continue;

            chinooksalmon[i].GetComponent<FishHomerspit>().SettingSearchRange(range);
        }

        LampCheck();
    }

    void LampCheck()
    {
        if(!_petManager.isLightOn)
        {
            for (int i = 0; i < blackfinflounder.Length; i++)
            {
                if (!blackfinflounder[i].activeSelf)
                    continue;

                blackfinflounder[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < rocksole.Length; i++)
            {
                if (!rocksole[i].activeSelf)
                    continue;

                rocksole[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < broadbandedthornyhead.Length; i++)
            {
                if (!broadbandedthornyhead[i].activeSelf)
                    continue;

                broadbandedthornyhead[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < salmonsnailfish.Length; i++)
            {
                if (!salmonsnailfish[i].activeSelf)
                    continue;

                salmonsnailfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < sailfinpoacher.Length; i++)
            {
                if (!sailfinpoacher[i].activeSelf)
                    continue;

                sailfinpoacher[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < manybandedsole.Length; i++)
            {
                if (!manybandedsole[i].activeSelf)
                    continue;

                manybandedsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < dragonpoacher.Length; i++)
            {
                if (!dragonpoacher[i].activeSelf)
                    continue;

                dragonpoacher[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < redtippedgrouper.Length; i++)
            {
                if (!redtippedgrouper[i].activeSelf)
                    continue;

                redtippedgrouper[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < kamchatkaflounder.Length; i++)
            {
                if (!kamchatkaflounder[i].activeSelf)
                    continue;

                kamchatkaflounder[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < flatheadsole.Length; i++)
            {
                if (!flatheadsole[i].activeSelf)
                    continue;

                flatheadsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < pacificoceanperch.Length; i++)
            {
                if (!pacificoceanperch[i].activeSelf)
                    continue;

                pacificoceanperch[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < beringwolffish.Length; i++)
            {
                if (!beringwolffish[i].activeSelf)
                    continue;

                beringwolffish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < lavenderjobfish.Length; i++)
            {
                if (!lavenderjobfish[i].activeSelf)
                    continue;

                lavenderjobfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < bigskate.Length; i++)
            {
                if (!bigskate[i].activeSelf)
                    continue;

                bigskate[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < yellowfintuna.Length; i++)
            {
                if (!yellowfintuna[i].activeSelf)
                    continue;

                yellowfintuna[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < lingcod.Length; i++)
            {
                if (!lingcod[i].activeSelf)
                    continue;

                lingcod[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < pacificsaury.Length; i++)
            {
                if (!pacificsaury[i].activeSelf)
                    continue;

                pacificsaury[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < gnomefish.Length; i++)
            {
                if (!gnomefish[i].activeSelf)
                    continue;

                gnomefish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < spotlinedsardine.Length; i++)
            {
                if (!spotlinedsardine[i].activeSelf)
                    continue;

                spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < spotlinedsardine.Length; i++)
            {
                if (!spotlinedsardine[i].activeSelf)
                    continue;

                spotlinedsardine[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < alaskapollack.Length; i++)
            {
                if (!alaskapollack[i].activeSelf)
                    continue;

                alaskapollack[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < pacificcod.Length; i++)
            {
                if (!pacificcod[i].activeSelf)
                    continue;

                pacificcod[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < sablefish.Length; i++)
            {
                if (!sablefish[i].activeSelf)
                    continue;

                sablefish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < yellowfinsole.Length; i++)
            {
                if (!yellowfinsole[i].activeSelf)
                    continue;

                yellowfinsole[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < bluefingurnard.Length; i++)
            {
                if (!bluefingurnard[i].activeSelf)
                    continue;

                bluefingurnard[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < chumsalmon.Length; i++)
            {
                if (!chumsalmon[i].activeSelf)
                    continue;

                chumsalmon[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < japanesepufferfish.Length; i++)
            {
                if (!japanesepufferfish[i].activeSelf)
                    continue;

                japanesepufferfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < quillbackrockfish.Length; i++)
            {
                if (!quillbackrockfish[i].activeSelf)
                    continue;

                quillbackrockfish[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < salmonshark.Length; i++)
            {
                if (!salmonshark[i].activeSelf)
                    continue;

                salmonshark[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < halibut.Length; i++)
            {
                if (!halibut[i].activeSelf)
                    continue;

                halibut[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
            for (int i = 0; i < chinooksalmon.Length; i++)
            {
                if (!chinooksalmon[i].activeSelf)
                    continue;

                chinooksalmon[i].GetComponent<FishHomerspit>().SearchRange += 1;
            }
        }
    }
}