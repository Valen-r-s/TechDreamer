using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Referencia al panel de Game Over en la UI

    void Start()
    {
        // Aseg�rate de que el panel de Game Over est� desactivado al iniciar el juego
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        else
            Debug.LogError("GameOverPanel is not assigned in the Inspector!");
    }

    // Funci�n para mostrar el panel de Game Over
    public void ShowGameOver()
    {
        Debug.Log("Showing Game Over Panel");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);  // Activa el panel de Game Over
        else
            Debug.LogError("GameOverPanel is not assigned in the Inspector!");
    }

    // Opcionalmente, puedes agregar un m�todo para reiniciar el juego
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        // Aqu� puedes agregar la l�gica para reiniciar el juego, t�picamente recargando la escena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}


