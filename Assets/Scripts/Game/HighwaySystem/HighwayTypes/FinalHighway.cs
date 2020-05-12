using Game.HighwaySystem.Base;
using UnityEngine;

namespace Game.HighwaySystem.HighwayTypes
{
    public class FinalHighway : HighwayBase
    {
        public override void SetDirection(HighwayDirection direction)
        {
            if(direction == Direction || direction == HighwayDirection.UP)
                return;
            
            Direction = direction;
            transform.eulerAngles = new Vector3(0,(int)direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Game Finished!");
        }
    }
}
