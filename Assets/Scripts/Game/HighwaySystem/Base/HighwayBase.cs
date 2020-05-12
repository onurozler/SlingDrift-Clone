﻿using UnityEngine;

namespace Game.HighwaySystem.Base
{
    public abstract class HighwayBase : MonoBehaviour
    {
        #region Public Properties

        public bool IsActive;
        public HighwayDirection Direction;
        public Vector3 FinishPosition => transform.Find("FinishPosition").position;

        #endregion

        
        private Transform _finishTransform;
        
        public virtual void Initialize()
        {
            Deactivate();
        }

        public abstract void SetDirection(HighwayDirection direction);

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
        UP = 90,
        LEFT = 180,
        RIGHT = 0
    }
}