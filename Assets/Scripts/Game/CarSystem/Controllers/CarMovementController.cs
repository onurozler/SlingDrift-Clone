using Config;
using Game.LevelSystem.LevelEvents;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarMovementController : MonoBehaviour
    {
        private CarAnimationController _carAnimationController;
        private CarDirectionController _carDirectionController;
        private CarCornerDetector _carCornerDetector;
        
        private SlingManager _slingManager;
        
        public bool IsActive;

        private bool _movingActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
            IsActive = false;
        }
        
        public void Initialize(CarAnimationController carAnimationController, CarDirectionController carDirectionController)
        {
            _carAnimationController = carAnimationController;
            _carDirectionController = carDirectionController;

            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=> IsActive = true);
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL, ()=> IsActive = false);
            
            _carCornerDetector = new CarCornerDetector(transform);
            _movingActive = true;
        }
        private void FixedUpdate()
        {
            if(!IsActive)
                return;
            
            _carCornerDetector.Detect();
            CheckInput();
            Move();
        }

        private void Move()
        {
            if(!_movingActive)
                return;
            
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }

        private int index = 0;
        private void CheckInput()
        {
            _movingActive = true;
            var closestSling = _slingManager.GetSlingByID(index);
            if(closestSling == null)
                return;
            
            if (Input.GetMouseButton(0))
            {
                if (closestSling.IsCloseTo(transform))
                {
                    _movingActive = false;
                    _carAnimationController.Pause();
                    closestSling.AddLine(transform);
                    transform.RotateAround(closestSling.transform.position,closestSling.transform.up * closestSling.GetDirection(), 
                        Time.deltaTime * GameConfig.CAR_ROTATING);
                    transform.Rotate(0,closestSling.GetDirection() * GameConfig.CAR_ROTATING * Time.deltaTime/4,0);
                }
            }
            else
            {
                if (closestSling.IsPassed(transform))
                {
                    _carDirectionController.Handle(_slingManager.GetSlingByID(++index).GetFirstPosition());
                }
                closestSling.ResetLine();
            }

        }
    }
}
