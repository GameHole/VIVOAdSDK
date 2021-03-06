using AdStrategy;
using UnityEngine;
namespace VivoAdSdk
{
    public class VivoBannerStrategy : IBannerStrategy
    {
        public void SetCloseBtnScale(float scale)
        {
            using(AndroidJavaClass helper=new AndroidJavaClass("com.ad.vivo.VivoNativeHelper"))
            {
                helper.CallStatic("SetBannerBtnScale", scale);
            }
        }

        public void SetReflshTime(float time)
        {
            SettingHelper.adsetting.bannerReflshTime = (int)time;
            VivoNativeAd.setting.changeDuration = time;
        }
    }
}
