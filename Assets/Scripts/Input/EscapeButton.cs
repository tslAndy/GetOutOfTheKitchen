using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            if (_pauseMenu.activeSelf == true)
            {
                GameManager.instance.SwitchToPaused();
                Time.timeScale = 0f;
            }
            else if(_pauseMenu.activeSelf == false)
            {
                GameManager.instance.SwitchToContinuing();
                Time.timeScale = 1f;
            }
           
        }
    }
}
