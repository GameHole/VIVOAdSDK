using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System.Xml;
using UnityEditor;
namespace VivoAdSdk
{
    public static class XmlAppEx
    {
        public static XmlNode CreateActovity(this XmlDocument doc, string name, string screenOrientation = null, string hardwareAccelerated = null)
        {
            var act = doc.CreateElement("activity");
            act.CreateAttribute("name", name);
            act.CreateAttribute("configChanges", "orientation|keyboardHidden|screenSize");
            if (!string.IsNullOrEmpty(hardwareAccelerated))
            {
                act.CreateAttribute("hardwareAccelerated", "true");
            }
            if (!string.IsNullOrEmpty(screenOrientation))
            {
                act.CreateAttribute("screenOrientation", "unspecified");
            }
            return act;
        }
    }
    public class XmlSetter : IParamSettng
    {
       

        public void SetParam()
        {
            var doc = XmlHelper.GetAndroidManifest();
            var app = doc.SelectSingleNode("/manifest/application");
            var act = app.FindNode("activity", "android:name", "com.vivo.mobilead.web.VivoADSDKWebView");
            if (act == null)
            {
                app.CreateAttribute("hardwareAccelerated", "true");
                app.AppendChild(doc.CreateActovity("com.vivo.mobilead.web.VivoADSDKWebView"));
                app.AppendChild(doc.CreateActovity("com.vivo.mobilead.video.RewardVideoActivity", "true","true"));
                app.AppendChild(doc.CreateActovity("com.vivo.mobilead.unified.reward.RewardVideoActivity", "true", "true"));
                app.AppendChild(doc.CreateActovity("com.vivo.mobilead.unified.interstitial.InterstitialVideoActivity", "true", "true"));
               
                var pdr = doc.CreateElement("provider");
                pdr.CreateAttribute("authorities", "${applicationId}.vivoprovider");
                pdr.CreateAttribute("name", "com.vivo.mobilead.manager.VivoContentProvider");
                pdr.CreateAttribute("exported", "false");
                app.AppendChild(pdr);
                var meta = doc.CreateElement("meta-data");
                meta.CreateAttribute("name", "vivo_ad_version_code");
                meta.CreateAttribute("value", "5220");
                app.AppendChild(meta);
                doc.SetPermissions(
                   "android.permission.INTERNET",
                   "android.permission.ACCESS_NETWORK_STATE",
                   "android.permission.ACCESS_WIFI_STATE",
                   "android.permission.READ_PHONE_STATE",
                   "android.permission.WRITE_EXTERNAL_STORAGE",
                   "android.permission.REQUEST_INSTALL_PACKAGES",
                   "android.permission.WAKE_LOCK");
                doc.Save();
            }
        }
        
    }
}
