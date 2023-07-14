using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioManager : MonoBehaviour
{
    static AudioManager _uniqueInstance;
    static public AudioManager INSTANCE
    {
        get { return _uniqueInstance; }
    }

    [SerializeField] AudioClip[] _bgmClips;
    [SerializeField] AudioClip[] _effectClips;

    [SerializeField] AudioSource _bgmPlayer;
    [SerializeField] Transform _effectPlayer;

    [SerializeField] GameObject _audioPoolObject;

    [SerializeField] AudioClip _castingReelEffectClip;

    // 배경음 기본 정보
    [HideInInspector] public float _bgmVolume;
    [HideInInspector] public bool _bgmMute;
    [HideInInspector] public float _currentBGMProgressTime = 0;

    // 효과음 기본 정보
    [HideInInspector] public float _effectVolume;
    [HideInInspector] public bool _effectMute;

    // 효과음 큐
    Queue<GameObject> _effectQueue = new Queue<GameObject>();

    private void Awake()
    {

        InitEffectQueue();
        _uniqueInstance = this;
    }

    //private void Update()
    //{
    //    Debug.Log(_bgmPlayer.clip.name + " , " + _bgmPlayer.volume + " , " + _bgmPlayer.mute);
    //}

    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void BGMSetting(float volume, bool mute)
    {
        //Debug.Log("오디오매니저BGM세팅 volume : " + volume);
        _bgmVolume = volume;
        _bgmMute = mute;

        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.mute = _bgmMute;
    }
    public void EffectSetting(float volume, bool mute)
    {
        _effectVolume = volume;
        _effectMute = mute;

        for(int i = 0; i < _effectPlayer.childCount; i++)
        {
            _effectPlayer.GetChild(i).GetComponent<AudioSource>().volume = _effectVolume;
            _effectPlayer.GetChild(i).GetComponent<AudioSource>().mute = _effectMute;
        }
    }
    public void InitEffectQueue()
    {
        for (int i = 0; i < _effectPlayer.childCount; i++)
        {
            _effectQueue.Enqueue(_effectPlayer.GetChild(i).gameObject);
        }
    }

    // BGM 재생, 정지
    public void PlayBGM(PublicDefined.eBGMType type, bool isBeginning)
    {
        if (_bgmPlayer.isPlaying)
        {
            _bgmPlayer.Stop();
        }

        _bgmPlayer.clip = _bgmClips[(int)type];

        // 처음부터 재생
        if (isBeginning)
        {
            _bgmPlayer.time = 0;
        }
        else
        {
            _bgmPlayer.time = Mathf.Min(_currentBGMProgressTime, _bgmPlayer.clip.length);
            //Debug.LogError(_currentBGMProgressTime + " , " +  _bgmPlayer.clip);
        }


        _bgmPlayer.Play();
    }

    public void StopBGM()
    {
        _bgmPlayer.Stop();
    }

    public void SaveBGMPlayerCurrentTime()
    {
        _currentBGMProgressTime = _bgmPlayer.time;
        //_currentBGMProgressTime = AudioSettings.dspTime;
        //Debug.LogError(_currentBGMProgressTime);
    }

    public GameObject PlayEffect(PublicDefined.eEffectSoundType type, bool loop = false)
    {
        //Debug.Log(type);
        if (_effectQueue.Count > 0)
        {
            GameObject go = _effectQueue.Dequeue();
            AudioSource a = go.GetComponent<AudioSource>();

            go.SetActive(true);

            a.clip = _effectClips[(int)type];
            a.loop = loop;
            a.Play();

            return go;
        }
        else
        {

            GameObject go = Instantiate(_audioPoolObject, _effectPlayer);
            AudioSource a = go.GetComponent<AudioSource>();
            go.SetActive(true);
            a.clip = _effectClips[(int)type];
            a.loop = loop;
            a.Play();

            return go;
        }
    }

    public void ReturnEffectObject(GameObject ob)
    {
        ob.SetActive(false);
        _effectQueue.Enqueue(ob);
    }

    public bool CheckIsSameBGM(PublicDefined.eBGMType type)
    {
        if (_bgmPlayer.clip == null)
            return false;

        if (_bgmPlayer.clip.Equals(_bgmClips[(int)type]))
            return true;
        else
            return false;
    }

    public bool CheckBGMPlaying()
    {
        return _bgmPlayer.isPlaying;
    }

    public void StopAllEffect()
    {
        for (int i = 0; i < _effectPlayer.childCount; i++)
        {
            if(_effectPlayer.GetChild(i).gameObject.activeSelf)
            {
                //Debug.Log(i + "번째");
                _effectPlayer.GetChild(i).GetComponent<AudioPoolObject>().ReturnThis();
            }
        }
    }

    public void StopReelEffect()
    {
        for (int i = 0; i < _effectPlayer.childCount; i++)
        {
            if (_effectPlayer.GetChild(i).gameObject.activeSelf)
            {
                //Debug.Log(_effectPlayer.GetChild(i).GetComponent<AudioSource>().clip);
                if(_effectPlayer.GetChild(i).GetComponent<AudioSource>().clip.Equals(_castingReelEffectClip))
                {
                    _effectPlayer.GetChild(i).GetComponent<AudioPoolObject>().ReturnThis();
                }
            }
        }
    }
}
