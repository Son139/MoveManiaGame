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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int remainingLives = HeathManager.instance.GetRemainingLives();
        UpdateLevelTitle(remainingLives);
    }

    public void UpdateLevelTitle(int remainingLives)
    {
        string levelName = SceneManager.GetActiveScene().name;
        titleText.text = (remainingLives <= 0) ? "YOU LOSE!" : levelName;
        titleText.fontSize = (remainingLives <= 0) ? 65 : titleText.fontSize;
    }
}
