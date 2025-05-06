using PlayerCharacter.Weapon;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class HighHeatTextIndicator : MonoBehaviour
{
    private const string HighHeatTextID = "HighHeatText";

    [Inject] private WeaponHeatMeter _weaponHeatMeter;
    [Inject(Id = HighHeatTextID)] private TMP_Text _highHeatText;

    private float _highHeatValue = 0.75f;

    private void Start()
    {
        _weaponHeatMeter.CurrentHeat
    .Where(heat => heat > _highHeatValue)
    .Subscribe(heat => Show());

        _weaponHeatMeter.CurrentHeat
    .Where(heat => heat < _highHeatValue)
    .Subscribe(heat => Hide());
    }

    private void Show()
    {
        _highHeatText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _highHeatText.gameObject.SetActive(false);
    }
}