using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class SceneMusicManager : MonoBehaviour
{
    private CurrentScene currentScene;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            currentScene = CurrentScene.MainMenu;
            Debug.Log("Main Menu");
        }
        else if (SceneManager.GetActiveScene().name == "VisualNovel1")
        {
            currentScene = CurrentScene.VisualNovel;
            Debug.Log("VisualNovel1");
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            currentScene = CurrentScene.Level;
            Debug.Log("SampleScene");
        }
    }
    private void Start()
    {
       if(currentScene == CurrentScene.MainMenu)
        {
            AudioManager.Instance.MusicSource.clip = AudioManager.Instance.AudioClips["MainMenu"];
            AudioManager.Instance.MusicSource.loop = true;
            AudioManager.Instance.MusicSource.Play();
        }
       else if(currentScene == CurrentScene.VisualNovel)
        {
            AudioManager.Instance.MusicSource.clip = AudioManager.Instance.AudioClips["VisualNovel"];
            AudioManager.Instance.MusicSource.loop = true;
            AudioManager.Instance.MusicSource.Play();
        }
       else if(currentScene == CurrentScene.Level)
        {
            AudioManager.Instance.MusicSource.clip = AudioManager.Instance.AudioClips["Level"];
            AudioManager.Instance.MusicSource.loop = true;
            AudioManager.Instance.MusicSource.Play();
        }
    }
    private enum CurrentScene
    {
        MainMenu,
        VisualNovel,
        Level
    }

}
