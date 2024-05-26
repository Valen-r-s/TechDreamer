using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModelingManagerTuto : MonoBehaviour
{
    public Button finishButton;
    public VertexComparisonScoreCalculatorT scoreCalculator;

    void Start()
    {
        finishButton.onClick.AddListener(OnFinishButtonClicked);
    }

    void OnFinishButtonClicked()
    {
        // Calcular la puntuación final
        float finalScore = scoreCalculator.CalculateScore();

        // Guardar la puntuación en una clase estática
        ScoreManager.finalScore = finalScore;

        // Cargar la escena de puntaje
        SceneManager.LoadScene("ResultadosT");
    }
}
