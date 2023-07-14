using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallerAnim : MonoBehaviour
{
    public GameObject particle;
    private float time;
    private bool isParticle;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        isParticle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 2f)
        {
            if (!isParticle)
            {
                particle.SetActive(true);
                isParticle = true;
            }
            else if (time <= 1)
            {
                transform.localScale = Vector3.one * (2 - time);
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
