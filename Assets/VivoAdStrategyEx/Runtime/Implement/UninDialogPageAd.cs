using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using VivoAdSdk;

namespace VivoAdStrategyEx
{
    [Refinter.Important(101)]
    public class UninDialogPageAd : IDialogPageAd, IInitializable
    {
        PriorityUnin<IDialogPageAd> unin = new PriorityUnin<IDialogPageAd>();
        public event Action<bool> onClose;

        public void Initialize()
        {
            unin.Add(new VivoExpressDialogPage());
            unin.Add(new VivoDialogPage());
            foreach (var item in unin.cntr)
            {
                item.onClose += (v) => onClose?.Invoke(v);
                //(item as IInitializable)?.Initialize();
            }
            unin.Initialize();
        }

        public bool isReady()
        {
            if (unin.current != null)
                return unin.current.isReady();
            return false;
        }

        public void Show()
        {
            unin.current?.Show();
        }
    }
}
