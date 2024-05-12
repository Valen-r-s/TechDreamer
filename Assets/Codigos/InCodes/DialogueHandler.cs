using UnityEngine;
using TMPro; // Aseg�rate de tener esta l�nea si usas TextMeshPro

public class DialogueHandler : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText; // Aseg�rate de cambiar a Text si no usas TextMeshPro

    // M�todo para mostrar un di�logo espec�fico
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