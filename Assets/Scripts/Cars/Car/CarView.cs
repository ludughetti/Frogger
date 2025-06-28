using System;
using UnityEngine;

namespace Cars.Car
{
    public class CarView : MonoBehaviour, ICarView
    {
        public event Action OnEnteredDestructionZone;
        public event Action OnPlayerCollision;
        
        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }
        
        public void Destroy()
        {
            UnityEngine.Object.Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DestructionZone"))
            {
                OnEnteredDestructionZone?.Invoke();
            } else if (other.CompareTag("Player"))
            {
                OnPlayerCollision?.Invoke();
            }
        }
    }
}
