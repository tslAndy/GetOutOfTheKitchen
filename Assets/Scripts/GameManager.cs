using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public GameState gameState;
    private MainInputActions _inputActions;
    [SerializeField] private GameObject _pauseMenu;

    private void OnEnable()
    {
        _inputActions = new MainInputActions();
        _inputActions.Enable();
        _inputActions.Player.Pause.performed += OnPausedPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Pause.performed -= OnPausedPerformed;
    }
    private void Awake()
    {
        instance = this;
        SwitchToContinuing();

    }


    public enum GameState                           // Sates of the game 
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
    }



    private void OnPausedPerformed (InputAction.CallbackContext context)                       // Method to subscribe on Input Actions, for pause
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
