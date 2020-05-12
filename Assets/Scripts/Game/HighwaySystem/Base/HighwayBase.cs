using UnityEngine;

namespace Game.HighwaySystem.Base
{
    public abstract class HighwayBase : MonoBehaviour
    {
        public bool IsActive;
        public Transform FinishPoint { get; protected set; }
        public Transform FirstPoint { get; protected set; }

        public virtual void Initialize()
        {
            FirstPoint = transform;
            FinishPoint = transform.Find("FinishPosition");
            Deactivate();
        }

        public virtual void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
    
}
