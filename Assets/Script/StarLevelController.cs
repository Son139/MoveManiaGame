using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarLevelController : MonoBehaviour
{
    public GameObject[] starEmpty;
    public GameObject[] starFilled;
    public TextMeshProUGUI AmountItemsEarn;
    private int maxStars = 3;

    private void Awake()
    {
        int levelCurrent = GameManager.instance.GetCurrentLevelIndex();
        int starsEarned = CalculateStarsEarned();
        DisplayStars(starsEarned);
        AmountItemsEarn.text = starsEarned.ToString();
        SaveStarsForLevel(levelCurrent, starsEarned);
        ResetStarsCollected();
    }

    private int CalculateStarsEarned()
    {
        int itemsCollected = ItemController.GetStarsCollected();
        int remainingLives = HeathManager.instance.GetRemainingLives();

        int starsEarned = itemsCollected - (maxStars - remainingLives);
        starsEarned = Mathf.Max(starsEarned, 0);
        starsEarned = Mathf.Clamp(starsEarned, 0, maxStars);

        return starsEarned;
    }

    private void DisplayStars(int starEarned)
    {
        //HideAllStars();
        for (int i = 0; i < starEarned; i++)
        {
            starEmpty[i].SetActive(false);
            starFilled[i].SetActive(true);
        }
    }

    private void SaveStarsForLevel(int levelIndex, int starsEarned)
    {
        StarManager.instance.SaveStarsForLevel(levelIndex, starsEarned);
    }

    private void ResetStarsCollected()
    {
        ItemController.ResetStarsCollected();
    }

    //private void HideAllStars()
    //{
    //    foreach (GameObject star in starEmpty)
    //    {
    //        star.SetActive(true);
    //    }

    //    foreach (GameObject star in starFilled)
    //    {
    //        star.SetActive(false);
    //    }
    //}
}
