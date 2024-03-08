using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioSource audioSource;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        audioSource.Pause();
    }

    public void GoToHome()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        MenuManager.instance.ShowMainMenu();
        GameManager.instance.hideGameObject();
        pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Main Menu");
        MenuManager.instance.ShowSelectLevelMenu();
        GameManager.instance.hideGameObject();
        pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu?.SetActive(false);
        //audioSource.UnPause();
        Timer.ResetTimer();
        HeathManager.instance.ResetLives();                                           
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        pauseMenu?.SetActive(false);
        audioSource?.UnPause();
        Time.timeScale = 1;
    }
}
