using Cars.Spawner;
using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.EditMode
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

            var dummyPrefab = new GameObject("DummyPrefab");
            var data = new CarSpawnerDataMocked
            {
                Prefab = dummyPrefab,
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
            _presenter.Update(999f);

            Assert.IsNotNull(_factory.LastSpawned);
            Assert.IsTrue(_factory.LastView.UpdatePositionCalled);
        }

        [Test]
        public void Update_CallsCarUpdate()
        {
            _presenter.Update(999f);
            _factory.LastView.ResetFlags();

            _presenter.Update(0.1f);

            Assert.IsTrue(_factory.LastView.UpdatePositionCalled);
        }

        [Test]
        public void SimulatePlayerCollision_InvokesOnPlayerCollision()
        {
            var wasCalled = false;
            _presenter.OnPlayerCollision += (_, _) => wasCalled = true;

            _presenter.Update(999f);
            _factory.LastView.SimulatePlayerCollision();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void SimulateDestruction_RemovesAndDestroysCar()
        {
            _presenter.Update(999f);
            _factory.LastView.SimulateEnteredDestructionZone();

            Assert.IsTrue(_factory.LastView.DestroyCalled);
        }
    }
}
