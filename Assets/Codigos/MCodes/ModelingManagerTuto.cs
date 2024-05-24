using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModelingManagerTuto : MonoBehaviour
{
    public Button finishButton;
    public VertexScoreCalculator vertexScoreCalculator;

    void Start()
    {
        finishButton.onClick.AddListener(OnFinishButtonClicked);
    }

    void OnFinishButtonClicked()
    {
        // Calcular la puntuación final
        float finalScore = vertexScoreCalculator.CalculateFinalScore();

        // Guardar la puntuación en una clase estática
        ScoreManager.finalScore = finalScore;

        // Cargar la escena de puntaje
        SceneManager.LoadScene("ResultadosT");
    }
}
