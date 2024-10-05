using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SliderValueHandler : MonoBehaviour
{
    public UnityEngine.UI.Slider[] sliders;
    public int question_1, question_2, question_3;
    public int time_1,time_2,time_3;
    public EventChannelSO questionUpdate;
    
    
    void Start()
    {
    

        for (int i = 0; i < sliders.Length; i++) 
        {
            int index = i;
            sliders[i].onValueChanged.AddListener(delegate { OnSliderValueChanged(index); });


        }

    }

    public void OnSliderValueChanged(int sliderindex) 
    {
        Debug.Log(sliderindex);

        switch (sliderindex) 
        {
            case 0:
                Debug.Log("Set question here");

                SetQuestion(sliderindex);
                break;

            case 1:
                Debug.Log("Set Time here");
                SetTime(sliderindex);
                break;
            case 2:
                Debug.Log("Set particle here");
                SetParticles(sliderindex);
                break;
            

        }


    }

    public void SetQuestion(int index) 
    {
        if (sliders[index].value == 1)
        {
            Manager.Instance.SetQuestionThreshHold(question_1);
            PlayerPrefs.SetInt("QuestionSet", question_1);
            questionUpdate.RaiseEvent();


        }
        else if (sliders[index].value == 2)
        {
            Manager.Instance.SetQuestionThreshHold(question_2);
            PlayerPrefs.SetInt("QuestionSet", question_2);
            questionUpdate.RaiseEvent();



        }
        else 
        {
            Manager.Instance.SetQuestionThreshHold(question_3);
            PlayerPrefs.SetInt("QuestionSet", question_3);
            questionUpdate.RaiseEvent();


        }

    }

    public void SetTime(int index) 
    {
        if (sliders[index].value == 1)
        {
            TimeManager.Instacne.SetTime(time_1);
            PlayerPrefs.SetInt("Time", time_1);


        }
        else if (sliders[index].value == 2)
        {
            TimeManager.Instacne.SetTime(time_2);
            PlayerPrefs.SetInt("Time", time_2);


        }
        else 
        {
            TimeManager.Instacne.SetTime(time_3);
            PlayerPrefs.SetInt("Time", time_3);


        }

    }

    public void SetParticles(int index) 
    {
        if (sliders[index].value == 1)
        {
            Manager.Instance.SetparticleValue(0);
            PlayerPrefs.SetInt("Particle", 0);
        }
        else if (sliders[index].value == 3)
        {
            Manager.Instance.SetparticleValue(1);
            PlayerPrefs.SetInt("Particle", 1);

        }
        

    
    }
  
}
