using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShooting playerShooting;          // is now on the "Weapon pivot point " object in Player
        [SerializeField] private PupilsMovement pupilsMovement;          // On the " Eyes " objects in Player 
        [SerializeField] private Camera cam; 

        private MainInputActions _inputActions;
        private Vector2 _moveVector;

        private void Awake()
        {
            _inputActions = new MainInputActions();
        }

        private void Update()
        {
            playerMovement.Move(_moveVector);

            Vector2 mousePosition = cam.ScreenToWorldPoint(_inputActions.Player.MousePosition.ReadValue<Vector2>());
            Vector2 direction = mousePosition - (Vector2)transform.position;
            pupilsMovement.EyesFollowMouse(direction);

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
        private void OnJumpStarted(InputAction.CallbackContext value) => playerMovement.Jump();

        private void OnShootStarted(InputAction.CallbackContext value)
        {
            Vector2 mousePosition = cam.ScreenToWorldPoint(_inputActions.Player.MousePosition.ReadValue<Vector2>());
            Vector2 direction = mousePosition - (Vector2) transform.position;
            playerShooting.RotateWeapon(direction);
            playerShooting.Shoot(direction);
        }
    }
}
