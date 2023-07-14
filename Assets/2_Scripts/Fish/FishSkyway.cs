using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSkyway : FishBase
{
    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }

    public GameManager gameMgr;
    private FishControl fishControl;    // 물고기 통합 스크립트    
    [SerializeField] private SpawnControl spawnControl;  // 스폰장소와 움직임 제어 스크립트

    public PublicDefined.eFishType fishType;

    private int randSpawn;  // 어디서 스폰 될지 랜덤변수(엑셀데이터 기반) 
    public int fishDBNum;
    public int second1;
    public int second2;
    public int chance1;
    public int chance2;
    public int activityType;
    public int motorType;
    public int price;
    private float realSize; // 실제 사이즈 변수
    private float weight;   // 무게 변수
    public float lookTargetTime;
    public float minY, maxY; // Y축 최소, 최대치 제어 변수
    public bool _isSurface;
    [HideInInspector] public float backBiteBait;
    [SerializeField] public float biteBait;
    [SerializeField] private float backSearchRange;
    [SerializeField] private float searchRange;
    private bool isFind = false; // 타겟에 대한 bool형 변수

    public string fishKoreanName;
    public string fishEnglishName;
    public string[] info;

    public GameObject mySkin;
    public Sprite myImg;
    public Sprite _mySprite;
    private Transform myTr; // 나의 트랜스폼변수   
    private Transform target;   // 내가 바라봐야 하는 타겟(바늘)

    public stFishData fishData;  // 데이테 셋팅을 위한 구조체 변수

    private Coroutine moveCor; // 움직임 코루틴
    private Coroutine lookCor; // 타겟(바늘)확인하는 코루틴 
    Coroutine _increaseSearchRangeCoroutine;
    int _increaseValue = 0;

    WaitForSeconds _lookDelay;
    public Transform _originParent;

    public float BiteBait { get { return biteBait; } set { biteBait = value; } }
    public float SearchRange { get => searchRange; set => searchRange = value; }

    private void Awake()
    {
        int d = Random.Range(1, 5);

        biteBait = fishType.Equals(PublicDefined.eFishType.Sundry) ? 10 : fishType.Equals(PublicDefined.eFishType.Normal) ? 5 : 1;

        mySkin = transform.GetChild(1).gameObject;

        //if (DataManager.INSTANCE._isMatch)
        //    searchRange = 2;
        //else
            searchRange = 0;

        backBiteBait = biteBait;
        backSearchRange = searchRange;

        _lookDelay = new WaitForSeconds(d);
    }
    void Start()
    {
        myTr = GetComponent<Transform>();
        mySkin = transform.GetChild(1).gameObject;
        spawnControl = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnControl>();
        fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void SettingSearchRange(int range)
    {
        if (_increaseSearchRangeCoroutine != null)
        {
            searchRange = range + _increaseValue;
        }
        else
        {
            searchRange = range;
        }
        backSearchRange = range;
    }
    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {

        if (moveCor != null)
        {
            StopCoroutine(moveCor);
            moveCor = null;
        }
        // 물고기 움직임
        moveCor = StartCoroutine(Moving(fishData._fishDBNumber));

        yield return PublicDefined._4secDelay;

        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
        // 바늘 찾기
        lookCor = StartCoroutine(LookTarget());
    }

    private void FishLenth(float ave, float min, float max)
    {
        int percentage = Random.Range(0, 100);
        float devL = (min - ave) / 3;
        float devR = (max - ave) / 3;

        if (percentage < 3)
            realSize = Random.Range(min, ave + (devL * 2));
        else if (percentage < 26)
            realSize = Random.Range(ave + (devL * 2), ave + devL);
        else if (percentage < 50)
            realSize = Random.Range(ave + devL, ave);
        else if (percentage < 84)
            realSize = Random.Range(ave, ave + devR);
        else if (percentage < 97)
            realSize = Random.Range(ave + devR, ave + (devR * 2));
        else
            realSize = Random.Range(ave + (devR * 2), max);
    }
    private void FishWeight(int type)
    {
        int choice = Random.Range(0, 2);
        float addRange = Random.Range(0.01f, 0.1f);
        switch(type)
        {
            case 0:
                weight = 0.000011f * (Mathf.Pow(realSize * 100, 2.9f));
                break;
            case 1:
                weight = 0.00001f * (Mathf.Pow(realSize * 100, 3));
                break;
            case 2:
                weight = 0.0000045f * (Mathf.Pow(realSize * 100, 3.2f));
                break;
            case 3:
                weight = 0.00065f * (Mathf.Pow(realSize * 100, 2));
                break;
        }
        if (choice == 0)
            weight -= weight * addRange;
        else
            weight += weight * addRange;
    }

    public void SetData(int DBNum)
    {
        stRareFish rareFish = new stRareFish();
        myTr = GetComponent<Transform>();
        mySkin = transform.GetChild(1).gameObject;

        if (spawnControl == null)
            spawnControl = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnControl>();
        if (fishControl == null)
            fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        if (gameMgr == null)
            gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _originParent = transform.parent;
        target = fishControl.target;

        biteBait = backBiteBait;
        searchRange = backSearchRange;

        if (moveCor != null)
        {
            StopCoroutine(moveCor);
            moveCor = null;
        }
        moveCor = StartCoroutine(Moving(DBNum));
        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
        lookCor = StartCoroutine(LookTarget());
        switch (DBNum)
        {
            // skyway
            #region 병어돔
            case 74:
                // 42(20 ~ 66), b
                FishLenth(0.43f, 0.20f, 0.66f);
                FishWeight(1);
                /* 스폰위치 선정 (암초, 해조류) 
                 * 바닥에서 2 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 피카소피쉬
            case 82:
                // 30(15 ~40), b
                FishLenth(0.30f, 0.15f, 0.40f);
                FishWeight(1);
                /* 스폰위치 선정 (해조류(산호초)) 
                 * 바닥에서 2 ~ 25m에 서식 */
                spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 점쏠배감펭
            case 85:
                // 30(20 ~ 47),b
                FishLenth(0.30f, 0.20f, 0.47f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 해조류, 암초) 
                 * 바닥에서 해수면 밑 5m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 10, minY, maxY - 11.5f, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 돼지물고기
            case 78:
                // 36(20 ~ 90), b
                FishLenth(0.36f, 0.20f, 0.90f);
                FishWeight(1);
                /* 스폰위치 선정 (암초, 해조류) 
                 * 바닥에서 해수면 밑 5m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 걸프가자미
            case 76:
                // 35(20 ~ 70), c
                FishLenth(0.35f, 0.20f, 0.70f);
                FishWeight(2);
                /* 스폰위치 선정 (급심지대, 사나질) 
                 * 바닥에서 1m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                }
                else
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 여을멸
            case 71:
                // 50(30 ~ 79), a
                FishLenth(0.50f, 0.30f, 0.79f);
                FishWeight(0);
                /* 스폰위치 선정 (끝포인트, 사나질, 물골) 
                 * 바닥에서 0 ~ 25m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 10, minY, maxY - 6.5f, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 속임금눈돔
            case 87:
                // 60(30 ~ 100), b
                FishLenth(0.60f, 0.30f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (해조류, 암초) 
                 * 바닥에서 0 ~ 25m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 대서양레크피쉬
            case 67:
                // 90(50 ~ 210), c
                FishLenth(0.90f, 0.50f, 2.10f);
                FishWeight(2);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에서 2 ~ 15m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion            

            #region 방어
            case 80:
                // 105(60 ~ 130), b
                FishLenth(1.05f, 0.60f, 1.30f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 바닥에서 10 ~ 40m에 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region 꼬치삼치
            case 96:
                // 150(100 ~ 250), a
                FishLenth(1.50f, 1.00f, 2.50f);
                FishWeight(0);
                /* 스폰위치 선정 (급심지대) 
                 * 해수면에서 10 ~ 20m에 서식 */
                spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 마설가자미
            case 77:
                // 140(100 ~ 300), c
                FishLenth(1.40f, 1.00f, 3.00f);
                FishWeight(2);
                /* 스폰위치 선정 (급심지대, 사나질) 
                 * 바닥에서 5m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                }
                else
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 만새기
            case 84:
                // 100(60 ~ 210), b
                FishLenth(1.00f, 0.60f, 2.10f);
                FishWeight(1);
                /* 스폰위치 선정 (해조류, 물골) 
                 * 해조류 -> 해수면에서 5 ~ 20m, 물골 -> 바닥에서 20 ~ 40m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY + 15, maxY + 20, 5, _isSurface);
                }
                break;
            #endregion

            #region 양머리돔
            case 92:
                // 40(20 ~ 76), b
                FishLenth(0.40f, 0.20f, 0.76f);
                FishWeight(1);
                /* 스폰위치 선정 (암초) 
                 * 바닥에서 2 ~ 20m에 서식 */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 감성돔
            case 70:
                // 30(25 ~ 70), b
                FishLenth(0.30f, 0.25f, 0.70f);
                FishWeight(1);
                /* 스폰위치 선정 (암초) 
                 * 바닥에서 5 ~ 25m에 서식 */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 블랙그루퍼
            case 69:
                // 70(30 ~ 150), c
                FishLenth(0.70f, 0.30f, 1.50f);
                FishWeight(2);
                /* 스폰위치 선정 (해조류, 암초) 
                 * 바닥에서 2 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 백미돔
            case 66:
                // 50(20 ~ 90), b
                FishLenth(0.50f, 0.20f, 0.90f);
                FishWeight(1);
                /* 스폰위치 선정 (해조류) 
                 * 전체에 서식 */
                spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 적돔
            case 86:
                // 75(30 ~ 105), b
                FishLenth(0.75f, 0.30f, 1.05f);
                FishWeight(1);
                /* 스폰위치 선정 (암초) 
                 * 해수면에서 5 ~ 20m에 서식 */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 옥돔
            case 89:
                // 17(10 ~ 40), b
                FishLenth(0.17f, 0.10f, 0.40f);
                FishWeight(1);
                /* 스폰위치 선정(사나질) 
                 * 바닥에서 2 ~ 10m에 서식 */
                spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 날새기
            case 72:
                // 100(80 ~ 200), a
                FishLenth(1.00f, 0.80f, 2.00f);
                FishWeight(0);
                /* 스폰위치 선정 (사나질, 암초, 물골) 
                 * 전체에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 왕고등어
            case 81:
                // 95(60 ~ 150), a
                FishLenth(0.95f, 0.60f, 1.50f);
                FishWeight(0);
                /* 스폰위치 선정 (물골) 
                 * 바닥에서 10 ~ 30m 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region 스눅
            case 73:
                // 50(30 ~ 140), a
                FishLenth(0.50f, 0.30f, 1.40f);
                FishWeight(0);
                /* 스폰위치 선정(끝포인트) 
                 * 해수면에서 10 ~ 20m에 서식 */
                //spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 갈색송어
            case 91:
                // 40(30 ~ 100), b
                FishLenth(0.40f, 0.30f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정(끝포인트) 
                 * 바닥에서 0 ~ 20m에 서식 */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 게르치
            case 75:
                // 50(40 ~ 150), b
                FishLenth(0.50f, 0.40f, 1.50f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 사나질, 암초) 
                 * 바닥에서 5 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 개꼴고기
            case 97:
                // 38(20 ~ 100), b
                FishLenth(0.38f, 0.20f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (급심지대, 해조류) 
                 * 바닥에서 5 ~ 15m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                }
                else
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 가숭어
            case 93:
                // 50(30 ~ 75), b
                FishLenth(0.50f, 0.30f, 0.75f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트) 
                 * 해수면에서 0 ~ 8m에 서식 */
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 고등어
            case 83:
                // 30(15 ~ 44), a
                FishLenth(0.30f, 0.15f, 0.44f);
                FishWeight(0);
                /* 스폰위치 선정 (급심지대, 물골) 
                 * 해수면에서 2 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 대서양삼치
            case 65:
                // 55(40 ~ 90), a
                FishLenth(0.55f, 0.40f, 0.90f);
                FishWeight(0);
                /* 스폰위치 선정 (끝포인트) 
                 * 바닥에서 8m에 서식 */
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 블랙드럼
            case 68:
                // 50(40 ~ 120), c
                FishLenth(0.50f, 0.40f, 1.20f);
                FishWeight(2);
                /* 스폰위치 선정 (해조류, 암초) 
                 * 바닥에서 5 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 노랑가오리
            case 88:
                // 100(70 ~ 180), b
                FishLenth(1.00f, 0.70f, 1.80f);
                FishWeight(1);
                /* 스폰위치 선정 (사나질) 
                 * 바닥에서 2 ~ 15m에 서식 */
                spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 대서양대구
            case 64:
                // 60(40 ~ 150), b
                FishLenth(0.60f, 0.40f, 1.50f);
                FishWeight(1);
                /* 스폰위치 선정 (암초, 물골) 
                 * 바닥에서 2 ~ 10m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 줄농어
            case 94:
                // 70(40 ~ 170), b
                FishLenth(0.70f, 0.40f, 1.70f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 바닥에서 2 ~ 15m에 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region 농어
            case 90:
                // 83(60 ~ 100), b
                FishLenth(0.83f, 0.60f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 바닥에서 2 ~ 20m에 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region 부시리
            case 98:
                // 105(70 ~ 160), b
                FishLenth(1.05f, 0.70f, 1.60f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 바닥에서 2 ~ 40m에 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region 돛새치
            case 79:
                // 150(100 ~ 270), a
                FishLenth(1.50f, 1.00f, 2.70f);
                FishWeight(0);
                /* 스폰위치 선정 (해조류, 사나질, 물골) 
                 * 해수면에서 2 ~ 10m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                }
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 황새치
            case 95:
                // 300(150 ~ 450), a
                FishLenth(2.00f, 1.50f, 3.00f);
                FishWeight(0);
                /* 스폰위치 선정 (물골) 
                 * 해수면에서 2 ~ 10m에 서식 */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion            
        }
        Price((int)fishType);
        /* 세팅된 데이터를 구조체에 삽입 */
        //fishData = new FishData(gameObject, myTr, 100f, activityType, motorType, (float)System.Math.Round(realSize, 2), (float)System.Math.Round(weight, 2), price, fishKoreanName, info, fishType, fishDBNum);

        // 전시할 때 물고기가 너무 안물리는 것을 방지하기 위한 코드이며 
        // 일반적인 빌드 파일에서는 제외해야 한다.
        if (fishControl._isConnectedToBluetooth)
        {
            if (fishType == PublicDefined.eFishType.Normal)
            {
                searchRange += 2;
                backSearchRange = searchRange;
            }
        }

        myTr.localScale = new Vector3(realSize, realSize, realSize);
    }

    public void Price(int grade)
    {
        // {7.5 x (어종무게g x 1/20)+1000}x1/10
        // {30 x(어종무게g x 1/20) + 3000}x1/10
        // {200 x (어종무게g x 1/100) + 30000}x1/10
        //switch (grade)
        //{
        //    case 0:
        //        price = (int)((7.5f * (weight * 50) + 1000) * 0.1f);
        //        break;
        //    case 1:
        //        price = (int)((30 * (weight * 50) + 3000) * 0.1f);
        //        break;
        //    case 2:
        //        price = (int)((200 * (weight * 10) + 30000) * 0.1f);
        //        break;
        //}
        {
            switch (grade)
            {
                case 0:
                    price = 200 + (int)(weight * 20);
                    break;
                case 1:
                    price = 500 + (int)(weight * 20);
                    break;
                case 2:
                    price = 3000 + (int)(weight * 20);
                    break;
                default:
                    price = 200 + (int)(weight * 20);
                    break;
            }
        }
    }
    public IEnumerator Moving(int DBNum)
    {
        float dirX;
        float dirY;
        float dirZ;
        int x = 0, y = 0, z = 0;
        while (true)
        {
            dirX = Random.Range(-0.5f, 0.5f);
            dirY = Random.Range(-0.2f, 0.2f);
            dirZ = Random.Range(-0.5f, 0.5f);
            switch (DBNum)
            {
                // skyway
                #region 병어돔
                case 74:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 피카소피쉬
                case 82:
                    spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 점쏠배감펭
                case 85:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY - 11.5f, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 돼지물고기
                case 78:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 걸프가자미
                case 76:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 여을멸
                case 71:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY - 6.5f, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 속임금눈돔
                case 87:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 대서양레크피쉬
                case 67:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 방어
                case 80:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 꼬치삼치
                case 96:
                    spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    break;
                #endregion

                #region 마설가자미
                case 77:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 만새기
                case 84:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY - 1.5f, maxY + 1f, 7);
                    }
                    break;
                #endregion

                #region 양머리돔
                case 92:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 감성돔
                case 70:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 블랙그루퍼

                case 69:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 백미돔
                case 66:
                    spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 적돔
                case 86:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 옥돔
                case 89:
                    spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 날새기
                case 72:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY + 12, 7);
                    }
                    break;
                #endregion

                #region 왕고등어
                case 81:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 스눅
                case 73:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region 갈색송어
                case 91:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 게르치
                case 75:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY - 2f, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 개꼴고기
                case 97:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 가숭어
                case 93:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region 고등어
                case 83:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY + 12, maxY + 12, 7);
                    }
                    break;
                #endregion

                #region 대서양삼치
                case 65:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region 블랙드럼
                case 68:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 노랑가오리
                case 88:
                    spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 대서양대구
                case 64:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 줄농어
                case 94:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 농어
                case 90:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 부시리
                case 98:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion                

                #region 돛새치
                case 79:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY + 12, maxY + 12, 7);
                    }
                    break;
                #endregion

                #region 황새치
                case 95:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                    #endregion
            }
            yield return _lookDelay;
        }
    }

    public IEnumerator LookTarget()
    {
        yield return new WaitUntil(() => fishControl.target != null);

        if (target == null)
            target = fishControl.target;
        

        float randChance;
        isFind = false;
        while (!isFind)
        {
            if (!fishControl.isFind && gameMgr.GetNeedleControlTransform().position.z > 5.5f)
            {
                if (Vector3.Magnitude(target.position - myTr.position) < searchRange)
                {
                    //Debug.LogError(gameObject.name);
                    randChance = Random.Range(0.0f, 100.0f);
                    // 바늘을 물었을 때
                    if (randChance <= biteBait && !fishControl.isFind && gameMgr._needleInWater && !gameMgr._baitThrowMode)
                    {
                        isFind = true;
                        fishControl.isFind = true;

                        if (DataManager.INSTANCE._vibration)
                            Vibration.Vibrate(500);

                        myTr.LookAt(target);

                        if (moveCor != null)
                        {
                            StopCoroutine(moveCor);
                            moveCor = null;
                        }
                        if (lookCor != null)
                        {
                            StopCoroutine(lookCor);
                            lookCor = null;
                        }
                        //Debug.Log("지금 " + fishKoreanName + "가 물었습니다");
                        fishControl.SetFish(gameObject, mySkin, myTr, fishData);
                        fishControl.OnBite(second1, second2, chance1, chance2);
                    }
                }
            }
            yield return _lookDelay;
        }
    }
    public void KeepDoingCoroutine()
    {
        if (moveCor == null)
        {
            moveCor = StartCoroutine(Moving(fishData._fishDBNumber));
        }
        if (lookCor == null)
        {
            lookCor = StartCoroutine(LookTarget());
        }
    }

    public void StartIncreaseSearchRangeOneMinute(int plusRate)
    {
        _increaseValue += plusRate;
        if (_increaseSearchRangeCoroutine != null)
        {
            SearchRange = backSearchRange + _increaseValue;

            StopCoroutine(_increaseSearchRangeCoroutine);
            _increaseSearchRangeCoroutine = null;

            _increaseSearchRangeCoroutine = StartCoroutine(SearchRangeIncreasedByPastebait());
        }
        else
        {
            SearchRange = backSearchRange + _increaseValue;
            _increaseSearchRangeCoroutine = StartCoroutine(SearchRangeIncreasedByPastebait());
        }
    }

    IEnumerator SearchRangeIncreasedByPastebait()
    {
        float time = 0;

        while (time < 60)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (gameMgr.petMgr.isLightOn)
        {
            if (fishDBNum.Equals(85) || fishDBNum.Equals(91))
                searchRange = backSearchRange + 1;
            else
                searchRange = backSearchRange - 1;
        }
        else
        {
            if (fishDBNum.Equals(85) || fishDBNum.Equals(91))
                searchRange = backSearchRange - 1;
            else
                searchRange = backSearchRange + 1;
        }
        StopCor();
        yield return null;
    }
    public void StopCor()
    {
        if (_increaseSearchRangeCoroutine != null)
        {
            StopCoroutine(_increaseSearchRangeCoroutine);
            _increaseSearchRangeCoroutine = null;
            _increaseValue = 0;
        }
    }
}