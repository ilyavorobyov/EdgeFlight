using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{
    public class ButtonClickSoundPlayer : MonoBehaviour, IPointerClickHandler
    {
        private const string SimpleButtonClickSound = "SimpleButtonClickSound";

        [Inject(Id = SimpleButtonClickSound)] private AudioSource _clickSound;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_clickSound != null)
                _clickSound.PlayDelayed(0);
        }
    }
}