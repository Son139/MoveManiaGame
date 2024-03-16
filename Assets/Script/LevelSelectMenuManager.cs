using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenuManager : MonoBehaviour
{
    public Sprite starFilled;

    public List<GameObject> starSystems = new List<GameObject>();

    private static LevelSelectMenuManager instance;
    public static LevelSelectMenuManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        FindStarSystems();
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
        if (GameManager.instance == null || StarLevelController.instance == null)
        {
            return;
        }

        int starsEarned = StarLevelController.instance.GetStarsForLevel();
        Debug.Log("số sao kiếm được: " + starsEarned);

        if (starsEarned == 0)
        {
            return;
        }
        int levelName = GameManager.instance.GetCurrentLevelIndex();

        GameObject starSystem = starSystems[levelName - 1];

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
