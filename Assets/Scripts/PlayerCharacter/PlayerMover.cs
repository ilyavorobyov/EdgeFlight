using Cysharp.Threading.Tasks;
using EnvironmentData;
using UI;
using UI.MobileControlButtons;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PlayerCharacter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        private DeviceChecker _deviceChecker;
        private MobileControlUpButton _mobileControlUpButton;
        private MobileControlDownButton _mobileControlDownButton;
        private PlayerInput _playerInput;
        private PlayerWeapon _playerWeapon;
        private GameUI _gameUI;
        private Rigidbody2D _rb;
        private bool _isMobile;
        private bool _isMoveDown = false;
        private bool _isMoveUp = false;
        private bool _isPlaying = false;
        private float _fallingSpeed = 0.4f;
        private int _startSpeed = 3;
        private int _currentSpeed;

        [Inject]
        private void Construct(
            DeviceChecker deviceChecker,
            MobileControlUpButton mobileControlUpButton,
            MobileControlDownButton mobileControlDownButton,
            PlayerWeapon playerWeapon,
            GameUI gameUI)
        {
            _deviceChecker = deviceChecker;
            _mobileControlUpButton = mobileControlUpButton;
            _mobileControlDownButton = mobileControlDownButton;
            _playerWeapon = playerWeapon;
            _gameUI = gameUI;
        }

        private void OnEnable()
        {
            _rb = GetComponent<Rigidbody2D>();
            _isMobile = _deviceChecker.IsMobile;
            _currentSpeed = _startSpeed;
            _gameUI.Started += OnGameStarted;

            if (_isMobile)
            {
                BindMobileControl(true);
                _playerWeapon.InitForMobile();
            }
            else
            {
                _playerInput = new PlayerInput();
                _playerWeapon.InitForDesktop(_playerInput);
                BindKeyboardControls(true);
            }
        }

        private void OnDisable()
        {
            _gameUI.Started += OnGameStarted;

            if (_isMobile)
            {
                BindMobileControl(false);
            }
            else
            {
                BindKeyboardControls(false);
                _playerInput.Disable();
            }
        }

        private void BindMobileControl(bool isBind)
        {
            if (isBind)
            {
                _mobileControlUpButton.OnPressed += OnMoveUp;
                _mobileControlUpButton.OnUpped += OnMoveCanceled;
                _mobileControlDownButton.OnPressed += OnMoveDown;
                _mobileControlDownButton.OnUpped += OnMoveCanceled;
            }
            else
            {
                _mobileControlUpButton.OnPressed -= OnMoveUp;
                _mobileControlUpButton.OnUpped -= OnMoveCanceled;
                _mobileControlDownButton.OnPressed -= OnMoveDown;
                _mobileControlDownButton.OnUpped -= OnMoveCanceled;
            }
        }

        private void BindKeyboardControls(bool bind)
        {
            if (bind)
            {
                _playerInput.Player.MoveUp.started += OnKeyboardMoveUp;
                _playerInput.Player.MoveUp.canceled += OnKeyboardMoveCanceled;
                _playerInput.Player.MoveDown.started += OnKeyboardMoveDown;
                _playerInput.Player.MoveDown.canceled += OnKeyboardMoveCanceled;
            }
            else
            {
                _playerInput.Player.MoveUp.started -= OnKeyboardMoveUp;
                _playerInput.Player.MoveUp.canceled -= OnKeyboardMoveCanceled;
                _playerInput.Player.MoveDown.started -= OnKeyboardMoveDown;
                _playerInput.Player.MoveDown.canceled -= OnKeyboardMoveCanceled;
            }
        }

        private async UniTask Move()
        {
            var token = this.GetCancellationTokenOnDestroy();

            while (_isPlaying && !token.IsCancellationRequested)
            {
                if (_isMoveUp)
                {
                    _rb.MovePosition(
                        _rb.position +
                        Vector2.up *
                        _currentSpeed *
                        Time.deltaTime);
                }
                else if (_isMoveDown)
                {
                    _rb.MovePosition(
                        _rb.position +
                        Vector2.down *
                        _currentSpeed *
                        Time.deltaTime);
                }
                else
                {
                    _rb.MovePosition(
                        _rb.position +
                        Vector2.down *
                        _fallingSpeed *
                        Time.deltaTime);
                }

                await UniTask.NextFrame();
            }
        }

        private void OnMoveUp()
        {
            _isMoveUp = true;
            _isMoveDown = false;
        }

        private void OnMoveDown()
        {
            _isMoveUp = false;
            _isMoveDown = true;
        }

        private void OnMoveCanceled()
        {
            _isMoveUp = false;
            _isMoveDown = false;
        }

        private void OnKeyboardMoveUp(InputAction.CallbackContext context)
        {
            OnMoveUp();
        }

        private void OnKeyboardMoveDown(InputAction.CallbackContext context)
        {
            OnMoveDown();
        }

        private void OnKeyboardMoveCanceled(InputAction.CallbackContext context)
        {
            OnMoveCanceled();
        }

        private void OnGameStarted()
        {
            if (!_isMobile)
            {
                _playerInput.Enable();
            }

            _isPlaying = true;
            Move().Forget();
        }

        private void OnGamePaused()
        {
            if (!_isMobile)
            {
                _playerInput.Disable();
            }

            _isPlaying = false;
        }
    }
}