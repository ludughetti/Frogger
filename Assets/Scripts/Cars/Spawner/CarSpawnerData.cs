using UnityEngine;

namespace Cars.Spawner
{
    public class CarSpawnerData : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnRate;
        [SerializeField] private float speed;
        
        public GameObject Prefab => prefab;
        public float SpawnRate => spawnRate;
        public float Speed => speed;
    }
}
