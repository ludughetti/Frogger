using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cars.Car
{
    public class CarPresenter
    {
        private CarModel _model;
        private CarView _view;
        
        public event Action<CarPresenter> OnRequestDestruction;

        public CarPresenter(CarModel model, CarView view)
        {
            _model = model;
            _view = view;
            
            _view.OnEnteredDestructionZone += HandleDestructionRequest;
            _view.UpdatePosition(_model.Position);
        }

        public void UpdateMovement()
        {
            // Calculate next position 
            var movement = _model.MoveDirection * (_model.Speed * Time.deltaTime);
            var currentPosition = _model.Position;
            var nextPosition = currentPosition + movement;
            
            // Move
            _model.Move(nextPosition);
            _view.UpdatePosition(_model.Position);
        }

        public void Dispose()
        {
            _view.OnEnteredDestructionZone -= HandleDestructionRequest;
            Object.Destroy(_view.gameObject);
        }
        
        private void HandleDestructionRequest()
        {
            OnRequestDestruction?.Invoke(this);
        }
    }
}
