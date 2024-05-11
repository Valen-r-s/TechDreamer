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

        while (asyncLoad.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressSlider.value = progress;
            textProgress.text = $"{progress * 100:00}%";
            yield return null;
        }

        float loadTime = Time.time - startTime;
        if (loadTime < minimumLoadTime)
        {
            yield return new WaitForSeconds(minimumLoadTime - loadTime);
        }

        progressSlider.value = 1f;
        textProgress.text = "100%";

        asyncLoad.allowSceneActivation = true;
    }
}
