using UnityEngine;

namespace Bullets
{
    public abstract class BaseBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _speed;

        private bool _isCanMove = false;
        private Vector3 _moveDirection;
        private float _additionalSpeed;

        public void Init(Vector3 direction,
            Vector3 position,
            float additionalSpeed)
        {
            _moveDirection = direction;
            transform.position = position;
            _additionalSpeed = additionalSpeed;
            _isCanMove = true;
        }

        private void Update()
        {
            if (_isCanMove)
            {
                transform.position += (_moveDirection * (_speed + _additionalSpeed)) * Time.deltaTime;
            }
        }

        public void Hide()
        {
            _isCanMove = false;
            gameObject.SetActive(false);
        }
    }
}