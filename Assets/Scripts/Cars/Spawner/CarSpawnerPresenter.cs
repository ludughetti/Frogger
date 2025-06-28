using System;
using System.Collections.Generic;
using Cars.Car;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Cars.Spawner
{
    public class CarSpawnerPresenter
    {
        private readonly CarSpawnerModel _model;
        private readonly CarSpawnerView _view;
        private float _spawnTimer;
        
        private List<CarPresenter> _activeCars = new();
        
        public event Action<CarSpawnerPresenter, CarPresenter> OnPlayerCollision;

        public CarSpawnerPresenter(CarSpawnerModel model, CarSpawnerView view)
        {
            _model = model;
            _view = view;
            _spawnTimer = _model.SpawnRate;
        }

        public void Update(float deltaTime)
        {
            // Update timer and move active cars
            _spawnTimer -= deltaTime;
            UpdateCars(deltaTime);
            
            // If spawn cooldown is not over, early return.
            // Else, spawn new car.
            if (!(_spawnTimer <= 0f)) return;
            
            SpawnCar();
            _spawnTimer = Random.Range(_model.SpawnRate - 0.5f, _model.SpawnRate + 0.5f);
        }

        private void SpawnCar()
        {
            var car = Object.Instantiate(_model.CarPrefab, _model.SpawnPosition, 
                Quaternion.identity, _model.SpawnContainer);
            if (!car.TryGetComponent(out CarView carView)) return;
            
            Debug.Log("Spawning car");
            
            var carModel = new CarModel(_model.SpawnPosition, _model.MoveDirection, _model.Speed);
            var carPresenter = new CarPresenter(carModel, carView);

            carPresenter.OnRequestDestruction += HandleCarDestruction;
            carPresenter.OnPlayerCollision += HandlePlayerCollision;
            _activeCars.Add(carPresenter);
        }

        private void UpdateCars(float deltaTime)
        {
            foreach (var carPresenter in _activeCars)
            {
                carPresenter.UpdateMovement(deltaTime);
            }
        }

        public void HandleCarDestruction(CarPresenter presenter)
        {
            presenter.OnRequestDestruction -= HandleCarDestruction;
            presenter.Dispose();
            
            _activeCars.Remove(presenter);
        }

        private void HandlePlayerCollision(CarPresenter presenter)
        {
            OnPlayerCollision?.Invoke(this, presenter);
        }
    }
}
