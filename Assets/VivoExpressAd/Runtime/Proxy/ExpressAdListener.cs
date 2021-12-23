using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class ExpressAdListener : AndroidJavaProxy
    {
        public Action<AndroidJavaObject> _onReady;
        public Action<AndroidJavaObject> _onError;
        public Action<AndroidJavaObject> _onClick;
        public Action<AndroidJavaObject> _onShow;
        public Action<AndroidJavaObject> _onClose;
        string name;
        public ExpressAdListener(string name) : base("com.vivo.mobilead.unified.nativead.UnifiedVivoNativeExpressAdListener")
        {
            this.name = name;
        }
        void Log(string func)
        {
            Debug.Log($"express ad (impl = {name}) {func}");
        }
        public void onAdReady(AndroidJavaObject view)
        {
            _onReady?.Invoke(view);
            Log("onAdReady");
        }

        public void onAdFailed(AndroidJavaObject adError)
        {
            _onError?.Invoke(adError);
            Log($"onAdFailed {adError.Call<string>("toString")}");
        }

        public void onAdClick(AndroidJavaObject view)
        {
            _onClick?.Invoke(view);
            Log("onAdClick");
        }

        public void onAdShow(AndroidJavaObject view)
        {
            _onShow?.Invoke(view);
            Log("onAdShow");
        }

        public void onAdClose(AndroidJavaObject view)
        {
            _onClose?.Invoke(view);
            Log("onAdClose");
        }
    }
}
