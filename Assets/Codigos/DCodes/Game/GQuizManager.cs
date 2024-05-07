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

    public GameObject hintPanel;
    public TextMeshProUGUI HintText;

    public GameObject MedalsPanel;
    public Image Images1; 
    public Image Images2;  
    public Image Images3; 
    public Sprite bronzeMedal;
    public Sprite silverMedal;
    public Sprite goldMedal;

    private void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        MedalsPanel.SetActive(false);
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
        MedalsPanel.SetActive(true);  // Mostrar el panel de medallas
        pause.gameObject.SetActive(false);
        PistaButton.gameObject.SetActive(false);
        nextQ.gameObject.SetActive(false);
        ScoreTxt.text = totalScore + "/" + totalQuestions;

        // Lógica para asignar medallas basadas en el puntaje
        if (totalScore == 9)
        {
            Images3.sprite = goldMedal;
            Images2.sprite = silverMedal;
            Images1.sprite = bronzeMedal;
        }
        else if (totalScore >= 5)
        {
            Images2.sprite = silverMedal;
            Images1.sprite = bronzeMedal;
        }
        else if (totalScore >= 1)
        {
            Images1.sprite = bronzeMedal;
        }
    }

    public void Correct(string explanation)
    {
        totalScore += 1;
        ExplanationText.text = explanation;
        PistaButton.gameObject.SetActive(false);
        nextQ.gameObject.SetActive(true);
        DisableAllOptions();
    }

    public void Wrong(string explanation)
    {
        ExplanationText.text = explanation;
        PistaButton.gameObject.SetActive(false);
        nextQ.gameObject.SetActive(true);
        DisableAllOptions();
    }
    void DisableAllOptions()
    {
        foreach (GameObject option in options)
        {
            option.GetComponent<Button>().interactable = false;
        }
    }

    public void OnNextQuestionButtonPressed()
    {
        PistaButton.gameObject.SetActive(true);
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
            // Re-enable the button component on each option
            options[i].GetComponent<Button>().interactable = true;
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
    public void ShowHint()
    {
        if (hintPanel.activeInHierarchy)
        {
            hintPanel.SetActive(false);
        }
        else
        {
            HintText.text = QnA[currentQuestion].Hint;
            hintPanel.SetActive(true);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hintPanel.activeInHierarchy)
            {
                RectTransform rect = hintPanel.GetComponent<RectTransform>();
                if (!RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, null))
                {
                    hintPanel.SetActive(false);
                }
            }
        }
    }
}
