using System.Collections.Generic;
using UnityEngine;

public class FishObjectManagerHomerspit : MonoBehaviour
{
    public PetManager _petManager;
    public GameManager _gameManager;

    #region 물고기 프리펩 변수
    // 비회유어종 - 잡어(8) 17 15 131 221
    public GameObject blackfinflounderPrefab;   // 기름가자미
    public GameObject rocksolePrefab;  // 까지가자미
    public GameObject broadbandedthornyheadPrefab;   // 홍살치
    public GameObject salmonsnailfishPrefab;    // 분홍꼼치
    public GameObject sailfinpoacherPrefab;   // 날개줄고기
    public GameObject manybandedsolePrefab;   // 노랑각시서대
    public GameObject dragonpoacherPrefab; // 네줄고기
    public GameObject redtippedgrouperPrefab;  // 홍밑둥바리
    // 비회유어종 - 일반(6)
    public GameObject kamchatkaflounderPrefab;  // 화살치가자미
    public GameObject flatheadsolePrefab;  // 마소치가자미
    public GameObject pacificoceanperchPrefab;    // 장문볼락
    public GameObject beringwolffishPrefab;  // 이리치
    public GameObject lavenderjobfishPrefab;  // 자붉돔
    public GameObject splendidalfonsinoPrefab; // 빚금눈돔
    // 비회유어종 - 희귀(3)
    public GameObject bigskatePrefab;   // 두눈가오리
    public GameObject yellowfintunaPrefab; // 황다랑어
    public GameObject lingcodPrefab;   // 범노래미
    // 회유어종 - 잡어(3)
    public GameObject pacificsauryPrefab;   // 꽁치
    public GameObject gnomefishPrefab;   // 게르치
    public GameObject spotlinedsardinePrefab;  // 정어리
    // 회유어종 - 일반(7)
    public GameObject alaskapollackPrefab;   // 명태
    public GameObject pacificcodPrefab;  // 대구
    public GameObject sablefishPrefab;   // 은대구
    public GameObject yellowfinsolePrefab;   // 각시가자미
    public GameObject bluefingurnardPrefab; // 성대
    public GameObject chumsalmonPrefab;   // 연어
    public GameObject japanesepufferfishPrefab;    // 자주복
    // 회유어종 - 희귀(5)
    public GameObject yelloweyerockfishPrefab;    // 노랑눈우럭
    public GameObject quillbackrockfishPrefab;    // 등침우럭
    public GameObject salmonsharkPrefab;    // 악상어
    public GameObject halibutPrefab;    // 마설가자미
    public GameObject chinooksalmonPrefab;    // 왕연어
    #endregion
    #region 물고기 마리수 지정
    // 비회유어종 - 잡어(8)
    [HideInInspector] public GameObject[] blackfinflounder = new GameObject[26]; // 기름가자미
    [HideInInspector] public GameObject[] rocksole = new GameObject[26];  // 까지가자미
    [HideInInspector] public GameObject[] broadbandedthornyhead = new GameObject[18]; // 홍살치
    [HideInInspector] public GameObject[] salmonsnailfish = new GameObject[18];  // 분홍꼼치
    [HideInInspector] public GameObject[] sailfinpoacher = new GameObject[18];  // 날개줄고기
    [HideInInspector] public GameObject[] manybandedsole = new GameObject[18];  // 노랑각시서대
    [HideInInspector] public GameObject[] dragonpoacher = new GameObject[18];   // 네줄고기
    [HideInInspector] public GameObject[] redtippedgrouper = new GameObject[18];   // 홍밑둥바리
    // 비회유어종 - 일반(6)
    [HideInInspector] public GameObject[] kamchatkaflounder = new GameObject[13];   // 화살치가자미
    [HideInInspector] public GameObject[] flatheadsole = new GameObject[19];   // 마소치가자미
    [HideInInspector] public GameObject[] pacificoceanperch = new GameObject[13];  // 장문볼락
    [HideInInspector] public GameObject[] beringwolffish = new GameObject[18];   // 이리치
    [HideInInspector] public GameObject[] lavenderjobfish = new GameObject[13];   // 자붉돔
    [HideInInspector] public GameObject[] splendidalfonsino = new GameObject[19];   // 빚금눈돔
    // 비회유어종 - 희귀(3)
    [HideInInspector] public GameObject[] bigskate = new GameObject[5];  // 두눈가오리
    [HideInInspector] public GameObject[] yellowfintuna = new GameObject[5];    // 황다랑어
    [HideInInspector] public GameObject[] lingcod = new GameObject[5];  // 범노래미
    // 회유어종 - 잡어(3)
    [HideInInspector] public GameObject[] pacificsaury = new GameObject[30];  // 꽁치
    [HideInInspector] public GameObject[] gnomefish = new GameObject[30]; // 게르치
    [HideInInspector] public GameObject[] spotlinedsardine = new GameObject[30];    // 정어리
    // 회유어종 - 일반(7)
    [HideInInspector] public GameObject[] alaskapollack = new GameObject[30]; // 명태
    [HideInInspector] public GameObject[] pacificcod = new GameObject[30];    // 대구
    [HideInInspector] public GameObject[] sablefish = new GameObject[30]; // 은대구
    [HideInInspector] public GameObject[] yellowfinsole = new GameObject[30]; // 각시가자미
    [HideInInspector] public GameObject[] bluefingurnard = new GameObject[30];   // 성대
    [HideInInspector] public GameObject[] chumsalmon = new GameObject[30]; // 연어
    [HideInInspector] public GameObject[] japanesepufferfish = new GameObject[30];  // 자주복
    // 회유어종 - 희귀(5)    
    [HideInInspector] public GameObject[] yelloweyerockfish = new GameObject[10];    // 노랑눈우럭
    [HideInInspector] public GameObject[] quillbackrockfish = new GameObject[10];  // 등침우럭
    [HideInInspector] public GameObject[] salmonshark = new GameObject[10];    // 악상어
    [HideInInspector] public GameObject[] halibut = new GameObject[10];  // 마설가자미
    [HideInInspector] public GameObject[] chinooksalmon = new GameObject[10];  // 왕연어
    #endregion

    public Transform[] distinctionObject; // 리스폰을 위한 물고기별 부모오브젝트(빈오브젝트) 설정

    // 레이더를 위한 레어 물고기 리스트
    [HideInInspector] public List<GameObject> _rareFishList = new List<GameObject>();

    // 각 떡밥에 영향을 받는 물고기 리스트
    // 크릴
    [HideInInspector] public List<GameObject> _influencedFishListByKrill = new List<GameObject>();

    // 움직임에 따라 확률이 증가하는 물고기 리스트
    // 가만히
    [HideInInspector] public List<FishHomerspit> _homerspitfishListIncreaseProbabilityWhenStill;
    // 움직임
    [HideInInspector] public List<FishHomerspit> _homerspitfishListIncreaseProbabilityWhenMove;

    bool _isIncreaseWhenMove = false;
    bool _isIncreaseWhenStill = false;

    [HideInInspector] public bool _isFirst;
    [HideInInspector] public List<FishHomerspit> _caughtFishs = new List<FishHomerspit>();
    // 대전 모드일 때 FishingBot에 물고기가 다 생성되었음을 알려주기 위해
    FishingBot _fishingBot;

    private void Awake()
    {
        _isFirst = true;
        AssignmentFishs();
        CreateFish(); //시작시 풀링을 위한 물고기 생성 함수
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
            bigskate[i].SetActive(false);  // 두눈가오리
            yellowfintuna[i].SetActive(false);    // 황다랑어
            lingcod[i].SetActive(false);  // 범노래미
        }
        for (int i = 0; i < 10; i++)
        {
            yelloweyerockfish[i].SetActive(false);     // 노랑눈우럭
            quillbackrockfish[i].SetActive(false);   // 등침우럭
            salmonshark[i].SetActive(false);     // 악상어
            halibut[i].SetActive(false);   // 마설가자미
            chinooksalmon[i].SetActive(false);  // 왕연어
        }

        for (int i = 0; i < 13; i++)
        {
            kamchatkaflounder[i].SetActive(false);  // 화살치가자미
            pacificoceanperch[i].SetActive(false); // 장문볼락
            lavenderjobfish[i].SetActive(false);  // 자붉돔
        }

        for (int i = 0; i < 18; i++)
        {
            broadbandedthornyhead[i].SetActive(false); // 홍살치
            salmonsnailfish[i].SetActive(false);  // 분홍꼼치
            sailfinpoacher[i].SetActive(false);  // 날개줄고기
            manybandedsole[i].SetActive(false); // 노랑각시서대
            dragonpoacher[i].SetActive(false);  // 네줄고기
            redtippedgrouper[i].SetActive(false);   // 홍밑둥바리
            beringwolffish[i].SetActive(false);   // 이리치
        }
        for (int i = 0; i < 19; i++)
        {
            flatheadsole[i].SetActive(false);   // 마소치가자미
            splendidalfonsino[i].SetActive(false);  // 빚금눈돔
        }
        for (int i = 0; i < 26; i++)
        {
            blackfinflounder[i].SetActive(false);  // 기름가자미
            rocksole[i].SetActive(false);   // 까지가자미
        }
        for (int i = 0; i < 30; i++)
        {
            pacificsaury[i].SetActive(false);   // 꽁치
            gnomefish[i].SetActive(false);  // 게르치
            spotlinedsardine[i].SetActive(false);     // 정어리
            alaskapollack[i].SetActive(false);  // 명태
            pacificcod[i].SetActive(false);     // 대구
            sablefish[i].SetActive(false);  // 은대구
            yellowfinsole[i].SetActive(false);  // 각시가자미
            bluefingurnard[i].SetActive(false);   // 성대
            chumsalmon[i].SetActive(false);  // 연어
            japanesepufferfish[i].SetActive(false);  // 자주복
        }
        ActiveFish(worldTime);
    }
    // 마릿수 선택 0 ~ 10
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
            // default는 필요없음
            default:
                count = 1;
                break;
        }
        return count;
    }
    void CreateFish() // 물고기 생성함수
    {
        int index;
        // 기름가자미(암석, 사나질)
        for (index = 0; index < blackfinflounder.Length; index++)
        {
            blackfinflounder[index] = Instantiate(blackfinflounderPrefab);
            blackfinflounder[index].transform.parent = distinctionObject[0].transform;
            blackfinflounder[index].SetActive(false);
        }
        // 까지가자미(암석, 사나질)
        for (index = 0; index < rocksole.Length; index++)
        {
            rocksole[index] = Instantiate(rocksolePrefab);
            rocksole[index].transform.parent = distinctionObject[1].transform;
            rocksole[index].SetActive(false);
        }
        // 홍살치(끝포인트, 사나질)
        for (index = 0; index < broadbandedthornyhead.Length; index++)
        {
            broadbandedthornyhead[index] = Instantiate(broadbandedthornyheadPrefab);
            broadbandedthornyhead[index].transform.parent = distinctionObject[2].transform;
            broadbandedthornyhead[index].SetActive(false);
        }
        // 분홍꼼치(사나질)
        for (index = 0; index < salmonsnailfish.Length; index++)
        {
            salmonsnailfish[index] = Instantiate(salmonsnailfishPrefab);
            salmonsnailfish[index].transform.parent = distinctionObject[3].transform;
            salmonsnailfish[index].SetActive(false);
        }
        // 날개줄고기(사나질)
        for (index = 0; index < sailfinpoacher.Length; index++)
        {
            sailfinpoacher[index] = Instantiate(sailfinpoacherPrefab);
            sailfinpoacher[index].transform.parent = distinctionObject[4].transform;
            sailfinpoacher[index].SetActive(false);
        }
        // 노랑각시서대(사나질, 끝포인트)
        for (index = 0; index < manybandedsole.Length; index++)
        {
            manybandedsole[index] = Instantiate(manybandedsolePrefab);
            manybandedsole[index].transform.parent = distinctionObject[5].transform;
            manybandedsole[index].SetActive(false);
        }
        // 네줄고기(암석, 해조류)
        for (index = 0; index < dragonpoacher.Length; index++)
        {
            dragonpoacher[index] = Instantiate(dragonpoacherPrefab);
            dragonpoacher[index].transform.parent = distinctionObject[6].transform;
            dragonpoacher[index].SetActive(false);
        }
        // 홍밑둥바리(암초, 해조류)
        for (index = 0; index < redtippedgrouper.Length; index++)
        {
            redtippedgrouper[index] = Instantiate(redtippedgrouperPrefab);
            redtippedgrouper[index].transform.parent = distinctionObject[7].transform;
            redtippedgrouper[index].SetActive(false);
        }
        // 화살치가자미(급심지대, 사나질)
        for (index = 0; index < kamchatkaflounder.Length; index++)
        {
            kamchatkaflounder[index] = Instantiate(kamchatkaflounderPrefab);
            kamchatkaflounder[index].transform.parent = distinctionObject[8].transform;
            kamchatkaflounder[index].SetActive(false);
        }
        // 마소치가자미(사나질)
        for (index = 0; index < flatheadsole.Length; index++)
        {
            flatheadsole[index] = Instantiate(flatheadsolePrefab);
            flatheadsole[index].transform.parent = distinctionObject[9].transform;
            flatheadsole[index].SetActive(false);
        }
        // 장문볼락(끝포인트)
        for (index = 0; index < pacificoceanperch.Length; index++)
        {
            pacificoceanperch[index] = Instantiate(pacificoceanperchPrefab);
            pacificoceanperch[index].transform.parent = distinctionObject[10].transform;
            pacificoceanperch[index].SetActive(false);
        }
        // 이리치(암석, 끝포인트)
        for (index = 0; index < beringwolffish.Length; index++)
        {
            beringwolffish[index] = Instantiate(beringwolffishPrefab);
            beringwolffish[index].transform.parent = distinctionObject[11].transform;
            beringwolffish[index].SetActive(false);
        }
        // 자붉돔(암석, 해조류)
        for (index = 0; index < lavenderjobfish.Length; index++)
        {
            lavenderjobfish[index] = Instantiate(lavenderjobfishPrefab);
            lavenderjobfish[index].transform.parent = distinctionObject[12].transform;
            lavenderjobfish[index].SetActive(false);
        }
        // 빚금눈돔(암석)
        for (index = 0; index < splendidalfonsino.Length; index++)
        {
            splendidalfonsino[index] = Instantiate(splendidalfonsinoPrefab);
            splendidalfonsino[index].transform.parent = distinctionObject[13].transform;
            splendidalfonsino[index].SetActive(false);
        }
        // 두눈가오리(사나질)
        for (index = 0; index < bigskate.Length; index++)
        {
            bigskate[index] = Instantiate(bigskatePrefab);
            bigskate[index].transform.parent = distinctionObject[14].transform;
            bigskate[index].SetActive(false);
        }
        // 황다랑어(물골)
        for (index = 0; index < yellowfintuna.Length; index++)
        {
            yellowfintuna[index] = Instantiate(yellowfintunaPrefab);
            yellowfintuna[index].transform.parent = distinctionObject[15].transform;
            yellowfintuna[index].SetActive(false);
        }
        // 범노래미(물골)
        for (index = 0; index < lingcod.Length; index++)
        {
            lingcod[index] = Instantiate(lingcodPrefab);
            lingcod[index].transform.parent = distinctionObject[16].transform;
            lingcod[index].SetActive(false);
        }
        // 꽁치(끝포인트)
        for (index = 0; index < pacificsaury.Length; index++)
        {
            pacificsaury[index] = Instantiate(pacificsauryPrefab);
            pacificsaury[index].transform.parent = distinctionObject[17].transform;
            pacificsaury[index].SetActive(false);
        }
        // 게르치(암석, 사나질)
        for (index = 0; index < gnomefish.Length; index++)
        {
            gnomefish[index] = Instantiate(gnomefishPrefab);
            gnomefish[index].transform.parent = distinctionObject[18].transform;
            gnomefish[index].SetActive(false);
        }
        // 정어리(끝포인트, 암석)
        for (index = 0; index < spotlinedsardine.Length; index++)
        {
            spotlinedsardine[index] = Instantiate(spotlinedsardinePrefab);
            spotlinedsardine[index].transform.parent = distinctionObject[19].transform;
            spotlinedsardine[index].SetActive(false);
        }
        // 명태(급심지대, 끝포인트)
        for (index = 0; index < alaskapollack.Length; index++)
        {
            alaskapollack[index] = Instantiate(alaskapollackPrefab);
            alaskapollack[index].transform.parent = distinctionObject[20].transform;
            alaskapollack[index].SetActive(false);
        }
        // 대구(암석, 해조류)
        for (index = 0; index < pacificcod.Length; index++)
        {
            pacificcod[index] = Instantiate(pacificcodPrefab);
            pacificcod[index].transform.parent = distinctionObject[21].transform;
            pacificcod[index].SetActive(false);
        }
        // 은대구(급심지대, 물골)
        for (index = 0; index < sablefish.Length; index++)
        {
            sablefish[index] = Instantiate(sablefishPrefab);
            sablefish[index].transform.parent = distinctionObject[22].transform;
            sablefish[index].SetActive(false);
        }
        // 각시가자미(사나질)
        for (index = 0; index < yellowfinsole.Length; index++)
        {
            yellowfinsole[index] = Instantiate(yellowfinsolePrefab);
            yellowfinsole[index].transform.parent = distinctionObject[23].transform;
            yellowfinsole[index].SetActive(false);
        }
        // 성대(급심지대, 물골)
        for (index = 0; index < bluefingurnard.Length; index++)
        {
            bluefingurnard[index] = Instantiate(bluefingurnardPrefab);
            bluefingurnard[index].transform.parent = distinctionObject[24].transform;
            bluefingurnard[index].SetActive(false);
        }
        // 연어(끝포인트)
        for (index = 0; index < chumsalmon.Length; index++)
        {
            chumsalmon[index] = Instantiate(chumsalmonPrefab);
            chumsalmon[index].transform.parent = distinctionObject[25].transform;
            chumsalmon[index].SetActive(false);
        }
        // 자주복(암석, 사나질, 끝포인트)
        for (index = 0; index < japanesepufferfish.Length; index++)
        {
            japanesepufferfish[index] = Instantiate(japanesepufferfishPrefab);
            japanesepufferfish[index].transform.parent = distinctionObject[26].transform;
            japanesepufferfish[index].SetActive(false);
        }
        // 노랑눈우럭(암석, 해조류)
        for (index = 0; index < yelloweyerockfish.Length; index++)
        {
            yelloweyerockfish[index] = Instantiate(yelloweyerockfishPrefab);
            yelloweyerockfish[index].transform.parent = distinctionObject[27].transform;
            yelloweyerockfish[index].SetActive(false);
        }
        // 등침우럭(끝포인트, 사나질)
        for (index = 0; index < quillbackrockfish.Length; index++)
        {
            quillbackrockfish[index] = Instantiate(quillbackrockfishPrefab);
            quillbackrockfish[index].transform.parent = distinctionObject[28].transform;
            quillbackrockfish[index].SetActive(false);
        }
        // 악상어(물골)
        for (index = 0; index < salmonshark.Length; index++)
        {
            salmonshark[index] = Instantiate(salmonsharkPrefab);
            salmonshark[index].transform.parent = distinctionObject[29].transform;
            salmonshark[index].SetActive(false);
        }
        // 마설가자미(사나질)
        for (index = 0; index < halibut.Length; index++)
        {
            halibut[index] = Instantiate(halibutPrefab);
            halibut[index].transform.parent = distinctionObject[30].transform;
            halibut[index].SetActive(false);
        }
        // 왕연어(끝포인트)
        for (index = 0; index < chinooksalmon.Length; index++)
        {
            chinooksalmon[index] = Instantiate(chinooksalmonPrefab);
            chinooksalmon[index].transform.parent = distinctionObject[31].transform;
            chinooksalmon[index].SetActive(false);
        }
    }
    private void ActiveFish(int worldTime)
    {
        int i; // for문용 변수
        int count; // 램던숫자 받아올 변수
        int choiceType; // 회유종 물고기 타입 변수
        int choice; // 회유종 물고기 선택용 변수
        int choiceTotal; // 회유종 물고기 총 종류 변수
        // 회유종(잡어) 확인용 변수
        bool isPacificsaury, isGnomefish, isSpotlinedsardine;
        // 회유종(일반) 확인용 변수
        bool isAlaskapollack, isPacificcod, isSablefish, isYellowfinsole, isBluefingurnard, isChumsalmon, isJapanesepufferfish;

        //bool isRockfish, isQuillback, isSalmonshark, isHalibut, isKingsalmon;
        int rand;
        switch (worldTime)
        {
            #region #6시 리스폰
            case 6:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 10); //Debug.Log("희귀" + rand);
                        //Debug.Log("희귀" + rand);
                        switch (rand)
                        {
                            case 0:
                                // 노랑눈우럭
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                                }
                                break;
                            case 1:
                            case 2:
                                // 등침우럭
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                                }
                                break;
                            case 3:
                            case 4:
                                // 마설가자미
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
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for(choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                                //Debug.Log("꽁치");
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 10); //Debug.Log("희귀" + rand);
                        //Debug.Log("희귀" + rand);
                        switch (rand)
                        {
                            case 0:
                                // 노랑눈우럭
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                                }
                                break;
                            case 1:
                            case 2:
                                // 등침우럭
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                                }
                                break;
                            case 3:
                            case 4:
                                // 마설가자미
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
            #region #7시 리스폰
            case 7:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
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
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
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
                            // 왕연어
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
            #region #8시 리스폰
            case 8:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #9시 리스폰
            case 9:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 86)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 93)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #10시 리스폰
            case 10:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #11시 리스폰
            case 11:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #12시 리스폰
            case 12:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #13시 리스폰
            case 13:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #14시 리스폰
            case 14:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #15시 리스폰
            case 15:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #16시 리스폰
            case 16:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #17시 리스폰
            case 17:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 80)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #18시 리스폰
            case 18:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 20)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 55)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 95)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 20)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 55)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 95)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #19시 리스폰
            case 19:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #20시 리스폰
            case 20:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #21시 리스폰
            case 21:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #22시 리스폰
            case 22:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #23시 리스폰
            case 23:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #0시 리스폰
            case 0:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #1시 리스폰
            case 1:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #2시 리스폰
            case 2:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #3시 리스폰
            case 3:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #4시 리스폰
            case 4:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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
            #region #5시 리스폰
            case 5:
                // 비회유종(잡어)
                // 기름가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackfinflounder[i].SetActive(true); blackfinflounder[i].GetComponent<FishHomerspit>().SetData(35);
                }
                // 까지가자미
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    rocksole[i].SetActive(true); rocksole[i].GetComponent<FishHomerspit>().SetData(54);
                }
                // 홍살치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    broadbandedthornyhead[i].SetActive(true); broadbandedthornyhead[i].GetComponent<FishHomerspit>().SetData(37);
                }
                // 분홍꼼치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    salmonsnailfish[i].SetActive(true); salmonsnailfish[i].GetComponent<FishHomerspit>().SetData(58);
                }
                // 날개줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    sailfinpoacher[i].SetActive(true); sailfinpoacher[i].GetComponent<FishHomerspit>().SetData(56);
                }
                // 노랑각시서대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    manybandedsole[i].SetActive(true); manybandedsole[i].GetComponent<FishHomerspit>().SetData(48);
                }
                // 네줄고기
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    dragonpoacher[i].SetActive(true); dragonpoacher[i].GetComponent<FishHomerspit>().SetData(40);
                }
                // 홍밑둥바리
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redtippedgrouper[i].SetActive(true); redtippedgrouper[i].GetComponent<FishHomerspit>().SetData(53);
                }
                // 비회유종(일반)
                // 화살치가자미
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    kamchatkaflounder[i].SetActive(true); kamchatkaflounder[i].GetComponent<FishHomerspit>().SetData(45);
                }
                // 마소치가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    flatheadsole[i].SetActive(true); flatheadsole[i].GetComponent<FishHomerspit>().SetData(41);
                }
                // 장문볼락
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    pacificoceanperch[i].SetActive(true); pacificoceanperch[i].GetComponent<FishHomerspit>().SetData(50);
                }
                // 이리치
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    beringwolffish[i].SetActive(true); beringwolffish[i].GetComponent<FishHomerspit>().SetData(33);
                }
                // 자붉돔
                count = RandomCount(5);
                for (i = 0; i < count; i++)
                {
                    lavenderjobfish[i].SetActive(true); lavenderjobfish[i].GetComponent<FishHomerspit>().SetData(46);
                }
                // 빚금눈돔
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    splendidalfonsino[i].SetActive(true); splendidalfonsino[i].GetComponent<FishHomerspit>().SetData(59);
                }
                // 비회유종(희귀)
                // 두눈가오리
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigskate[i].SetActive(true); bigskate[i].GetComponent<FishHomerspit>().SetData(34);
                }
                // 황다랑어
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    yellowfintuna[i].SetActive(true); yellowfintuna[i].GetComponent<FishHomerspit>().SetData(63);
                }
                // 범노래미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    lingcod[i].SetActive(true); lingcod[i].GetComponent<FishHomerspit>().SetData(47);
                }
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어1 일반3 희귀1
                    case 0:
                        // 회유종(잡어)
                        choice = Random.Range(0, 10);
                        // 꽁치
                        if (choice < 3)
                        {
                            //Debug.Log("꽁치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                            }
                        }
                        // 게르치
                        else if (choice < 6)
                        {
                            //Debug.Log("게르치");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                            }
                        }
                        // 정어리
                        else
                        {
                            //Debug.Log("정어리");
                            count = RandomCount(10);
                            for (i = 0; i < count; i++)
                            {
                                spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                chinooksalmon[i].SetActive(true); chinooksalmon[i].GetComponent<FishHomerspit>().SetData(38);
                            }
                        }
                        break;
                    // 잡어2 일반2 희귀1
                    case 1:
                        // 회유종(잡어)
                        isPacificsaury = false; isGnomefish = false; isSpotlinedsardine = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 10);

                            // 꽁치
                            if (choice < 3 && !isPacificsaury)
                            {
                                //Debug.Log("꽁치");
                                choiceTotal++;
                                isPacificsaury = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    pacificsaury[i].SetActive(true); pacificsaury[i].GetComponent<FishHomerspit>().SetData(51);
                                }
                            }
                            // 게르치
                            else if (choice < 6 && !isGnomefish)
                            {
                                //Debug.Log("게르치");
                                choiceTotal++;
                                isGnomefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishHomerspit>().SetData(42);
                                }
                            }
                            // 정어리
                            else if (choice < 10 && !isSpotlinedsardine)
                            {
                                //Debug.Log("정어리");
                                choiceTotal++;
                                isSpotlinedsardine = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    spotlinedsardine[i].SetActive(true); spotlinedsardine[i].GetComponent<FishHomerspit>().SetData(60);
                                }
                            }
                        }
                        // 회유종(일반)
                        isAlaskapollack = false; isPacificcod = false; isSablefish = false; isYellowfinsole = false;
                        isBluefingurnard = false; isChumsalmon = false; isJapanesepufferfish = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);
                            //Debug.Log("일반" + choice);
                            // 명태
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
                            // 대구
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
                            // 은대구
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
                            // 각시가자미
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
                            // 성대
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
                            // 연어
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
                            // 자주복
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
                        // 회유종(희귀)
                        rand = Random.Range(0, 100); //Debug.Log("희귀" + rand);
                        if (rand < 30)
                        {
                            // 노랑눈우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                yelloweyerockfish[i].SetActive(true); yelloweyerockfish[i].GetComponent<FishHomerspit>().SetData(61);
                            }
                        }
                        else if (rand < 40)
                        {
                            // 등침우럭
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                quillbackrockfish[i].SetActive(true); quillbackrockfish[i].GetComponent<FishHomerspit>().SetData(52);
                            }
                        }
                        else if (rand < 50)
                        {
                            // 악상어
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                salmonshark[i].SetActive(true); salmonshark[i].GetComponent<FishHomerspit>().SetData(57);
                            }
                        }
                        else if (rand < 90)
                        {
                            // 마설가자미
                            count = RandomCount(4);
                            for (i = 0; i < count; i++)
                            {
                                halibut[i].SetActive(true); halibut[i].GetComponent<FishHomerspit>().SetData(43);
                            }
                        }
                        else
                        {
                            // 왕연어
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

        // 두눈가오리
        for (int i = 0; i < bigskate.Length; i++)
        {
            if (!bigskate[i].activeSelf)
                continue;

            _rareFishList.Add(bigskate[i]);
        }
        // 황다랑어
        for (int i = 0; i < yellowfintuna.Length; i++)
        {
            if (!yellowfintuna[i].activeSelf)
                continue;

            _rareFishList.Add(yellowfintuna[i]);
        }
        // 범노래미
        for (int i = 0; i < lingcod.Length; i++)
        {
            if (!lingcod[i].activeSelf)
                continue;

            _rareFishList.Add(lingcod[i]);
        }
        // 노랑눈우럭
        for (int i = 0; i < yelloweyerockfish.Length; i++)
        {
            if (!yelloweyerockfish[i].activeSelf)
                continue;

            _rareFishList.Add(yelloweyerockfish[i]);
        }
        // 등침우럭
        for (int i = 0; i < quillbackrockfish.Length; i++)
        {
            if (!quillbackrockfish[i].activeSelf)
                continue;

            _rareFishList.Add(quillbackrockfish[i]);
        }
        // 악상어
        for (int i = 0; i < salmonshark.Length; i++)
        {
            if (!salmonshark[i].activeSelf)
                continue;

            _rareFishList.Add(salmonshark[i]);
        }
        // 마설가자미
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            _rareFishList.Add(halibut[i]);
        }
        // 왕연어
        for (int i = 0; i < chinooksalmon.Length; i++)
        {
            if (!chinooksalmon[i].activeSelf)
                continue;

            _rareFishList.Add(chinooksalmon[i]);
        }


    }

    public void RadarSort()
    {
        // z 거리에 따라서 정렬해둔다.
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

        // 움직임에 따라 확률이 증가하는 물고기들
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

        // 가만히 있을 때 확률이 증가하는 물고기들
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

    public void UpdateProbilityWhenLampOn() // 램프 ON
    {
        // 도다리
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
    public void UpdateProbilityWhenLampOff() // 램프 OFF
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
    public void UpdateRangeWhenThrowPastebait(float leftX, float rightX, float upZ, float downZ) // 떡밥
    {
        GameObject[] fishs;
        int cnt = 0;
        switch (DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"])
        {
            case 1: // 크릴떡밥
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
    void PlusBaitProbability(GameObject[] fishObject, int addRate) // 미끼 물 확률 ++
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
        blackfinflounder = new GameObject[26]; // 기름가자미
        rocksole = new GameObject[26];  // 까지가자미
        broadbandedthornyhead = new GameObject[18]; // 홍살치
        salmonsnailfish = new GameObject[18];  // 분홍꼼치
        sailfinpoacher = new GameObject[18];  // 날개줄고기
        manybandedsole = new GameObject[18];  // 노랑각시서대
        dragonpoacher = new GameObject[18];   // 네줄고기
        redtippedgrouper = new GameObject[18];   // 홍밑둥바리

        kamchatkaflounder = new GameObject[13];   // 화살치가자미
        flatheadsole = new GameObject[19];   // 마소치가자미
        pacificoceanperch = new GameObject[13];  // 장문볼락
        beringwolffish = new GameObject[18];   // 이리치
        lavenderjobfish = new GameObject[13];   // 자붉돔
        splendidalfonsino = new GameObject[19];   // 빚금눈돔

        bigskate = new GameObject[5];  // 두눈가오리
        yellowfintuna = new GameObject[5];    // 황다랑어
        lingcod = new GameObject[5];  // 범노래미

        pacificsaury = new GameObject[30];  // 꽁치
        gnomefish = new GameObject[30]; // 게르치
        spotlinedsardine = new GameObject[30];    // 정어리

        alaskapollack = new GameObject[30]; // 명태
        pacificcod = new GameObject[30];    // 대구
        sablefish = new GameObject[30]; // 은대구
        yellowfinsole = new GameObject[30]; // 각시가자미
        bluefingurnard = new GameObject[30];   // 성대
        chumsalmon = new GameObject[30]; // 연어
        japanesepufferfish = new GameObject[30];  // 자주복

        yelloweyerockfish = new GameObject[10];    // 노랑눈우럭
        quillbackrockfish = new GameObject[10];  // 등침우럭
        salmonshark = new GameObject[10];    // 악상어
        halibut = new GameObject[10];  // 마설가자미
        chinooksalmon = new GameObject[10];  // 왕연어
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