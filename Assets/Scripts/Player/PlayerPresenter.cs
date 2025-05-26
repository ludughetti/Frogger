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

        public PlayerPresenter(PlayerModel model, PlayerView playerView, InputHandler inputHandler, float moveSpeed)
        {
            _model = model;
            _view = playerView;
            _moveSpeed = moveSpeed;
            
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

        private void OnMove(Vector2 direction)
        {
            // Check for deadzone threshold
            _currentDirection = direction.sqrMagnitude > 0.01f ? direction.normalized : Vector2.zero; 
        }
        
        private void UpdateMovement()
        {
            var movement = _currentDirection * (_moveSpeed * Time.deltaTime);
            _model.Move(movement);
            _view.UpdatePosition(_model.Position);
        }
    }
}
