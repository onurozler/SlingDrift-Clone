﻿using System.Collections.Generic;
using Config;
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
        private FinalHighway _finalHighway;

        [Inject]
        private void OnInstaller(PoolManager poolManager)
        {
            _poolManager = poolManager;
            _finalHighway = null;
            
             GenerateLevels(3);
             GenerateLevels(1);
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
            }
        }

        private T GenerateHighway<T>(HighwayDirection cornerDirection, HighwayBase highwayBase, bool rotate = true) 
            where T:HighwayBase
        {
            var corner = _poolManager.GetAvailableHighWay<T>();
            corner.SetDirection(cornerDirection);
            if (highwayBase != null)
            {
                corner.transform.position = highwayBase.FinishPosition;
                if(rotate)
                    corner.transform.Rotate(highwayBase.transform.eulerAngles.y * Vector3.up);
            }
            return corner;
        }
        
        /*
        private void HandleDirectionLogic(ref HighwayDirection straight, HighwayDirection corner)
        {
            if (straight == corner)
            {
                straight = HighwayDirection.UP;
                return;
            }
            
            straight = corner == HighwayDirection.LEFT
                ? HighwayDirection.RIGHT
                : HighwayDirection.LEFT;
        }*/
        
    }
}
