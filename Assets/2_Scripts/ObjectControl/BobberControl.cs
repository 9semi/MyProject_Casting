using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobberControl : MonoBehaviour
{
    // y축 힘
    private float quillPoy = 100f;
    // 찌의 위아래 애니메이션
    //public Animation quillWave;
    // 찌 리지드바디
    public Rigidbody rd;
    GameManager gameMgr;
    public Transform myTr;
    public Text distTx;
    private float dist;
    public bool isBobberCollision = false;
    // 파티클 연결 
    public Transform particleObject;

    void Start()
    {
        gameMgr = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        myTr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody>();
        // 파티클 연결 작업
        particleObject = GameObject.FindGameObjectWithTag("Object").transform.GetChild(0);
    }


    // 찌가 다른 콜리더에 들어갈때
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Water"))
        {
            gameMgr.NeedleInWater = true;
            // 물과 접촉시 포지션 정지
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //SoundManager.instance.EffectPlay("Sinker_Hits_The_Surface");
            //AudioManager.INSTANCE.PlayEffect(PublicDefined.eEffectSoundType.sinkerReachesTheSurface).GetComponent<AudioPoolObject>().Init();
        }
    }
}
