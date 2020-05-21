
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.LevelSystem.LevelEvents
{
    public static class LevelEventBus
    {
        private static List<LevelEvent> _levelEvents = new List<LevelEvent>
        {
            new LevelEvent(LevelEventType.STARTED),
            new LevelEvent(LevelEventType.LEVEL_UP),
            new LevelEvent(LevelEventType.FAIL)
        };
        
        public static void InvokeEvent(LevelEventType type)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Invoke();
        }
        
        public static void InvokeEvent(LevelEventType type,int eventParams)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Invoke(eventParams);
        }

        public static void SubscribeEvent(LevelEventType type, Action action)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Subscribe(action);
        }
        
        public static void SubscribeEvent(LevelEventType type, Action<int> action)
        {
            var specificEvent = _levelEvents?.FirstOrDefault(x => x.LevelEventType == type);
            specificEvent?.Subscribe(action);
        }
    }
}
