using CameraBehavior;
using UnityEngine;
using Zenject;

namespace LevelElements
{
    public abstract class Bounds : MonoBehaviour
    {
        [Inject] protected EdgeDetector EdgeDetector;

        private void Awake()
        {
            SetPosition();
        }

        public abstract void SetPosition();

        public abstract Vector3 CalculatePosition();
    }
}