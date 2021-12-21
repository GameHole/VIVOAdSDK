using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    public class VivoNativeInterstitial : VivoNativeAd, IDialogPageAd
    {
        ClickListenerProxy proxy;
        protected override string[] Ids => setting.pageDialogId;

        public event Action<bool> onClose;

        public bool isReady()
        {
            return true;
        }
        public override void Initialize()
        {
            base.Initialize();
            proxy = new ClickListenerProxy((v)=>
            {
                onClose?.Invoke(true);
            });
        }
        protected override void ShowInternal(AndroidJavaObject resp)
        {
            var view = helper.CallStatic<AndroidJavaObject>("ShowInter", resp, proxy);
            var layoutInfo = new FrameLayout.LayoutParams(WHParams.MATCH_PARENT, WHParams.MATCH_PARENT);
            layout.AddView(view, layoutInfo);
        }
    }
}
