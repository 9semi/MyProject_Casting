using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnim : MonoBehaviour
{
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0.1f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (time <= 0.2f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (time <= 0.3f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (time <= 0.4f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (time <= 0.5f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (time <= 0.6f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (time <= 0.7f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (time <= 0.8f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (time <= 0.9f)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (time > 1)
        {
            time = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
    }
}
