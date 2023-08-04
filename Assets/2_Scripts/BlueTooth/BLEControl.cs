using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android; // 퍼미션
using UnityEngine.UI;

public class BLEControl : MonoBehaviour
{
    // 확인용 버튼식 추가
    //public GameObject BLEOnOffBtn;

    BLETotal _bleTotal;  // BLETotal.cs
    [SerializeField] bool _isBluetoothOn;    // 블루투스 On, Off?
    public bool IsBlueToothOn { set { _isBluetoothOn = value; } }
    [SerializeField] GameObject _bluetoothTable;   // 블루투스 MenuUI
    [SerializeField] GameObject[] _bluetoothMainArray;  // 블루투스 MainBtn
    public GameObject[] BluetoothMainArray { get { return _bluetoothMainArray; } }
    [SerializeField] GameObject[] _bluetoothReelArray;  // 블루투스 ReelBtn
    public GameObject[] BluetoothReelArray { get { return _bluetoothReelArray; } }
    [SerializeField] Text[] _mainNameTextArray; // 블루투스 Main 이름 텍스트
    public Text[] MainNameTextArray { get { return _mainNameTextArray; } }
    [SerializeField] Text[] _reelNameTextArray; // 블루투스 Reel 이름 텍스트
    public Text[] ReelNameTextArray { get { return _reelNameTextArray; } }
    [SerializeField] Text[] _mainAddressTextArray;  // 블루투스 Main 주소 텍스트
    public Text[] MainAddressTextArray { get { return _mainAddressTextArray; } }
    [SerializeField] Text[] _reelAddressTextArray;  // 블루투스 Reel 주소 텍스트
    public Text[] ReelAddressTextArray { get { return _reelAddressTextArray; } }
    [SerializeField] Sprite[] _bluetoothStateImageArray;
    public Sprite[] BluetoothStateImageArray { get { return _bluetoothStateImageArray; } }
    // 스캔 애니메이션
    [SerializeField] Animator _scanButtonAnimator; public Animator ScanButtonAnimator {  get { return _scanButtonAnimator; } }
    bool _reelOn = false; public bool ReelOn { set { _reelOn = value; } }

    private void Update()
    {
        // 릴스핀 애니메이션 
        if (_bluetoothReelArray[0].activeSelf && _reelOn)
        {
            //reelAnim.SetActive(false);
            _reelOn = false;
        }
    }

    private void Start()
    {       
        _isBluetoothOn = false;
        GameObject bleManager = GameObject.FindGameObjectWithTag("Bluetooth");

        if (bleManager)
        {
            _bleTotal = bleManager.GetComponent<BLETotal>();
            _bleTotal.SetBELControl(this);
            _isBluetoothOn = true;

            _bleTotal.MotorResetCheck();

            if (_bleTotal.AddressMain != null)   // Main address가 저장되어진 것이 있는가? -> 연결되어 있는가?
            {
                _mainNameTextArray[0].text = _bleTotal.NameMain;
                _mainAddressTextArray[0].text = _bleTotal.AddressMain;
                _bluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bluetoothStateImageArray[0];
                _bluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                _bluetoothMainArray[0].SetActive(true);
            }
            if (_bleTotal.AddressReel != null)   // Reel address가 저장되어진 것이 있는가? -> 연결되어 있는가?
            {
                _reelNameTextArray[0].text = _bleTotal.NameReel;
                _reelAddressTextArray[0].text = _bleTotal.AddressReel;
                _bluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bluetoothStateImageArray[0];
                _bluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                _bluetoothReelArray[0].SetActive(true);
            }
        }        
    }
    // Lobby Scene -> Canvas -> Bluetooth
    public void BluetoothButtonOnOff() // lobby의 블루투스 버튼
    {
        // _bluetoothTable 꺼짐? _bluetoothTable On : _bluetoothTable Off

        if (!_bluetoothTable.activeSelf)
            _bluetoothTable.SetActive(true);
        else
        {
            if (_bleTotal.Scanning) // 스캔 중?
                _bleTotal.OnScan();  // 스캔 정지

            //if (reelAnim.activeSelf)    // 릴스핀 애니메이션 도는중?
            //    reelAnim.SetActive(false);  // 릴스핀 비활성

            _bluetoothTable.SetActive(false);
        }
       // _bleTotal.OffConnectMain();
    }

    // Lobby Scene -> Canvas -> _bluetoothTable -> Exit
    public void BluetoothExit()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        _bluetoothTable.SetActive(false);

        if (_bleTotal.Scanning)
            _bleTotal.OnScan();
    }
    // Lobby Scene -> Canvas -> _bluetoothTable -> MainButton -> Viewport -> Content -> Main
    public void TryConnectMain(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _bleTotal.NameMain = _mainNameTextArray[index].text;
        _bleTotal.AddressMain = _mainAddressTextArray[index].text;
        _bleTotal.OnConnectMain(index);
    }
    // Lobby Scene -> Canvas -> _bluetoothTable -> ReelButton -> Viewport -> Content -> Reel
    public void TryConnectReel(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _bleTotal.NameReel = _reelNameTextArray[index].text;
        _bleTotal.AddressReel = _reelAddressTextArray[index].text;
        _bleTotal.OnConnectReel(index);
    }
    // 연결완료 된 버튼 빼고 비활성
    public void ResetButton()
    {
        for (int i = 0; i < _bluetoothMainArray.Length; i++)
        {
            _bluetoothMainArray[i].SetActive(false);
            _bluetoothReelArray[i].SetActive(false);
        }
        for (int i = 0; i < _mainAddressTextArray.Length; i++)
        {
            if (_bleTotal.AddressMain == _mainAddressTextArray[i].text)
            {
                _mainNameTextArray[0].text = _mainNameTextArray[i].text;
                _mainAddressTextArray[0].text = _mainAddressTextArray[i].text;                
                _bluetoothMainArray[0].SetActive(true);
            }
            if (_bleTotal.AddressReel == _reelAddressTextArray[i].text)
            {
                _reelNameTextArray[0].text = _reelNameTextArray[i].text;
                _reelAddressTextArray[0].text = _reelAddressTextArray[i].text;
                _bluetoothReelArray[0].SetActive(true);
            }
        }       
    }

    // Main 데이터들 받아서 버튼 활성
    public void ActiveMainBtn(string name, string address)
    {
        for (int i = 0; i < _bluetoothMainArray.Length; i++)
        {
            if (!_bluetoothMainArray[i].activeSelf)
            {
                if (!_bluetoothMainArray[i].GetComponent<Button>().interactable)
                    _bluetoothMainArray[i].GetComponent<Button>().interactable = true;

                _bluetoothMainArray[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                _bluetoothMainArray[i].SetActive(true);
                _mainNameTextArray[i].text = name;
                _mainAddressTextArray[i].text = address;
                break;
            }           
        }
    }
    
    // Reel 데이터 받아서 버튼 활성
    public void ActiveReelBtn(string name, string address)
    {
        for (int i = 0; i < _bluetoothReelArray.Length; i++)
        {
            if (!_bluetoothReelArray[i].activeSelf)
            {
                if (!_bluetoothReelArray[i].GetComponent<Button>().interactable)
                    _bluetoothReelArray[i].GetComponent<Button>().interactable = true;
                _bluetoothReelArray[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
                _bluetoothReelArray[i].SetActive(true);
                _reelNameTextArray[i].text = name;
                _reelAddressTextArray[i].text = address;
                break;
            }
        }
    }

    // Lobby Scene -> Canvas -> _bluetoothTable -> Scan
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

            if (_bleTotal == null)
            {
                _bleTotal = BLEMgr.AddComponent<BLETotal>();
                _bleTotal.SetBELControl(this);
            }
            DontDestroyOnLoad(BLEMgr);
        }

        if (BLEMgr != null)
        {
            _bleTotal.OnScan();

            //if (!_bluetoothReel[0].activeSelf)
            //{
            //    reelAnim.SetActive(true);
            //    _reelOn = true;
            //}
        }
    }

    public void MotorTest1()
    {
        _bleTotal.Motor(10, 0);
    }
    public void MotorTest2()
    {
        _bleTotal.Motor(10, 10);
    }
    public void MotorTest3()
    {
        _bleTotal.Motor(10, 30);
    }

    // 릴 UI 애니메이션 끄기
    public void ReelAnimOff()
    {
        //reelAnim.SetActive(false);
        _reelOn = false;
        //Debug.Log(reelAnim.activeSelf);
    }

    // BLETOTAL 넘겨주기
    public BLETotal GetBletotal()
    {
        return _bleTotal;
    }
} 