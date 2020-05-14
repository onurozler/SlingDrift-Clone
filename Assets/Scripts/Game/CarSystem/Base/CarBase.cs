﻿using Game.CarSystem.Controllers;
using Game.LevelSystem.LevelEvents;
using UnityEngine;

namespace Game.CarSystem.Base
{
    public class CarBase : MonoBehaviour
    {
        [SerializeField] 
        private Camera _carCamera;
        private Vector3 _cameraOffset;

        #region Controllers

        private CarMovementController _carMovementController;
        private CarDirectionController _carDirectionController;

        #endregion

        
        public void Initialize()
        {
            _carCamera = _carCamera == null ? Camera.main : _carCamera;
            _cameraOffset = _carCamera.transform.position - transform.position;
            
            _carDirectionController = new CarDirectionController(transform);

            _carMovementController = GetComponent<CarMovementController>();
            _carMovementController.Initialize(_carDirectionController);
            
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=>
            {
                gameObject.SetActive(true);
            });
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL,() 
                =>
            {
                gameObject.SetActive(false);
            });
        }

        public void SetCarPosition(Transform objeTransform)
        {
            transform.position = objeTransform.position;
            transform.eulerAngles = objeTransform.eulerAngles;
        }
        
        private void LateUpdate()
        {
            _carCamera.transform.position = transform.position + _cameraOffset;
        }


    }
}
