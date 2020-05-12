using Game.HighwaySystem.Base;
using Game.HighwaySystem.HighwayTypes;
using Game.Managers;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.LevelSystem
{
    public class LevelGenerator : MonoBehaviour
    {
        private PoolManager _poolManager;
        
        [Inject]
        private void OnInstaller(PoolManager poolManager)
        {
            _poolManager = poolManager;
             GenerateLevels();
        }

        private void GenerateLevels()
        {
            var straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
            var corner = _poolManager.GetAvailableHighWay<CornerHighway>();
            corner.SetDirection(HighwayDirection.RIGHT);
            corner.transform.position = straightHighway.FinishPosition;
            
            straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
            straightHighway.SetDirection(HighwayDirection.UP);
            straightHighway.transform.position = corner.FinishPosition;
            
            //var lastPos = straightHighway.FinishTransform;
            for (int i = 0; i < 5; i++)
            {
                //AddCorner(straightHighway.Direction);
            }
        }
    }
}
