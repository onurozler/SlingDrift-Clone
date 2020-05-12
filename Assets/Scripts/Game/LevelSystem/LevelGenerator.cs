using System.Collections.Generic;
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
             //GenerateLevels();
             var straightHighway = _poolManager.GetAvailableHighWay<UCornerHighway>();
             straightHighway.SetDirection(HighwayDirection.LEFT);

        }

        private void GenerateLevels()
        {
            var highwayDirections = new  List<HighwayDirection>
            {
                HighwayDirection.UP,
                HighwayDirection.LEFT,
                HighwayDirection.RIGHT
            };
            var straightDirection = highwayDirections.GetRandomElementFromList(HighwayDirection.UP);
            var straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
            
            straightHighway.SetDirection(straightDirection);

            for (int i = 0; i < 5; i++)
            {
                var corner = _poolManager.GetAvailableHighWay<CornerHighway>();
                corner.SetDirection(straightDirection);
                corner.transform.position = straightHighway.FinishPosition;
                corner.transform.Rotate(straightHighway.transform.eulerAngles.y * Vector3.up);

                straightDirection =
                    straightDirection == HighwayDirection.LEFT || straightDirection == HighwayDirection.RIGHT
                        ? HighwayDirection.UP
                        : highwayDirections.GetRandomElementFromList(HighwayDirection.UP);
                
                straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
                straightHighway.transform.position = corner.FinishPosition;
                straightHighway.SetDirection(straightDirection);
                
                corner = _poolManager.GetAvailableHighWay<CornerHighway>();
                corner.SetDirection(straightDirection);
                corner.transform.position = straightHighway.FinishPosition;
                corner.transform.Rotate(straightHighway.transform.eulerAngles.y * Vector3.up);
            }
        }
    }
}
