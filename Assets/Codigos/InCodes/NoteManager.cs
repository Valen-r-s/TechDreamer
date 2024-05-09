using UnityEngine;

public class NoteManager : MonoBehaviour
{
  
    public static NoteManager Instance { get; private set; }  // Singleton instance

    public GameObject[] notas;
    private int indiceNotaActual = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Opcional, solo si necesitas que persista entre escenas
        }
        else
        {
            Destroy(gameObject);  // Asegura que solo exista una instancia
        }
    }
    void Start()
    {
        // Inicializar todas las notas como inactivas excepto la primera
        for (int i = 1; i < notas.Length; i++)
        {
            notas[i].SetActive(false);
        }
        notas[0].SetActive(true);
    }

    // Llamar este método desde ActivadorNotas cuando el usuario cierre la nota con Escape
    public void AvanzarNota()
    {
        notas[indiceNotaActual].SetActive(false);
        indiceNotaActual++;
        if (indiceNotaActual >= notas.Length)
        {
            indiceNotaActual = 0; // Opcional: Reiniciar el ciclo o detener la interacción
        }
        notas[indiceNotaActual].SetActive(true);
    }
}