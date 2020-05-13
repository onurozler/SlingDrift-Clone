using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using Game.LevelSystem.LevelEvents;
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

        public T GetAvailableHighWay<T>() where T : HighwayBase
        {
            T highway = _highwayBases?.FirstOrDefault(x => x.GetType() == typeof(T) && !x.IsActive) as T;
            if (highway == null)
            {
                highway = _assetManager.GetHighWayByType(typeof(T)) as T;
                highway = Instantiate(highway, transform);
                highway.Initialize();
                _highwayBases?.Add(highway);
            }
            highway.Activate();
            return highway;
        }

        public T GetAvailableHighWay<T>(Vector3 position) where T : HighwayBase
        {
            var highWay = GetAvailableHighWay<T>();
            highWay.transform.position = position;
            return highWay;
        }

        public void DeactivateWholePool()
        {
            _highwayBases.ForEach(x=>x.Deactivate());
        }
    }
}
