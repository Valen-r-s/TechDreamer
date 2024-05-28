using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MostrarPuntajeNat : MonoBehaviour
{
    // Para mostrar el puntaje
    public TextMeshProUGUI textoPuntaje;
    public Image imagen1;
    public Image imagen2;
    public Image imagen3;
    public Sprite bronce;
    public Sprite plata;
    public Sprite oro;

    private float timeRemaining = 180; // 3 minutos en segundos
    private bool timerIsRunning = false;

    void Start()
    {
        StartTimer(); // Inicia el temporizador
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Llamar la funci?n que calcula y muestra el puntaje
                CalculateAndShowScore();
            }
        }
    }

    private void StartTimer()
    {
        timerIsRunning = true;
    }

    private void CalculateAndShowScore()
    {
        float puntajePorImagenes = PlayerPrefs.GetFloat("PuntajePorImagenes", 0);
        float puntajePorTiempo = CalculateScore(timeRemaining);
        float puntajeFinal = puntajePorImagenes + puntajePorTiempo;
        textoPuntaje.text = puntajeFinal.ToString("F0");

        if (puntajePorTiempo >= 300)
        {
            imagen1.sprite = bronce;
            imagen2.sprite = plata;
            imagen3.sprite = oro;
        }
        else if (puntajePorTiempo >= 200 && puntajePorTiempo < 300)
        {
            imagen1.sprite = bronce;
            imagen2.sprite = plata;
        }
        else if (puntajePorTiempo >= 100 && puntajePorTiempo < 200)
        {
            imagen1.sprite = bronce;
        }

        PlayerPrefs.SetFloat("PuntajePorImagenes", 0);
        PlayerPrefs.SetFloat("PuntajePorTiempo", 0);
        PlayerPrefs.Save();
    }

    private float CalculateScore(float timeRemaining)
    {
        if (timeRemaining >= 160) // Entre 2:40 y 3:00 minutos
        {
            return 300; // Oro
        }
        else if (timeRemaining >= 120 && timeRemaining < 160) // Entre 2:00 y 2:39 minutos
        {
            return 200; // Plata
        }
        else if (timeRemaining >= 0 && timeRemaining < 120) // Entre 0 y 1:59 minutos
        {
            return 100; // Bronce
        }
        else // M?s de 3 minutos
        {
            return 50; // Puntuaci?n m?nima por completar la misi?n
        }
    }
}
