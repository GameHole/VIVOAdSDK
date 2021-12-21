using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    class SplashProxy : AndroidJavaProxy
    {
        public VivoSplash owener;
        public SplashProxy() : base("com.vivo.ad.splash.SplashAdListener")
        {
        }
        public void onADDismissed()
        {
            owener.act?.Call("finish");
            Debug.Log("VivoSplash onADDismissed");
        }
        public void onNoAD(AndroidJavaObject error)
        {
            owener.act?.Call("finish");
            Debug.Log($"VivoSplash onNoAD code::{error.Call<int>("getErrorCode")} msg::{error.Call<string>("getErrorMsg")}");
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
}
