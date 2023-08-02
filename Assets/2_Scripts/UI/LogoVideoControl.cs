using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class LogoVideoControl : MonoBehaviour
{
    static LogoVideoControl _uniqueInstance;
    static public LogoVideoControl INSTANCE
    {
        get { return _uniqueInstance; }
    }

    public VideoPlayer _logoVideo;

    //AppUpdateManager _appUpdateManager;

    void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        LoadScene_IfVideoDone();
    }

    public void LoadScene_IfVideoDone()
    {
        //Debug.Log("비디오2");
        StartCoroutine(MoveToLobby());
    }
    IEnumerator MoveToLobby()
    {
        // 로고 영상은 모바일만 실행하는 것으로.
        //Debug.Log(_logoVideo.frame);

        while (_logoVideo.frame < 119)
        {
            yield return PublicDefined._05secDelay;
        }

        if(DataManager.INSTANCE._isNoneLoginVersion)
            SceneManager.LoadScene("LobbyScene");
        else
            SceneManager.LoadScene("LoginScene");
    }
}
