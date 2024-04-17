using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pistas : MonoBehaviour
{
    public GameObject dialogPanel;      // Panel que contiene el texto de las pistas
    public TextMeshProUGUI dialogText;  // Componente de texto para mostrar las pistas
    public string[] hints;              // Array público de pistas, editable desde el Inspector
    private int currentHintIndex = 0;   // Índice para controlar la pista actual mostrada
    private bool isWaitingToClose = false;

    void Start()
    {
        if (hints.Length == 0)
            Debug.LogWarning("No se han configurado pistas en el HintManager.");

        dialogPanel.SetActive(false);    // Asegura que el panel de pistas está desactivado al inicio
    }

    public void Mostrarpista()
    {
        if (dialogPanel.activeSelf)
        {
            // Si el panel ya está activo, ocultarlo
            dialogPanel.SetActive(false);
            StartCoroutine(WaitAndClosePanel());
        }
        else
        {
            // Mostrar la siguiente pista si el panel está inactivo
            if (currentHintIndex < hints.Length)
            {
                dialogText.text = hints[currentHintIndex];  // Establece la pista actual
                dialogPanel.SetActive(true);                // Muestra el panel
                currentHintIndex++;                         // Avanza al siguiente índice de pista
            }
            else
            {
                currentHintIndex = 0;
            }
        }
    }

    IEnumerator WaitAndClosePanel()
    {
        // Espera un breve momento para evitar conflictos de input
        isWaitingToClose = true;
        yield return new WaitForSeconds(0.01f);
        isWaitingToClose = false;
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (dialogPanel.activeSelf)
            {
                dialogPanel.SetActive(false);
            }
        }

    }

}
