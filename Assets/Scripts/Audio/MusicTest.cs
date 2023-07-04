using UnityEngine;

public class MusicTest : MonoBehaviour
{
    private void Start()
    {
        if(AudioManager.Instance != null)
        {
            Debug.Log("pLAY ");
            AudioManager.Instance.MusicSource.clip = AudioManager.Instance.AudioClips["Soundtrack Jungle"];
            AudioManager.Instance.MusicSource.Play();
        }
   
    }
}
