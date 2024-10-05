using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instacne; 
    public int countdownTime = 10;  
    public TextMeshProUGUI countdownDisplay;
    public EventChannelSO timerEvent;
    public bool isCountDownStart;
     

    private void Start()
    {
       
        
        Instacne = this;
    }
    private void OnEnable() 
    {
        timerEvent.OnEventRaise += StartTime;
    
    }

    private void OnDisable()
    {
        timerEvent.OnEventRaise -= StartTime;
    }

    public void SetTime(int time) 
    {
        this.countdownTime = time;
    
    }
    public void StartTime()
    {
       
         StartCoroutine(StartCountdown());        

    }

    
    IEnumerator StartCountdown()
    {
        int currentTime = countdownTime;

        
        while (currentTime >= 0 && isCountDownStart)
        {
            
            countdownDisplay.text = currentTime.ToString();

           
            yield return new WaitForSeconds(1f);

            
            currentTime--;
        }
            
        OnCountdownFinished();
    }

  
    void OnCountdownFinished()
    {
        Debug.Log("Countdown has finished!");
       // LoadNext();
    


    }

    public void LoadNext()
    {
        StartCoroutine(LoadNextQuestion());

    }



    IEnumerator LoadNextQuestion()
    {


        if (Manager.Instance.currentImageIndex != Manager.Instance.questionThresh - 1)
        {
            yield return new WaitForSeconds(0.5f);
            
           Manager.Instance.LoadNextImage();
           InGameAnswerHandler.Instance.questionCounter++;
           int count=InGameAnswerHandler.Instance. questionCounter;
           InGameAnswerHandler.Instance. questionCounterTxt.text = count.ToString();
           TimeManager.Instacne.isCountDownStart = true;
           TimeManager.Instacne.StartTime();

        }

        else
        {
           yield return new WaitForSeconds(0.5f);
           InGameAnswerHandler.Instance. questionFinished.RaiseEvent();

        }




    }
}
