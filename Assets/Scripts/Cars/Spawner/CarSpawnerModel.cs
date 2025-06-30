using UnityEngine;

namespace Cars.Spawner
{
    public class CarSpawnerModel
    {
        public GameObject CarPrefab { get; private set; }
        public Transform SpawnContainer { get; private set; }
        public Vector2 SpawnPosition { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public float SpawnRate { get; private set; }
        public float Speed { get; private set; }

        public CarSpawnerModel(ICarSpawnerData spawnerData, Transform spawnContainer, 
            Vector2 spawnPosition, Vector2 moveDirection)
        {
            CarPrefab = spawnerData.Prefab;
            SpawnContainer = spawnContainer;
            SpawnPosition = spawnPosition;
            MoveDirection = moveDirection.normalized;
            SpawnRate = spawnerData.SpawnRate;
            Speed = spawnerData.Speed;
        }
    }
}
