using GameLogic;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Parallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [Inject] private SpeedBooster _speedBooster;
        [Inject] private GameStateSwitcher _gameStateSwitcher;

        [SerializeField] private float _addingSpeed;
        [SerializeField] private float _resetX;
        [SerializeField] private float _startX;

        private float _currentSpeed;
        private bool _isCanMove = false;

        private void OnEnable()
        {
            _gameStateSwitcher.Started += OnGameStarted;
            _gameStateSwitcher.Exited += OnGameExited;
        }

        private void OnDisable()
        {
            _gameStateSwitcher.Started += OnGameStarted;
            _gameStateSwitcher.Exited += OnGameExited;
        }

        private void Awake()
        {
            _speedBooster.CurrentSpeed
                .Subscribe(value  => _currentSpeed = value)
                .AddTo(this);
        }

        private void Update()
        {
            if (_isCanMove)
            {
                transform.Translate(Vector3.left * (_currentSpeed + _addingSpeed) * Time.deltaTime);

                if (transform.position.x <= _resetX)
                {
                    transform.position = new Vector3(_startX, transform.position.y, transform.position.z);
                }
            }
        }

        private void OnGameExited()
        {
            _isCanMove = false;
        }

        private void OnGameStarted()
        {
            _isCanMove = true;
        }
    }
}