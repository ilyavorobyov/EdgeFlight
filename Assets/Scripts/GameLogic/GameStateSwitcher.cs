using System;
using UI;
using UnityEngine;
using Zenject;

namespace GameLogic
{
    public class GameStateSwitcher : MonoBehaviour
    {
        [Inject] private GameUI _gameUI;

        private float _normalTimeScale = 1;
        private float _stoppedTimeScale = 0;

        public event Action Started;
        public event Action Paused;
        public event Action Resumed;
        public event Action Exited;
        public event Action GameOvered;

        private void OnEnable()
        {
            _gameUI.StartButtonPressed += OnStart;
            _gameUI.PauseButtonPressed += OnPause;
            _gameUI.ResumeButtonPressed += OnResume;
            _gameUI.ExitButtonPressed += OnExit;
        }

        private void OnDisable()
        {
            _gameUI.StartButtonPressed -= OnStart;
            _gameUI.PauseButtonPressed -= OnPause;
            _gameUI.ResumeButtonPressed -= OnResume;
            _gameUI.ExitButtonPressed -= OnExit;
        }

        private void Awake()
        {
            Time.timeScale = _stoppedTimeScale;
        }

        private void OnStart()
        {
            Time.timeScale = _normalTimeScale;
            Started?.Invoke();
        }

        private void OnPause()
        {
            Time.timeScale = _stoppedTimeScale;
            Paused?.Invoke();
        }

        private void OnResume()
        {
            Time.timeScale = _normalTimeScale;
            Resumed?.Invoke();
        }
        private void OnExit()
        {
            Exited?.Invoke();
            Time.timeScale = _stoppedTimeScale;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnGameOver();
            }
        }

        private void OnGameOver()
        {
            Time.timeScale = _stoppedTimeScale;
            GameOvered?.Invoke();
        }
    }
}