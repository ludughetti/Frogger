using Player;
using UnityEngine;

namespace Tests.EditMode.Player.Mocked
{
    public class PlayerViewMocked : IPlayerView
    {
        public Vector2 LastPosition { get; private set; }
        public Vector2 LastDirection { get; private set; }
        public bool WasMoving { get; private set; }
        public bool UpdatePositionCalled { get; private set; }
        public bool SetAnimationValuesCalled { get; private set; }
        public bool HandleSfxCalled { get; private set; }
        public bool DestroyCalled { get; private set; }

        public void UpdatePosition(Vector2 position)
        {
            LastPosition = position;
            UpdatePositionCalled = true;
        }

        public void SetAnimationValues(Vector2 direction, bool isMoving)
        {
            LastDirection = direction;
            WasMoving = isMoving;
            SetAnimationValuesCalled = true;
        }

        public void HandlePlayerWalkSfx(bool isMoving)
        {
            WasMoving = isMoving;
            HandleSfxCalled = true;
        }

        public void Destroy()
        {
            DestroyCalled = true;
        }

        public void ResetFlags()
        {
            UpdatePositionCalled = false;
            SetAnimationValuesCalled = false;
            HandleSfxCalled = false;
        }
    }
}

