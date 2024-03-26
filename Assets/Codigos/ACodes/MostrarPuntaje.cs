using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarPuntaje : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje; 

    void Start()
    {
        float puntajeFinal = PlayerPrefs.GetFloat("PuntajePorImagenes", 0) + PlayerPrefs.GetFloat("PuntajePorTiempo", 0); 
        textoPuntaje.text = "" + puntajeFinal.ToString("F0");
        PlayerPrefs.SetFloat("Puntaje", 0); 
        PlayerPrefs.Save();
    }
}
