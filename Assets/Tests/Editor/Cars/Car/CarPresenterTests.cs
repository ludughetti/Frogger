using Moq;
using NUnit.Framework;
using UnityEngine;
using Cars.Car;

namespace Tests.Editor.Cars.Car
{
    [TestFixture]
    public class CarPresenterTests
    {
        private CarModel _model;
        private Mock<ICarView> _mockView;
        private CarPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new CarModel(Vector2.zero, Vector2.right, 5f);
            _mockView = new Mock<ICarView>();
            _presenter = new CarPresenter(_model, _mockView.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _presenter.Dispose();
        }

        [Test]
        public void Constructor_InitializesViewAndSubscribesEvents()
        {
            // Assert that UpdatePosition was called once with initial position
            _mockView.Verify(v => v.UpdatePosition(_model.Position), Times.Once);

            // Assert that event subscriptions happened
            _mockView.VerifyAdd(v => v.OnEnteredDestructionZone += It.IsAny<System.Action>(), Times.Once);
            _mockView.VerifyAdd(v => v.OnPlayerCollision += It.IsAny<System.Action>(), Times.Once);
        }

        [Test]
        public void UpdateMovement_MovesModelAndUpdatesView()
        {
            // Arrange
            Vector2 initialPosition = _model.Position;

            // Simulate UnityEngine.Time.deltaTime
            float fakeDeltaTime = 0.2f;
            float expectedMoveDistance = _model.Speed * fakeDeltaTime;
            Vector2 expectedPosition = initialPosition + (_model.MoveDirection * expectedMoveDistance);

            // Act
            _presenter.UpdateMovement(fakeDeltaTime);

            // Assert model moved
            Assert.AreEqual(expectedPosition, _model.Position);

            // Assert view was updated with new model position
            _mockView.Verify(v => v.UpdatePosition(_model.Position), Times.Exactly(2)); // once on init, once on update
        }

        [Test]
        public void Dispose_UnsubscribesEventsAndDestroysView()
        {
            // Act
            _presenter.Dispose();

            // Assert events were unsubscribed
            _mockView.VerifyRemove(v => v.OnEnteredDestructionZone -= It.IsAny<System.Action>(), Times.Once);
            _mockView.VerifyRemove(v => v.OnPlayerCollision -= It.IsAny<System.Action>(), Times.Once);

            // Assert view was destroyed
            _mockView.Verify(v => v.Destroy(), Times.Once);
        }

        [Test]
        public void HandleDestructionRequest_InvokesOnRequestDestruction()
        {
            // Arrange
            CarPresenter captured = null;
            _presenter.OnRequestDestruction += p => captured = p;

            // Act
            _mockView.Raise(v => v.OnEnteredDestructionZone += null);

            // Assert
            Assert.AreSame(_presenter, captured);
        }

        [Test]
        public void HandlePlayerCollision_InvokesOnPlayerCollision()
        {
            // Arrange
            CarPresenter captured = null;
            _presenter.OnPlayerCollision += p => captured = p;

            // Act
            _mockView.Raise(v => v.OnPlayerCollision += null);

            // Assert
            Assert.AreSame(_presenter, captured);
        }
    }
}