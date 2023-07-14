using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/* 효과음(이펙트 플레이어) 목록
 /////// 게임씬 랜덤 재생
0 : seagull
1 : 고양이 울음소리1
2 : 고양이 울음소리2
///////////////////////////
3 : 낚싯대 휘두르기
4 : 릴 풀리는 소리(샷 호출때)
5 : 봉돌 수면에 닿는 소리
6 : 봉돌 물속 바닥에 닿는 소리
7 : reeling
8 : 물고기 저항( 물안에서 헤엄소리)
9 : 낚싯대 휘는 소리
10 : 물고기 낚아 올리는 소리 (첨벙)
11 : 고양이 클릭시(플레이어 선택)
12 : 고양이 떡밥 던질 때 소리
13 : 고양이 떡밥 주걱 휘두르는 소리
14 : 떡밥 수면에 부딪히는 소리
15 : 그냥 성공
16 : 대 성공
17 : 실패
18 : 할아버지 기합
19 : 할아버지 실패
20 : 별똥별
21 : 손전등
22 : 도감 스크롤
23 : UI 버튼 클릭
24 : 챔질 성공
25 : 챔질 실패
26 : bitting 시작때 나는 소리
*/
public class SoundManager : MonoBehaviour
{
    static SoundManager _unqueInstance;
    //static SoundManager _unqueInstance;
    static public SoundManager instance
    {
        get { return _unqueInstance; }
    }

    [System.Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip clip;
    }


    public Option option;
    public Sound[] bgmSound;
    public Sound[] effectSounds;

    public AudioSource[] bgmPlayer;
    public AudioSource[] effectPlayer;

    void Awake()
    {
        if (_unqueInstance == null)
            _unqueInstance = this;
    }

    void Start()
    {
        BGMPlay(0);
    }

    public void Initialize()
    {
        //start

    }


    public void BGMPlay(int bgmNum)
    {
        bgmPlayer[bgmNum].clip = bgmSound[bgmNum].clip;
        bgmPlayer[bgmNum].Play();
    }
    public void BGMStop(int bgmNum)
    {
        bgmPlayer[bgmNum].Stop();
    }
    public void EffectPlay(string _soundName)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_soundName == effectSounds[i].soundName)
            {
                effectPlayer[i].Play();
                return;
            }
        }
    }
}