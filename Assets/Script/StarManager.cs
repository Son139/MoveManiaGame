using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;

    private int[] starsEarnedPerLevel;

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
    public void SaveStarsForLevel(int levelIndex, int starsEarned)
    {
        string key = "StarsForLevel" + levelIndex;
        PlayerPrefs.SetInt(key, starsEarned);
        PlayerPrefs.Save();
    }

    public int GetStarsForLevel()
    {
        int levelIndex = GameManager.instance.GetCurrentLevelIndex();
        string key = "StarsForLevel" + levelIndex;
        return PlayerPrefs.GetInt(key, 0); // Trả về 0 nếu không tìm thấy dữ liệu
    }
}
