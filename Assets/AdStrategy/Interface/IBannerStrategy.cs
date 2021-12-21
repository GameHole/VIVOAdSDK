using System.Collections.Generic;
using UnityEngine;
namespace AdStrategy
{
	public interface IBannerStrategy : IInterface
	{
        void SetReflshTime(float time);
        void SetCloseBtnScale(float scale);
	}
}
