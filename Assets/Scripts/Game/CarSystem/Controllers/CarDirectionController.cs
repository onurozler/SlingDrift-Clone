using DG.Tweening;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarDirectionController
    {
        private Transform _carBase;
        
        public CarDirectionController(Transform carBase)
        {
            _carBase = carBase;
        }

        public void Handle(Vector3 target)
        {
            _carBase.transform.LookAt(target);
            
            //var rotationOfCar = _carBase.localEulerAngles;
            //rotationOfCar.y *= -1;

            //_carBase.DOLocalRotate(rotationOfCar, 0.5f).OnComplete(() => {  });
        }
    }
    
}
