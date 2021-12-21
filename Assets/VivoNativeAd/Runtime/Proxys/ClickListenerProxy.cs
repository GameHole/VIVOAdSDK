using System;
using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class ClickListenerProxy : AndroidJavaProxy
    {
        Action<AndroidJavaObject> _onClick;
        public ClickListenerProxy(Action<AndroidJavaObject> onClick) : base("android.view.View$OnClickListener")
        {
            _onClick = onClick;
        }
        public void onClick(AndroidJavaObject view)
        {
            _onClick?.Invoke(view);
        }
    }
}
