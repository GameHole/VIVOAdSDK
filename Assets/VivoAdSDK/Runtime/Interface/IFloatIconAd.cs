using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace VivoAdSdk
{
	public interface IFloatIconAd:IInterface
	{
        Vector2Int screenPosition { get; set; }
        Action onClose { get; set; }
        void Show();
        void Hide();
	}
}
