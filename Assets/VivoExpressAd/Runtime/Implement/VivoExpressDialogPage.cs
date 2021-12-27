using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    //[Refinter.Important(500)]
    public class VivoExpressDialogPage : VivoExpressAd, IDialogPageAd
    {
        protected override string[] Ids => setting.pageDialogId;

        public event Action<bool> onClose;
        AndroidJavaObject act;
        public bool isReady()
        {
            return view != null;
        }
        public override void Initialize()
        {
            base.Initialize();
            listener._onClose += (v) =>
            {
                onClose?.Invoke(true);
                act?.Call("finish");
            };
        }
        public override void Show()
        {
            layout = new FrameLayout();
            ActivityHelper.CreateEmpty((v) =>
            {
                act = v;
                layout.AddToContentView(v, new FrameLayout.LayoutParams(WHParams.MATCH_PARENT, WHParams.MATCH_PARENT));
                base.Show();
            });
        }
    }
}
