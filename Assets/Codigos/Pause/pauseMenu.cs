using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject pausePanel; 

    private void Start()
    {
        pausePanel.SetActive(false); 
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}
