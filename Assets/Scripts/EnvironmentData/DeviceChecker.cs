using UnityEngine;
using YG;

namespace EnvironmentData
{
    public class DeviceChecker : MonoBehaviour
    {
        private bool _isMobile;

        public bool IsMobile => _isMobile;

        private void Awake()
        {
            _isMobile = YG2.envir.isMobile;
        }
    }
}