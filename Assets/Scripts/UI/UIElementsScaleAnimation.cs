using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIElementsScaleAnimation : MonoBehaviour
    {
        private float _effectDuration = 0.3f;

        public void Appear(GameObject uiElement)
        {
            uiElement.gameObject.transform.localScale = Vector3.zero;
            uiElement.gameObject.SetActive(true);
            uiElement.transform.DOScale(Vector3.one, _effectDuration).
                SetUpdate(true);
        }

        public void Disappear(GameObject uiElement)
        {
            uiElement.transform.DOScale(Vector3.zero, _effectDuration)
                .SetUpdate(true)
                .OnComplete(() => uiElement.SetActive(false));
        }
    }
}