using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageAnim : MonoBehaviour
{
    private float time;
    private int randomX;
    private bool isRand;
    private RectTransform myTr;
    private Text myText;    

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        isRand = false;
        myTr = GetComponent<RectTransform>();
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!isRand)
            {
                randomX = Random.Range(-20, 20);
                isRand = true;
            }
            myTr.anchoredPosition = new Vector2(myTr.anchoredPosition.x + randomX,
                myTr.anchoredPosition.y + 10);
            myText.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, time * 2));
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                isRand = false;
                time = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
