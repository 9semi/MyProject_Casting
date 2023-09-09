using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    readonly int _blockspeedHash = Animator.StringToHash("BlockSpeed");
    
    Animator _ani;
    Vector3 _myOriginPos;
    GameObject _reelAudioObject = null;

    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>() ;
        _gameManager.SetCharacterManagerInstance(this);
        _ani = GetComponent<Animator>();
        _ani.SetFloat("ReelingSpeed", 0.5f);
        _ani.SetFloat(_blockspeedHash, 1f);
        _myOriginPos = transform.position;
    }
    public void Shot()
    {
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.casting).GetComponent<AudioPoolObject>().Init();
        
        _gameManager.SettingTargetOfCamera();
        _gameManager.CastingNeedle();
        
        StartCoroutine(ShotCoroutine());

        if (!_gameManager.UserData.GetCurrentEquipmentDictionary()["sinker"].Equals(-1))
        {
            _gameManager.SetSinkerObjectActive(false);
        }
        
        _reelAudioObject = AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.reelUntied, true);
    }
    public void ForwardSpeed()
    {
        _ani.SetFloat(_blockspeedHash, 1f);
        AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.rodSpring).GetComponent<AudioPoolObject>().Init();
    }
    public void ReverseSpeed()
    {
        _ani.SetFloat(_blockspeedHash, -0.5f);
    }
    public void CharacterTransformReset()
    {
        transform.eulerAngles = Vector3.zero;
        transform.position += Vector3.forward;
    }
    
    public void ResetPos()
    {
        transform.position = _myOriginPos;
        _gameManager.RotateStop = false;
    }

    IEnumerator ShotCoroutine()
    {
        yield return new WaitForSeconds(0.08f);
        _gameManager.IsFly = true;
        yield return null;
    }
    public GameObject GetReelAudioObject()
    {
        return _reelAudioObject;
    }
    public float GettingAnimator(int animatorStringHash)
    {
        return _ani.GetFloat(animatorStringHash);
    }
    public void SetReelAudioObject(GameObject g)
    {
        _reelAudioObject = g;
    }
    public void SettingAnimator(int animatorStringHash)
    {
        _ani.SetTrigger(animatorStringHash);
    }
    public void SettingAnimator(int animatorStringHash, bool b)
    {
        _ani.SetBool(animatorStringHash, b);
    }
    public void SettingAnimator(int animatorStringHash, float f)
    {
        _ani.SetFloat(animatorStringHash, f);
    }
}
