using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    float time;
    public float tempTime;
    // Start is called before the first frame update
    void Start()
    {
        tempTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            time += Time.deltaTime;
            if (time >= tempTime)
            {
                gameObject.SetActive(false);
                time = 0;
            }
        }
    }
}
