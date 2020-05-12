using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using Game.LevelSystem.LevelEvents;

namespace Game.LevelSystem.Managers
{
    public class LevelManager
    {
        private List<LevelData> _levelDatas;
        private int _deleteIndex;

        public LevelManager()
        {
            _levelDatas = new List<LevelData>();
            _deleteIndex = 0;
            
            LevelEventBus.SubscribeEvent(LevelEventType.LEVEL_UP, ()=>DeleteLevel(_deleteIndex++));
        }
        
        public void AddLevel(int levelIndex, HighwayBase highwayBase)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex) ?? new LevelData(levelIndex);
            _levelDatas.Add(level);
            level.AllLevelHighways.Add(highwayBase);
        }

        public HighwayBase GetHighwayOfLevel(int levelIndex,int highwayIndex)
        {
            return _levelDatas?.FirstOrDefault(x => x.LevelIndex == levelIndex)?.AllLevelHighways[highwayIndex];
        }

        private void DeleteLevel(int levelIndex)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex);
            if(level == null)
                return;
            
            level.AllLevelHighways.ForEach(x=>x.Deactivate());
            _levelDatas.Remove(level);
        }
    }
}
