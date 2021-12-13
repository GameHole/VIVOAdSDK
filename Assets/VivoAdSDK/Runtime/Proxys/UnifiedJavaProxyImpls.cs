using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class UnifiedVivoNormalProxy : AUnifiedJavaProxy
    {
        public UnifiedVivoNormalProxy(string javaInterface) : base(javaInterface)
        { }
        public void onAdReady()
        {
            _onAdReady?.Invoke(null);
            Log("onAdReady");
        }
    }
    public class UnifiedVivoInterstitialProxy : AUnifiedJavaProxy
    {
        public UnifiedVivoInterstitialProxy() : base("com.vivo.mobilead.unified.interstitial.UnifiedVivoInterstitialAdListener")
        { }
        public void onAdReady(bool hasCache)
        {
            _onAdReady?.Invoke(hasCache);
            Log("onAdReady");
        }
    }
    public class UnifiedVivoBannerProxy : AUnifiedJavaProxy
    {
        public UnifiedVivoBannerProxy() : base("com.vivo.mobilead.unified.banner.UnifiedVivoBannerAdListener")
        { }
        public void onAdReady(AndroidJavaObject adView)
        {
            _onAdReady?.Invoke(adView);
            Log("onAdReady");
        }
    }
    public class UnifiedVivoRewardProxy : UnifiedVivoNormalProxy
    {
        public Action _onRewardVerify;
        public UnifiedVivoRewardProxy() : base("com.vivo.mobilead.unified.reward.UnifiedVivoRewardVideoAdListener")
        {
        }
        public void onRewardVerify()
        {
            _onRewardVerify?.Invoke();
            Log("onRewardVerify");
        }
    }
    public class UnifiedVivoFloatIconProxy : UnifiedVivoNormalProxy
    {
        public UnifiedVivoFloatIconProxy() : base("com.vivo.mobilead.unified.icon.UnifiedVivoFloatIconAdListener")
        { }
    }
}
