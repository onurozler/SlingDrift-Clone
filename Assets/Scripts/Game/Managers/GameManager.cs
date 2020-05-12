using Game.CarSystem;
using Game.CarSystem.Base;
using Game.LevelSystem.Controllers;
using Game.LevelSystem.Managers;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        private LevelGenerator _levelGenerator;
        private LevelManager _levelManager;
        private CarBase _carBase;

        [Inject]
        private void OnInstaller(LevelManager levelManager, LevelGenerator levelGenerator, CarBase carBase)
        {
            _levelManager = levelManager;
            _levelGenerator = levelGenerator;
            _carBase = carBase;
        }
        
        private void Awake()
        {
            _levelGenerator.Initialize();
            _carBase.Initialize(_levelManager.GetHighwayOfLevel(0,0).transform);
        }

        private void Update()
        {
            
            return;
           // _slingTowerBase.transform.LookAt(_carBase.transform);
           // _test.transform.localScale = new Vector3(_test.transform.localScale.x,Vector3.Distance(_test.position,_carBase.transform.position)/4f,_test.transform.localScale.z);
            
            if (Input.GetMouseButton(0))
            {
               // _slingTowerBase.OnClicking(_carBase);
            }            
        }
    }
}
