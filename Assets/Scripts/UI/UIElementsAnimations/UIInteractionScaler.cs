using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.UIElementsAnimations
{
    public class UIInteractionScaler : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        private Vector3 _hoverSize = new Vector3(1.05f, 1.05f, 1.05f);
        private Vector3 _clickSize = new Vector3(0.9f, 0.9f, 0.9f);

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = _clickSize;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = _hoverSize;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = _hoverSize;
        }
    }
}