using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public Vector2 Position { get; private set; }
        public Vector2 BoundTopLeft { get; private set; }
        public Vector2 BoundBottomRight { get; private set; }
        public float PlayerHalfWidth { get; private set; }
        public float PlayerHalfHeight { get; private set; }

        public PlayerModel(Vector2 position, Vector2 boundTopLeft, Vector2 boundBottomRight, 
            float playerHalfWidth, float playerHalfHeight)
        {
            Position = position;
            BoundTopLeft = boundTopLeft;
            BoundBottomRight = boundBottomRight;
            PlayerHalfWidth = playerHalfWidth;
            PlayerHalfHeight = playerHalfHeight;
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
