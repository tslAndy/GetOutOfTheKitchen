using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("----- Audio Mixers -----")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    [Header("----- Audio Clips -----")]
    public List<AudioClip> Clips;
    [HideInInspector] public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Instance = this;
        foreach(var clip in Clips)
        {
            AudioClips.Add(clip.name, clip);
        }
    }
    public void PlaySFX(string _clip)
    {
        foreach(KeyValuePair<string, AudioClip> clip in AudioClips)
        {
            if (clip.Key == _clip)
            {
                SFXSource.PlayOneShot(clip.Value);
                Debug.Log("Play SFX");
            }
        }
    }
}
