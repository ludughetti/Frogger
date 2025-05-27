using UnityEngine;

namespace Utils
{
    public class CameraHandler : MonoBehaviour
    {
        private const float FixedX = 0f;
        private const float FixedZ = -10f;
        
        private float _targetY;
        private float _halfHeight;
        private float _topBound;
        private float _bottomBound;
        private float _moveSpeed;

        private void Start()
        {
            if (Camera.main != null) 
                _halfHeight = Camera.main.orthographicSize;
        }

        public void Setup(Vector2 topLeftBound, Vector2 bottomRightBound, float playerMoveSpeed)
        {
            _topBound = topLeftBound.y;
            _bottomBound = bottomRightBound.y;
            _moveSpeed = playerMoveSpeed;
        }

        public void Follow(float playerPosition)
        {
            _targetY = playerPosition;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var desiredY = Mathf.Lerp(currentPosition.y, _targetY, _moveSpeed * Time.deltaTime);
            var clampedY = Mathf.Clamp(desiredY, _bottomBound + _halfHeight, _topBound - _halfHeight);

            transform.position = new Vector3(FixedX, clampedY, FixedZ);
        }
    }
}
