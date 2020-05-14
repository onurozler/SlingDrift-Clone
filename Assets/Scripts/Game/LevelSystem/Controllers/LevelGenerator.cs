using System.Collections.Generic;
using Config;
using Game.CarSystem.Base;
using Game.HighwaySystem.Base;
using Game.HighwaySystem.HighwayTypes;
using Game.LevelSystem.LevelEvents;
using Game.LevelSystem.Managers;
using Game.Managers;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.LevelSystem.Controllers
{
    public class LevelGenerator : MonoBehaviour
    {
        private static int _levelIndex;
        
        private PoolManager _poolManager;
        private LevelManager _levelManager;
        private FinalHighway _finalHighway;
        private CarBase _carBase;

        [Inject]
        private void OnInstaller(PoolManager poolManager,LevelManager levelManager, CarBase carBase)
        {
            _carBase = carBase;
            _poolManager = poolManager;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL, () =>
            {
                _finalHighway = null;
                _levelIndex = 0;
                _levelManager.DeleteWholeLevels();
            });
            
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=>
            {
                GenerateLevels(3);
                _carBase.SetCarPosition(_levelManager.GetHighwayOfLevel(0,0).transform);
            });
            LevelEventBus.SubscribeEvent(LevelEventType.LEVEL_UP, () =>
            {
                Timer.Instance.TimerWait(5f, () => GenerateLevels(1));
            });
        }
        
        private void GenerateLevels(int levelCount)
        {
            var highwayDirections = new  List<HighwayDirection>
            {
                HighwayDirection.UP,
                HighwayDirection.LEFT,
                HighwayDirection.RIGHT
            };
            var straightDirection = highwayDirections.GetRandomElementFromList();
            var cornerDirection = straightDirection;

            HighwayBase straightHighway = null;
            HighwayBase corner = null;

            for (int j = 0; j < levelCount; j++)
            {
                for (int i = 0; i < GameConfig.LEVEL_LENGTH; i++)
                {
                    if (straightDirection == cornerDirection)
                        straightDirection = HighwayDirection.UP;

                    if (_finalHighway == null)
                        straightHighway = GenerateHighway<StraightHighway>(straightDirection, corner, false);
                    else
                        straightHighway = _finalHighway;

                    if (straightDirection == HighwayDirection.UP)
                    {
                        cornerDirection = highwayDirections.GetRandomElementFromList(HighwayDirection.UP);
                        corner = GenerateHighway<CornerHighway>(cornerDirection, straightHighway);

                        straightDirection = cornerDirection == HighwayDirection.LEFT
                            ? HighwayDirection.RIGHT
                            : HighwayDirection.LEFT;
                        straightHighway = GenerateHighway<StraightHighway>(straightDirection, corner, false);

                        // Random U Corner Generation
                        int rnd = Random.Range(0, 10);
                        if (rnd > GameConfig.U_CORNER_PROBABILITY)
                        {
                            cornerDirection = straightDirection;
                            straightDirection = cornerDirection == HighwayDirection.LEFT
                                ? HighwayDirection.RIGHT
                                : HighwayDirection.LEFT;
                            corner = GenerateHighway<UCornerHighway>(cornerDirection, straightHighway, false);
                            _finalHighway = null;
                            continue;
                        }
                    }

                    _finalHighway = null;
                    cornerDirection = straightDirection;
                    corner = GenerateHighway<CornerHighway>(cornerDirection, straightHighway);
                }

                if (straightDirection == cornerDirection)
                    straightDirection = HighwayDirection.UP;

                _finalHighway = GenerateHighway<FinalHighway>(straightDirection, corner, false);
                _levelIndex++;
            }
        }

        private T GenerateHighway<T>(HighwayDirection cornerDirection, HighwayBase highwayBase, bool rotate = true) 
            where T:HighwayBase
        {
            var highway = _poolManager.GetAvailableHighWay<T>();
            highway.SetDirection(cornerDirection);
            if (highwayBase != null)
            {
                highway.transform.position = highwayBase.FinishPosition;
                if(rotate)
                    highway.transform.Rotate(highwayBase.transform.eulerAngles.y * Vector3.up);
            }
            
            _levelManager.AddLevel(_levelIndex,highway);
            return highway;
        }

    }
}
