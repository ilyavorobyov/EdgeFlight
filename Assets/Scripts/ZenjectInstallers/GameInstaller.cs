using CameraBehavior;
using EnvironmentData;
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
        [SerializeField] private EdgeDetector _edgeDetector;
        [SerializeField] private WeaponHeatMeter _weaponHeatMeter;

        public override void InstallBindings()
        {
            Container.Bind<DeviceChecker>().FromInstance(_deviceChecker).AsSingle();
            Container.Bind<PlayerWeapon>().FromInstance(_playerWeapon).AsSingle();
            Container.Bind<EdgeDetector>().FromInstance(_edgeDetector).AsSingle();
            Container.Bind<WeaponHeatMeter>().FromInstance(_weaponHeatMeter).AsSingle();
        }
    }
}