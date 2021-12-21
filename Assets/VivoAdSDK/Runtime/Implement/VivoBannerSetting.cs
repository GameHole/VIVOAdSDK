using System.Collections.Generic;
using UnityEngine;
using AndroidNativeProxy;
using MiniGameSDK;

namespace VivoAdSdk
{
    public class VivoBannerSetting : IBannerSetting
    {
        public Vector2Int Size { get; set; } = new Vector2Int(600, 90);
        public int gravity { get; set; } = Gravity.BOTTOM | Gravity.CENTER;
    }
}
