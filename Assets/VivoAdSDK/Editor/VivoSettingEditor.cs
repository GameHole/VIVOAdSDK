using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace VivoAdSdk
{
    [CustomEditor(typeof(VivoSetting))]
	public class VivoSettingEditor:Editor
	{
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InitSetter.SetJavaFile();
        }
    }
    [CustomEditor(typeof(VivoAdSetting))]
    public class VivoAdSettingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InitSetter.SetJavaFile();
        }
    }
}
