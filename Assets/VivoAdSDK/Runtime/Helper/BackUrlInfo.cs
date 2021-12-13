using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public interface IJavaProxy
    {
        AndroidJavaObject ToNative();
    }
    public class BackUrlInfo: IJavaProxy
    {
        public string backUrl;
        public string btnName;

        public BackUrlInfo(string backUrl, string btnName)
        {
            this.backUrl = backUrl;
            this.btnName = btnName;
        }

        public AndroidJavaObject ToNative()
        {
            return new AndroidJavaObject("com.vivo.mobilead.model.BackUrlInfo", backUrl, btnName);
        }
    }
}
