using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class ButtonBlink : MonoBehaviour
{
    public Button buttonToBlink;  
    public float delayBeforeBlinking = 30.0f;  
    public float blinkInterval = 0.1f;

    private Image buttonImage;

    void Start()
    {
        buttonImage = buttonToBlink.GetComponent<Image>();
        if (buttonToBlink != null)
        {
            StartCoroutine(BlinkButton());
        }
        else
        {
            Debug.LogError("Image component not found on the button");
        }
    }

    IEnumerator BlinkButton()
    {
        // Esperar un tiempo antes de comenzar a parpadear
        yield return new WaitForSeconds(delayBeforeBlinking);

        // Loop infinito para hacer parpadear el botón
        bool isVisible = true;
        while (true)
        {
            // Cambia la visibilidad del botón
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, isVisible ? 1.0f : 0.0f);
            isVisible = !isVisible;

            // Espera un intervalo antes de cambiar la visibilidad nuevamente
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
