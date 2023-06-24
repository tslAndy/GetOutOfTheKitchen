using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;       
        private MainInputActions _inputActions;
        private Vector2 _moveVector;

        private void Awake()
        {
            _inputActions = new MainInputActions();
        }

        private void Update()
        {
            playerMovement.Move(_moveVector);
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.Movement.performed += OnMovementPerformed;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
            
            _inputActions.Player.Jump.started += OnJumpStarted;
        }
 
        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;

            _inputActions.Player.Jump.started -= OnJumpStarted;
            
            _inputActions.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnMovementCanceled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;
        private void OnJumpStarted(InputAction.CallbackContext value) => playerMovement.Jump();  
    }
}
