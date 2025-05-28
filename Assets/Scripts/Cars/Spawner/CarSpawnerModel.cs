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

        public CarSpawnerModel(GameObject carPrefab, Transform spawnContainer, Vector2 spawnPosition, 
            Vector2 moveDirection, float spawnRate, float carSpeed)
        {
            CarPrefab = carPrefab;
            SpawnContainer = spawnContainer;
            SpawnPosition = spawnPosition;
            MoveDirection = moveDirection.normalized;
            SpawnRate = spawnRate;
            Speed = carSpeed;
        }
    }
}
