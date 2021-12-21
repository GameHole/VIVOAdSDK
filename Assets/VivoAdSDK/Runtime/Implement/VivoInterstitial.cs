using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    public abstract class AVivoInterstitial:IReloader,IInitializable
    {
        protected IRetryer retryer;
        UnifiedVivoInterstitialProxy proxy;
        protected AndroidJavaObject ad;
        protected abstract string loadFuncName { get; }
        //protected abstract string showFuncName { get; }
        protected abstract string[] Ids { get; }
        public int RetryCount => 5;

        public int IdCount => Ids.Length;

        public Action<bool> onReloaded { get; set; }

        public event Action<bool> onClose;

        public void Initialize()
        {
            proxy = new UnifiedVivoInterstitialProxy() { reloader = this };
            proxy.extraLogInfo = " local impl = " + GetType().Name;
            proxy._onAdClose += () =>
            {
                onClose?.Invoke(true);
            };
            retryer?.Regist(this);
        }

        public bool isReady()
        {
            return proxy.isReady;
        }

        public void Reload(int id)
        {
            if (PlatfotmHelper.isEditor()) return;
            ad?.Dispose();
            var pama = SettingHelper.CreateAdParams(Ids[id]);
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.unified.interstitial.UnifiedVivoInterstitialAd", ActivityGeter.GetActivity(), pama, proxy);
                ad.Call(loadFuncName);
            });
        }
       
        public void Show()
        {
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ShowInternal();
            });
        }
        protected abstract void ShowInternal();
    }
    class VivoInterstitial : AVivoInterstitial, IInterstitialAdAPI
    {
        protected override string loadFuncName => "loadVideoAd";

        //protected override string showFuncName => "showVideoAd";

        protected override string[] Ids => SettingHelper.adsetting.interstitialId;

        protected override void ShowInternal()
        {
            ad?.Call("showVideoAd", ActivityGeter.GetActivity());
        }
    }
}
