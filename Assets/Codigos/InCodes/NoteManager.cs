using UnityEngine;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    public List<GameObject> notes;
    private int currentNoteIndex = 0;

    void Start()
    {
        // Inicializar todas las notas como inactivas
        foreach (var note in notes)
        {
            note.SetActive(false);
        }

        // Activar la primera nota
        if (notes.Count > 0)
        {
            notes[currentNoteIndex].SetActive(true);
        }
    }

    // Llamado para activar la siguiente nota en la secuencia
    public void ActivateNextNote()
    {
        // Desactivar la nota actual
        notes[currentNoteIndex].SetActive(false);

        // Incrementar el índice para la próxima nota
        currentNoteIndex++;

        // Activar la próxima nota si hay más notas en la lista
        if (currentNoteIndex < notes.Count)
        {
            notes[currentNoteIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Todas las notas han sido recolectadas.");
        }
    }
}
