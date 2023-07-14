using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGuideNoticeUI : MonoBehaviour
{
    float _time = 0;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time > 3) 
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        _time = 0;
    }

    public void ClickGuideButton()
    {
        Application.OpenURL("https://cafe.naver.com/casting0406/13");
    }
}
