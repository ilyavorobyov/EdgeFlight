using EnvironmentData;
using GameLogic;
using System;
using UI.MobileControlButtons;
using UI.Panels;
using UI.UIElementsAnimations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private UIElementsScaleAnimation _uiElementsScaleAnimation;
        [SerializeField] private UIStretchAnimation _uiStretchAnimation;
        [SerializeField] private CanvasGroup _weaponHeatMeterBar;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitPausePanelButton;
        [SerializeField] private Button _exitGameOverPanelButton;
        [SerializeField] private PausePanel _pausePanel;
        [SerializeField] private GameOverPanel _gameOverPanel;

        private GameStateSwitcher _gameStateSwitcher;
        private MobileControlUpButton _mobileControlUpButton;
        private MobileControlDownButton _mobileControlDownButton;
        private MobileControlShootButton _mobileControlShootButton;
        private DeviceChecker _deviceChecker;
        private float _visibleAlpha = 1;
        private float _invisibleAlpha = 0;
        private bool _isMobile;
        private bool _isVisible;

        public event Action StartButtonPressed;
        public event Action PauseButtonPressed;
        public event Action ResumeButtonPressed;
        public event Action ExitButtonPressed;

        [Inject]
        private void Construct(
            DeviceChecker deviceChecker,
            MobileControlUpButton mobileControlUpButton,
            MobileControlDownButton mobileControlDownButton,
            MobileControlShootButton mobileControlShootButton,
            GameStateSwitcher gameStateSwitcher)
        {
            _deviceChecker = deviceChecker;
            _mobileControlUpButton = mobileControlUpButton;
            _mobileControlDownButton = mobileControlDownButton;
            _mobileControlShootButton = mobileControlShootButton;
            _gameStateSwitcher = gameStateSwitcher;
        }

        private void OnEnable()
        {
            _gameStateSwitcher.GameOvered += OnGameOvered;
            _startButton.onClick.AddListener(OnStartButtonClick);
            _restartButton.onClick.AddListener(OnStartButtonClick);
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
            _resumeButton.onClick.AddListener(OnResumeButtonClick);
            _exitPausePanelButton.onClick.AddListener(OnExitButtonClick);
            _exitGameOverPanelButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _gameStateSwitcher.GameOvered -= OnGameOvered;
            _startButton.onClick.RemoveListener(OnStartButtonClick);
            _restartButton.onClick.RemoveListener(OnStartButtonClick);
            _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
            _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
            _exitPausePanelButton.onClick.RemoveListener(OnExitButtonClick);
            _exitGameOverPanelButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void Awake()
        {
            _isMobile = _deviceChecker.IsMobile;
        }

        private void Start()
        {
            _uiStretchAnimation.Appear(_startButton.gameObject);
        }

        private void SetMobileControlsVisible(bool isVisible)
        {
            if (!_isMobile) return;

            if (isVisible)
            {
                _uiElementsScaleAnimation.Appear(_mobileControlUpButton.gameObject);
                _uiElementsScaleAnimation.Appear(_mobileControlDownButton.gameObject);
                _uiElementsScaleAnimation.Appear(_mobileControlShootButton.gameObject);
            }
            else
            {
                _uiElementsScaleAnimation.Disappear(_mobileControlUpButton.gameObject);
                _uiElementsScaleAnimation.Disappear(_mobileControlDownButton.gameObject);
                _uiElementsScaleAnimation.Disappear(_mobileControlShootButton.gameObject);
            }
        }

        private void OnStartButtonClick()
        {
            StartButtonPressed?.Invoke();
            _uiStretchAnimation.Disappear(_startButton.gameObject);
            _uiStretchAnimation.Appear(_pauseButton.gameObject);
            _weaponHeatMeterBar.alpha = _visibleAlpha;
            _isVisible = true;
            SetMobileControlsVisible(_isVisible);

            if(_gameOverPanel.isActiveAndEnabled)
            {
                _uiStretchAnimation.Disappear(_gameOverPanel.gameObject);
            }
        }

        private void OnPauseButtonClick()
        {
            _uiStretchAnimation.Disappear(_pauseButton.gameObject);
            _uiStretchAnimation.Appear(_pausePanel.gameObject);
            _weaponHeatMeterBar.alpha = _invisibleAlpha;
            PauseButtonPressed?.Invoke();
            _isVisible = false;
            SetMobileControlsVisible(_isVisible);
        }

        private void OnResumeButtonClick()
        {
            _uiStretchAnimation.Appear(_pauseButton.gameObject);
            _uiStretchAnimation.Disappear(_pausePanel.gameObject);
            _uiStretchAnimation.Appear(_weaponHeatMeterBar.gameObject);
            _weaponHeatMeterBar.alpha = _visibleAlpha;
            ResumeButtonPressed?.Invoke();
            _isVisible = true;
            SetMobileControlsVisible(_isVisible);
        }

        private void OnExitButtonClick()
        {
            ExitButtonPressed?.Invoke();
            _weaponHeatMeterBar.alpha = _invisibleAlpha;
            _uiStretchAnimation.Disappear(_pausePanel.gameObject);
            _uiStretchAnimation.Appear(_startButton.gameObject);
            _isVisible = false;
            SetMobileControlsVisible(_isVisible);

            if (_gameOverPanel.isActiveAndEnabled)
            {
                _uiStretchAnimation.Disappear(_gameOverPanel.gameObject);
            }
        }

        private void OnGameOvered()
        {
            _uiStretchAnimation.Disappear(_pauseButton.gameObject);
            _uiStretchAnimation.Appear(_gameOverPanel.gameObject);
            _weaponHeatMeterBar.alpha = _invisibleAlpha;
            _isVisible = false;
            SetMobileControlsVisible(_isVisible);
        }
    }
}