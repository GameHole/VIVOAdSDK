using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoSetting : AScriptableObject
    {
        public override string filePath => "Vivo/Setting";
        public string mediaID;
        public bool isDebug;
    }
}
