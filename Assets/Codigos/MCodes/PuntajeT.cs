using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuntajeT : MonoBehaviour
{
    public TextMeshProUGUI mensajeTexto; // Texto para mostrar el mensaje

    void Start()
    {
        // Obtener el puntaje final desde el ScoreManager
        float finalScore = ScoreManager.finalScore;

        // Mostrar medallas en funci�n del puntaje final
        if (finalScore >= 600)
        {
            mensajeTexto.text = "�Incre�ble! Se te da bastante bien esto, te va a ir muy bien en el juego �T� puedes!.";
        }
        else if (finalScore >= 400)
        {
            mensajeTexto.text = "�Buen trabajo! lo hiciste muy bien para ser tu primera vez, puedes iniciar el juego para algo m�s desafiante.";
        }
        else if (finalScore >= 200)
        {
            mensajeTexto.text = "Lo hiciste bien, pero puedes mejorar un poco, podr�as repetirlo a ver si te queda mejor.";
        }
        else if (finalScore >= 0)
        {
            mensajeTexto.text = "Parece que no completaste el tutorial, deberias practicar para poder comenzar el juego.";
        }
    }
}
