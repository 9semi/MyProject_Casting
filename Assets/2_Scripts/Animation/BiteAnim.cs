using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAnim : MonoBehaviour
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
        if(time > 4)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (time > 2)
        {
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (time > 0)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        time += Time.deltaTime;
    }

    public void OnReset()
    {
        time = 0;
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
