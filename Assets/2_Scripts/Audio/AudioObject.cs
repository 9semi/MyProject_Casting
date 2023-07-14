using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    readonly string _methodName = "ReturnThis";
    readonly float _multiply = 1.5f;
    private void OnEnable()
    {
        Invoke(_methodName, GetComponent<AudioSource>().clip.length * _multiply);

    }
    public void ReturnThis()
    {
        AudioManager.INSTANCE.ReturnEffectObject(gameObject);
    }
}
