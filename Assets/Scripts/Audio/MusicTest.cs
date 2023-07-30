using UnityEngine;

public class MusicTest : MonoBehaviour
{
    private void Start()
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.MusicSource.clip = AudioManager.Instance.AudioClips["MainMenu"];
            AudioManager.Instance.MusicSource.Play();
        }
   
    }
}
