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
        }
        else if (SceneManager.GetActiveScene().name == "VisualNovel1")
        {
            currentScene = CurrentScene.VisualNovel;
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            currentScene = CurrentScene.Level;
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
