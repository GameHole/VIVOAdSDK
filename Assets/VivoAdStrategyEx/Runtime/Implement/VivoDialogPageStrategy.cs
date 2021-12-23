using AdStrategy;
using UnityEngine;

namespace VivoAdStrategyEx
{
    public class VivoDialogPageStrategy : IDialogPageStrategy
    {
        public void SetClickRangeScale(float scale)
        {
            //using (AndroidJavaClass helper = new AndroidJavaClass("com.ad.vivo.VivoNativeHelper"))
            //{
            //    helper.CallStatic("SetInterClickRangeScale", scale);
            //}
        }
    }
}
