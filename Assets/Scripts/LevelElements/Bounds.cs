using CameraBehaviour;
using UnityEngine;
using Zenject;

namespace LevelElements
{
    public abstract class Bounds : MonoBehaviour
    {
        [Inject] protected EdgeDetector EdgeDetector;

        [SerializeField] private bool _isRightSide;

        protected float _xOffset = 25f;
        protected float _xPos;

        private void Awake()
        {
            SetPosition();
        }

        public virtual void SetPosition()
        {
            if (!_isRightSide)
            {
                _xPos = EdgeDetector.CenterX;
            }
            else
            {
                _xPos = EdgeDetector.CenterX + _xOffset;
            }
        }
    }
}