using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace VivoAdSdk
{
    public class GradleSetter : IParamSettng
    {
        public void SetParam()
        {
            var mgd = GradleHelper.Open(GradleType.Basic);
            var od = mgd.Root.FindValue((v) =>
            {
                if (!string.IsNullOrEmpty(v.str))
                {
                    if (v.str.Contains("com.android.tools.build:gradle"))
                    {
                        return true;
                    }
                }
                return false;
            });
            if (od != null)
            {
                od.str = "classpath 'com.android.tools.build:gradle:3.4.3'";
                mgd.Save();
            }
            var rps= mgd.Root.FindNode("allprojects/repositories");
            var node = new GradleHelper.Node("maven");
            node.AddValue("url 'http://localhost:8081/nexus/content/repositories/renrundong/'");
            rps.Add(node);
            mgd.Save();
            var gd = GradleHelper.Open();
            gd.SetImplementation("implementation 'com.android.support:support-v4:28.0.0'");
            gd.SetImplementation("implementation 'com.android.support:recyclerview-v7:28.0.0'");
            gd.SetImplementation("implementation 'com.android.support:appcompat-v7:28.0.0'");
            gd.SetImplementation("implementation 'com.squareup.picasso:picasso:2.5.2'");
            //gd.SetImplementation("implementation 'com.unity.androidapi:nativeview:1.0.0'");
            SetDex(gd);
            SetDex(GradleHelper.Open(GradleType.Luncher));
        }
        static void SetDex(GradleHelper.Gradle gd)
        {
            var dcfg = gd.Root.FindNode("android/defaultConfig");
            dcfg?.InsertValue(0, "multiDexEnabled true");
            gd.Save();
        }
    }
}
