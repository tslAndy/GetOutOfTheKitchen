using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] PlayerInputActions input;
        [SerializeField] Camera cam;

        private Vector2 _moveVector;
        public Vector2 MoveVector {get => _moveVector; private set => _moveVector = value;}

        private Vector3 _mouseVector;
        public Vector3 MouseVector { get => _mouseVector; private set => _mouseVector = value;}

        private bool _jumping;
        public bool Jumping => _jumping;

        private bool _shooting;
        public bool Shooting => _shooting;

        private void Awake()
        {
            input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            input.Enable();

            input.Player.Movement.performed += OnInputPerformed;
            input.Player.Movement.canceled += OnInputCancelled;

            input.Player.MousePosition.performed += OnMouseMoved;

            input.Player.Jumping.started += OnJumpStarted;
            input.Player.Jumping.canceled += OnJumpCancelled;

            input.Player.Shooting.started += OnShootStarted;
            input.Player.Shooting.canceled += OnShootCancelled;
        }

        private void OnDisable()
        {
            input.Player.Movement.performed -= OnInputPerformed;
            input.Player.Movement.canceled -= OnInputCancelled;

            input.Player.MousePosition.performed -= OnMouseMoved;

            input.Player.Jumping.started -= OnJumpStarted;
            input.Player.Jumping.canceled -= OnJumpCancelled;

            input.Player.Shooting.started -= OnShootStarted;
            input.Player.Shooting.canceled -= OnShootCancelled;

            input.Disable();
        }

        private void OnInputPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnInputCancelled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;

        private void OnMouseMoved(InputAction.CallbackContext value) => MouseVector = cam.ScreenToWorldPoint(value.ReadValue<Vector2>());

        private void OnShootStarted(InputAction.CallbackContext value) => _shooting = true;
        private void OnShootCancelled(InputAction.CallbackContext value) => _shooting = false;

        private void OnJumpStarted(InputAction.CallbackContext value) => _jumping = true;
        private void OnJumpCancelled(InputAction.CallbackContext value) => _jumping = false;

        public void EndJump() => _jumping = false;
    }
}
