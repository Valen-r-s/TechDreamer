using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzParpadeante : MonoBehaviour
{
    public Light targetLight; // La luz a modificar
    private float minIntensity = 6; // Intensidad m�nima de la luz
    private float maxIntensity = 30; // Intensidad m�xima de la luz
    private float speed = 1.5f; // Velocidad de la transici�n de la intensidad

    private void Update()
    {
        // Calcular la nueva intensidad usando una funci�n sinusoidal
        float intensity = minIntensity + (Mathf.Sin(Time.time * speed) + 1f) / 2f * (maxIntensity - minIntensity);
        targetLight.intensity = intensity;
    }
}