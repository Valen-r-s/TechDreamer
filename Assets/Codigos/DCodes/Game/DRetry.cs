using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DRetry : MonoBehaviour
{
    public bool Pasaresc;
    public int indiceesc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pasaresc)
        {
            Cambiaresc(indiceesc);
        }
    }

    public void Cambiaresc(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
