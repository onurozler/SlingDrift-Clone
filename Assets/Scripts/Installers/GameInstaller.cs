using Game.Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PoolManager _poolManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_poolManager);
            Container.Bind<AssetManager>().AsSingle().NonLazy();
        }
    }
}