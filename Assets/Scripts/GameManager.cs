using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public GameState gameState;
    private void Awake()
    {
        instance = this;
        SwitchToContinuing();
    }
    public enum GameState
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

}
