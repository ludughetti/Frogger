using System.Collections;
using Cars.Car;
using Cars.Spawner.Factory.FactoryImpl;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayMode
{
    public class DefaultCarFactoryTests
    {
        private GameObject _prefab;
        private DefaultCarFactory _factory;
        private GameObject _parent;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Create a prefab GameObject with a component implementing ICarView
            _prefab = new GameObject("CarPrefab");
            _prefab.AddComponent<CarView>(); // CarView implements ICarView
            
            _factory = new DefaultCarFactory(_prefab);

            _parent = new GameObject("ParentContainer");

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            if (_prefab != null)
                Object.DestroyImmediate(_prefab);
            if (_parent != null)
                Object.DestroyImmediate(_parent);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Spawn_ReturnsICarViewAndInstantiatesAtPositionAndParent()
        {
            var spawnPosition = new Vector2(1f, 2f);

            var carView = _factory.Spawn(spawnPosition, _parent.transform);

            Assert.IsNotNull(carView, "Spawn should return a non-null ICarView");
            Assert.IsInstanceOf<ICarView>(carView);

            var carGameObject = ((MonoBehaviour)carView).gameObject;

            Assert.AreEqual(spawnPosition, (Vector2)carGameObject.transform.position);
            Assert.AreEqual(_parent.transform, carGameObject.transform.parent);

            yield return null;

            Object.Destroy(carGameObject);
        }

        [UnityTest]
        public IEnumerator Spawn_WithPrefabWithoutICarView_LogsErrorAndReturnsNull()
        {
            // Create prefab without ICarView implementation
            var badPrefab = new GameObject("BadPrefab");

            var badFactory = new DefaultCarFactory(badPrefab);

            LogAssert.Expect(LogType.Error, "Spawned car prefab does not implement ICarView!");

            var result = badFactory.Spawn(Vector2.zero, null);

            Assert.IsNull(result);

            Object.DestroyImmediate(badPrefab);

            yield return null;
        }
    }
}
