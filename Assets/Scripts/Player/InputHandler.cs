using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerScripts player;
        [SerializeField] private Camera cam;
        [SerializeField] private Rotations rotations;

        private MainInputActions _inputActions;
        private Vector2 _moveVector;
        private bool _attackPressed;

        private void Awake()
        {
            _inputActions = new MainInputActions();
        }
        private void Start()
        {
            rotations.SetRotationsOnStart(_inputActions, cam);
        }

        private void Update()
        {
            player.Movement.Move(_moveVector);
            Vector2 mousePosition = cam.ScreenToWorldPoint(_inputActions.Player.MousePosition.ReadValue<Vector2>());

            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
            rotations.RotateArms(direction, mousePosition);
            rotations.RotateEyes(direction);

            if (GameManager.Instance.State != GameManager.GameState.Continuing || !_attackPressed)
                return;
            player.CurrentWeapon.Attack(direction, transform);
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;

            _inputActions.Player.Jump.started += OnJumpStarted;

            _inputActions.Player.Shoot.started += OnShootStarted;
            _inputActions.Player.Shoot.canceled += OnShootCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;

            _inputActions.Player.Jump.started -= OnJumpStarted;

            _inputActions.Player.Shoot.started -= OnShootStarted;
            _inputActions.Player.Shoot.canceled -= OnShootCanceled;

            _inputActions.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnMovementCanceled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;
        private void OnJumpStarted(InputAction.CallbackContext value) => player.Movement.Jump();

        private void OnShootStarted(InputAction.CallbackContext value) => _attackPressed = true;
        private void OnShootCanceled(InputAction.CallbackContext value) => _attackPressed = false;
    }
}
