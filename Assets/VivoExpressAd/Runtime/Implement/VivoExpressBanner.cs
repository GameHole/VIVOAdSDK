using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    //[Refinter.Important(500)]
    public class VivoExpressBanner : VivoExpressAd, IBannerAd
    {
        IBannerSetting bannerSetting;
        public Action<int> onClose { get; set; }
        public Action onHide { get; set; }

        protected override string[] Ids => setting.bannerId;

        public event Action onShow;

        public void Hide()
        {
            ReleaseAndReload();
            onHide?.Invoke();
        }
        public override void Initialize()
        {
            base.Initialize();
            bannerSetting = Refinter.Reflection.Get<IBannerSetting>();
            listener._onShow += (v) => onShow?.Invoke();
        }
        protected override FrameLayout.LayoutParams GetParams()
        {
            return new FrameLayout.LayoutParams(WHParams.WRAP_CONTENT, WHParams.WRAP_CONTENT, bannerSetting.gravity);
        }
        protected override void SetExtraInfo(AdParams adParams)
        {
            adParams.nativeExpressWidth = bannerSetting.Size.x;
            adParams.nativeExpressHegiht = bannerSetting.Size.y;
            adParams.refreshIntervalSeconds = SettingHelper.adsetting.bannerReflshTime;
        }
    }
}
