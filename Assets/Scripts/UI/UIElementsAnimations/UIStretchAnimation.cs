using DG.Tweening;
using UnityEngine;

namespace UI.UIElementsAnimations
{
    public class UIStretchAnimation : MonoBehaviour
    {
        private float _durationPerStep = 0.1f;
        private float _minXScale = 0.4f;

        public void Appear(GameObject uiElement)
        {
            uiElement.SetActive(true);
            uiElement.transform.localScale = new Vector3(_minXScale, 0f, 1f);
            uiElement.transform.DOScaleY(1f, _durationPerStep)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    uiElement.transform.DOScaleX(1f, _durationPerStep)
                        .SetEase(Ease.OutBack)
                        .SetUpdate(true);
                });
        }

        public void Disappear(GameObject uiElement)
        {
            uiElement.transform.DOScaleX(_minXScale, _durationPerStep)
                            .SetEase(Ease.InBack)
                            .SetUpdate(true)
                            .OnComplete(() =>
                            {
                                uiElement.transform.DOScaleY(0f, _durationPerStep)
                                    .SetEase(Ease.InBack)
                                    .SetUpdate(true)
                                    .OnComplete(() =>
                                    {
                                        uiElement.SetActive(false);
                                    });
                            });
        }
    }
}