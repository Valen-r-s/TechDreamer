using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class DialogueManager1 : MonoBehaviour
{
    public TextMeshProUGUI Dialogue;
    public TextMeshProUGUI continuarButton;
    public string[] lines;
    public float textSpeed;

    private int index;

    void Start()
    {
        Dialogue.text = string.Empty;
        continuarButton.gameObject.SetActive(false);
        startDialogo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (Dialogue.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                Dialogue.text = lines[index];
                mostrarContinuar(false); 
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
        Dialogue.text = string.Empty; 
        foreach (char c in lines[index].ToCharArray())
        {
            Dialogue.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        mostrarContinuar(true); 
    }


    void EndDialog()
    {
        gameObject.SetActive(false);
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            Dialogue.text = string.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            EndDialog();
        }
        mostrarContinuar(false); 
    }

    void mostrarContinuar(bool mostrar)
    {
        continuarButton.gameObject.SetActive(mostrar);
    }
}
