using Game.CarSystem.Base;
using UnityEngine;
using Utils;

namespace Game.SlingSystem.Base
{
    public class SlingTowerBase : MonoBehaviour
    {
        private CarBase _currentCar;
        
        public void AddLine(Transform carBase)
        {
            var sling = transform.Find("Sling");
            transform.LookAt(carBase);
            sling.transform.ChangeScaleY(Vector3.Distance(sling.position,carBase.transform.position) / 4f);
        }

        public void ResetLine()
        {
            var sling = transform.Find("Sling");
            sling.transform.ChangeScaleY(1f);
        }
    }
}
