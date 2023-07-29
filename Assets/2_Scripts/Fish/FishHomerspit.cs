using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishHomerspit : FishBase
{
    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }

    public GameManager gameMgr;
    private FishControl fishControl;    // ����� ���� ��ũ��Ʈ    
    [SerializeField] private SpawnControl spawnControl;  // ������ҿ� ������ ���� ��ũ��Ʈ

    public PublicDefined.eFishType fishType;

    private int randSpawn;  // ��� ���� ���� ��������(���������� ���) 
    private int randChoice;
    public int fishDBNum;
    public int second1;
    public int second2;
    public int chance1;
    public int chance2;
    public int activityType;
    public int motorType;
    public int price;
    private float realSize; // ���� ������ ����
    private float weight;   // ���� ����
    public float lookTargetTime;
    public float minY, maxY; // Y�� �ּ�, �ִ�ġ ���� ����
    public bool _isSurface;
    [HideInInspector] public float backBiteBait;
    [SerializeField] public float biteBait;
    [SerializeField] private float backSearchRange;
    [SerializeField] private float searchRange;
    private bool isFind = false; // Ÿ�ٿ� ���� bool�� ����
    private bool isCaught = false;

    public string fishKoreanName;
    public string fishEnglishName;
    public string[] info;

    public GameObject mySkin;
    public Sprite myImg;
    public Sprite _mySprite;

    private Transform myTr; // ���� Ʈ����������   
    private Transform target;   // ���� �ٶ���� �ϴ� Ÿ��(�ٴ�)

    public stFishData fishData;  // ������ ������ ���� ����ü ����

    private Coroutine moveCor; // ������ �ڷ�ƾ
    private Coroutine lookCor; // Ÿ��(�ٴ�)Ȯ���ϴ� �ڷ�ƾ
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


        //if (DataManager.INSTANCE._isMatch)
        //    searchRange = 2f;
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
        // ����� ������
        moveCor = StartCoroutine(Moving(fishData._fishDBNumber));

        yield return PublicDefined._4secDelay;

        if (lookCor != null)
        {
            StopCoroutine(lookCor);
            lookCor = null;
        }
        // �ٴ� ã��
        lookCor = StartCoroutine(LookTarget());
    }

    private void FishLenth(float ave, float min, float max)
    {
        int percentage = Random.Range(0, 100);
        //float devL = (min - ave) / 3; 
        float devL = (ave - min) / 3; // 0.5
        float devR = (max - ave) / 3; // 0.7

        if (percentage < 3)
        {
            realSize = Random.Range(min, ave + (devL * 2));

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName+ ", 1" + "realSize = " + realSize);
        }
        else if (percentage < 26)
        {
            realSize = Random.Range(ave + (devL * 2), ave + devL);

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName + ", 2" + "realSize = " + realSize);
        }
        else if (percentage < 50)
        {
            realSize = Random.Range(ave + devL, ave);

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName + ", 3" + "realSize = " + realSize);
        }

        else if (percentage < 84)
        {
            realSize = Random.Range(ave, ave + devR);

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName + ", 4" + "realSize = " + realSize);
        }
        else if (percentage < 97)
        {
            realSize = Random.Range(ave + devR, ave + (devR * 2));

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName + ", 5" + "realSize = " + realSize);
        }
        else
        {
            realSize = Random.Range(ave + (devR * 2), max);

            //if (fishDBNum == 57)
            //    Debug.Log(fishKoreanName + ", 6" + "realSize = " + realSize);
        }
        // realSize = max;
    }
    private void FishWeight(int type)
    {
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

        // �׽�Ʈ
        //choice = 1;
        //addRange = 0.1f;

        if (choice == 0)
            weight -= weight * addRange;
        else
            weight += weight * addRange;

        //Debug.Log(weight);
    }

    public void SetData(int DBNum)
    {
        stRareFish rareFish = new stRareFish();
        myTr = GetComponent<Transform>();
        mySkin = transform.GetChild(1).gameObject;


        // �׽�Ʈ
        //mySkin.SetActive(true);

        if (spawnControl == null)
            spawnControl = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnControl>();
        if (fishControl == null)
            fishControl = GameObject.FindGameObjectWithTag("FishControl").GetComponent<FishControl>();
        if (gameMgr == null)
            gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        biteBait = backBiteBait;
        searchRange = backSearchRange;

        target = fishControl.Target;
        _originParent = transform.parent;
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
            // homerspit
            #region �⸧���ڹ�
            case 35:
                // 35(22 ~ 55), c
                FishLenth(0.35f, 0.22f, 0.55f);
                FishWeight(2);
                /* ������ġ ���� (�ϼ�, �糪��) 
                 * �ٴڿ� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                break;
            #endregion

            #region �������ڹ�
            case 54:
                // 35(25 ~ 60), c
                FishLenth(0.35f, 0.25f, 0.65f);
                FishWeight(2);
                /* ������ġ ���� (�ϼ�, �糪��) 
                 * �ٴڿ� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                break;
            #endregion

            #region ȫ��ġ
            case 37:
                // 26(15 ~ 44),b
                FishLenth(0.26f, 0.15f, 0.44f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ, �糪��) 
                 * ǥ������ 10 ~ 20m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                break;
            #endregion

            #region ��ȫ��ġ
            case 58:
                // 20(10 ~ 33), b
                FishLenth(0.20f, 0.10f, 0.33f);
                FishWeight(1);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ��� 0 ~ 15m�� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                break;
            #endregion

            #region �����ٰ��
            case 56:
                // 35(25 ~ 50), a
                FishLenth(0.35f, 0.25f, 0.50f);
                FishWeight(0);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ��� 0 ~10m�� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                break;
            #endregion

            #region ������ü���
            case 48:
                // 14(10 ~ 25), b
                FishLenth(0.14f, 0.10f, 0.25f);
                FishWeight(1);
                /* ������ġ ���� (�糪��, ������Ʈ) 
                 * �ٴڿ��� 10 ~ 20m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                else
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region ���ٰ��
            case 40:
                // 24(18 ~ 42), b
                FishLenth(0.24f, 0.18f, 0.42f);
                FishWeight(1);
                /* ������ġ ���� (�ϼ�, ������) 
                 * �ٴڿ��� 5 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Seaweed(myTr, randChoice, 5, minY, maxY, 2, _isSurface);
                }
                break;
            #endregion

            #region ȫ�صչٸ�
            case 53:
                // 29(20 ~ 50), c
                FishLenth(0.29f, 0.20f, 0.50f);
                FishWeight(2);
                /* ������ġ ���� (�ϼ�, ������) 
                 * �ٴڿ��� 5 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Seaweed(myTr, randChoice, 5, minY, maxY, 2, _isSurface);
                }
                break;
            #endregion            

            #region ȭ��ġ���ڹ�
            case 45:
                // 70(50 ~ 100), 
                FishLenth(0.70f, 0.50f, 1.00f);
                FishWeight(2);
                /* ������ġ ���� (�޽�����, �糪��) 
                 * �ٴڿ� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 8);
                    if (randChoice < 4)
                    {
                        spawnControl.Steepzone(myTr, randChoice, 14, minY, maxY, 0.5f, _isSurface);
                    }
                    else
                    {
                        spawnControl.Steepzone(myTr, randChoice, 0.5f, minY, maxY, 14, _isSurface);
                    }
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                break;
            #endregion

            #region ����ġ���ڹ�
            case 41:
                // 30(20 ~ 52), c
                FishLenth(0.30f, 0.20f, 0.52f);
                FishWeight(2);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                break;
            #endregion

            #region �幮����
            case 50:
                // 30(20 ~ 51), c
                FishLenth(0.30f, 0.20f, 0.51f);
                FishWeight(2);
                /* ������ġ ���� (������Ʈ) 
                 * ǥ������ 5 ~ 15m�� ���� */
                spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                break;
            #endregion

            #region �̸�ġ
            case 33:
                // 112(80 ~ 120), c
                FishLenth(1.12f, 0.80f, 1.20f);
                FishWeight(2);
                /* ������ġ ���� (�ϼ�, ������Ʈ) 
                 * �ٴڿ��� 0 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region �ںӵ�
            case 46:
                // 39(30 ~ 79), b
                FishLenth(0.39f, 0.30f, 0.79f);
                FishWeight(1);
                /* ������ġ ���� (�ϼ�, ������) 
                 * �ٴڿ��� 0 ~ 20m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Seaweed(myTr, randChoice, 5, minY, maxY, 2, _isSurface);
                }
                break;
            #endregion

            #region ���ݴ���
            case 59:
                // 32(25 ~ 70), b
                FishLenth(0.32f, 0.25f, 0.70f);
                FishWeight(1);
                /* ������ġ ���� (�ϼ�) 
                 * �ٴڿ��� 0 ~ 20m�� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region �δ�������
            case 34:
                // 150(100 ~ 240), b
                FishLenth(1.50f, 1.00f, 2.40f);
                FishWeight(1);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ��� 0 ~ 5m�� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region Ȳ�ٶ���
            case 63:
                // 101(70 ~ 220), b
                FishLenth(1.01f, 0.70f, 2.20f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * ǥ������ 5 ~ 20m�� ���� */
                spawnControl.Though(myTr, 0, 16, minY, maxY, 6, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ���뷡��
            case 47:
                // 45(30 ~ 152), b
                FishLenth(0.45f, 0.30f, 1.52f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * ǥ������ 5 ~ 20m�� ���� */
                spawnControl.Though(myTr, 0, 16, minY, maxY, 6, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ��ġ
            case 51:
                // 25(20 ~ 40), a
                FishLenth(0.25f, 0.20f, 0.40f);
                FishWeight(0);
                /* ������ġ ����(������Ʈ) 
                 * ǥ������ 5 ~ 10m�� ���� */
                spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                break;
            #endregion

            #region �Ը�ġ
            case 42:
                // 50(40 ~ 150), b
                FishLenth(0.50f, 0.40f, 1.50f);
                FishWeight(1);
                /* ������ġ ���� (�ϼ�, �糪��) 
                 * �ٴڿ��� 0 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                break;
            #endregion

            #region ���
            case 60:
                // 22(15 ~ 30), a
                FishLenth(0.22f, 0.15f, 0.30f);
                FishWeight(0);
                /* ������ġ ���� (������Ʈ, �ϼ�) 
                 * ǥ������ 5 ~ 20m ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                break;
            #endregion

            #region ����
            case 32:
                // 37(30 ~ 90), b
                FishLenth(0.37f, 0.30f, 0.90f);
                FishWeight(1);
                /* ������ġ ����(�޽�����, ������Ʈ) 
                 * �ٴڿ��� 5 ~ 15m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 8);
                    if (randChoice < 4)
                    {
                        spawnControl.Steepzone(myTr, randChoice, 14, minY, maxY, 0.5f, _isSurface);
                    }
                    else
                    {
                        spawnControl.Steepzone(myTr, randChoice, 0.5f, minY, maxY, 14, _isSurface);
                    }
                }
                else
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region �뱸
            case 49:
                // 60(20 ~ 100), b
                FishLenth(0.60f, 0.20f, 1.00f);
                FishWeight(1);
                /* ������ġ ����(�ϼ�, ������) 
                 * �ٴڿ��� 5 ~ 15m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Seaweed(myTr, randChoice, 5, minY, maxY, 2, _isSurface);
                }
                break;
            #endregion

            #region ���뱸
            case 55:
                // 70(60 ~ 114), b
                FishLenth(0.70f, 0.60f, 1.14f);
                FishWeight(1);
                /* ������ġ ���� (�޽�����, ����) 
                 * �ٴڿ��� 5 ~ 15m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 8);
                    if (randChoice < 4)
                    {
                        spawnControl.Steepzone(myTr, randChoice, 14, minY, maxY, 0.5f, _isSurface);
                    }
                    else
                    {
                        spawnControl.Steepzone(myTr, randChoice, 0.5f, minY, maxY, 14, _isSurface);
                    }
                }
                else
                {
                    spawnControl.Though(myTr, 0, 16, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region ���ð��ڹ�
            case 62:
                // 30(20 ~ 49), c
                FishLenth(0.30f, 0.20f, 0.49f);
                FishWeight(2);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                break;
            #endregion

            #region ����
            case 36:
                // 27(22 ~ 40), a
                FishLenth(0.27f, 0.22f, 0.40f);
                FishWeight(0);
                /* ������ġ ���� (�޽�����, ����) 
                 * �ٴڿ��� 0 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 8);
                    if (randChoice < 4)
                    {
                        spawnControl.Steepzone(myTr, randChoice, 14, minY, maxY, 0.5f, _isSurface);
                    }
                    else
                    {
                        spawnControl.Steepzone(myTr, randChoice, 0.5f, minY, maxY, 14, _isSurface);
                    }
                }
                else
                {
                    spawnControl.Though(myTr, 0, 16, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region ����
            case 39:
                // 60(40 ~ 112), b
                FishLenth(0.60f, 0.40f, 1.12f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ) 
                 * ǥ������ 0 ~ 10m�� ���� */
                spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                break;
            #endregion

            #region ���ֺ�
            case 44:
                // 40(30 ~ 70), b
                FishLenth(0.40f, 0.30f, 0.70f);
                FishWeight(1);
                /* ������ġ ���� (�ϼ�, �糪��, ������Ʈ) 
                 * ǥ������ 5 ~ 15m�� ���� */
                randSpawn = Random.Range(0, 3);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else if (randSpawn.Equals(1))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                else
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                break;
            #endregion

            #region ������췰
            case 61:
                // 40(28 ~ 90), c
                FishLenth(0.40f, 0.28f, 0.90f);
                FishWeight(2);
                /* ������ġ ���� (�ϼ�, ������) 
                 * �ٴڿ��� 5 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Rock(myTr, randChoice, 8, minY, maxY, 2, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Seaweed(myTr, randChoice, 5, minY, maxY, 2, _isSurface);
                }
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ��ħ�췰
            case 52:
                // 30(20 ~ 61), c
                FishLenth(0.30f, 0.20f, 0.61f);
                FishWeight(2);
                /* ������ġ ���� (������Ʈ, �糪��) 
                 * �ٴڿ��� 5 ~ 10m�� ���� */
                randSpawn = Random.Range(0, 2);
                if (randSpawn.Equals(0))
                {
                    spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                }
                else
                {
                    randChoice = Random.Range(0, 5);
                    spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                }
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region �ǻ��
            case 57:
                // 200(150 ~ 300), c
                FishLenth(2.00f, 1.50f, 2.70f);
                FishWeight(2);
                /* ������ġ ���� (����) 
                 * ǥ������ 0 ~ 15m�� ���� */
                spawnControl.Though(myTr, 0, 16, minY, maxY, 6, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region �������ڹ�
            case 43:
                // 140(100 ~ 300), c
                FishLenth(1.40f, 1.00f, 3.00f);
                FishWeight(1);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ� ���� */
                randChoice = Random.Range(0, 5);
                spawnControl.Sand(myTr, randChoice, 2, minY, maxY, 26, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region �տ���
            case 38:
                // 60(20 ~ 150), b
                FishLenth(0.60f, 0.20f, 1.50f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ) 
                 * ǥ������ 0 ~ 25m�� ���� */
                spawnControl.End(myTr, 0, 17, minY, maxY, 6, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion
        }
        Price((int)fishType);

        /* ���õ� �����͸� ����ü�� ���� */
        _fishData = new stFishData(gameObject, transform, activityType, motorType,
            (float)System.Math.Round(realSize, 2), (float)System.Math.Round(weight, 2), price, fishKoreanName, info, fishType, fishDBNum);

        // ������ �� ����Ⱑ �ʹ� �ȹ����� ���� �����ϱ� ���� �ڵ��̸� 
        // �Ϲ����� ���� ���Ͽ����� �����ؾ� �Ѵ�.
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
        // {7.5 x (��������g x 1/20)+1000}x1/10
        // {30 x(��������g x 1/20) + 3000}x1/10
        // {200 x (��������g x 1/100) + 30000}x1/10
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
                // homerspit
                #region �⸧���ڹ�
                case 35:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);                    
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region �������ڹ�
                case 54:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region ȫ��ġ
                case 37:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region ��ȫ��ġ
                case 58:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region �����ٰ��
                case 56:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region ������ü���
                case 48:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    else
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region ���ٰ��
                case 40:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, randChoice, dirX, dirY, dirZ, 6, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ȫ�صչٸ�
                case 53:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, randChoice, dirX, dirY, dirZ, 6, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ȭ��ġ���ڹ�
                case 45:
                    if (randSpawn.Equals(0))
                    {
                        if (randChoice < 4)
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 15, minY, maxY, 1);
                        }
                        else
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 1, minY, maxY, 15);
                        }
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region ����ġ���ڹ�
                case 41:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region �幮����
                case 50:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    break;
                #endregion

                #region �̸�ġ
                case 33:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region �ںӵ�
                case 46:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, randChoice, dirX, dirY, dirZ, 6, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ���ݴ���
                case 59:
                    spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    break;
                #endregion

                #region �δ�������
                case 34:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region Ȳ�ٶ���
                case 63:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 17, minY, maxY, 7);
                    break;
                #endregion

                #region ���뷡��
                case 47:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 17, minY, maxY, 7);
                    break;
                #endregion

                #region ��ġ
                case 51:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    break;
                #endregion

                #region �Ը�ġ
                case 42:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region ���
                case 60:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    else
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ����
                case 32:
                    if (randSpawn.Equals(0))
                    {
                        if (randChoice < 4)
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 15, minY, maxY, 1);
                        }
                        else
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 1, minY, maxY, 15);
                        }
                    }
                    else
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region �뱸
                case 49:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, randChoice, dirX, dirY, dirZ, 6, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ���뱸
                case 55:
                    if (randSpawn.Equals(0))
                    {
                        if (randChoice < 4)
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 15, minY, maxY, 1);
                        }
                        else
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 1, minY, maxY, 15);
                        }
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 17, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region ���ð��ڹ�
                case 62:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region ����
                case 36:
                    if (randSpawn.Equals(0))
                    {
                        if (randChoice < 4)
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 15, minY, maxY, 1);
                        }
                        else
                        {
                            spawnControl.SteepzoneMove(myTr, randChoice, dirX, dirY, dirZ, 1, minY, maxY, 15);
                        }
                    }
                    else
                    {
                        spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 17, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region ����
                case 39:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    break;
                #endregion

                #region ���ֺ�
                case 44:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else if (randSpawn.Equals(1))
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    else
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    break;
                #endregion

                #region ������췰
                case 61:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.RockMove(myTr, randChoice, dirX, dirY, dirZ, 9, minY, maxY, 3);
                    }
                    else
                    {
                        spawnControl.SeaweedMove(myTr, randChoice, dirX, dirY, dirZ, 6, minY, maxY, 3);
                    }
                    break;
                #endregion

                #region ��ħ�췰
                case 52:
                    if (randSpawn.Equals(0))
                    {
                        spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    }
                    else
                    {
                        spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    }
                    break;
                #endregion

                #region �ǻ��
                case 57:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 17, minY, maxY, 7);
                    break;
                #endregion

                #region �������ڹ�
                case 43:
                    spawnControl.SandMove(myTr, randChoice, dirX, dirY, dirZ, 3, minY, maxY, 27);
                    break;
                #endregion

                #region �տ���
                case 38:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 18, minY, maxY, 7);
                    break;
                #endregion
            }
            yield return _lookDelay;
        }
    }

    public IEnumerator LookTarget()
    {
        yield return new WaitUntil(() => fishControl.Target != null);

        if (target == null)
            target = fishControl.Target;

        float randChance;
        isFind = false;
        while (!isFind)
        {
            if (!fishControl.IsFind && gameMgr.GetNeedleControlTransform().position.z > 5.5f)
            {
                if (Vector3.Magnitude(target.position - myTr.position) < searchRange)
                {
                    randChance = Random.Range(0.0f, 100.0f);

                    // �ٴ��� ������ ��
                    if (randChance <= biteBait && !fishControl.IsFind && gameMgr.NeedleInWater && !gameMgr.BaitThrowMode)
                    {
                        isFind = true;
                        fishControl.IsFind = true;
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
                        //Debug.Log("���� " + fishKoreanName + "�� �������ϴ�");
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
            searchRange = backSearchRange - 1;
        }
        else
        {
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