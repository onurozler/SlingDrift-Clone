using System.Collections.Generic;
using System.Linq;
using Game.SlingSystem.Base;
using UnityEngine;

namespace Game.SlingSystem.Managers
{
    public class SlingManager
    {
        private static int _index;
        private List<SlingTowerBase> _slingTowerBases;

        public SlingManager()
        {
            _slingTowerBases = new List<SlingTowerBase>();
            _index = 0;
        }

        public void Add(SlingTowerBase slingTowerBase)
        {
            if(slingTowerBase == null)
                return;
            
            slingTowerBase.ID = _index++;
            _slingTowerBases.Add(slingTowerBase);
        }

        public SlingTowerBase GetSlingByID(int index)
        {
            return _slingTowerBases?.FirstOrDefault(x=>x.ID == index);
        }
    }
}
