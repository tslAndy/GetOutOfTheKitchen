using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName;
    
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void MainMenu() => SceneManager.LoadScene(mainMenuSceneName);
    public void QuitGame() => Application.Quit();
}
