using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    // 스크립트 연결
    private WorldManager worldMgr;
    public FishControl fishControl;
    public FishObjectManager fishobjMgrJeong;
    public FishObjectManagerSkyway fishobjMgrSkyway;
    public FishObjectManagerHomerspit fishobjMgrHomerspit;

    public Light light_Sun; // Directional Light의 Light
    public Material highCloud;  // 큰 구름
    public Material lowCloud;   // 작은 구름
    public Material sunCruise;  // jeongdongjin 건물들에 쓰는 매터리얼
    public Material skywayBridge;   // skyway 다리에 쓰는 매터리얼
    public Material skywayLight;    // skyway 다리 외 건물에 쓰는 매터리얼
    public Material[] homerspitMat; // homerspit 매터리얼 3종류
    public GameObject[] homerspitStreetLamp;    // homerspit 가로등불빛 -> 오브젝트 On, Off용
    public Material starField;  //  밤 배경 : 별 매터리얼
    //public PostProcessLayer postLayer;  // 각 게임씬별 포스트카메라의 레이어
    public GameObject starBackground;   // 밤 배경 : 별 오브젝트
    public GameObject yellowStar;   // 밤 배경 : 유성 오브젝트
    public GameObject blueStar;     // 밤 배경 : 유성 오브젝트
    public GameObject pinkStar;     // 밤 배경 : 유성 오브젝트
    public GameObject moon; // 밤 배경 : 달 오브젝트
    private Coroutine meteorCor;    // 밤배경 : 유성 떨어지는 코루틴
    public GameObject houseLight;       // 현 tutorial(전 jeongdongjin)맵의 등대 불빛 관련 오브젝트
    public GameObject pointLight;       // 현 tutorial(전 jeongdongjin)맵의 등대 불빛 관련 오브젝트
    private Coroutine lightHouseCor;    // 현 tutorial(전 jeongdongjin)맵의 등대 불빛 관련 코루틴
    private Coroutine bridgeLightCor;   // skyway 다리 불빛 관련 코루틴
    // Gstar용 구름(빠른거임)
    public Transform cloud1;    // Gstar영상 촬영용으로 사용함(코루틴과 묶어서 삭제해도 됨)
    public Transform cloud2;    // Gstar영상 촬영용으로 사용함(코루틴과 묶어서 삭제해도 됨)
    // 시간별로 수정
    public GameObject sun;  // Directional Light의 오브젝트
    public Material skyMat; // 시간별 하늘효과 매터리얼(Lighting에 있음)
    private int moonSelect = 10;    // 달 선택용(순서대로 총 9종류 들어가있음)
    private Coroutine backgroundCor;    // 전체 배경 코루틴
    private int worldTime = 0;  // MapSelect Scene에서 선택된 월드시간
    private bool isSet = false; // 밤하늘 On, Off 확인용

    // 인게임 UI에 시간 관련
    public Text _hourText;
    public Text _minuteText;
    public WeatherNotice _weatherNotice;

    void Start()
    {
        //worldMgr = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        worldTime = DataManager.INSTANCE._worldTime;
        //Debug.LogError("현재 시간: " + worldTime);
        BackGroundSetting();
    }

    ///<summary>시간++</summary>
    ///ScreenCanvas -> TimeUp(현재 Hierarchy정리 후에는 homerspit씬에만 있음)
    ///WeatherManager.cs의 Line(48, 49) 주석 후 
    ///worldTime = 0;
    ///삽입 후 사용하면 됨
    public void TimeUp()
    {
        if (!fishControl.isFind)
        {
            if (meteorCor != null)
                StopCoroutine(meteorCor);
            if (bridgeLightCor != null)
            {
                StopCoroutine(bridgeLightCor);
                bridgeLightCor = null;
            }
            StopCoroutine(backgroundCor);
            if (worldTime < 23)
                worldTime++;
            else
                worldTime = 0;

            BackGroundSetting();
        }
    }


    // 전체 배경 초기셋팅하는 함수
    private void BackGroundSetting()
    {
        float sunSize;              // Lighting 조절
        float sunSizeConvergence;   // Lighting 조절
        float atmosphereThickness;  // Lighting 조절

        fishControl.rareFishData.Clear();

        //Debug.Log("[" + worldTime + "]시로 바뀝니다.");

        switch (worldTime)
        {
            case 0:
                ///<summary>물고기가 잡히지 않았을 때 배경 교체</summary>
                ///물고기가 잡혔을 때 시간이 바뀌면서 배경 교체? -> 
                ///지금 잡고 있는 물고기가 그 다음시간에는 안나오는 물고기 종류 이거나 확률로 인해 나오지 않을 수 있음 ->
                ///따라서 지금 물고기를 잡고 있지 않은 상태에서만 시간과 배경 교체가 이루어져야 하며
                ///시간 및 다른 조건들은 BackGround()함수에서 처리 중
                //if (fishControl.target != null)
                    NightOn();
                ///<summary>Lighting 세팅</summary>
                ///이전시간의 세팅에서 지금시간의 세팅으로 맞춰줌
                ///일출, 일몰, 낮, 밤, 태양, 달, 별, 구름초기세팅
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(270, 0, 0);
                break;
            case 1:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(255, 0, 0);
                break;
            case 2:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(240, 0, 0);                
                break;
            case 3:
                //if (fishControl.target != null)
                        NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(225, 0, 0);
                break;
            case 4:
                //if (fishControl.target != null)
                        NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(210, 0, 0);                
                break;
            case 5:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                starBackground.SetActive(true);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                sun.transform.localEulerAngles = new Vector3(195, 0, 0);
                break;
            case 6:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                sun.transform.localEulerAngles = new Vector3(180, 0, 0);
                break;
            case 7:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(165, 0, 0);
                break;
            case 8:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(150, 0, 0);
                break;
            case 9:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(135, 0, 0);
                break;
            case 10:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(120, 0, 0);                
                break;
            case 11:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(105, 0, 0);
                break;
            case 12:
                //Debug.LogError(fishControl.target);
                //if (fishControl.target != null)
                    NightOff();

                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case 13:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(75, 0, 0);
                break;
            case 14:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(60, 0, 0);
                break;
            case 15:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(45, 0, 0);
                break;
            case 16:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.5f));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(30, 0, 0);                
                break;
            case 17:
                //if (fishControl.target != null)
                    NightOff();
                sun.transform.localEulerAngles = new Vector3(15, 0, 0);               
                break;
            case 18:
                //if (fishControl.target != null)
                    NightOff();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                starBackground.SetActive(true);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                sun.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 19:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                ///<summary>달 선택 및 초기화</summary>
                ///달 9종류 중 하나를 랜덤으로 선택 ->
                ///활성화 -> 포지션 초기화
                if (moonSelect == 10)
                    moonSelect = Random.Range(0, 9);
                moon.transform.GetChild(moonSelect).gameObject.SetActive(true);
                moon.transform.position = new Vector3(0, 0, 0);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(345, 0, 0);
                break;
            case 20:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                if (moonSelect == 10)
                    moonSelect = Random.Range(0, 9);
                moon.transform.GetChild(moonSelect).gameObject.SetActive(true);
                moon.transform.position = new Vector3(0, 90, 0);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(330, 0, 0);
                break;
            case 21:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                if (moonSelect == 10)
                    moonSelect = Random.Range(0, 9);
                moon.transform.GetChild(moonSelect).gameObject.SetActive(true);
                moon.transform.position = new Vector3(0, 180, 0);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(315, 0, 0);                
                break;
            case 22:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                if (moonSelect == 10)
                    moonSelect = Random.Range(0, 9);
                moon.transform.GetChild(moonSelect).gameObject.SetActive(false);
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(300, 0, 0);
                break;
            case 23:
                //if (fishControl.target != null)
                    NightOn();
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                skyMat.SetFloat("_SunSize", sunSize);
                skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                highCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                lowCloud.SetColor("_CloudColor", new Color(1, 1, 1, 0.1f));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, 1));
                light_Sun.color = new Color(1, 1, 1, 1);
                sun.transform.localEulerAngles = new Vector3(285, 0, 0);                
                break;            
        }
        
        // 초기설정 완료 후 코루틴 시작
        backgroundCor = StartCoroutine(BackGround());
    }

    // 전체 배경 코루틴, 실제 시간 10분 = 게임시간 1시간
    private IEnumerator BackGround()
    {
        int time = 0;
        float starLight;
        float sunSize;
        float sunSizeConvergence;
        float atmosphereThickness;
        float sunColorR, sunColorG, sunColorB;
        float cloudColorR, cloudColorG, cloudColorB, cloudAlpha;


        //WaitForSeconds delay = new WaitForSeconds(4);
        WaitForSeconds delay = PublicDefined._1secDelay;
        //WaitForSeconds delay = new WaitForSeconds(0.0125f);
        //WaitForSeconds delay = new WaitForSeconds(0.025f);
        //WaitForSeconds delay = new WaitForSeconds(0.5f);
        //Debug.Log(worldTime + "시가 되었습니다.");

        if(worldTime < 10)
        {
            _hourText.text = "0" + worldTime.ToString();
        }
        else
        {
            _hourText.text = worldTime.ToString();
        }
        
        switch (worldTime)
        {
            #region 0시 밤 셋팅
            case 0:
                // 태양 각도
                {
                    //while (time < 150)
                    //{
                    //    sun.transform.Rotate(new Vector3(-0.1f, sun.transform.position.y, sun.transform.position.z));
                    //    time++;
                    //    //Debug.Log("BackGround / time : " + time);
                    //    yield return delay;
                    //}
                    //// 조건 여부에 따라 시간++
                    //while (worldTime.Equals(0))
                    //{
                    //    if (!fishControl.isFind)
                    //    {
                    //        if (meteorCor != null)
                    //            StopCoroutine(meteorCor);

                    //        StopCoroutine(backgroundCor);
                    //        worldTime++;
                    //        BackGroundSetting();
                    //    }
                    //    yield return null;
                    //}
                }
                {
                    while (time < 599)
                    {
                        time++;

                        if (time < 100)
                        {
                            _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                        }
                        else
                        {
                            _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                        }

                        sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                        yield return delay;
                    }
                    // 조건 여부에 따라 시간++
                    while (worldTime.Equals(0))
                    {
                        if (!fishControl.isFind)
                        {
                            if (meteorCor != null)
                                StopCoroutine(meteorCor);

                            StopCoroutine(backgroundCor);
                            worldTime++;
                            //_weatherNotice.Init(worldTime);
                            BackGroundSetting();
                        }
                        yield return null;
                    }
                }
                break;
            #endregion
            #region 1시 밤 셋팅
            case 1:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                while (worldTime.Equals(1))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }

                    yield return null;
                }
                break;
            #endregion
            #region 2시 밤 셋팅
            case 2:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, sun.transform.position.y, sun.transform.position.z));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(2))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 3시 밤 셋팅
            case 3:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, sun.transform.position.y, sun.transform.position.z));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(3))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 4시 밤 셋팅
            case 4:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, sun.transform.position.y, sun.transform.position.z));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(4))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 5시 별 Off, 별 알파값 감소, 태양색 조절 구름색 조절, 구름 알파값 증가, 일출준비
            case 5:
                starLight = 1;
                sunColorG = 1; sunColorB = 1;
                cloudColorR = 1; cloudColorG = 1; cloudColorB = 1; cloudAlpha = 0.1f;
                //while (time < 150)
                //{
                //    starLight -= 0.00668f;
                //    sunColorG -= 0.0048f; sunColorB -= 0.00556f;
                //    cloudColorR -= 0.003f; cloudColorG -= 0.0052f; cloudColorB -= 0.0058f; cloudAlpha += 0.001664f;
                //    starField.SetColor("_CloudColor", new Color(1, 1, 1, starLight));
                //    highCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                //    lowCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                //    light_Sun.color = new Color(1, sunColorG, sunColorB, 1);
                //    sun.transform.Rotate(new Vector3(-0.1f, sun.transform.position.y, sun.transform.position.z));
                //    time++;
                //    yield return delay;
                //}

                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    starLight -= 0.00167f;
                    sunColorG -= 0.0012f; sunColorB -= 0.00139f;
                    cloudColorR -= 0.0008f; cloudColorG -= 0.0013f; cloudColorB -= 0.0015f; cloudAlpha += 0.000416f;
                    starField.SetColor("_CloudColor", new Color(1, 1, 1, starLight));
                    highCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    lowCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    light_Sun.color = new Color(1, sunColorG, sunColorB, 1);

                    sun.transform.Rotate(new Vector3(-0.025f, sun.transform.position.y, sun.transform.position.z));
                    yield return delay;
                }

                while (worldTime.Equals(5))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion            
            #region 6시 일출시작, 태양 색 조절, 구름 색 조절
            case 6:
                sunColorG = 0.2784f; sunColorB = 0.1647f;
                //sunColorG = 0.8f; sunColorB = 0.85f;
                cloudColorR = 0.549f; cloudColorG = 0.215f; cloudColorB = 0.125f; cloudAlpha = 0.35f;
                sunSize = 0.12f; sunSizeConvergence = 3.7f; atmosphereThickness = 2.5f;
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sunColorG += 0.0012f; sunColorB += 0.00139f;
                    sunSize -= 0.000133f; sunSizeConvergence += 0.0105f; atmosphereThickness -= 0.0025f;
                    cloudColorR += 0.00075f; cloudColorG += 0.0013f; cloudColorB += 0.00145f; cloudAlpha += 0.000246f;

                    skyMat.SetFloat("_SunSize", sunSize);
                    skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                    skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                    highCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    lowCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    light_Sun.color = new Color(1, sunColorG, sunColorB, 1);
                    sun.transform.Rotate(new Vector3(-0.025f, sun.transform.position.y, sun.transform.position.z));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(6))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 7시 낮 셋팅
            case 7:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f,0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(7))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 8시 낮 셋팅
            case 8:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(8))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 9시 낮 셋팅
            case 9:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(9))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 10시 낮 셋팅
            case 10:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(10))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 11시 낮 셋팅
            case 11:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(11))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 12시 낮 셋팅
            case 12:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(12))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 13시 낮 셋팅
            case 13:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(13))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 14시 낮 셋팅
            case 14:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(14))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 15시 낮 셋팅
            case 15:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(15))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 16시 낮 셋팅
            case 16:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(16))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 17시 일몰시작, 태양색 조절, 구름 알파값 감소
            case 17:
                sunColorG = 1; sunColorB = 1;
                cloudColorR = 1; cloudColorG = 1; cloudColorB = 1; cloudAlpha = 0.5f;
                sunSize = 0.04f; sunSizeConvergence = 10; atmosphereThickness = 1;
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sunColorG -= 0.0012f; sunColorB -= 0.00139f;
                    sunSize += 0.00013f; sunSizeConvergence -= 0.0105f; atmosphereThickness += 0.0025f;
                    cloudColorR -= 0.00075f; cloudColorG -= 0.0013f; cloudColorB -= 0.00145f; cloudAlpha -= 0.000246f;
                    skyMat.SetFloat("_SunSize", sunSize);
                    skyMat.SetFloat("_SunSizeConvergence", sunSizeConvergence);
                    skyMat.SetFloat("_AtmosphereThickness", atmosphereThickness);
                    highCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    lowCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    light_Sun.color = new Color(1, sunColorG, sunColorB, 1);
                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(17))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 18시 별 On, 별 알파값 증가, 구름 알파값 감소, 일몰감소
            case 18:
                starLight = 0;
                sunColorG = 0.2784f; sunColorB = 0.1647f;
                cloudColorR = 0.549f; cloudColorG = 0.215f; cloudColorB = 0.125f; cloudAlpha = 0.35f;
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    starLight += 0.00167f;
                    sunColorG += 0.0012f; sunColorB += 0.00139f;
                    cloudColorR += 0.0008f; cloudColorG += 0.0013f; cloudColorB += 0.0015f; cloudAlpha -= 0.000416f;
                    light_Sun.color = new Color(1, sunColorG, sunColorB, 1);
                    starField.SetColor("_CloudColor", new Color(1, 1, 1, starLight));
                    highCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    lowCloud.SetColor("_CloudColor", new Color(cloudColorR, cloudColorG, cloudColorB, cloudAlpha));
                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));

                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(18))
                {
                    if (!fishControl.isFind)
                    {
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 19시 밤배경 On, 달 On, 일몰셋팅
            case 19:
                // 태양만 돌던 낮이랑 다르게 달도 함께 셋팅 
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    moon.transform.position = new Vector3(moon.transform.position.x, moon.transform.position.y + 0.15f, moon.transform.position.z);
                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(19))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 20시 밤 셋팅
            case 20:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    moon.transform.position = new Vector3(moon.transform.position.x, moon.transform.position.y + 0.15f, moon.transform.position.z);
                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(20))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 21시 밤 셋팅
            case 21:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    moon.transform.position = new Vector3(moon.transform.position.x, moon.transform.position.y + 0.15f, moon.transform.position.z);
                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(21))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);

                        StopCoroutine(backgroundCor);

                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 22시 밤 셋팅
            case 22:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(22))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);
                        StopCoroutine(backgroundCor);
                        worldTime++;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion
            #region 23시 밤 셋팅
            case 23:
                while (time < 599)
                {
                    time++;

                    if (time < 100)
                    {
                        _minuteText.text = "0" + System.Math.Truncate(time * 0.1f).ToString();
                    }
                    else
                    {
                        _minuteText.text = System.Math.Truncate(time * 0.1f).ToString();
                    }

                    sun.transform.Rotate(new Vector3(-0.025f, 0, 0));
                    yield return delay;
                }
                // 조건 여부에 따라 시간++
                while (worldTime.Equals(23))
                {
                    if (!fishControl.isFind)
                    {
                        if (meteorCor != null)
                            StopCoroutine(meteorCor);

                        StopCoroutine(backgroundCor);
                        worldTime = 0;
                        //_weatherNotice.Init(worldTime);
                        BackGroundSetting();
                    }
                    yield return null;
                }
                break;
            #endregion           
        }
    }
    #region 밤 배경 On, Off
    private void NightOn()
    {
        isSet = true;

        // 유성 코루틴시작
        meteorCor = StartCoroutine(Meteor());

        // 별 활성화
        starBackground.SetActive(true);

        // 각 게임씬에 맞는 매터리얼과 포스트레이어, 물고기 셋팅
        //Debug.LogError(DataManager.INSTANCE._mapType);
        if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.tutorial))
        {
            sunCruise.SetColor("_EmissionColor", new Color(1, 0.7f, 0));
            //postLayer.enabled = true;
            fishobjMgrJeong.ReActiveFish(worldTime);
        }
        else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.skyway))
        {
            skywayLight.SetColor("_EmissionColor", new Color(191, 134, 0));
            //postLayer.enabled = true;
            bridgeLightCor = StartCoroutine(BridgeLight());
            fishobjMgrSkyway.ReActiveFish(worldTime);
        }
        else if(DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
        {
            homerspitMat[0].SetColor("_EmissionColor", new Color(255, 254, 90));
            homerspitMat[1].SetColor("_EmissionColor", new Color(255, 234, 8));
            homerspitMat[2].SetColor("_EmissionColor", new Color(255, 134, 32));
            for (int i = 0; i < homerspitStreetLamp.Length; i++)
                homerspitStreetLamp[i].SetActive(true);
            //postLayer.enabled = true;
            fishobjMgrHomerspit.ReActiveFish(worldTime);
        }
    }
    private void NightOff()
    {
        isSet = true;
        // 달 끄기
        if (moonSelect != 10)
            moon.transform.GetChild(moonSelect).gameObject.SetActive(false);

        starBackground.SetActive(false);


        // 각 게임씬에 맞는 매터리얼과 포스트레이어, 물고기 셋팅
        if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin) || DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.tutorial))
        {
            sunCruise.SetColor("_EmissionColor", new Color(0, 0, 0));
            //postLayer.enabled = false;
            fishobjMgrJeong.ReActiveFish(worldTime);
        }
        else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.skyway))
        {
            skywayBridge.SetColor("_EmissionColor", new Color(0, 0, 0));
            skywayLight.SetColor("_EmissionColor", new Color(0, 0, 0));
            //postLayer.enabled = false;
            fishobjMgrSkyway.ReActiveFish(worldTime);
        }
        else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
        {
            homerspitMat[0].SetColor("_EmissionColor", new Color(0, 0, 0));
            homerspitMat[1].SetColor("_EmissionColor", new Color(0, 0, 0));
            homerspitMat[2].SetColor("_EmissionColor", new Color(0, 0, 0));

            for (int i = 0; i < homerspitStreetLamp.Length; i++)
                homerspitStreetLamp[i].SetActive(false); ;

            //postLayer.enabled = false;
            fishobjMgrHomerspit.ReActiveFish(worldTime);
        }
    }
    #endregion

    #region Light 관련(기존에 쓰던 방식이라 삭제해도 됨)
    IEnumerator LightRatio()
    {
        int time = 0;
        int moonCount = 4;
        float starLight;
        while (time <= 1440)
        {
            while (time < 60)
            {
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                light_Sun.color = new Color(1, light_Sun.color.g + 0.00167f * 6, light_Sun.color.b + 0.00189f * 6, 1);
                time++;
                yield return new WaitForSeconds(10f);
            }
            while (time < 660)
            {
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                light_Sun.color = new Color(1, 1, 1, 1);
                time++;
                yield return new WaitForSeconds(0.1f);

            }
            while (time < 720)
            {
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                light_Sun.color = new Color(1, light_Sun.color.g - 0.00167f * 6, light_Sun.color.b - 0.00189f * 6, 1);
                time++;
                yield return new WaitForSeconds(0.1f);
            }
            while (time < 780)
            {
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                light_Sun.color = new Color(1, light_Sun.color.g + 0.00167f * 6, light_Sun.color.b + 0.00189f * 6, 1);
                time++;
                yield return new WaitForSeconds(0.1f);
            }
            moon.transform.position = new Vector3(0, -17, 0);
            moon.transform.GetChild(moonCount).gameObject.SetActive(true);
            meteorCor = StartCoroutine(Meteor());
            starBackground.SetActive(true);
            starLight = 0;
            if (SceneManager.GetActiveScene().name == "EastSea_Jeongdongjin")
            {
                sunCruise.SetColor("_EmissionColor", new Color(1, 0.7f, 0));
                //postLayer.enabled = true;
            }
            else if (SceneManager.GetActiveScene().name == "SkyWayFishingPier")
            {
                skywayLight.SetColor("_EmissionColor", new Color(191, 134, 0));
                //postLayer.enabled = true;
                bridgeLightCor = StartCoroutine(BridgeLight());
            }
            while (time < 1380)
            {
                if (starLight < 0.98)
                    starLight += 0.0166f;
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                starField.SetColor("_CloudColor", new Color(1, 1, 1, starLight));
                time++;
                //달 부분
                moon.transform.position = new Vector3(moon.transform.position.x, moon.transform.position.y + 0.75f, moon.transform.position.z);
                yield return new WaitForSeconds(0.1f);
            }
            moon.transform.GetChild(moonCount).gameObject.SetActive(false);
            StopCoroutine(meteorCor);
            if (SceneManager.GetActiveScene().name == "EastSea_Jeongdongjin")
            {
                sunCruise.SetColor("_EmissionColor", new Color(0, 0, 0));
                //postLayer.enabled = false;
            }
            else if (SceneManager.GetActiveScene().name == "SkyWayFishingPier")
            {
                skywayBridge.SetColor("_EmissionColor", new Color(0, 0, 0));
                skywayLight.SetColor("_EmissionColor", new Color(0, 0, 0));
                //postLayer.enabled = false;
                StopCoroutine(bridgeLightCor);
            }
            while (time < 1440)
            {
                if (starLight > 0.03)
                    starLight -= 0.03f;
                light_Sun.transform.Rotate(new Vector3(-0.25f, 0, 0));
                light_Sun.color = new Color(1, light_Sun.color.g - 0.00167f * 6, light_Sun.color.b - 0.00189f * 6, 1);
                starField.SetColor("_CloudColor", new Color(1, 1, 1, starLight));
                time++;
                yield return new WaitForSeconds(0.1f);
            }
            starBackground.SetActive(false);
            time = 0;
            moonCount++;
            if (moonCount == 9)
            {
                moonCount = 0;
            }
        }
    }
    #endregion

    #region 구름 관련(지스타용 빨리감기 - 삭제해도 됨)
    IEnumerator CloudColor()
    {
        int i = 0;
        float colorR = 0.878f, colorG = 0.404f, colorB = 0.298f, alpha = 0.5f;
        while (i <= 1440)
        {
            while (i < 60)
            {
                i++;
                colorR += 0.00102f * 2;
                colorG += 0.00497f * 2;
                colorB += 0.00585f * 2;
                highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            colorR = 1;
            colorG = 1;
            colorB = 1;
            highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
            lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
            while (i < 660)
            {
                i++;
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            while (i < 720)
            {
                i++;
                colorR -= 0.00102f * 2;
                colorG -= 0.00497f * 2;
                colorB -= 0.00585f * 2;
                highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            while (i < 780)
            {
                i++;
                colorR -= 0.0063f;
                colorG += 0.0016f;
                colorB += 0.00337f;
                alpha -= 0.005f;
                highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            colorR = 0.5f;
            colorG = 0.5f;
            colorB = 0.5f;
            highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
            lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
            while (i < 1380)
            {
                i++;
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            while (i < 1440)
            {
                i++;
                colorR += 0.0063f;
                colorG -= 0.0016f;
                colorB -= 0.00337f;
                alpha += 0.005f;
                highCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                lowCloud.SetColor("_CloudColor", new Color(colorR, colorG, colorB, alpha));
                cloud1.Rotate(0, -0.2f, 0);
                cloud2.Rotate(0, -0.15f, 0);
                yield return new WaitForSeconds(0.1f);
            }
            i = 0;
        }
    }
    #endregion

    #region 유성 관련
    IEnumerator Meteor()
    {
        WaitForSeconds delay = new WaitForSeconds(50);
        int randStar, meteorX, meteorY = 150, powerX, powerY, total = 0;
        // 시간당 15번이 최대로 설정
        while (total <= 15)
        {
            // 유성 끄고 시작
            yellowStar.SetActive(false);
            blueStar.SetActive(false);
            pinkStar.SetActive(false);
            if (!yellowStar.activeSelf && !blueStar.activeSelf && !pinkStar.activeSelf)
            {
                randStar = Random.Range(0, 4);
                meteorX = Random.Range(-100, 300);
                // 각 게임씬마다 설정이 다름
                if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.jeongdongjin))
                {
                    meteorY = 120;
                }
                else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.skyway))
                {
                    meteorY = 150;
                }
                else if (DataManager.INSTANCE._mapType.Equals(PublicDefined.eMapType.homerspit))
                {
                    meteorY = 160;
                }
                // 유성 떨어지는 속도
                powerX = Random.Range(-2200, -3000);
                powerY = Random.Range(-750, -850);
                switch (randStar)
                {
                    case 0:
                        yellowStar.SetActive(true);
                        yellowStar.transform.position = new Vector3(meteorX, meteorY, 300);
                        yellowStar.GetComponent<Rigidbody>().AddForce(powerX, powerY, 0);
                        yellowStar.GetComponent<Rigidbody>().AddTorque(0, 0, 200);
                        total++;
                        //SoundManager.instance.EffectPlay("ShootingStar");
                        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.meteor).GetComponent<AudioPoolObject>().Init();
                        break;
                    case 1:
                        blueStar.SetActive(true);
                        blueStar.transform.position = new Vector3(meteorX, meteorY, 300);
                        blueStar.GetComponent<Rigidbody>().AddForce(powerX, powerY, 0);
                        blueStar.GetComponent<Rigidbody>().AddTorque(0, 0, 200);
                        total++;
                        //SoundManager.instance.EffectPlay("ShootingStar");
                        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.meteor).GetComponent<AudioPoolObject>().Init();
                        break;
                    case 2:
                        pinkStar.SetActive(true);
                        pinkStar.transform.position = new Vector3(meteorX, meteorY, 300);
                        pinkStar.GetComponent<Rigidbody>().AddForce(powerX, powerY, 0);
                        pinkStar.GetComponent<Rigidbody>().AddTorque(0, 0, 200);
                        total++;
                        //SoundManager.instance.EffectPlay("ShootingStar");
                        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.meteor).GetComponent<AudioPoolObject>().Init();
                        break;
                    default:
                        break;
                }
            }
            yield return delay;
            meteorCor = null;
        }
    }
    #endregion

    #region 등대 빛관련(tutorial 등대)
    IEnumerator Lighthouse()
    {
        int i = 0;
        while (i < 700)
        {
            houseLight.transform.Rotate(0, 3, 0);
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    #region 다리 빛관련(skyway 다리불빛)
    IEnumerator BridgeLight()
    {
        int i = 0;
        float colorR = 0, colorG = 1, colorB = 1;
        // 시간에 모든 색깔 한번씩만
        while (i < 149)
        {
            i++;
            if (i < 25) // 0 1 1 -> 0 0 1
                colorG -= 0.04f;
            else if (i >= 25 && i < 50) // 0 0 1 -> 1 0 1
                colorR += 0.04f;
            else if (i >= 50 && i < 75) // 1 0 1 -> 1 0 0            
                colorB -= 0.04f;
            else if (i >= 75 && i < 100) // 1 0 0 -> 1 1 0
                colorG += 0.04f;
            else if (i >= 100 && i < 125) // 1 1 0 -> 0 1 0
                colorR -= 0.04f;
            else if (i >= 125 && i < 150) // 0 1 0 -> 0 1 1
                colorB += 0.04f;   
            skywayBridge.SetColor("_EmissionColor", new Color(colorR, colorG, colorB) * 1.5f);
            yield return new WaitForSeconds(4);
        }
        if (bridgeLightCor != null)
        {
            StopCoroutine(bridgeLightCor);
            bridgeLightCor = null;
        }
    }
    #endregion
}