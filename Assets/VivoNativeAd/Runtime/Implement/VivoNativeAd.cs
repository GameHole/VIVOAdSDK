using MiniGameSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using AndroidNativeProxy;
namespace VivoAdSdk
{
    public abstract class VivoNativeAd : IInitializable, IReloader
    {
        protected static AndroidJavaClass helper;
        public static VivoNativeAdSetting setting = AScriptableObject.Get<VivoNativeAdSetting>();

        public IRetryer tryer;

        protected FrameLayout layout;

        AndroidJavaObject ad;
        List<AndroidJavaObject> resps;
        NativeAdListener listener;
        
        public int RetryCount => 5;

        public int IdCount => Ids.Length;

        public Action<bool> onReloaded { get; set; }

        public virtual void Initialize()
        {
            InitHelper();
            layout = new FrameLayout();
            layout.JoinToRenderCurrentActivity(new FrameLayout.LayoutParams(WHParams.MATCH_PARENT, WHParams.MATCH_PARENT));
            listener = NewListener();
            listener._onLoaded += (list) =>
            {
                resps = list;
                onReloaded?.Invoke(true);
            };
            listener._onError += (e) =>
            {
                onReloaded?.Invoke(false);
            };
            tryer?.Regist(this);
        }
        void InitHelper()
        {
            if (helper == null)
            {
                helper = new AndroidJavaClass("com.ad.vivo.VivoNativeHelper");
                helper.CallStatic("Init", ActivityGeter.GetActivity());
            }
        }
        protected virtual NativeAdListener NewListener() => new NativeAdListener(GetType().Name);
        public virtual void Reload(int id)
        {
            if (Ids == null) return;
            var builder = new AndroidJavaObject("com.vivo.mobilead.nativead.NativeAdParams$Builder", Ids[id]);
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.nativead.VivoNativeAd", ActivityGeter.GetActivity(), builder.Call<AndroidJavaObject>("build"), listener);
                ad.Call("loadAd");
            });
        }
        public virtual void Show()
        {
            layout.RemoveAllViews();
            var resp = FindFirst();
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ShowInternal(resp);
            });
        }
        AndroidJavaObject FindFirst()
        {
            return resps == null ? null : resps[0];
        }
        protected abstract string[] Ids { get; }
        protected abstract void ShowInternal(AndroidJavaObject resp);
    }
}
