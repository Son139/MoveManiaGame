using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTitleManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string levelName = currentScene.name;

        titleText.text = levelName;
    }
}
