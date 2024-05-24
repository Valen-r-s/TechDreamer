using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoTutoM : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI continuar;
    public string[] lines;
    public float textSpeed;
    public bool closeOnEnter = false; // Controla si el diálogo se cierra con Enter

    public GameObject[] arrows; // Arreglos de GameObjects de flechas en la escena
    public Canvas tutorialBlockCanvas; // Canvas para bloquear la interacción del juego

    // Lista de índices de líneas de diálogo que deben mostrar flechas
    public List<int> linesWithArrows;

    private int index;
    private int arrowIndex;

    void Start()
    {
        textComponent.text = string.Empty;
        continuar.gameObject.SetActive(false); // Asegurarse de que el texto está oculto al inicio
        HideAllArrows(); // Asegurarse de que las flechas estén ocultas al inicio
        if (tutorialBlockCanvas != null)
        {
            tutorialBlockCanvas.gameObject.SetActive(true); // Bloquea la interacción al inicio del tutorial
        }
        startDialogo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!closeOnEnter)
            {
                // Tu lógica para manejar el avance del diálogo
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                    mostrarContinuar(false);
                }
            }
            else
            {
                //Cerrar el diálogo si closeOnEnter está activo
                EndDialog();
            }
        }
    }

    void startDialogo()
    {
        index = 0;
        arrowIndex = 0;
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        mostrarContinuar(true); // Muestra el texto "Presiona Enter para continuar" al final de cada línea
        ShowArrowForCurrentLine(); // Mostrar la flecha para la línea actual, si corresponde
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            // En lugar de intentar avanzar, oculta directamente el panel de diálogo
            EndDialog();
        }
        mostrarContinuar(false); // Asegurarse de que el texto de continuar se oculta cuando se avanza a la próxima línea o el diálogo finaliza
    }

    void EndDialog()
    {
        gameObject.SetActive(false);
        if (tutorialBlockCanvas != null)
        {
            tutorialBlockCanvas.gameObject.SetActive(false); // Desbloquea la interacción al finalizar el tutorial
        }
        HideAllArrows(); // Oculta todas las flechas al finalizar el diálogo
    }

    void mostrarContinuar(bool mostrar)
    {
        continuar.gameObject.SetActive(mostrar);
    }

    void ShowArrowForCurrentLine()
    {
        HideAllArrows(); // Oculta todas las flechas antes de mostrar la nueva, si corresponde

        if (linesWithArrows.Contains(index) && arrowIndex < arrows.Length)
        {
            arrows[arrowIndex].SetActive(true);
            arrowIndex++;
        }
    }

    void HideAllArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
}
