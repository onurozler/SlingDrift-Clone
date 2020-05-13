using Config;
using DG.Tweening;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarMovementController : MonoBehaviour
    {
        private CarDirectionController _carDirectionController;
        private CarAnimationController _carAnimationController;
        private SlingManager _slingManager;
        
        public bool IsActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
        }
        
        public void Initialize(CarAnimationController carAnimationController)
        {
            _carAnimationController = carAnimationController;
            _carDirectionController = new CarDirectionController(transform);
            IsActive = true;
        }
        private void Update()
        {
            if(!IsActive)
                return;
            
            CheckInput();
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
                        Time.deltaTime * GameConfig.CAR_ROTATING);
                    transform.Rotate(0,closestSling.GetDirection() * GameConfig.CAR_ROTATING * Time.deltaTime/4,0);
                }
            }
            else
            {
                transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);

                /*
                if (transform.position.x < closestSling.GetFinishParent())
                { 
                    transform.Rotate(0,5,0);   
                }
                else
                {
                    
                }*/
                

                //transform.position = new Vector3(closestSling.GetFinishParent(),transform.position.y,transform.position.z);
                
                //_carDirectionController.HandleDirection();
                closestSling.ResetLine();
                //_carAnimationController.Play();
            }

        }
    }
}
