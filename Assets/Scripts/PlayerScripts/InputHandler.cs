using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class InputHandler : Singleton<InputHandler>
    {
        [SerializeField] private Player player;
        [SerializeField] private Camera cam;
        [SerializeField] private Rotations rotations;

        private MainInputActions _inputActions;
        private Vector2 _moveVector;

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new MainInputActions();
        }
        private void Start()
        {
            // ! Uncomment before commit
            rotations.SetRotationsOnStart(_inputActions, cam);
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameManager.GameState.Continuing)
                return;

            player.Movement.Move(_moveVector);

            // ! Uncomment before commit
            rotations.RotateArms(GetMouseDirection(transform), GetMousePosition());
            rotations.RotateEyes(GetMouseDirection(transform));
            rotations.RotateWeapon(GetMouseDirection(transform));
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
        private void OnShootStarted(InputAction.CallbackContext value)
        {
            player.WeaponManager.CurrentWeapon.OnShootStarted(GetMouseDirection(transform));
        }

        private void OnShootCanceled(InputAction.CallbackContext value)
        {
            player.WeaponManager.CurrentWeapon.OnShootCanceled(GetMouseDirection(transform));
        }

        private Vector2 GetMousePosition()
        {
            Vector2 screenPosition = _inputActions.Player.MousePosition.ReadValue<Vector2>();
            Vector2 mousePosition = cam.ScreenToWorldPoint(screenPosition);
            return mousePosition;
        }

        public Vector2 GetMouseDirection(Transform countFrom)
        {
            Vector2 result = (GetMousePosition() - (Vector2)countFrom.position).normalized;
            return result;
        }
    }
}
