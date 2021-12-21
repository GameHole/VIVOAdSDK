using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace VivoAdSdk
{
    public class AssetSetter : IParamSettng
    {
        public void SetParam()
        {
            AssetHelper.CreateOrGetAsset<VivoNativeAdSetting>();
        }
    }
}
