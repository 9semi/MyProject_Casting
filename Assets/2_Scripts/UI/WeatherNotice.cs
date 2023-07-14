using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherNotice : MonoBehaviour
{
    public Text _timeNoticeText;
    public Text _respawnNoticeText;

    StringBuilder _sb;
    

    public void Init(int worldTime)
    {
        if (_sb == null)
            _sb = new StringBuilder();
        else
            _sb.Length = 0;

        _sb.Append(worldTime);
        _sb.Append("시가 되었습니다.");
        _timeNoticeText.text = _sb.ToString();
        gameObject.SetActive(true);
        StartCoroutine(NoticeCoroutine());
    }

    IEnumerator NoticeCoroutine()
    {

        _timeNoticeText.gameObject.SetActive(true);

        yield return PublicDefined._4secDelay;

        _timeNoticeText.gameObject.SetActive(false);
        _respawnNoticeText.gameObject.SetActive(true);

        yield return PublicDefined._4secDelay;

        _respawnNoticeText.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }
}
