using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ReelBlueToothData
{
	public int Xa;
	public int Ya;
	public int Za;
	public int Xg;
	public int Yg;
	public int Zg;
	public int H;
}

public class BLETotal : MonoBehaviour
{
    readonly int _scanningHash = Animator.StringToHash("Scanning");
    
    GameManager _gameManager; public void SetGameManager(GameManager instance) { _gameManager = instance; }
    InGameUIManager _ingameUIManager; public void SetIngameUIManager(InGameUIManager instance) { _ingameUIManager = instance; }
    Reeling _reeling; public void SetReeling(Reeling instance) { _reeling = instance; }
    NeedleControl _needleControl; public void SetNeedleControl(NeedleControl instance) { _needleControl = instance; }
    FishControl _fishControl; public void SetFishControl(FishControl instance) { _fishControl = instance; }
    BLEControl _bleControl; public void SetBELControl(BLEControl instance) { _bleControl = instance; }
    ReelBlueToothData __reelData;

    bool _scanning = false; public bool Scanning { get { return _scanning; } }
    bool _connectedMain;
	bool _connectingMain;
	bool _readFoundMain;
	bool _writeFoundMain;
	bool _isMain;
    bool _connectedReel;
    bool _connectingReel;
    bool _readFoundReel;
    bool _writeFoundReel;
    bool _isReel;
    bool _isAni = false;
    bool _isOn = false;
    bool _isFail = false;
    bool _isInGame_Main = false; public bool IsInGame_Main { set { _isInGame_Main = value; } }
    bool _isInGame_Reel = false; public bool IsInGame_Reel { set { _isInGame_Reel = value; } }
    int _error = 0;
    int _bldcForce;
    int _dcForce;
    int _preH = 0;
    string _connectedIDMain;
	string _nameMain; public string NameMain { get { return _nameMain; } set { _nameMain = value; } }
	string _addressMain; public string AddressMain { get { return _addressMain; } set { _addressMain = value; } }
    string _connectedIDReel;
    string _nameReel; public string NameReel { get { return _nameReel; } set { _nameReel = value; } }
    string _addressReel; public string AddressReel { get { return _addressReel; } set { _addressReel = value; } }
    string _serviceUUID = "0001";
    string _writeCharacteristicUUID = "0002";
    string _readCharacteristicUUID = "0003";
    float _subscribingTimeoutMain;
    float _subscribingTimeoutReel;
    float _count = 0;
	float _scanTimeLimit = 0;
	float _searchTimeLimit = 0;
    float _testTime = 0;
    float _reelOffDelayTime = 0;
    byte[] _first, _second;
    byte[] _checkSum;
    byte[] _reelData;
    Coroutine _aniCoroutine;
    Coroutine _connectingAniCoroutine;
    Coroutine _reelOnCoroutine;

    
    public bool ConnectedMain
	{
		get 
		{
			return _connectedMain; 
		}
		set
		{
			_connectedMain = value;

			if (_connectedMain)
			{
				_connectingMain = false;
			}
			else
			{
				_connectedIDMain = null;
			}
		}
	}

	public bool ConnectedReel
	{
		get { return _connectedReel; }
		set
		{
			_connectedReel = value;

			if (_connectedReel)
			{
				_connectingReel = false;
			}
			else
			{
				_connectedIDReel = null;
			}
		}
	}

    public bool CheckIsInGame(string DeviceAddress)
    {
        return DeviceAddress.Equals(_connectedIDMain) && _isInGame_Main;
    }

	void Start()
	{
		gameObject.tag = "Bluetooth";
		_reelData = null;
		_first = null;
		_second = null;

		__reelData = new ReelBlueToothData();
	}

	void Update()
	{
		if (_scanning)
		{
            _scanTimeLimit += Time.deltaTime;
			if (_scanTimeLimit > 10)
			{
				OnScan();
			}
		}

		if (_isMain && !ConnectedMain)
		{
			_searchTimeLimit += Time.deltaTime;
			if (_searchTimeLimit > 7)
			{
				for (int i = 0; i < _bleControl.BluetoothMainArray.Length; i++)
				{
					_bleControl.BluetoothMainArray[i].GetComponent<Button>().interactable = true;
					_bleControl.BluetoothMainArray[i].GetComponent<Button>().interactable = true;
					_bleControl.BluetoothMainArray[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
					_bleControl.BluetoothMainArray[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
				}
				_isMain = false;
			}
		}

		if (_isReel && !ConnectedReel)
		{
			_searchTimeLimit += Time.deltaTime;
			if (_searchTimeLimit > 7)
			{
				for (int i = 0; i < _bleControl.BluetoothMainArray.Length; i++)
				{
					_bleControl.BluetoothMainArray[i].GetComponent<Button>().interactable = true;
					_bleControl.BluetoothMainArray[i].GetComponent<Button>().interactable = true;
					_bleControl.BluetoothMainArray[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
					_bleControl.BluetoothMainArray[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
				}
				_isReel = false;
			}
		}

		if (_readFoundMain && _writeFoundMain)
		{
			_readFoundMain = false;
			_writeFoundMain = false;
			_subscribingTimeoutMain = 1f;
		}

		if (_readFoundReel && _writeFoundReel)
		{
			_readFoundReel = false;
			_writeFoundReel = false;
			_subscribingTimeoutReel = 1f;
		}
        
        if (_subscribingTimeoutMain > 0f)
        {
            _subscribingTimeoutMain -= Time.deltaTime;
            if (_subscribingTimeoutMain <= 0f)
            {
                _subscribingTimeoutMain = 0f;
                BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_connectedIDMain, FullUUID(_serviceUUID), FullUUID(_readCharacteristicUUID), (deviceAddress, notification) =>
                {

                }, (deviceAddress2, characteristic, data) =>
                {
                    if (CheckIsInGame(deviceAddress2))
                    {
                        byte[] bytes = data;
                        _checkSum = data;
                        if (_checkSum[2] == 1)
                        {
                            _error = 0;
                        }
                        else if (_error < 10 && _checkSum[2] == 2)
                        {
                            _error++;
                            _fishControl.FishBLDC = 0;
                            _fishControl.NormalBLDC = 0;
                            _fishControl.DcValue = 0;
                            Motor(0, 0);
                        }
                    }
                });
            }
        }
        
        if (_subscribingTimeoutReel > 0f)
		{
			_subscribingTimeoutReel -= Time.deltaTime;
			if (_subscribingTimeoutReel <= 0f)
			{
				_subscribingTimeoutReel = 0f;
				BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_connectedIDReel, FullUUID(_serviceUUID), FullUUID(_readCharacteristicUUID), (deviceAddress, notification) =>
				{

				}, (deviceAddress2, characteristic, data) =>
				{
                    {
                        if (deviceAddress2.Equals(_connectedIDReel))
                        {
                            if (_isInGame_Reel)
                            {
                                string jsonString = Encoding.Default.GetString(data);
                                __reelData = JsonConvert.DeserializeObject<ReelBlueToothData>(jsonString);
                                _gameManager.ReelData = __reelData;
                                _fishControl.SetReelData(__reelData);

                                if (!_preH.Equals(__reelData.H) && _gameManager.NeedleInWater)
                                {
                                    if ( _gameManager.NeedleInWater)
                                    {
                                        ReelOn2();
                                    }

                                    if (_count < 1 && _gameManager.NeedleInWater)
                                    {
                                        _count += 0.22f;
                                    }

                                    _first = data;

                                    if (!_isAni && _gameManager.NeedleInWater)
                                    {
                                        _aniCoroutine = StartCoroutine(AnimSpeed());
                                    }

                                    _reelOffDelayTime = 0;
                                }
                                else
                                {
                                    _reelOffDelayTime += Time.deltaTime;
                                }

                                if (_reelOffDelayTime > 0.07f)
                                {
                                    _reeling.IsReeling = false;
                                }

                                _preH = __reelData.H;
                            }
                        }
                    }
				});
			}
		}
	}

	public void ReelOn2()
    {
		_reeling.IsReeling = true;

		if (!_fishControl.IsBite)
			_reeling.OnReeling();
	}
    
	private IEnumerator ReelOn()
	{
		_reeling.IsReeling = true;

		if (!_fishControl.IsBite)
			_reeling.OnReeling();
        
		yield return PublicDefined._02secDelay;

        StopCoroutine(_reelOnCoroutine);
		_reelOnCoroutine = null;
		_reeling.IsReeling = false;
		yield return null;

		if (!_fishControl.IsBite)
			_reeling.OnReeling();
	}
    
	public void IsConnecting()
	{
		if (_nameMain != null)
		{
			_bleControl.BluetoothMainArray[0].SetActive(true);
			_bleControl.ReelNameTextArray[0].text = _nameMain;
			_bleControl.MainAddressTextArray[0].text = _addressMain;
			_bleControl.BluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[0];
			_bleControl.BluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

		}
		if (_nameReel != null)
		{
			_bleControl.BluetoothReelArray[0].SetActive(true);
			_bleControl.ReelNameTextArray[0].text = _nameReel;
			_bleControl.ReelAddressTextArray[0].text = _addressReel;
			_bleControl.BluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[0];
			_bleControl.BluetoothReelArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
		}
	}

	public void Motor(int bldc, int dc)
	{
		_bldcForce = bldc;
		_dcForce = dc;
		byte[] bytes = { 1, 1, (byte)_bldcForce, (byte)_dcForce, (byte)(2 + bldc + dc) };
		SendBytes(bytes);
	}

	public void MotorResetCheck()
    {
		if(_bldcForce > 0 || _dcForce > 0)
        {
			_bldcForce = 0;
			_dcForce = 0;
			byte[] bytes = { 1, 1, (byte)_bldcForce, (byte)_dcForce, (byte)(2 + _bldcForce + _dcForce) };
			SendBytes(bytes);
		}
    }

	private void SendBytes(byte[] data)
	{
		BluetoothLEHardwareInterface.WriteCharacteristic(_connectedIDMain, FullUUID(_serviceUUID), 
            FullUUID(_writeCharacteristicUUID), data, data.Length, true, (characteristicUUID) => {
		});
	}
    
	private void OnAnim()
	{
		if(_aniCoroutine == null)
		{
			_aniCoroutine = StartCoroutine(AnimSpeed());
		}
		else
		{
			StopCoroutine(_aniCoroutine);
			_aniCoroutine = null;
		}
	}
    
	private IEnumerator AnimSpeed()
	{
		WaitForSeconds delay = new WaitForSeconds(0.12f);

		_isAni = true;
		while (_count >= 0.2f && _gameManager.NeedleInWater)
		{
			if (_first.Equals(_second) && _count >= 0.2f)
			{
                _count -= 0.2f;
			}
			_second = _first;
			_reeling.Ani.SetFloat("Speed", _count);
			yield return delay;
			if (_first.Equals(_second) && _count >= 0.2f)
			{
                _count -= 0.2f;
			}
			_second = _first;
			_reeling.Ani.SetFloat("Speed", _count);
			yield return delay;
			if (_first.Equals(_second) && _count >= 0.2f)
			{
                _count -= 0.2f;
			}
			_second = _first;
			_reeling.Ani.SetFloat("Speed", _count);
			yield return delay;
		}
		_isAni = false;
        _count = 0;
		_reeling.Ani.SetFloat("Speed", 0);
		StopCoroutine(_aniCoroutine);
	}

	public void Initialize0()
	{
		BluetoothLEHardwareInterface.Initialize(true, false, () => {

		}, (_error) => {
		});
	}
    
	public void OnScan()
	{
		_bleControl.IsBlueToothOn = true;
		if (_scanning)
		{
            _scanTimeLimit = 0;
			_scanning = false;
			_bleControl.ScanButtonAnimator.SetBool(_scanningHash, false);
			BluetoothLEHardwareInterface.StopScan();
		}
		else
		{
			_bleControl.ResetButton();
			_bleControl.ScanButtonAnimator.SetBool(_scanningHash, true);
			BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
			{
				AddPeripheral(name, address);
			}, (address, name, rssi, advertisingInfo) =>
			{
				if (advertisingInfo != null)
					BluetoothLEHardwareInterface.Log(string.Format("Device: {0} RSSI: {1} Data Length: {2} Bytes: {3}", name, rssi, advertisingInfo.Length, BytesToString(advertisingInfo)));
			});
		    _bleControl.ReelOn = true;
			_scanning = true;
		}
	}
    
	public IEnumerator ConnectingAnim(int index)
	{
		float addColor = 0;
		bool isDown = false;
		while (_isMain && !ConnectedMain)
		{
			if (addColor < 0.9 && !isDown)
				addColor += 0.01f;
			else if (addColor >= 0.9 && !isDown)
				isDown = true;
			else if (addColor > 0.1f && isDown)
				addColor -= 0.01f;
			else if (addColor <= 0.1f && isDown)
				isDown = false;
			_bleControl.BluetoothMainArray[index].transform.GetChild(0).Rotate(0, 0, -15);
			_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(addColor, addColor, 1);
			yield return null;
		}
		while (_isReel && !ConnectedReel)
		{
			if (addColor < 0.9 && !isDown)
				addColor += 0.01f;
			else if (addColor >= 0.9 && !isDown)
				isDown = true;
			else if (addColor > 0.1f && isDown)
				addColor -= 0.01f;
			else if (addColor <= 0.1f && isDown)
				isDown = false;
			_bleControl.BluetoothReelArray[index].transform.GetChild(0).Rotate(0, 0, -15);
			_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(addColor, addColor, 1);
			yield return null;
		}
		_bleControl.BluetoothMainArray[index].transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
		_bleControl.BluetoothReelArray[index].transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
	}
	
	public void OnConnectMain(int index)
	{
		if (ConnectedMain)
		{
			DisconnectMain(Address =>
			{
				ConnectedMain = false;
				_isMain = false;
				_nameMain = null;
				_addressMain = null;
				_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[2];
				_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				
				if (!_scanning)
					OnScan();
				else
                    _scanTimeLimit = 0;
			});
		}
		else
		{
			_isMain = true;
			_searchTimeLimit = 0;
			for (int i = 0; i < _bleControl.BluetoothMainArray.Length; i++)
			{
				_bleControl.BluetoothMainArray[i].GetComponent<Button>().interactable = false;
			}
            
			_readFoundMain = false;
			_writeFoundMain = false;
            
			_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[1];
			_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 1);
			_connectingAniCoroutine = StartCoroutine(ConnectingAnim(index));

			BluetoothLEHardwareInterface.ConnectToPeripheral(_addressMain, (address) => {
			},
			(address, serviceUUID) => {
			},
			(address, serviceUUID, characteristicUUID) => {
				if (IsEqual(serviceUUID, _serviceUUID))
				{
					_connectedIDMain = address;
					ConnectedMain = true;
					_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[0];
					_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

					if (IsEqual(characteristicUUID, _readCharacteristicUUID))
					{
						_readFoundMain = true;
					}
					else if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
					{
						_writeFoundMain = true;
					}
				}
			}, (address) => {
				if (address == _addressMain)
				{
					ConnectedMain = false;
					_isMain = false;		
					_nameMain = null;		
					_addressMain = null;	
					_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[2];
					_bleControl.BluetoothMainArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				}
			});
		}
	}
    
	public void OffConnectMain()
	{
		BluetoothLEHardwareInterface.ConnectToPeripheral(_addressMain, (address) =>
		{
		},
		(address, serviceUUID) =>
		{
		},
		(address, serviceUUID, characteristicUUID) =>
		{
		}, (address) =>
		{
			if (address == _addressMain)
			{
				ConnectedMain = false;
				_isMain = false;
				_nameMain = null;
				_addressMain = null;
				_bleControl.BluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[2];
				_bleControl.BluetoothMainArray[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
			}
		});
	}
    
	public void OnConnectReel(int index)
	{
		if (ConnectedReel)
		{
			if (_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().sprite == _bleControl.BluetoothStateImageArray[0])
			{
				DisconnectReel((Address) =>
				{
					ConnectedReel = false;
					_isReel = false;
					_nameReel = null;
					_addressReel = null;
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[2];
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
					if (!_scanning)
						OnScan();
					else
					{
						_scanning = false;
						OnScan();
					}
				});
			}
		}
		else
		{
			_isReel = true;
			_searchTimeLimit = 0;
			for (int i = 0; i < _bleControl.BluetoothMainArray.Length; i++)
			{
				_bleControl.BluetoothReelArray[i].GetComponent<Button>().interactable = false;
			}
			_readFoundReel = false;
			_writeFoundReel = false;
			_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[1];
			_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 1);
			_connectingAniCoroutine = StartCoroutine(ConnectingAnim(index));

			BluetoothLEHardwareInterface.ConnectToPeripheral(_addressReel, (address) => {
			},
			(address, serviceUUID) => {
			},
			(address, serviceUUID, characteristicUUID) => {
				if (IsEqual(serviceUUID, _serviceUUID))
				{
					Debug.LogError("OnConnectReel의 else문 : " + serviceUUID);

					_connectedIDReel = address;
					ConnectedReel = true;
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[0];
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

					if (IsEqual(characteristicUUID, _readCharacteristicUUID))
					{
						_readFoundReel = true;
					}
					else if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
					{
						_writeFoundReel = true;
					}
					for (int i = 0; i < _bleControl.BluetoothReelArray.Length; i++)
					{
						_bleControl.BluetoothReelArray[i].GetComponent<Button>().interactable = true;
						if (i == index)
							_bleControl.BluetoothReelArray[i].GetComponent<Button>().interactable = true;
					}
				}
			}, (address) => {
				if (address == _addressReel)
				{
					ConnectedReel = false;
					_isReel = false;
					_nameReel = null;
					_addressReel = null;
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().sprite = _bleControl.BluetoothStateImageArray[2];
					_bleControl.BluetoothReelArray[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				}
			});
		}
		
	}

	void DisconnectMain(Action<string> action)
	{
		BluetoothLEHardwareInterface.DisconnectPeripheral(_addressMain, action);
	}

	void DisconnectReel(Action<string> action)
	{
		BluetoothLEHardwareInterface.DisconnectPeripheral(_addressReel, action);
	}
    
	bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID(uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID(uuid2);        

		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}

	string FullUUID(string uuid)
	{
		return "6E40" + uuid + "-B5A3-F393-E0A9-E50E24DCCA9E";
	}
    
	void AddPeripheral(string name, string address)
	{
		string mainString = "MM_Main";
		string reelString = "Fishing";
		if (name.Contains(mainString))
		{
			Debug.Log("메인 기기 주소와 이름  " + address + " , " + name);
			_bleControl.ActiveMainBtn(name, address);
		}
		else if (name.Contains(reelString))
        {
			{
				if (name.Contains("MMFishing"))
					return;
			}
			_bleControl.ActiveReelBtn(name, address);
		}
	}
    
	protected string BytesToString(byte[] bytes)
	{
		string result = "";

		foreach (var b in bytes)
			result += b.ToString("X2");

		return result;
	}
}
