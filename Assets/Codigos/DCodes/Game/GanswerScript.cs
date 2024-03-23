using UnityEngine;
using UnityEngine.UI;

public class GanswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public GQuizManager quizManager;
    public Color starColor;
    public string explanation;

    private void Start()
    {
        starColor = GetComponent<Image>().color;
    }
    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            Debug.Log("Respuesta Correcta");
            quizManager.Correct(explanation);
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Respuesta Incorrecta");
            quizManager.Wrong(explanation);
        }
    }
}
