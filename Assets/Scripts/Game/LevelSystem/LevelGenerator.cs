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
             GenerateLevels();
        }

        private void GenerateLevels()
        {
            var highwayDirections = new  List<HighwayDirection>
            {
                HighwayDirection.UP,
                HighwayDirection.LEFT,
                HighwayDirection.RIGHT
            };
            var currentDirection = highwayDirections.GetRandomElementFromList();
            var straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
            var corner = _poolManager.GetAvailableHighWay<CornerHighway>();
            
            straightHighway.SetDirection(currentDirection);

            corner.SetDirection(currentDirection);
            corner.transform.position = straightHighway.FinishPosition;
            corner.transform.Rotate(straightHighway.transform.eulerAngles.y * Vector3.up);
            
            
            for (int i = 0; i < 10; i++)
            {
                currentDirection =
                    currentDirection == HighwayDirection.LEFT || currentDirection == HighwayDirection.RIGHT
                        ? HighwayDirection.UP
                        : highwayDirections.GetRandomElementFromList(HighwayDirection.UP);
                
                straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
                straightHighway.transform.position = corner.FinishPosition;
                straightHighway.SetDirection(currentDirection);

                corner = _poolManager.GetAvailableHighWay<CornerHighway>();
                corner.SetDirection(currentDirection);
                corner.transform.position = straightHighway.FinishPosition;
                corner.transform.Rotate(straightHighway.transform.eulerAngles.y * Vector3.up);
            }

           // straightHighway = _poolManager.GetAvailableHighWay<StraightHighway>();
           // straightHighway.SetDirection(HighwayDirection.UP);
           // straightHighway.transform.position = corner.FinishPosition;
            
            
            
            for (int i = 0; i < 5; i++)
            {
               
            }
        }
    }
}
