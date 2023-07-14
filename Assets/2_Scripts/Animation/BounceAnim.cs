using System.Collections;
using System.Collections.Generic;
//using System.Xml.Serialization.Advanced;
using UnityEngine;

public class BounceAnim : MonoBehaviour
{
    private float time;
    private float size;
    private float upSizeTime;
    private int timeTemp;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        size = 5;
        upSizeTime = 0.3f;
        timeTemp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= upSizeTime)
        {
            transform.localScale = Vector3.one * (1 + size * time);
        }
        else if (time <= upSizeTime * 2)
        {
            transform.localScale = Vector3.one * (2 * size * upSizeTime + 1 - time * size);
        }
        if (time > 0.6)
        {
            time = 0;
            timeTemp++;
        }
        if (timeTemp >= 10)
        {
            ActiveFalse();
        }
        time += Time.deltaTime;
    }

    public void ActiveFalse()
    {
        time = 0;
        size = 5;
        upSizeTime = 0.3f;
        timeTemp = 0;
        gameObject.SetActive(false);
    }
}
