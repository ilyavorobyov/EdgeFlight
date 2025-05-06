using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(PlayerBullet))]
    public class PlayerBulletCollisionHandler : BaseBulletCollisionHandler
    {
        private PlayerBullet _playerBullet;

        private void Awake()
        {
            _playerBullet = GetComponent<PlayerBullet>();
            _bullet = _playerBullet;
        }

        protected override void HandleCollision(Collider2D other)
        {
            Debug.Log("другой коллайдер");
        }
    }
}