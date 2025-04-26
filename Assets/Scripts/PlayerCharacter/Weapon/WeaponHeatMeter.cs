using System;
using Cysharp.Threading.Tasks;
using UI;
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
        [Inject] private GameUI _gameUI;

        private readonly ReactiveProperty<float> _currentHeat = new ReactiveProperty<float>(0f);

        private Image _fillableImage;
        private float _minHeat = 0;
        private float _heatPerShot = 0.2f;
        private float _predictedHeat = 0.16f;
        private float _coolingPerTick = 0.0085f;
        private float _tickDelay = 0.1f;
        private float _maxDelay = 0.8f;
        private float _maxHeat = 1;
        private bool _isPlaying = false;

        public IReadOnlyReactiveProperty<float> CurrentHeat => _currentHeat;

        private void OnEnable()
        {
            _gameUI.Started += OnGameStarted;
        }

        private void OnDisable()
        {
            _gameUI.Started -= OnGameStarted;
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

        private async UniTask ReduceHeat()
        {
            var coolingTickDelay = TimeSpan.FromSeconds(_tickDelay);
            var coolingMaxHeat = TimeSpan.FromSeconds(_maxDelay);
            var token = this.GetCancellationTokenOnDestroy();

            while (_isPlaying && !token.IsCancellationRequested)
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

        private void OnGameStarted()
        {
            _fillableImage.fillAmount = 0;
            _isPlaying = true;
            ReduceHeat().Forget();
        }
    }
}