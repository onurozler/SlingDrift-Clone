using Config;
using Game.SlingSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.CarSystem.Controllers
{
    public class CarController : MonoBehaviour
    {
        private SlingManager _slingManager;
        
        public bool IsActive;
        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
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
        }
        
        private void Move()
        {
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);

            var closestSling = _slingManager.GetClosesSling(transform.position);
            if (Vector3.Distance(closestSling.transform.position,transform.position) < 20f)
            {
                Debug.Log("TEST");
            }
        }
    }
}
