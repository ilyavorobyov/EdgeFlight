using UnityEngine;
using UnityEngine.UI;

namespace UI.WeaponHeatMeterBar
{
    [RequireComponent(typeof(Image))]
    public class WeaponHeatMeterBarFilling : MonoBehaviour
    {
        private Image _fillableImage;

        public Image FillableImage => _fillableImage;

        private void Awake()
        {
            _fillableImage = GetComponent<Image>();
        }
    }
}