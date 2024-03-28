using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    static float elapsedTime;
    static bool isTimerRunning = true;

    private void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void ResetTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true; // Bật lại thời gian đếm khi reset
    }

    public static void StopTimer()
    {
        isTimerRunning = false; // Tắt thời gian đếm
    }

    public static void ResumeTimer()
    {
        isTimerRunning = true; // Tiếp tục thời gian đếm
    }
}
