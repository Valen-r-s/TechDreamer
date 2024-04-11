using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorNotas : MonoBehaviour
{
    public GameObject notaVisual;
    public GameObject ObjetoVisual;
    public GameObject CamaraVisual;
    public GameObject ObjEnEscena;

    public bool activa;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activa == true)
        {
            notaVisual.SetActive(true);
            ObjetoVisual.SetActive(true);
            CamaraVisual.SetActive(true);
            ObjEnEscena.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && activa == true)
        {
            notaVisual.SetActive(false);
            ObjetoVisual.SetActive(false);
            CamaraVisual.SetActive(false);
            ObjEnEscena.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activa = true;          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activa = false;
            notaVisual.SetActive(false);
            ObjetoVisual.SetActive(false);
            CamaraVisual.SetActive(false);
            ObjEnEscena.SetActive(true);
        }
    }

}