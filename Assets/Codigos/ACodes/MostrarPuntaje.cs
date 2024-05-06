using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MostrarPuntaje : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje;
    public Image imagen1;
    public Image imagen2;
    public Image imagen3;
    public Sprite bronce;
    public Sprite plata;
    public Sprite oro;

    void Start()
    {
        float puntajeFinal = PlayerPrefs.GetFloat("PuntajePorImagenes", 0) + PlayerPrefs.GetFloat("PuntajePorTiempo", 0);
        textoPuntaje.text = "" + puntajeFinal.ToString("F0");

        if (puntajeFinal  > 60 && puntajeFinal < 600)
        {
            imagen1.sprite = bronce;
        }
        else if (puntajeFinal >= 600 && puntajeFinal <=1400)
        {
            imagen1.sprite = bronce;
            imagen2.sprite = plata;
        }
        else if (puntajeFinal > 1300)
        {
            imagen1.sprite = bronce;
            imagen2.sprite = plata;
            imagen3.sprite = oro;
        }


        PlayerPrefs.SetFloat("Puntaje", 0); 
        PlayerPrefs.Save();
    }
}
