using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MobileControlButtons
{
    public abstract class MobileControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnPressed;
        public event Action OnUpped;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpped?.Invoke();
        }
    }
}