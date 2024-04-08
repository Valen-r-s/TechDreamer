using UnityEngine;

public class ActivadorNotas: MonoBehaviour
{
    public GameObject notaVisual; // La representación visual de la nota
    private NoteManager noteManager;

    void Start()
    {
        // Encuentra el NoteManager en la escena
        noteManager = FindObjectOfType<NoteManager>();
    }

    void Update()
    {
        // Verifica si el jugador está presionando 'E' y la nota está activa
        if (Input.GetKeyDown(KeyCode.E) && gameObject.activeInHierarchy)
        {
            // Posiblemente realizar una acción, como mostrar un detalle de la nota
            notaVisual.SetActive(true);
        }
        // Verifica si el jugador está presionando 'Escape' y la nota está activa
        else if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeInHierarchy)
        {
            notaVisual.SetActive(false);
        }
    }

    // Se llama cuando el jugador colisiona con la nota
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notifica al NoteManager que active la siguiente nota
            noteManager.ActivateNextNote();
        }
    }

    // Opcional: Si quieres desactivar la visualización cuando el jugador se aleja
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notaVisual.SetActive(false);
        }
    }
}
