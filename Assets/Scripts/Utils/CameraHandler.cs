using System;
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

        private void Start()
        {
            if (Camera.main != null) 
                _halfHeight = Camera.main.orthographicSize;
        }

        public void Setup(Vector2 topLeftBound, Vector2 bottomRightBound)
        {
            _topBound = topLeftBound.y;
            _bottomBound = bottomRightBound.y;
        }

        public void Follow(float playerPosition)
        {
            _targetY = playerPosition;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var desiredY = Mathf.Lerp(currentPosition.y, _targetY, 5f * Time.deltaTime);
            var clampedY = Mathf.Clamp(desiredY, _bottomBound + _halfHeight, _topBound - _halfHeight);

            transform.position = new Vector3(FixedX, clampedY, FixedZ);
        }
    }
}
