using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GQuizManager : MonoBehaviour
{
    public List<GQandA> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ExplanationText;
    public TextMeshProUGUI ScoreTxt;

    int totalQuestions = 0;
    public int totalScore = 0;

    public GameObject QuizPanel;
    public GameObject GoPanel;
    public GameObject DialogoPanel;
    public Button nextQ;
    public Button pause;
    public Button PistaButton;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        nextQ.gameObject.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void GameOver()
    {
        QuizPanel.SetActive(false);
        DialogoPanel.SetActive(false);
        GoPanel.SetActive(true);
        pause.gameObject.SetActive(false);
        PistaButton.gameObject.SetActive(false);
        nextQ.gameObject.SetActive(false);
        ScoreTxt.text = totalScore + "/" + totalQuestions;
    }

    public void Correct(string explanation)
    {
        totalScore += 1;
        ExplanationText.text = explanation;
        nextQ.gameObject.SetActive(true);
    }

    public void Wrong(string explanation)
    {
        ExplanationText.text = explanation;
        nextQ.gameObject.SetActive(true);
    }

    public void OnNextQuestionButtonPressed()
    {
        nextQ.gameObject.SetActive(false);
        ExplanationText.text = "";
        QnA.RemoveAt(currentQuestion);

        if (QnA.Count > 0)
        {
            generateQuestion();
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
            options[i].GetComponent<Image>().color = options[i].GetComponent<GanswerScript>().starColor;
            options[i].GetComponent<GanswerScript>().isCorrect = false;
            options[i].GetComponent<GanswerScript>().explanation = QnA[currentQuestion].Explanations[i];
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<GanswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
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
