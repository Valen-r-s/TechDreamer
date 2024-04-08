using UnityEngine;

public class ActivadorNotas: MonoBehaviour
{
    public GameObject notaVisual; // La representaci�n visual de la nota
    private NoteManager noteManager;

    void Start()
    {
        // Encuentra el NoteManager en la escena
        noteManager = FindObjectOfType<NoteManager>();
    }

    void Update()
    {
        // Verifica si el jugador est� presionando 'E' y la nota est� activa
        if (Input.GetKeyDown(KeyCode.E) && gameObject.activeInHierarchy)
        {
            // Posiblemente realizar una acci�n, como mostrar un detalle de la nota
            notaVisual.SetActive(true);
        }
        // Verifica si el jugador est� presionando 'Escape' y la nota est� activa
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

    // Opcional: Si quieres desactivar la visualizaci�n cuando el jugador se aleja
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notaVisual.SetActive(false);
        }
    }
}
