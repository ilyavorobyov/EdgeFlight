using ObjectPools;
using UnityEngine;

namespace Bullets
{
    public abstract class BaseBulletCollisionHandler : MonoBehaviour
    {
        protected BaseBullet _bullet;

        protected abstract void HandleCollision(Collider2D other);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out DespawnZone despawnZone))
                HandleCollision(other);

            if (_bullet != null)
                _bullet.Hide();
        }
    }
}