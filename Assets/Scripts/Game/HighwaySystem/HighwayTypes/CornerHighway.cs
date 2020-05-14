using System;
using Game.HighwaySystem.Base;
using UnityEngine;
using Utils;

namespace Game.HighwaySystem.HighwayTypes
{
    public class CornerHighway : HighwayBase
    {
        
        public override void SetDirection(HighwayDirection direction)
        {
            if(direction == Direction || direction == HighwayDirection.UP)
                return;
        
            Direction = direction;
            transform.eulerAngles = new Vector3(0,(int)direction * 0.5f);
            transform.ChangePositionWithChild("FinishPosition");
        }
    }
}
