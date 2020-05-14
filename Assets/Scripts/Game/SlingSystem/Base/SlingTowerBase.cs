using Game.HighwaySystem.Base;
using Game.HighwaySystem.HighwayTypes;
using UnityEngine;
using Utils;

namespace Game.SlingSystem.Base
{
    public class SlingTowerBase : MonoBehaviour
    {
        public int ID;
        
        private HighwayBase _parentHighway;
        private Transform _sling;

        public void Initialize(HighwayBase parentHighway)
        {
            _parentHighway = parentHighway;
            _sling = transform.Find("Sling");
        }
        
        public void AddLine(Transform carBase)
        {
            transform.LookAt(carBase);
            _sling.transform.ChangeScaleY(Vector3.Distance(_sling.position,carBase.transform.position) / 4f);
        }

        public void ResetLine()
        {
            _sling.transform.ChangeScaleY(1f);
        }

        public Vector3 GetFirstPosition()
        {
            return _parentHighway.transform.position;
        }

        public bool IsPassed(Transform carBase)
        {
            return Vector3.Distance(carBase.transform.position, _parentHighway.FinishPosition) < 10f;
        }

        public bool IsCloseTo(Transform carBase)
        {
            if (_parentHighway.GetType() == typeof(UCornerHighway))
            {
                Debug.Log(Vector3.Distance(carBase.transform.position, transform.position));
                return Vector3.Distance(carBase.transform.position, transform.position) < 27f;
            }

            return Vector3.Distance(carBase.transform.position, transform.position) < 25f;
        }
        
        public int GetDirection()
        {
            if (_parentHighway.Direction == HighwayDirection.RIGHT)
                return 1;

            return -1;
        }
    }
}
