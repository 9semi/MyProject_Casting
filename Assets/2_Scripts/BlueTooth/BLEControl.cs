using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class BLEControl : MonoBehaviour
{
    BLETotal _bleTotal;
    [SerializeField] bool _isBluetoothOn;
    public bool IsBlueToothOn { set { _isBluetoothOn = value; } }
    [SerializeField] GameObject _bluetoothTable; 
    [SerializeField] GameObject[] _bluetoothMainArray;
    public GameObject[] BluetoothMainArray { get { return _bluetoothMainArray; } }
    [SerializeField] GameObject[] _bluetoothReelArray; 
    public GameObject[] BluetoothReelArray { get { return _bluetoothReelArray; } }
    [SerializeField] Text[] _mainNameTextArray;
    public Text[] MainNameTextArray { get { return _mainNameTextArray; } }
    [SerializeField] Text[] _reelNameTextArray;
    public Text[] ReelNameTextArray { get { return _reelNameTextArray; } }
    [SerializeField] Text[] _mainAddressTextArray;
    public Text[] MainAddressTextArray { get { return _mainAddressTextArray; } }
    [SerializeField] Text[] _reelAddressTextArray; 
    public Text[] ReelAddressTextArray { get { return _reelAddressTextArray; } }
    [SerializeField] Sprite[] _bluetoothStateImageArray;
    public Sprite[] BluetoothStateImageArray { get { return _bluetoothStateImageArray; } }
    [SerializeField] Animator _scanButtonAnimator; public Animator ScanButtonAnimator {  get { return _scanButtonAnimator; } }
    bool _reelOn = false; public bool ReelOn { set { _reelOn = value; } }

    private void Update()
    {
        if (_bluetoothReelArray[0].activeSelf && _reelOn)
        {
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

            if (_bleTotal.AddressMain != null) 
            {
                _mainNameTextArray[0].text = _bleTotal.NameMain;
                _mainAddressTextArray[0].text = _bleTotal.AddressMain;
                _bluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bluetoothStateImageArray[0];
                _bluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                _bluetoothMainArray[0].SetActive(true);
            }
            if (_bleTotal.AddressReel != null)  
            {
                _reelNameTextArray[0].text = _bleTotal.NameReel;
                _reelAddressTextArray[0].text = _bleTotal.AddressReel;
                _bluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bluetoothStateImageArray[0];
                _bluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
                _bluetoothReelArray[0].SetActive(true);
            }
        }        
    }

    public void BluetoothButtonOnOff() 
    {
        if (!_bluetoothTable.activeSelf)
            _bluetoothTable.SetActive(true);
        else
        {
            if (_bleTotal.Scanning)
                _bleTotal.OnScan();

            _bluetoothTable.SetActive(false);
        }
    }
    
    public void BluetoothExit()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.exit).GetComponent<AudioPoolObject>().Init();
        _bluetoothTable.SetActive(false);

        if (_bleTotal.Scanning)
            _bleTotal.OnScan();
    }

    public void TryConnectMain(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _bleTotal.NameMain = _mainNameTextArray[index].text;
        _bleTotal.AddressMain = _mainAddressTextArray[index].text;
        _bleTotal.OnConnectMain(index);
    }

    public void TryConnectReel(int index)
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        _bleTotal.NameReel = _reelNameTextArray[index].text;
        _bleTotal.AddressReel = _reelAddressTextArray[index].text;
        _bleTotal.OnConnectReel(index);
    }

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

    public void ClickBluetooth()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.bluetoothScan).GetComponent<AudioPoolObject>().Init();

        GameObject BLEMgr = GameObject.FindGameObjectWithTag("Bluetooth");

        if (BLEMgr == null)
        {
            BluetoothLEHardwareInterface.Initialize(true, false, () => {

            }, (error) => {
            });

            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
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
    
    public void ReelAnimOff()
    {
        _reelOn = false;
    }
    
    public BLETotal GetBletotal()
    {
        return _bleTotal;
    }
} 