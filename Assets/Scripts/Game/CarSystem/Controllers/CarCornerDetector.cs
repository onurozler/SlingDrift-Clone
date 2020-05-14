using Game.HighwaySystem.HighwayTypes;
using Game.LevelSystem.LevelEvents;
using UnityEngine;
using Utils;

namespace Game.CarSystem.Controllers
{
    public class CarCornerDetector
    {
        private bool _isActive;
        
        private Ray _ray;
        private RaycastHit _raycastHit;
        private Transform _carBase;
        private int _layerMask;

        public CarCornerDetector(Transform carBase)
        {
            _carBase = carBase;
            _ray = new Ray();
            _layerMask = 1 << 8;
            _isActive = true;
        }
        
        public void Detect()
        {
            if(!_isActive)
                return;
            
            _ray.origin = _carBase.transform.position;
            _ray.direction = _carBase.transform.forward;
            
            if (Physics.Raycast(_ray,out _raycastHit,3f,_layerMask))
            {
                var final = _raycastHit.collider.GetComponentInParent<FinalHighway>();
                if (final != null)
                {
                    final.FinishLevel(_carBase);
                    DisableDetectorForSeconds(2);
                }
                else
                {
                    DisableDetectorForSeconds(1);
                    LevelEventBus.InvokeEvent(LevelEventType.FAIL);
                }
            }
        }

        private void DisableDetectorForSeconds(float seconds)
        {
            _isActive = false;
            Timer.Instance.TimerWait(seconds, () => _isActive = true);
        }
    }
}
