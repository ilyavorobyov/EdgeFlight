using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameLogic;
using UI.WeaponHeatMeterBar;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerCharacter.Weapon
{
    public class WeaponHeatMeter : MonoBehaviour
    {
        [Inject] private WeaponHeatMeterBarFilling _weaponHeatMeterBarFilling;
        [Inject] private GameStateSwitcher _gameStateSwitcher;

        private readonly ReactiveProperty<float> _currentHeat = new ReactiveProperty<float>(0f);

        private CancellationTokenSource _reduceCancellationTokenSource;
        private Image _fillableImage;
        private float _minHeat = 0;
        private float _heatPerShot = 0.14f;
        private float _predictedHeat = 0.16f;
        private float _coolingPerTick = 0.012f;
        private float _tickDelay = 0.1f;
        private float _maxDelay = 0.8f;
        private float _maxHeat = 1;
        private bool _isPlaying = false;

        public IReadOnlyReactiveProperty<float> CurrentHeat => _currentHeat;

        private void OnEnable()
        {
            _gameStateSwitcher.Started += OnGameStarted;
            _gameStateSwitcher.Exited += OnExited;
        }

        private void OnDisable()
        {
            _gameStateSwitcher.Started -= OnGameStarted;
            _gameStateSwitcher.Exited -= OnExited;
        }

        private void Awake()
        {
            _fillableImage = _weaponHeatMeterBarFilling.FillableImage;
        }

        public bool CanShoot()
        {
            return _currentHeat.Value + _predictedHeat < _maxHeat;
        }

        public void AddHeat()
        {
            _currentHeat.Value += _heatPerShot;
            ShowHeat();
        }

        private void ShowHeat()
        {
            _fillableImage.fillAmount = _currentHeat.Value;
        }

        private void ClearToken()
        {
            if (_reduceCancellationTokenSource != null && !_reduceCancellationTokenSource.IsCancellationRequested)
            {
                _reduceCancellationTokenSource.Cancel();
                _reduceCancellationTokenSource.Dispose();
                _reduceCancellationTokenSource = null;
            }
        }

        private async UniTask ReduceHeat(CancellationToken token)
        {
            var coolingTickDelay = TimeSpan.FromSeconds(_tickDelay);
            var coolingMaxHeat = TimeSpan.FromSeconds(_maxDelay);

            while (!token.IsCancellationRequested)
            {
                if (_isPlaying)
                {
                    if (_currentHeat.Value >= _maxHeat)
                    {
                        await UniTask.Delay(coolingMaxHeat);
                    }
                    else if (_currentHeat.Value > _minHeat)
                    {
                        ShowHeat();
                    }

                    await UniTask.Delay(coolingTickDelay);
                    _currentHeat.Value = Mathf.Max(_minHeat, _currentHeat.Value - _coolingPerTick);
                }
            }
        }

        private void OnGameStarted()
        {
            _currentHeat.Value = _minHeat;
            ShowHeat();
            _isPlaying = true;
            ClearToken();
            _reduceCancellationTokenSource = new CancellationTokenSource();
            ReduceHeat(_reduceCancellationTokenSource.Token).Forget();
        }

        private void OnExited()
        {
            _isPlaying = false;
            ClearToken();
        }
    }
}