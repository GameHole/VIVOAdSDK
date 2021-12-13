using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace VivoAdSdk
{
    public class VivoDialogPage : AVivoInterstitial, IDialogPageAd
    {
        protected override string loadFuncName => "loadAd";

        //protected override string showFuncName => "showAd";

        protected override string[] Ids => SettingHelper.adsetting.pageDialogId;

        protected override void ShowInternal()
        {
            ad?.Call("showAd");
        }
    }
}
