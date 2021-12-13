using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace VivoAdSdk
{
    public class GradleSetter : IParamSettng
    {
        public void SetParam()
        {
            var mgd = GradleHelper.Open( GradleType.Basic);
            var ddpn= mgd.Root.FindNode("buildscript/dependencies");
            if (ddpn != null)
            {
                var od = ddpn.FindValue("classpath 'com.android.tools.build:gradle:3.4.0'");
                if (od != null)
                {
                    od.str = "classpath 'com.android.tools.build:gradle:3.4.3'";
                    mgd.Save();
                }
            }
            var gd = GradleHelper.Open();
            gd.SetImplementation("implementation 'com.android.support:support-v4:28.0.0'");
            gd.SetImplementation("implementation 'com.android.support:recyclerview-v7:28.0.0'");
            gd.SetImplementation("implementation 'com.android.support:appcompat-v7:28.0.0'");
            var dcfg = gd.Root.FindNode("android/defaultConfig");
            dcfg?.InsertValue(0, "multiDexEnabled true");
            gd.Save();
        }
    }
}
