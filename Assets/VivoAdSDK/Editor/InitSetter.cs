using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using UnityEditor;
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
            var javaTxt = AssetDatabase.GUIDToAssetPath("7d2aa4c49a60e5c49aa04be1dd144eea");//Editor/VivoAdInitor.txt
            var output = "Assets/Plugins/Android/VivoAdInitor.java";
            IOHelper.CopyFileWithReplease(javaTxt, output,
                new KeyValuePair<string, string>("##APPID##", setting.mediaID),
                new KeyValuePair<string, string>("##DEBUG##", setting.isDebug.ToString().ToLower()));
            JavaHelper.RegistJavaInterface(output);
        } 
    }
}
