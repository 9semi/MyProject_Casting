using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    readonly string _methodName = "ReturnThis";
    AquariumEffectManager AE;

    public void Play(AquariumEffectManager instance)
    {
        AE = instance;

        Invoke(_methodName, 1.5f);
    }

    public void ReturnThis()
    {
        AE.ReturnObject(gameObject);
    }
}
