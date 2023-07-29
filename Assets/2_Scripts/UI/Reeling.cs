using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reeling : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 스크립트 연결
    InGameUIManager _ingameUIManager;
    GameManager _gameManager;
    BLETotal bleTotal;
    WaitForSeconds _delay = PublicDefined._02secDelay;

    public FishControl fishControl;
    bool _isReeling = false; public bool _IsReeling { get { return _isReeling; } set { _isReeling = value; } }
    public Animator myAnim; // 릴 애니메이션
    public Coroutine _reelingCoroutine = null;   // reeling 유지 코루틴

    BoxCollider _needleCollider;

    bool _isConnectedReel = false;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _ingameUIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUIManager>();
        GameObject.FindGameObjectWithTag("Tension").GetComponent<TensionUI>().GetReelingInstance(this);
    }

    void Start()
    {
        myAnim = GetComponent<Animator>();
        // 블루투스가 있으면 연결시켜줌

        if (GameObject.FindGameObjectWithTag("Bluetooth"))
        {
            bleTotal = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BLETotal>();

            if(bleTotal._connectedReel)
            {
                _isConnectedReel = true;
            }
            
            bleTotal.reeling = this;
        }

        _ingameUIManager._Reeling = this;
        _needleCollider = _gameManager.ReelPoint3.GetComponent<BoxCollider>();
    }

    // Screen Canvas -> Reel_Metal
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameManager.NeedleInWater)
        {
            _isReeling = true;
            myAnim.SetFloat("Speed", 1);

            if (!fishControl.IsBite)
                OnReeling();
        }
    }

    // Screen Canvas -> Reel_Metal
    public void OnPointerUp(PointerEventData eventData)
    {
        _isReeling = false;
        //_needleCollider.isTrigger = false;
        myAnim.SetFloat("Speed", 0);

        if (!fishControl.IsBite)
            OnReeling();
    }

    // reeling 중? 
    public void OnReeling()
    {
        if (_reelingCoroutine == null)
        {
            _reelingCoroutine = StartCoroutine(KeepReeling());

            if (!_gameManager.IsNeedleMoving && _gameManager.NeedleInWater)
                _gameManager.NeedleStartMoving();
        }
        else
        {
            StopCoroutine(_reelingCoroutine);
            _reelingCoroutine = null;
        }
    }
    public void BluetoothReeling()
    {
        _ingameUIManager.Reeling();
    }

    public void StopReeling()
    {
        if(_reelingCoroutine != null)
        {
            //Debug.Log("reeling 멈추기");
            StopCoroutine(_reelingCoroutine);
            _reelingCoroutine = null;
        }
    }

    // reeling 유지시
    private IEnumerator KeepReeling()
    {
        while (_isReeling)
        {
            // 주기적으로 reeling 호출
            _ingameUIManager.Reeling();
            yield return _delay;
        }
    }

    public void BluetoothReelOn()
    {
        if (_gameManager.NeedleInWater)
        {
            _isReeling = true;
            //_needleCollider.isTrigger = true;
            myAnim.SetFloat("Speed", 1);

            if (!fishControl.IsBite)
                OnReeling();
        }
    }
    public void BluetoothReelOff()
    {
        _isReeling = false;
        //_needleCollider.isTrigger = false;
        myAnim.SetFloat("Speed", 0);

        if (!fishControl.IsBite)
            OnReeling();
    }
}
