using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Class for handling timers
public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    private IEnumerator countdown;

    public event Action OnTimerEnd;

    public float TimeLeft { get; private set; } = 0f;
    public bool IsRunning { get { return countdown != null; } }

    // Start a timer for the given duration
    public void StartTimer(float duration)
    {
        PauseTimer();
        countdown = Countdown(duration);
        StartCoroutine(countdown);
    }

    public void SetTimer(float duration)
    {
        TimeLeft = duration;
    }

    public void StopTimer()
    {
        PauseTimer();
        TimeLeft = 0f;
        if (timerText != null) timerText.text = TimeLeft.ToString("F1");
        OnTimerEnd?.Invoke();
        Debug.Log($"Countdown on {gameObject.name} ended.");
    }

    public void PauseTimer()
    {
        if (countdown != null)
        {
            StopCoroutine(countdown);
            countdown = null;
        }
    }

    public void ResumeTimer()
    {
        if (countdown != null)
        {
            PauseTimer();
        }
        countdown = Countdown(TimeLeft);
        StartCoroutine(countdown);
    }

    public void AddTime(float duration)
    {
        TimeLeft += duration;
    }

    public void SubtractTime(float duration)
    {
        TimeLeft -= duration;
    }

    // Assign a text component to display the timer
    public void AssignText(TMP_Text text)
    {
        timerText = text;
        Debug.Log($"Assigned {gameObject.name}'s timer text to {text.gameObject.name}.");
    }

    private IEnumerator Countdown(float duration)
    {
        Debug.Log($"Starting countdown on {gameObject.name} for {duration}.");
        TimeLeft = duration;
        while (TimeLeft > 0)
        {
            if (timerText != null) timerText.text = TimeLeft.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            TimeLeft -= 0.1f;
        }
        StopTimer();
    }
}
