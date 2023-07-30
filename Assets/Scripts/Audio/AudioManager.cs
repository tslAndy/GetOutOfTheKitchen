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
    [SerializeField] private List<AudioClip> Clips;
    [HideInInspector] public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
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
            }
        }
    }
}
