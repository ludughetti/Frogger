using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 BoundTopLeft { get; private set; }
        public Vector2 BoundBottomRight { get; private set; }

        public PlayerModel(Vector2 position, Vector2 boundTopLeft, Vector2 boundBottomRight)
        {
            Position = position;
            BoundTopLeft = boundTopLeft;
            BoundBottomRight = boundBottomRight;
        }

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
