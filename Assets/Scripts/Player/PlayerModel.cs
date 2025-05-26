using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public Vector2 Position { get; private set; }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void Move(Vector2 delta)
        {
            Position += delta;
        }
    }
}
