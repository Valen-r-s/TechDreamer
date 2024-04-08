using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public List<GameObject> notes; // Asegúrate de asignar esto en el inspector
    private int currentNoteIndex = 0;

    void Start()
    {
        DisableAllNotes();
        ActivateNextNote();
    }

    void DisableAllNotes()
    {
        foreach (var note in notes)
        {
            note.SetActive(false);
        }
    }

    public void ActivateNextNote()
    {
        if (currentNoteIndex < notes.Count)
        {
            notes[currentNoteIndex].SetActive(true);
            currentNoteIndex++;
        }
    }
}
