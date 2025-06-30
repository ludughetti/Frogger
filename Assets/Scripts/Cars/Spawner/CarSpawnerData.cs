using UnityEngine;

namespace Cars.Spawner
{
    [CreateAssetMenu(fileName = "CarSpawnerData", menuName = "Scriptable Objects/CarSpawnerData")]
    public class CarSpawnerData : ScriptableObject, ICarSpawnerData
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnRate;
        [SerializeField] private float speed;
        
        public GameObject Prefab => prefab;
        public float SpawnRate => spawnRate;
        public float Speed => speed;

        // Setter for testing
        public void SetData(GameObject testPrefab, float testSpawnRate, float testSpeed)
        {
            prefab = testPrefab;
            spawnRate = testSpawnRate;
            speed = testSpeed;
        }
    }
}
