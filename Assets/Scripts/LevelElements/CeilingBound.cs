using UnityEngine;

namespace LevelElements
{
    public class CeilingBound : Bounds
    {
        private float _topOffset = 0.3f;

        public override void SetPosition()
        {
            base.SetPosition();

            transform.position = new Vector3(
                _xPos,
                EdgeDetector.Top + _topOffset,
                transform.position.z); ;
        }
    }
}