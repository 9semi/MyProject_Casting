using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlapObject : MonoBehaviour
{
    [SerializeField] string _noticeText;
    [SerializeField] Text _text;
    Coroutine _timerCoroutine;

    private void OnEnable()
    {
        _text.text = _noticeText;

        if(_timerCoroutine == null)
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        else
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            _timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }
    IEnumerator TimerCoroutine()
    {
        //while(_time > 0)
        //{
        //    _time -= 0.1f;
        //    Debug.Log(_time);
        //    yield return null;
        //}
        //Debug.Log("≈ª√‚");
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
        
    }
}
