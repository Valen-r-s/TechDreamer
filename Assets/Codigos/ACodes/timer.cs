using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class timer : MonoBehaviour
{
    public float Timer = 60; 
    public TextMeshProUGUI textotimer;
    public int indiceEscenaPuntuacion; 

    void Update()
    {
        Timer -= Time.deltaTime;
        int minutos = Mathf.FloorToInt(Timer / 60);
        int segundos = Mathf.FloorToInt(Timer % 60);
        textotimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void CompletarSecuenciaYCambiarEscena()
    {
        
        if (Timer > 0)
        {
            float puntaje = Timer * 10; 
            PlayerPrefs.SetFloat("Puntaje", puntaje);
            PlayerPrefs.Save();

            SceneManager.LoadScene(indiceEscenaPuntuacion);
        }
    }
}