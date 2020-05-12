using Config;
using UnityEngine;

namespace Game.CarSystem
{
    public class CarBase : MonoBehaviour
    {
        public bool IsActive;

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }
    }
}
