using UnityEngine;
namespace VivoAdSdk
{
    class NativeBannerAutoChange 
    {
        TimeCounter counter;
        public float changeDuration;
        public VivoNativeBanner bannerAd;
        bool isShowed;
        public void Start()
        {
            CreateCounter();
            Stop();
            counter.time = changeDuration;
            bannerAd.onHide += () => Stop();
            bannerAd.onClose += (v) =>Stop();
            bannerAd.onShow += () =>
            {
                //Debug.Log("banner show");
                isShowed = true;
                counter.Restart();
            };
            bannerAd.onReloaded += (isSucc) =>
            {
                //Debug.Log($"auto loaded succ {isSucc}");
                if (isSucc && isShowed)
                {
                    bannerAd.Show();
                }
            };
            counter.action = () =>
            {
                //Debug.Log("count down");
                bannerAd.onReloaded?.Invoke(false);
            };
        }
        public void Stop()
        {
            counter.Stop();
            isShowed = false;
        }
        void CreateCounter()
        {
            if (counter == null)
            {
                var go = new GameObject("TimeCounter");
                go.hideFlags = HideFlags.HideAndDontSave;
                UnityEngine.Object.DontDestroyOnLoad(go);
                counter = go.AddComponent<TimeCounter>();
            }
        }
    }
}
