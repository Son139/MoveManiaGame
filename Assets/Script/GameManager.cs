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
    public GameObject pauseSystem;
    public GameObject healthSystem;
    public GameObject finishPoint;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
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
        //pauseSystem?.SetActive(true);
        //healthSystem?.SetActive(true);
        //finishPoint?.SetActive(true);
    }

    public void HideGameObject()
    {
        //pauseSystem?.SetActive(false);
        //healthSystem?.SetActive(false);
        //finishPoint?.SetActive(false);
    }
}
