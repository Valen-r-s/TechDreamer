using UnityEngine;
using UnityEngine.UI;

public class ActivadorNotas : MonoBehaviour
{
    public GameObject notaVisual;
    public GameObject ObjetoVisual;
    public GameObject CamaraVisual;
    public GameObject ObjEnEscena;
    public DialogoIntermedios dialogueManager;
    public Button CloseButton;
    public int dialogueIndex;
    public TimerINVESt timer; // Añade esta línea
    public NoteCounter noteCounter;  // Añade una referencia al NoteCounter

    private bool activa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activa)
        {
            ActivarNota();
        }
        //if (Input.GetKeyDown(KeyCode.Q) && activa)
        //{
        //    DesactivarNota();
        //    NoteManager.Instance.AvanzarNota();
        //}
    }

    public void ClosePanelAndShowDialogue()
    {
        DesactivarNota();
        NoteManager.Instance.AvanzarNota(); // Asegúrate de que este método hace lo que necesitas
        dialogueManager.ShowDialogueByIndex(dialogueIndex); // Asume que quieres mostrar un diálogo específico después de cerrar
    }

    public void ActivarNota()
    {
        notaVisual.SetActive(true);
        ObjetoVisual.SetActive(true);
        CamaraVisual.SetActive(true);
        ObjEnEscena.SetActive(false);
        dialogueManager.ShowDialogueForDocument(gameObject);

        // Pausar el temporizador
        timer.PauseTimer();
        
    }

    public void DesactivarNota()
    {
        notaVisual.SetActive(false);
        ObjetoVisual.SetActive(false);
        CamaraVisual.SetActive(false);
        ObjEnEscena.SetActive(true);

        // Reanudar el temporizador
        timer.ResumeTimer();
        // Incrementar el contador de notas
        noteCounter.IncrementCounter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activa = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activa = false;
            DesactivarNota();
        }
    }
}
