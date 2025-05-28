using System;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerPresenter
    {
        private PlayerModel _model;
        private PlayerView _view;
        private float _moveSpeed;

        private Vector2 _currentDirection = Vector2.zero;

        public Action<float> OnPlayerMoved;

        public PlayerPresenter(PlayerModel model, PlayerView playerView, InputHandler inputHandler, float moveSpeed)
        {
            _model = model;
            _view = playerView;
            _moveSpeed = moveSpeed;
            
            // Listen to inputHandler
            inputHandler.OnMove += OnMove;
        }
        
        // Cleanup if presenter gets disposed:
        public void Dispose(InputHandler inputHandler)
        {
            inputHandler.OnMove -= OnMove;
        }

        public void Update()
        {
            UpdateMovement();
        }

        public void SetSpawnPosition(Vector2 spawnPosition)
        {
            _model.SetPosition(spawnPosition);
            _view.UpdatePosition(_model.Position);
        }

        public void RespawnPlayer()
        {
            _model.SetPosition(_model.StartPosition);
            _view.UpdatePosition(_model.Position);
        }

        private void OnMove(Vector2 direction)
        {
            // Check for deadzone threshold
            _currentDirection = direction.sqrMagnitude > 0.01f ? direction.normalized : Vector2.zero; 
        }
        
        private void UpdateMovement()
        {
            // Calculate next position 
            var movement = _currentDirection * (_moveSpeed * Time.deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;
            
            // Clamp the next position so the player is within bounds
            nextPosition.x = Mathf.Clamp(nextPosition.x, 
                _model.BoundTopLeft.x + _model.PlayerHalfWidth, 
                _model.BoundBottomRight.x - _model.PlayerHalfWidth);
            nextPosition.y = Mathf.Clamp(nextPosition.y, 
                _model.BoundBottomRight.y + _model.PlayerHalfHeight, 
                _model.BoundTopLeft.y - _model.PlayerHalfHeight);

            // Move
            _model.Move(nextPosition - currentPosition);
            _view.UpdatePosition(_model.Position);
            
            // Invoke OnPlayerMoved event
            OnPlayerMoved?.Invoke(_model.Position.y);
        }
    }
}
