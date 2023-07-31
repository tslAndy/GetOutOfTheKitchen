using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GoQuitLogic : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu;
    [Header("Only for Main Menu")]
    [SerializeField] private GameObject MainMenu, HowToPlay;
    public void GoToLevel()
   {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("VisualNovel1");
        }
        else
        {
            SceneManager.LoadScene("VisualNovel2");
        }
   }

   public void Quit()
   {
        Application.Quit(); 
   }
   public void OpenSettings()
   {
       if(SceneManager.GetActiveScene().buildIndex == 0)
       {
            MainMenu.SetActive(false);
            SettingsMenu.SetActive(true);
       }
       else
       {
            Time.timeScale = 0f;
            SettingsMenu.SetActive(true);
       }
   }
    public void CloseSettings()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.SetActive(true);
            SettingsMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            SettingsMenu.SetActive(true);
        }
    }
    public void OpenHowToPlay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.SetActive(false);
            HowToPlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            HowToPlay.SetActive(true);
        }
    }
    public void CloseHowToPlay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.SetActive(true);
            HowToPlay.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            HowToPlay.SetActive(true);
        }
    }

}
