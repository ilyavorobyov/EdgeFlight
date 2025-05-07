using UnityEngine;

namespace LevelElements
{
    public class FloorBound : Bounds
    {
        private float _bottomOffset = 0.3f;

        public override void SetPosition()
        {
            base.SetPosition();

            transform.position = new Vector3(
                _xPos,
                EdgeDetector.Bottom - _bottomOffset,
                transform.position.z);
        }
    }
}