using Cars.Car;
using Cars.Spawner.Factory;
using UnityEngine;

namespace Editor.Tests.EditMode.Mocked
{
    public class FakeCarFactory : ICarFactory
    {
        public GameObject LastSpawned { get; private set; }
        
        public CarViewMocked LastView { get; private set; }

        public ICarView Spawn(Vector2 position, Transform parent)
        {
            LastSpawned = new GameObject("FakeCar");
            LastSpawned.transform.parent = parent;

            LastView = new CarViewMocked();

            return LastView;
        }

    }
}