using System;
using System.Collections.Generic;
using System.Threading;
using Bullets;
using Cysharp.Threading.Tasks;
using GameLogic;
using ObjectPools;
using PlayerCharacter.Weapon;
using UI.MobileControlButtons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace PlayerCharacter
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Inject] private MobileControlShootButton _mobileControlShootButton;
        [Inject] private WeaponHeatMeter _weaponHeatMeter;
        [Inject] private GameStateSwitcher _gameStateSwitcher;
        [Inject] private PlayerBulletPool _bulletPool;
        [Inject] private SpeedBooster _speedBooster;

        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AudioSource _shootSound;

        private PlayerInput _playerInput;
        private CancellationTokenSource _cts;
        private bool _isShooting = false;
        private float _shootingDelay = 0.8f;
        private Vector3 _shootDirection = Vector3.right;
        private bool _isMobile;

        private void OnEnable()
        {
            _gameStateSwitcher.Exited += StopShoot;
        }

        private void OnDisable()
        {
            if (_playerInput != null)
            {
                _playerInput.Player.Shoot.started -= OnDesktopStartShoot;
                _playerInput.Player.Shoot.canceled -= OnDesktopStopShoot;
            }
            else
            {
                _mobileControlShootButton.OnPressed -= StartShoot;
                _mobileControlShootButton.OnUpped -= StopShoot;
            }

            _gameStateSwitcher.Exited -= StopShoot;

            if (_cts != null)
            {
                StopShoot();
            }
        }

        public void InitForDesktop(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Player.Shoot.started += OnDesktopStartShoot;
            _playerInput.Player.Shoot.canceled += OnDesktopStopShoot;
            _isMobile = false;
        }

        public void InitForMobile()
        {
            _mobileControlShootButton.OnPressed += StartShoot;
            _mobileControlShootButton.OnUpped += StopShoot;
            _isMobile = true;
        }

        private void StartShoot()
        {
            StopShoot();
            _cts = new CancellationTokenSource();
            _isShooting = true;
            Shoot(_cts.Token).Forget();
        }

        private void StopShoot()
        {
            if (_cts != null)
            {
                if (!_cts.IsCancellationRequested)
                    _cts.Cancel();

                _cts.Dispose();
                _cts = null;
            }

            _isShooting = false;
        }

        private async UniTask Shoot(CancellationToken token)
        {
            while (!token.IsCancellationRequested
                && _isShooting)
            {
                if (IsPointerOverUI() && !_isMobile)
                    return;

                OnTryShoot();
                await UniTask.Delay(TimeSpan.FromSeconds(_shootingDelay));
            }
        }

        private bool IsPointerOverUI()
        {
            var pointerData = new PointerEventData(_eventSystem)
            {
                position = Mouse.current.position.ReadValue()
            };

            var results = new List<RaycastResult>();
            _raycaster.Raycast(pointerData, results);

            return results.Count > 0;
        }

        private void OnDesktopStartShoot(InputAction.CallbackContext context)
        {
            StartShoot();
        }

        private void OnDesktopStopShoot(InputAction.CallbackContext context)
        {
            StopShoot();
        }

        private void OnTryShoot()
        {
            if (_weaponHeatMeter.CanShoot())
            {
                Shoot();
                _weaponHeatMeter.AddHeat();
            }
        }

        private void Shoot()
        {
            PlayerBullet bullet = _bulletPool.Get();
            bullet.Init(_shootDirection,
                _shootPoint.transform.position,
                _speedBooster.CurrentSpeed.Value);
            _shootSound.PlayDelayed(0);
        }
    }
}