using Game.CarSystem;
using Game.CarSystem.Base;
using Game.LevelSystem.Controllers;
using Game.LevelSystem.Managers;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        private PlayerView _playerView;
        private LevelGenerator _levelGenerator;
        private LevelManager _levelManager;
        private CarBase _carBase;

        [Inject]
        private void OnInstaller(PlayerView playerView,LevelManager levelManager, LevelGenerator levelGenerator, CarBase carBase)
        {
            _playerView = playerView;
            _levelManager = levelManager;
            _levelGenerator = levelGenerator;
            _carBase = carBase;
        }
        
        private void Awake()
        {
            _levelGenerator.Initialize();
            _playerView.Initialize();
            _carBase.Initialize(_levelManager.GetHighwayOfLevel(0,0).transform);
        }
    }
}
