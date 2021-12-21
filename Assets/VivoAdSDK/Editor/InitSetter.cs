using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using UnityEditor;
using System.IO;

namespace VivoAdSdk
{
    public class InitSetter : IParamSettng
    {
        public void SetParam()
        {
            SetJavaFile();
            AssetDatabase.Refresh();
        }
        public static void SetJavaFile()
        {
            var setting = AssetHelper.CreateOrGetAsset<VivoSetting>();
            var adsetting = AssetHelper.CreateOrGetAsset<VivoAdSetting>();
            CopyAndRegist("7d2aa4c49a60e5c49aa04be1dd144eea",//Editor/templete/VivoAdInitor.txt
                new KeyValuePair<string, string>("##APPID##", setting.mediaID),
                new KeyValuePair<string, string>("##DEBUG##", setting.isDebug.ToString().ToLower()));
            if (adsetting.splashId != null && adsetting.splashId.Length > 0)
            {
                CopyAndRegist("89bf541fb59d76f49a207bb05251406e",//Editor/templete/VivoSplashShower.txt
                                  new KeyValuePair<string, string>("##SPLASH_ID##", adsetting.splashId[0]),
                                  new KeyValuePair<string, string>("##SPLASH_ACTIVE##", adsetting.showSplashOnAppInit.ToString().ToLower()));
            }
        }
        static void CopyAndRegist(string orgGuid,params KeyValuePair<string,string>[] rps)
        {
            var outputDir = "Assets/Plugins/Android";
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            var javaTxt = AssetDatabase.GUIDToAssetPath(orgGuid);
            string output = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(javaTxt)}.java");
            IOHelper.CopyFileWithReplease(javaTxt, output, rps);
            JavaHelper.RegistJavaInterface(output);
        }
    }
}
