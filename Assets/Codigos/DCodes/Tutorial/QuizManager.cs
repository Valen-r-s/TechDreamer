using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<DisenoQandA> QnA; 
    public GameObject[] options; 
    public int currentQuestion;
    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ExplanationText;
    public GameObject QuizPanel;
    public GameObject ExplanationPanel;
    public GameObject StartButton;
    public GameObject ReTakeButton;
    public Button NextQuestionButton;
    public Button PistaButton;

    private void Start()
    {
        StartButton.SetActive(false);
        ReTakeButton.SetActive(false);
        NextQuestionButton.gameObject.SetActive(false);
        GenerateQuestion();
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        StartButton.SetActive(true);
        ReTakeButton.SetActive(true);
        ExplanationPanel.SetActive(true);
        NextQuestionButton.gameObject.SetActive(false);
        PistaButton.gameObject.SetActive(false); 
        ExplanationText.text = "¡Perfecto! Has finalizado el quiz, ahora puedes volver a tomarlo o empezar el juego.";
    }

    public void Correct(string explanation)
    {
        ExplanationText.text = explanation;
        NextQuestionButton.gameObject.SetActive(true); 
    }

    public void Wrong(string explanation)
    {
        ExplanationText.text = explanation;
        NextQuestionButton.gameObject.SetActive(true); 
    }

    public void OnNextQuestionButtonPressed()
    {
        NextQuestionButton.gameObject.SetActive(false);
        ExplanationText.text = "";
        QnA.RemoveAt(currentQuestion);

        if (QnA.Count > 0)
        {
            GenerateQuestion();
        }
        else
        {
            GameOver();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<dAnswerScript>().starColor;
            options[i].GetComponent<dAnswerScript>().isCorrect = false;
            options[i].GetComponent<dAnswerScript>().explanation = QnA[currentQuestion].Explanations[i];
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<dAnswerScript>().isCorrect = true;
            }
        }
    }

    void GenerateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            GameOver();
        }
    }
}
