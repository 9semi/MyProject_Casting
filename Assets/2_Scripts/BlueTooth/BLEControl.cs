using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android; // 퍼미션
using UnityEngine.UI;

public class BLEControl : MonoBehaviour
{
    // 확인용 버튼식 추가
    //public GameObject BLEOnOffBtn;

    private BLETotal bleTotal;  // BLETotal.cs
    public bool isBleOn;    // 블루투스 On, Off?
    public GameObject bluetoothTable;   // 블루투스 MenuUI
    public GameObject[] bluetoothMain;  // 블루투스 MainBtn
    public GameObject[] bluetoothReel;  // 블루투스 ReelBtn
    public Text[] nameMainText; // 블루투스 Main 이름 텍스트
    public Text[] nameReelText; // 블루투스 Reel 이름 텍스트
    public Text[] addressMainText;  // 블루투스 Main 주소 텍스트
    public Text[] addressReelText;  // 블루투스 Reel 주소 텍스트
    public Sprite[] stateImg;   // 블루투스 상태 Image

    // 데이터 보내기 테스트
    public GameObject testButtons;  // Canvas -> TestButtons

    // 스캔 애니메이션
    public Animator animator;   // Scan 버튼 애니메이션
    //public GameObject reelAnim; // ReelSpin 애니메이션
    public bool reelOn = false;    // ReelSpin 애니메이션 도는 중?

    private void Update()
    {
        // 릴스핀 애니메이션 
        if (bluetoothReel[0].activeSelf && reelOn)
        {
            //reelAnim.SetActive(false);
            reelOn = false;
        }
    }

    // testButtons On, Off(버튼에 삽입안되어 있음)
    public void OnTest()
    {
        if (testButtons.activeSelf)
            testButtons.SetActive(false);
        else
            testButtons.SetActive(true);
    }
    
    #region 이전 코드 ChecksumButtonTest
    // 참조 : testButtons 자식 버튼
    public void BLDC0DC0()
    {
        bleTotal.BLDC0DC0();
    } 
    public void BLDC30DC0()
    {
        bleTotal.BLDC30DC0();
    }
    public void BLDC60DC0()
    {
        bleTotal.BLDC60DC0();
    }
    public void BLDC90DC0()
    {
        bleTotal.BLDC90DC0();
    }
    public void BLDC30DC30()
    {
        bleTotal.BLDC30DC30();
    }
    public void BLDC60DC60()
    {
        bleTotal.BLDC60DC60();
    }
    public void BLDC90DC90()
    {
        bleTotal.BLDC90DC90();
    }
    public void BLDC0DC0X()
    {
        bleTotal.BLDC0DC0X();
    }
    public void BLDC30DC0X()
    {
        bleTotal.BLDC30DC0X();
    }
    public void BLDC60DC0X()
    {
        bleTotal.BLDC60DC0X();
    }
    public void BLDC90DC0X()
    {
        bleTotal.BLDC90DC0X();
    }
    public void BLDC30DC30X()
    {
        bleTotal.BLDC30DC30X();
    }
    public void BLDC60DC60X()
    {
        bleTotal.BLDC60DC60X();
    }
    public void BLDC90DC90X()
    {
        bleTotal.BLDC90DC90X();
    }
    #endregion

    private void Start()
    {       
        isBleOn = false;
        GameObject bleManager = GameObject.FindGameObjectWithTag("Bluetooth");

        if (bleManager)
        {
            bleTotal = bleManager.GetComponent<BLETotal>();
            bleTotal.bleControl = this;
            isBleOn = true;

            bleTotal.MotorResetCheck();

            if (bleTotal.addressMain != null)   // Main address가 저장되어진 것이 있는가? -> 연결되어 있는가?
            {
                nameMainText[0].text = bleTotal.nameMain;
                addressMainText[0].text = bleTotal.addressMain;
                bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().sprite = stateImg[0];
                bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                bluetoothMain[0].SetActive(true);
            }
            if (bleTotal.addressReel != null)   // Reel address가 저장되어진 것이 있는가? -> 연결되어 있는가?
            {
                nameReelText[0].text = bleTotal.nameReel;
                addressReelText[0].text = bleTotal.addressReel;
                bluetoothReel[0].transform.GetChild(0).GetComponent<Image>().sprite = stateImg[0];
                bluetoothReel[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                bluetoothReel[0].SetActive(true);
            }
        }        
    }
    // Lobby Scene -> Canvas -> Bluetooth
    public void BluetoothButtonOnOff() // lobby의 블루투스 버튼
    {
        // BluetoothTable 꺼짐? bluetoothTable On : bluetoothTable Off

        if (!bluetoothTable.activeSelf)
            bluetoothTable.SetActive(true);
        else
        {
            if (bleTotal._scanning) // 스캔 중?
                bleTotal.OnScan();  // 스캔 정지

            //if (reelAnim.activeSelf)    // 릴스핀 애니메이션 도는중?
            //    reelAnim.SetActive(false);  // 릴스핀 비활성

            bluetoothTable.SetActive(false);
        }
       // bleTotal.OffConnectMain();
    }

    // Lobby Scene -> Canvas -> BluetoothTable -> Exit
    public void BluetoothExit()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        bluetoothTable.SetActive(false);

        if (bleTotal._scanning)
            bleTotal.OnScan();
    }
    // Lobby Scene -> Canvas -> BluetoothTable -> MainButton -> Viewport -> Content -> Main
    public void TryConnectMain(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        bleTotal.nameMain = nameMainText[index].text;
        bleTotal.addressMain = addressMainText[index].text;
        bleTotal.OnConnectMain(index);
    }
    // Lobby Scene -> Canvas -> BluetoothTable -> ReelButton -> Viewport -> Content -> Reel
    public void TryConnectReel(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        bleTotal.nameReel = nameReelText[index].text;
        bleTotal.addressReel = addressReelText[index].text;
        bleTotal.OnConnectReel(index);
    }
    // 연결완료 된 버튼 빼고 비활성
    public void ResetButton()
    {
        for (int i = 0; i < bluetoothMain.Length; i++)
        {
            bluetoothMain[i].SetActive(false);
            bluetoothReel[i].SetActive(false);
        }
        for (int i = 0; i < addressMainText.Length; i++)
        {
            if (bleTotal.addressMain == addressMainText[i].text)
            {
                nameMainText[0].text = nameMainText[i].text;
                addressMainText[0].text = addressMainText[i].text;                
                bluetoothMain[0].SetActive(true);
            }
            if (bleTotal.addressReel == addressReelText[i].text)
            {
                nameReelText[0].text = nameReelText[i].text;
                addressReelText[0].text = addressReelText[i].text;                
                bluetoothReel[0].SetActive(true);
            }
        }       
    }

    // Main 데이터들 받아서 버튼 활성
    public void ActiveMainBtn(string name, string address)
    {
        for (int i = 0; i < bluetoothMain.Length; i++)
        {
            if (!bluetoothMain[i].activeSelf)
            {
                if (!bluetoothMain[i].GetComponent<Button>().interactable)
                    bluetoothMain[i].GetComponent<Button>().interactable = true;

                bluetoothMain[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                bluetoothMain[i].SetActive(true);
                nameMainText[i].text = name;
                addressMainText[i].text = address;
                break;
            }           
        }
    }
    
    // Reel 데이터 받아서 버튼 활성
    public void ActiveReelBtn(string name, string address)
    {
        for (int i = 0; i < bluetoothReel.Length; i++)
        {
            if (!bluetoothReel[i].activeSelf)
            {
                if (!bluetoothReel[i].GetComponent<Button>().interactable)
                    bluetoothReel[i].GetComponent<Button>().interactable = true;
                bluetoothReel[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                bluetoothReel[i].SetActive(true);
                nameReelText[i].text = name;
                addressReelText[i].text = address;
                break;
            }
        }
    }

    // Lobby Scene -> Canvas -> BluetoothTable -> Scan
    /// <summary> 블루투스 오브젝트 생성 </summary>
    /// 'Bluetooth'태그 찾아서 지역변수 BLEMgr에 저장
    /// 'Bluetooth'태그가 없으면 -> Bluetooth사용에 대해서 허용을 물어봄 -> true? ->
    /// 새로운 오브젝트 생성 후 'Bluetooth'태그 붙여줌 -> BLETotal.cs 컴포넌트 추가 ->
    /// 게임실행중(게임종료전까지)에는 파괴되지 않게 설정
    /// 'Bluetooth'태그가 존재하면 -> 스캔 시작 -> 릴 정보가 하나도 없으면 -> 릴스핀 애니메이션 활성
    public void ClickBluetooth() // 스캔 버튼 
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.bluetoothScan).GetComponent<AudioPoolObject>().Init();

        GameObject BLEMgr = GameObject.FindGameObjectWithTag("Bluetooth");

        if (BLEMgr == null)
        {
            BluetoothLEHardwareInterface.Initialize(true, false, () => {

            }, (error) => {
            });

            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation)) // 사용자 위치 권한
            {
                BLEMgr = new GameObject("BLETotal");
                BLEMgr.tag = "Bluetooth";
            }

            if (bleTotal == null)
            {
                bleTotal = BLEMgr.AddComponent<BLETotal>();
                bleTotal.bleControl = this;
            }
            DontDestroyOnLoad(BLEMgr);
        }

        if (BLEMgr != null)
        {
            bleTotal.OnScan();

            //if (!bluetoothReel[0].activeSelf)
            //{
            //    reelAnim.SetActive(true);
            //    reelOn = true;
            //}
        }
    }

    public void MotorTest1()
    {
        bleTotal.Motor(10, 0);
    }
    public void MotorTest2()
    {
        bleTotal.Motor(10, 10);
    }
    public void MotorTest3()
    {
        bleTotal.Motor(10, 30);
    }

    // 릴 UI 애니메이션 끄기
    public void ReelAnimOff()
    {
        //reelAnim.SetActive(false);
        reelOn = false;
        //Debug.Log(reelAnim.activeSelf);
    }

    // BLETOTAL 넘겨주기
    public BLETotal GetBletotal()
    {
        return bleTotal;
    }
} 