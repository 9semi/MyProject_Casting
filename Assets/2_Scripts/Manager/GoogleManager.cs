using Firebase.Auth;
using Firebase.Database;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
//
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//
//using Google.Play.AppUpdate;
//using Google.Play.Common;

public class GoogleManager : MonoBehaviour
{
    static GoogleManager _unqueInstance;
    static public GoogleManager INSTANCE
    {
        get { return _unqueInstance; }
    }

    public GameObject _gameStartButton;
    public Button _loginButton;
    public Text _stateText; // 테스트용 Text
    public GameObject _createNicknameUI;
    public GameObject _updatePlzUI;

    FirebaseAuth auth;

    string ID;
    bool _isLogin;

    // 업데이트 관련
    bool _updateNecessity;
    //AppUpdateManager _appUpdateManager;


    public class UserInformation
    {
        string uid;
        public string email;
        bool emailVerified;

        public UserInformation(string email, string uid, bool emailVerified)
        {
            // 초기화하기 쉽게 생성자 사용
            this.email = email;
            this.uid = uid;
            this.emailVerified = emailVerified;
        }

        public string GetUId()
        {
            return uid;
        }

        public void SetUId(string id)
        {
            uid = id;
        }
    }

    public DatabaseReference reference
    {
        get; set;
    }
    private void Awake()
    {
        _unqueInstance = this;
        _isLogin = true;
    }

    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        _loginButton.gameObject.SetActive(false);
        auth = FirebaseAuth.DefaultInstance;

        if (DataManager.INSTANCE._isTryLogout)
        {
            DataManager.INSTANCE._isTryLogout = false;
            TryGoogleLogout();
        }
        else
        {
            _loginButton.gameObject.SetActive(false);

            if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
                TryGoogleLogin();
        }
    }

    //IEnumerator CheckForUpdate()
    //{
    //    yield return PublicDefined._1secDelay;

    //    //Debug.Log("CheckForUpdate");

    //    PlayAsyncOperation <AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = _appUpdateManager.GetAppUpdateInfo();

    //    //Debug.Log("appUpdateInfoOperation : " + appUpdateInfoOperation);

    //    yield return appUpdateInfoOperation;

    //    if (appUpdateInfoOperation.IsSuccessful)
    //    {
    //        // 업데이트가 가능한지 확인
    //        var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

    //        bool test = appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable;
    //        //Debug.Log("업데이트 하나요: " + test);

    //        // 업데이트가 필요하다면 업데이트를 한다.
    //        if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
    //        {
    //            _stateText.text = "업데이트가 필요합니다.";
    //            yield return PublicDefined._2secDelay;

    //            _updateNecessity = true;

    //            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
    //            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
    //        }
    //        // 업데이트가 필요없다면 바로 로그인한다.
    //        else
    //        {
    //            _stateText.text = "최신 버전입니다.";
    //            _updateNecessity = false;
    //            DataManager.INSTANCE._updateCheck = true;

    //            _loginButton.gameObject.SetActive(false);
    //            // 구글로 로그인을 한다.

    //            yield return PublicDefined._1secDelay;

    //            if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
    //                TryGoogleLogin();
    //        }
    //    }
    //}
    //IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    //{
    //    Debug.Log("StartImmediateUpdate");
    //    var startUpdateRequest = _appUpdateManager.StartUpdate(appUpdateInfoOp_i, appUpdateOptions_i);
    //    yield return startUpdateRequest;
    //}

    public void ClickLoginButton()
    {
        _loginButton.gameObject.SetActive(false);
        _stateText.gameObject.SetActive(true);

        // 구글로 로그인을 한다.
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate(); // Activate: 작동시키다..
        auth = FirebaseAuth.DefaultInstance; // Firebase 액세스 //using Firebase.Auth

        //
        TryGoogleLogin();
    }


    public void TryGoogleLogin()
    {
        _loginButton.gameObject.SetActive(false);
        _stateText.gameObject.SetActive(true);

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    _stateText.text = "구글 로그인 성공";
                    TryFirebaseLogin();
                }
                else
                {
                    _stateText.text = "구글 로그인 실패(다시 시도)";
                    TryGoogleLogin();
                }
            });
        }
    }

    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // 로그인 되어 있다면
        {
            PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃         // using GooglePlayGames.BasicApi 
            auth.SignOut(); // Firebase 로그아웃
            _isLogin = false;
            _loginButton.gameObject.SetActive(true);
            _gameStartButton.SetActive(false);
            DBManager.INSTANCE.DataLoadSuccess = false;
            DBManager.INSTANCE.DataLoadProgress = 0;
            DBManager.INSTANCE.UserDataInit();

            //Debug.Log("로그아웃");
        }
    }

    void TryFirebaseLogin()
    {
        _stateText.text = "서버에 접근 중입니다.";

        if(string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
        {
            TryGoogleLogin();
        }
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(taskSign =>
        {
            if(taskSign.IsCompleted)
            {
                _stateText.text = "구글 로그인 완료";

                FirebaseUser user = auth.CurrentUser; 
                UserInformation userInformation = new UserInformation(user.Email, user.UserId, user.IsEmailVerified);
                DBManager.INSTANCE.SetUId(user.UserId);
                StartCoroutine(DBManager.INSTANCE.VersionCheck(userInformation));
            }
            else if (taskSign.IsCanceled)
            {
                _stateText.text = "구글 로그인 취소";
                return;
            }
            else if (taskSign.IsFaulted)
            {
                _stateText.text = "구글 로그인 실패";
                return;
            }
        });
    }

    public void SomethingsWrong()
    {
        if(!_stateText.gameObject.activeSelf)
            _stateText.gameObject.SetActive(true);

        _stateText.text = "무언가 잘못됐습니다. 로그인 실패.";
    }

    public void EverythingIsReadyUntilStart()
    { // 데이터를 로드하고 게임 시작 버튼을 킨다.
        _stateText.text = string.Empty;
        _gameStartButton.SetActive(true);
    }

    //public void SelectRodReelUI()
    //{
    //    _stateText.text = string.Empty;
    //    _selectRodReelUI.transform.localPosition = Vector3.zero;
    //}

    public void CreateNicknameUI()
    {
        //_createNicknameUI.SetActive(true);
        //Instantiate(_createNicknameUI);
        _createNicknameUI.transform.localPosition = Vector3.zero;
    }
    public void StateTextUpdate(string text)
    {
        if (!_stateText.gameObject.activeSelf)
            _stateText.gameObject.SetActive(true);

        _stateText.text = text;
    }

    public void StateTextOnOff(bool isOn)
    {
        _stateText.gameObject.SetActive(isOn);
    }

    public void UpdatePlz()
    {
        _updatePlzUI.SetActive(true);
    }
    public void ClickUpdateOKButton()
    {
        PlayClickEffectAudio();

        if (Application.platform == RuntimePlatform.Android)
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.mobilemovement.realisticfishinggamecasting");
        else if(Application.platform == RuntimePlatform.WindowsEditor)
        {

        }
        else
        {
            _updatePlzUI.SetActive(false);
        }
    }
    public void PlayClickEffectAudio()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
    }
}