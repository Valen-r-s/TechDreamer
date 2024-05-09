using UnityEngine;

public class ActivadorNotas : MonoBehaviour
{
    public GameObject notaVisual;
    public GameObject ObjetoVisual;
    public GameObject CamaraVisual;
    public GameObject ObjEnEscena;

    private bool activa;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activa)
        {
            ActivarNota();
        }
        if (Input.GetKeyDown(KeyCode.Return) && activa)
        {
            DesactivarNota();
            // Llama al controlador para avanzar a la siguiente nota
            NoteManager.Instance.AvanzarNota();
        }
    }

    public void ActivarNota()
    {
        notaVisual.SetActive(true);
        ObjetoVisual.SetActive(true);
        CamaraVisual.SetActive(true);
        ObjEnEscena.SetActive(false);
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
