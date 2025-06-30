using Cars.Car;
using UnityEngine;

namespace Cars.Spawner.Factory
{
    public interface ICarFactory
    {
        ICarView Spawn(Vector2 position, Transform parent);
    }
}