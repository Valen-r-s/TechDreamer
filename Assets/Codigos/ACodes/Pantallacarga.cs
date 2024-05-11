using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject menuCanvas;   
    public GameObject loadingCanvas; 
    public Slider progressSlider;    
    public TextMeshProUGUI textProgress; 

    [Header("Loading")]
    public int sceneIndexToLoad = 1;
    public float minimumLoadTime = 3.0f;

    public void StartGame()
    {
        menuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneIndexToLoad));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        float startTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        float progress = 0f;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress < 0.9f)
            {
                progress = Mathf.Lerp(progress, asyncLoad.progress, Time.deltaTime * 0.5f);
            }
            else
            {
                progress = Mathf.Lerp(progress, 1f, Time.deltaTime * 0.5f);
            }

            progressSlider.value = progress / 0.9f; 
            textProgress.text = $"{(progress / 0.9f) * 100:00}%";

            if (Time.time - startTime > minimumLoadTime && asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

    }
}
