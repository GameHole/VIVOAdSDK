using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    public class VivoNativeBanner : VivoNativeAd,IBannerAd
    {
        NativeBannerAutoChange autoChanger;
        ClickListenerProxy proxy;
        IBannerSetting bannerSetting;
        public Action<int> onClose { get; set; }
        public Action onHide { get; set; }

        protected override string[] Ids=>setting.bannerId;

        public event Action onShow;

        public void Hide()
        {
            layout.RemoveAllViews();
            onHide?.Invoke();
        }
        public override void Initialize()
        {
            base.Initialize();
            bannerSetting = Refinter.Reflection.Get<IBannerSetting>();
            proxy = new ClickListenerProxy((v) =>
            {
                onClose?.Invoke(0);
            });
            autoChanger = new NativeBannerAutoChange() { bannerAd = this, changeDuration = setting.changeDuration };
            if (setting.bannerAutoChange)
            {
                autoChanger.Start();
            }
        }
        //For Test
        //public override void Reload(int id)
        //{
        //    Debug.Log("load native banner");
        //    onReloaded?.Invoke(true);
        //}
        protected override void ShowInternal(AndroidJavaObject resp)
        {
            //Debug.Log("banner show");
            if (resp != null)
            {
                var view = helper.CallStatic<AndroidJavaObject>("Showbanner", resp, proxy);
                var layoutInfo = new FrameLayout.LayoutParams(bannerSetting.Size.x, bannerSetting.Size.y, bannerSetting.gravity);
                layout.AddView(view, layoutInfo);
            }
            onShow?.Invoke();
        }
    }
}
