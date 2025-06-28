using Cars.Spawner;
using NUnit.Framework;
using Tests.EditMode.Cars.Car.Mocked;
using Tests.EditMode.Cars.Spawner.Mocked;
using UnityEngine;

namespace Tests.EditMode.Cars.Spawner
{
    public class CarSpawnerPresenterTests
    {
        private CarSpawnerPresenter _presenter;
        private FakeCarFactory _factory;
        private CarSpawnerModel _model;
        private Transform _spawnContainer;

        [SetUp]
        public void Setup()
        {
            var containerGo = new GameObject("SpawnContainer");
            _spawnContainer = containerGo.transform;

            var prefab = new GameObject("CarPrefab");
            prefab.AddComponent<CarViewMocked>();

            var data = new CarSpawnerDataMocked
            {
                Prefab = prefab,
                SpawnRate = 1f,
                Speed = 5f
            };

            _factory = new FakeCarFactory();
            _model = new CarSpawnerModel(data, _spawnContainer, Vector2.zero, Vector2.right);
            _presenter = new CarSpawnerPresenter(_model, _factory);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_spawnContainer.gameObject);
            Object.DestroyImmediate(_factory.LastSpawned);
        }

        [Test]
        public void Update_WhenSpawnTimerExpires_SpawnsCar()
        {
            // Act
            _presenter.Update(999f);

            // Assert
            Assert.IsNotNull(_factory.LastSpawned);
            var carView = _factory.LastView;
            Assert.IsTrue(carView.UpdatePositionCalled);
        }

        [Test]
        public void Update_CallsCarUpdate()
        {
            // Arrange
            _presenter.Update(999f);
            var carView = _factory.LastView;
            carView.ResetFlags();

            // Act
            _presenter.Update(0.1f); 

            // Assert
            Assert.IsTrue(carView.UpdatePositionCalled);
        }

        [Test]
        public void SimulatePlayerCollision_InvokesOnPlayerCollision()
        {
            // Arrange
            bool wasCalled = false;
            _presenter.OnPlayerCollision += (_, _) => wasCalled = true;

            _presenter.Update(999f); 
            var carView = _factory.LastView;

            // Act
            carView.SimulatePlayerCollision();

            // Assert
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void SimulateDestruction_RemovesAndDestroysCar()
        {
            // Arrange
            _presenter.Update(999f);
            var carView = _factory.LastView;

            // Act
            carView.SimulateEnteredDestructionZone();

            // Assert
            Assert.IsTrue(carView.DestroyCalled);
        }
    }
}
