using System.Collections.Generic;
using UnityEngine;
namespace AndroidNativeProxy
{
    public class WHParams
    {
        public static readonly int MATCH_PARENT = -1;
        public static readonly int WRAP_CONTENT = -2;
    }
    public class FrameLayout
    {
        public class LayoutParams
        {
            internal AndroidJavaObject v;
            static readonly string proxyname = "android.widget.FrameLayout$LayoutParams";
            public LayoutParams(int width, int height)
            {
                v = new AndroidJavaObject(proxyname, width, height);
            }
            public LayoutParams(int width, int height, int gravity)
            {
                v = new AndroidJavaObject(proxyname, width, height, gravity);
            }
        }
        AndroidJavaObject act;
        AndroidJavaObject v;
        static readonly string proxyname = "android.widget.FrameLayout";
        public FrameLayout() : this(ActivityGeter.GetActivity()) { }
        public FrameLayout(AndroidJavaObject activity)
        {
            act = activity;
            v = new AndroidJavaObject(proxyname, activity);
        }
        public void AddView(AndroidJavaObject view, LayoutParams layout)
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                v.Call("addView", view, layout.v);
            });
        }
        public void AddView(AndroidJavaObject child)
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                v.Call("addView", child);
            });
        }
        public void AddView(AndroidJavaObject child, int index)
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                v.Call("addView", child, index);
            });
        }
        public void AddView(AndroidJavaObject child, int width, int height)
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                v.Call("addView", child, width, height);
            });
        }
        public void JoinToRenderCurrentActivity(LayoutParams layoutParams)
        {
            AddToContentView(act, layoutParams);
        }
        public void AddToContentView(AndroidJavaObject activity, LayoutParams layoutParams)
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                activity.Call("addContentView", v, layoutParams.v);
            });
        }
        public void RemoveAllViews()
        {
            MiniGameSDK.PlatfotmHelper.PostToAndroidUIThread(() =>
            {
                v.Call("removeAllViews");
            });
        }
    }
}
