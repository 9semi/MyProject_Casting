using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;

public class AndroidPlugin : MonoBehaviour
{
    private AndroidJavaObject ajo;

    // 코틀린언어로 되어있는 단말기 진동기능가져옴
    private void Awake()=>    
        ajo = new AndroidJavaObject(className: "com.example.castingapplication.UnityVibration");

    public void Vibrate(long val)=>    
        ajo.Call(methodName: "Vibration", val);   
}
