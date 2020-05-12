using Game.HighwaySystem.Base;
using Game.HighwaySystem.HighwayTypes;
using Game.Managers;
using UnityEngine;
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
           // GenerateLevels();
        }

        private void GenerateLevels()
        {
            var straightHighway = _poolManager.GetAvailableHighWay(HighwayType.STRAIGHT) as StraightHighway;
            var lastPos = straightHighway.FinishPoint;
            for (int i = 0; i < 5; i++)
            {
                AddCorner(straightHighway.Direction);
            }
        }

        private void AddCorner(StraightDirection direction)
        {
            var corner = _poolManager.GetAvailableHighWay(HighwayType.CORNER) as CornerHighway;
            
        }
    }
}
