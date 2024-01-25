using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Class for handling timers
[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    private IEnumerator countdown;

    // Only invoked when the timer ends naturally
    public event Action OnTimerEnd;

    public float TimeLeft { get; private set; } = 0f;

    private void Awake()
    {
        if (!TryGetComponent(out timerText))
        {
            Debug.LogError("Timer component not found on the game object.");
        }
    }

    // Start a timer for the given duration
    public void StartTimer(float duration)
    {
        StopTimer();
        countdown = Countdown(duration);
        StartCoroutine(countdown);
    }

    public void StopTimer()
    {
        PauseTimer();
        countdown = null;
        TimeLeft = 0f;
        timerText.text = TimeLeft.ToString("F1");
    }

    public void PauseTimer()
    {
        if (countdown != null)
        {
            StopCoroutine(countdown);
        }
    }

    public void ResumeTimer()
    {
        if (countdown != null)
        {
            StartCoroutine(countdown);
        }
    }

    public void AddTime(float duration)
    {
        TimeLeft += duration;
    }

    public void SubtractTime(float duration)
    {
        TimeLeft -= duration;
    }

    private IEnumerator Countdown(float duration)
    {
        Debug.Log($"Starting countdown on {transform.parent.name} for {duration}.");
        TimeLeft = duration;
        while (TimeLeft > 0)
        {
            timerText.text = TimeLeft.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            TimeLeft -= 0.1f;
        }
        timerText.text = "0.0";
        countdown = null;
        OnTimerEnd?.Invoke();
        Debug.Log($"Countdown on {transform.parent.name} ended.");
    }
}
