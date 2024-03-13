using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    //[SerializeField] Animator transitionAnim;

    private bool fromPauseGame = false;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //fromPauseGame = true;
        AudioManager.instance.PauseMusic();
        Time.timeScale = 0;
    }

    public void GoToHome()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        MenuManager.instance.ShowMainMenu();
        GameManager.instance.hideGameObject();
        AudioManager.instance.RestartMusic();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Main Menu");
        MenuManager.instance.ShowSelectLevelMenu();
        GameManager.instance.hideGameObject();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
    }

    //IEnumerator LoadLevel()
    //{
    //    transitionAnim.SetTrigger("End");

    //    transitionAnim.SetTrigger("Start");
    //}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.instance.RestartMusic();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        ItemController.ResetStarsCollected();
        Timer.ResetTimer();
        HeathManager.instance.ResetLives();
        Time.timeScale = 1;
    }

    public void ResumeGame()
    {
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
        AudioManager.instance.UnpauseMusic();
    }
}
