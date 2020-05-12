using System.Collections.Generic;
using Game.HighwaySystem.Base;

namespace Game.LevelSystem
{
    public class LevelData
    {
        public int LevelIndex;
        public List<HighwayBase> AllLevelHighways;

        public LevelData(int levelIndex)
        {
            LevelIndex = levelIndex;
            AllLevelHighways = new List<HighwayBase>();
        }
    }
}
