using UnityEngine;

public class Notemanager : MonoBehaviour
{
    public GameObject[] notas; // Arreglo que contendrá todas las notas
    private int indiceNotaActual = 0; // Índice de la nota que se está mostrando actualmente

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
        // Si se presiona la tecla "E" y la nota actual está activa
        if (Input.GetKeyDown(KeyCode.E) && notas[indiceNotaActual].activeSelf)
        {
            // Desactivar la nota actual
            notas[indiceNotaActual].SetActive(false);

            // Incrementar el índice para mostrar la siguiente nota
            indiceNotaActual++;

            // Si el índice es mayor o igual al tamaño del arreglo, volver al inicio
            if (indiceNotaActual >= notas.Length)
            {
                indiceNotaActual = 0;
            }

            // Activar la siguiente nota
            notas[indiceNotaActual].SetActive(true);
        }
    }
}