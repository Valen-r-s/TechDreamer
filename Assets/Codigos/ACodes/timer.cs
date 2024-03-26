using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class timer : MonoBehaviour
{
    public float Timer = 60; 
    public TextMeshProUGUI textotimer;
    public int indiceEscenaPuntuacion;

    void Update()
    {

        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            int minutos = Mathf.FloorToInt(Timer / 60);
            int segundos = Mathf.FloorToInt(Timer % 60);
            textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            FinalizarJuego();
        }
        else
        {
            Timer = 0;
            int minutos = Mathf.FloorToInt(Timer / 60);
            int segundos = Mathf.FloorToInt(Timer % 60);
            textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            FinalizarJuego();
            CambiarEscena();
        }

    }


    public void FinalizarJuego()
    {
        float factorDePuntosPorSegundo = 10;
        float puntajePorTiempo = Mathf.Max(0, Timer) * factorDePuntosPorSegundo;
        PlayerPrefs.SetFloat("PuntajePorTiempo", puntajePorTiempo);
        PlayerPrefs.Save();
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene(indiceEscenaPuntuacion);
    }

}