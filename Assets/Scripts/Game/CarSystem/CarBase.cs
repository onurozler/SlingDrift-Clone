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
            transform.Translate(transform.forward * (Time.deltaTime * 8f),Space.World);
        }
    }
}
