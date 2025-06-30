using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using UnityEngine;
using Zones.StartZone;

namespace Editor.Tests.EditMode
{
    public class StartZonePresenterTests
    {
        private StartZoneModel _model;
        private StartZoneViewMocked _view;
        private StartZonePresenter _presenter;

        [SetUp]
        public void Setup()
        {
            var spawn = new Vector2(2f, 3f);
            _model = new StartZoneModel(spawn);
            _view = new StartZoneViewMocked();
            _presenter = new StartZonePresenter(_model, _view);
        }

        [Test]
        public void Constructor_SetsViewPosition()
        {
            Assert.AreEqual(_model.SpawnPosition, _view.LastSetPosition);
        }

        [Test]
        public void GetPlayerSpawnPosition_ReturnsModelPosition()
        {
            var result = _presenter.GetPlayerSpawnPosition();
            Assert.AreEqual(_model.SpawnPosition, result);
        }
    }
}