using System;
using UnityEngine;
using Utils;

namespace Tests.EditMode.Utils
{
    public class FakeInputHandler : InputHandler
    {
        public void SimulateMove(Vector2 direction)
        {
            OnMove?.Invoke(direction);
        }
    }
}
