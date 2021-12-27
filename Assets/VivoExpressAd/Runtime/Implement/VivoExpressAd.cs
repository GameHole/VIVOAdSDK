using MiniGameSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using AndroidNativeProxy;
namespace VivoAdSdk
{
    public abstract class VivoExpressAd : IInitializable, IReloader
    {
        public static VivoExpressAdSetting setting = AScriptableObject.Get<VivoExpressAdSetting>();

        public IRetryer tryer;

        protected FrameLayout layout;

        AndroidJavaObject ad;
        protected AndroidJavaObject view;
        protected ExpressAdListener listener;
        
        public int RetryCount => 5;

        public int IdCount => Ids.Length;

        public Action<bool> onReloaded { get; set; }

        public virtual void Initialize()
        {
            layout = new FrameLayout();
            layout.JoinToRenderCurrentActivity(new FrameLayout.LayoutParams(WHParams.MATCH_PARENT, WHParams.MATCH_PARENT));
            listener = NewListener();
            listener._onReady += (view) =>
            {
                this.view = view;
                onReloaded?.Invoke(true);
            };
            listener._onError += (e) =>
            {
                onReloaded?.Invoke(false);
            };
            listener._onClose += (v) =>
            {
                ReleaseAndReload();
            };
            tryer?.Regist(this);
        }
        protected virtual ExpressAdListener NewListener() => new ExpressAdListener(GetType().Name);
        public virtual void Reload(int id)
        {
            if (Ids == null) return;
            Release();
            var pama = new AdParams();
            pama.positionId = Ids[id];
            SetExtraInfo(pama);
            AndroidHelper.PostToAndroidUIThread(() =>
            {
                ad = new AndroidJavaObject("com.vivo.mobilead.unified.nativead.UnifiedVivoNativeExpressAd", getAct(), pama.ToNative(), listener);
                ad.Call("loadAd");
            });
        }
        protected virtual AndroidJavaObject getAct() => ActivityGeter.GetActivity();
        protected virtual void SetExtraInfo(AdParams adParams) { }
        public virtual void Show()
        {
            layout.RemoveAllViews();
            if (view != null)
            {
                layout.AddView(view, GetParams());
            }
        }
        protected virtual FrameLayout.LayoutParams GetParams()
        {
            return new FrameLayout.LayoutParams(WHParams.WRAP_CONTENT, WHParams.WRAP_CONTENT,Gravity.CENTER);
        }
        protected void ReleaseAndReload()
        {
            Release();
            onReloaded?.Invoke(false);
        }
        protected void Release()
        {
            layout.RemoveAllViews();
            if (view != null)
            {
                AndroidHelper.PostToAndroidUIThread(() =>
                {
                    view.Call("destroy");
                    view.Dispose();
                    view = null;
                });
            }
        }
        protected abstract string[] Ids { get; }
    }
}
