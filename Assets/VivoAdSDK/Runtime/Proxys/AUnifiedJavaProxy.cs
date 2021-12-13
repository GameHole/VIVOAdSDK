using MiniGameSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class AUnifiedJavaProxy : AndroidJavaProxy
    {
        public IReloader reloader;
        string typeName;
        public string extraLogInfo;
        public Action<object> _onAdReady;
        public Action _onAdClick;
        public Action<AndroidJavaObject> _onAdFailed;
        public Action _onAdShow;
        public Action _onAdClose;
        public bool isReady;
        public AUnifiedJavaProxy(string javaInterface) : base(javaInterface)
        {
            int last= javaInterface.LastIndexOf('.')+1;
            typeName = javaInterface.Substring(last, javaInterface.Length - last).Replace("Listener","");
            _onAdReady += (v) =>
            {
                isReady = true;
                reloader?.onReloaded?.Invoke(true);
            };
            _onAdClose += () =>
            {
                reloader?.onReloaded?.Invoke(false);
            };
            _onAdFailed += (v) =>
            {
                reloader?.onReloaded?.Invoke(false);
            };
        }
        protected void Log(string func)
        {
            Debug.Log($"{typeName}{extraLogInfo} {func}");
        }
        public void onAdFailed(AndroidJavaObject error)
        {
            isReady = false;
            _onAdFailed?.Invoke(error);
            Log("onAdFailed " + error.Call<string>("toString"));
        }

        public void onAdClick()
        {
            _onAdClick?.Invoke();
            Log("onAdClick");
        }

        public void onAdShow()
        {
            _onAdShow?.Invoke();
            Log("onAdShow");
        }

        public void onAdClose()
        {
            isReady = false;
            _onAdClose?.Invoke();
            Log("onAdClose");
        }
    }
}
