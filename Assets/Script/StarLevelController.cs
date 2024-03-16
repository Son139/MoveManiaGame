using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarLevelController : MonoBehaviour
{
    public static StarLevelController instance;
    public GameObject[] starEmpty;
    public GameObject[] starFilled;
    public TextMeshProUGUI AmountItemsEarn;
    private int maxStars = 3;

    public bool loseGame = false;

    public float[] delays;
    public float scaleDuration = 1f;

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

    public void CompletedLevel()
    {
        int levelCurrent = GameManager.instance.GetCurrentLevelIndex();
        int starsEarned = CalculateStarsEarned();
        Debug.Log(starsEarned);
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

    public void DisplayStars(int starsEarned)
    {
        for (int i = 0; i < maxStars; i++)
        {
            if (i < starsEarned)
            {
                // Hiển thị và áp dụng animation cho ngôi sao đã kiếm được
                starEmpty[i].SetActive(false);
                starFilled[i].SetActive(true);
                AnimateStar(starFilled[i], delays[i]);
            }
            else
            {
                // Hiển thị và áp dụng animation cho ngôi sao không kiếm được
                starEmpty[i].SetActive(true);
                starFilled[i].SetActive(false);
                AnimateStar(starEmpty[i], delays[i]);
            }
        }
    }

    private void AnimateStar(GameObject star, float delay)
    {
        LeanTween.scale(star, new Vector3(1f, 1f, 1f), scaleDuration)
            .setDelay(delay)
            .setEase(LeanTweenType.easeOutElastic);
    }

    private void SaveStarsForLevel(int levelIndex, int starsEarned)
    {
        StarManager.instance.SaveStarsForLevel(levelIndex, starsEarned);
    }

    private void ResetStarsCollected()
    {
        ItemController.ResetStarsCollected();
    }
}
