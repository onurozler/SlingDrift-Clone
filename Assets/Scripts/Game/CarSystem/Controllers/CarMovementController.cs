﻿using Config;
using Game.LevelSystem.LevelEvents;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarMovementController : MonoBehaviour
    {
        private CarCornerDetector _carCornerDetector;
        private CarDirectionController _carDirectionController;
        private CarAnimationController _carAnimationController;
        private SlingManager _slingManager;
        
        public bool IsActive;

        private bool _movingActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
            IsActive = false;
        }
        
        public void Initialize(CarAnimationController carAnimationController)
        {
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=> IsActive = true);
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL, ()=> IsActive = false);
            
            _carAnimationController = carAnimationController;
            _carCornerDetector = new CarCornerDetector(transform);
            _carDirectionController = new CarDirectionController(transform);
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
            if (Input.GetMouseButton(0))
            {
                if (Vector3.Distance(closestSling.transform.position,transform.position) < 25f)
                {
                    _movingActive = false;
                    //_carAnimationController.Pause();
                    closestSling.AddLine(transform);
                    transform.RotateAround(closestSling.transform.position,closestSling.transform.up * closestSling.GetDirection(), 
                        Time.deltaTime * GameConfig.CAR_ROTATING);
                    transform.Rotate(0,closestSling.GetDirection() * GameConfig.CAR_ROTATING * Time.deltaTime/4,0);
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(1))
                    _carDirectionController.Handle(_slingManager.GetSlingByID(++index).GetFirstPosition());
                
                closestSling.ResetLine();
                //_carAnimationController.Play();
            }

        }
    }
}
