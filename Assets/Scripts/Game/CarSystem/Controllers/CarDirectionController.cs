using DG.Tweening;
using Game.LevelSystem.LevelEvents;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarDirectionController
    {
        private Tween _handleAnim;
        private Transform _carBase;
        
        public CarDirectionController(Transform carBase)
        {
            _carBase = carBase;
            
            LevelEventBus.SubscribeEvent(LevelEventType.FAIL,()=>_handleAnim.Kill());
        }

        public void Handle(Vector3 target)
        {
            Vector3 carFirstTarget = target;
            carFirstTarget.x += 30;

            _handleAnim = _carBase.transform.DOLookAt(carFirstTarget, 0.5f).OnComplete(() =>
                _carBase.transform.DOLookAt(target, 0.4f)
                );
        }
    }
    
}
