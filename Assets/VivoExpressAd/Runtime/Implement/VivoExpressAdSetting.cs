using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoExpressAdSetting : AScriptableObject
    {
        public override string filePath => "Vivo/ExpressAdSettings";
        public string[] bannerId;
        public string[] pageDialogId;
    }
}
