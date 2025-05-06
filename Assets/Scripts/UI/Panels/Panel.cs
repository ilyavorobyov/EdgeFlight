using UnityEngine;

namespace UI.Panels
{
    public abstract class Panel : MonoBehaviour
    {
        [SerializeField] private AudioSource _activateSound;

        private void OnEnable()
        {
            if (_activateSound != null)
                _activateSound.PlayDelayed(0);
        }
    }
}