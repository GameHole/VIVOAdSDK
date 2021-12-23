using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
    [Refinter.Important(10)]
    public class BannerSetting : MonoBehaviour, MiniGameSDK.IBannerSetting
    {
        public Vector2Int _size;
        public int g = MiniGameSDK.Gravity.BOTTOM | MiniGameSDK.Gravity.CENTER;
        public Vector2Int Size { get=> _size; set=> _size=value; }
        public int gravity { get=>g; set=>g=value; }
    }
}
