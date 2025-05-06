using UnityEngine;

namespace Bullets
{
    public interface IBullet
    {
        public void Init(
            Vector3 direction,
            Vector3 position,
            float additionalSpeed);
    }
}