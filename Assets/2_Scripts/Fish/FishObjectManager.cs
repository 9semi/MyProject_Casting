using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishObjectManager : MonoBehaviour
{
    PetManager _petManager;
    GameManager _gameManager;
    FishingBot _fishingBot;

    #region 물고기 프리펩 변수
    // 비회유어종 - 잡어(7)
    [SerializeField] GameObject finespottedflounderPrefab; // 도다리
    [SerializeField] GameObject brownsolePrefab; // 참가자미
    [SerializeField] GameObject fatgreenlingPrefab; // 쥐노래미
    [SerializeField] GameObject spottybellygreenlingPrefab; // 노래미
    [SerializeField] GameObject surfperchPrefab;// 망상어
    [SerializeField] GameObject sandsmeltPrefab; // 보리멸
    [SerializeField] GameObject pacificherringPrefab;// 청어
    // 비회유어종 - 일반(11)
    [SerializeField] GameObject bigfinsquidPrefab; // 무늬오징어
    [SerializeField] GameObject commonoctopusPrefab; // 참문어
    [SerializeField] GameObject oliveflounderPrefab;// 넙치
    [SerializeField] GameObject darkbandedrockfishPrefab; // 볼락
    [SerializeField] GameObject koreanrockfishPrefab; // 조피볼락
    [SerializeField] GameObject goldeyerockfishPrefab; // 불볼락
    [SerializeField] GameObject spotbellyrockfishPrefab; // 개볼락
    [SerializeField] GameObject whitespottedcongerPrefab; // 붕장어
    [SerializeField] GameObject indianflatheadPrefab; // 양태
    [SerializeField] GameObject bluefingurnardPrefab; // 성대
    [SerializeField] GameObject sandfishPrefab; // 도루묵    
    // 비회유어종 - 희귀(2)
    [SerializeField] GameObject blackporgyPrefab; // 감성돔
    [SerializeField] GameObject largescaleblackfishPrefab;// 벵에돔
    // 회유어종 - 잡어(5)
    [SerializeField] GameObject babyseabassPrefab; // 새끼농어
    [SerializeField] GameObject mackerelPrefab;// 고등어
    [SerializeField] GameObject horsemackerelPrefab; // 전갱이
    [SerializeField] GameObject konosiruspunctatusPrefab; // 전어
    [SerializeField] GameObject halfbeakPrefab; // 학공치
    // 회유어종 - 일반(3)
    [SerializeField] GameObject okhotskatkamackerelPrefab; // 임연수어
    [SerializeField] GameObject bigscaledredfinPrefab; // 황어
    [SerializeField] GameObject flatheadgreymulletPrefab; //숭어
    // 회유어종 - 희귀(4)
    [SerializeField] GameObject japaneseamberjackPrefab; // 방어
    [SerializeField] GameObject japanesespanishmackerelPrefab; // 삼치
    [SerializeField] GameObject seabassPrefab;// 농어
    [SerializeField] GameObject spottedseabassPrefab; // 점농어
    #endregion

    [SerializeField] List<AquariumFishDB> _aquariumFishDB = new List<AquariumFishDB>();
    [SerializeField] Transform[] distinctionObject; // 0. 끝포인트 1. 급심지대 2. 암초 3. 사나질  4. 해조류 5. 물골

    #region 물고기 마리수 지정 
    // 비회유어종 - 잡어(7)
    GameObject[] finespottedflounder = new GameObject[18]; // 도다리
     GameObject[] brownsole = new GameObject[18];  // 참가자미 
     GameObject[] fatgreenling = new GameObject[26]; // 쥐노래미
     GameObject[] spottybellygreenling = new GameObject[18];// 노래미
     GameObject[] surfperch = new GameObject[18];// 망상어
     GameObject[] sandsmelt = new GameObject[18]; // 보리멸
     GameObject[] pacificherring = new GameObject[18]; // 청어
    // 비회유어종 - 일반(11)
     GameObject[] bigfinsquid = new GameObject[18]; // 무늬오징어
     GameObject[] commonoctopus = new GameObject[12]; // 참문어
     GameObject[] oliveflounder = new GameObject[12]; // 넙치
     GameObject[] darkbandedrockfish = new GameObject[18]; // 볼락
     GameObject[] koreanrockfish = new GameObject[18]; // 조피볼락
     GameObject[] goldeyerockfish = new GameObject[12]; // 불볼락
     GameObject[] spotbellyrockfish = new GameObject[12]; // 개볼락
     GameObject[] whitespottedconger = new GameObject[18]; // 붕장어
     GameObject[] indianflathead = new GameObject[12]; // 양태
     GameObject[] bluefingurnard = new GameObject[12]; // 성대
     GameObject[] sandfish = new GameObject[18]; // 도루묵
    // 비회유어종 - 희귀(2)
     GameObject[] blackporgy = new GameObject[5]; // 감성돔
     GameObject[] largescaleblackfish = new GameObject[5]; // 벵에돔
    // 회유어종 - 잡어(5)
     GameObject[] babyseabass = new GameObject[30]; // 새끼농어
     GameObject[] mackerel = new GameObject[30]; // 고등어
     GameObject[] horsemackerel = new GameObject[30]; // 전갱이
     GameObject[] konosiruspunctatus = new GameObject[30]; // 전어
     GameObject[] halfbeak = new GameObject[30]; // 학공치
    // 회유어종 - 일반(3)
    GameObject[] okhotskatkamackerel = new GameObject[30]; // 임연수어 
    GameObject[] bigscaledredfin = new GameObject[30]; // 황어
    GameObject[] flatheadgreymullet = new GameObject[30]; // 숭어
    // 회유어종 - 희귀(4)    
     GameObject[] japaneseamberjack = new GameObject[10]; // 방어
     GameObject[] japanesespanishmackerel = new GameObject[10]; // 삼치
     GameObject[] seabass = new GameObject[5]; // 농어
     GameObject[] spottedseabass = new GameObject[5]; // 점농어
    #endregion
    
    bool _isIncreaseWhenNeedleStay = false;
    bool _isIncreaseWhenNeedleMove = false;
    bool _isFirstSpawn;

    List<GameObject> _allFishList = new List<GameObject>(256); public List<GameObject> _AllFishList { get { return _allFishList; } }
    List<GameObject> _rareFishList = new List<GameObject>(); public List<GameObject> _RareFishList { get { return _rareFishList; } }

    // 각 떡밥에 영향을 받는 물고기 리스트
    // 곤쟁이 새우
    List<GameObject> _influencedFishListByOpossumshrimp;
    // 크릴
    List<GameObject> _influencedFishListByKrill;
    // 조개부스러기
    List<GameObject> _influencedFishListByClam;

    // 움직임에 따라 확률이 증가하는 물고기 리스트
    // 가만히
    List<Fish> _fishListIncreaseProbabilityWhenStill;
    // 움직임
    List<Fish> _fishListIncreaseProbabilityWhenMove;
    
    List<Fish> _caughtFishs = new List<Fish>(); public void AddCaughtFishList(Fish fishObject) { _caughtFishs.Add(fishObject); }
    
    private void Awake()
    {
        _isFirstSpawn = true;
        AssignmentFishs();
        CreateFish();

        DataManager.INSTANCE.SetFishObjectManagerInstance(this);
    }

    public void InitializeFishSearchRange()
    {
        if (_gameManager == null)
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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

    void CreateFish() // 물고기 생성함수
    {
        int index;

        // 도다리(급심지대, 사나질, 물골)
        for (index = 0; index < finespottedflounder.Length; index++)
        {
            finespottedflounder[index] = Instantiate(finespottedflounderPrefab);    // 물고기 프리팹 생성
            finespottedflounder[index].transform.parent = distinctionObject[0].transform;   // 순번에 맞게 자식객체로

            finespottedflounder[index].SetActive(false);    // 비활성(프리팹 자체를 끄고 코드삭제해도 됨)
        }
        // 참가자미
        for (index = 0; index < brownsole.Length; index++)
        {
            brownsole[index] = Instantiate(brownsolePrefab);
            brownsole[index].transform.parent = distinctionObject[1].transform;
            brownsole[index].SetActive(false);
        }
        // 쥐노래미
        for (index = 0; index < fatgreenling.Length; index++)
        {
            fatgreenling[index] = Instantiate(fatgreenlingPrefab);
            fatgreenling[index].transform.parent = distinctionObject[2].transform;
            fatgreenling[index].SetActive(false);
        }
        // 노래미
        for (index = 0; index < spottybellygreenling.Length; index++)
        {
            spottybellygreenling[index] = Instantiate(spottybellygreenlingPrefab);
            spottybellygreenling[index].transform.parent = distinctionObject[3].transform;
            spottybellygreenling[index].SetActive(false);
        }
        // 망상어
        for (index = 0; index < surfperch.Length; index++)
        {
            surfperch[index] = Instantiate(surfperchPrefab);
            surfperch[index].transform.parent = distinctionObject[4].transform;
            surfperch[index].SetActive(false);
        }
        // 보리멸
        for (index = 0; index < sandsmelt.Length; index++)
        {
            sandsmelt[index] = Instantiate(sandsmeltPrefab);
            sandsmelt[index].transform.parent = distinctionObject[5].transform;
            sandsmelt[index].SetActive(false);
        }
        // 청어
        for (index = 0; index < pacificherring.Length; index++)
        {
            pacificherring[index] = Instantiate(pacificherringPrefab);
            pacificherring[index].transform.parent = distinctionObject[6].transform;
            pacificherring[index].SetActive(false);
        }
        // 무늬오징어
        for (index = 0; index < bigfinsquid.Length; index++)
        {
            bigfinsquid[index] = Instantiate(bigfinsquidPrefab);
            bigfinsquid[index].transform.parent = distinctionObject[7].transform;
            bigfinsquid[index].SetActive(false);
        }
        // 참문어
        for (index = 0; index < commonoctopus.Length; index++)
        {
            commonoctopus[index] = Instantiate(commonoctopusPrefab);
            commonoctopus[index].transform.parent = distinctionObject[8].transform;
            commonoctopus[index].SetActive(false);
        }
        // 넙치
        for (index = 0; index < oliveflounder.Length; index++)
        {
            oliveflounder[index] = Instantiate(oliveflounderPrefab);
            oliveflounder[index].transform.parent = distinctionObject[9].transform;
            oliveflounder[index].SetActive(false);
        }
        // 볼락
        for (index = 0; index < darkbandedrockfish.Length; index++)
        {
            darkbandedrockfish[index] = Instantiate(darkbandedrockfishPrefab);
            darkbandedrockfish[index].transform.parent = distinctionObject[10].transform;
            darkbandedrockfish[index].SetActive(false);
        }
        // 조피볼락
        for (index = 0; index < koreanrockfish.Length; index++)
        {
            koreanrockfish[index] = Instantiate(koreanrockfishPrefab);
            koreanrockfish[index].transform.parent = distinctionObject[11].transform;
            koreanrockfish[index].SetActive(false);
        }
        // 불볼락
        for (index = 0; index < goldeyerockfish.Length; index++)
        {
            goldeyerockfish[index] = Instantiate(goldeyerockfishPrefab);
            goldeyerockfish[index].transform.parent = distinctionObject[12].transform;
            goldeyerockfish[index].SetActive(false);
        }
        // 개볼락
        for (index = 0; index < spotbellyrockfish.Length; index++)
        {
            spotbellyrockfish[index] = Instantiate(spotbellyrockfishPrefab);
            spotbellyrockfish[index].transform.parent = distinctionObject[13].transform;
            spotbellyrockfish[index].SetActive(false);
        }
        // 붕장어
        for (index = 0; index < whitespottedconger.Length; index++)
        {
            whitespottedconger[index] = Instantiate(whitespottedcongerPrefab);
            whitespottedconger[index].transform.parent = distinctionObject[14].transform;
            whitespottedconger[index].SetActive(false);
        }
        // 양태
        for (index = 0; index < indianflathead.Length; index++)
        {
            indianflathead[index] = Instantiate(indianflatheadPrefab);
            indianflathead[index].transform.parent = distinctionObject[15].transform;
            indianflathead[index].SetActive(false);
        }
        // 성대
        for (index = 0; index < bluefingurnard.Length; index++)
        {
            bluefingurnard[index] = Instantiate(bluefingurnardPrefab);
            bluefingurnard[index].transform.parent = distinctionObject[16].transform;
            bluefingurnard[index].SetActive(false);
        }
        // 도루묵
        for (index = 0; index < sandfish.Length; index++)
        {
            sandfish[index] = Instantiate(sandfishPrefab);
            sandfish[index].transform.parent = distinctionObject[17].transform;
            sandfish[index].SetActive(false);
        }
        // 감성돔
        for (index = 0; index < blackporgy.Length; index++)
        {
            blackporgy[index] = Instantiate(blackporgyPrefab);
            blackporgy[index].transform.parent = distinctionObject[18].transform;
            blackporgy[index].SetActive(false);
        }
        // 벵에돔
        for (index = 0; index < largescaleblackfish.Length; index++)
        {
            largescaleblackfish[index] = Instantiate(largescaleblackfishPrefab);
            largescaleblackfish[index].transform.parent = distinctionObject[19].transform;
            largescaleblackfish[index].SetActive(false);
        }
        // 새끼농어
        for (index = 0; index < babyseabass.Length; index++)
        {
            babyseabass[index] = Instantiate(babyseabassPrefab);
            babyseabass[index].transform.parent = distinctionObject[20].transform;
            babyseabass[index].SetActive(false);
        }
        // 고등어
        for (index = 0; index < mackerel.Length; index++)
        {
            mackerel[index] = Instantiate(mackerelPrefab);
            mackerel[index].transform.parent = distinctionObject[21].transform;
            mackerel[index].SetActive(false);
        }
        // 전갱이
        for (index = 0; index < horsemackerel.Length; index++)
        {
            horsemackerel[index] = Instantiate(horsemackerelPrefab);
            horsemackerel[index].transform.parent = distinctionObject[22].transform;
            horsemackerel[index].SetActive(false);
        }
        // 전어
        for (index = 0; index < konosiruspunctatus.Length; index++)
        {
            konosiruspunctatus[index] = Instantiate(konosiruspunctatusPrefab);
            konosiruspunctatus[index].transform.parent = distinctionObject[23].transform;
            konosiruspunctatus[index].SetActive(false);
        }
        // 학공치
        for (index = 0; index < halfbeak.Length; index++)
        {
            halfbeak[index] = Instantiate(halfbeakPrefab);
            halfbeak[index].transform.parent = distinctionObject[24].transform;
            halfbeak[index].SetActive(false);
        }
        // 임연수어
        for (index = 0; index < okhotskatkamackerel.Length; index++)
        {
            okhotskatkamackerel[index] = Instantiate(okhotskatkamackerelPrefab);
            okhotskatkamackerel[index].transform.parent = distinctionObject[25].transform;
            okhotskatkamackerel[index].SetActive(false);
        }
        // 황어
        for (index = 0; index < bigscaledredfin.Length; index++)
        {
            bigscaledredfin[index] = Instantiate(bigscaledredfinPrefab);
            bigscaledredfin[index].transform.parent = distinctionObject[26].transform;
            bigscaledredfin[index].SetActive(false);
        }
        // 숭어
        for (index = 0; index < flatheadgreymullet.Length; index++)
        {
            flatheadgreymullet[index] = Instantiate(flatheadgreymulletPrefab);
            flatheadgreymullet[index].transform.parent = distinctionObject[27].transform;
            flatheadgreymullet[index].SetActive(false);
        }
        // 방어
        for (index = 0; index < japaneseamberjack.Length; index++)
        {
            japaneseamberjack[index] = Instantiate(japaneseamberjackPrefab);
            japaneseamberjack[index].transform.parent = distinctionObject[28].transform;
            japaneseamberjack[index].SetActive(false);

        }
        // 삼치
        for (index = 0; index < japanesespanishmackerel.Length; index++)
        {
            japanesespanishmackerel[index] = Instantiate(japanesespanishmackerelPrefab);
            japanesespanishmackerel[index].transform.parent = distinctionObject[29].transform;
            japanesespanishmackerel[index].SetActive(false);

        }
        // 농어
        for (index = 0; index < seabass.Length; index++)
        {
            seabass[index] = Instantiate(seabassPrefab);
            seabass[index].transform.parent = distinctionObject[30].transform;
            seabass[index].SetActive(false);

        }
        // 점농어
        for (index = 0; index < spottedseabass.Length; index++)
        {
            spottedseabass[index] = Instantiate(spottedseabassPrefab);
            spottedseabass[index].transform.parent = distinctionObject[31].transform;
            spottedseabass[index].SetActive(false);
        }
    }

    // 물고기 생성
    public void ReActiveFish(int worldTime)
    {
       // Debug.Log(_caughtFishs.Count);

        if(_caughtFishs.Count > 0)
        {
            for(int i = 0; i < _caughtFishs.Count; i++)
            {
                _caughtFishs[i].transform.SetParent(_caughtFishs[i]._originParent);
                _caughtFishs[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            _caughtFishs = new List<Fish>();
        }

        // 오브젝트 풀링
        for (int i = 0; i < 5; i++)
        {
            blackporgy[i].SetActive(false);  // 감성돔
            largescaleblackfish[i].SetActive(false);    // 벵에돔
            seabass[i].SetActive(false);  // 농어
            spottedseabass[i].SetActive(false);  // 점농어
        }
        for (int i = 0; i < 10; i++)
        {
            japaneseamberjack[i].SetActive(false); // 방어
            japanesespanishmackerel[i].SetActive(false);  // 삼치
        }

        for (int i = 0; i < 12; i++)
        {
            commonoctopus[i].SetActive(false);   // 참문어
            oliveflounder[i].SetActive(false);  // 넙치
            goldeyerockfish[i].SetActive(false);    // 불볼락
            spotbellyrockfish[i].SetActive(false);  // 개볼락
            indianflathead[i].SetActive(false);  // 양태
            bluefingurnard[i].SetActive(false);  // 성대
        }
        for (int i = 0; i < 18; i++)
        {
            finespottedflounder[i].SetActive(false); // 도다리
            brownsole[i].SetActive(false); // 참가자미
            spottybellygreenling[i].SetActive(false);  // 노래미
            surfperch[i].SetActive(false);  // 망상어
            sandsmelt[i].SetActive(false);  // 보리멸
            pacificherring[i].SetActive(false);   // 청어
            bigfinsquid[i].SetActive(false); // 무늬오징어
            darkbandedrockfish[i].SetActive(false);   // 볼락
            koreanrockfish[i].SetActive(false);   // 조피볼락
            whitespottedconger[i].SetActive(false);   // 붕장어
            sandfish[i].SetActive(false);   // 도루묵
        }
        for (int i = 0; i < 26; i++)
        {
            fatgreenling[i].SetActive(false);  // 쥐노래미
        }
        for (int i = 0; i < 30; i++)
        {
            babyseabass[i].SetActive(false);   // 새끼농어
            mackerel[i].SetActive(false); // 고등어
            horsemackerel[i].SetActive(false);  // 전갱이
            konosiruspunctatus[i].SetActive(false); // 전어
            halfbeak[i].SetActive(false); // 학공치
            okhotskatkamackerel[i].SetActive(false); // 임연수어
            bigscaledredfin[i].SetActive(false);  // 황어
            flatheadgreymullet[i].SetActive(false); // 숭어
        }

        // 월드 시간에 맞춰 물고기 활성
        ActiveFish(worldTime);
        
    }
    private void ActiveFish(int worldTime)
    {
        int i; // for문용 변수
        int count; // 램던숫자 받아올 변수
        int choiceType=0; // 회유종 물고기 타입 변수
        int choice; // 회유종 물고기 선택용 변수
        int choiceTotal; // 회유종 물고기 총 종류 변수
        // 희귀종(잡어) 확인용 변수
        bool isCalttagu;
        bool isMackerel;
        bool isPompano;
        bool isKonosiruspunctatus;
        bool isHyporhamphussajori;

        switch (worldTime)
        {
            #region #0시 리스폰
            case 0:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    _allFishList.Add(finespottedflounder[i]);
                    finespottedflounder[i].SetActive(true);
                    finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true);
                    brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true);
                    fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true);
                    spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); 
                    surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {

                    sandsmelt[i].SetActive(true); 
                    sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true);
                    pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); 
                    bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); 
                    commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); 
                    oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);

                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);

                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;
                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if(choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if(choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if(choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if(choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3);
                        //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        //choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            //choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);

                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);

                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0; 
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #1시 리스폰
            case 1:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                //count = 18;
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------                
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #2시 리스폰
            case 2:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }

                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #3시 리스폰
            case 3:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }

                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #4시 리스폰
            case 4:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }

                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #5시 리스폰
            case 5:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 삼치
                        choice = Random.Range(0, 2); //Debug.Log("희귀" + choice);
                        if (choice.Equals(0))
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japanesespanishmackerel[i].SetActive(true); japanesespanishmackerel[i].GetComponent<Fish>().SetData(16);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            //else
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }

                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #6시 리스폰
            case 6:
                // 비회유종------------------------------------------------
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                //보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(2);
                //Debug.Log(DataManager.INSTANCE._tutorialIsInProgress);
                if (DataManager.INSTANCE._tutorialIsInProgress)
                {
                    for (i = 0; i < blackporgy.Length; i++)
                    {
                        blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                    }
                }
                else
                {
                    for (i = 0; i < count; i++)
                    {
                        blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                    }
                }

                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어 2 일반 1 희귀 1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)                     
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        // 삼치
                        else
                        {
                            count = RandomCount(5);
                            for (i = 0; i < count; i++)
                            {
                                japanesespanishmackerel[i].SetActive(true); japanesespanishmackerel[i].GetComponent<Fish>().SetData(16);
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #7시 리스폰
            case 7:
                // 비회유종------------------------------------------------
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어 2 일반 1 희귀 1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        // 방어
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        // 삼치
                        //if (choiceTotal.Equals(0))
                        else
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japanesespanishmackerel[i].SetActive(true); japanesespanishmackerel[i].GetComponent<Fish>().SetData(16);
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #8시 리스폰
            case 8:
                // 비회유종------------------------------------------------ 
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------ 
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #9시 리스폰
            case 9:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #10시 리스폰
            case 10:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #11시 리스폰
            case 11:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #12시 리스폰
            case 12:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #13시 리스폰
            case 13:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                //count = 18;
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #14시 리스폰
            case 14:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                //count = 18;
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #15시 리스폰
            case 15:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)                        
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #16시 리스폰
            case 16:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #17시 리스폰
            case 17:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #18시 리스폰
            case 18:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(9);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(2);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        // 방어
                        count = RandomCount(5); //Debug.Log("희귀" + 1);
                        for (i = 0; i < count; i++)
                        {
                            japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 25 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 50 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            //else if (choice < 70 && !isPompano)
                            //{
                            //    //Debug.Log("전갱");
                            //    choiceTotal++;
                            //    isPompano = true;
                            //    count = RandomCount(10);
                            //    for (i = 0; i < count; i++)
                            //    {
                            //        horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                            //    }
                            //}
                            else if (choice < 75 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #19시 리스폰
            case 19:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #20시 리스폰
            case 20:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }

                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #21시 리스폰
            case 21:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)   
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #22시 리스폰
            case 22:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                           // if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            #region #23시 리스폰
            case 23:
                // 비회유종------------------------------------------------                
                // 도다리
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    finespottedflounder[i].SetActive(true); finespottedflounder[i].GetComponent<Fish>().SetData(6);
                }
                // 참가자미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    brownsole[i].SetActive(true); brownsole[i].GetComponent<Fish>().SetData(27);
                }
                // 쥐노래미
                count = RandomCount(3);
                for (i = 0; i < count; i++)
                {
                    fatgreenling[i].SetActive(true); fatgreenling[i].GetComponent<Fish>().SetData(26);
                }
                // 노래미
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    spottybellygreenling[i].SetActive(true); spottybellygreenling[i].GetComponent<Fish>().SetData(4);
                }
                // 망상어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    surfperch[i].SetActive(true); surfperch[i].GetComponent<Fish>().SetData(8);
                }
                // 보리멸
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    sandsmelt[i].SetActive(true); sandsmelt[i].GetComponent<Fish>().SetData(12);
                }
                // 청어
                count = RandomCount(7);
                for (i = 0; i < count; i++)
                {
                    pacificherring[i].SetActive(true); pacificherring[i].GetComponent<Fish>().SetData(29);
                }
                // 무늬오징어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    bigfinsquid[i].SetActive(true); bigfinsquid[i].GetComponent<Fish>().SetData(9);
                }
                // 참문어
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    commonoctopus[i].SetActive(true); commonoctopus[i].GetComponent<Fish>().SetData(28);
                }
                // 넙치
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    oliveflounder[i].SetActive(true); oliveflounder[i].GetComponent<Fish>().SetData(3);
                }
                // 볼락
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    darkbandedrockfish[i].SetActive(true); darkbandedrockfish[i].GetComponent<Fish>().SetData(13);
                }
                // 조피볼락
                count = RandomCount(1);
                for (i = 0; i < count; i++)
                {
                    koreanrockfish[i].SetActive(true); koreanrockfish[i].GetComponent<Fish>().SetData(25);
                }
                // 불볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    goldeyerockfish[i].SetActive(true); goldeyerockfish[i].GetComponent<Fish>().SetData(14);
                }
                // 개볼락
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    spotbellyrockfish[i].SetActive(true); spotbellyrockfish[i].GetComponent<Fish>().SetData(1);
                }
                // 붕장어
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    whitespottedconger[i].SetActive(true); whitespottedconger[i].GetComponent<Fish>().SetData(15);
                }
                // 양태
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    indianflathead[i].SetActive(true); indianflathead[i].GetComponent<Fish>().SetData(20);
                }
                // 성대
                count = RandomCount(6);
                for (i = 0; i < count; i++)
                {
                    bluefingurnard[i].SetActive(true); bluefingurnard[i].GetComponent<Fish>().SetData(18);
                }
                // 도루묵
                count = RandomCount(8);
                for (i = 0; i < count; i++)
                {
                    sandfish[i].SetActive(true); sandfish[i].GetComponent<Fish>().SetData(7);
                }
                // 감성돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    blackporgy[i].SetActive(true); blackporgy[i].GetComponent<Fish>().SetData(99);
                }
                // 벵에돔
                count = RandomCount(0);
                for (i = 0; i < count; i++)
                {
                    largescaleblackfish[i].SetActive(true); largescaleblackfish[i].GetComponent<Fish>().SetData(11);
                }
                // 회유종------------------------------------------------
                choiceType = Random.Range(0, 2);
                switch (choiceType)
                {
                    // 잡어2 일반1 희귀1------------------------------------------------
                    case 0:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 2;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)     
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        // 회유종(희귀)
                        choiceTotal = 0;
                        // 방어
                        choice = Random.Range(0, 10); //Debug.Log("희귀" + choice);
                        if (choice < 3)
                        {
                            count = RandomCount(5);
                            choiceTotal++;
                            for (i = 0; i < count; i++)
                            {
                                japaneseamberjack[i].SetActive(true); japaneseamberjack[i].GetComponent<Fish>().SetData(10);
                            }
                        }
                        else
                        {
                            // 농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    seabass[i].SetActive(true); seabass[i].GetComponent<Fish>().SetData(5);
                                }
                            }
                            // 점농어
                            //if (choiceTotal.Equals(0))
                            {
                                count = RandomCount(4);
                                for (i = 0; i < count; i++)
                                {
                                    spottedseabass[i].SetActive(true); spottedseabass[i].GetComponent<Fish>().SetData(24);
                                }
                            }
                        }
                        break;
                    // 잡어 3 일반 1 희귀 0------------------------------------------------
                    case 1:
                        // 회유종(잡어)
                        isCalttagu = false; isMackerel = false; isPompano = false;
                        isKonosiruspunctatus = false; isHyporhamphussajori = false;

                        for (choiceTotal = 0; choiceTotal < 3;)
                        {
                            choice = Random.Range(0, 100);

                            if (choice < 15 && !isCalttagu)
                            {
                                //Debug.Log("새끼농어");     
                                choiceTotal++;
                                isCalttagu = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    babyseabass[i].SetActive(true); babyseabass[i].GetComponent<Fish>().SetData(17);
                                }
                            }
                            else if (choice < 30 && !isMackerel)
                            {
                                //Debug.Log("고등어");
                                choiceTotal++;
                                isMackerel = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    mackerel[i].SetActive(true); mackerel[i].GetComponent<Fish>().SetData(2);
                                }
                            }
                            else if (choice < 70 && !isPompano)
                            {
                                //Debug.Log("전갱");
                                choiceTotal++;
                                isPompano = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    horsemackerel[i].SetActive(true); horsemackerel[i].GetComponent<Fish>().SetData(22);
                                }
                            }
                            else if (choice < 85 && !isKonosiruspunctatus)
                            {
                                //Debug.Log("전어");
                                choiceTotal++;
                                isKonosiruspunctatus = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    konosiruspunctatus[i].SetActive(true); konosiruspunctatus[i].GetComponent<Fish>().SetData(23);
                                }
                            }
                            else if (choice < 100 && !isHyporhamphussajori)
                            {
                                //Debug.Log("학공치");
                                choiceTotal++;
                                isHyporhamphussajori = true;
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    halfbeak[i].SetActive(true); halfbeak[i].GetComponent<Fish>().SetData(30);
                                }
                            }
                        }
                        // 회유종(일반)                        
                        choiceTotal = 0;
                        choice = Random.Range(0, 3); //Debug.Log("일반" + choice);
                        switch (choice)
                        {
                            // 임연수어
                            case 0:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    okhotskatkamackerel[i].SetActive(true); okhotskatkamackerel[i].GetComponent<Fish>().SetData(21);
                                }
                                break;
                            // 황어
                            case 1:
                                count = RandomCount(10);
                                choiceTotal++;
                                for (i = 0; i < count; i++)
                                {
                                    bigscaledredfin[i].SetActive(true); bigscaledredfin[i].GetComponent<Fish>().SetData(31);
                                }
                                break;
                            // 숭어
                            case 2:
                                count = RandomCount(10);
                                for (i = 0; i < count; i++)
                                {
                                    flatheadgreymullet[i].SetActive(true); flatheadgreymullet[i].GetComponent<Fish>().SetData(19);
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion
            default:
                worldTime = 0;
                break;
        }

        _rareFishList = new List<GameObject>();

        if (DataManager.INSTANCE._tutorialIsInProgress)
        {
           // Debug.Log(1);
            // tutorial이라면 감성돔 따로 다 담아야한다.
            for (int k = 0; k < blackporgy.Length; k++)
            {
                if (!blackporgy[k].activeSelf)
                    blackporgy[k].SetActive(true);

                _rareFishList.Add(blackporgy[k]);
            }
        }
        else
        {
            CheckInfluencedPastebaitFish();
            CheckFishAccordingToMovement();
            RareFishCheck();
            DataManager.INSTANCE.CheckBaitProbability();
            
            InitializeFishSearchRange();

            if (_gameManager == null)
                _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            _gameManager.isNeedleMoving = false;
            _isIncreaseWhenNeedleMove = false;
            _isIncreaseWhenNeedleStay = false;


            if (DataManager.INSTANCE._matchGameIsInProgress)
            {
                if (_fishingBot == null)
                {
                    _fishingBot = GameObject.FindGameObjectWithTag("FishingBot").GetComponent<FishingBot>();
                    _fishingBot.CheckFish();
                }
                else
                {
                    _fishingBot.CheckFish();
                }
            }
        }

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
                count = Random.Range(3, 6);
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
            // default는 필요없음
            default:
                count = 1;
                break;            
        }
        return count;
    }
    void RareFishCheck()
    {
        // 감성돔
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;

            _rareFishList.Add(blackporgy[i]);
        }

        // 벵에돔
        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (!largescaleblackfish[i].activeSelf)
                continue;

            _rareFishList.Add(largescaleblackfish[i]);
        }
        // 방어
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;

            _rareFishList.Add(japaneseamberjack[i]);
        }
        // 삼치
        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (!japanesespanishmackerel[i].activeSelf)
                continue;

            _rareFishList.Add(japanesespanishmackerel[i]);
        }
        // 농어
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;

            _rareFishList.Add(seabass[i]);
        }
        // 점농어
        for (int i = 0; i < spottedseabass.Length; i++)
        {
            if (!spottedseabass[i].activeSelf)
                continue;

            _rareFishList.Add(spottedseabass[i]);
        }


    }
    public void RemoveRareFishList(GameObject fishObject)
    {
        int index = _rareFishList.IndexOf(fishObject);

        if (index.Equals(-1))
            return;

        _rareFishList.RemoveAt(index);
    }
    public void RadarSort()
    {
        // z 거리에 따라서 정렬해둔다.
        _rareFishList.Sort(delegate (GameObject f1, GameObject f2)
        {
            return f2.transform.position.z.CompareTo(f1.transform.position.z);
        });
    }
    void CheckInfluencedPastebaitFish()
    {
        _influencedFishListByOpossumshrimp = new List<GameObject>();
        _influencedFishListByKrill = new List<GameObject>();
        _influencedFishListByClam = new List<GameObject>();

        for (int i = 0; i < konosiruspunctatus.Length; i++)
        {
            if (konosiruspunctatus[i].activeSelf)
            {
                _influencedFishListByOpossumshrimp.Add(konosiruspunctatus[i]);
                _influencedFishListByKrill.Add(konosiruspunctatus[i]);
            }
            else
                continue ;
        }
        for (int i = 0; i < halfbeak.Length; i++)
        {
            if (halfbeak[i].activeSelf)
            {
                _influencedFishListByOpossumshrimp.Add(halfbeak[i]);
                _influencedFishListByKrill.Add(halfbeak[i]);
            }
            else
                continue;
        }
        for (int i = 0; i < okhotskatkamackerel.Length; i++)
        {
            if (okhotskatkamackerel[i].activeSelf)
            {
                _influencedFishListByOpossumshrimp.Add(okhotskatkamackerel[i]);
                _influencedFishListByKrill.Add(okhotskatkamackerel[i]);
            }
            else
                continue;
        }
        for (int i = 0; i < fatgreenling.Length; i++)
        {
            if (fatgreenling[i].activeSelf)
                _influencedFishListByKrill.Add(fatgreenling[i]);
            else
                continue;
        }
        for (int i = 0; i < spottybellygreenling.Length; i++)
        {
            if (spottybellygreenling[i].activeSelf)
                _influencedFishListByKrill.Add(spottybellygreenling[i]);
            else
                continue;
        }
        for (int i = 0; i < surfperch.Length; i++)
        {
            if (surfperch[i].activeSelf)
                _influencedFishListByKrill.Add(surfperch[i]);
            else
                continue;
        }
        for (int i = 0; i < pacificherring.Length; i++)
        {
            if (pacificherring[i].activeSelf)
                _influencedFishListByKrill.Add(pacificherring[i]);
            else
                continue;
        }
        for (int i = 0; i < bigfinsquid.Length; i++)
        {
            if (bigfinsquid[i].activeSelf)
                _influencedFishListByKrill.Add(bigfinsquid[i]);
            else
                continue;
        }
        for (int i = 0; i < oliveflounder.Length; i++)
        {
            if (oliveflounder[i].activeSelf)
                _influencedFishListByKrill.Add(oliveflounder[i]);
            else
                continue;
        }
        for (int i = 0; i < darkbandedrockfish.Length; i++)
        {
            if (darkbandedrockfish[i].activeSelf)
                _influencedFishListByKrill.Add(darkbandedrockfish[i]);
            else
                continue;
        }
        for (int i = 0; i < koreanrockfish.Length; i++)
        {
            if (koreanrockfish[i].activeSelf)
                _influencedFishListByKrill.Add(koreanrockfish[i]);
            else
                continue;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (blackporgy[i].activeSelf)
            {
                _influencedFishListByKrill.Add(blackporgy[i]);
                _influencedFishListByClam.Add(blackporgy[i]);
            }
            else
                continue;
        }
        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (largescaleblackfish[i].activeSelf)
                _influencedFishListByKrill.Add(largescaleblackfish[i]);
            else
                continue;
        }
        for (int i = 0; i < babyseabass.Length; i++)
        {
            if (babyseabass[i].activeSelf)
                _influencedFishListByKrill.Add(babyseabass[i]);
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
        for (int i = 0; i < horsemackerel.Length; i++)
        {
            if (horsemackerel[i].activeSelf)
                _influencedFishListByKrill.Add(horsemackerel[i]);
            else
                continue;
        }
        for (int i = 0; i < bigscaledredfin.Length; i++)
        {
            if (bigscaledredfin[i].activeSelf)
                _influencedFishListByKrill.Add(bigscaledredfin[i]);
            else
                continue;
        }
        for (int i = 0; i < flatheadgreymullet.Length; i++)
        {
            if (flatheadgreymullet[i].activeSelf)
                _influencedFishListByKrill.Add(flatheadgreymullet[i]);
            else
                continue;
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (japaneseamberjack[i].activeSelf)
                _influencedFishListByKrill.Add(japaneseamberjack[i]);
            else
                continue;
        }
        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (japanesespanishmackerel[i].activeSelf)
                _influencedFishListByKrill.Add(japanesespanishmackerel[i]);
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
    }
    void CheckFishAccordingToMovement()
    {
        _fishListIncreaseProbabilityWhenMove = new List<Fish>();
        _fishListIncreaseProbabilityWhenStill = new List<Fish>();

        // 움직임에 따라 확률이 증가하는 물고기들
        for (int i = 0; i < finespottedflounder.Length; i++)
        {
            if (finespottedflounder[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(finespottedflounder[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < brownsole.Length; i++)
        {
            if (brownsole[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(brownsole[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < fatgreenling.Length; i++)
        {
            if (fatgreenling[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(fatgreenling[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < spottybellygreenling.Length; i++)
        {
            if (spottybellygreenling[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(spottybellygreenling[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < sandsmelt.Length; i++)
        {
            if (sandsmelt[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(sandsmelt[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < pacificherring.Length; i++)
        {
            if (pacificherring[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(pacificherring[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < bigfinsquid.Length; i++)
        {
            if (bigfinsquid[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(bigfinsquid[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < commonoctopus.Length; i++)
        {
            if (commonoctopus[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(commonoctopus[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < oliveflounder.Length; i++)
        {
            if (oliveflounder[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(oliveflounder[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < koreanrockfish.Length; i++)
        {
            if (koreanrockfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(koreanrockfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < indianflathead.Length; i++)
        {
            if (indianflathead[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(indianflathead[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (bluefingurnard[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(bluefingurnard[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < sandfish.Length; i++)
        {
            if (sandfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(sandfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (blackporgy[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(blackporgy[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < konosiruspunctatus.Length; i++)
        {
            if (konosiruspunctatus[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(konosiruspunctatus[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < halfbeak.Length; i++)
        {
            if (halfbeak[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(halfbeak[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (japaneseamberjack[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(japaneseamberjack[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (japanesespanishmackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(japanesespanishmackerel[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < spottedseabass.Length; i++)
        {
            if (spottedseabass[i].activeSelf)
                _fishListIncreaseProbabilityWhenMove.Add(spottedseabass[i].GetComponent<Fish>());
            else
                continue;
        }

        // 가만히 있을 때 확률이 증가하는 물고기들
        for (int i = 0; i < surfperch.Length; i++)
        {
            if (surfperch[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(surfperch[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < darkbandedrockfish.Length; i++)
        {
            if (darkbandedrockfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(darkbandedrockfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < spotbellyrockfish.Length; i++)
        {
            if (spotbellyrockfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(spotbellyrockfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < whitespottedconger.Length; i++)
        {
            if (whitespottedconger[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(whitespottedconger[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < goldeyerockfish.Length; i++)
        {
            if (goldeyerockfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(goldeyerockfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (largescaleblackfish[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(largescaleblackfish[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < babyseabass.Length; i++)
        {
            if (babyseabass[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(babyseabass[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < mackerel.Length; i++)
        {
            if (mackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(mackerel[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < horsemackerel.Length; i++)
        {
            if (horsemackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(horsemackerel[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < okhotskatkamackerel.Length; i++)
        {
            if (okhotskatkamackerel[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(okhotskatkamackerel[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < bigscaledredfin.Length; i++)
        {
            if (bigscaledredfin[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(bigscaledredfin[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < flatheadgreymullet.Length; i++)
        {
            if (flatheadgreymullet[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(flatheadgreymullet[i].GetComponent<Fish>());
            else
                continue;
        }

        for (int i = 0; i < seabass.Length; i++)
        {
            if (seabass[i].activeSelf)
                _fishListIncreaseProbabilityWhenStill.Add(seabass[i].GetComponent<Fish>());
            else
                continue;
        }
    }
    public void UpdateProbilityWhenLampOn()
    {
        // 도다리
        for (int i = 0; i < finespottedflounder.Length; i++)
        {
            if (!finespottedflounder[i].activeSelf)
                continue;

            if (finespottedflounder[i].GetComponent<Fish>().SearchRange > 0)
                finespottedflounder[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < brownsole.Length; i++)
        {
            if (!brownsole[i].activeSelf)
                continue;

            if (brownsole[i].GetComponent<Fish>().SearchRange > 0)
                brownsole[i].GetComponent<Fish>().SearchRange -=1;
        }
        for (int i = 0; i < fatgreenling.Length; i++)
        {
            if (!fatgreenling[i].activeSelf)
                continue;

            if (fatgreenling[i].GetComponent<Fish>().SearchRange > 0)
                fatgreenling[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < spottybellygreenling.Length; i++)
        {
            if (!spottybellygreenling[i].activeSelf)
                continue;

            if (spottybellygreenling[i].GetComponent<Fish>().SearchRange > 0)
                spottybellygreenling[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < surfperch.Length; i++)
        {
            if (!surfperch[i].activeSelf)
                continue;

            if (surfperch[i].GetComponent<Fish>().SearchRange > 0)
                surfperch[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < sandsmelt.Length; i++)
        {
            if (!sandsmelt[i].activeSelf)
                continue;

            if (sandsmelt[i].GetComponent<Fish>().SearchRange > 0)
                sandsmelt[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < pacificherring.Length; i++)
        {
            if (!pacificherring[i].activeSelf)
                continue;

            if (pacificherring[i].GetComponent<Fish>().SearchRange > 0)
                pacificherring[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < bigfinsquid.Length; i++)
        {
            if (!bigfinsquid[i].activeSelf)
                continue;

            bigfinsquid[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < commonoctopus.Length; i++)
        {
            if (!commonoctopus[i].activeSelf)
                continue;

            if (commonoctopus[i].GetComponent<Fish>().SearchRange > 0)
                commonoctopus[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < oliveflounder.Length; i++)
        {
            if (!oliveflounder[i].activeSelf)
                continue;

            if (oliveflounder[i].GetComponent<Fish>().SearchRange > 0)
                oliveflounder[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < darkbandedrockfish.Length; i++)
        {
            if (!darkbandedrockfish[i].activeSelf)
                continue;

            darkbandedrockfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < koreanrockfish.Length; i++)
        {
            if (!koreanrockfish[i].activeSelf)
                continue;

            if (koreanrockfish[i].GetComponent<Fish>().SearchRange > 0)
                koreanrockfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < goldeyerockfish.Length; i++)
        {
            if (!goldeyerockfish[i].activeSelf)
                continue;

            if (goldeyerockfish[i].GetComponent<Fish>().SearchRange > 0)
                goldeyerockfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < spotbellyrockfish.Length; i++)
        {
            if (!spotbellyrockfish[i].activeSelf)
                continue;

            if (spotbellyrockfish[i].GetComponent<Fish>().SearchRange > 0)
                spotbellyrockfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < whitespottedconger.Length; i++)
        {
            if (!whitespottedconger[i].activeSelf)
                continue;

            if (whitespottedconger[i].GetComponent<Fish>().SearchRange > 0)
                whitespottedconger[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < indianflathead.Length; i++)
        {
            if (!indianflathead[i].activeSelf)
                continue;

            if (indianflathead[i].GetComponent<Fish>().SearchRange > 0)
                indianflathead[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;

            if (bluefingurnard[i].GetComponent<Fish>().SearchRange > 0)
                bluefingurnard[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < sandfish.Length; i++)
        {
            if (!sandfish[i].activeSelf)
                continue;

            if (sandfish[i].GetComponent<Fish>().SearchRange > 0)
                sandfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;

            if (blackporgy[i].GetComponent<Fish>().SearchRange > 0)
                blackporgy[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (!largescaleblackfish[i].activeSelf)
                continue;

            if (largescaleblackfish[i].GetComponent<Fish>().SearchRange > 0)
                largescaleblackfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < babyseabass.Length; i++)
        {
            if (!babyseabass[i].activeSelf)
                continue;

            if (babyseabass[i].GetComponent<Fish>().SearchRange > 0)
                babyseabass[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;

            if (mackerel[i].GetComponent<Fish>().SearchRange > 0)
                mackerel[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < horsemackerel.Length; i++)
        {
            if (!horsemackerel[i].activeSelf)
                continue;

            if (horsemackerel[i].GetComponent<Fish>().SearchRange > 0)
                horsemackerel[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < konosiruspunctatus.Length; i++)
        {
            if (!konosiruspunctatus[i].activeSelf)
                continue;

            if (konosiruspunctatus[i].GetComponent<Fish>().SearchRange > 0)
                konosiruspunctatus[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < halfbeak.Length; i++)
        {
            if (!halfbeak[i].activeSelf)
                continue;

            if (halfbeak[i].GetComponent<Fish>().SearchRange > 0)
                halfbeak[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < okhotskatkamackerel.Length; i++)
        {
            if (!okhotskatkamackerel[i].activeSelf)
                continue;

            if (okhotskatkamackerel[i].GetComponent<Fish>().SearchRange > 0)
                okhotskatkamackerel[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < bigscaledredfin.Length; i++)
        {
            if (!bigscaledredfin[i].activeSelf)
                continue;

            if (bigscaledredfin[i].GetComponent<Fish>().SearchRange > 0)
                bigscaledredfin[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < flatheadgreymullet.Length; i++)
        {
            if (!flatheadgreymullet[i].activeSelf)
                continue;

            if (flatheadgreymullet[i].GetComponent<Fish>().SearchRange > 0)
                flatheadgreymullet[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;

            if (japaneseamberjack[i].GetComponent<Fish>().SearchRange > 0)
                japaneseamberjack[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (!japanesespanishmackerel[i].activeSelf)
                continue;

            if (japanesespanishmackerel[i].GetComponent<Fish>().SearchRange > 0)
                japanesespanishmackerel[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;

            if (seabass[i].GetComponent<Fish>().SearchRange > 0)
                seabass[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < spottedseabass.Length; i++)
        {
            if (!spottedseabass[i].activeSelf)
                continue;

            if (spottedseabass[i].GetComponent<Fish>().SearchRange > 0)
                spottedseabass[i].GetComponent<Fish>().SearchRange -= 1;
        }
    } // 램프 ON
    public void UpdateProbilityWhenLampOff()
    {
        // 도다리
        for (int i = 0; i < finespottedflounder.Length; i++)
        {
            if (!finespottedflounder[i].activeSelf)
                continue;

            finespottedflounder[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < brownsole.Length; i++)
        {
            if (!brownsole[i].activeSelf)
                continue;
            brownsole[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < fatgreenling.Length; i++)
        {
            if (!fatgreenling[i].activeSelf)
                continue;
            fatgreenling[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < spottybellygreenling.Length; i++)
        {
            if (!spottybellygreenling[i].activeSelf)
                continue;
            spottybellygreenling[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < surfperch.Length; i++)
        {
            if (!surfperch[i].activeSelf)
                continue;
            surfperch[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < sandsmelt.Length; i++)
        {
            if (!sandsmelt[i].activeSelf)
                continue;
            sandsmelt[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < pacificherring.Length; i++)
        {
            if (!pacificherring[i].activeSelf)
                continue;
            pacificherring[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < bigfinsquid.Length; i++)
        {
            if (!bigfinsquid[i].activeSelf)
                continue;

            if (bigfinsquid[i].GetComponent<Fish>().SearchRange > 0)
                bigfinsquid[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < commonoctopus.Length; i++)
        {
            if (!commonoctopus[i].activeSelf)
                continue;
            commonoctopus[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < oliveflounder.Length; i++)
        {
            if (!oliveflounder[i].activeSelf)
                continue;
            oliveflounder[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < darkbandedrockfish.Length; i++)
        {
            if (!darkbandedrockfish[i].activeSelf)
                continue;

            if (darkbandedrockfish[i].GetComponent<Fish>().SearchRange > 0)
                darkbandedrockfish[i].GetComponent<Fish>().SearchRange -= 1;
        }
        for (int i = 0; i < koreanrockfish.Length; i++)
        {
            if (!koreanrockfish[i].activeSelf)
                continue;
            koreanrockfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < goldeyerockfish.Length; i++)
        {
            if (!goldeyerockfish[i].activeSelf)
                continue;
            goldeyerockfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < spotbellyrockfish.Length; i++)
        {
            if (!spotbellyrockfish[i].activeSelf)
                continue;
            spotbellyrockfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < whitespottedconger.Length; i++)
        {
            if (!whitespottedconger[i].activeSelf)
                continue;
            whitespottedconger[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < indianflathead.Length; i++)
        {
            if (!indianflathead[i].activeSelf)
                continue;
            indianflathead[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;
            bluefingurnard[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < sandfish.Length; i++)
        {
            if (!sandfish[i].activeSelf)
                continue;
            sandfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;
            blackporgy[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (!largescaleblackfish[i].activeSelf)
                continue;
            largescaleblackfish[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < babyseabass.Length; i++)
        {
            if (!babyseabass[i].activeSelf)
                continue;
            babyseabass[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;
            mackerel[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < horsemackerel.Length; i++)
        {
            if (!horsemackerel[i].activeSelf)
                continue;
            horsemackerel[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < konosiruspunctatus.Length; i++)
        {
            if (!konosiruspunctatus[i].activeSelf)
                continue;
            konosiruspunctatus[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < halfbeak.Length; i++)
        {
            if (!halfbeak[i].activeSelf)
                continue;
            halfbeak[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < okhotskatkamackerel.Length; i++)
        {
            if (!okhotskatkamackerel[i].activeSelf)
                continue;
            okhotskatkamackerel[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < bigscaledredfin.Length; i++)
        {
            if (!bigscaledredfin[i].activeSelf)
                continue;
            bigscaledredfin[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < flatheadgreymullet.Length; i++)
        {
            if (!flatheadgreymullet[i].activeSelf)
                continue;
            flatheadgreymullet[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;
            japaneseamberjack[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (!japanesespanishmackerel[i].activeSelf)
                continue;
            japanesespanishmackerel[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;
            seabass[i].GetComponent<Fish>().SearchRange += 1;
        }
        for (int i = 0; i < spottedseabass.Length; i++)
        {
            if (!spottedseabass[i].activeSelf)
                continue;
            spottedseabass[i].GetComponent<Fish>().SearchRange += 1;
        }
        
        _isFirstSpawn = false;
    } // 램프 OFF
    public void UpdateRangeWhenThrowPastebait(float leftX, float rightX, float upZ, float downZ)
    {
        GameObject[] fishs;
        int cnt = 0;

        switch(DBManager.INSTANCE.GetUserData().GetCurrentEquipmentDictionary()["pastebait"])
        {
            case 0: // 곤쟁이떡밥
                fishs = new GameObject[_influencedFishListByOpossumshrimp.Count];
                for (int i = 0; i < _influencedFishListByOpossumshrimp.Count; i++)
                {
                    if (_influencedFishListByOpossumshrimp[i].transform.position.x > leftX && _influencedFishListByOpossumshrimp[i].transform.position.x < rightX
                        && _influencedFishListByOpossumshrimp[i].transform.position.z < upZ && _influencedFishListByOpossumshrimp[i].transform.position.z > downZ)
                    {
                        fishs[cnt] = _influencedFishListByOpossumshrimp[i].gameObject;
                        cnt++;
                    }
                }
                if (fishs.Length > 0)
                    PlusBaitProbability(fishs, 2);
                break;
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
            case 2: // 조개부스러기
                fishs = new GameObject[_influencedFishListByClam.Count];
                for (int i = 0; i < _influencedFishListByClam.Count; i++)
                {
                    if (_influencedFishListByClam[i].transform.position.x > leftX && _influencedFishListByClam[i].transform.position.x < rightX
                        && _influencedFishListByClam[i].transform.position.z < upZ && _influencedFishListByClam[i].transform.position.z > downZ)
                    {
                        fishs[cnt] = _influencedFishListByClam[i].gameObject;
                        cnt++;
                    }
                }
                if (fishs.Length > 0)
                    PlusBaitProbability(fishs, 2);
                break;
        }
    }
    void PlusBaitProbability(GameObject[] fishObject, int addRate)
    {
        for (int i = 0; i < fishObject.Length; i++)
        {
            if (fishObject[i] != null)
            {
                fishObject[i].GetComponent<Fish>().StartIncreaseSearchRangeOneMinute(addRate);
            }
            else 
                break;
        }
    }

    public void IncreaseProbabilityAccordingToMovement(bool isMoving)
    {
        //Debug.Log("움직임: " + _isIncreaseWhenNeedleMove + " , 정지: " + _isIncreaseWhenNeedleStay);

        // 움직임
        if (isMoving)
        {
            if(!_isIncreaseWhenNeedleMove)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenMove.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenMove[i].biteBait += 10f;
                }
                _isIncreaseWhenNeedleMove = true;
            }

            if (_isIncreaseWhenNeedleStay)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenStill[i].biteBait -= 10f; 
                }
                _isIncreaseWhenNeedleStay = false;
            }
        }

        // 멈춤
        else
        {
            if(_isIncreaseWhenNeedleMove)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenMove.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenMove[i].biteBait -= 10f;
                }
                _isIncreaseWhenNeedleMove = false;
            }

            if(!_isIncreaseWhenNeedleStay)
            {
                for (int i = 0; i < _fishListIncreaseProbabilityWhenStill.Count; i++)
                {
                    _fishListIncreaseProbabilityWhenStill[i].biteBait += 10f;
                }

                _isIncreaseWhenNeedleStay = true;
            }
        }
    }
    void AssignmentFishs()
    {
        finespottedflounder = new GameObject[18]; // 도다리
        brownsole = new GameObject[18];  // 참가자미 
        fatgreenling = new GameObject[26]; // 쥐노래미
        spottybellygreenling = new GameObject[18];// 노래미
        surfperch = new GameObject[18];// 망상어
        sandsmelt = new GameObject[18]; // 보리멸
        pacificherring = new GameObject[18]; // 청어

        bigfinsquid = new GameObject[18]; // 무늬오징어
        commonoctopus = new GameObject[12]; // 참문어
        oliveflounder = new GameObject[12]; // 넙치
        darkbandedrockfish = new GameObject[18]; // 볼락
        koreanrockfish = new GameObject[18]; // 조피볼락
        goldeyerockfish = new GameObject[12]; // 불볼락
        spotbellyrockfish = new GameObject[12]; // 개볼락
        whitespottedconger = new GameObject[18]; // 붕장어
        indianflathead = new GameObject[12]; // 양태
        bluefingurnard = new GameObject[12]; // 성대
        sandfish = new GameObject[18]; // 도루묵

        blackporgy = new GameObject[5]; // 감성돔
        largescaleblackfish = new GameObject[5]; // 벵에돔

        babyseabass = new GameObject[30]; // 새끼농어
        mackerel = new GameObject[30]; // 고등어
        horsemackerel = new GameObject[30]; // 전갱이
        konosiruspunctatus = new GameObject[30]; // 전어
        halfbeak = new GameObject[30]; // 학공치

        okhotskatkamackerel = new GameObject[30]; // 임연수어 
        bigscaledredfin = new GameObject[30]; // 황어
        flatheadgreymullet = new GameObject[30]; // 숭어

        japaneseamberjack = new GameObject[10]; // 방어
        japanesespanishmackerel = new GameObject[10]; // 삼치
        seabass = new GameObject[5]; // 농어
        spottedseabass = new GameObject[5]; // 점농어
    }

    public void UpdateFishSearchRange(int range)
    {
        for (int i = 0; i < finespottedflounder.Length; i++)
        {
            if (!finespottedflounder[i].activeSelf)
                continue;

            finespottedflounder[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < brownsole.Length; i++)
        {
            if (!brownsole[i].activeSelf)
                continue;

            brownsole[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < fatgreenling.Length; i++)
        {
            if (!fatgreenling[i].activeSelf)
                continue;

            fatgreenling[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < spottybellygreenling.Length; i++)
        {
            if (!spottybellygreenling[i].activeSelf)
                continue;

                spottybellygreenling[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < surfperch.Length; i++)
        {
            if (!surfperch[i].activeSelf)
                continue;

                surfperch[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < sandsmelt.Length; i++)
        {
            if (!sandsmelt[i].activeSelf)
                continue;

                sandsmelt[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < pacificherring.Length; i++)
        {
            if (!pacificherring[i].activeSelf)
                continue;

                pacificherring[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < bigfinsquid.Length; i++)
        {
            if (!bigfinsquid[i].activeSelf)
                continue;

            bigfinsquid[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < commonoctopus.Length; i++)
        {
            if (!commonoctopus[i].activeSelf)
                continue;

                commonoctopus[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < oliveflounder.Length; i++)
        {
            if (!oliveflounder[i].activeSelf)
                continue;

                oliveflounder[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < darkbandedrockfish.Length; i++)
        {
            if (!darkbandedrockfish[i].activeSelf)
                continue;

            darkbandedrockfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < koreanrockfish.Length; i++)
        {
            if (!koreanrockfish[i].activeSelf)
                continue;

                koreanrockfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < goldeyerockfish.Length; i++)
        {
            if (!goldeyerockfish[i].activeSelf)
                continue;

                goldeyerockfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < spotbellyrockfish.Length; i++)
        {
            if (!spotbellyrockfish[i].activeSelf)
                continue;

                spotbellyrockfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < whitespottedconger.Length; i++)
        {
            if (!whitespottedconger[i].activeSelf)
                continue;

                whitespottedconger[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < indianflathead.Length; i++)
        {
            if (!indianflathead[i].activeSelf)
                continue;

                indianflathead[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < bluefingurnard.Length; i++)
        {
            if (!bluefingurnard[i].activeSelf)
                continue;

                bluefingurnard[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < sandfish.Length; i++)
        {
            if (!sandfish[i].activeSelf)
                continue;

                sandfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < blackporgy.Length; i++)
        {
            if (!blackporgy[i].activeSelf)
                continue;

                blackporgy[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < largescaleblackfish.Length; i++)
        {
            if (!largescaleblackfish[i].activeSelf)
                continue;

                largescaleblackfish[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < babyseabass.Length; i++)
        {
            if (!babyseabass[i].activeSelf)
                continue;

                babyseabass[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < mackerel.Length; i++)
        {
            if (!mackerel[i].activeSelf)
                continue;

                mackerel[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < horsemackerel.Length; i++)
        {
            if (!horsemackerel[i].activeSelf)
                continue;

                horsemackerel[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < konosiruspunctatus.Length; i++)
        {
            if (!konosiruspunctatus[i].activeSelf)
                continue;

                konosiruspunctatus[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < halfbeak.Length; i++)
        {
            if (!halfbeak[i].activeSelf)
                continue;

                halfbeak[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < okhotskatkamackerel.Length; i++)
        {
            if (!okhotskatkamackerel[i].activeSelf)
                continue;

                okhotskatkamackerel[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < bigscaledredfin.Length; i++)
        {
            if (!bigscaledredfin[i].activeSelf)
                continue;

                bigscaledredfin[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < flatheadgreymullet.Length; i++)
        {
            if (!flatheadgreymullet[i].activeSelf)
                continue;

                flatheadgreymullet[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < japaneseamberjack.Length; i++)
        {
            if (!japaneseamberjack[i].activeSelf)
                continue;

                japaneseamberjack[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < japanesespanishmackerel.Length; i++)
        {
            if (!japanesespanishmackerel[i].activeSelf)
                continue;

                japanesespanishmackerel[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < seabass.Length; i++)
        {
            if (!seabass[i].activeSelf)
                continue;

                seabass[i].GetComponent<Fish>().SettingSearchRange(range);
        }
        for (int i = 0; i < spottedseabass.Length; i++)
        {
            if (!spottedseabass[i].activeSelf)
                continue;

                spottedseabass[i].GetComponent<Fish>().SettingSearchRange(range);
        }

        // 램프 여부를 확인해서 해당 물고기만 1씩 올리면 된다.
        // UpdateProbilityWhenLampOn(); 사용하면 안되고 따로 만들어야 한다.
        LampCheck();
    }

    void LampCheck()
    {
        if (_petManager == null)
            _petManager = GameObject.FindGameObjectWithTag("Pet").GetComponent<PetManager>();

        if (_petManager.isLightOn)
        {
            for (int i = 0; i < bigfinsquid.Length; i++)
            {
                if (!bigfinsquid[i].activeSelf)
                    continue;

                bigfinsquid[i].GetComponent<Fish>().SearchRange += 1;
            }

            for (int i = 0; i < darkbandedrockfish.Length; i++)
            {
                if (!darkbandedrockfish[i].activeSelf)
                    continue;

                darkbandedrockfish[i].GetComponent<Fish>().SearchRange += 1;
            }
        }
        else
        {
            // 도다리
            for (int i = 0; i < finespottedflounder.Length; i++)
            {
                if (!finespottedflounder[i].activeSelf)
                    continue;

                finespottedflounder[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < brownsole.Length; i++)
            {
                if (!brownsole[i].activeSelf)
                    continue;

                brownsole[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < fatgreenling.Length; i++)
            {
                if (!fatgreenling[i].activeSelf)
                    continue;

                fatgreenling[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < spottybellygreenling.Length; i++)
            {
                if (!spottybellygreenling[i].activeSelf)
                    continue;

                spottybellygreenling[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < surfperch.Length; i++)
            {
                if (!surfperch[i].activeSelf)
                    continue;

                surfperch[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < sandsmelt.Length; i++)
            {
                if (!sandsmelt[i].activeSelf)
                    continue;

                sandsmelt[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < pacificherring.Length; i++)
            {
                if (!pacificherring[i].activeSelf)
                    continue;

                pacificherring[i].GetComponent<Fish>().SearchRange += 1;
            }

            for (int i = 0; i < commonoctopus.Length; i++)
            {
                if (!commonoctopus[i].activeSelf)
                    continue;

                commonoctopus[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < oliveflounder.Length; i++)
            {
                if (!oliveflounder[i].activeSelf)
                    continue;

                oliveflounder[i].GetComponent<Fish>().SearchRange += 1;
            }

            for (int i = 0; i < koreanrockfish.Length; i++)
            {
                if (!koreanrockfish[i].activeSelf)
                    continue;

                koreanrockfish[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < goldeyerockfish.Length; i++)
            {
                if (!goldeyerockfish[i].activeSelf)
                    continue;

                goldeyerockfish[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < spotbellyrockfish.Length; i++)
            {
                if (!spotbellyrockfish[i].activeSelf)
                    continue;

                spotbellyrockfish[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < whitespottedconger.Length; i++)
            {
                if (!whitespottedconger[i].activeSelf)
                    continue;

                whitespottedconger[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < indianflathead.Length; i++)
            {
                if (!indianflathead[i].activeSelf)
                    continue;

                indianflathead[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < bluefingurnard.Length; i++)
            {
                if (!bluefingurnard[i].activeSelf)
                    continue;

                bluefingurnard[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < sandfish.Length; i++)
            {
                if (!sandfish[i].activeSelf)
                    continue;

                sandfish[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < blackporgy.Length; i++)
            {
                if (!blackporgy[i].activeSelf)
                    continue;

                blackporgy[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < largescaleblackfish.Length; i++)
            {
                if (!largescaleblackfish[i].activeSelf)
                    continue;

                largescaleblackfish[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < babyseabass.Length; i++)
            {
                if (!babyseabass[i].activeSelf)
                    continue;

                babyseabass[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < mackerel.Length; i++)
            {
                if (!mackerel[i].activeSelf)
                    continue;

                mackerel[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < horsemackerel.Length; i++)
            {
                if (!horsemackerel[i].activeSelf)
                    continue;

                horsemackerel[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < konosiruspunctatus.Length; i++)
            {
                if (!konosiruspunctatus[i].activeSelf)
                    continue;

                konosiruspunctatus[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < halfbeak.Length; i++)
            {
                if (!halfbeak[i].activeSelf)
                    continue;

                halfbeak[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < okhotskatkamackerel.Length; i++)
            {
                if (!okhotskatkamackerel[i].activeSelf)
                    continue;

                okhotskatkamackerel[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < bigscaledredfin.Length; i++)
            {
                if (!bigscaledredfin[i].activeSelf)
                    continue;

                bigscaledredfin[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < flatheadgreymullet.Length; i++)
            {
                if (!flatheadgreymullet[i].activeSelf)
                    continue;

                flatheadgreymullet[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < japaneseamberjack.Length; i++)
            {
                if (!japaneseamberjack[i].activeSelf)
                    continue;

                japaneseamberjack[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < japanesespanishmackerel.Length; i++)
            {
                if (!japanesespanishmackerel[i].activeSelf)
                    continue;

                japanesespanishmackerel[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < seabass.Length; i++)
            {
                if (!seabass[i].activeSelf)
                    continue;

                seabass[i].GetComponent<Fish>().SearchRange += 1;
            }
            for (int i = 0; i < spottedseabass.Length; i++)
            {
                if (!spottedseabass[i].activeSelf)
                    continue;

                spottedseabass[i].GetComponent<Fish>().SearchRange += 1;
            }
        }
    }
}