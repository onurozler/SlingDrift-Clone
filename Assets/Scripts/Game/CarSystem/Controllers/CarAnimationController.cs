using DG.Tweening;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarAnimationController
    {
        private Tween _carAnimation;
        private Transform _carBase;
        
        public CarAnimationController(Transform carBase)
        {
            _carBase = carBase;
        }

        public void Play()
        {
            if (_carAnimation == null || !_carAnimation.IsPlaying())
            {
                _carAnimation?.Kill();
                _carAnimation = _carBase.DOShakeRotation(0.2f,_carBase.up * 6f).SetLoops(-1);
            }
        }

        public void Pause()
        {
            _carAnimation.Pause();
        }
    }
}
