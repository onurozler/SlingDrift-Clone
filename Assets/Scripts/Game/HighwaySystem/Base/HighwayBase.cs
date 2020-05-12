using UnityEngine;

namespace Game.HighwaySystem.Base
{
    public abstract class HighwayBase : MonoBehaviour
    {
        #region Public Properties

        public bool IsActive;
        public HighwayDirection Direction;

        #endregion

        
        private Transform _finishTransform;
        
        public virtual void Initialize()
        {
            _finishTransform = transform.Find("FinishPosition");
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

    public enum HighwayDirection
    {
        UP,
        LEFT,
        RIGHT
    }
}
