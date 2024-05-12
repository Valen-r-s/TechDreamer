using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    public string text; // El texto del di�logo
    public GameObject relatedDocument; // El documento relacionado con este di�logo
}

public class DialogoIntermedios : MonoBehaviour
{
    public Dialogue[] dialogues;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    void Start()
    {
        dialoguePanel.SetActive(false); // Aseg�rate de que el panel est� oculto al inicio
    }

    public void ShowDialogueForDocument(GameObject document)
    {
        foreach (var dialogue in dialogues)
        {
            if (dialogue.relatedDocument == document)
            {
                dialogueText.text = dialogue.text;
                dialoguePanel.SetActive(true);
                return;
            }
        }
    }


    public void ShowDialogueByIndex(int index)
    {
        if (index >= 0 && index < dialogues.Length)
        {
            dialogueText.text = dialogues[index].text;
            dialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("�ndice de di�logo fuera de rango: " + index);
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeSelf)
        {
            HideDialogue();
        }
    }
}
