using Firebase.Auth;
using Firebase.Database;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GoogleManager : MonoBehaviour
{
    [SerializeField] GameObject _gameStartButton;
    [SerializeField] Button _loginButton;
    [SerializeField] Text _stateText;
    [SerializeField] GameObject _createNicknameUIObject;
    [SerializeField] GameObject _updatePlzUI;

    FirebaseAuth auth;
    CreateNicknameUI _createNicknameUI;

    string ID;
    bool _isLogin;
    bool _updateNecessity;

    public class UserInformation
    {
        string uid;
        public string email;
        bool emailVerified;

        public UserInformation(string email, string uid, bool emailVerified)
        {
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
        _isLogin = true;
    }

    void Start()
    {
        _createNicknameUI = _createNicknameUIObject.GetComponent<CreateNicknameUI>();
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
    public void ClickLoginButton()
    {
        _loginButton.gameObject.SetActive(false);
        _stateText.gameObject.SetActive(true);
        
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestIdToken().RequestEmail().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        auth = FirebaseAuth.DefaultInstance;
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
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut(); 
            auth.SignOut();
            _isLogin = false;
            _loginButton.gameObject.SetActive(true);
            _gameStartButton.SetActive(false);
            DBManager.INSTANCE.DataLoadSuccess = false;
            DBManager.INSTANCE.DataLoadProgress = 0;
            DBManager.INSTANCE.UserDataInit();
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
    { 
        _stateText.text = string.Empty;
        _gameStartButton.SetActive(true);
    }

    public void CreateNicknameUI()
    {
        _createNicknameUIObject.transform.localPosition = Vector3.zero;
    }
    public void TheNameAlreadyExists()
    {
        _createNicknameUI.TheNameAlreadyExists();
    }

    public void TheNameIsAvailable()
    {
        _createNicknameUI.TheNameIsAvailable();
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