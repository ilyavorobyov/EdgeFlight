using UnityEngine;

namespace CameraBehaviour
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