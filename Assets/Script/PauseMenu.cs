using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    //[SerializeField] Animator transitionAnim;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        AudioManager.instance.PauseMusic();
        Time.timeScale = 0;
        PausePanelIntro();
    }

    public void GoToHome()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        MenuManager.instance.ShowMainMenu();
        GameManager.instance.HideGameObject();
        AudioManager.instance.RestartMusic();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene("Main Menu");
        MenuManager.instance.ShowSelectLevelMenu();
        GameManager.instance.HideGameObject();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
        int nextLevelToUnlock = PlayerPrefs.GetInt("NextLevelToUnlock", -1);
        Debug.Log("in cái lưu: " + nextLevelToUnlock);
    }

    //IEnumerator LoadLevel()
    //{
    //    transitionAnim.SetTrigger("End");

    //    transitionAnim.SetTrigger("Start");
    //}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.instance.RestartMusic();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        ItemController.ResetItemsCollected();
        Timer.ResetTimer();
        HealthManager.instance.ResetLives();
        Time.timeScale = 1;
    }

    public async void ResumeGame()
    {
        await PausePanelOutro();
        if (pauseMenu != null && !pauseMenu.IsDestroyed())
        {
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1;
        AudioManager.instance.UnpauseMusic();
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        AudioManager.instance.PlaySFX(AudioManager.instance.displayPanel);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        AudioManager.instance.PlaySFX(AudioManager.instance.displayPanel);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
