using Config;
using DG.Tweening;
using Game.CarSystem.Base;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarController : MonoBehaviour
    {
        private CarAnimationController _carAnimationController;
        private SlingManager _slingManager;
        private Tween _carAnimation;
        
        public bool IsActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
        }
        
        public void Initialize(CarAnimationController carAnimationController)
        {
            _carAnimationController = carAnimationController;
            IsActive = true;
        }
        private void Update()
        {
            if(!IsActive)
                return;
            
            Move();
            CheckInput();
        }
        
        private void Move()
        {
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }

        private void CheckInput()
        {
            var closestSling = _slingManager.GetClosesSling(transform.position);
            if (Input.GetMouseButton(0))
            {
                if (Vector3.Distance(closestSling.transform.position,transform.position) < 25f)
                {
                    _carAnimationController.Pause();
                    closestSling.AddLine(transform);
                    transform.RotateAround(closestSling.transform.position,closestSling.transform.up * closestSling.GetDirection(), 
                        Time.deltaTime * closestSling.GetAxis());
                    transform.Rotate(0,closestSling.GetDirection() * closestSling.GetAxis() * Time.deltaTime,0);
                }
            }
            else
            {
                closestSling.ResetLine();
                _carAnimationController.Play();
            }

        }
    }
}
