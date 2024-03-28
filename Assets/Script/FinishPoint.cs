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
                MarkNextLevelToUnlock();
                //UnlockNextLevel();
                completedLevel.SetActive(true);
                LevelTween.instance.LevelComplete();
                AudioManager.instance.PlaySFX(AudioManager.instance.winGame);

                int remainingLives = HealthManager.instance.GetRemainingLives();
                LevelTitleManager.instance.UpdateLevelTitle(remainingLives);

                StarLevelController.instance.CompletedLevel();
                LevelSelectMenuManager.Instance.UpdateStarMenuLevel();

                Timer.StopTimer();
                // Mở khóa level 2 ngay sau khi kết thúc level 1
                //PlayerPrefs.SetInt("UnlockedLevel", 2);
                //PlayerPrefs.Save();
            }
            else
            {
                GameManager.instance.LoadScene(levelName);
            }
        }
    }

    //void UnlockNextLevel()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);

    //    if (currentSceneIndex >= reachedIndex)
    //    {
    //        PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
    //        PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
    //        PlayerPrefs.Save();
    //    }
    //}
    void MarkNextLevelToUnlock()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;
        PlayerPrefs.SetInt("NextLevelToUnlock", nextLevelIndex);
        Debug.Log("lưu "+ nextLevelIndex);
        PlayerPrefs.Save();
    }
}
