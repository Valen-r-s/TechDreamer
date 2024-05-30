using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Referencia al componente VideoPlayer
    public string nextSceneName;     // Nombre de la siguiente escena

    void Start()
    {
        // Asegúrate de que el VideoPlayer esté asignado
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Suscribir al evento loopPointReached
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambiar a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
