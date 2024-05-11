using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class pantallacarga : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public string sceneToLoad;

    [Header("Loading")]
    public float minimumLoadTime = 2.0f; // Tiempo mínimo en segundos que la pantalla de carga debe mostrarse

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        float startTime = Time.time;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float progress = 0f;

        while (!operation.isDone)
        {
            if (operation.progress < 0.9f)
            {
                progress = Mathf.Lerp(progress, operation.progress, Time.deltaTime * 0.5f);
            }
            else
            {
                progress = Mathf.Lerp(progress, 1f, Time.deltaTime * 0.5f);
            }

            progressBar.value = progress / 0.9f;
            progressText.text = $"{(progress / 0.9f) * 100:00}%";

            if (Time.time - startTime > minimumLoadTime && operation.progress >= 0.9f)
            {
               operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
