using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    public class VivoSplash : ISplashAd,IInitializable
    {
        SplashProxy callback;
        internal AndroidJavaObject act;
        public Action OnClsoe { get; set; }
        int idx;
        public void Initialize()
        {
            callback = new SplashProxy() { owener = this };
        }
        AndroidJavaObject GetPama(string id)
        {
            string dir = "ORIENTATION_PORTRAIT";
            if (Screen.width > Screen.height)
                dir = "ORIENTATION_LANDSCAPE";
            var bd = new AndroidJavaObject("com.vivo.mobilead.splash.SplashAdParams$Builder", id);
            bd.Call<AndroidJavaObject>("setFetchTimeout", 3000);
            bd.Call<AndroidJavaObject>("setAppTitle", "广告联盟");
            bd.Call<AndroidJavaObject>("setSplashOrientation",new AndroidJavaClass("com.vivo.mobilead.splash.SplashAdParams").GetStatic<int>(dir));
            return bd.Call<AndroidJavaObject>("build");
        }
        public void Show()
        {
            if (PlatfotmHelper.isEditor()) return;
            var adids = SettingHelper.adsetting.splashId;
            var len = adids.Length;
            AndroidJavaObject pama = GetPama(adids[idx++ % len]);
            ActivityHelper.CreateEmpty((act) =>
            {
                this.act = act;
                AndroidHelper.PostToAndroidUIThread(() =>
                {
                    act = new AndroidJavaObject("com.vivo.mobilead.splash.VivoSplashAd", this.act, callback, pama);
                    act.Call("loadAd");
                });
            });
           
        }
    }
}
