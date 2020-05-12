using Game.CarSystem;
using Game.SlingSystem.Base;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        private CarBase _carBase;
        private SlingTowerBase _slingTowerBase;
        private Transform _test;
        
        private void Awake()
        {
            _carBase = FindObjectOfType<CarBase>();
            _slingTowerBase = FindObjectOfType<SlingTowerBase>();

            _test = _slingTowerBase.transform.Find("TEST");
        }

        private void Update()
        {
            
            return;
            _slingTowerBase.transform.LookAt(_carBase.transform);
            _test.transform.localScale = new Vector3(_test.transform.localScale.x,Vector3.Distance(_test.position,_carBase.transform.position)/4f,_test.transform.localScale.z);
            
            if (Input.GetMouseButton(0))
            {
                _slingTowerBase.OnClicking(_carBase);
            }            
        }
    }
}
