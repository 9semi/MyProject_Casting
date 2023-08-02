using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{
    [SerializeField] Animator _startButtonAni;
    private void Start()
    {
        AudioManager.INSTANCE.PlayBGM(PublicDefined.eBGMType.loginscene, true);
    }

    private void OnEnable()
    {
        _startButtonAni.SetTrigger("Click");
    }
    public void Click()
    {
        DataManager.INSTANCE._mapType = PublicDefined.eMapType.lobby;
        //SoundManager.instance.EffectPlay("Click");
        //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.gameStartButton).GetComponent<AudioPoolObject>().Init();
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.mainClick).GetComponent<AudioPoolObject>().Init();
        AudioManager.INSTANCE.StopBGM();

        LoadingSceneManager.LoadScene("LobbyScene");
    }
}
