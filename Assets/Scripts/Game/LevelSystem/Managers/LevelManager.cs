using System.Collections.Generic;
using System.Linq;
using Game.HighwaySystem.Base;
using Game.LevelSystem.LevelEvents;
using Game.SlingSystem.Base;
using Game.SlingSystem.Managers;
using Utils;
using Zenject;

namespace Game.LevelSystem.Managers
{
    public class LevelManager
    {
        private SlingManager _slingManager;
        private List<LevelData> _levelDatas;
        private int _deleteIndex;

        
        [Inject]
        private void OnInstaller(SlingManager slingManager)
        {
            _slingManager = slingManager;
            
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, () => _deleteIndex = 0);
            LevelEventBus.SubscribeEvent(LevelEventType.LEVEL_UP, ()=>DeleteLevel(_deleteIndex++));
        }
        
        public LevelManager()
        {
            _levelDatas = new List<LevelData>();
        }
        
        public void AddLevel(int levelIndex, HighwayBase highwayBase)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex);
            if (level == null)
            {
                level = new LevelData(levelIndex);
                _levelDatas.Add(level);
            }
            level.AllLevelHighways.Add(highwayBase);
            _slingManager.Add(highwayBase.GetComponentInChildren<SlingTowerBase>());
        }

        public HighwayBase GetHighwayOfLevel(int levelIndex,int highwayIndex)
        {
            return _levelDatas?.FirstOrDefault(x => x.LevelIndex == levelIndex)?.AllLevelHighways[highwayIndex];
        }

        public void DeleteWholeLevels()
        {
            if(_levelDatas.Count <= 0)
                return;
            
            foreach (var level in _levelDatas)
            {
                level.AllLevelHighways.ForEach(x=>x.Deactivate());
            }
            _levelDatas.Clear();
            _slingManager.Reset();
        }

        private void DeleteLevel(int levelIndex)
        {
            var level = _levelDatas.FirstOrDefault(x => x.LevelIndex == levelIndex);
            if(level == null)
                return;

            Timer.Instance.TimerWait(4f, () =>
            {
                level.AllLevelHighways.ForEach(x =>
                {
                    var sling = x.GetComponentInChildren<SlingTowerBase>();
                    if (sling != null)
                        _slingManager.Delete(sling.ID);
                    
                    x.Deactivate();
                });
            });
            _levelDatas.Remove(level);
        }
    }
}
