using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerAnim : MonoBehaviour
{
    [SerializeField]
    private GameObject particle; // 파티클 오브젝트 연결
    private float time; // 시간변수
    private bool isParticle;    // 한번만 타게끔 on,off 역할
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        isParticle = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 2초 활성화
        if (time <= 2f)
        {
            // 0.2초에 파티클 활성화
            if (!isParticle)
            {
                particle.SetActive(true);
                isParticle = true;
            }
            // 0.3초까지 커지다가 유지
            else if (time <= 0.5f)
            {
                transform.localScale = Vector3.one * (0.5f + time * 1.5f);
            }
        }
        else
        {
            time = 0;
            isParticle = false;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }
}
