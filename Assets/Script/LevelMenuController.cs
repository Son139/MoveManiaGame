using System.Collections;
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
        SceneManager.sceneLoaded += OnSceneLoaded; // Đăng ký sự kiện khi scene được load
        InitializeButtons();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Hủy đăng ký sự kiện khi đối tượng bị hủy
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeButtons(); // Gọi hàm InitializeButtons() khi scene được load lại
    }

    public void InitializeButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int nextLevelToUnlock = PlayerPrefs.GetInt("NextLevelToUnlock", 1); // Lấy index của level cần mở khóa

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            SetButtonState(buttons[i], false);
        }

        for (int i = 0; i < nextLevelToUnlock; i++)
        {
            buttons[i].interactable = true;
            SetButtonState(buttons[i], true);
        }

        // Mở khóa level tiếp theo nếu cần
        //if (nextLevelToUnlock != -1 && nextLevelToUnlock <= buttons.Length)
        //{
        //    buttons[nextLevelToUnlock - 1].interactable = true;
        //    SetButtonState(buttons[nextLevelToUnlock - 1], true);
        //}
    }

    public void SetButtonState(Button button, bool interactable)
    {
        Image[] childImages = button.GetComponentsInChildren<Image>();

        if (childImages.Length > 1)
        {
            Color targetColor = new Color(1f, 1f, 1f, interactable ? 1f : disabledAlpha);
            foreach (Image image in childImages)
            {
                if (image != button.image)
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
        yield return null;

        AudioManager.instance.RestartMusic();
        HealthManager.instance.ResetLives();
        Timer.ResetTimer();
        MenuManager.instance.HideMenuAll();

        GameManager.instance.SetupNewLevel();
    }
}
