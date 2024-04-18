using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class tutoDialogue1 : MonoBehaviour
{
    public TextMeshProUGUI Dialogue;
    public Button Button;
    public Button skip;
    public string[] lines;
    public float textSpeed;

    private int index;

    void Start()
    {
        Dialogue.text = string.Empty;
        Button.gameObject.SetActive(false);
        skip.gameObject.SetActive(false);
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
                mostrarContinuar(true);

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

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            Dialogue.text = string.Empty;
            StartCoroutine(typeLine());
        }

    }

    void mostrarContinuar(bool mostrar)
    {
        Button.gameObject.SetActive(mostrar);
        skip.gameObject.SetActive(mostrar);
    }
}
