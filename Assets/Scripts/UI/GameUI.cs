using EnvironmentData;
using System;
using UI.MobileControlButtons;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        private StartButton _startButton;
        private MobileControlUpButton _mobileControlUpButton;
        private MobileControlDownButton _mobileControlDownButton;
        private MobileControlShootButton _mobileControlShootButton;
        private UIElementsScaleAnimation _uiElementsScaleAnimation;
        private UIStretchAnimation _uiStretchAnimation;
        private DeviceChecker _deviceChecker;
        private bool _isMobile;
        private bool _isVisible;

        public event Action Started;

        [Inject]
        private void Construct(
            DeviceChecker deviceChecker,
            UIElementsScaleAnimation uiElementsScaleAnimation,
            UIStretchAnimation uiStretchAnimation,
            StartButton startButton,
            MobileControlUpButton mobileControlUpButton,
            MobileControlDownButton mobileControlDownButton,
            MobileControlShootButton mobileControlShootButton)
        {
            _deviceChecker = deviceChecker;
            _uiElementsScaleAnimation = uiElementsScaleAnimation;
            _uiStretchAnimation = uiStretchAnimation;
            _startButton = startButton;
            _mobileControlUpButton = mobileControlUpButton;
            _mobileControlDownButton = mobileControlDownButton;
            _mobileControlShootButton = mobileControlShootButton;
        }

        private void Awake()
        {
            _isMobile = _deviceChecker.IsMobile;
            _startButton.OnPressed += OnStartButtonClick;
        }

        private void Start()
        {
            _uiStretchAnimation.Appear(_startButton.gameObject);
        }

        private void OnDestroy()
        {
            _startButton.OnPressed -= OnStartButtonClick;
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
            Started?.Invoke();
            _uiStretchAnimation.Disappear(_startButton.gameObject);
            _isVisible = true;
            SetMobileControlsVisible(_isVisible);
        }
    }
}