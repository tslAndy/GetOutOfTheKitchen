using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("PlayerMusicVolume"))
        {
            LoadMusic();
        }
        else
        {
            SetMusicVolume();
        }

        if(PlayerPrefs.HasKey("PlayerSFXVolume"))
        {
            LoadSFX();
        }
        else
        {
            SetSFXVolume();
        }
       
    }
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 30);
        PlayerPrefs.SetFloat("PlayerMusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 30);
        PlayerPrefs.SetFloat("PlayerSFXVolume", volume);
    }

    private void LoadMusic()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("PlayerMusicVolume", _musicSlider.value);
        SetMusicVolume();
    }
    private void LoadSFX()
    {
        _SFXSlider.value = PlayerPrefs.GetFloat("PlayerSFXVolume", _SFXSlider.value);
        SetMusicVolume();
    }
}
