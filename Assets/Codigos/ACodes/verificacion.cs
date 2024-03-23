using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class verificacion : MonoBehaviour
{
    public List<GameObject> correctSequence; 
    public List<GameObject> userSequence; 
    public GameObject dialogPanel; 
    public TextMeshProUGUI dialogText; 
    public List<DropSlot> slots;
    public string mensajeIntentaDeNuevo = "¡Intenta nuevamente!";
    public dDialogo dialogoScript;
    public Button botonFelicidades; 


    public void UpdateUserSequence()
    {
        userSequence.Clear();
        foreach (var slot in slots)
        {
            if (slot.item != null) 
            {
                userSequence.Add(slot.item);
            }
            else
            {
                userSequence.Add(null); 
            }
        }
    }

    public void VerifySequence()
    {
        UpdateUserSequence(); 

        int wrongCount = 0;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (i >= userSequence.Count || userSequence[i] != correctSequence[i])
            {
                wrongCount++;
            }
        }

        ShowDialog(wrongCount);
    }

    void ShowDialog(int wrongCount)
    {
        if (wrongCount > 0)
        {
            dialogText.text = $"Tienes {wrongCount} imágenes en la posición incorrecta. ¡Intenta nuevamente!. {mensajeIntentaDeNuevo}";
            botonFelicidades.gameObject.SetActive(false);
        }
        else
        {
            dialogText.text = "¡Felicidades! Has organizado todas las imágenes correctamente. Ahora dale al botón play para ver tu animación.";
            botonFelicidades.gameObject.SetActive(true);
        }

        dialogPanel.SetActive(true);
        dialogoScript.closeOnEnter = true;
    }


}
