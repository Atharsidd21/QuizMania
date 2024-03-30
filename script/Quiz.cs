using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header ("Questions")]
     [SerializeField] TextMeshProUGUI questionText;
     [SerializeField] List<questionSO> questions = new List<questionSO>();
     questionSO currentquestion;

    [Header("Answers")]
     [SerializeField] GameObject[] answerButtons;
     int correctAnswerIndex;
     bool hasAnsweredEarly;

     [Header ("Button Colors")]
     [SerializeField] Sprite defaultAnswerSprite;
     [SerializeField] Sprite correctAnswerSprite;
     [Header("Timer")]
     [SerializeField] Image timerImage;
     Timer timer;
     [Header("Scoring")]
     [SerializeField] TextMeshProUGUI scoreText;
     ScoreKeeper scoreKeeper;
     [Header("Sidebar")]
     [SerializeField] Slider progressBar;

     public bool isComplete;
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
      

    }
    void Update()
    {
      timerImage.fillAmount = timer.fillFraction;
      if(timer.loadNextQuestion)
      {
        if(progressBar.value == progressBar.maxValue)
         {
           isComplete = true;
           return;
         }

        hasAnsweredEarly = false;
        GetNextQuestion();
        timer.loadNextQuestion = false;
      }
      else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
      {
        DisplayAnswer(-1);
        SetButtonState(false);
      }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
         SetButtonState(false);
         timer.CancelTImer();
         scoreText.text = "Score:" + scoreKeeper.CalculateScore() + "%";

         
    }
    void DisplayAnswer(int index)
    {
        
        Image buttonImage;
        if(index == currentquestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
           correctAnswerIndex = currentquestion.GetCorrectAnswerIndex();
           string correctAnswer = currentquestion.GetAnswer(correctAnswerIndex);
           questionText.text = "Wrong, the correct answer is ;\n " + correctAnswer;
           buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
           buttonImage.sprite = correctAnswerSprite;
        }
    }

        void GetNextQuestion()
        {
             if(questions.Count > 0)
             {
             SetButtonState(true);
             SetDefaultButtonSprites();
             GetRandomQuestion();
             DisplayQuestion();
             progressBar.value++;
             scoreKeeper.IncrementQuestionsSeen();

             }
        }

        void GetRandomQuestion()
        {
          int index = Random.Range(0 , questions.Count);
          currentquestion = questions[index];

          if(questions.Contains(currentquestion))
          {
          questions.Remove(currentquestion);

          }
        }
    void DisplayQuestion()
    {
         questionText.text = currentquestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++)
        {
           TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren< TextMeshProUGUI>();
           buttonText.text = currentquestion.GetAnswer(i);
        }
        
       

    }
    
    void SetButtonState(bool state)
    {
      for(int i = 0; i < answerButtons.Length; i++)
      {
        Button button = answerButtons[i].GetComponent<Button>();
        button.interactable = state;
      }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i <answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    
}

