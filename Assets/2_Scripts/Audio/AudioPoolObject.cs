using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolObject : MonoBehaviour
{
    readonly string _methodName = "ReturnThis";

    public void Init()
    {
        Invoke(_methodName, transform.GetComponent<AudioSource>().clip.length * 3);
        //StartCoroutine(AudioCoroutine(transform.GetComponent<AudioSource>().clip.length));
    }

    WaitForSecondsRealtime ReturnDelay(float clipLength)
    {
        Debug.Log(clipLength);

        if(clipLength < 1)
        {
            return PublicDefined._1secRealDelay;
        }
        else if(clipLength < 5)
        {
            return PublicDefined._5secRealDelay;
        }
        else
        {
            return PublicDefined._10secRealDelay;
        }
    }

    IEnumerator AudioCoroutine(float clipLength)
    {
        yield return ReturnDelay(clipLength);
        ReturnThis();
        yield return null;
    }

    public void ReturnThis()
    {
        AudioManager.INSTANCE.ReturnEffectObject(gameObject);
    }
}
