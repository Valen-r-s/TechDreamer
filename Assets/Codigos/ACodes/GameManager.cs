using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float puntaje;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CalcularPuntaje(float tiempoRestante)
    {
        // Ejemplo de cálculo, ajusta esto según tu juego
        puntaje = tiempoRestante * 10; // Por ejemplo, cada segundo restante vale 100 puntos
    }
}
