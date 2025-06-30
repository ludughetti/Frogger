using System.Collections;
using Cars.Spawner;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayMode
{
    public class CarSpawnerViewTests
    {
        private GameObject _spawnerGo;
        private CarSpawnerView _view;
        private CarSpawnerData _data;
        private GameObject _prefab;
        private Transform _container;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _spawnerGo = new GameObject("CarSpawner");
            _view = _spawnerGo.AddComponent<CarSpawnerView>();

            _prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _container = new GameObject("SpawnContainer").transform;

            _data = ScriptableObject.CreateInstance<CarSpawnerData>();
            _data.SetData(_prefab, 2f, 10f);
            
            _view.SetData(_data, _container);

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(_spawnerGo);
            Object.DestroyImmediate(_prefab);
            Object.DestroyImmediate(_container.gameObject);
            Object.DestroyImmediate(_data);
            yield return null;
        }

        [UnityTest]
        public IEnumerator SpawnerView_ExposesDataCorrectly()
        {
            Assert.AreEqual(_data, _view.SpawnerData);
            Assert.AreEqual(_container, _view.SpawnContainer);

            Assert.AreEqual(_prefab, _view.SpawnerData.Prefab);
            Assert.AreEqual(2f, _view.SpawnerData.SpawnRate);
            Assert.AreEqual(10f, _view.SpawnerData.Speed);

            yield return null;
        }
    }
}
