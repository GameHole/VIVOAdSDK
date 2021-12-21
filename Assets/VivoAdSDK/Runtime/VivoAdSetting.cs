using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoAdSetting : AScriptableObject
    {
        public override string filePath => "Vivo/AdSettings";
        public bool showSplashOnAppInit;
        public int bannerReflshTime = 20;
        public string[] splashId;
        public string[] bannerId;
        public string[] rewardId;
        public string[] interstitialId;
        public string[] pageDialogId;
        public string[] floatIcon;
    }
}
