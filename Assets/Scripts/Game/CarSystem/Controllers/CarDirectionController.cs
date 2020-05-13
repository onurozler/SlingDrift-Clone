using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.CarSystem.Controllers
{
    public class CarDirectionController
    {
        private Transform _carBase;
        private CarDirection _currentDirection;
        private List<CarDirection> _directions;

        public CarDirectionController(Transform carBase)
        {
            _carBase = carBase;
            _directions = new List<CarDirection>
            {
                CarDirection.UP, CarDirection.LEFT,CarDirection.RIGHT
            };
        }

        public void HandleDirection()
        {
            var ordered = _directions.OrderBy(x => 
                Mathf.Abs((int) x - _carBase.transform.eulerAngles.y)).ToList();

            _currentDirection = ordered[0];
            Test();
        }

        private void Test()
        {
            var carY = _carBase.transform.eulerAngles.y;

            Debug.Log(carY);
            Debug.Log((int)_currentDirection);
            
            if (carY > (int)_currentDirection)
            {
                var test2=
                new Vector3(_carBase.transform.eulerAngles.x,
                    _carBase.transform.eulerAngles.y - (carY - (int) _currentDirection),
                    _carBase.transform.eulerAngles.z);

                _carBase.transform.DORotate(test2, 0.2f);
            }
            else
            {
                
            }
        }
    }
    
    public enum CarDirection
    {
        UP = 90,
        LEFT = 0,
        RIGHT = 180,
    }
}
