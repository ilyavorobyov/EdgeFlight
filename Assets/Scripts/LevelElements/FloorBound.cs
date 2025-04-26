using UnityEngine;

namespace LevelElements
{
    public class FloorBound : Bounds
    {
        public override void SetPosition()
        {
            transform.position = CalculatePosition();
        }

        public override Vector3 CalculatePosition()
        {
            return new Vector3(
                EdgeDetector.CenterX,
                EdgeDetector.Bottom,
                transform.position.z);
        }
    }
}