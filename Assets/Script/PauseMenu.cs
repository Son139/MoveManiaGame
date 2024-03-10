using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private bool fromPauseGame = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        fromPauseGame = true;
        audioManager.PauseMusic();
        Time.timeScale = 0;
    }

    public void GoToHome()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        MenuManager.instance.ShowMainMenu();
        GameManager.instance.hideGameObject();
        audioManager.RestartMusic();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        audioManager.StopMusic();
        SceneManager.LoadScene("Main Menu");
        MenuManager.instance.ShowSelectLevelMenu();
        GameManager.instance.hideGameObject();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.RestartMusic();
        pauseMenu?.SetActive(false);
        Timer.ResetTimer();
        HeathManager.instance.ResetLives();                                           
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        pauseMenu?.SetActive(false);
        audioManager.UnpauseMusic();
        Time.timeScale = 1;
    }
}
