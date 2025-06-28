using NUnit.Framework;
using Player;
using UnityEngine;
using Tests.EditMode.Player.Mocked;
using Tests.EditMode.Utils;

namespace Tests.EditMode.Player
{
    public class PlayerPresenterTests
    {
        private PlayerModel _model;
        private PlayerPresenter _presenter;
        private PlayerViewMocked _view;
        private FakeInputHandler _input;
        private const float MoveSpeed = 5f;

        [SetUp]
        public void Setup()
        {
            var startPos = Vector2.zero;
            var topLeft = new Vector2(-10, 10);
            var bottomRight = new Vector2(10, -10);
            const float halfWidth = 0.5f;
            const float halfHeight = 0.5f;

            _model = new PlayerModel(startPos, topLeft, bottomRight, halfWidth, halfHeight);
            _view = new PlayerViewMocked();
            var inputGameObject = new GameObject("FakeInputHandler");
            _input = inputGameObject.AddComponent<FakeInputHandler>();
            
            _presenter = new PlayerPresenter(_model, _view, _input, MoveSpeed);
        }

        [TearDown]
        public void Teardown()
        {
            _presenter.Dispose(_input);
            Object.DestroyImmediate(_input.gameObject);
        }

        [Test]
        public void SetSpawnPosition_SetsModelAndViewPosition()
        {
            var spawnPos = new Vector2(3, 4);

            _presenter.SetSpawnPosition(spawnPos);

            Assert.AreEqual(spawnPos, _model.Position);
            Assert.AreEqual(spawnPos, _view.LastPosition);
        }

        [Test]
        public void RespawnPlayer_MovesBackToStartPosition()
        {
            _presenter.SetSpawnPosition(new Vector2(5, 6));
            _presenter.RespawnPlayer();

            Assert.AreEqual(_model.StartPosition, _model.Position);
            Assert.AreEqual(_model.StartPosition, _view.LastPosition);
        }

        [Test]
        public void OnMove_TriggersAnimationAndSFX()
        {
            var moveDir = Vector2.right;

            _input.SimulateMove(moveDir);

            Assert.IsTrue(_view.SetAnimationValuesCalled);
            Assert.IsTrue(_view.HandleSfxCalled);
            Assert.AreEqual(moveDir.normalized, _view.LastDirection);
            Assert.IsTrue(_view.WasMoving);
        }

        [Test]
        public void Update_MovesPlayerAndUpdatesView()
        {
            _input.SimulateMove(Vector2.right);
            _view.ResetFlags();

            _presenter.Update(0.1f);

            Assert.IsTrue(_view.UpdatePositionCalled);
            Assert.AreNotEqual(Vector2.zero, _model.Position);
        }

        [Test]
        public void OnPlayerMoved_InvokedOnUpdate()
        {
            var wasCalled = false;
            // Subscribe to event through lambda
            _presenter.OnPlayerMoved += _ => wasCalled = true;

            _input.SimulateMove(Vector2.up);
            _presenter.Update(0.1f);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void Movement_IsClampedWithinBounds()
        {
            _input.SimulateMove(Vector2.right);
            _presenter.Update(0.1f);

            var pos = _model.Position;
            Assert.LessOrEqual(pos.x, _model.BoundBottomRight.x - _model.PlayerHalfWidth);
            Assert.GreaterOrEqual(pos.x, _model.BoundTopLeft.x + _model.PlayerHalfWidth);
        }
    }
}
