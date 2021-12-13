using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace VivoAdSdk
{
    public class VivoSplash : ISplashAd,IInitializable
    {
        class Callback : AndroidJavaProxy
        {
            public VivoSplash owener;
            public Callback() : base("com.vivo.ad.splash.SplashAdListener")
            {
            }
            public void onADDismissed()
            {
                Debug.Log("VivoSplash onADDismissed");
            }
            public void onNoAD(AndroidJavaObject error)
            {
                owener.ad?.Call("close");
                Debug.Log($"VivoSplash onNoAD code::{error.Call<int>("getErrorCode")} msg::{error.Call<string>("getErrorMsg")}" );
            }
            public void onADPresent()
            {
                Debug.Log("VivoSplash onADPresent");
            }
            public void onADClicked()
            {
                Debug.Log("VivoSplash onADClicked");
            }
        }

        Callback callback;
        AndroidJavaObject ad;
        public Action OnClsoe { get; set; }
        int idx;
        public void Initialize()
        {
            callback = new Callback() { owener = this };
        }
        AndroidJavaObject GetPama(string id)
        {
            var bd = new AndroidJavaObject("com.vivo.mobilead.splash.SplashAdParams$Builder", id);
            bd.Call<AndroidJavaObject>("setFetchTimeout", 3000);
            bd.Call<AndroidJavaObject>("setAppTitle", "广告联盟");
            return bd.Call<AndroidJavaObject>("build");
        }
        public void Show()
        {
            if (PlatfotmHelper.isEditor()) return;
            var adids = SettingHelper.adsetting.splashId;
            var len = adids.Length;
            AndroidJavaObject pama = GetPama(adids[idx++ % len]);
            PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.splash.VivoSplashAd", ActivityGeter.GetActivity(), callback, pama);
                ad.Call("loadAd");
                Debug.Log("aa");
            });
        }
    }
}
