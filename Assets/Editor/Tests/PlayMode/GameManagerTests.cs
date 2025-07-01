using System.Collections;
using System.Collections.Generic;
using Editor.Tests.PlayMode.Mocked;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayMode
{
    public class GameManagerTests
    {
        private GameObject _gameManagerGo;
        private FakeGameManager _fakeGameManager;
        private Transform _boundTopLeft;
        private Transform _boundBottomRight;
        private Transform _spawnPoint;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _boundTopLeft = new GameObject("TopLeft").transform;
            _boundBottomRight = new GameObject("BottomRight").transform;
            _spawnPoint = new GameObject("SpawnPoint").transform;

            _gameManagerGo = new GameObject("FakeGameManager");
            _fakeGameManager = _gameManagerGo.AddComponent<FakeGameManager>();

            _fakeGameManager.SetDependencies(_boundTopLeft, _boundBottomRight, _spawnPoint);

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(_gameManagerGo);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Initialize_SetsAllPresenters()
        {
            _fakeGameManager.Initialize();

            Assert.IsTrue(_fakeGameManager.WasCarSpawnerPresenterSet(), "CarSpawnerPresenter not set.");
            Assert.IsTrue(_fakeGameManager.WasEndZonePresenterSet(), "EndZonePresenter not set.");
            Assert.IsTrue(_fakeGameManager.WasPlayerPresenterSet(), "PlayerPresenter not set.");
            Assert.IsTrue(_fakeGameManager.WasStartZonePresenterSet(), "StartZonePresenter not set.");
            Assert.IsTrue(_fakeGameManager.WasTimerPresenterSet(), "TimerPresenter not set.");
            Assert.IsTrue(_fakeGameManager.WasSceneLoaderSet(), "SceneLoader not set.");

            yield return null;
        }
    }
}
