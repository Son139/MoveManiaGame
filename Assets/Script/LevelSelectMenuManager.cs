using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenuManager : MonoBehaviour
{
    public static LevelSelectMenuManager instance;
    public Sprite starFilled;

    public List<GameObject> starSystems = new List<GameObject>();

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
        FindStarSystems();
    }

    public void Update()
    {
        //UpdateStarMenuLevel();
    }

    void FindStarSystems()
    {
        // Tìm tất cả các GameObject có chứa starSystem component
        GameObject[] starSystemObjects = GameObject.FindGameObjectsWithTag("StarSystem");

        // Lưu trữ các starSystem vào danh sách
        foreach (GameObject starSystemObject in starSystemObjects)
        {
            starSystems.Add(starSystemObject);
        }
    }

    public void UpdateStarMenuLevel()
    {
        if (GameManager.instance == null || StarManager.instance == null)
        {
            Debug.LogError("Lỗi ở đây");
            return;
        }

        int starsEarned = StarManager.instance.GetStarsForLevel();
        Debug.Log("Số sao ở StarLevelMenu: " + starsEarned);

        if (starsEarned == 0)
        {
            return;
        }
        int levelName = GameManager.instance.GetCurrentLevelIndex();

        GameObject starSystem = starSystems[levelName-1];
        
        Debug.Log("level " + levelName + " :" + starSystem.transform.childCount);

        if (starsEarned == 0)
        {
            return;
        }

        for (int i = 0; i < starSystem.transform.childCount; i++)
        {
            GameObject starObject = starSystem.transform.GetChild(i).gameObject;
            Image starImage = starObject.GetComponent<Image>();

            if (i < starsEarned)
            {
                starImage.sprite = starFilled;
            }
        }
    }
}
