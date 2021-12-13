using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace VivoAdSdk
{
    public class VivoReward : AReward,IInitializable,IReloader
    {
        IRetryer retryer;
        UnifiedVivoRewardProxy proxy;
        AndroidJavaObject ad;
        bool isReward;

        public int RetryCount => 5;

        public int IdCount => SettingHelper.adsetting.rewardId.Length;

        public Action<bool> onReloaded { get; set; }

        public void Initialize()
        {
            proxy = new UnifiedVivoRewardProxy() { reloader = this };
            retryer.Regist(this);
            proxy._onAdClose += () =>
            {
                OnClose(isReward);
            };
            proxy._onRewardVerify += () =>
            {
                isReward = true;
            };
        }

        public override bool isReady()
        {
            return proxy.isReady;
        }

        public void Reload(int id)
        {
            ad?.Dispose();
            var set = SettingHelper.adsetting.rewardId[id];
            var pama = SettingHelper.CreateAdParams(set);
            PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                 ad = new AndroidJavaObject("com.vivo.mobilead.unified.reward.UnifiedVivoRewardVideoAd", ActivityGeter.GetActivity(), pama, proxy);
                 ad.Call("loadAd");
            });
        }

        protected override void Show()
        {
            isReward = false;
            if (isReady())
            {
                PlatfotmHelper.PostToAndroidUIThread(() =>
                {
                    ad.Call("showAd", ActivityGeter.GetActivity());
                });
            }
        }
    }
}
