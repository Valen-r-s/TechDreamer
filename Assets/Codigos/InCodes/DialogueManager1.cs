using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager1 : MonoBehaviour
{
    public TextMeshProUGUI Dialogue;
    public GameObject panelDialogo; // Asegúrate de asignar el panel de diálogo en el Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ClosePanel();
        }
    }

    void ClosePanel()
    {
        panelDialogo.SetActive(false); // Desactiva el panel de diálogo
    }
}