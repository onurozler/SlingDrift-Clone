using System;
using Utils;

namespace Game.LevelSystem.LevelEvents
{
    public class LevelEvent
    {
        private event Action _levelAction;
        private event Action<int> _levelActionWithParam;
        public LevelEventType LevelEventType;
        
        public LevelEvent(LevelEventType levelEventType)
        {
            LevelEventType = levelEventType;
        }

        public void Invoke()
        {
            _levelAction.SafeInvoke();
        }
        
        public void Invoke(int param)
        {
            _levelActionWithParam.SafeInvoke(param);
        }

        public void Subscribe(Action<int> action)
        {
            _levelActionWithParam += action;
        }
        
        public void Subscribe(Action action)
        {
            _levelAction += action;
        }
    }
    
    public enum LevelEventType
    {
        STARTED,
        LEVEL_UP,
        FAIL
    }
}
