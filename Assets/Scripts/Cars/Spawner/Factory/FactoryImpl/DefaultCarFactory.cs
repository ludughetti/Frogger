using UnityEngine;

namespace Cars.Spawner.Factory.FactoryImpl
{
    public class DefaultCarFactory : ICarFactory
    {
        private readonly GameObject _prefab;

        public DefaultCarFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public GameObject Spawn(Vector2 position, Transform parent)
        {
            return Object.Instantiate(_prefab, position, Quaternion.identity, parent);
        }
    }
}