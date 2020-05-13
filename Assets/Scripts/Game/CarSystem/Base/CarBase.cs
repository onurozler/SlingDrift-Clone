using Game.CarSystem.Controllers;
using UnityEngine;

namespace Game.CarSystem.Base
{
    public class CarBase : MonoBehaviour
    {
        [SerializeField] 
        private Camera _carCamera;
        private Vector3 _cameraOffset;

        private CarController _carController;
        private CarAnimationController _carAnimationController;
        
        public void Initialize(Transform objeTransform)
        {
            _carCamera = _carCamera == null ? Camera.main : _carCamera;
            _cameraOffset = _carCamera.transform.position - transform.position;
            transform.position = objeTransform.position;
            transform.eulerAngles = objeTransform.eulerAngles;
            
            _carAnimationController = new CarAnimationController(transform);
            _carController = GetComponent<CarController>();
            _carController.Initialize(_carAnimationController);
        }

        private void LateUpdate()
        {
            _carCamera.transform.position = transform.position + _cameraOffset;
        }


    }
}
