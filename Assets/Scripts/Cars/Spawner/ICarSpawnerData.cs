using UnityEngine;

namespace Cars.Spawner
{
    public interface ICarSpawnerData
    {
        GameObject Prefab { get; }
        float SpawnRate { get; }
        float Speed { get; }
    }
}