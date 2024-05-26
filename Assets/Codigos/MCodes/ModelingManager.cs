using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModelingManager : MonoBehaviour
{
    public Button finishButton;
    public VertexComparisonScoreCalculatorM scoreCalculator;

    void Start()
    {
        finishButton.onClick.AddListener(OnFinishButtonClicked);
    }

    void OnFinishButtonClicked()
    {
        // Calcular la puntuaci�n final
        float finalScore = scoreCalculator.CalculateScore();

        // Guardar la puntuaci�n en una clase est�tica
        ScoreManager.finalScore = finalScore;

        // Cargar la escena de puntaje
        SceneManager.LoadScene("FinalM");
    }
}
