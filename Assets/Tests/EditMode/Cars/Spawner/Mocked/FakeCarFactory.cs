using Cars.Spawner.Factory;
using Tests.EditMode.Cars.Car.Mocked;
using UnityEngine;

namespace Tests.EditMode.Cars.Spawner.Mocked
{
    public class FakeCarFactory : ICarFactory
    {
        public GameObject LastSpawned { get; private set; }
        
        public CarViewMocked LastView => LastSpawned.GetComponent<CarViewMocked>();

        public GameObject Spawn(Vector2 position, Transform parent)
        {
            LastSpawned = new GameObject("FakeCar");
            LastSpawned.transform.parent = parent;
            var view = LastSpawned.AddComponent<CarViewMocked>();
            // Presenter will assign this after creation
            return LastSpawned;
        }

    }
}