using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerINVESt : MonoBehaviour
{

    public Text timerText;
    private float timeRemaining = 180; // 3 minutos en segundos
    private bool timerIsRunning = false;
    public int indiceEscenaPuntuacion;

    void Start()
    {
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
                FinalizarJuego();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                FinalizarJuego();
                CambiarEscena();
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void PauseTimer()
    {
        timerIsRunning = false;
    }

    public void ResumeTimer()
    {
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(indiceEscenaPuntuacion);
    }

    public void FinalizarJuego()
    {
        float factorDePuntosPorSegundo = 10;
        float puntajePorTiempo = Mathf.Max(0, timeRemaining) * factorDePuntosPorSegundo;
        PlayerPrefs.SetFloat("PuntajeTiempo", puntajePorTiempo);
        PlayerPrefs.Save();
    }
}
