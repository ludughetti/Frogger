using System;
using UnityEngine;

namespace Utils
{
    public interface IInputHandler
    {
        event Action<Vector2> OnMove;
    }
}