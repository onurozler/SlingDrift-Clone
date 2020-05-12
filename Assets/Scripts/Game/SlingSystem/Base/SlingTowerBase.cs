using Game.CarSystem;
using Game.CarSystem.Base;
using UnityEngine;

namespace Game.SlingSystem.Base
{
    public class SlingTowerBase : MonoBehaviour
    {
        private CarBase _currentCar;
        
        public void OnClicking(CarBase carBase)
        {
            carBase.transform.SetParent(transform);
            
            transform.Rotate(Vector3.up * Time.deltaTime * 30f);
        }
    }
}
