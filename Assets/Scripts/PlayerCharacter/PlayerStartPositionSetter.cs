using CameraBehaviour;
using Cysharp.Threading.Tasks;
using GameLogic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace PlayerCharacter
{
    public class PlayerStartPositionSetter : MonoBehaviour
    {
        [Inject] private EdgeDetector _edgeDetector;
        [Inject] private PlayerMover _playerMover;
        [Inject] private GameStateSwitcher _gameStateSwitcher;

        private float _xOffset = 2.5f;
        private float _yOffset = 1.1f;
        private float _riseSpeed = 2;
        private float _duration = 1;
        private CancellationTokenSource _cts;

        private void OnEnable()
        {
            _gameStateSwitcher.Started += OnStarted;
            _gameStateSwitcher.Exited += StopRise;
            _gameStateSwitcher.Exited += SetStartPosition;
        }

        private void OnDisable()
        {
            _gameStateSwitcher.Started -= OnStarted;
            _gameStateSwitcher.Exited -= StopRise;
            _gameStateSwitcher.Exited -= SetStartPosition;
        }

        private void Awake()
        {
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            _playerMover.transform.position = new Vector3(
                _edgeDetector.Left + _xOffset,
                _edgeDetector.Bottom + _yOffset,
                transform.position.z);
        }

        public async UniTask StartedRise(CancellationToken token)
        {
            float elapsed = 0f;

            while (elapsed < _duration && !token.IsCancellationRequested)
            {
                elapsed += Time.deltaTime;
                transform.position += Vector3.up * _riseSpeed * Time.deltaTime;
                await UniTask.Yield();
            }
        }

        private void StartRise()
        {
            StopRise();
            _cts = new CancellationTokenSource();
            StartedRise(_cts.Token).Forget();
        }

        private void StopRise()
        {
            if (_cts != null)
            {
                if (!_cts.IsCancellationRequested)
                    _cts.Cancel();

                _cts.Dispose();
                _cts = null;
            }
        }

        private void OnStarted()
        {
            StartRise();
        }
    }
}