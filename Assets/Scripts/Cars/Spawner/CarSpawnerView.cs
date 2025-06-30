using UnityEngine;

namespace Cars.Spawner
{
    public class CarSpawnerView : MonoBehaviour
    {
        [SerializeField] private CarSpawnerData spawnerData;
        [SerializeField] private Transform spawnContainer;
        
        public ICarSpawnerData SpawnerData => spawnerData;
        public Transform SpawnContainer => spawnContainer;

        // Setter for testing
        public void SetData(CarSpawnerData testSpawnerData, Transform testSpawnContainer)
        {
            spawnerData = testSpawnerData;
            spawnContainer = testSpawnContainer;
        }
    }
}
