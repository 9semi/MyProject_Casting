using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpatulaControl : MonoBehaviour
{
    public GameManager _gameManager;

    float _inputY = 0;
    float _rotY; public float RotY { get { return _rotY; } }
    float _minRotY = -19f;
    float _maxRotY = 25f;

    bool _tutorialStop;
    Coroutine _rotateCoroutine;
    bool _isBluetoothReelConnect;
    private void Awake()
    {
        _tutorialStop = false;
    }

    public void StartRotateCoroutine()
    {
        //_gameManager.characterTr.eulerAngles = Vector3.zero;
        _isBluetoothReelConnect = _gameManager.IsConnectedToBluetooth_Reel;

        _rotateCoroutine = StartCoroutine(RotateCoroutine());
    }
    public void StopRotateCoroutine()
    {
        //_gameManager.characterTr.eulerAngles = Vector3.zero;

        StopCoroutine(_rotateCoroutine);
    }

    IEnumerator RotateCoroutine()
    {
        while(_gameManager.BaitThrowMode)
        {
            if(!_tutorialStop)
            {
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    _inputY = Input.GetAxis("Horizontal");
                    _rotY += _inputY * 0.7f;
                    _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                    transform.eulerAngles = new Vector3(0, _rotY, 0);
                }
                // 모바일
                else
                {
                    // 블루투스 연결O
                    if (_isBluetoothReelConnect)
                    {
                        // 신형 릴
                        {
                            _inputY = _gameManager.ReelData.Za * -0.0007f;
                            _rotY += _inputY;
                            _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                            transform.eulerAngles = new Vector3(0, _rotY, 0);
                        }
                        // 구형 릴
                        {
                            //_inputY = Input.acceleration.x;
                            //_rotY += _inputY * 1.2f;
                            //_rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                            //transform.eulerAngles = new Vector3(0, _rotY, 0);
                        }
                    }
                    // 블루투스 연결X
                    else
                    {
                        _inputY = Input.acceleration.x;
                        _rotY += _inputY * 1.2f;
                        _rotY = Mathf.Clamp(_rotY, _minRotY, _maxRotY);

                        transform.eulerAngles = new Vector3(0, _rotY, 0);
                    }
                }
            }
            yield return null;
        }
    }
}
