using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerInputActions input;

        private Vector2 _moveVector;
        public Vector2 MoveVector {get => _moveVector; private set => _moveVector = value;}

        private void Awake()
        {
            input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Movement.performed += OnInputPerformed;
            input.Player.Movement.canceled += OnInputCancelled;

        }

        private void OnDisable()
        {
            input.Player.Movement.performed -= OnInputPerformed;
            input.Player.Movement.canceled -= OnInputCancelled;

            input.Disable();
        }

        private void OnInputPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnInputCancelled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;

        public bool IsJumping() => input.Player.Jumping.WasPressedThisFrame();

    }
}
