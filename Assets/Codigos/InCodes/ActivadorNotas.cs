using UnityEngine;

public class ActivadorNotas : MonoBehaviour
{
    public GameObject notaVisual;
    public GameObject ObjetoVisual;
    public GameObject CamaraVisual;
    public GameObject ObjEnEscena;
    public DialogoIntermedios dialogueManager;

    private bool activa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activa)
        {
            ActivarNota();
        }
        if (Input.GetKeyDown(KeyCode.Q) && activa)
        {
            DesactivarNota();
            NoteManager.Instance.AvanzarNota();
        }
    }

    public void ActivarNota()
    {
        notaVisual.SetActive(true);
        ObjetoVisual.SetActive(true);
        CamaraVisual.SetActive(true);
        ObjEnEscena.SetActive(false);
        dialogueManager.ShowDialogueForDocument(gameObject);
    }

    public void DesactivarNota()
    {
        notaVisual.SetActive(false);
        ObjetoVisual.SetActive(false);
        CamaraVisual.SetActive(false);
        ObjEnEscena.SetActive(true);
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
