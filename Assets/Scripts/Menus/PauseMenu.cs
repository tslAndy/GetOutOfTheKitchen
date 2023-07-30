using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private List<GameObject> _menus = new List<GameObject>();

    public void EnterMenu(GameObject menu)
    {
        if (_menus.Count != 0)
            _menus.Last().SetActive(false);

        _menus.Add(menu);
        _menus.Last().SetActive(true);
    }

    public void HandleEscapeAction()
    {
        // if just entering pause menu
        if (_menus.Count == 0)
        {
            GameManager.Instance.SwitchToPaused();
            EnterMenu(pauseMenu);
        }
        else
        {
            // exit current menu
            _menus.Last().SetActive(false);
            _menus.RemoveAt(_menus.Count - 1);


            // if we exited from pause menu
            if (_menus.Count == 0)
                GameManager.Instance.SwitchToContinuing();
            // if we have previous menu, enable it
            else
                _menus.Last().SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
