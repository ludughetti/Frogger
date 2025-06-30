using System;
using UnityEngine;

namespace Utils
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        private InputActions _inputActions;

        public event Action<Vector2> OnMove;

        private void Awake()
        {
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()
        {
            var moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            OnMove?.Invoke(moveInput);
        }
    }
}