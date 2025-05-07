using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class SpeedBooster : MonoBehaviour
    {
        [Inject] private GameStateSwitcher _gameStateSwitcher;

        private CancellationTokenSource _increaseCancellationTokenSource;
        private bool _isPlaying = false;
        private float _delayBeforeIncrease = 2;
        private float _additionAmount = 0.1f;
        private float _maxSpeed = 5f;

        private ReactiveProperty<float> _currentSpeed = new ReactiveProperty<float>();
        private ReactiveProperty<float> _startSpeed = new ReactiveProperty<float>(1);
        private ReactiveProperty<float> _stoppedSpeed = new ReactiveProperty<float>(0);

        public IReadOnlyReactiveProperty<float> CurrentSpeed => _currentSpeed;

        private void OnEnable()
        {
            _currentSpeed.Value = _stoppedSpeed.Value;
            _gameStateSwitcher.Started += OnGameStarted;
            _gameStateSwitcher.Exited += OnGameEnded;
        }

        private void OnDisable()
        {
            _gameStateSwitcher.Started -= OnGameStarted;
            _gameStateSwitcher.Exited -= OnGameEnded;
        }

        private async UniTask Increase(CancellationToken token)
        {
            var coolingTickDelay = TimeSpan.FromSeconds(_delayBeforeIncrease);

            while (!token.IsCancellationRequested)
            {
                if (_isPlaying)
                {
                    await UniTask.Delay(coolingTickDelay, cancellationToken: token);

                    if (!Mathf.Approximately(_currentSpeed.Value, _maxSpeed))
                    {
                        _currentSpeed.Value += _additionAmount;
                    }
                }
            }
        }

        private void ClearToken()
        {
            if (_increaseCancellationTokenSource != null && !_increaseCancellationTokenSource.IsCancellationRequested)
            {
                _increaseCancellationTokenSource.Cancel();
                _increaseCancellationTokenSource.Dispose();
                _increaseCancellationTokenSource = null;
            }
        }

        private void OnGameStarted()
        {
            ClearToken();
            _increaseCancellationTokenSource = new CancellationTokenSource();
            _currentSpeed.Value = _startSpeed.Value;
            _isPlaying = true;
            Increase(_increaseCancellationTokenSource.Token).Forget();
        }

        private void OnGameEnded()
        {
            _currentSpeed.Value = _stoppedSpeed.Value;
            _isPlaying = false;
            ClearToken();
        }
    }
}