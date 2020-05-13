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

        public SlingTowerBase GetClosestSling(Vector3 carPos)
        {
            var ordered =_slingTowerBases.OrderBy(x => 
                Vector3.Distance(x.transform.position, carPos)).ToList();
            
            return ordered[0];
        }
    }
}
