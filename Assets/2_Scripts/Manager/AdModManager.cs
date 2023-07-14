using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class AdModManager : MonoBehaviour
{
    InterstitialAd interstitial;
    Action _action;

    private void Start()
    {
//#if UNITY_ANDROID
//        string appID = "ca-app-pub-8936705062968000~4115193263";
//#else
//        string appID = "unexpected_platform";
//#endif

        MobileAds.Initialize(initStatus => { });
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-8936705062968000/6390002814";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowAD(Action action)
    {
        RequestInterstitial();

        _action = action;

        StartCoroutine(ShowADCoroutine());
    }

    IEnumerator ShowADCoroutine()
    {
        Debug.Log(interstitial);

        while (!interstitial.IsLoaded())
        {
            Debug.Log(interstitial.IsLoaded());
            yield return PublicDefined._02secDelay;
        }

        interstitial.Show();
    }

    // 광고 종료 후 실행되는 코드
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        _action();
    }
}
