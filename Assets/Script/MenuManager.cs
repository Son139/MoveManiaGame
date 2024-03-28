using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public GameObject mainMenu;
    public GameObject selectLevelMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ShowSelectLevelMenu()
    {
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
    }

    public void HideMenuAll()
    {
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TrophyBtn()
    {
        LevelTitleManager.instance.UpdateLevelTitle(3);
    }
}
