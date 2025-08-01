using System;
using Cars.Car;
using UnityEngine;

namespace Editor.Tests.EditMode.Mocked
{
    public class CarViewMocked : ICarView
    {
        public Vector2 LastPosition { get; private set; }
        public bool UpdatePositionCalled { get; private set; }
        public bool DestroyCalled { get; private set; }

        public event Action OnEnteredDestructionZone;
        public event Action OnPlayerCollision;

        public void UpdatePosition(Vector2 position)
        {
            UpdatePositionCalled = true;
            LastPosition = position;
        }
        
        public void ResetFlags()
        {
            UpdatePositionCalled = false;
        }

        public void Destroy()
        {
            DestroyCalled = true;
        }

        // Helper methods to raise events manually
        public void SimulateEnteredDestructionZone() => OnEnteredDestructionZone?.Invoke();
        public void SimulatePlayerCollision() => OnPlayerCollision?.Invoke();
    }
}