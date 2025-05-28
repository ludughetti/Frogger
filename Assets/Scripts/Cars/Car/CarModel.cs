using UnityEngine;

namespace Cars.Car
{
    public class CarModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public float Speed { get; private set; }

        public CarModel(Vector2 startPosition, Vector2 moveDirection, float speed)
        {
            Position = startPosition;
            MoveDirection = moveDirection.normalized;
            Speed = speed;
        }

        public void Move(Vector2 nextPosition)
        {
            Position = nextPosition;
        }
    }
}
