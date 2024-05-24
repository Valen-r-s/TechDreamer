using UnityEngine;
using UnityEngine.UI;

public class NoteCounter : MonoBehaviour
{
    public Text noteCounterText;  // Asigna un Text UI en el Inspector
    private int noteCount = 0;

    void Start()
    {
        UpdateCounterUI();
    }

    public void IncrementCounter()
    {
        noteCount++;
        UpdateCounterUI();
    }

    private void UpdateCounterUI()
    {
        noteCounterText.text = "Notas: " + noteCount + "/8" ;
    }
}
