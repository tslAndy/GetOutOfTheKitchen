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

        private bool _jumping;
        public bool Jumping => _jumping;

        private void Awake()
        {
            input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Movement.performed += OnInputPerformed;
            input.Player.Movement.canceled += OnInputCancelled;

            input.Player.Jumping.started += OnJumpStarted;
            input.Player.Jumping.canceled += OnJumpCancelled;
        }

        private void OnDisable()
        {
            input.Player.Movement.performed -= OnInputPerformed;
            input.Player.Movement.canceled -= OnInputCancelled;

            input.Player.Jumping.started -= OnJumpStarted;
            input.Player.Jumping.canceled -= OnJumpCancelled;

            input.Disable();
        }

        private void OnInputPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnInputCancelled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;

        private void OnJumpStarted(InputAction.CallbackContext value) => _jumping = true;

        private void OnJumpCancelled(InputAction.CallbackContext value) => _jumping = false;

        public void EndJump() => _jumping = false;
    }
}
