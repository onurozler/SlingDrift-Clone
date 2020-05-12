using Config;
using DG.Tweening;
using UnityEngine;

namespace Game.CarSystem.Base
{
    public class CarBase : MonoBehaviour
    {
        [SerializeField] 
        private Camera _carCamera;
        private Vector3 _cameraOffset;
        public bool IsActive;

        public void Initialize(Transform objeTransform)
        {
            IsActive = true;
            _carCamera = _carCamera == null ? Camera.main : _carCamera;
            _cameraOffset = _carCamera.transform.position - transform.position;
            transform.position = objeTransform.position;
            transform.eulerAngles = objeTransform.eulerAngles;
            
            transform.DOShakeRotation(0.2f,Vector3.up * 5f).SetLoops(-1);
        }
        
        private void Update()
        {
            if(!IsActive)
                return;
            
            
            Move();
        }

        private void LateUpdate()
        {
            _carCamera.transform.position = transform.position + _cameraOffset;
        }

        private void Move()
        {
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }
    }
}
