using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using VivoAdSdk;
namespace VivoAdStrategyEx
{
    [Refinter.Important(101)]
    public class BannerAd : IBannerAd, IInitializable
    {
        PriorityUnin<IBannerAd> unin = new PriorityUnin<IBannerAd>();
        public Action<int> onClose { get; set; }
        public Action onHide { get; set; }
        public event Action onShow;

        public void Hide()
        {
            unin.current?.Hide();
        }

        public void Initialize()
        {
            unin.Add(new VivoExpressBanner());
            unin.Add(new VivoBanner());
            foreach (var item in unin.cntr)
            {
                item.onClose += (v) => onClose?.Invoke(v);
                item.onHide += () => onHide?.Invoke();
                item.onShow += () => onShow?.Invoke();
                //(item as IInitializable)?.Initialize();
            }
            unin.Initialize();
        }

        public void Show()
        {
            unin.current?.Show();
        }
    }
}
