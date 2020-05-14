using Config;
using Game.LevelSystem.LevelEvents;
using Game.SlingSystem.Base;
using Game.SlingSystem.Managers;
using Game.View;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarSlingController
    {
        private CarDirectionController _carDirectionController;
        private SlingManager _slingManager;
        private SlingTowerBase _targetSling;
        private PlayerView _playerView;
        
        private int _targetSlingIndex;
        
        
        public CarSlingController(CarDirectionController carDirectionController, SlingManager slingManager, 
            PlayerView playerView)
        {
            _slingManager = slingManager;
            _carDirectionController = carDirectionController;
            _playerView = playerView;
            
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=>
            {
                _targetSling = null;
                _targetSlingIndex = 0;
            });
            
            
        }

        public bool CheckAvailableSling()
        {
            _targetSling = _slingManager.GetSlingByID(_targetSlingIndex);
            return _targetSling != null;
        }

        public bool OnDrifting(Transform carBase)
        {
            if (_targetSling.IsCloseTo(carBase))
            {
                carBase.RotateAround(_targetSling.transform.position,_targetSling.transform.up * _targetSling.GetDirection(), 
                    Time.deltaTime * GameConfig.CAR_ROTATING);
                carBase.Rotate(0,_targetSling.GetDirection() * GameConfig.CAR_ROTATING * Time.deltaTime/4,0);
                _targetSling.AddLine(carBase);

                return true;
            }

            return false;
        }

        public void OnDriftingFinished(Transform carBase)
        {
            _targetSling.ResetLine();
            if (_targetSling.IsPassed(carBase))
            {
                _carDirectionController.Handle(_slingManager.GetSlingByID(++_targetSlingIndex).GetFirstPosition());
                _playerView.UpdateCounter(_targetSlingIndex);
            }
        }
    }
}
