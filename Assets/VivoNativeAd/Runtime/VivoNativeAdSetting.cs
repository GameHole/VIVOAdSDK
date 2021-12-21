using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoNativeAdSetting : AScriptableObject
    {
        public override string filePath => "Vivo/NativeAdSettings";
        public bool bannerAutoChange;
        public float changeDuration = 20;
        public string[] bannerId;
        public string[] pageDialogId;
    }
}
