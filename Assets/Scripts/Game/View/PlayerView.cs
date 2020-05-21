using Game.LevelSystem.LevelEvents;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] 
        private Button _startButton;
        [SerializeField]
        private Text _levelUpText;
        [SerializeField]
        private Text _counterText;

        private int _gamePlayCount = 0;
        
        public void Initialize()
        {
            ButtonVisible(true);
            _levelUpText.enabled = false;
            _counterText.enabled = false;
            
            _startButton.onClick.AddListener(() => LevelEventBus.InvokeEvent(LevelEventType.STARTED));
            _startButton.onClick.AddListener(() => LevelEventBus.InvokeEvent(LevelEventType.STARTED , ++_gamePlayCount));
            
            LevelEventBus.SubscribeEvent(LevelEventType.STARTED, ()=>
            {
                UpdateCounter(0);
                _counterText.enabled = true;
                ButtonVisible(false);
            });
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL, ()=>ButtonVisible(true));
            LevelEventBus.SubscribeEvent(LevelEventType.LEVEL_UP,OnLevelUp);
        }

        public void UpdateCounter(int count)
        {
            _counterText.text = count.ToString();
        }
        
        private void OnLevelUp()
        {
            _levelUpText.enabled = true;
            Timer.Instance.TimerWait(1f, () => _levelUpText.enabled = false);
        }

        private void ButtonVisible(bool isVisible)
        {
            _startButton.gameObject.SetActive(isVisible);
        }
    }
}
