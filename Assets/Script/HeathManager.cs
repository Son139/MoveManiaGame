using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathManager : MonoBehaviour
{
    public static HeathManager instance;
    public int maxLives = 3;
    public Image[] heartImages;
    private int currentLives;
    public int remainingLives;

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
        currentLives = maxLives;
    }

        

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartUI();
        if (currentLives <= 0)
        {
            Debug.Log("Lose!!!");
        }
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentLives)
            {
                heartImages[i].enabled = true; 
            }
            else
            {
                heartImages[i].enabled = false; 
            }
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetRemainingLives()
    {
        remainingLives = currentLives;
        return remainingLives;
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateHeartUI();
    }
}
