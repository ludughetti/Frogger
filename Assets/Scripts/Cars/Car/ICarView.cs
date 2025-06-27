using System;
using UnityEngine;

namespace Cars.Car
{
    public interface ICarView
    {
        event Action OnEnteredDestructionZone;
        event Action OnPlayerCollision;
        
        void UpdatePosition(Vector2 position);
        void Destroy();
    }
}