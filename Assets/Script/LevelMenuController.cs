﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public Button[] buttons;
    public float disabledAlpha = 0.4f;

    public float delayTime = 0.5f;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        InitializeButtons(unlockedLevel);
    }
    //private void Awake()
    //{
    //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    //    InitializeButtons(unlockedLevel);
    //    //InitializeButtons();
    //}

    //public void InitializeButtons()
    //{
    //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    //    for (int i = 0; i < buttons.Length; i++)
    //    {
    //        bool interactable = i < unlockedLevel;
    //        buttons[i].interactable = interactable;
    //        SetButtonState(buttons[i], interactable);
    //    }
    //}

    public void InitializeButtons(int unlockedLevel)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            bool interactable = i < unlockedLevel;
            buttons[i].interactable = interactable;
            SetButtonState(buttons[i], interactable);
        }
    }

    public void SetButtonState(Button button, bool interactable)
    {
        Image[] childImages = button.GetComponentsInChildren<Image>();

        if (childImages.Length > 1)
        {
            Color targetColor = new Color(1f, 1f, 1f, interactable ? 1f : disabledAlpha);
            foreach (Image image in childImages)
            {
                if (image != button.image) // Skip the button's own image
                {
                    image.color = targetColor;
                }
            }
        }
    }

     public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        StartCoroutine(LoadLevelWithDelay(levelName));
    }

    private IEnumerator LoadLevelWithDelay(string levelName)
    {
        // Load scene
        SceneManager.LoadScene(levelName);

        // Đợi cho một phần nhỏ của frame hiện tại
        yield return null;
        HeathManager.instance.ResetLives();

        Timer.ResetTimer();

        // Ẩn tất cả các menu
        MenuManager.instance.HideMenuAll();

        // Setup new level
        GameManager.instance.SetupNewLevel();
    }
}