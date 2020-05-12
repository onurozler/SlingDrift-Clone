using Game.HighwaySystem.Base;
using UnityEngine;
using Utils;

namespace Game.HighwaySystem.HighwayTypes
{
    public class UCornerHighway : HighwayBase
    {
        public override void SetDirection(HighwayDirection direction)
        {
            if(direction == Direction)
                return;
            
            Direction = direction;
            
            transform.eulerAngles = new Vector3(0,(int)direction);
            transform.ChangePositionWithChild("FinishPosition");
        }
    }
}
