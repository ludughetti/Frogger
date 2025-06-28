using Cars.Car;
using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests.EditMode
{
    [TestFixture]
    public class CarPresenterTests
    {
        private CarModel _model;
        private CarViewMocked _mockedView;
        private CarPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new CarModel(Vector2.zero, Vector2.right, 5f);
            _mockedView = new CarViewMocked();
            _presenter = new CarPresenter(_model, _mockedView);
        }

        [TearDown]
        public void TearDown()
        {
            _presenter.Dispose();
        }

        [Test]
        public void Constructor_InitializesViewAndSubscribesEvents()
        {
            Assert.IsTrue(_mockedView.UpdatePositionCalled);
            Assert.AreEqual(Vector2.zero, _mockedView.LastPosition);
        }

        [Test]
        public void UpdateMovement_MovesModelAndUpdatesView()
        {
            var initialPosition = _model.Position;
            const float fakeDeltaTime = 0.2f;
            var expectedMoveDistance = _model.Speed * fakeDeltaTime;
            var expectedPosition = initialPosition + (_model.MoveDirection * expectedMoveDistance);

            _presenter.UpdateMovement(fakeDeltaTime);

            Assert.AreEqual(expectedPosition, _model.Position);
            Assert.IsTrue(_mockedView.UpdatePositionCalled);
            Assert.AreEqual(expectedPosition, _mockedView.LastPosition);
        }

        [Test]
        public void Dispose_UnsubscribesEventsAndDestroysView()
        {
            _presenter.Dispose();

            Assert.IsTrue(_mockedView.DestroyCalled);
        }

        [Test]
        public void HandleDestructionRequest_InvokesOnRequestDestruction()
        {
            CarPresenter captured = null;
            _presenter.OnRequestDestruction += p => captured = p;

            _mockedView.SimulateEnteredDestructionZone();

            Assert.AreSame(_presenter, captured);
        }

        [Test]
        public void HandlePlayerCollision_InvokesOnPlayerCollision()
        {
            CarPresenter captured = null;
            _presenter.OnPlayerCollision += p => captured = p;

            _mockedView.SimulatePlayerCollision();

            Assert.AreSame(_presenter, captured);
        }
    }
}