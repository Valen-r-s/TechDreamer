using UnityEngine;
using TMPro; // Asegúrate de tener esta línea si usas TextMeshPro

public class DialogueHandler : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText; // Asegúrate de cambiar a Text si no usas TextMeshPro

    // Método para mostrar un diálogo específico
    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}