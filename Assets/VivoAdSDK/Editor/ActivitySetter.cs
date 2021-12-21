using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using AndroidNativeProxy;

namespace VivoAdSdk
{
    public class ActivitySetter : IParamSettng
    {
        public void SetParam()
        {
            EmptyActivity.Apply();
        }
    }
}
