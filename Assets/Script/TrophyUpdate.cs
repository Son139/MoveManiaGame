using TMPro;
using UnityEngine;
public class TrophyUpdate : MonoBehaviour
{
    public TextMeshProUGUI highLevelText;

    private void Start()
    {
        UpdateHighLevelText();
    }

    private void UpdateHighLevelText()
    {
        if (PlayerPrefs.HasKey("HighLevelIndex"))
        {
            int highLevelIndex = PlayerPrefs.GetInt("HighLevelIndex");
            highLevelText.text = "Level: " + highLevelIndex.ToString();
        }
        else
        {
            highLevelText.text = "No Level";
        }
    }
}
