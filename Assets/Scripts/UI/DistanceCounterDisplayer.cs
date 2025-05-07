using GameLogic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(DistanceCounter))]
    public class DistanceCounterDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentDistanceText;
        [SerializeField] private TMP_Text _recordText;

        private DistanceCounter _distanceCounter;

        private void Awake()
        {
            _distanceCounter = GetComponent<DistanceCounter>();

            _distanceCounter.CurrentDistance
            .Subscribe(Show)
            .AddTo(this);
        }

        private void Show(float value)
        {
            _currentDistanceText.text = value.ToString();
        }

        private void ShowRecord()
        {
            // тут отображать макс результат
        }
    }
}