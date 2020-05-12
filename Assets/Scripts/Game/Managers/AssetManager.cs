using System;
using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using UnityEngine;

namespace Game.Managers
{
    public class AssetManager
    {
        private const string HIGHWAY_PATH = "HighwayPrefabs/";
        
        private List<HighwayBase> _highwayBases;
        
        public AssetManager()
        {
            _highwayBases = Resources.LoadAll<HighwayBase>(HIGHWAY_PATH).ToList();
        }

        public HighwayBase GetHighWayByType(Type highwayType)
        {
            return _highwayBases?.FirstOrDefault(x => x.GetType() == highwayType);
        }
    }
}
