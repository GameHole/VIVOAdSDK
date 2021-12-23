using AndroidNativeProxy;
using MiniGameSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoIconAd : IFloatIconAd,IInitializable,IReloader
    {
        IRetryer retryer;
        AndroidJavaObject ad;
        UnifiedVivoFloatIconProxy proxy;
        public Action onClose { get; set; }

        public int RetryCount => 5;

        public int IdCount => SettingHelper.adsetting.floatIcon.Length;

        public Action<bool> onReloaded { get; set; }
        public Vector2Int screenPosition { get; set; }

        public void Hide()
        {
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad?.Call("destroy");
                ad?.Dispose();
            });
            retryer.Load(this);
        }

        public void Initialize()
        {
            proxy = new UnifiedVivoFloatIconProxy() { reloader = this };
            proxy._onAdClose += () => onClose?.Invoke();
            retryer.Regist(this);

        }

        public void Show()
        {
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad?.Call("showAd", ActivityGeter.GetActivity(), screenPosition.x, screenPosition.y);
            });
        }

        public void Reload(int id)
        {
            var pama = SettingHelper.CreateAdParams(SettingHelper.adsetting.floatIcon[id]);
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.unified.icon.UnifiedVivoFloatIconAd", ActivityGeter.GetActivity(), pama, proxy);
                ad.Call("loadAd");
            });
        }
    }
}
