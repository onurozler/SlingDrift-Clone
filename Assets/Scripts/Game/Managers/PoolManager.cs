using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class PoolManager : MonoBehaviour
    {
        private AssetManager _assetManager;
        private List<HighwayBase> _highwayBases;
        
        [Inject]
        private void OnInstaller(AssetManager assetManager)
        {
            _assetManager = assetManager;
            _highwayBases = GetComponentsInChildren<HighwayBase>(true)?.ToList() ?? new List<HighwayBase>();
        }

        public HighwayBase GetAvailableHighWay(HighwayType highwayType)
        {
            var highway = _highwayBases?.FirstOrDefault(x => x.HighwayType == highwayType && !x.IsActive);
            if (highway == null)
            {
                highway = _assetManager.GetHighWayByType(highwayType);
                highway = Instantiate(highway, transform);
                highway.Initialize();
                _highwayBases?.Add(highway);
            }
            highway.Activate();
            return highway;
        }

        public HighwayBase GetAvailableHighWay(HighwayType highwayType, Vector3 position)
        {
            var highWay = GetAvailableHighWay(highwayType);
            highWay.transform.position = position;
            return highWay;
        }

        public void DeactivateWholePool()
        {
            foreach (var highwayBase in _highwayBases)
            {
                highwayBase.Deactivate();
            }
        }
    }
}
