using UnityEngine;

namespace Player
{
    public interface IPlayerView
    {
        void SetAnimationValues(Vector2 direction, bool isMoving);
        void UpdatePosition(Vector2 position);
        void HandlePlayerWalkSfx(bool isMoving);
        void Destroy();
    }
}