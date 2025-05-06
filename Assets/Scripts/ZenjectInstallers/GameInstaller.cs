using CameraBehaviour;
using EnvironmentData;
using GameLogic;
using PlayerCharacter;
using PlayerCharacter.Weapon;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DeviceChecker _deviceChecker;
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private EdgeDetector _edgeDetector;
        [SerializeField] private WeaponHeatMeter _weaponHeatMeter;
        [SerializeField] private GameStateSwitcher _gameStateSwitcher;
        [SerializeField] private SpeedBooster _speedBooster;

        public override void InstallBindings()
        {
            Container.Bind<DeviceChecker>().FromInstance(_deviceChecker).AsSingle();
            Container.Bind<PlayerWeapon>().FromInstance(_playerWeapon).AsSingle();
            Container.Bind<EdgeDetector>().FromInstance(_edgeDetector).AsSingle();
            Container.Bind<WeaponHeatMeter>().FromInstance(_weaponHeatMeter).AsSingle();
            Container.Bind<GameStateSwitcher>().FromInstance(_gameStateSwitcher).AsSingle();
            Container.Bind<PlayerMover>().FromInstance(_playerMover).AsSingle();
            Container.Bind<SpeedBooster>().FromInstance(_speedBooster).AsSingle();
        }
    }
}