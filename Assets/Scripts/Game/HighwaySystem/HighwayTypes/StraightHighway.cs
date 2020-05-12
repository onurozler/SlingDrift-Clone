using Game.HighwaySystem.Base;
using UnityEngine;

namespace Game.HighwaySystem.HighwayTypes
{
    public class StraightHighway : HighwayBase
    {
        public override void SetDirection(HighwayDirection direction)
        {
            if(direction == Direction)
                return;
            
            Direction = direction;
            transform.eulerAngles = new Vector3(0,(int)direction);

        }
    }
}
