using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Source ----------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip portalIn;
    public AudioClip portalOut;
    public AudioClip itemCollected;
    public AudioClip buttonClick;
    public AudioClip displayPanel;
    public AudioClip winGame;
    public AudioClip loseGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnpauseMusic()
    {
        musicSource.UnPause();
    }

    public void StopMusic()
    {
        musicSource?.Stop();
    }

    public void RestartMusic()
    {
        StopMusic();
        PlayMusic();
    }
}
