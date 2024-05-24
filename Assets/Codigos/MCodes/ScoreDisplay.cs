using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Mostrar la puntuaci�n final
        scoreText.text = "Puntaje: " + Mathf.RoundToInt(ScoreManager.finalScore).ToString();

    }
}
