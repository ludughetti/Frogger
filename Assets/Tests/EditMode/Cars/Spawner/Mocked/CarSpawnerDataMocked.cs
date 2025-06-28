using Cars.Spawner;
using UnityEngine;

namespace Tests.EditMode.Cars.Spawner.Mocked
{
    public class CarSpawnerDataMocked : ICarSpawnerData
    {
        public GameObject Prefab { get; set; }
        public float SpawnRate { get; set; }
        public float Speed { get; set; }
    }
}