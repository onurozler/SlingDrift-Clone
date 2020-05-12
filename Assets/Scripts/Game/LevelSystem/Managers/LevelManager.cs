using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using UnityEngine;

namespace Game.LevelSystem.Managers
{
    public class LevelManager
    {
        private List<LevelData> _levelDatas;

        public LevelManager()
        {
            _levelDatas = new List<LevelData>();
        }
        
        public void AddLevel(int levelIndex, HighwayBase highwayBase)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex) ?? new LevelData(levelIndex);
            level.AllLevelHighways.Add(highwayBase);
        }

        public void DeleteLevel(int levelIndex)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex);
            if(level == null)
                return;
            
            level.AllLevelHighways.ForEach(x=>x.Deactivate());
            _levelDatas.Remove(level);
        }
    }
}
