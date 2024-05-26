using UnityEngine;
using UnityEngine.UI;

public class PuntajeM : MonoBehaviour
{
    public Image imagen1; // Medalla de bronce
    public Image imagen2; // Medalla de plata
    public Image imagen3; // Medalla de oro
    public Sprite bronce;
    public Sprite plata;
    public Sprite oro;

    void Start()
    {
        // Obtener el puntaje final desde el ScoreManager
        float finalScore = ScoreManager.finalScore;

        // Mostrar medallas en función del puntaje final
        if (finalScore >= 2400)
        {
            imagen3.sprite = oro;
            imagen2.sprite = plata;
            imagen1.sprite = bronce;
        }
        else if (finalScore >= 1500)
        {
            imagen2.sprite = plata;
            imagen1.sprite = bronce;
        }
        else if (finalScore >= 100)
        {
            imagen1.sprite = bronce;
        }
       
    }
}
