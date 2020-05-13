using Game.LevelSystem.LevelEvents;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Text _levelUpText;
        [SerializeField]
        private Text _counterText;

        public void Initialize()
        {
            _levelUpText.enabled = false;
            _counterText.enabled = false;
            
            LevelEventBus.SubscribeEvent(LevelEventType.LEVEL_UP,OnLevelUp);
        }

        private void OnLevelUp()
        {
            _levelUpText.enabled = true;
            Timer.Instance.TimerWait(1f, () => _levelUpText.enabled = false);
        }
    }
}
