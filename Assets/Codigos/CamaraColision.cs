using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraColision : MonoBehaviour
{
    private float minDistancia = 0.6f;
    private float maxDistancia = 2.26f;
    private float suavidad = 0.4f; // Ajusta este valor para controlar la velocidad de la suavidad
    private float Distancia;

    private Vector3 Direccion;
    private float velocidadSuavidad = 0.0f; // Velocidad de cambio para SmoothDamp

    void Start()
    {
        Direccion = transform.localPosition.normalized;
        Distancia = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 PosDeCamara = transform.parent.TransformPoint(Direccion * maxDistancia);

        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, PosDeCamara, out hit))
        {
            Distancia = Mathf.Clamp(hit.distance, minDistancia, maxDistancia);
        }
        else
        {
            Distancia = maxDistancia;
        }

        float targetDistancia = Mathf.SmoothDamp(transform.localPosition.magnitude, Distancia, ref velocidadSuavidad, suavidad);
        transform.localPosition = Direccion * targetDistancia;
    }
}
