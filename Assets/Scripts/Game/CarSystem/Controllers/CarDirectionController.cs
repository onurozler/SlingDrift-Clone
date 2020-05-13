using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Game.CarSystem.Controllers
{
    public class CarDirectionController
    {
        private Transform _carBase;
        private List<CarDirection> _directions;
        private CarDirection _currentDirection;


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
            var carY = _carBase.transform.eulerAngles.y;
            Debug.Log(carY);
            var ordered = _directions.OrderBy(x => 
                Mathf.Abs((int) _currentDirection - carY)).ToList();

            Debug.Log(ordered[0]);
        }

        private void Test()
        {
            var carY = _carBase.transform.eulerAngles.y;
            _carBase.DORotate(_carBase.up * -carY, 0.5f).OnComplete(() =>
                {
                    _carBase.DORotate(_carBase.up * (int)_currentDirection, 0.5f);
                });
            _currentDirection = CarDirection.NONE;
        }
    }
    
    public enum CarDirection
    {
        NONE,
        UP = 90,
        LEFT = 360,
        RIGHT = 180
    }
}
