using Game.CarSystem.Base;
using Game.LevelSystem.Controllers;
using Game.LevelSystem.Managers;
using Game.Managers;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PoolManager _poolManager;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private CarBase _carBase;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_poolManager);
            Container.BindInstance(_levelGenerator);
            Container.BindInstance(_carBase);
            
            Container.Bind<AssetManager>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();
            Container.Bind<SlingManager>().AsSingle().NonLazy();
        }
    }
}