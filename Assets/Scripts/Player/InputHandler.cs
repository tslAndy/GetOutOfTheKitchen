using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerScripts player;
        [SerializeField] private Camera cam;

        private MainInputActions _inputActions;
        private Vector2 _moveVector;

        private void Awake()
        {
            _inputActions = new MainInputActions();
        }

        private void Update()
        {
            player.Movement.Move(_moveVector);
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;

            _inputActions.Player.Jump.started += OnJumpStarted;
            _inputActions.Player.Shoot.started += OnShootStarted;
        }

        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;

            _inputActions.Player.Jump.started -= OnJumpStarted;
            _inputActions.Player.Shoot.started += OnShootStarted;

            _inputActions.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnMovementCanceled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;
        private void OnJumpStarted(InputAction.CallbackContext value) => player.Movement.Jump();

        private void OnShootStarted(InputAction.CallbackContext value)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(_inputActions.Player.MousePosition.ReadValue<Vector2>());
            Vector2 direction = (mousePosition - (Vector2) transform.position).normalized;
            player.CurrentWeapon.Attack(direction, transform);
            //player.CurrentWeapon.RotateWeapon(direction);
        }
    }
}
