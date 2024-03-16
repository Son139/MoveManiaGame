using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeathManager : MonoBehaviour
{
    public static HeathManager instance;

    [SerializeField] GameObject completedLevel;

    public int maxLives = 3;
    public Image[] heartImages;
    public int currentLives;
    public int remainingLives;

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
            return;
        }
    }

    public void LoseLife()
    {
        currentLives--;
        //remainingLives = currentLives;
        //GetCurrentLives();
        //GetRemainingLives();
        UpdateHeartUI();
        if (currentLives <= 0)
        {
            StarLevelController.instance.loseGame = true;
            LevelTitleManager.instance.UpdateLevelTitle(currentLives);
            AudioManager.instance.PlaySFX(AudioManager.instance.loseGame);
            completedLevel.SetActive(true);
            LevelTween.instance.LevelComplete();
            StarLevelController.instance.DisplayStars(0);
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentLives;
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetRemainingLives()
    {
        remainingLives = currentLives;
        return remainingLives;
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateHeartUI();
    }
}
