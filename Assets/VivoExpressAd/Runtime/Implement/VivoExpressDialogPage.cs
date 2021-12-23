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

        public bool isReady()
        {
            return view != null;
        }
        public override void Initialize()
        {
            base.Initialize();
            listener._onClose += (v) => onClose?.Invoke(true);
        }
    }
}
