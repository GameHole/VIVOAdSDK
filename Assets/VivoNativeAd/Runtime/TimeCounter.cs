using System;
using UnityEngine;
namespace VivoAdSdk
{
    class TimeCounter : MonoBehaviour
    {
        public float time;
        public float add;
        public Action action;
        public void Restart()
        {
            enabled = true;
        }
        public void Stop()
        {
            enabled = false;
            add = 0;
        }
        private void Update()
        {
            if ((add += Time.deltaTime) >= time)
            {
                Stop();
                action?.Invoke();
            }
        }
    }
}
