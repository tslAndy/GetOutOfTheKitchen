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

            _inputActions.Player.MainShoot.started += OnMainShootStarted;
            _inputActions.Player.MainShoot.canceled += OnMainShootCanceled;

            _inputActions.Player.AdditionalShoot.started += OnAdditionalShootStarted;
            _inputActions.Player.AdditionalShoot.canceled += OnAdditionalShootCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Movement.canceled -= OnMovementCanceled;

            _inputActions.Player.Jump.started -= OnJumpStarted;

            _inputActions.Player.MainShoot.started -= OnMainShootStarted;
            _inputActions.Player.MainShoot.canceled -= OnMainShootCanceled;

            _inputActions.Player.AdditionalShoot.started -= OnAdditionalShootStarted;
            _inputActions.Player.AdditionalShoot.canceled -= OnAdditionalShootCanceled;

            _inputActions.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext value) => _moveVector = value.ReadValue<Vector2>();
        private void OnMovementCanceled(InputAction.CallbackContext value) => _moveVector = Vector2.zero;
        private void OnJumpStarted(InputAction.CallbackContext value) => player.Movement.Jump();
        private void OnMainShootStarted(InputAction.CallbackContext value)
        {
            if (GameManager.Instance.State != GameManager.GameState.Continuing)
                return;

            player.WeaponManager.CurrentWeapon.OnMainShootStarted();
        }

        private void OnMainShootCanceled(InputAction.CallbackContext value)
        {
            if (GameManager.Instance.State != GameManager.GameState.Continuing)
                return;

            player.WeaponManager.CurrentWeapon.OnMainShootCanceled();
        }

        private void OnAdditionalShootStarted(InputAction.CallbackContext value)
        {
            if (GameManager.Instance.State != GameManager.GameState.Continuing)
                return;

            player.WeaponManager.CurrentWeapon.OnAdditionalShootStarted();
        }

        private void OnAdditionalShootCanceled(InputAction.CallbackContext value)
        {
            if (GameManager.Instance.State != GameManager.GameState.Continuing)
                return;

            player.WeaponManager.CurrentWeapon.OnAdditionalShootCanceled();
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
