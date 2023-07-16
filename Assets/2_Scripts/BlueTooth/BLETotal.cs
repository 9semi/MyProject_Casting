using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

// PC플랫폼 변경시 확인용 로그들 참조
public class BLETotal : MonoBehaviour
{
    readonly int _scanningHash = Animator.StringToHash("Scanning");

    // 스크립트 연결
    public GameManager gameMgr;
	public InGameUIManager inGameUIMgr;	
	public Reeling reeling;
	public NeedleControl needleControl;
	public FishControl fishControl;
	public BLEControl bleControl;

	public bool _scanning = false;
	//private Dictionary<string, CentralPeripheralButtonScript> _peripheralList;

	// Main장비 변수
	public bool _connectedMain = false;	// Main 연결?
	public bool _connectingMain = false;	// Main 연결중?
	private bool _readFoundMain = false;	// Main 읽기? 
	private bool _writeFoundMain = false;	// Main 쓰기?
	public bool isMain = false; // Main 연결?(내가 사용할 용도로 집어넣음)
	private string _connectedIDMain = null;	// Main address 판별용
	public string nameMain = null;	//	Main 이름 저장
	public string addressMain = null;	// Main 주소 저장
	private float _subscribingTimeoutMain = 0f;	// Main 송, 수신 통신시간

	// Reel장비 변수
	public bool _connectedReel = false;	// Reel 연결?
	public bool _connectingReel = false;	// Reel 연결중?
	private bool _readFoundReel = false;	// Reel 읽기?
	private bool _writeFoundReel = false;	// Reel 쓰기?
	public bool isReel = false; // Reel 연결?(내가 사용할 용도로 집어넣음)
	private string _connectedIDReel = null;	// address 판별용
	public string nameReel = null;	// Reel 이름 저장
	public string addressReel = null;	// Reel 주소 저장
	private float _subscribingTimeoutReel = 0f; // Reel 송, 수신 통신시간

	/// <summary>
	/// UUID(Universally Unique Identifiers) 128비트의 숫자조합
	/// </summary>
	/// 0000 XXXX - 0000 - 1000 - 8000 - 000000000000
	/// XXXX에 예약된 UUID와 충돌하지 않는 숫자나 맞는 특성으로 생성하여 삽입
	/// 개발자가 설정해서 준 값, 수정없이 사용 중
	private string _serviceUUID = "0001";
	private string _writeCharacteristicUUID = "0002";
	private string _readCharacteristicUUID = "0003";

	// 릴 애니메이션용 변수
	private float count = 0;	// reel UI 애니메이션 속도
	private byte[] first, second;	// 첫 데이터, 두번째 데이터
	private Coroutine animCor;	// reel 애니메이션 코루틴
	private bool isAnim = false;	// reel 애니메이션 도는중?

	public bool isOn = false;	// 지금 사용 X
	public bool isFail = false;	// 지금 사용 X
	public float time = 0;	// Scan용 시간
	public float time1 = 0;	// 연결용 시간
	private int error = 0;	// CheckSum error 확인
	private byte[] checkSum;	// CheckSum 변수
	private Coroutine connectingAnimCor;	// 연결중 UI 코루틴

	// 릴 down, up
	private Coroutine _reelOnCoroutine;	// reel 코루틴 저장용
	private byte[] reelData;    // 수신된 reelData 저장용

	// lobbyscene, 맵선택씬, 수족관씬 등등 게임이 진행 중이지 않을 때 bldc, dc가 0이 아니라면 블루투스가 작동 중이라는 의미이다.
	int _bldcForce; // 당기는 힘
	int _dcForce; // 흔드는 힘

	float _testTime = 0;

	// 자이로 블루투스 
	public ReelBlueToothData _reelData;
	int _preH = 0;
	float _reelOffDelayTime = 0;

	public bool _isInGame_Main = false;
	public bool _isInGame_Reel = false;

	// Main 장비 연결? _connetedMain(true) : _connectedMain(false)
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
	// Reel 장비 연결? _connetedReel(true) : _connectedReel(false)
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
		reelData = null;
		first = null;
		second = null;

		_reelData = new ReelBlueToothData();
	}

	void Update()
	{
		if (_scanning)
		{
			time += Time.deltaTime;
			if (time > 10)
			{
				OnScan();
			}
		}
		// Main 연결 실패시
		if (isMain && !ConnectedMain)
		{
			time1 += Time.deltaTime;
			if (time1 > 7)
			{
				for (int i = 0; i < bleControl.bluetoothMain.Length; i++)
				{
					bleControl.bluetoothMain[i].GetComponent<Button>().interactable = true;
					bleControl.bluetoothReel[i].GetComponent<Button>().interactable = true;
					bleControl.bluetoothMain[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
					bleControl.bluetoothMain[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
				}
				isMain = false;
			}
		}
		// Reel 연결 실패시
		if (isReel && !ConnectedReel)
		{
			time1 += Time.deltaTime;
			if (time1 > 7)
			{
				for (int i = 0; i < bleControl.bluetoothMain.Length; i++)
				{
					bleControl.bluetoothMain[i].GetComponent<Button>().interactable = true;
					bleControl.bluetoothReel[i].GetComponent<Button>().interactable = true;
					bleControl.bluetoothReel[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
					bleControl.bluetoothReel[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
				}
				isReel = false;
			}
		}
		// Main 연결성공시 _readFoundMain(true), _writeFoundMain(true)
		if (_readFoundMain && _writeFoundMain)
		{
			_readFoundMain = false;
			_writeFoundMain = false;
			_subscribingTimeoutMain = 1f;
		}
		// Reel 연결성공시 _readFoundReel(true), _writeFoundReel(true)
		if (_readFoundReel && _writeFoundReel)
		{
			_readFoundReel = false;
			_writeFoundReel = false;
			_subscribingTimeoutReel = 1f;
		}

        // Main 장비 데이터 수신
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
                        checkSum = data;
                        if (checkSum[2] == 1)
                        {
                            error = 0;
                        }
                        else if (error < 10 && checkSum[2] == 2)
                        {
                            error++;
                            fishControl._fishBLDC = 0;
                            fishControl._normalBLDC = 0;
                            fishControl.dc = 0;
                            Motor(0, 0);
                        }
                    }
                });
            }
        }

        // Reel 장비 데이터 수신
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
                    // 신형 릴
                    {
                        if (deviceAddress2.Equals(_connectedIDReel))
                        {
                            if (_isInGame_Reel)
                            {
                                string jsonString = Encoding.Default.GetString(data);
                                _reelData = JsonConvert.DeserializeObject<ReelBlueToothData>(jsonString);
                                gameMgr._reelData = _reelData;
                                fishControl._reelData = _reelData;

                                if (!_preH.Equals(_reelData.H) && gameMgr.NeedleInWater)
                                {
                                    if ( gameMgr.NeedleInWater)
                                    {
                                        ReelOn2();
                                    }

                                    if (count < 1 && gameMgr.NeedleInWater)
                                    {
                                        count += 0.22f;
                                    }

                                    first = data;

                                    if (!isAnim && gameMgr.NeedleInWater)
                                    {
                                        animCor = StartCoroutine(AnimSpeed());
                                    }

                                    _reelOffDelayTime = 0;
                                }
                                else
                                {
                                    _reelOffDelayTime += Time.deltaTime;
                                }

                                if (_reelOffDelayTime > 0.07f)
                                {
                                    reeling._IsReeling = false;
                                }

                                _preH = _reelData.H;
                            }
                        }
                    }
				});
			}
		}
	}

	public void ReelOn2()
    {
		reeling._IsReeling = true;

		if (!fishControl.isBite)
			reeling.OnReeling();
	}

	// 릴 데이터 수신시
	private IEnumerator ReelOn()
	{
		reeling._IsReeling = true;

		if (!fishControl.isBite)
			reeling.OnReeling();

		//yield return PublicDefined._01secDelay;
		yield return PublicDefined._02secDelay;

        StopCoroutine(_reelOnCoroutine);
		_reelOnCoroutine = null;
		reeling._IsReeling = false;
		yield return null;

		if (!fishControl.isBite)
			reeling.OnReeling();
	}

	// 연결되어있다는것을 표시할 때 사용하면 됨
	public void IsConnecting()
	{
		if (nameMain != null)
		{
			bleControl.bluetoothMain[0].SetActive(true);
			bleControl.nameMainText[0].text = nameMain;
			bleControl.addressMainText[0].text = addressMain;
			bleControl.bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[0];
			bleControl.bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

		}
		if (nameReel != null)
		{
			bleControl.bluetoothReel[0].SetActive(true);
			bleControl.nameReelText[0].text = nameReel;
			bleControl.addressReelText[0].text = addressReel;
			bleControl.bluetoothReel[0].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[0];
			bleControl.bluetoothReel[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);
		}
	}


	#region 이전 코드(string방식)
	public void Btn1()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@300");
		SendBytes(bytes);
	}
	public void Btn2()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@205");
		SendBytes(bytes);
	}
	public void Btn3()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@310");
		SendBytes(bytes);
	}
	public void Btn4()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@340");
		SendBytes(bytes);
	}
	public void Btn5()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@370");
		SendBytes(bytes);
	}
	public void Btn6()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@400");
		SendBytes(bytes);
	}
	public void Btn7()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@520");
		SendBytes(bytes);
	}
	public void Btn8()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@508");
		SendBytes(bytes);
	}
	public void Btn9()
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes("@@599");
		SendBytes(bytes);
	}
	public void Btn90(string data)
	{
		byte[] bytes = ASCIIEncoding.UTF8.GetBytes(data);
		SendBytes(bytes);
	}
	#endregion
	#region ChecksumButtonTest
	public void BLDC0DC0()
	{
		byte[] bytes = { 1, 1, 0, 0, 2 };
		SendBytes(bytes);
	}
	public void BLDC30DC0()
	{
		byte[] bytes = { 1, 1, 30, 0, 32 };
		SendBytes(bytes);
	}
	public void BLDC60DC0()
	{
		byte[] bytes = { 1, 1, 60, 0, 62 };
		SendBytes(bytes);
	}
	public void BLDC90DC0()
	{
		byte[] bytes = { 1, 1, 90, 0, 92 };
		SendBytes(bytes);
	}
	public void BLDC30DC30()
	{
		byte[] bytes = { 1, 1, 30, 30, 62 };
		SendBytes(bytes);
	}
	public void BLDC60DC60()
	{
		byte[] bytes = { 1, 1, 60, 60, 122 };
		SendBytes(bytes);
	}
	public void BLDC90DC90()
	{
		byte[] bytes = { 1, 1, 90, 90, 182 };
		SendBytes(bytes);
	}
	public void BLDC0DC0X()
	{
		byte[] bytes = { 1, 1, 0, 0, 0 };
		SendBytes(bytes);
	}
	public void BLDC30DC0X()
	{
		byte[] bytes = { 1, 1, 30, 0, 0 };
		SendBytes(bytes);
	}
	public void BLDC60DC0X()
	{
		byte[] bytes = { 1, 1, 60, 0, 0 };
		SendBytes(bytes);
	}
	public void BLDC90DC0X()
	{
		byte[] bytes = { 1, 1, 90, 0, 0 };
		SendBytes(bytes);
	}
	public void BLDC30DC30X()
	{
		byte[] bytes = { 1, 1, 30, 30, 0 };
		SendBytes(bytes);
	}
	public void BLDC60DC60X()
	{
		byte[] bytes = { 1, 1, 60, 60, 0 };
		SendBytes(bytes);
	}
	public void BLDC90DC90X()
	{
		byte[] bytes = { 1, 1, 90, 90, 0 };
		SendBytes(bytes);
	}
	#endregion
	#region 사용중인 코드(byte방식)
	public void Motor(int bldc, int dc)
	{
		_bldcForce = bldc;
		_dcForce = dc;
		//Debug.Log("현재 Motor()로 들어오는 bldc : " + bldc + ", dc : " + dc);
		//Debug.Log("BLETotal/Motor()/bldc : " + bldc);
		byte[] bytes = { 1, 1, (byte)_bldcForce, (byte)_dcForce, (byte)(2 + bldc + dc) };
		SendBytes(bytes);
	}
	#endregion

	public void MotorResetCheck()
    {
		// 모터가 돌아가고 있다면 리셋해준다.
		if(_bldcForce > 0 || _dcForce > 0)
        {
			_bldcForce = 0;
			_dcForce = 0;

			//Debug.Log("현재 모터가 작동 중이라서 리셋합니다.");
			byte[] bytes = { 1, 1, (byte)_bldcForce, (byte)_dcForce, (byte)(2 + _bldcForce + _dcForce) };
			SendBytes(bytes);
		}
    }

	private void SendBytes(byte[] data)
	{
		BluetoothLEHardwareInterface.WriteCharacteristic(_connectedIDMain, FullUUID(_serviceUUID), 
            FullUUID(_writeCharacteristicUUID), data, data.Length, true, (characteristicUUID) => {
			
			// 로그 확인시 주석 해제
			//BluetoothLEHardwareInterface.Log ("Write Succeeded");
		});
	}

	// 이전 Anim돌리던 방식
	private void OnAnim()
	{
		if(animCor == null)
		{
			animCor = StartCoroutine(AnimSpeed());
		}
		else
		{
			StopCoroutine(animCor);
			animCor = null;
		}
	}

	// 지금 사용중인 Anim 방식
	private IEnumerator AnimSpeed()
	{
		WaitForSeconds delay = new WaitForSeconds(0.12f);

		isAnim = true;
		while (count >= 0.2f && gameMgr.NeedleInWater)
		{
			if (first.Equals(second) && count >= 0.2f)
			{
				count -= 0.2f;
			}
			second = first;
			reeling.myAnim.SetFloat("Speed", count);
			yield return delay;
			if (first.Equals(second) && count >= 0.2f)
			{
				count -= 0.2f;
			}
			second = first;
			reeling.myAnim.SetFloat("Speed", count);
			yield return delay;
			if (first.Equals(second) && count >= 0.2f)
			{
				count -= 0.2f;
			}
			second = first;
			reeling.myAnim.SetFloat("Speed", count);
			yield return delay;
		}
		isAnim = false;
		count = 0;
		reeling.myAnim.SetFloat("Speed", 0);
		StopCoroutine(animCor);
	}

	public void Initialize0()
	{
		BluetoothLEHardwareInterface.Initialize(true, false, () => {

		}, (error) => {
		});
	}

	// 주변 블루투스 탐색 후 데이터 받아와서 보여줌
	public void OnScan()
	{
		bleControl.isBleOn = true;
		if (_scanning)
		{
			time = 0;
			_scanning = false;
			bleControl.animator.SetBool(_scanningHash, false);
			BluetoothLEHardwareInterface.StopScan();
		}
		else // 멈추기 -> 찾기
		{
			bleControl.ResetButton();
			bleControl.animator.SetBool(_scanningHash, true);
			BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
			{
				AddPeripheral(name, address);
			}, (address, name, rssi, advertisingInfo) =>
			{
				if (advertisingInfo != null)
					BluetoothLEHardwareInterface.Log(string.Format("Device: {0} RSSI: {1} Data Length: {2} Bytes: {3}", name, rssi, advertisingInfo.Length, BytesToString(advertisingInfo)));
			});
		    bleControl.reelOn = true;
			_scanning = true;
		}
	}

	// 장비 연결 중 UI 애니메이션(Main 따로 Reel따로 해주면 좋을 듯)
	public IEnumerator ConnectingAnim(int index)
	{
		float addColor = 0;
		bool isDown = false;
		while (isMain && !ConnectedMain)
		{
			if (addColor < 0.9 && !isDown)
				addColor += 0.01f;
			else if (addColor >= 0.9 && !isDown)
				isDown = true;
			else if (addColor > 0.1f && isDown)
				addColor -= 0.01f;
			else if (addColor <= 0.1f && isDown)
				isDown = false;
			bleControl.bluetoothMain[index].transform.GetChild(0).Rotate(0, 0, -15);
			bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().color = new Color(addColor, addColor, 1);
			yield return null;
		}
		while (isReel && !ConnectedReel)
		{
			if (addColor < 0.9 && !isDown)
				addColor += 0.01f;
			else if (addColor >= 0.9 && !isDown)
				isDown = true;
			else if (addColor > 0.1f && isDown)
				addColor -= 0.01f;
			else if (addColor <= 0.1f && isDown)
				isDown = false;
			bleControl.bluetoothReel[index].transform.GetChild(0).Rotate(0, 0, -15);
			bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().color = new Color(addColor, addColor, 1);
			yield return null;
		}
		bleControl.bluetoothMain[index].transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
		bleControl.bluetoothReel[index].transform.GetChild(0).rotation = new Quaternion(0, 0, 0, 0);
	}
	
	// 탐색 완료된 Main장비 클릭시
	public void OnConnectMain(int index)
	{
		// 이미 연결되어 있을 때
		if (ConnectedMain)
		{
			disconnectMain(Address =>
			{
				ConnectedMain = false;	// 연결 false
				isMain = false;	// 연결 false
				nameMain = null;	// 이름 비움
				addressMain = null;	// 주소 비움
				// 연결 해제 UI
				bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[2];
				bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				// 스캔 중? 처음부터 스캔 : 스캔시작
				if (!_scanning)
					OnScan();
				else
					time = 0;
			});
		}
		// 연결 시도
		else
		{
			isMain = true;
			time1 = 0;
			for (int i = 0; i < bleControl.bluetoothMain.Length; i++)
			{
				bleControl.bluetoothMain[i].GetComponent<Button>().interactable = false;
				//bleControl.bluetoothReel[i].GetComponent<Button>().interactable = false;
			}

			//Debug.LogError("메인 연결");

			// 데이터 받기 준비(연결 후에 true로 바꿔줌)
			_readFoundMain = false;
			_writeFoundMain = false;

			// 연결 중 UI 
			bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[1];
			bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 1);
			connectingAnimCor = StartCoroutine(ConnectingAnim(index));

			BluetoothLEHardwareInterface.ConnectToPeripheral(addressMain, (address) => {
			},
			(address, serviceUUID) => {
			},
			(address, serviceUUID, characteristicUUID) => {

				// discovered characteristic
				if (IsEqual(serviceUUID, _serviceUUID))
				{
					_connectedIDMain = address;
					ConnectedMain = true;
					bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[0];
					bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

					if (IsEqual(characteristicUUID, _readCharacteristicUUID))
					{
						_readFoundMain = true;
						//Debug.LogError(_readFoundMain);
					}
					else if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
					{
						_writeFoundMain = true;
						//Debug.LogError(_writeFoundMain);
					}
					//for (int i = 0; i < 10; i++)
					//{
					//	if (i == bleControl.bluetoothMain.Length) 
					//		bleControl.bluetoothMain[i].GetComponent<Button>().interactable = true;
					//	bleControl.bluetoothReel[i].GetComponent<Button>().interactable = true;
					//}
				}
			}, (address) => {
				///<summary>연결 실패</ summary >
				/// this will get called when the device disconnects
				/// be aware that this will also get called when the disconnect
				/// is called above. both methods get call for the same action
				/// this is for backwards compatibility
				/// 이것은 장치 연결이 끊길 때 호출됩니다.
				/// 위에서 연결 끊기가 호출될 때도 호출됩니다.
				/// 두 메서드 모두 동일한 작업에 대한 호출을 가져옵니다.
				/// 이는 이전 버전과의 호환성을 위한 것입니다.
				if (address == addressMain)
				{
					ConnectedMain = false;	// 연결실패시에 초기화
					isMain = false;			// 연결실패시에 초기화
					nameMain = null;		// 연결실패시에 초기화
					addressMain = null;		// 연결실패시에 초기화
					bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[2];
					bleControl.bluetoothMain[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				}
			});
		}
	}

	// 연결 해제(연결해제만 넣어놓은 함수, 지금은 사용 X)
	public void OffConnectMain()
	{
		BluetoothLEHardwareInterface.ConnectToPeripheral(addressMain, (address) =>
		{
		},
		(address, serviceUUID) =>
		{
		},
		(address, serviceUUID, characteristicUUID) =>
		{
		}, (address) =>
		{
			// this will get called when the device disconnects
			// be aware that this will also get called when the disconnect
			// is called above. both methods get call for the same action
			// this is for backwards compatibility
			if (address == addressMain)
			{
				ConnectedMain = false;
				isMain = false;
				nameMain = null;
				addressMain = null;
				bleControl.bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[2];
				bleControl.bluetoothMain[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
			}
		});
	}

	// 탐색 완료된 Reel장비 클릭시
	public void OnConnectReel(int index)
	{
		if (ConnectedReel)
		{
			if (bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().sprite == bleControl.stateImg[0])
			{
				disconnectReel((Address) =>
				{
					ConnectedReel = false;
					isReel = false;
					nameReel = null;
					addressReel = null;
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[2];
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
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
			isReel = true;
			time1 = 0;
			for (int i = 0; i < bleControl.bluetoothMain.Length; i++)
			{
				//bleControl.bluetoothMain[i].GetComponent<Button>().interactable = false;
				bleControl.bluetoothReel[i].GetComponent<Button>().interactable = false;
			}
			_readFoundReel = false;
			_writeFoundReel = false;
			bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[1];
			bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 1, 1);
			connectingAnimCor = StartCoroutine(ConnectingAnim(index));

			BluetoothLEHardwareInterface.ConnectToPeripheral(addressReel, (address) => {
			},
			(address, serviceUUID) => {
			},
			(address, serviceUUID, characteristicUUID) => {

				// discovered characteristic
				if (IsEqual(serviceUUID, _serviceUUID))
				{
					Debug.LogError("OnConnectReel의 else문 : " + serviceUUID);

					_connectedIDReel = address;
					ConnectedReel = true;
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[0];
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, 1);

					if (IsEqual(characteristicUUID, _readCharacteristicUUID))
					{
						_readFoundReel = true;
					}
					else if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
					{
						_writeFoundReel = true;
					}
					for (int i = 0; i < bleControl.bluetoothReel.Length; i++)
					{
						bleControl.bluetoothReel[i].GetComponent<Button>().interactable = true;
						if (i == index)
							bleControl.bluetoothReel[i].GetComponent<Button>().interactable = true;
					}
				}
			}, (address) => {

				// this will get called when the device disconnects
				// be aware that this will also get called when the disconnect
				// is called above. both methods get call for the same action
				// this is for backwards compatibility
				if (address == addressReel)
				{
					ConnectedReel = false;
					isReel = false;
					nameReel = null;
					addressReel = null;
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().sprite = bleControl.stateImg[2];
					bleControl.bluetoothReel[index].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, 1);
				}
			});
		}
		
	}
	// Main 연결 해제
	void disconnectMain(Action<string> action)
	{
		BluetoothLEHardwareInterface.DisconnectPeripheral(addressMain, action);
	}
	// Reel 연결 해제
	void disconnectReel(Action<string> action)
	{
		BluetoothLEHardwareInterface.DisconnectPeripheral(addressReel, action);
	}

	// UUID 확인(uuid 비교하여 bool형으로 반환)
	bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID(uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID(uuid2);        

		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}
	// 예약 UUID에 4자리 숫자 삽입
	string FullUUID(string uuid)
	{
		// 개발자가 작업한 UUID 번호임
		return "6E40" + uuid + "-B5A3-F393-E0A9-E50E24DCCA9E";
	}

	// 탐색된 블루투스 추가
	void AddPeripheral(string name, string address)
	{
		string mainString = "MM_Main";

		// 신형 릴
		string reelString = "Fishing";
		// 구형 릴
		//string reelString = "MMFishing";
		//if (_peripheralList == null)
		//{
		//	_peripheralList = new Dictionary<string, CentralPeripheralButtonScript>();
		//}
		// 이름에 MM_Main 있을 떄
		if (/*!_peripheralList.ContainsKey(address) && */name.Contains(mainString))
		{
			//Debug.Log("함수(AddPeripheral) 속 메인 장치 address : " + address + " , name : " + name);
			Debug.Log("메인 기기 주소와 이름  " + address + " , " + name);
			bleControl.ActiveMainBtn(name, address);
		}
		// 또는 MMFishing 있을 때
		else if (/*!_peripheralList.ContainsKey(address) && */ name.Contains(reelString))
        {
			// 신형 릴
			{
				if (name.Contains("MMFishing"))
					return;
			}

            //Debug.Log("함수(AddPeripheral) 속 릴 address : " + address + " , name : " + name);
            //Debug.Log("릴 주소와 이름  " + address + " , " + name);
			bleControl.ActiveReelBtn(name, address);
		}
		/// <summary>주소를 확인하는 이유</summary>
		/// 이름은 변경가능한 부분이지만
		/// 주소는 고유하기 때문에 name보다는 address를 이용해서
		/// 사용하는게 나음
	}

	// byte -> string
	protected string BytesToString(byte[] bytes)
	{
		string result = "";

		foreach (var b in bytes)
			result += b.ToString("X2");

		return result;
	}
}
