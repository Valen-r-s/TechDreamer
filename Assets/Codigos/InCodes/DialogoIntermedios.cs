using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class Dialogue
{
    public string text; // El texto del diálogo
    public GameObject relatedDocument; // El documento relacionado con este diálogo
}

public class DialogoIntermedios : MonoBehaviour
{
    public Dialogue[] dialogues;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string puntajeSceneName; // Nombre de la escena del puntaje

    void Start()
    {
        dialoguePanel.SetActive(false); // Asegúrate de que el panel está oculto al inicio
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
            // Verificar si es el último diálogo
            if (index == dialogues.Length - 1)
            {
                // Esperar unos segundos antes de cambiar de escena para permitir que el diálogo se lea
                StartCoroutine(WaitAndLoadScene(5)); // Espera 5 segundos, puedes ajustar esto
            }
        }
        else
        {
            Debug.LogWarning("Índice de diálogo fuera de rango: " + index);
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

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(puntajeSceneName);
    }
}
