using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
	public class SettingHelper
	{
        private static VivoAdSetting _setting;
        public static VivoAdSetting adsetting
        {
            get
            {
                return _setting ?? (_setting = AScriptableObject.Get<VivoAdSetting>());
            }
        }
        public static AndroidJavaObject CreateAdParams(string id)
        {
            try
            {
                var ad = new AdParams();
                ad.positionId = id;
                return ad.ToNative();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
	}
}
