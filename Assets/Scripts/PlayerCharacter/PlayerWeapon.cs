using PlayerCharacter.Weapon;
using UI.MobileControlButtons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PlayerCharacter
{
    public class PlayerWeapon : MonoBehaviour
    {
        [Inject] private MobileControlShootButton _mobileControlShootButton;
        [Inject] private WeaponHeatMeter _weaponHeatMeter;

        private PlayerInput _playerInput;

        private void OnDestroy()
        {
            if (_playerInput != null)
            {
                _playerInput.Player.Shoot.started += OnDesktopTryShoot;
            }
        }

        public void InitForDesktop(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Player.Shoot.started += OnDesktopTryShoot;
        }

        public void InitForMobile()
        {
            _mobileControlShootButton.OnPressed += OnTryShoot;
        }

        private void OnDisable()
        {
            _mobileControlShootButton.OnPressed -= OnTryShoot;
        }

        private void OnDesktopTryShoot(InputAction.CallbackContext context)
        {
            OnTryShoot();
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
            Debug.Log("shoot");
        }
    }
}