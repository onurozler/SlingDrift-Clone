using Config;
using Game.LevelSystem.LevelEvents;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarMovementController : MonoBehaviour
    {
        private CarSlingController _carSlingController;
        private CarDirectionController _carDirectionController;
        private CarCornerDetector _carCornerDetector;
        
        private TrailRenderer _driftEffect;
        
        public bool IsActive;

        private bool _movingActive;
        
        public void Initialize(CarSlingController carSlingController)
        {
            IsActive = false;

            _carSlingController = carSlingController;

            _driftEffect = GetComponentInChildren<TrailRenderer>();

            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=>
            {
                IsActive = true;
                _driftEffect.Clear();
            });
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL, ()=>
            {
                IsActive = false;
            });
            
            _carCornerDetector = new CarCornerDetector(transform);
            _movingActive = true;
        }
        private void FixedUpdate()
        {
            if(!IsActive)
                return;
            
            CheckInput();
            Move();
        }

        private void Move()
        {
            if(!_movingActive)
                return;
            
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }

        private void CheckInput()
        {
            _movingActive = true;
            _carCornerDetector.Detect();
            
            if(!_carSlingController.CheckAvailableSling())
                return;

            if (Input.GetMouseButton(0))
            {
                if (_carSlingController.OnDrifting(transform))
                {
                    _movingActive = false;
                    _driftEffect.emitting = true;
                }
            }
            else
            {
                _carSlingController.OnDriftingFinished(transform);
                _driftEffect.emitting = false;
            }
        }
    }
}
