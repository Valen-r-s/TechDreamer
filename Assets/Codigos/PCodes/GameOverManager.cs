using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;  // Arrastra aquí el panel de Game Over desde el Inspector

    void Start()
    {
        gameOverPanel.SetActive(false);  // Oculta el panel de Game Over al inicio
    }


    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over Panel Activated");
        }
        else
        {
            Debug.LogError("Game Over Panel is not assigned in the Inspector!");
        }
    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarga la escena actual para reiniciar el juego
    }
}

