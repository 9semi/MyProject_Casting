using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
//using Google.Play.AppUpdate;
//using Google.Play.Common;


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
        //_appUpdateManager = new AppUpdateManager();
    }

    //private void Update()
    //{
    //    Debug.Log(_logoVideo.frame);
    //}

    void Start()
    {
        //StartCoroutine(CheckUpdate());

        LoadScene_IfVideoDone();
    }

    //IEnumerator CheckUpdate()
    //{
    //    PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOper = _appUpdateManager.GetAppUpdateInfo();

    //    yield return appUpdateInfoOper;

    //    if(appUpdateInfoOper.IsSuccessful)
    //    {
    //        var appUpdateInfoResult = appUpdateInfoOper.GetResult();

    //        Debug.Log(appUpdateInfoResult.UpdateAvailability.Equals(UpdateAvailability.UpdateAvailable));

    //        //if(appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
    //        //{

    //        //}
    //        //else
    //        //{

    //        //}

    //        var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
    //        StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
    //    }

    //    yield return null;
    //}

    //IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    //{
    //    var startUpdateRequest = _appUpdateManager.StartUpdate(appUpdateInfoOp_i, appUpdateOptions_i);
    //    yield return startUpdateRequest;
    //}


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
