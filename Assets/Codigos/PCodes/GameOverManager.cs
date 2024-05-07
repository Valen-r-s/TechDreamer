using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Referencia al panel de Game Over en la UI

    void Start()
    {
        // Asegúrate de que el panel de Game Over está desactivado al iniciar el juego
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        else
            Debug.LogError("GameOverPanel is not assigned in the Inspector!");
    }

    // Función para mostrar el panel de Game Over
    public void ShowGameOver()
    {
        Debug.Log("Showing Game Over Panel");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);  // Activa el panel de Game Over
        else
            Debug.LogError("GameOverPanel is not assigned in the Inspector!");
    }

    // Opcionalmente, puedes agregar un método para reiniciar el juego
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        // Aquí puedes agregar la lógica para reiniciar el juego, típicamente recargando la escena
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}


