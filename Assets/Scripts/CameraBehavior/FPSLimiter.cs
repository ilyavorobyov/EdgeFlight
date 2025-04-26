using UnityEngine;

namespace CameraBehavior
{

    public class FPSLimiter : MonoBehaviour
    {
        private int _maxValue = 60;

        private void Awake()
        {
            Application.targetFrameRate = _maxValue;
        }
    }
}