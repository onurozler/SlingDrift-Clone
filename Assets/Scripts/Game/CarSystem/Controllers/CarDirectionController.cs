using DG.Tweening;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarDirectionController
    {
        private Transform _carBase;
        private CarAnimationController _carAnimationController;
        
        public CarDirectionController(Transform carBase, CarAnimationController carAnimationController)
        {
            _carBase = carBase;
            _carAnimationController = carAnimationController;
        }

        public void Handle(Vector3 target)
        {
            Vector3 carFirstTarget = target;
            carFirstTarget.x += 30;

            _carBase.transform.DOLookAt(carFirstTarget, 0.5f).OnComplete(() =>
            {
                _carBase.transform.DOLookAt(target, 0.4f).
                    OnComplete(_carAnimationController.Play);
            });
        }
    }
    
}
