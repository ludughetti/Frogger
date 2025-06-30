using Cars.Car;
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

        public ICarView Spawn(Vector2 position, Transform parent)
        {
            var instance  = Object.Instantiate(_prefab, position, Quaternion.identity, parent);
            if (instance.TryGetComponent(out ICarView view))
                return view;

            Debug.LogError("Spawned car prefab does not implement ICarView!");
            return null;
        }
    }
}