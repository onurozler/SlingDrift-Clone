using Config;
using DG.Tweening;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarController : MonoBehaviour
    {
        private SlingManager _slingManager;
        private Tween _carAnimation;

        
        public bool IsActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
            
            //_carAnimation = transform.DOShakeRotation(0.2f,transform.up * 5f).SetLoops(-1);
        }
        
        public void Initialize()
        {
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
                    //_carAnimation.Pause();
                    closestSling.AddLine(transform);
                    transform.RotateAround(closestSling.transform.position,closestSling.transform.up * closestSling.GetDirection(), 
                        Time.deltaTime * closestSling.GetAxis());
                }
            }
            else
            {
                closestSling.ResetLine();
                
               // if(!_carAnimation.IsPlaying())
                    //_carAnimation.Play();
            }

        }
    }
}
