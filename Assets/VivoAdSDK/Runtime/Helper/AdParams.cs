using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class AdParams: IJavaProxy
    {
        public static readonly int ORIENTATION_LANDSCAPE = 2;
        public static readonly int ORIENTATION_PORTRAIT = 1;
        public string positionId;
        public BackUrlInfo backUrlInfo = new BackUrlInfo("vivobrowser://browser.vivo.com?i=12", "test");
        public int fetchTimeout;
        public string appTitle;
        public string appDesc;
        public AndroidJavaObject customView;
        public string gameId;
        public int splashOrientation = 1;
        public int refreshIntervalSeconds;
        public int videoPolicy = 0;
        public int nativeExpressWidth = -1;
        public int nativeExpressHegiht = -1;
        public AndroidJavaObject ToNative()
        {
            if (MiniGameSDK.PlatfotmHelper.isEditor()) return null;
            AndroidJavaObject builder = new AndroidJavaObject("com.vivo.mobilead.unified.base.AdParams$Builder", positionId);
            builder.Call<AndroidJavaObject>("setBackUrlInfo", backUrlInfo.ToNative());
            builder.Call<AndroidJavaObject>("setGameId", gameId);
            builder.Call<AndroidJavaObject>("setAppTitle", appTitle);
            builder.Call<AndroidJavaObject>("setAppDesc", appDesc);
            builder.Call<AndroidJavaObject>("setCustomView", customView);
            builder.Call<AndroidJavaObject>("setSplashOrientation", splashOrientation);
            builder.Call<AndroidJavaObject>("setRefreshIntervalSeconds", refreshIntervalSeconds);
            builder.Call("setVideoPolicy", videoPolicy);
            builder.Call("setNativeExpressWidth", nativeExpressWidth);
            builder.Call("setNativeExpressHegiht", nativeExpressHegiht);
            return builder.Call<AndroidJavaObject>("build");
        }
    }
}
