using System.Collections.Generic;
using UnityEngine;

public class FishObjectManagerSkyway : MonoBehaviour
{
    public PetManager _petManager;
    public GameManager _gameManager;

    #region 물고기 프리펩 변수
    // 비회유어종 - 잡어(6) 18 17(5) 221 320
    public GameObject floridapompanoPrefab;   // 병어돔
    public GameObject lagoontriggerfishPrefab;  // 피카소피쉬
    public GameObject redlionfishPrefab;   // 점쏠배감펭
    public GameObject hogfishPrefab;    // 돼지물고기
    public GameObject gulfflounderPrefab;   // 걸프가자미
    public GameObject bonefishPrefab;   // 여을멸
    // 비회유어종 - 일반(8)
    public GameObject redsnapperPrefab; // 속임금눈돔
    public GameObject atlanticwreckfishPrefab;  // 대서양레크피쉬
    public GameObject japaneseamberjackPrefab;  // 방어
    public GameObject wahooPrefab;  // 꼬리삼치
    public GameObject halibutPrefab;    // 마설가자미
    public GameObject mahimahiPrefab;  // 만새기
    public GameObject sheepsheadPrefab;  // 양머리돔
    public GameObject blackporgyPrefab; // 감성돔
    // 비회유어종 - 희귀(4)
    public GameObject blackgrouperPrefab;   // 블랙그루퍼
    public GameObject atlantictripletailPrefab; // 백미돔
    public GameObject redporgyPrefab;   // 적돔
    public GameObject redtilefishPrefab;   // 옥돔
    // 회유어종 - 잡어(8)
    public GameObject cobiaPrefab;  // 날새기
    public GameObject kingmackerelPrefab;   // 왕고등어
    public GameObject commonsnookPrefab;  // 스눅
    public GameObject seatroutPrefab;   // 갈색송어
    public GameObject gnomefishPrefab;   // 게르치
    public GameObject weakfishPrefab;   // 개꼴고기
    public GameObject soinymulletPrefab; // 가숭어
    public GameObject mackerelPrefab;   // 고등어
    // 회유어종 - 일반(7)
    public GameObject atlanticspanishmackerelPrefab;    // 대서양삼치
    public GameObject blackdrumPrefab;    // 블랙드럼
    public GameObject redstingrayPrefab;    // 노랑가오리
    public GameObject atlanticcodPrefab;    // 대서양대구
    public GameObject stripedbassPrefab;    // 줄농어
    public GameObject seabassPrefab;    // 농어
    public GameObject yellowtailamberjackPrefab; // 부시리
    // 회유어종 - 희귀(2)
    public GameObject indopacificsailfishPrefab;    // 돛새치
    public GameObject swordfishPrefab;  // 황새치
    #endregion
    #region 물고기 마리수 지정
    // 비회유어종 - 잡어(6)
    [HideInInspector] public GameObject[] floridapompano = new GameObject[26]; // 병어돔
    [HideInInspector] public GameObject[] lagoontriggerfish = new GameObject[18];  // 피카소피쉬
    [HideInInspector] public GameObject[] redlionfish = new GameObject[26]; // 점쏠배감펭
    [HideInInspector] public GameObject[] hogfish = new GameObject[26];  // 돼지물고기
    [HideInInspector] public GameObject[] gulfflounder = new GameObject[26];  // 걸프가자미
    [HideInInspector] public GameObject[] bonefish = new GameObject[18];  // 여을멸
    // 비회유어종 - 일반(8)
    [HideInInspector] public GameObject[] redsnapper = new GameObject[18];   // 속임금눈돔
    [HideInInspector] public GameObject[] atlanticwreckfish = new GameObject[18];   // 대서양레크피쉬
    [HideInInspector] public GameObject[] japaneseamberjack = new GameObject[12];   // 방어
    [HideInInspector] public GameObject[] wahoo = new GameObject[18];    // 꼬치삼치
    [HideInInspector] public GameObject[] halibut = new GameObject[18];  // 마설가자미
    [HideInInspector] public GameObject[] mahimahi = new GameObject[18];   // 만새기
    [HideInInspector] public GameObject[] sheepshead = new GameObject[18];   // 양머리돔
    [HideInInspector] public GameObject[] blackporgy = new GameObject[12];   // 감성돔

    // 비회유어종 - 희귀(4)
    [HideInInspector] public GameObject[] blackgrouper = new GameObject[5];  // 블랙그루퍼
    [HideInInspector] public GameObject[] atlantictripletail = new GameObject[5];    // 백미돔
    [HideInInspector] public GameObject[] redporgy = new GameObject[5];  // 적돔
    [HideInInspector] public GameObject[] redtilefish = new GameObject[5];  // 옥돔

    // 회유어종 - 잡어(8)
    [HideInInspector] public GameObject[] cobia = new GameObject[30];    // 날새기
    [HideInInspector] public GameObject[] kingmackerel = new GameObject[30]; // 왕고등어
    [HideInInspector] public GameObject[] commonsnook = new GameObject[30];    // 스눅
    [HideInInspector] public GameObject[] seatrout = new GameObject[30]; // 갈색송어
    [HideInInspector] public GameObject[] gnomefish = new GameObject[30]; // 게르치
    [HideInInspector] public GameObject[] weakfish = new GameObject[30]; // 개꼴고기
    [HideInInspector] public GameObject[] soinymullet = new GameObject[30];   // 가숭어
    [HideInInspector] public GameObject[] mackerel = new GameObject[30]; // 고등어
    // 회유어종 - 일반(7)
    [HideInInspector] public GameObject[] atlanticspanishmackerel = new GameObject[30];  // 대서양삼치
    [HideInInspector] public GameObject[] blackdrum = new GameObject[30];    // 블랙드럼
    [HideInInspector] public GameObject[] redstingray = new GameObject[30];  // 노랑가오리
    [HideInInspector] public GameObject[] atlanticcod = new GameObject[30];    // 대서양대구
    [HideInInspector] public GameObject[] stripedbass = new GameObject[30];  // 줄농어
    [HideInInspector] public GameObject[] seabass = new GameObject[30];  // 농어
    [HideInInspector] public GameObject[] yellowtailamberjack = new GameObject[30];   // 부시리
    // 회유어종 - 희귀(2)    
    [HideInInspector] public GameObject[] indopacificsailfish = new GameObject[10];  // 돛새치
    [HideInInspector] public GameObject[] swordfish = new GameObject[10];    // 황새치
    #endregion

    //public Transform[] spawnPos; // 0. 끝포인트 1. 급심지대 2. 암초 3. 사나질  4. 해조류 5. 물골
    public Transform[] distinctionObject; // 리스폰을 위한 물고기별 부모오브젝트(빈오브젝트) 설정

    // 레이더를 위한 레어 물고기 리스트
    [HideInInspector] public List<GameObject> _rareFishList = new List<GameObject>();
    // 각 떡밥에 영향을 받는 물고기 리스트
    // 크릴
    [HideInInspector] public List<GameObject> _influencedFishListByKrill = new List<GameObject>();

    // 움직임에 따라 확률이 증가하는 물고기 리스트
    // 가만히
    [HideInInspector] public List<FishSkyway> _fishListIncreaseProbabilityWhenStill = new List<FishSkyway>();
    // 움직임
    [HideInInspector] public List<FishSkyway> _fishListIncreaseProbabilityWhenMove = new List<FishSkyway>();

    bool _isIncreaseWhenMove = false;
    bool _isIncreaseWhenStill = false;

    [HideInInspector] public bool _isFirst;
    [HideInInspector] public List<FishSkyway> _caughtFishs = new List<FishSkyway>();

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
        if (_gameManager.UserData == null)
            _gameManager.UserData = DBManager.INSTANCE.GetUserData();

        if (_gameManager.UserData.GetCurrentEquipmentDictionary()["bait"].Equals(-1))
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

            _caughtFishs = new List<FishSkyway>();
        }

        for (int i = 0; i < 5; i++)
        {
            blackgrouper[i].SetActive(false);  // 블랙그루퍼
            atlantictripletail[i].SetActive(false);    // 백미돔
            redporgy[i].SetActive(false);  // 적돔
            redtilefish[i].SetActive(false);  // 옥돔
        }
        for (int i = 0; i < 10; i++)
        {
            indopacificsailfish[i].SetActive(false); // 돛새치
            swordfish[i].SetActive(false);  // 황새치
        }
        for (int i = 0; i < 12; i++)
        {

            japaneseamberjack[i].SetActive(false);  // 방어
            blackporgy[i].SetActive(false);  // 감성돔
        }
        for (int i = 0; i < 18; i++)
        {
            sheepshead[i].SetActive(false);  // 양머리돔
            lagoontriggerfish[i].SetActive(false); // 피카소피쉬
            redsnapper[i].SetActive(false);   // 속임금눈돔
            wahoo[i].SetActive(false);    // 꼬치삼치
            bonefish[i].SetActive(false);  // 여을멸
            atlanticwreckfish[i].SetActive(false);   // 대서양레크피쉬
            halibut[i].SetActive(false); // 마설가자미
            mahimahi[i].SetActive(false);   // 만새기
        }
        for (int i = 0; i < 26; i++)
        {
            floridapompano[i].SetActive(false); // 병어돔
            hogfish[i].SetActive(false);  // 돼지물고기
            redlionfish[i].SetActive(false);  // 점쏠배감펭
            gulfflounder[i].SetActive(false);  // 걸프가자미
        }
        for (int i = 0; i < 30; i++)
        {
            cobia[i].SetActive(false);   // 날새기
            kingmackerel[i].SetActive(false); // 왕고등어
            commonsnook[i].SetActive(false);  // 스눅
            seatrout[i].SetActive(false); // 갈색송어
            gnomefish[i].SetActive(false); // 게르치
            weakfish[i].SetActive(false); // 개꼴고기
            soinymullet[i].SetActive(false);  // 가숭어
            mackerel[i].SetActive(false); // 고등어
            atlanticspanishmackerel[i].SetActive(false);  // 대서양삼치
            blackdrum[i].SetActive(false);   // 블랙드럼
            redstingray[i].SetActive(false);  // 노랑가오리
            atlanticcod[i].SetActive(false);    // 대서양대구
            stripedbass[i].SetActive(false);  // 줄농어
            seabass[i].SetActive(false);  // 농어
            yellowtailamberjack[i].SetActive(false);   // 부시리  
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
                count = Random.Range(0, 7);
                break;
            case 2:
                count = Random.Range(1, 6);
                break;
            case 3:
                count = Random.Range(2, 11);
                break;
            case 4:
                count = Random.Range(1, 4);
                break;
            case 5:
                count = Random.Range(5, 11);
                break;
            case 6:
                count = Random.Range(6, 13);
                break;
            case 7:
                count = Random.Range(10, 19);
                break;
            case 8:
                count = Random.Range(12, 19);
                break;
            case 9:
                count = Random.Range(18, 27);
                break;
            case 10:
                count = Random.Range(20, 31);
                break;
            case 11:
                count = Random.Range(2, 12);
                break;
            case 12:
                count = Random.Range(2, 13);
                break;
            case 13:
                count = Random.Range(2, 14);
                break;
            case 14:
                count = Random.Range(2, 15);
                break;
            case 15:
                count = Random.Range(2, 16);
                break;
            case 16:
                count = Random.Range(2, 17);
                break;
            case 17:
                count = Random.Range(2, 18);
                break;
            case 18:
                count = Random.Range(2, 19);
                break;
            case 19:
                count = Random.Range(2, 20);
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
        // 병어돔(암초, 해조류)
        for (index = 0; index < floridapompano.Length; index++)
        {
            floridapompano[index] = Instantiate(floridapompanoPrefab);
            floridapompano[index].transform.parent = distinctionObject[0].transform;
            floridapompano[index].SetActive(false);
        }
        // 피카소피쉬(해조류(산호초))
        for (index = 0; index < lagoontriggerfish.Length; index++)
        {
            lagoontriggerfish[index] = Instantiate(lagoontriggerfishPrefab);
            lagoontriggerfish[index].transform.parent = distinctionObject[1].transform;
            lagoontriggerfish[index].SetActive(false);
        }
        // 점쏠배감펭(끝포인트, 해조류, 암초)
        for (index = 0; index < redlionfish.Length; index++)
        {
            redlionfish[index] = Instantiate(redlionfishPrefab);
            redlionfish[index].transform.parent = distinctionObject[2].transform;
            redlionfish[index].SetActive(false);
        }
        // 돼지물고기(암초, 해조류)
        for (index = 0; index < hogfish.Length; index++)
        {
            hogfish[index] = Instantiate(hogfishPrefab);
            hogfish[index].transform.parent = distinctionObject[3].transform;
            hogfish[index].SetActive(false);
        }
        // 걸프가자미(급심지대, 사나질)
        for (index = 0; index < gulfflounder.Length; index++)
        {
            gulfflounder[index] = Instantiate(gulfflounderPrefab);
            gulfflounder[index].transform.parent = distinctionObject[4].transform;
            gulfflounder[index].SetActive(false);
        }
        // 여을멸(끝포인트, 암초, 해조류)
        for (index = 0; index < bonefish.Length; index++)
        {
            bonefish[index] = Instantiate(bonefishPrefab);
            bonefish[index].transform.parent = distinctionObject[5].transform;
            bonefish[index].SetActive(false);
        }
        // 속임금눈돔(해조류, 암초)
        for (index = 0; index < redsnapper.Length; index++)
        {
            redsnapper[index] = Instantiate(redsnapperPrefab);
            redsnapper[index].transform.parent = distinctionObject[6].transform;
            redsnapper[index].SetActive(false);
        }
        // 대서양레크피쉬(끝포인트, 암초, 해조류)
        for (index = 0; index < atlanticwreckfish.Length; index++)
        {
            atlanticwreckfish[index] = Instantiate(atlanticwreckfishPrefab);
            atlanticwreckfish[index].transform.parent = distinctionObject[7].transform;
            atlanticwreckfish[index].SetActive(false);
        }
        // 방어(물골)
        for (index = 0; index < japaneseamberjack.Length; index++)
        {
            japaneseamberjack[index] = Instantiate(japaneseamberjackPrefab);
            japaneseamberjack[index].transform.parent = distinctionObject[8].transform;
            japaneseamberjack[index].SetActive(false);
        }
        // 꼬치삼치(급심지대)
        for (index = 0; index < wahoo.Length; index++)
        {
            wahoo[index] = Instantiate(wahooPrefab);
            wahoo[index].transform.parent = distinctionObject[9].transform;
            wahoo[index].SetActive(false);
        }
        // 마설가자미 (급심지대)
        for (index = 0; index < halibut.Length; index++)
        {
            halibut[index] = Instantiate(halibutPrefab);
            halibut[index].transform.parent = distinctionObject[10].transform;
            halibut[index].SetActive(false);
        }
        // 만새기(해조류, 물골)
        for (index = 0; index < mahimahi.Length; index++)
        {
            mahimahi[index] = Instantiate(mahimahiPrefab);
            mahimahi[index].transform.parent = distinctionObject[11].transform;
            mahimahi[index].SetActive(false);
        }
        //Debug.Log(sheepshead.Length);
        // 양머리돔(암초)
        for (index = 0; index < sheepshead.Length; index++)
        {
            sheepshead[index] = Instantiate(sheepsheadPrefab);
            sheepshead[index].transform.parent = distinctionObject[12].transform;
            sheepshead[index].SetActive(false);
        }

        // Debug.Log(sheepshead.Length);

        // 감성돔(암초)
        for (index = 0; index < blackporgy.Length; index++)
        {
            blackporgy[index] = Instantiate(blackporgyPrefab);
            blackporgy[index].transform.parent = distinctionObject[13].transform;
            blackporgy[index].SetActive(false);
        }
        // 블랙그루퍼(해조류, 암초)
        for (index = 0; index < blackgrouper.Length; index++)
        {
            blackgrouper[index] = Instantiate(blackgrouperPrefab);
            blackgrouper[index].transform.parent = distinctionObject[14].transform;
            blackgrouper[index].SetActive(false);
        }
        // 백미돔(해조류)
        for (index = 0; index < atlantictripletail.Length; index++)
        {
            atlantictripletail[index] = Instantiate(atlantictripletailPrefab);
            atlantictripletail[index].transform.parent = distinctionObject[15].transform;
            atlantictripletail[index].SetActive(false);
        }
        // 적돔(암초)
        for (index = 0; index < redporgy.Length; index++)
        {

            redporgy[index] = Instantiate(redporgyPrefab);
            redporgy[index].transform.parent = distinctionObject[16].transform;
            redporgy[index].SetActive(false);
        }
        // 옥돔(사나질)
        for (index = 0; index < redtilefish.Length; index++)
        {

            redtilefish[index] = Instantiate(redtilefishPrefab);
            redtilefish[index].transform.parent = distinctionObject[17].transform;
            redtilefish[index].SetActive(false);
        }
        // 날새기(사나질, 암초, 물골)
        for (index = 0; index < cobia.Length; index++)
        {
            cobia[index] = Instantiate(cobiaPrefab);
            cobia[index].transform.parent = distinctionObject[18].transform;
            cobia[index].SetActive(false);
        }
        // 왕고등어(물골)
        for (index = 0; index < kingmackerel.Length; index++)
        {
            kingmackerel[index] = Instantiate(kingmackerelPrefab);
            kingmackerel[index].transform.parent = distinctionObject[19].transform;
            kingmackerel[index].SetActive(false);
        }
        // 스눅(끝포인트)
        for (index = 0; index < commonsnook.Length; index++)
        {
            commonsnook[index] = Instantiate(commonsnookPrefab);
            commonsnook[index].transform.parent = distinctionObject[20].transform;
            commonsnook[index].SetActive(false);
        }
        // 갈색송어(암초)
        for (index = 0; index < seatrout.Length; index++)
        {
            seatrout[index] = Instantiate(seatroutPrefab);
            seatrout[index].transform.parent = distinctionObject[21].transform;
            seatrout[index].SetActive(false);
        }
        // 게르치(끝포인트, 사나질, 암초)
        for (index = 0; index < gnomefish.Length; index++)
        {
            gnomefish[index] = Instantiate(gnomefishPrefab);
            gnomefish[index].transform.parent = distinctionObject[22].transform;
            gnomefish[index].SetActive(false);
        }
        // 개꼴고기(급심지대, 해조류)
        for (index = 0; index < weakfish.Length; index++)
        {
            weakfish[index] = Instantiate(weakfishPrefab);
            weakfish[index].transform.parent = distinctionObject[23].transform;
            weakfish[index].SetActive(false);
        }
        // 가숭어(끝포인트)
        for (index = 0; index < soinymullet.Length; index++)
        {
            soinymullet[index] = Instantiate(soinymulletPrefab);
            soinymullet[index].transform.parent = distinctionObject[24].transform;
            soinymullet[index].SetActive(false);
        }
        // 고등어(급심지대, 물골)
        for (index = 0; index < mackerel.Length; index++)
        {
            mackerel[index] = Instantiate(mackerelPrefab);
            mackerel[index].transform.parent = distinctionObject[25].transform;
            mackerel[index].SetActive(false);
        }
        // 대서양삼치(끝포인트)
        for (index = 0; index < atlanticspanishmackerel.Length; index++)
        {
            atlanticspanishmackerel[index] = Instantiate(atlanticspanishmackerelPrefab);
            atlanticspanishmackerel[index].transform.parent = distinctionObject[26].transform;
            atlanticspanishmackerel[index].SetActive(false);
        }
        // 블랙드럼(해조류, 암초)
        for (index = 0; index < blackdrum.Length; index++)
        {
            blackdrum[index] = Instantiate(blackdrumPrefab);
            blackdrum[index].transform.parent = distinctionObject[27].transform;
            blackdrum[index].SetActive(false);
        }
        // 노랑가오리(사나질)
        for (index = 0; index < redstingray.Length; index++)
        {
            redstingray[index] = Instantiate(redstingrayPrefab);
            redstingray[index].transform.parent = distinctionObject[28].transform;
            redstingray[index].SetActive(false);
        }
        // 대서양대구(암초, 물골)
        for (index = 0; index < atlanticcod.Length; index++)
        {
            atlanticcod[index] = Instantiate(atlanticcodPrefab);
            atlanticcod[index].transform.parent = distinctionObject[29].transform;
            atlanticcod[index].SetActive(false);
        }
        // 줄농어(물골)
        for (index = 0; index < stripedbass.Length; index++)
        {
            stripedbass[index] = Instantiate(stripedbassPrefab);
            stripedbass[index].transform.parent = distinctionObject[30].transform;
            stripedbass[index].SetActive(false);
        }
        // 농어(물골)
        for (index = 0; index < seabass.Length; index++)
        {
            seabass[index] = Instantiate(seabassPrefab);
            seabass[index].transform.parent = distinctionObject[31].transform;
            seabass[index].SetActive(false);
        }
        // 부시리(물골)
        for (index = 0; index < yellowtailamberjack.Length; index++)
        {
            yellowtailamberjack[index] = Instantiate(yellowtailamberjackPrefab);
            yellowtailamberjack[index].transform.parent = distinctionObject[32].transform;
            yellowtailamberjack[index].SetActive(false);
        }
        // 돛새치(해조류, 사나질, 물골)
        for (index = 0; index < indopacificsailfish.Length; index++)
        {

            indopacificsailfish[index] = Instantiate(indopacificsailfishPrefab);
            indopacificsailfish[index].transform.parent = distinctionObject[33].transform;
            indopacificsailfish[index].SetActive(false);
        }
        // 황새치(물골)
        for (index = 0; index < swordfish.Length; index++)
        {

            swordfish[index] = Instantiate(swordfishPrefab);
            swordfish[index].transform.parent = distinctionObject[34].transform;
            swordfish[index].SetActive(false);
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
        bool isCobia, isKingMackerel, isSnook, isSeatrout, isJapaneseBluefish, isWeakfish, isMullet, isMackerel;
        // 회유종(일반) 확인용 변수
        bool isSpanishMackerel, isBlackDrum, isRedStingray, isCodfish, isStripedbass, isSeabass, isYellowtail;
        int rand;

        switch (worldTime)
        {
            #region #6시 리스폰
            case 6:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {

                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for(choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 5 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 25 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 35 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 40 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 5 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 25 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 35 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 40 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #7시 리스폰
            case 7:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 5 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            //// 왕고등어
                            //else if (choice < 15 && !isKingMackerel)
                            //{
                            //    choiceTotal++;
                            //    isKingMackerel = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                            //    }
                            //}
                            // 스눅
                            else if (choice < 20 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 35 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 40 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 85 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 100 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            //else if (choice < 100 && !isMackerel)
                            //{
                            //    choiceTotal++;
                            //    isMackerel = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                            //    }
                            //}
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 5 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            //// 왕고등어
                            //else if (choice < 15 && !isKingMackerel)
                            //{
                            //    choiceTotal++;
                            //    isKingMackerel = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                            //    }
                            //}
                            // 스눅
                            else if (choice < 20 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 35 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 40 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 85 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 100 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            //else if (choice < 100 && !isMackerel)
                            //{
                            //    choiceTotal++;
                            //    isMackerel = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                            //    }
                            //}
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #8시 리스폰
            case 8:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 60 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 75 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 60 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 75 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 30 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 줄농어
                            else if (choice < 50 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 70 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #9시 리스폰
            case 9:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 30 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 50 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if(choice < 55 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 60 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 65 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 30 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 50 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 55 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 60 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 65 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #10시 리스폰
            case 10:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 60 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 60 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #11시 리스폰
            case 11:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 60 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 60 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #12시 리스폰
            case 12:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 40 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 50 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 70 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 80 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 90 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 10 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 35 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 60 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 70 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 80 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #13시 리스폰
            case 13:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 40 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 50 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 70 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 80 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 90 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 10 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 35 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 60 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 70 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 80 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #14시 리스폰
            case 14:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 40 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 50 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 70 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 80 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 90 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 10 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 35 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 60 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 70 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 80 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #15시 리스폰
            case 15:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 25 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 30 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 60 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 80 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 90 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 95 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 10 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 35 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 60 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 70 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 80 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #16시 리스폰
            case 16:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 25 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 30 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 60 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 80 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 90 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 95 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #17시 리스폰
            case 17:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 20 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 25 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 30 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 60 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 80 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 90 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 95 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #18시 리스폰
            case 18:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 25 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 55 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 65 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 70 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 85 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 50 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 55 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 75 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 95 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 25 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 55 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 65 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 70 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 85 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 20 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 35 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 50 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 55 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 75 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 95 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #19시 리스폰
            case 19:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #20시 리스폰
            case 20:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #21시 리스폰
            case 21:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 10 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 15 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 45 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 60 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 65 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 95 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 25 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 40 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 55 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 80 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 85 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #22시 리스폰
            case 22:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 40 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 40 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #23시 리스폰
            case 23:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------ 
                // 희귀
                choice = Random.Range(0, 10);
                if (choice < 5)
                {
                    rand = Random.Range(0, 2);
                    count = RandomCount(5);
                    if (rand.Equals(0))
                    {
                        // 돛새치
                        for (i = 0; i < count; i++)
                        {
                            indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                        }
                    }
                    else
                    {
                        // 황새치
                        for (i = 0; i < count; i++)
                        {
                            swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                        }
                    }
                    choiceType = 0;
                }
                else
                {
                    choiceType = 1;
                }
                switch (choiceType)
                {
                    // 잡어2 일반2 희귀1
                    case 0:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 40 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                    // 잡어3 일반2 희귀0
                    case 1:
                        // 잡어
                        isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                        isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            // 날새기
                            if (choice < 20 && !isCobia)
                            {
                                choiceTotal++;
                                isCobia = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                                }
                            }
                            // 왕고등어
                            else if (choice < 30 && !isKingMackerel)
                            {
                                choiceTotal++;
                                isKingMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                                }
                            }
                            // 스눅
                            else if (choice < 40 && !isSnook)
                            {
                                choiceTotal++;
                                isSnook = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                                }
                            }
                            // 갈색송어
                            else if (choice < 50 && !isSeatrout)
                            {
                                choiceTotal++;
                                isSeatrout = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                                }
                            }
                            // 게르치
                            else if (choice < 70 && !isJapaneseBluefish)
                            {
                                choiceTotal++;
                                isJapaneseBluefish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                                }
                            }
                            // 개꼴고기
                            else if (choice < 80 && !isWeakfish)
                            {
                                choiceTotal++;
                                isWeakfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                                }
                            }
                            // 가숭어
                            else if (choice < 90 && !isMullet)
                            {
                                choiceTotal++;
                                isMullet = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                                }
                            }
                            // 고등어
                            else if (choice < 100 && !isMackerel)
                            {
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                                }
                            }
                        }
                        // 일반
                        isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                        isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            // 대서양삼치
                            if (choice < 10 && !isSpanishMackerel)
                            {
                                choiceTotal++;
                                isSpanishMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                                }
                            }
                            // 블랙드럼
                            else if (choice < 25 && !isBlackDrum)
                            {
                                choiceTotal++;
                                isBlackDrum = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                                }
                            }
                            // 노랑가오리
                            else if (choice < 40 && !isRedStingray)
                            {
                                choiceTotal++;
                                isRedStingray = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                                }
                            }
                            // 대서양대구
                            else if (choice < 70 && !isCodfish)
                            {
                                choiceTotal++;
                                isCodfish = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                                }
                            }
                            // 줄농어
                            else if (choice < 80 && !isStripedbass)
                            {
                                choiceTotal++;
                                isStripedbass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                                }
                            }
                            // 농어
                            else if (choice < 90 && !isSeabass)
                            {
                                choiceTotal++;
                                isSeabass = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                                }
                            }
                            // 부시리
                            else if (choice < 100 && !isYellowtail)
                            {
                                choiceTotal++;
                                isYellowtail = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                                }
                            }
                        }
                        break;
                }
                break;
            #endregion
            #region #0시 리스폰
            case 0:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 55 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 75 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 95 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #1시 리스폰
            case 1:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #2시 리스폰
            case 2:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #3시 리스폰
            case 3:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #4시 리스폰
            case 4:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
                }
                break;
            #endregion
            #region #5시 리스폰
            case 5:
                // 비회유종------------------------------------------------  
                // 잡어
                // 병어돔
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    floridapompano[i].SetActive(true); floridapompano[i].GetComponent<FishSkyway>().SetData(74);
                }
                // 피카소피쉬
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    lagoontriggerfish[i].SetActive(true); lagoontriggerfish[i].GetComponent<FishSkyway>().SetData(82);
                }
                // 점쏠배감펭
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    redlionfish[i].SetActive(true); redlionfish[i].GetComponent<FishSkyway>().SetData(85);
                }
                // 돼지물고기
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    hogfish[i].SetActive(true); hogfish[i].GetComponent<FishSkyway>().SetData(78);
                }
                // 걸프가자미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    gulfflounder[i].SetActive(true); gulfflounder[i].GetComponent<FishSkyway>().SetData(76);
                }
                // 여을멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    bonefish[i].SetActive(true); bonefish[i].GetComponent<FishSkyway>().SetData(71);
                }
                // 일반
                // 속임금눈돔
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    redsnapper[i].SetActive(true); redsnapper[i].GetComponent<FishSkyway>().SetData(87);
                }
                // 대서양레크피쉬
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    atlanticwreckfish[i].SetActive(true); atlanticwreckfish[i].GetComponent<FishSkyway>().SetData(67);
                }
                // 방어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<FishSkyway>().SetData(80);
                }
                // 꼬치삼치
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    wahoo[i].SetActive(true); wahoo[i].GetComponent<FishSkyway>().SetData(96);
                }
                // 마설가자미
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    halibut[i].SetActive(true); halibut[i].GetComponent<FishSkyway>().SetData(77);
                }
                // 만새기
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    mahimahi[i].SetActive(true); mahimahi[i].GetComponent<FishSkyway>().SetData(84);
                }
                // 양머리돔
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sheepshead[i].SetActive(true); sheepshead[i].GetComponent<FishSkyway>().SetData(92);
                }
                // 감성돔
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<FishSkyway>().SetData(70);
                }
                // 희귀
                // 블랙그루퍼
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackgrouper[i].SetActive(true); blackgrouper[i].GetComponent<FishSkyway>().SetData(69);
                }
                // 백미돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    atlantictripletail[i].SetActive(true); atlantictripletail[i].GetComponent<FishSkyway>().SetData(66);
                }
                // 적돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redporgy[i].SetActive(true); redporgy[i].GetComponent<FishSkyway>().SetData(86);
                }
                // 옥돔
                count = RandomCount(4);
                for (i = 0; i < count; i++)
                {
                    redtilefish[i].SetActive(true); redtilefish[i].GetComponent<FishSkyway>().SetData(89);
                }
                // 회유종------------------------------------------------
                // 잡어3 일반2 희귀0
                // 잡어
                isCobia = false; isKingMackerel = false; isSnook = false; isSeatrout = false;
                isJapaneseBluefish = false; isWeakfish = false; isMullet = false; isMackerel = false;

                for (choiceTotal = 0; choiceTotal < 3;)
                {
                    choice = Random.Range(0, 100);

                    // 날새기
                    if (choice < 10 && !isCobia)
                    {
                        choiceTotal++;
                        isCobia = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            cobia[i].SetActive(true); cobia[i].GetComponent<FishSkyway>().SetData(72);
                        }
                    }
                    // 왕고등어
                    else if (choice < 30 && !isKingMackerel)
                    {
                        choiceTotal++;
                        isKingMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            kingmackerel[i].SetActive(true); kingmackerel[i].GetComponent<FishSkyway>().SetData(81);
                        }
                    }
                    // 스눅
                    else if (choice < 35 && !isSnook)
                    {
                        choiceTotal++;
                        isSnook = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            commonsnook[i].SetActive(true); commonsnook[i].GetComponent<FishSkyway>().SetData(73);
                        }
                    }
                    // 갈색송어
                    else if (choice < 55 && !isSeatrout)
                    {
                        choiceTotal++;
                        isSeatrout = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seatrout[i].SetActive(true); seatrout[i].GetComponent<FishSkyway>().SetData(91);
                        }
                    }
                    // 게르치
                    else if (choice < 65 && !isJapaneseBluefish)
                    {
                        choiceTotal++;
                        isJapaneseBluefish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            gnomefish[i].SetActive(true); gnomefish[i].GetComponent<FishSkyway>().SetData(75);
                        }
                    }
                    // 개꼴고기
                    else if (choice < 75 && !isWeakfish)
                    {
                        choiceTotal++;
                        isWeakfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            weakfish[i].SetActive(true); weakfish[i].GetComponent<FishSkyway>().SetData(97);
                        }
                    }
                    // 가숭어
                    else if (choice < 80 && !isMullet)
                    {
                        choiceTotal++;
                        isMullet = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            soinymullet[i].SetActive(true); soinymullet[i].GetComponent<FishSkyway>().SetData(93);
                        }
                    }
                    // 고등어
                    else if (choice < 100 && !isMackerel)
                    {
                        choiceTotal++;
                        isMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            mackerel[i].SetActive(true); mackerel[i].GetComponent<FishSkyway>().SetData(83);
                        }
                    }
                }
                // 일반
                isSpanishMackerel = false; isBlackDrum = false; isRedStingray = false;
                isCodfish = false; isStripedbass = false; isSeabass = false; isYellowtail = false;

                for (choiceTotal = 0; choiceTotal < 2;)
                {
                    choice = Random.Range(0, 100);

                    // 대서양삼치
                    if (choice < 5 && !isSpanishMackerel)
                    {
                        choiceTotal++;
                        isSpanishMackerel = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticspanishmackerel[i].SetActive(true); atlanticspanishmackerel[i].GetComponent<FishSkyway>().SetData(65);
                        }
                    }
                    // 블랙드럼
                    else if (choice < 20 && !isBlackDrum)
                    {
                        choiceTotal++;
                        isBlackDrum = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            blackdrum[i].SetActive(true); blackdrum[i].GetComponent<FishSkyway>().SetData(68);
                        }
                    }
                    // 노랑가오리
                    else if (choice < 35 && !isRedStingray)
                    {
                        choiceTotal++;
                        isRedStingray = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            redstingray[i].SetActive(true); redstingray[i].GetComponent<FishSkyway>().SetData(88);
                        }
                    }
                    // 대서양대구
                    else if (choice < 40 && !isCodfish)
                    {
                        choiceTotal++;
                        isCodfish = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            atlanticcod[i].SetActive(true); atlanticcod[i].GetComponent<FishSkyway>().SetData(64);
                        }
                    }
                    // 줄농어
                    else if (choice < 65 && !isStripedbass)
                    {
                        choiceTotal++;
                        isStripedbass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            stripedbass[i].SetActive(true); stripedbass[i].GetComponent<FishSkyway>().SetData(94);
                        }
                    }
                    // 농어
                    else if (choice < 90 && !isSeabass)
                    {
                        choiceTotal++;
                        isSeabass = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            seabass[i].SetActive(true); seabass[i].GetComponent<FishSkyway>().SetData(90);
                        }
                    }
                    // 부시리
                    else if (choice < 100 && !isYellowtail)
                    {
                        choiceTotal++;
                        isYellowtail = true;
                        count = RandomCount(10);
                        for (i = 0; i < count; i++)
                        {
                            yellowtailamberjack[i].SetActive(true); yellowtailamberjack[i].GetComponent<FishSkyway>().SetData(98);
                        }
                    }
                }
                // 희귀
                rand = Random.Range(0, 2);
                count = RandomCount(5);
                if (rand.Equals(0))
                {
                    // 돛새치
                    for (i = 0; i < count; i++)
                    {
                        indopacificsailfish[i].SetActive(true); indopacificsailfish[i].GetComponent<FishSkyway>().SetData(79);
                    }
                }
                else
                {
                    // 황새치
                    for (i = 0; i < count; i++)
                    {
                        swordfish[i].SetActive(true); swordfish[i].GetComponent<FishSkyway>().SetData(95);
                    }
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

        //if (_petManager.isLightOn)
        //    UpdateProbilityWhenLampOn();
        //else
        //    UpdateProbilityWhenLampOff();

        InitializeFishSearchRange();

        _gameManager.IsNeedleMoving= false;
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
        // 블랙그루퍼
        for (int i = 0; i < blackgrouper.Length; i++)
        {
            if (!blackgrouper[i].activeSelf)
                continue;

            _rareFishList.Add(blackgrouper[i]);
        }
        // 백미돔
        for (int i = 0; i < atlantictripletail.Length; i++)
        {
            if (!atlantictripletail[i].activeSelf)
                continue;

            _rareFishList.Add(atlantictripletail[i]);
        }
        // 적돔
        for (int i = 0; i < redporgy.Length; i++)
        {
            if (!redporgy[i].activeSelf)
                continue;

            _rareFishList.Add(redporgy[i]);
        }
        // 옥돔
        for (int i = 0; i < redtilefish.Length; i++)
        {
            if (!redtilefish[i].activeSelf)
                continue;

            _rareFishList.Add(redtilefish[i]);
        }
        // 돛새치
        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (!indopacificsailfish[i].activeSelf)
                continue;

            _rareFishList.Add(indopacificsailfish[i]);
        }
        // 황새치
        for (int i = 0; i < swordfish.Length; i++)
        {
            if (!swordfish[i].activeSelf)
                continue;

            _rareFishList.Add(swordfish[i]);
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

        for (int i = 0; i < floridapompano.Length; i++)
        {
            if (floridapompano[i].activeSelf)
                _influencedFishListByKrill.Add(floridapompano[i]);
            else
                continue;
        }
        for (int i = 0; i < lagoontriggerfish.Length; i++)
        {
            if (lagoontriggerfish[i].activeSelf)
                _influencedFishListByKrill.Add(lagoontriggerfish[i]);
            else
                continue;
        }
        for (int i = 0; i < bonefish.Length; i++)
        {
            if (bonefish[i].activeSelf)
                _influencedFishListByKrill.Add(bonefish[i]);
            else
                continue;
        }
        for (int i = 0; i < redsnapper.Length; i++)
        {
            if (redsnapper[i].activeSelf)
                _influencedFishListByKrill.Add(redsnapper[i]);
            else
                continue;
        }
        for (int i = 0; i < sheepshead.Length; i++)
        {
            if (sheepshead[i].activeSelf)
                _influencedFishListByKrill.Add(sheepshead[i]);
            else
                continue;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (blackporgy[i].activeSelf)
                _influencedFishListByKrill.Add(blackporgy[i]);
            else
                continue;
        }
        for (int i = 0; i < cobia.Length; i++)
        {
            if (cobia[i].activeSelf)
                _influencedFishListByKrill.Add(cobia[i]);
            else
                continue;
        }
        for (int i = 0; i < kingmackerel.Length; i++)
        {
            if (kingmackerel[i].activeSelf)
                _influencedFishListByKrill.Add(kingmackerel[i]);
            else
                continue;
        }
        for (int i = 0; i < soinymullet.Length; i++)
        {
            if (soinymullet[i].activeSelf)
                _influencedFishListByKrill.Add(soinymullet[i]);
            else
                continue;
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (mackerel[i].activeSelf)
                _influencedFishListByKrill.Add(mackerel[i]);
            else
                continue;
        }
        for (int i = 0; i < atlanticspanishmackerel.Length; i++)
        {
            if (atlanticspanishmackerel[i].activeSelf)
                _influencedFishListByKrill.Add(atlanticspanishmackerel[i]);
            else
                continue;
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (seabass[i].activeSelf)
                _influencedFishListByKrill.Add(seabass[i]);
            else
                continue;
        }
        for (int i = 0; i < yellowtailamberjack.Length; i++)
        {
            if (yellowtailamberjack[i].activeSelf)
                _influencedFishListByKrill.Add(yellowtailamberjack[i]);
            else
                continue;
        }
        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (indopacificsailfish[i].activeSelf)
                _influencedFishListByKrill.Add(indopacificsailfish[i]);
            else
                continue;
        }
        for (int i = 0; i < swordfish.Length; i++)
        {
            if (swordfish[i].activeSelf)
                _influencedFishListByKrill.Add(swordfish[i]);
            else
                continue;
        }
    }

    void CheckFishAccordingToMovement()
    {
        _fishListIncreaseProbabilityWhenStill = new List<FishSkyway>();
        _fishListIncreaseProbabilityWhenMove = new List<FishSkyway>();

        // 움직임에 따라 확률이 증가하는 물고기들
        for (int i = 0; i < floridapompano.Length; i++)
        {
            if (floridapompano[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(floridapompano[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < redlionfish.Length; i++)
        {
            if (redlionfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(redlionfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < hogfish.Length; i++)
        {
            if (hogfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(hogfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < gulfflounder.Length; i++)
        {
            if (gulfflounder[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(gulfflounder[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < bonefish.Length; i++)
        {
            if (bonefish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(bonefish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < redsnapper.Length; i++)
        {
            if (redsnapper[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(redsnapper[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < atlanticwreckfish.Length; i++)
        {
            if (atlanticwreckfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(atlanticwreckfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (japaneseamberjack[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(japaneseamberjack[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < wahoo.Length; i++)
        {
            if (wahoo[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(wahoo[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < mahimahi.Length; i++)
        {
            if (mahimahi[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(mahimahi[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < sheepshead.Length; i++)
        {
            if (sheepshead[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(sheepshead[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (blackporgy[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(blackporgy[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < blackgrouper.Length; i++)
        {
            if (blackgrouper[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(blackgrouper[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < atlantictripletail.Length; i++)
        {
            if (atlantictripletail[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(atlantictripletail[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < redporgy.Length; i++)
        {
            if (redporgy[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(redporgy[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < redtilefish.Length; i++)
        {
            if (redtilefish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(redtilefish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < cobia.Length; i++)
        {
            if (cobia[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(cobia[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < kingmackerel.Length; i++)
        {
            if (kingmackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(kingmackerel[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < seatrout.Length; i++)
        {
            if (seatrout[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(seatrout[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (gnomefish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(gnomefish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < weakfish.Length; i++)
        {
            if (weakfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(weakfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < soinymullet.Length; i++)
        {
            if (soinymullet[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(soinymullet[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < mackerel.Length; i++)
        {
            if (mackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(mackerel[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < atlanticspanishmackerel.Length; i++)
        {
            if (atlanticspanishmackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(atlanticspanishmackerel[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < blackdrum.Length; i++)
        {
            if (blackdrum[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(blackdrum[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < stripedbass.Length; i++)
        {
            if (stripedbass[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(stripedbass[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < seabass.Length; i++)
        {
            if (seabass[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(seabass[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < yellowtailamberjack.Length; i++)
        {
            if (yellowtailamberjack[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(yellowtailamberjack[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (indopacificsailfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(indopacificsailfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < swordfish.Length; i++)
        {
            if (swordfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(swordfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        // 가만히 있을 때 확률이 증가하는 물고기들
        for (int i = 0; i < lagoontriggerfish.Length; i++)
        {
            if (lagoontriggerfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(lagoontriggerfish[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < halibut.Length; i++)
        {
            if (halibut[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(halibut[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < commonsnook.Length; i++)
        {
            if (commonsnook[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(commonsnook[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < redstingray.Length; i++)
        {
            if (redstingray[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(redstingray[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        for (int i = 0; i < atlanticcod.Length; i++)
        {
            if (atlanticcod[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(atlanticcod[i].GetComponent<FishSkyway>());
            else
                continue;
        }

        //IncreaseProbabilityAccordingToMovement(true);
        _isFirst = false;
    }

    public void UpdateProbilityWhenLampOn()
    {
        // 도다리
        for (int i = 0; i < floridapompano.Length; i++)
        {
            if (!floridapompano[i].activeSelf)
                continue;

            if (floridapompano[i].GetComponent<FishSkyway>().SearchRange > 0)
                floridapompano[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < lagoontriggerfish.Length; i++)
        {
            if (!lagoontriggerfish[i].activeSelf)
                continue;

            if (lagoontriggerfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                lagoontriggerfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < redlionfish.Length; i++)
        {
            if (!redlionfish[i].activeSelf)
                continue;

            redlionfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < hogfish.Length; i++)
        {
            if (!hogfish[i].activeSelf)
                continue;

            if (hogfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                hogfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < gulfflounder.Length; i++)
        {
            if (!gulfflounder[i].activeSelf)
                continue;

            gulfflounder[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < bonefish.Length; i++)
        {
            if (!bonefish[i].activeSelf)
                continue;

            if (bonefish[i].GetComponent<FishSkyway>().SearchRange > 0)
                bonefish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < redsnapper.Length; i++)
        {
            if (!redsnapper[i].activeSelf)
                continue;

            if (redsnapper[i].GetComponent<FishSkyway>().SearchRange > 0)
                redsnapper[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < atlanticwreckfish.Length; i++)
        {
            if (!atlanticwreckfish[i].activeSelf)
                continue;

            if (atlanticwreckfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                atlanticwreckfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;

            if (japaneseamberjack[i].GetComponent<FishSkyway>().SearchRange > 0)
                japaneseamberjack[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < wahoo.Length; i++)
        {
            if (!wahoo[i].activeSelf)
                continue;

            if (wahoo[i].GetComponent<FishSkyway>().SearchRange > 0)
                wahoo[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            if (halibut[i].GetComponent<FishSkyway>().SearchRange > 0)
                halibut[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < mahimahi.Length; i++)
        {
            if (!mahimahi[i].activeSelf)
                continue;

            if (mahimahi[i].GetComponent<FishSkyway>().SearchRange > 0)
                mahimahi[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < sheepshead.Length; i++)
        {
            if (!sheepshead[i].activeSelf)
                continue;

            if (sheepshead[i].GetComponent<FishSkyway>().SearchRange > 0)
                sheepshead[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;

            if (blackporgy[i].GetComponent<FishSkyway>().SearchRange > 0)
                blackporgy[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < blackgrouper.Length; i++)
        {
            if (!blackgrouper[i].activeSelf)
                continue;

            if (blackgrouper[i].GetComponent<FishSkyway>().SearchRange > 0)
                blackgrouper[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < atlantictripletail.Length; i++)
        {
            if (!atlantictripletail[i].activeSelf)
                continue;

            if (atlantictripletail[i].GetComponent<FishSkyway>().SearchRange > 0)
                atlantictripletail[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < redporgy.Length; i++)
        {
            if (!redporgy[i].activeSelf)
                continue;

            if (redporgy[i].GetComponent<FishSkyway>().SearchRange > 0)
                redporgy[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < redtilefish.Length; i++)
        {
            if (!redtilefish[i].activeSelf)
                continue;

            if (redtilefish[i].GetComponent<FishSkyway>().SearchRange > 0)
                redtilefish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < cobia.Length; i++)
        {
            if (!cobia[i].activeSelf)
                continue;

            if (cobia[i].GetComponent<FishSkyway>().SearchRange > 0)
                cobia[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < kingmackerel.Length; i++)
        {
            if (!kingmackerel[i].activeSelf)
                continue;

            if (kingmackerel[i].GetComponent<FishSkyway>().SearchRange > 0)
                kingmackerel[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < commonsnook.Length; i++)
        {
            if (!commonsnook[i].activeSelf)
                continue;

            commonsnook[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < seatrout.Length; i++)
        {
            if (!seatrout[i].activeSelf)
                continue;

            if (seatrout[i].GetComponent<FishSkyway>().SearchRange > 0)
                seatrout[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;

            if (gnomefish[i].GetComponent<FishSkyway>().SearchRange > 0)
                gnomefish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < weakfish.Length; i++)
        {
            if (!weakfish[i].activeSelf)
                continue;

            if (weakfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                weakfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < soinymullet.Length; i++)
        {
            if (!soinymullet[i].activeSelf)
                continue;

            if (soinymullet[i].GetComponent<FishSkyway>().SearchRange > 0)
                soinymullet[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;

            if (mackerel[i].GetComponent<FishSkyway>().SearchRange > 0)
                mackerel[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < atlanticspanishmackerel.Length; i++)
        {
            if (!atlanticspanishmackerel[i].activeSelf)
                continue;

            if (atlanticspanishmackerel[i].GetComponent<FishSkyway>().SearchRange > 0)
                atlanticspanishmackerel[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < blackdrum.Length; i++)
        {
            if (!blackdrum[i].activeSelf)
                continue;

            if (blackdrum[i].GetComponent<FishSkyway>().SearchRange > 0)
                blackdrum[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < redstingray.Length; i++)
        {
            if (!redstingray[i].activeSelf)
                continue;

            if (redstingray[i].GetComponent<FishSkyway>().SearchRange > 0)
                redstingray[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < atlanticcod.Length; i++)
        {
            if (!atlanticcod[i].activeSelf)
                continue;

            if (atlanticcod[i].GetComponent<FishSkyway>().SearchRange > 0)
                atlanticcod[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < stripedbass.Length; i++)
        {
            if (!stripedbass[i].activeSelf)
                continue;

            if (stripedbass[i].GetComponent<FishSkyway>().SearchRange > 0)
                stripedbass[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;

            if (seabass[i].GetComponent<FishSkyway>().SearchRange > 0)
                seabass[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }

        for (int i = 0; i < yellowtailamberjack.Length; i++)
        {
            if (!yellowtailamberjack[i].activeSelf)
                continue;

            if (yellowtailamberjack[i].GetComponent<FishSkyway>().SearchRange > 0)
                yellowtailamberjack[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (!indopacificsailfish[i].activeSelf)
                continue;

            if (indopacificsailfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                indopacificsailfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < swordfish.Length; i++)
        {
            if (!swordfish[i].activeSelf)
                continue;

            if (swordfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                swordfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
    }
    public void UpdateProbilityWhenLampOff()
    {
        // 도다리
        for (int i = 0; i < floridapompano.Length; i++)
        {
            if (!floridapompano[i].activeSelf)
                continue;

            floridapompano[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < lagoontriggerfish.Length; i++)
        {
            if (!lagoontriggerfish[i].activeSelf)
                continue;
            lagoontriggerfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < redlionfish.Length; i++)
        {
            if (!redlionfish[i].activeSelf)
                continue;

            if(redlionfish[i].GetComponent<FishSkyway>().SearchRange > 0)
                redlionfish[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < hogfish.Length; i++)
        {
            if (!hogfish[i].activeSelf)
                continue;
            hogfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < gulfflounder.Length; i++)
        {
            if (!gulfflounder[i].activeSelf)
                continue;

            if (gulfflounder[i].GetComponent<FishSkyway>().SearchRange > 0)
                gulfflounder[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < bonefish.Length; i++)
        {
            if (!bonefish[i].activeSelf)
                continue;
            bonefish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < redsnapper.Length; i++)
        {
            if (!redsnapper[i].activeSelf)
                continue;
            redsnapper[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < atlanticwreckfish.Length; i++)
        {
            if (!atlanticwreckfish[i].activeSelf)
                continue;
            atlanticwreckfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;
            japaneseamberjack[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < wahoo.Length; i++)
        {
            if (!wahoo[i].activeSelf)
                continue;
            wahoo[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;
            halibut[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < mahimahi.Length; i++)
        {
            if (!mahimahi[i].activeSelf)
                continue;
            mahimahi[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < sheepshead.Length; i++)
        {
            if (!sheepshead[i].activeSelf)
                continue;
            sheepshead[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;
            blackporgy[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < blackgrouper.Length; i++)
        {
            if (!blackgrouper[i].activeSelf)
                continue;
            blackgrouper[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < atlantictripletail.Length; i++)
        {
            if (!atlantictripletail[i].activeSelf)
                continue;
            atlantictripletail[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < redporgy.Length; i++)
        {
            if (!redporgy[i].activeSelf)
                continue;
            redporgy[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < redtilefish.Length; i++)
        {
            if (!redtilefish[i].activeSelf)
                continue;
            redtilefish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < cobia.Length; i++)
        {
            if (!cobia[i].activeSelf)
                continue;
            cobia[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < kingmackerel.Length; i++)
        {
            if (!kingmackerel[i].activeSelf)
                continue;
            kingmackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < commonsnook.Length; i++)
        {
            if (!commonsnook[i].activeSelf)
                continue;

            if(commonsnook[i].GetComponent<FishSkyway>().SearchRange > 0)
                commonsnook[i].GetComponent<FishSkyway>().SearchRange -= 1;
        }
        for (int i = 0; i < seatrout.Length; i++)
        {
            if (!seatrout[i].activeSelf)
                continue;
            seatrout[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;
            gnomefish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < weakfish.Length; i++)
        {
            if (!weakfish[i].activeSelf)
                continue;
            weakfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < soinymullet.Length; i++)
        {
            if (!soinymullet[i].activeSelf)
                continue;
            soinymullet[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;
            mackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < atlanticspanishmackerel.Length; i++)
        {
            if (!atlanticspanishmackerel[i].activeSelf)
                continue;
            atlanticspanishmackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < blackdrum.Length; i++)
        {
            if (!blackdrum[i].activeSelf)
                continue;
            blackdrum[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < redstingray.Length; i++)
        {
            if (!redstingray[i].activeSelf)
                continue;
            redstingray[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < atlanticcod.Length; i++)
        {
            if (!atlanticcod[i].activeSelf)
                continue;
            atlanticcod[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < stripedbass.Length; i++)
        {
            if (!stripedbass[i].activeSelf)
                continue;
            stripedbass[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;
            seabass[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < yellowtailamberjack.Length; i++)
        {
            if (!yellowtailamberjack[i].activeSelf)
                continue;
            yellowtailamberjack[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (!indopacificsailfish[i].activeSelf)
                continue;
            indopacificsailfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
        for (int i = 0; i < swordfish.Length; i++)
        {
            if (!swordfish[i].activeSelf)
                continue;
            swordfish[i].GetComponent<FishSkyway>().SearchRange += 1;
        }
    }
    public void UpdateRangeWhenThrowPastebait(float leftX, float rightX, float upZ, float downZ)
    {
        GameObject[] fishs;
        int cnt = 0;
        //Debug.Log(DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"]);
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

    void PlusBaitProbability(GameObject[] fishObject, int addRate)
    {
        for (int i = 0; i < fishObject.Length; i++)
        {
            if (fishObject[i] != null)
            {
                fishObject[i].GetComponent<FishSkyway>().StartIncreaseSearchRangeOneMinute(addRate);
            }
            else
                break;
        }
    }

    public void IncreaseProbabilityAccordingToMovement(bool isMoving)
    {
        //Debug.Log("움직임: " + _isIncreaseWhenMove + " , 정지: " + _isIncreaseWhenStill);
        if (isMoving)
        {
            if (!_isIncreaseWhenMove)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenMove.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenMove[i].biteBait += 10f;
                }
                _isIncreaseWhenMove = true;
            }

            if (_isIncreaseWhenStill)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenStill[i].biteBait -= 10f;
                }
                _isIncreaseWhenStill = false;
            }
        }
        else
        {
            if (_isIncreaseWhenMove)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenMove.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenMove[i].biteBait -= 10f;
                }
                _isIncreaseWhenMove = false;
            }

            if (!_isIncreaseWhenStill)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenStill[i].biteBait += 10f;
                }
                _isIncreaseWhenStill = true;
            }
        }
    }

    void AssignmentFishs()
    {
        floridapompano = new GameObject[26]; // 병어돔
        lagoontriggerfish = new GameObject[18];  // 피카소피쉬
        redlionfish = new GameObject[26]; // 점쏠배감펭
        hogfish = new GameObject[26];  // 돼지물고기
        gulfflounder = new GameObject[26];  // 걸프가자미
        bonefish = new GameObject[18];  // 여을멸

        redsnapper = new GameObject[18];   // 속임금눈돔
        atlanticwreckfish = new GameObject[18];   // 대서양레크피쉬
        japaneseamberjack = new GameObject[12];   // 방어
        wahoo = new GameObject[18];    // 꼬치삼치
        halibut = new GameObject[18];  // 마설가자미
        mahimahi = new GameObject[18];   // 만새기
        sheepshead = new GameObject[18];   // 양머리돔
        blackporgy = new GameObject[12];   // 감성돔


        blackgrouper = new GameObject[5];  // 블랙그루퍼
        atlantictripletail = new GameObject[5];    // 백미돔
        redporgy = new GameObject[5];  // 적돔
        redtilefish = new GameObject[5];  // 옥돔


        cobia = new GameObject[30];    // 날새기
        kingmackerel = new GameObject[30]; // 왕고등어
        commonsnook = new GameObject[30];    // 스눅
        seatrout = new GameObject[30]; // 갈색송어
        gnomefish = new GameObject[30]; // 게르치
        weakfish = new GameObject[30]; // 개꼴고기
        soinymullet = new GameObject[30];   // 가숭어
        mackerel = new GameObject[30]; // 고등어

        atlanticspanishmackerel = new GameObject[30];  // 대서양삼
        blackdrum = new GameObject[30];    // 블랙드럼
        redstingray = new GameObject[30];  // 노랑가오리
        atlanticcod = new GameObject[30];    // 대서양대구
        stripedbass = new GameObject[30];  // 줄농어
        seabass = new GameObject[30];  // 농어
        yellowtailamberjack = new GameObject[30];   // 부시리

        indopacificsailfish = new GameObject[10];  // 돛새치
        swordfish = new GameObject[10];    // 황새치
    }

    public void UpdateFishSearchRange(int range)
    {
        // 도다리
        for (int i = 0; i < floridapompano.Length; i++)
        {
            if (!floridapompano[i].activeSelf)
                continue;

            floridapompano[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < lagoontriggerfish.Length; i++)
        {
            if (!lagoontriggerfish[i].activeSelf)
                continue;

            lagoontriggerfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < redlionfish.Length; i++)
        {
            if (!redlionfish[i].activeSelf)
                continue;

            redlionfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < hogfish.Length; i++)
        {
            if (!hogfish[i].activeSelf)
                continue;

            hogfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < gulfflounder.Length; i++)
        {
            if (!gulfflounder[i].activeSelf)
                continue;

                gulfflounder[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < bonefish.Length; i++)
        {
            if (!bonefish[i].activeSelf)
                continue;

            bonefish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < redsnapper.Length; i++)
        {
            if (!redsnapper[i].activeSelf)
                continue;

            redsnapper[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < atlanticwreckfish.Length; i++)
        {
            if (!atlanticwreckfish[i].activeSelf)
                continue;

            atlanticwreckfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;

            japaneseamberjack[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < wahoo.Length; i++)
        {
            if (!wahoo[i].activeSelf)
                continue;

            wahoo[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < halibut.Length; i++)
        {
            if (!halibut[i].activeSelf)
                continue;

            halibut[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < mahimahi.Length; i++)
        {
            if (!mahimahi[i].activeSelf)
                continue;

            mahimahi[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < sheepshead.Length; i++)
        {
            if (!sheepshead[i].activeSelf)
                continue;

            sheepshead[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;

            blackporgy[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < blackgrouper.Length; i++)
        {
            if (!blackgrouper[i].activeSelf)
                continue;
            
            blackgrouper[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < atlantictripletail.Length; i++)
        {
            if (!atlantictripletail[i].activeSelf)
                continue;

            atlantictripletail[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < redporgy.Length; i++)
        {
            if (!redporgy[i].activeSelf)
                continue;

            redporgy[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < redtilefish.Length; i++)
        {
            if (!redtilefish[i].activeSelf)
                continue;
              
            redtilefish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < cobia.Length; i++)
        {
            if (!cobia[i].activeSelf)
                continue;

            cobia[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < kingmackerel.Length; i++)
        {
            if (!kingmackerel[i].activeSelf)
                continue;

            kingmackerel[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < commonsnook.Length; i++)
        {
            if (!commonsnook[i].activeSelf)
                continue;

                commonsnook[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < seatrout.Length; i++)
        {
            if (!seatrout[i].activeSelf)
                continue;

            seatrout[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < gnomefish.Length; i++)
        {
            if (!gnomefish[i].activeSelf)
                continue;

            gnomefish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < weakfish.Length; i++)
        {
            if (!weakfish[i].activeSelf)
                continue;

            weakfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < soinymullet.Length; i++)
        {
            if (!soinymullet[i].activeSelf)
                continue;

            soinymullet[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;

            mackerel[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < atlanticspanishmackerel.Length; i++)
        {
            if (!atlanticspanishmackerel[i].activeSelf)
                continue;

            atlanticspanishmackerel[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < blackdrum.Length; i++)
        {
            if (!blackdrum[i].activeSelf)
                continue;

            blackdrum[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < redstingray.Length; i++)
        {
            if (!redstingray[i].activeSelf)
                continue;

            redstingray[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < atlanticcod.Length; i++)
        {
            if (!atlanticcod[i].activeSelf)
                continue;

            atlanticcod[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < stripedbass.Length; i++)
        {
            if (!stripedbass[i].activeSelf)
                continue;

            stripedbass[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;

            seabass[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < yellowtailamberjack.Length; i++)
        {
            if (!yellowtailamberjack[i].activeSelf)
                continue;

            yellowtailamberjack[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < indopacificsailfish.Length; i++)
        {
            if (!indopacificsailfish[i].activeSelf)
                continue;

            indopacificsailfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }
        for (int i = 0; i < swordfish.Length; i++)
        {
            if (!swordfish[i].activeSelf)
                continue;

            swordfish[i].GetComponent<FishSkyway>().SettingSearchRange(range);
        }

        LampCheck();
    }

    void LampCheck()
    {
        if(_petManager.IsLightOn)
        {
            for (int i = 0; i < redlionfish.Length; i++)
            {
                if (!redlionfish[i].activeSelf)
                    continue;

                redlionfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }

            for (int i = 0; i < gulfflounder.Length; i++)
            {
                if (!gulfflounder[i].activeSelf)
                    continue;

                gulfflounder[i].GetComponent<FishSkyway>().SearchRange += 1;
            }

            for (int i = 0; i < commonsnook.Length; i++)
            {
                if (!commonsnook[i].activeSelf)
                    continue;

                commonsnook[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
        }
        else
        {
            for (int i = 0; i < floridapompano.Length; i++)
            {
                if (!floridapompano[i].activeSelf)
                    continue;

                floridapompano[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < lagoontriggerfish.Length; i++)
            {
                if (!lagoontriggerfish[i].activeSelf)
                    continue;

                lagoontriggerfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }

            for (int i = 0; i < hogfish.Length; i++)
            {
                if (!hogfish[i].activeSelf)
                    continue;

                hogfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }

            for (int i = 0; i < bonefish.Length; i++)
            {
                if (!bonefish[i].activeSelf)
                    continue;

                bonefish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < redsnapper.Length; i++)
            {
                if (!redsnapper[i].activeSelf)
                    continue;

                redsnapper[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < atlanticwreckfish.Length; i++)
            {
                if (!atlanticwreckfish[i].activeSelf)
                    continue;

                atlanticwreckfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < japaneseamberjack.Length; i++)
            {
                if (!japaneseamberjack[i].activeSelf)
                    continue;

                japaneseamberjack[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < wahoo.Length; i++)
            {
                if (!wahoo[i].activeSelf)
                    continue;

                wahoo[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < halibut.Length; i++)
            {
                if (!halibut[i].activeSelf)
                    continue;

                halibut[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < mahimahi.Length; i++)
            {
                if (!mahimahi[i].activeSelf)
                    continue;

                mahimahi[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < sheepshead.Length; i++)
            {
                if (!sheepshead[i].activeSelf)
                    continue;

                sheepshead[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < blackporgy.Length; i++)
            {
                if (!blackporgy[i].activeSelf)
                    continue;

                blackporgy[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < blackgrouper.Length; i++)
            {
                if (!blackgrouper[i].activeSelf)
                    continue;

                blackgrouper[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < atlantictripletail.Length; i++)
            {
                if (!atlantictripletail[i].activeSelf)
                    continue;

                atlantictripletail[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < redporgy.Length; i++)
            {
                if (!redporgy[i].activeSelf)
                    continue;

                redporgy[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < redtilefish.Length; i++)
            {
                if (!redtilefish[i].activeSelf)
                    continue;

                redtilefish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < cobia.Length; i++)
            {
                if (!cobia[i].activeSelf)
                    continue;

                cobia[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < kingmackerel.Length; i++)
            {
                if (!kingmackerel[i].activeSelf)
                    continue;

                kingmackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < seatrout.Length; i++)
            {
                if (!seatrout[i].activeSelf)
                    continue;

                seatrout[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < gnomefish.Length; i++)
            {
                if (!gnomefish[i].activeSelf)
                    continue;

                gnomefish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < weakfish.Length; i++)
            {
                if (!weakfish[i].activeSelf)
                    continue;

                weakfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < soinymullet.Length; i++)
            {
                if (!soinymullet[i].activeSelf)
                    continue;

                soinymullet[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < mackerel.Length; i++)
            {
                if (!mackerel[i].activeSelf)
                    continue;

                mackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < atlanticspanishmackerel.Length; i++)
            {
                if (!atlanticspanishmackerel[i].activeSelf)
                    continue;

                atlanticspanishmackerel[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < blackdrum.Length; i++)
            {
                if (!blackdrum[i].activeSelf)
                    continue;

                blackdrum[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < redstingray.Length; i++)
            {
                if (!redstingray[i].activeSelf)
                    continue;

                redstingray[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < atlanticcod.Length; i++)
            {
                if (!atlanticcod[i].activeSelf)
                    continue;

                atlanticcod[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < stripedbass.Length; i++)
            {
                if (!stripedbass[i].activeSelf)
                    continue;

                stripedbass[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < seabass.Length; i++)
            {
                if (!seabass[i].activeSelf)
                    continue;

                seabass[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < yellowtailamberjack.Length; i++)
            {
                if (!yellowtailamberjack[i].activeSelf)
                    continue;

                yellowtailamberjack[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < indopacificsailfish.Length; i++)
            {
                if (!indopacificsailfish[i].activeSelf)
                    continue;

                indopacificsailfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
            for (int i = 0; i < swordfish.Length; i++)
            {
                if (!swordfish[i].activeSelf)
                    continue;

                swordfish[i].GetComponent<FishSkyway>().SearchRange += 1;
            }
        }
    }
}