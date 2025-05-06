using UnityEngine;

namespace LevelElements
{
    public class CeilingBound : Bounds
    {
        private float _topOffset = 0.3f;

        public override void SetPosition()
        {
            transform.position = CalculatePosition();
        }

        public override Vector3 CalculatePosition()
        {
            return new Vector3(
                EdgeDetector.CenterX,
                EdgeDetector.Top + _topOffset,
                transform.position.z);
        }
    }
}