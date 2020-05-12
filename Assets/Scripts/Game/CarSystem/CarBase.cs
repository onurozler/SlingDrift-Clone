using Config;
using UnityEngine;

namespace Game.CarSystem
{
    public class CarBase : MonoBehaviour
    {
        public bool IsActive;

        public void Initialize(Transform objeTransform)
        {
            IsActive = true;
            transform.position = objeTransform.position;
            transform.eulerAngles = objeTransform.eulerAngles;
        }
        
        private void Update()
        {
            if(!IsActive)
                return;
            
            Move();
        }

        public void Move()
        {
            transform.Translate(transform.forward * (Time.deltaTime * GameConfig.CAR_SPEED),Space.World);
        }
    }
}
