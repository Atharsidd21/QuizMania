using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
     [SerializeField] float timeToCorrectAnswer= 10f;

     public bool loadNextQuestion;
    public bool isAnsweringQuestion; 
    public float fillFraction;
     float timerValue;

   
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTImer()
    {
        timerValue = 0;
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if(isAnsweringQuestion)
        {

            if(timerValue > 0) 
            {
              fillFraction = timerValue/timeToCompleteQuestion;
            }
          else
           {
             isAnsweringQuestion = false;
             timerValue = timeToCorrectAnswer;
           }
        }
           else
           {
            if(timerValue > 0)
            {
              fillFraction = timerValue/timeToCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
           }
    
        
        if(timerValue <= 0)
        {
            timerValue = timeToCompleteQuestion;
        }
        //Debug.Log(isAnsweringQuestion + ": " + timerValue + ": " + fillFraction);
    }
    
}
