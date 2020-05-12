﻿using Game.HighwaySystem.Base;
using Game.LevelSystem.LevelEvents;
using UnityEngine;

namespace Game.HighwaySystem.HighwayTypes
{
    public class FinalHighway : HighwayBase
    {
        public override void SetDirection(HighwayDirection direction)
        {
            if(direction == Direction)
                return;
            
            Direction = direction;
            transform.eulerAngles = new Vector3(0,(int)direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("LEVEL FINISHED!");
            LevelEventBus.InvokeEvent(LevelEventType.LEVEL_UP);
        }
    }
}
