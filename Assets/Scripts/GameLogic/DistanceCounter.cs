using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class DistanceCounter : MonoBehaviour
    {
        [Inject] private GameStateSwitcher _gameStateSwitcher;
        [Inject] private SpeedBooster _speedBooster;

        private float _currentSpeed;
        private float _distancePerSecond = 1.1f;
        private bool _isPlaying = false;
        private float _delayBeforeIncrease = 1;
        private CancellationTokenSource _counterCancellationTokenSource;

        private ReactiveProperty<float> _currentDistance = new ReactiveProperty<float>();
        private ReactiveProperty<float> _startDistance = new ReactiveProperty<float>(0);

        public IReadOnlyReactiveProperty<float> CurrentDistance => _currentDistance;

        private void OnEnable()
        {
            _gameStateSwitcher.Started += OnGameStarted;
            _gameStateSwitcher.Exited += OnGameEnded;
            _gameStateSwitcher.GameOvered += OnGameEnded;
        }

        private void OnDisable()
        {
            _gameStateSwitcher.Started -= OnGameStarted;
            _gameStateSwitcher.Exited -= OnGameEnded;
            _gameStateSwitcher.GameOvered -= OnGameEnded;
        }

        private void Awake()
        {
            _speedBooster.CurrentSpeed
                .Subscribe(value => _currentSpeed = value)
                .AddTo(this);
        }

        private void ClearToken()
        {
            if (_counterCancellationTokenSource != null
                && !_counterCancellationTokenSource.IsCancellationRequested)
            {
                _counterCancellationTokenSource.Cancel();
                _counterCancellationTokenSource.Dispose();
                _counterCancellationTokenSource = null;
            }
        }

        private async UniTask Increase(CancellationToken token)
        {
            var increaseTick = TimeSpan.FromSeconds(_delayBeforeIncrease);

            while (!token.IsCancellationRequested)
            {
                if (_isPlaying)
                {
                    await UniTask.Delay(increaseTick, cancellationToken: token);
                    _currentDistance.Value += (int)Math.Ceiling(_distancePerSecond * _currentSpeed);
                }
            }
        }

        private void OnGameStarted()
        {
            ClearToken();
            _currentDistance.Value = _startDistance.Value;
            _counterCancellationTokenSource = new CancellationTokenSource();
            _isPlaying = true;
            Increase(_counterCancellationTokenSource.Token).Forget();
        }

        private void OnGameEnded()
        {
            _isPlaying = false;
            ClearToken();
        }
    }
}