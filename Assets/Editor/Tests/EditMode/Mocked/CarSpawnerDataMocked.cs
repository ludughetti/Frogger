using Cars.Spawner;
using UnityEngine;

namespace Editor.Tests.EditMode.Mocked
{
    public class CarSpawnerDataMocked : ICarSpawnerData
    {
        public GameObject Prefab { get; set; }
        public float SpawnRate { get; set; }
        public float Speed { get; set; }
    }
}