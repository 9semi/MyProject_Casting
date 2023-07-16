using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : FishBase
{
    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }

    SpawnControl _spawnControl;
    FishControl _fishControl;   
    GameManager _gameManager;
    NeedleControl _needle;

    [SerializeField] PublicDefined.eFishType fishType;

    int randSpawn;
    float realSize;
    float weight;
    [HideInInspector] public float backBiteBait;

    public int fishDBNum;   // 데이터 번호
    // 물고기 엑셀 시트에 따르면 총 게이지는 6초에 걸쳐 채워지고      
    // second1 ~ second2 초내에 챔질을 하면 
    // chance1 * 10% 확률로 성공하고
    // second1 ~ second2 초내에 챔질을 못하면
    // chance2 * 10% 확률로 성공한다.    Ex) second1: 2 second2: 5 -> 2초와 5초 사이에 챔질을 해야 확률이 늘어난다.
    public int second1; // bitting -> 챔질 -> 확률 처음시간
    public int second2; // bitting -> 챔질 -> 확률 마지막시간
    public int chance1; // bitting -> 챔질 -> 확률 처음시간 마지막시간 사이
    public int chance2; // bitting -> 챔질 -> 확률 그 외 시간
    public int activityType;    // 활성도
    public int motorType;   // 모터 활성도
    int price;   // 가격

    public int lookTargetTime;    // 바늘 찾는 시간
    public bool _isSurface;
    public float minY, maxY; // Y축 최소, 최대치 제어 변수


    float backSearchRange;
    public float biteBait; // bitting 확률
    [SerializeField] float searchRange;  // 바늘 찾는 범위
    bool isFind = false; // 타겟에 대한 bool형 변수

    public string fishKoreanName;   // 물고기 한글명
    public string fishEnglishName;  // 물고기 영어명
    public string[] info;   // 물고기 정보

    public GameObject mySkin;   // 물고기 그래픽(skin mesh renderer)
    public Sprite myImg;    // 레어물고기 아이콘
    public Sprite _mySprite;

    Transform target;   // 내가 바라봐야 하는 타겟(바늘)
    
    Coroutine moveCor; // 움직임 코루틴
    Coroutine lookCor; // 타겟(바늘)확인하는 코루틴 
    Coroutine _increaseSearchRangeCoroutine;
    int _increaseValue = 0;

    WaitForSeconds _lookDelay;

    public Transform _originParent;
    public float BiteBait 
    {
        get { return biteBait; }
        set { biteBait = value; } 
    }
    public float SearchRange 
    {
        get => searchRange; 
        set => searchRange = value; 
    }

    private void Awake()
    {
        int d = Random.Range(1, 5);
        biteBait = fishType.Equals(PublicDefined.eFishType.Sundry) ? 10 : fishType.Equals(PublicDefined.eFishType.Normal) ? 5 : 1;

        searchRange = 0;

        backBiteBait = biteBait;
        backSearchRange = searchRange;

        _lookDelay = new WaitForSeconds(d);
    }

    public void SettingSearchRange(int range)
    {
        if(_increaseSearchRangeCoroutine != null)
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
        moveCor = StartCoroutine(Moving(_fishData._fishDBNumber));

        yield return PublicDefined._4secDelay;

        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
        // 바늘 찾기
        lookCor = StartCoroutine(LookTarget());
    }

    // 물고기 길이 함수
    private void FishLenth(float ave, float min, float max)
    {
        // 물고기 길이 랜덤
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

    // 물고기 무게 함수
    private void FishWeight(int type)
    {
        // 0: a
        // 1: b
        // 2: c
        // 3: d
        // 물고기 타입 * 물고기 길이
        int choice = Random.Range(0, 2);
        float addRange = Random.Range(0.01f, 0.1f);
        switch (type)
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
        // 오차 +-
        if (choice == 0)
            weight -= weight * addRange;
        else
            weight += weight * addRange;
    }

    // 물고기 데이터 삽입
    public void SetData(int DBNum)
    {         
        stRareFish rareFish = new stRareFish(); // 레어 물고기만
        mySkin = transform.GetChild(1).gameObject;  // 물고기 그래픽

        // 테스트
        //mySkin.SetActive(true);

        searchRange = backSearchRange;
        biteBait = backBiteBait;

        _originParent = transform.parent;

        if (_spawnControl == null)
            _spawnControl = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnControl>();
        if (_fishControl == null)
            _fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        if (_gameManager == null)
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        target = _fishControl.target;    // 바늘(루어)

        if(!DataManager.INSTANCE._tutorialIsInProgress)
        {
            //Debug.LogError("tutorial 모드가 아닙니다.");
            if (moveCor != null)
            {
                StopCoroutine(moveCor);
                moveCor = null;
            }
            // 물고기 움직임
            moveCor = StartCoroutine(Moving(DBNum));
            if (lookCor != null)
            {
                StopCoroutine(lookCor);
                lookCor = null;
            }
            // 바늘 찾기
            lookCor = StartCoroutine(LookTarget());
        }

        switch (DBNum)
        {
            // 동해안
            #region 도루묵
            case 7:
                // 16(13 ~ 25), a
                FishLenth(0.16f, 0.13f, 0.25f);
                FishWeight(0);
                /* 스폰위치 선정 (해조류, 물골) 
                * 바닥에서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface); // 해초
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface); // 물골
                }
                break;
            #endregion

            #region 감성돔
            case 99:
                FishLenth(0.30f, 0.25f, 0.70f);
                FishWeight(1);
                //Debug.LogError(DataManager.INSTANCE._tutorialIsInProgress);
                if (DataManager.INSTANCE._tutorialIsInProgress)
                {
                    float randomX = Random.Range(-20f, 20);
                    float randomY = Random.Range(-15f, -30f);
                    float randomZ = Random.Range(50f, 70f);

                    transform.position = new Vector3(randomX, randomY, randomZ);
                }
                else
                {
                    // 30(25 ~ 70), b

                    /* 스폰위치 선정 (끝포인트, 암초, 해조류, 물골) 
                     * 바닥에서 5 ~ 15m에 서식 */
                    randSpawn = Random.Range(0, 4);
                    switch (randSpawn)
                    {
                        case 0:
                            _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                            break;
                        case 1:
                            _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                            break;
                        case 2:
                            _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                            break;
                        case 3:
                            _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                            break;
                    }
                }
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 새끼농어
            case 17:
                // 20(15 ~ 30), b
                FishLenth(0.20f, 0.15f, 0.30f);
                FishWeight(1);
                /* 스폰위치 선정 (암초, 사나질, 해조류, 물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                randSpawn = Random.Range(0, 4);
                switch(randSpawn)
                {
                    case 0:
                        _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                        break;
                    case 1:
                        _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                        break;
                    case 2:
                        _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                        break;
                    case 3:
                        _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                        break;
                }                
                break;
            #endregion

            #region 성대
            case 18:
                // 27(22 ~ 40), a
                FishLenth(0.27f, 0.22f, 0.40f);
                FishWeight(0);
                /* 스폰위치 선정 (사나질) 
                 * 바닥에 서식 */
                _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 붕장어
            case 15:
                // 50(15 ~ 90), a
                FishLenth(0.50f, 0.15f, 0.90f);
                FishWeight(0);
                /* 스폰위치 선정 (암초, 사나질) 
                 * 바닥에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 망상어
            case 8:
                // 20(15 ~ 35), b
                FishLenth(0.20f, 0.15f, 0.35f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류, 사나질) 
                * 바닥에서 3 ~ 표층에서 5m에 서식 */
                randSpawn = Random.Range(0, 4);
                switch(randSpawn)
                {
                    case 0:
                        _spawnControl.End(transform, 0, 10, minY, maxY - 0.5f, 2, _isSurface);
                        break;
                    case 1:
                        _spawnControl.Rock(transform, 0, 7, minY, maxY+6.5f, 14, _isSurface);
                        break;
                    case 2:
                        _spawnControl.Seaweed(transform, 0, 7, minY, maxY+6.5f, 14, _isSurface);
                        break;
                    case 3:
                        _spawnControl.Sand(transform, 0, 8, minY, maxY+6.5f, 14, _isSurface);
                        break;
                }         
                break;
            #endregion

            #region 넙치
            case 3:
                // 45(40 ~ 80), c
                FishLenth(0.45f, 0.40f, 0.80f);
                FishWeight(2);
                /* 스폰위치 선정 (급심지대, 사나질, 물골) 
                 * 바닥에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Steepzone(transform, 0, 25, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 벵에돔
            case 11:
                // 35(30 ~ 60), b
                FishLenth(0.35f, 0.30f, 0.60f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 암초) 
                 * 바닥에서 5 ~ 15m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 청어
            case 29:
                // 35(30 ~ 40), b
                FishLenth(0.35f, 0.30f, 0.40f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에서 2 ~ 5m에 서식*/
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 노래미
            case 4:
                // 17(12 ~ 35), b
                FishLenth(0.17f, 0.12f, 0.35f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에서 2 ~ 8m에 서식*/
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 쥐노래미
            case 26:
                // 25(20 ~ 60), b
                FishLenth(0.25f, 0.20f, 0.60f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에서 2 ~ 8m에 서식*/
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 학공치
            case 30:
                // 25(20 ~ 40), a
                FishLenth(0.25f, 0.20f, 0.40f);
                FishWeight(0);
                /* 스폰위치 선정 (암초, 물골) 
                 * 표면에서 0 ~ 3m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 전어
            case 23:
                // 20(15 ~ 31), b
                FishLenth(0.20f, 0.15f, 0.31f);
                FishWeight(1);
                /* 스폰위치 선정 (끝포인트, 해조류) 
                 * 표면에서 5 ~ 20m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 농어
            case 5:
                // 83(60 ~ 100), b
                FishLenth(0.83f, 0.60f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 점농어
            case 24:
                // 83(60 ~ 100), b
                FishLenth(0.83f, 0.60f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 고등어
            case 2:
                // 27(20 ~ 40), a
                FishLenth(0.27f, 0.20f, 0.40f);
                FishWeight(0);
                /* 스폰위치 선정 (사나질, 물골) 
                 * 표면에서 0 ~ 1m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 숭어
            case 19:
                // 50(45 ~ 80), b
                FishLenth(0.50f, 0.45f, 0.80f);
                FishWeight(1);
                /* 스폰위치 선정 (암초, 물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 참문어
            case 28:
                // 60(50 ~ 100), d
                FishLenth(0.60f, 0.50f, 1.00f);
                FishWeight(3);
                /* 스폰위치 선정 (끝포인트) 
                 * 바닥에 서식 */
                _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 임연수어
            case 21:
                // 47(27 ~ 60), b
                FishLenth(0.47f, 0.27f, 0.60f);
                FishWeight(1);
                /* 스폰위치 선정 (급심지대, 암초, 물골) 
                 * 표면에서 10 ~ 40m에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Steepzone(transform, 0, 25, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 참가자미
            case 27:
                // 25(20 ~ 50), c
                FishLenth(0.25f, 0.20f, 0.50f);
                FishWeight(2);
                /* 스폰위치 선정 (급심지대, 사나질, 물골) 
                 * 바닥에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Steepzone(transform, 0, 25, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 도다리
            case 6:
                // 35(25 ~ 80), c
                FishLenth(0.35f, 0.25f, 0.80f);
                FishWeight(2);
                /* 스폰위치 선정 (급심지대, 사나질, 물골) 
                 * 바닥에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Steepzone(transform, 0, 25, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 양태
            case 20:
                // 50(40 ~ 100), b
                FishLenth(0.50f, 0.40f, 1.00f);
                FishWeight(1);
                /* 스폰위치 선정 (급심지대, 사나질, 물골) 
                 * 바닥에 서식 */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Steepzone(transform, 0, 25, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 전갱이
            case 22:
                // 20(15 ~ 35), a
                FishLenth(0.20f, 0.15f, 0.35f);
                FishWeight(0);
                /* 스폰위치 선정 (사나질, 물골) 
                 * 표면에서 0 ~ 1m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                }
                break;
            #endregion

            #region 삼치
            case 16:
                // 80(60 ~ 100), a
                FishLenth(0.80f, 0.60f, 1.00f);
                FishWeight(0);
                /* 스폰위치 선정 (물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 볼락
            case 13:
                // 20(15 ~ 35), c
                FishLenth(0.20f, 0.15f, 0.35f);
                FishWeight(2);
                /* 스폰위치 선정 (암초, 해조류) 
                 * 표면에서 5 ~ 15m에 서식*/
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 개볼락
            case 1:
                // 22(20 ~ 35), c
                FishLenth(0.22f, 0.20f, 0.35f);
                FishWeight(2);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에 서식*/
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 조피볼락
            case 25:
                // 40(35 ~ 80), c
                FishLenth(0.40f, 0.35f, 0.80f);
                FishWeight(2);
                /* 스폰위치 선정 (끝포인트, 암초, 해조류) 
                 * 바닥에 서식*/
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 불볼락
            case 14:
                // 20(15 ~ 30), c
                FishLenth(0.20f, 0.15f, 0.30f);
                FishWeight(2);
                /* 스폰위치 선정 (암초, 해조류) 
                 * 표면에서 5 ~ 15m에 서식*/
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Rock(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                break;
            #endregion

            #region 무늬오징어
            case 9:
                // 40(30 ~ 50), d
                FishLenth(0.40f, 0.30f, 0.50f);
                FishWeight(3);
                /* 스폰위치 선정 (끝포인트) 
                 * 바닥에서 0 ~ 15m에 서식 */
                _spawnControl.End(transform, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region 방어
            case 10:
                // 105(60 ~ 130), b
                FishLenth(1.05f, 0.60f, 1.30f);
                FishWeight(1);
                /* 스폰위치 선정 (물골) 
                 * 표면에서 0 ~ 5m에 서식 */
                _spawnControl.Though(transform, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = transform;
                rareFish.image = myImg;
                _fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region 보리멸
            case 12:
                // 20(15 ~ 30), a
                FishLenth(0.20f, 0.15f, 0.30f);
                FishWeight(0);
                /* 스폰위치 선정 (사나질) 
                 * 바닥에 서식 */
                _spawnControl.Sand(transform, 0, 8, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region 황어
            case 31:
                // 30(25 ~ 40), b
                FishLenth(0.30f, 0.25f, 0.40f);
                FishWeight(1);
                /* 스폰위치 선정 (해조류, 물골) 
                 * 표면에서 2 ~ 5m에 서식 */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    _spawnControl.Seaweed(transform, 0, 7, minY, maxY, 14, _isSurface);
                }
                else
                {
                    _spawnControl.Though(transform, 0, 28, minY, maxY , 5, _isSurface);
                }
                break;
            #endregion            
        }

        Price((int)fishType);

        /* 세팅된 데이터를 구조체에 삽입 */
        _fishData = new stFishData(gameObject, transform, activityType, motorType, 
            (float)System.Math.Round(realSize, 2), (float)System.Math.Round(weight, 2), price, fishKoreanName, info, fishType, fishDBNum);

        // 게임내 물고기 크기 = 실제 사이즈
        if (DBNum.Equals(28))
            transform.localScale = new Vector3(realSize * 0.3f, realSize * 0.3f, realSize * 0.3f);
        else
            transform.localScale = new Vector3(realSize * 1.05f, realSize * 1.05f, realSize * 1.05f);
    }

    // 물고기 가격
    public void Price(int grade)
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

    // 물고기 움직이는 함수(이동)
    public IEnumerator Moving(int DBNum)
    {
        float dirX;
        float dirY;
        float dirZ;

        int x = 0, y = 0, z = 0;

        while (true)
        {
            //dirX = Random.Range(-0.5f, 0.5f);
            //dirY = Random.Range(-0.2f, 0.2f);
            //dirZ = Random.Range(-0.5f, 0.5f);

            dirX = Random.Range(-1.5f, 1.5f);
            dirY = Random.Range(-0.2f, 0.2f);
            dirZ = Random.Range(-1.5f, 1.5f);

            switch (DBNum)
            {
                #region 도루묵
                case 7:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 감성돔
                case 99:
                    switch (randSpawn)
                    {
                        case 0:
                            _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY - 1, maxY - 5, 4);
                            break;
                        case 1:
                            _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 2:
                            _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 3:
                            _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                            break;
                    }
                    break;
                #endregion

                #region 새끼농어
                case 17:
                    switch (randSpawn)
                    {
                        case 0:
                            _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 1:
                            _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 2:
                            _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 3:
                            _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                            break;
                    }
                    break;
                #endregion

                #region 성대
                case 18:
                    _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 붕장어
                case 15:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 망상어
                case 8:
                    switch (randSpawn)
                    {
                        case 0:
                            _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY - 11.5f, 4);
                            break;
                        case 1:
                            _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 2:
                            _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                        case 3:
                            _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                            break;
                    }
                    break;
                #endregion

                #region 넙치
                case 3:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SteepzoneMove(transform, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 벵에돔
                case 11:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 청어
                case 29:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 노래미
                case 4:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 쥐노래미
                case 26:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 학공치
                case 30:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion

                #region 전어
                case 23:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY-9.5f, maxY - 11.5f, 4);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 농어
                case 5:
                    _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 점농어
                case 24:
                    _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 고등어
                case 2:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion

                #region 숭어
                case 19:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0,dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion

                #region 참문어
                case 28:
                    _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region 임연수어
                case 21:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SteepzoneMove(transform, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion

                #region 참가자미
                case 27:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SteepzoneMove(transform, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 도다리
                case 6:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SteepzoneMove(transform, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 양태
                case 20:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SteepzoneMove(transform, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region 전갱이
                case 22:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion

                #region 삼치
                case 16:
                    _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);                    
                    break;
                #endregion

                #region 볼락
                case 13:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 개볼락
                case 1:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 조피볼락
                case 25:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 불볼락
                case 14:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.RockMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    break;
                #endregion

                #region 무늬오징어
                case 9:
                    _spawnControl.EndMove(transform, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region 방어
                case 10:
                    _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region 보리멸
                case 12:
                    _spawnControl.SandMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region 황어
                case 31:
                    if (randSpawn.Equals(0))
                    {
                        _spawnControl.SeaweedMove(transform, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    }
                    else
                    {
                        _spawnControl.ThoughMove(transform, 0, dirX, dirY, dirZ, 29, minY + 12f, maxY + 12f, 7);
                    }
                    break;
                #endregion                
            }
            yield return _lookDelay;
        }
    }
    
    // 바늘 찾는 함수(bitting)
    public IEnumerator LookTarget()
    {
        yield return _lookDelay;

        while (_fishControl.target == null || target == null)
        {
            //Debug.Log(1);
            yield return _lookDelay;

            if(_gameManager.needleControl != null)
            {
                _fishControl.target = _gameManager.needleControl.transform;
                target = _fishControl.target;
            }
        }

        float randChance;
        isFind = false;

        while (!isFind)
        {
            if (!_fishControl.isFind && _gameManager.GetNeedleControlTransform().position.z > 5.5f)
            {
                // 범위 = 평균크기 * 10 -> 기본 포착범위 2m(물고기 개체수가 많아 콜라이더 탐색방식은 사용안함) 

                if (Vector3.Magnitude(target.position - transform.position) < searchRange)
                {
                    randChance = Random.Range(0.0f, 100.0f);                
                    
                    // 바늘을 물었을 때
                    if (randChance <= biteBait && !_fishControl.isFind && _gameManager.NeedleInWater && !_gameManager.BaitThrowMode)
                    {
                        isFind = true;
                        _fishControl.isFind = true;

                        if(DataManager.INSTANCE._vibration)
                            Vibration.Vibrate(500); //  진동0.5f                   

                        transform.LookAt(target);

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

                        _fishControl.SetFish(gameObject,mySkin, transform, _fishData);
                        _fishControl.OnBite(second1, second2, chance1, chance2);
                        //Debug.Log("지금 " + fishData.name + "가 물었습니다.");
                    }
                }
            }
            yield return _lookDelay;
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

        while(time < 60)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (_gameManager.petMgr.isLightOn)
        {
            if (fishDBNum.Equals(9) || fishDBNum.Equals(13))
            {
                searchRange = backSearchRange + 1;
            }
            else
            {
                searchRange = backSearchRange - 1;
            }
        }
        else
        {
            if (fishDBNum.Equals(9) || fishDBNum.Equals(13))
            {
                searchRange = backSearchRange - 1;
            }
            else
            {
                searchRange = backSearchRange + 1;
            }
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