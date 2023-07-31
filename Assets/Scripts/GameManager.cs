using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PauseMenu pauseMenu;                      //Pause menu
    [SerializeField] private GameObject deathScreen, winScreen;                    //Death screen

    public GameState State { get; private set; }
    private MainInputActions _inputActions;
    private bool _playerIsDead, _playerWon;

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
        Time.timeScale = 1f;
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

    private void OnPausePressed(InputAction.CallbackContext value)
    {
        if (!(_playerIsDead || _playerWon))
            pauseMenu.HandleEscapeAction();
    }

    public void SetPlayerIsDead()
    { 
        _playerIsDead = true;
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public bool IsPlayerDead()
    {
        if (_playerIsDead)
            return true;
        else
            return false;
    }

        public void SetPlayerIsWon()
    {
        _playerWon = true;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
