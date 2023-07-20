using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public GameState gameState;

    [SerializeField] private GameObject _pauseMenu;                       //Pause menu
    private MainInputActions _inputActions;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        instance = this;
        SwitchToContinuing();

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
    }


    public enum GameState               // Game States
    { 
        Continuing,
        Paused,
        Finished
    }
    public void SwitchToPaused()
    {
        gameState = GameState.Paused;
    }
    public void SwitchToFinished()
    {
        gameState = GameState.Finished;
    }
    public void SwitchToContinuing()
    {
        gameState = GameState.Continuing;
    }                                             // Game States



    private void OnPausePressed(InputAction.CallbackContext vakue)
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        if (_pauseMenu.activeSelf == true)
        {
            SwitchToPaused();
            Time.timeScale = 0f;
        }
        else if (_pauseMenu.activeSelf == false)
        {
            SwitchToContinuing();
            Time.timeScale = 1f;
        }
    }

}
