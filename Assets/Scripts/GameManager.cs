using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PauseMenu pauseMenu;                      //Pause menu

    public GameState State { get; private set; }
    private MainInputActions _inputActions;

    public enum GameState               // Game States
    {
        Continuing,
        Paused,
        Finished
    }

    protected override void Awake()
    {
        base.Awake();
        _inputActions = new MainInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Pause.started += OnPausePressed;
    }
    private void OnDisable()
    {
        _inputActions.Player.Pause.started -= OnPausePressed;
        _inputActions.Disable();
    }


    public void SwitchToPaused()
    {
        Time.timeScale = 0f;
        State = GameState.Paused;
    }
    public void SwitchToFinished()
    {
        State = GameState.Finished;
    }
    public void SwitchToContinuing()
    {
        Time.timeScale = 1f;
        State = GameState.Continuing;
    }                                             // Game States

    private void OnPausePressed(InputAction.CallbackContext vakue)
    {
        pauseMenu.HandleEscapeAction();
        Debug.Log("esc pressed");
    }

}
