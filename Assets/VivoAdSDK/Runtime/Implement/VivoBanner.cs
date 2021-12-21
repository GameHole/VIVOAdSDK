using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;
namespace VivoAdSdk
{
    public class VivoBanner : IBannerAd,IInitializable,IReloader
    {
        IRetryer retryer;
        IBannerSetting setting;
        FrameLayout layout;
        AndroidJavaObject ad;
        AndroidJavaObject view;
        public UnifiedVivoBannerProxy proxy;
        public Action<int> onClose { get; set; }
        public Action onHide { get; set; }

        public int RetryCount => 2;

        public int IdCount => SettingHelper.adsetting.bannerId.Length;

        public Action<bool> onReloaded { get; set; }

        public event Action onShow;

        public void Hide()
        {
            layout.RemoveAllViews();
            onHide?.Invoke();
        }
        void Release()
        {
            ad?.Call("destroy");
            ad?.Dispose();
        }
        public void Initialize()
        {
            layout = new FrameLayout();
            layout.JoinToRenderCurrentActivity(new FrameLayout.LayoutParams(WHParams.MATCH_PARENT, WHParams.MATCH_PARENT));
            proxy = new UnifiedVivoBannerProxy() { reloader = this };
            proxy._onAdClose += () =>
            {
                onClose?.Invoke(0);
            };
            proxy._onAdReady += (v) =>
            {
                view = v as AndroidJavaObject;
            };
            proxy._onAdShow += onShow;
            retryer?.Regist(this);
        }
        AndroidJavaObject CreateAdParams(string id)
        {
            var adParams = new AdParams();
            adParams.positionId = id;
            adParams.refreshIntervalSeconds = SettingHelper.adsetting.bannerReflshTime;
            return adParams.ToNative();
        }
        public void Reload(int id)
        {
            Release();
            var pama = CreateAdParams(SettingHelper.adsetting.bannerId[id]);
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.unified.banner.UnifiedVivoBannerAd", ActivityGeter.GetActivity(), pama, proxy);
                ad.Call("loadAd");
            });
        }

        public void Show()
        {
            if (view != null)
            {
                layout.RemoveAllViews();
                var pama = new FrameLayout.LayoutParams(setting.Size.x, setting.Size.y, setting.gravity);
                layout.AddView(view, pama);
            }
        }
    }
}
