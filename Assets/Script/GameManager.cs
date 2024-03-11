using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Animator transitionAnim;
    public GameObject pauseSystem;
    public GameObject healthSystem;

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

    public void GoToHome()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        //StartCoroutine(LoadLevel());
        SceneManager.LoadScene("Main Menu");
        MenuManager.instance.ShowSelectLevelMenu();
        hideGameObject();
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public int GetCurrentLevelIndex()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Match match = Regex.Match(sceneName, @"\d+");
        int levelNumber = 1;

        if (match.Success)
        {
            levelNumber = Convert.ToInt32(match.Value);
        }

        return levelNumber;
    }

    public void SetupNewLevel()
    {
        pauseSystem?.SetActive(true);
        healthSystem?.SetActive(true);
    }

    public void hideGameObject()
    {
        pauseSystem?.SetActive(false);
        healthSystem?.SetActive(false);
    }

    //IEnumerator LoadLevel()
    //{
    //    transitionAnim.SetTrigger("End");
    //    yield return new WaitForSeconds(1);
    //    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    //    HeathManager.instance.ResetLives();
    //    transitionAnim.SetTrigger("Start");
    //}
}
