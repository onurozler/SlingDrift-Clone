using Game.CarSystem.Controllers;
using UnityEngine;

namespace Game.CarSystem.Base
{
    public class CarBase : MonoBehaviour
    {
        [SerializeField] 
        private Camera _carCamera;
        private Vector3 _cameraOffset;

        private CarMovementController _carMovementController;
        private CarAnimationController _carAnimationController;
        
        public void Initialize()
        {
            _carCamera = _carCamera == null ? Camera.main : _carCamera;
            _cameraOffset = _carCamera.transform.position - transform.position;
            
            _carAnimationController = new CarAnimationController(transform);
            _carMovementController = GetComponent<CarMovementController>();
            _carMovementController.Initialize(_carAnimationController);
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
