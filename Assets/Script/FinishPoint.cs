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
                setHighLevel();
                completedLevel.SetActive(true);
                LevelTween.instance.LevelComplete();
                AudioManager.instance.PlaySFX(AudioManager.instance.winGame);

                int remainingLives = HealthManager.instance.GetCurrentLives();
                LevelTitleManager.instance.UpdateLevelTitle(remainingLives);

                StarLevelController.instance.CompletedLevel();
                LevelSelectMenuManager.Instance.UpdateStarMenuLevel();

                Timer.StopTimer();
            }
            else
            {
                GameManager.instance.LoadScene(levelName);
            }
        }
    }

    void MarkNextLevelToUnlock()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;
        PlayerPrefs.SetInt("NextLevelToUnlock", nextLevelIndex);
        Debug.Log("lưu " + nextLevelIndex);
        PlayerPrefs.Save();
    }

    void setHighLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int savedHighLevelIndex = PlayerPrefs.GetInt("HighLevelIndex", 0); 
        // So sánh index của cấp độ hiện tại với index của high level đã lưu
        if (currentSceneIndex > savedHighLevelIndex)
        {
            PlayerPrefs.SetInt("HighLevelIndex", currentSceneIndex); 
            PlayerPrefs.Save();
            Debug.Log("Lưu high level với index: " + currentSceneIndex);
        }
    }

}
