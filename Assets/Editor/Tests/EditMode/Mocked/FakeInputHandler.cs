using System;
using UnityEngine;
using Utils;

namespace Editor.Tests.EditMode.Mocked
{
    public class FakeInputHandler : IInputHandler
    {
        public event Action<Vector2> OnMove;

        public void SimulateMove(Vector2 direction)
        {
            OnMove?.Invoke(direction);
        }
    }
}