using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    [SerializeField] GameObject completedLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            {
                //UnlockNextLevel();
                completedLevel.SetActive(true);
                LevelTween.instance.LevelComplete();
                AudioManager.instance.PlaySFX(AudioManager.instance.winGame);
                int remainingLives = HeathManager.instance.GetRemainingLives();
                LevelTitleManager.instance.UpdateLevelTitle(remainingLives);
                //Time.timeScale = 0f;
                LevelSelectMenuManager.instance.UpdateStarMenuLevel();
                StarLevelController.instance.CompletedLevel();
            }
            else
            {
                GameManager.instance.LoadScene(levelName);
            }
        }
    }

    void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
