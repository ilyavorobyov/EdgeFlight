using UnityEngine;

namespace CameraBehavior
{
    public class EdgeDetector : MonoBehaviour
    {
        private Camera _camera;
        private float _left;
        private float _right;
        private float _top;
        private float _bottom;
        private float _centerX;
        private float _halfMultiplier = 0.5f;
        private Vector3 _bottomLeft = Vector3.zero;
        private Vector3 _topRight = new Vector3(1, 1, 0);

        public float Left => _left;
        public float Right => _right;
        public float Top => _top;
        public float Bottom => _bottom;
        public float CenterX => _centerX;

        private void Awake()
        {
            _camera = Camera.main;
            Calculate();
        }

        private void Calculate()
        {
            _left = _camera.ViewportToWorldPoint(_bottomLeft).x;
            _bottom = _camera.ViewportToWorldPoint(_bottomLeft).y;
            _right = _camera.ViewportToWorldPoint(_topRight).x;
            _top = _camera.ViewportToWorldPoint(_topRight).y;
            _centerX = (_left + _right) * _halfMultiplier;
        }
    }
}