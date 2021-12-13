using System.Collections.Generic;
using UnityEngine;
namespace VivoAdSdk
{
	public interface IBannerSetting:IInterface
	{
        Vector2Int Size { get; set; }
        int gravity { get; set; }
	}
}
