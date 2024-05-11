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
