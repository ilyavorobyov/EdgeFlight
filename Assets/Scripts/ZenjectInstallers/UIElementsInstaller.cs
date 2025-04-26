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

        [SerializeField] private UIElementsScaleAnimation _uiElementsScaleAnimation;
        [SerializeField] private UIStretchAnimation _uiStretchAnimation;
        [SerializeField] private MobileControlUpButton _mobileControlUpButton;
        [SerializeField] private MobileControlDownButton _mobileControlDownButton;
        [SerializeField] private MobileControlShootButton _mobileControlShootButton;
        [SerializeField] private StartButton _startButton;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private WeaponHeatMeterBarFilling _weaponHeatMeterBarFilling;
        [SerializeField] private TMP_Text _highHeatText;

        public override void InstallBindings()
        {
            Container.Bind<UIElementsScaleAnimation>().FromInstance(_uiElementsScaleAnimation).AsSingle();
            Container.Bind<UIStretchAnimation>().FromInstance(_uiStretchAnimation).AsSingle();
            Container.Bind<MobileControlUpButton>().FromInstance(_mobileControlUpButton).AsSingle();
            Container.Bind<MobileControlDownButton>().FromInstance(_mobileControlDownButton).AsSingle();
            Container.Bind<MobileControlShootButton>().FromInstance(_mobileControlShootButton).AsSingle();
            Container.Bind<StartButton>().FromInstance(_startButton).AsSingle();
            Container.Bind<GameUI>().FromInstance(_gameUI).AsSingle();
            Container.Bind<WeaponHeatMeterBarFilling>().FromInstance(_weaponHeatMeterBarFilling).AsSingle();
            Container.Bind<TMP_Text>().WithId(HighHeatTextID).FromInstance(_highHeatText).AsSingle();
        }
    }
}