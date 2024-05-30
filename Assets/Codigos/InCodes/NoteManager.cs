using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }
    public GameObject[] notas;
    private int indiceNotaActual = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Reasignar las referencias de los documentos
        notas = GameObject.FindGameObjectsWithTag("Documento");
        System.Array.Sort(notas, (x, y) => string.Compare(x.name, y.name));  // Ordena por nombre

        // Reactiva el documento inicial y desactiva los demás
        if (notas.Length > 0)
        {
            foreach (GameObject nota in notas)
            {
                nota.SetActive(false);
            }
            notas[0].SetActive(true);
            indiceNotaActual = 0; // Restablece el índice al principio para cada nuevo juego
        }
    }

    public void AvanzarNota()
    {
        if (notas != null && indiceNotaActual < notas.Length)
        {
            notas[indiceNotaActual].SetActive(false);
            indiceNotaActual++;
            if (indiceNotaActual >= notas.Length)
            {
                indiceNotaActual = 0; // O reiniciar el ciclo o finalizar
            }
            notas[indiceNotaActual].SetActive(true);
        }
    }
}
