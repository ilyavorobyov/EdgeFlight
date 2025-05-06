using UnityEngine;

namespace LevelElements
{
    public class FloorBound : Bounds
    {
        private float _bottomOffset = 0.3f;

        public override void SetPosition()
        {
            transform.position = CalculatePosition();
        }

        public override Vector3 CalculatePosition()
        {
            return new Vector3(
                EdgeDetector.CenterX,
                EdgeDetector.Bottom - _bottomOffset,
                transform.position.z);
        }
    }
}