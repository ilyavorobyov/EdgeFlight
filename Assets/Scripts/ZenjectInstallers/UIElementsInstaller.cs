using TMPro;
using UI;
using UI.MobileControlButtons;
using UI.WeaponHeatMeterBar;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class UIElementsInstaller : MonoInstaller
    {
        private const string HighHeatTextID = "HighHeatText";
        private const string SimpleButtonClickSound = "SimpleButtonClickSound";

        [SerializeField] private MobileControlUpButton _mobileControlUpButton;
        [SerializeField] private MobileControlDownButton _mobileControlDownButton;
        [SerializeField] private MobileControlShootButton _mobileControlShootButton;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private WeaponHeatMeterBarFilling _weaponHeatMeterBarFilling;
        [SerializeField] private TMP_Text _highHeatText;
        [SerializeField] private AudioSource _simpleButtonClickSound;

        public override void InstallBindings()
        {
            Container.Bind<MobileControlUpButton>().FromInstance(_mobileControlUpButton).AsSingle();
            Container.Bind<MobileControlDownButton>().FromInstance(_mobileControlDownButton).AsSingle();
            Container.Bind<MobileControlShootButton>().FromInstance(_mobileControlShootButton).AsSingle();
            Container.Bind<GameUI>().FromInstance(_gameUI).AsSingle();
            Container.Bind<WeaponHeatMeterBarFilling>().FromInstance(_weaponHeatMeterBarFilling).AsSingle();
            Container.Bind<TMP_Text>().WithId(HighHeatTextID).FromInstance(_highHeatText).AsSingle();
            Container.Bind<AudioSource>().WithId(SimpleButtonClickSound).FromInstance(_simpleButtonClickSound).AsSingle();
        }
    }
}