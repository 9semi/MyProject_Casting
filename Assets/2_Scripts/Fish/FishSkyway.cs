using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSkyway : FishBase
{
    stFishData _fishData; public stFishData GetStructFishData() { return _fishData; }

    public GameManager gameMgr;
    private FishControl fishControl;    // ����� ���� ��ũ��Ʈ    
    [SerializeField] private SpawnControl spawnControl;  // ������ҿ� ������ ���� ��ũ��Ʈ

    public PublicDefined.eFishType fishType;

    private int randSpawn;  // ��� ���� ���� ��������(���������� ���) 
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
            #region ���
            case 74:
                // 42(20 ~ 66), b
                FishLenth(0.43f, 0.20f, 0.66f);
                FishWeight(1);
                /* ������ġ ���� (����, ������) 
                 * �ٴڿ��� 2 ~ 20m�� ���� */
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

            #region ��ī���ǽ�
            case 82:
                // 30(15 ~40), b
                FishLenth(0.30f, 0.15f, 0.40f);
                FishWeight(1);
                /* ������ġ ���� (������(��ȣ��)) 
                 * �ٴڿ��� 2 ~ 25m�� ���� */
                spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region ����谨��
            case 85:
                // 30(20 ~ 47),b
                FishLenth(0.30f, 0.20f, 0.47f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ, ������, ����) 
                 * �ٴڿ��� �ؼ��� �� 5m�� ���� */
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

            #region ���������
            case 78:
                // 36(20 ~ 90), b
                FishLenth(0.36f, 0.20f, 0.90f);
                FishWeight(1);
                /* ������ġ ���� (����, ������) 
                 * �ٴڿ��� �ؼ��� �� 5m�� ���� */
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

            #region �������ڹ�
            case 76:
                // 35(20 ~ 70), c
                FishLenth(0.35f, 0.20f, 0.70f);
                FishWeight(2);
                /* ������ġ ���� (�޽�����, �糪��) 
                 * �ٴڿ��� 1m�� ���� */
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

            #region ������
            case 71:
                // 50(30 ~ 79), a
                FishLenth(0.50f, 0.30f, 0.79f);
                FishWeight(0);
                /* ������ġ ���� (������Ʈ, �糪��, ����) 
                 * �ٴڿ��� 0 ~ 25m�� ���� */
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

            #region ���ӱݴ���
            case 87:
                // 60(30 ~ 100), b
                FishLenth(0.60f, 0.30f, 1.00f);
                FishWeight(1);
                /* ������ġ ���� (������, ����) 
                 * �ٴڿ��� 0 ~ 25m�� ���� */
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

            #region �뼭�緹ũ�ǽ�
            case 67:
                // 90(50 ~ 210), c
                FishLenth(0.90f, 0.50f, 2.10f);
                FishWeight(2);
                /* ������ġ ���� (������Ʈ, ����, ������) 
                 * �ٴڿ��� 2 ~ 15m�� ���� */
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

            #region ���
            case 80:
                // 105(60 ~ 130), b
                FishLenth(1.05f, 0.60f, 1.30f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 10 ~ 40m�� ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region ��ġ��ġ
            case 96:
                // 150(100 ~ 250), a
                FishLenth(1.50f, 1.00f, 2.50f);
                FishWeight(0);
                /* ������ġ ���� (�޽�����) 
                 * �ؼ��鿡�� 10 ~ 20m�� ���� */
                spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region �������ڹ�
            case 77:
                // 140(100 ~ 300), c
                FishLenth(1.40f, 1.00f, 3.00f);
                FishWeight(2);
                /* ������ġ ���� (�޽�����, �糪��) 
                 * �ٴڿ��� 5m�� ���� */
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

            #region ������
            case 84:
                // 100(60 ~ 210), b
                FishLenth(1.00f, 0.60f, 2.10f);
                FishWeight(1);
                /* ������ġ ���� (������, ����) 
                 * ������ -> �ؼ��鿡�� 5 ~ 20m, ���� -> �ٴڿ��� 20 ~ 40m�� ���� */
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

            #region ��Ӹ���
            case 92:
                // 40(20 ~ 76), b
                FishLenth(0.40f, 0.20f, 0.76f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 2 ~ 20m�� ���� */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region ������
            case 70:
                // 30(25 ~ 70), b
                FishLenth(0.30f, 0.25f, 0.70f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 5 ~ 25m�� ���� */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region ���׷���
            case 69:
                // 70(30 ~ 150), c
                FishLenth(0.70f, 0.30f, 1.50f);
                FishWeight(2);
                /* ������ġ ���� (������, ����) 
                 * �ٴڿ��� 2 ~ 20m�� ���� */
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

            #region ��̵�
            case 66:
                // 50(20 ~ 90), b
                FishLenth(0.50f, 0.20f, 0.90f);
                FishWeight(1);
                /* ������ġ ���� (������) 
                 * ��ü�� ���� */
                spawnControl.Seaweed(myTr, 0, 7, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ����
            case 86:
                // 75(30 ~ 105), b
                FishLenth(0.75f, 0.30f, 1.05f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ؼ��鿡�� 5 ~ 20m�� ���� */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ����
            case 89:
                // 17(10 ~ 40), b
                FishLenth(0.17f, 0.10f, 0.40f);
                FishWeight(1);
                /* ������ġ ����(�糪��) 
                 * �ٴڿ��� 2 ~ 10m�� ���� */
                spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion

            #region ������
            case 72:
                // 100(80 ~ 200), a
                FishLenth(1.00f, 0.80f, 2.00f);
                FishWeight(0);
                /* ������ġ ���� (�糪��, ����, ����) 
                 * ��ü�� ���� */
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

            #region �հ���
            case 81:
                // 95(60 ~ 150), a
                FishLenth(0.95f, 0.60f, 1.50f);
                FishWeight(0);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 10 ~ 30m ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region ����
            case 73:
                // 50(30 ~ 140), a
                FishLenth(0.50f, 0.30f, 1.40f);
                FishWeight(0);
                /* ������ġ ����(������Ʈ) 
                 * �ؼ��鿡�� 10 ~ 20m�� ���� */
                //spawnControl.Steepzone(myTr, 0, 25, minY, maxY, 2, _isSurface);
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region �����۾�
            case 91:
                // 40(30 ~ 100), b
                FishLenth(0.40f, 0.30f, 1.00f);
                FishWeight(1);
                /* ������ġ ����(������Ʈ) 
                 * �ٴڿ��� 0 ~ 20m�� ���� */
                spawnControl.Rock(myTr, 0, 7, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region �Ը�ġ
            case 75:
                // 50(40 ~ 150), b
                FishLenth(0.50f, 0.40f, 1.50f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ, �糪��, ����) 
                 * �ٴڿ��� 5 ~ 20m�� ���� */
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

            #region ���ð��
            case 97:
                // 38(20 ~ 100), b
                FishLenth(0.38f, 0.20f, 1.00f);
                FishWeight(1);
                /* ������ġ ���� (�޽�����, ������) 
                 * �ٴڿ��� 5 ~ 15m�� ���� */
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

            #region ������
            case 93:
                // 50(30 ~ 75), b
                FishLenth(0.50f, 0.30f, 0.75f);
                FishWeight(1);
                /* ������ġ ���� (������Ʈ) 
                 * �ؼ��鿡�� 0 ~ 8m�� ���� */
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region ����
            case 83:
                // 30(15 ~ 44), a
                FishLenth(0.30f, 0.15f, 0.44f);
                FishWeight(0);
                /* ������ġ ���� (�޽�����, ����) 
                 * �ؼ��鿡�� 2 ~ 20m�� ���� */
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

            #region �뼭���ġ
            case 65:
                // 55(40 ~ 90), a
                FishLenth(0.55f, 0.40f, 0.90f);
                FishWeight(0);
                /* ������ġ ���� (������Ʈ) 
                 * �ٴڿ��� 8m�� ���� */
                spawnControl.End(myTr, 0, 10, minY, maxY, 2, _isSurface);
                break;
            #endregion

            #region ���巳
            case 68:
                // 50(40 ~ 120), c
                FishLenth(0.50f, 0.40f, 1.20f);
                FishWeight(2);
                /* ������ġ ���� (������, ����) 
                 * �ٴڿ��� 5 ~ 20m�� ���� */
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

            #region ���������
            case 88:
                // 100(70 ~ 180), b
                FishLenth(1.00f, 0.70f, 1.80f);
                FishWeight(1);
                /* ������ġ ���� (�糪��) 
                 * �ٴڿ��� 2 ~ 15m�� ���� */
                spawnControl.Sand(myTr, 0, 8, minY, maxY, 14, _isSurface);
                break;
            #endregion

            #region �뼭��뱸
            case 64:
                // 60(40 ~ 150), b
                FishLenth(0.60f, 0.40f, 1.50f);
                FishWeight(1);
                /* ������ġ ���� (����, ����) 
                 * �ٴڿ��� 2 ~ 10m�� ���� */
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

            #region �ٳ��
            case 94:
                // 70(40 ~ 170), b
                FishLenth(0.70f, 0.40f, 1.70f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 2 ~ 15m�� ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region ���
            case 90:
                // 83(60 ~ 100), b
                FishLenth(0.83f, 0.60f, 1.00f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 2 ~ 20m�� ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region �νø�
            case 98:
                // 105(70 ~ 160), b
                FishLenth(1.05f, 0.70f, 1.60f);
                FishWeight(1);
                /* ������ġ ���� (����) 
                 * �ٴڿ��� 2 ~ 40m�� ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                break;
            #endregion

            #region ����ġ
            case 79:
                // 150(100 ~ 270), a
                FishLenth(1.50f, 1.00f, 2.70f);
                FishWeight(0);
                /* ������ġ ���� (������, �糪��, ����) 
                 * �ؼ��鿡�� 2 ~ 10m�� ���� */
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

            #region Ȳ��ġ
            case 95:
                // 300(150 ~ 450), a
                FishLenth(2.00f, 1.50f, 3.00f);
                FishWeight(0);
                /* ������ġ ���� (����) 
                 * �ؼ��鿡�� 2 ~ 10m�� ���� */
                spawnControl.Though(myTr, 0, 28, minY, maxY, 5, _isSurface);
                rareFish.transform = myTr;
                rareFish.image = myImg;
                fishControl.rareFishData.Add(rareFish);
                break;
            #endregion            
        }
        Price((int)fishType);
        /* ���õ� �����͸� ����ü�� ���� */
        //fishData = new FishData(gameObject, myTr, 100f, activityType, motorType, (float)System.Math.Round(realSize, 2), (float)System.Math.Round(weight, 2), price, fishKoreanName, info, fishType, fishDBNum);

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
                // skyway
                #region ���
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

                #region ��ī���ǽ�
                case 82:
                    spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ����谨��
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

                #region ���������
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

                #region �������ڹ�
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

                #region ������
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

                #region ���ӱݴ���
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

                #region �뼭�緹ũ�ǽ�
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

                #region ���
                case 80:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region ��ġ��ġ
                case 96:
                    spawnControl.SteepzoneMove(myTr, 0, dirX, dirY, dirZ, 26, minY, maxY, 4);
                    break;
                #endregion

                #region �������ڹ�
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

                #region ������
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

                #region ��Ӹ���
                case 92:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ������
                case 70:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ���׷���

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

                #region ��̵�
                case 66:
                    spawnControl.SeaweedMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ����
                case 86:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ����
                case 89:
                    spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region ������
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

                #region �հ���
                case 81:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region ����
                case 73:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region �����۾�
                case 91:
                    spawnControl.RockMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region �Ը�ġ
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

                #region ���ð��
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

                #region ������
                case 93:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region ����
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

                #region �뼭���ġ
                case 65:
                    spawnControl.EndMove(myTr, 0, dirX, dirY, dirZ, 12, minY, maxY, 4);
                    break;
                #endregion

                #region ���巳
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

                #region ���������
                case 88:
                    spawnControl.SandMove(myTr, 0, dirX, dirY, dirZ, 8, minY, maxY, 16);
                    break;
                #endregion

                #region �뼭��뱸
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

                #region �ٳ��
                case 94:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region ���
                case 90:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion

                #region �νø�
                case 98:
                    spawnControl.ThoughMove(myTr, 0, dirX, dirY, dirZ, 29, minY, maxY, 7);
                    break;
                #endregion                

                #region ����ġ
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

                #region Ȳ��ġ
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
                    // �ٴ��� ������ ��
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