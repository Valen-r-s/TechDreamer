using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerINVESt : MonoBehaviour
{
    public Text timerText;
    private float timeRemaining;
    private bool timerIsRunning = false;
    private bool isPaused = false;

    void Start()
    {
        // Inicia el temporizador con un tiempo específico
        timeRemaining = 60; // Por ejemplo, 60 segundos
        timerText.text = timeRemaining.ToString();
    }

    void Update()
    {
        if (timerIsRunning && !isPaused)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Aquí puedes llamar a una función cuando el tiempo se acabe
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Round(timeRemaining).ToString();
    }
}
