using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminHandler : MonoBehaviour
{
    public static AdminHandler Instance;
    public Slider[] adminSliders;
    private int  time,questionSet,particles;
    void Start()
    {
        Instance = this;

       
        
    }

    private void OnEnable()
    {
        time = PlayerPrefs.GetInt("Time", 5);
        questionSet = PlayerPrefs.GetInt("QuestionSet", 5);
        particles = PlayerPrefs.GetInt("Particle", 0);

        UpdateTimeSlider();
        UpdateQuestionSlider();
        UpdateParticleSlider();

    }

    public void UpdateTimeSlider() 
    {
        if (time == 5)
        {
            adminSliders[1].value = 1;

        }
        else if (time == 7)
        {
            adminSliders[1].value = 2;

        }
        else
        {
            adminSliders[1].value = 3;

        }

    }

    public void UpdateQuestionSlider() 
    {
        if (questionSet == 5)
        {
            adminSliders[0].value = 1;

        }
        else if (questionSet == 7)
        {
            adminSliders[0].value = 2;

        }
        else
        {
            adminSliders[0].value = 3;

        }

    }

    public void UpdateParticleSlider() 
    {
        if (particles == 0)
        {
            adminSliders[2].value = 1;

        }
        else if (particles == 1)
        {
            adminSliders[2].value = 3;

        }

    }



    
   
}
