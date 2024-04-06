using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTitleManager : MonoBehaviour
{
    public static LevelTitleManager instance;
    public TextMeshProUGUI titleText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (HealthManager.instance != null)
        {
            int remainingLives = HealthManager.instance.GetRemainingLives();
            UpdateLevelTitle(remainingLives);
        }
    }

    public void UpdateLevelTitle(int remainingLives)
    {
        string levelName = SceneManager.GetActiveScene().name;
        titleText.text = (remainingLives <= 0) ? "YOU LOSE!" : levelName;
        titleText.fontSize = (remainingLives <= 0) ? 65 : titleText.fontSize;
    }
}