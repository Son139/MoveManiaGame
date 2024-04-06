using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

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
        }
    }

    private void Start()
    {
        currentLives = maxLives;
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartUI();
        if (currentLives <= 0)
        {
            StarLevelController.instance.loseGame = true;
            LevelTitleManager.instance.UpdateLevelTitle(currentLives);
            AudioManager.instance.PlaySFX(AudioManager.instance.loseGame);
            completedLevel.SetActive(true);
            LevelTween.instance.LevelComplete();
            StarLevelController.instance.CompletedLevel();
            StartCoroutine(PauseGameAfterDelay(1.4f)); 
        }
    }

    public IEnumerator PauseGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); 
        Time.timeScale = 0f; 
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
