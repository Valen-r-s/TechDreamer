using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiarescprin : MonoBehaviour
{
    public int numEsc;
    public GameObject texto;

    private bool lugar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && lugar == true)
        {
            SceneManager.LoadScene(numEsc);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            texto.SetActive(true);
            lugar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            texto.SetActive(false);
            lugar = false;
        }
    }
}
