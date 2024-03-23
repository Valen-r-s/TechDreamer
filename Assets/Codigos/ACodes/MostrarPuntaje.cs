using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarPuntaje : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje; // Asigna esto en el Inspector

    void Start()
    {
        // Lee el puntaje de PlayerPrefs
        float puntaje = PlayerPrefs.GetFloat("Puntaje", 0); // Usa 0 como valor por defecto
        textoPuntaje.text = "" + puntaje.ToString("F2");
    }
}
