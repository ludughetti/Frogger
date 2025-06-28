using UnityEngine;

namespace Cars.Spawner.Factory
{
    public interface ICarFactory
    {
        GameObject Spawn(Vector2 position, Transform parent);
    }
}