using Game.HighwaySystem.Base;
using UnityEngine;
using Utils;

namespace Game.SlingSystem.Base
{
    public class SlingTowerBase : MonoBehaviour
    {
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
        
        public int GetDirection()
        {
            if (_parentHighway.Direction == HighwayDirection.RIGHT)
                return 1;

            return -1;
        }
    }
}
