using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class NativeAdListener : AndroidJavaProxy
    {
        public Action<List<AndroidJavaObject>> _onLoaded;
        public Action<AndroidJavaObject> _onError;
        public Action<AndroidJavaObject> _onClick;
        public Action<AndroidJavaObject> _onShow;
        string name;
        public NativeAdListener(string name) : base("com.vivo.ad.nativead.NativeAdListener")
        {
            this.name = name;
        }
        void Log(string func)
        {
            Debug.Log($"native ad (impl = {name}) {func}");
        }
        public void onADLoaded(AndroidJavaObject responseList)
        {
            if (responseList != null)
            {
                var list = new List<AndroidJavaObject>();
                for (int i = 0, c = responseList.Call<int>("size"); i < c; i++)
                {
                    var v = responseList.Call<AndroidJavaObject>("get", i);
                    if (v != null)
                        list.Add(v);
                }
                if (list.Count > 0)
                {
                    _onLoaded?.Invoke(list);
                    Log("onADLoaded");
                }
                else
                {
                    _onError?.Invoke(null);
                }
            }
            else
            {
                _onError?.Invoke(null);
            }
        }

        public void onNoAD(AndroidJavaObject adError)
        {
            _onError?.Invoke(adError);
            Debug.LogError($"NativeAd onAdError {adError.Call<string>("toString")}");
        }

        public void onClick(AndroidJavaObject nativeResponse)
        {
            _onClick?.Invoke(nativeResponse);
            Log("onClick");
        }

        public void onAdShow(AndroidJavaObject nativeResponse)
        {
            _onShow?.Invoke(nativeResponse);
            Log("onAdShow");
        }
    }
}
