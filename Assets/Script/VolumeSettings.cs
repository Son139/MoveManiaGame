using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolumn"))
        {
            LoadVolumn();
        }
        else
        {
            SetMusicVolumn();
            SetSFXVolumn();
        }
    }

    public void SetMusicVolumn()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolumn", volume);
    }
    public void SetSFXVolumn()
    {
        float volume = sfxSlider.value;
        float volumeInDecibels = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("SFX", volumeInDecibels);
        PlayerPrefs.SetFloat("SFXVolumn", volume);
    }

    private void LoadVolumn()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolumn", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumn", 0.5f);

        SetMusicVolumn();
        SetSFXVolumn();
    }
}
