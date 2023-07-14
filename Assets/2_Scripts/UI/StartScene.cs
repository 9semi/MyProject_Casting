using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void Awake()
    {
        // 블루투스 권한

        //Debug.LogError(1);

        //AndroidRuntimePermissions.Permission result3 = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
        //if (result3 == AndroidRuntimePermissions.Permission.Granted)
        //    Debug.Log("android.permission.ACCESS_FINE_LOCATION 권한 부여");
        //else
        //    Debug.Log("android.permission.ACCESS_FINE_LOCATION 권한 에러");

        //AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.BLUETOOTH");
        //if (result == AndroidRuntimePermissions.Permission.Granted)
        //    Debug.Log("android.permission.BLUETOOTH 권한 부여");
        //else
        //    Debug.Log("android.permission.BLUETOOTH 권한 에러");

        //AndroidRuntimePermissions.Permission result2 = AndroidRuntimePermissions.RequestPermission("android.permission.BLUETOOTH_ADMIN");
        //if (result2 == AndroidRuntimePermissions.Permission.Granted)
        //    Debug.Log("android.permission.BLUETOOTH_ADMIN 권한 부여");
        //else
        //    Debug.Log("android.permission.BLUETOOTH_ADMIN 권한 에러");
    }
    private void Start()
    {
        //Debug.Log("StartScene/Start()");

        StartCoroutine(LoadScene());

        if (!Application.platform.Equals(RuntimePlatform.WindowsEditor))
        {
            //SceneManager.LoadScene("LoginScene");
            LogoVideoControl.INSTANCE.LoadScene_IfVideoDone();
        }
    }

    IEnumerator LoadScene()
    {
        AudioManager.INSTANCE.DontDestroy();
        DBManager.INSTANCE.DontDestroy();
        DataManager.INSTANCE.DontDestroy();
        PassManager.INSTANCE.DontDestroy();
        ItemData.Instance.DontDestroy();


        //if (Application.platform.Equals(RuntimePlatform.WindowsEditor))
        //    SceneManager.LoadScene("LobbyScene");
        //else
        //    SceneManager.LoadScene("LoginScene");

        //Debug.Log("StartScene/LoadScene 마지막");

        yield return null;
    }
}
