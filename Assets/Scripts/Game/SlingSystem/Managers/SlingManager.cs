using System.Collections.Generic;
using System.Linq;
using Game.SlingSystem.Base;
using UnityEngine;

namespace Game.SlingSystem.Managers
{
    public class SlingManager
    {
        private List<SlingTowerBase> _slingTowerBases;

        public SlingManager()
        {
            _slingTowerBases = new List<SlingTowerBase>();
        }

        public void Add(SlingTowerBase slingTowerBase)
        {
            if(slingTowerBase == null)
                return;
            
            _slingTowerBases.Add(slingTowerBase);
        }

        public SlingTowerBase GetSling(int index)
        {
            return _slingTowerBases[index];
        }
    }
}
