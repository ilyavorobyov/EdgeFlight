using ObjectPools;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PlayerBulletPool _playerBulletPool;

        public override void InstallBindings()
        {
            Container.Bind<PlayerBulletPool>().FromInstance(_playerBulletPool).AsSingle();
        }
    }
}