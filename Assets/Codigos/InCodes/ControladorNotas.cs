using UnityEngine;

public class Notemanager : MonoBehaviour
{
    public GameObject[] notas; // Arreglo que contendr� todas las notas
    private int indiceNotaActual = 0; // �ndice de la nota que se est� mostrando actualmente

    void Start()
    {
        // Desactivar todas las notas excepto la primera
        for (int i = 1; i < notas.Length; i++)
        {
            notas[i].SetActive(false);
        }
    }

    void Update()
    {
        // Si se presiona la tecla "E" y la nota actual est� activa
        if (Input.GetKeyDown(KeyCode.E) && notas[indiceNotaActual].activeSelf)
        {
            // Desactivar la nota actual
            notas[indiceNotaActual].SetActive(false);

            // Incrementar el �ndice para mostrar la siguiente nota
            indiceNotaActual++;

            // Si el �ndice es mayor o igual al tama�o del arreglo, volver al inicio
            if (indiceNotaActual >= notas.Length)
            {
                indiceNotaActual = 0;
            }

            // Activar la siguiente nota
            notas[indiceNotaActual].SetActive(true);
        }
    }
}