using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteFailAnim : MonoBehaviour
{
    [SerializeField]
    //private GameObject particle;
    private float time;
    private bool isParticle;

    // Start is called before the first frame update
    void OnEable()
    {
        time = 0;
        isParticle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isParticle)
        {
            isParticle = true;
            //particle.SetActive(true);
        }
        if (time > 2)
        {
            time = 0;
            isParticle = false;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }
}
