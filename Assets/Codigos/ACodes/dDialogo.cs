using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dDialogo : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI continuar;
    public string[] lines;
    public float textSpeed;
    public bool closeOnEnter = false; // Controla si el diálogo se cierra con Enter

    public GameObject QuizPanel;

    private int index;
    void Start()
    {
        textComponent.text = string.Empty;
        continuar.gameObject.SetActive(false); // Asegurarse de que el texto esta oculto al inicio

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
        StartCoroutine(typeLine());
    }

    IEnumerator typeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        mostrarContinuar(true); // Muestra el texto "Presiona Enter para continuar" al final de cada l?nea
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
            // En lugar de intentar avanzar, oculta directamente el panel de di?logo
            EndDialog();
        }
        mostrarContinuar(false); // Asegurarse de que el texto de continuar se oculta cuando se avanza a la pr?xima l?nea o el di?logo finaliza
    }

    void EndDialog()
    {
        gameObject.SetActive(false); // O quiz?s desees desactivar solo el panel de di?logo en lugar de todo el gameObject si es m?s apropiado.
                                     // Aqu? puedes agregar cualquier otra l?gica de limpieza o de transici?n que necesites.
    }

    void mostrarContinuar(bool mostrar)
    {
        continuar.gameObject.SetActive(mostrar);
    }
}
