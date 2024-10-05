using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public Toggle[] togles;
    public TMP_InputField[] inputFields;
    public InputDataSO[] inputData;
    public EventChannelSO loadImage,questionUpdate;
    [SerializeField] private Image questionSlot;
    private int currentImageIndex = 0;
    public Button next;
    public int questionCounter,questionThreshHold;
    public TextMeshProUGUI questionCounterTxt;
    public Button questionBack;

    void Start()
    {
        questionThreshHold = PlayerPrefs.GetInt("QuestionSet", 5);
        questionCounter = 1;
        questionCounterTxt.text=questionCounter.ToString();

        for (int i = 0; i < togles.Length; i++) 
        {
            togles[i].isOn = false;
        }
       
        for (int i =0 ; i < inputFields.Length; i++)
        {
            int index = i;  
            inputFields[i].onValueChanged.AddListener(delegate { OnInputFieldValueChanged(index); });
        }

        for (int i = 0; i < togles.Length; i++)
        {
            int index = i;
            togles[i].onValueChanged.AddListener(delegate { OnTogleValueChanged(index); });
        }

        next.onClick.AddListener(LoadNextImage);
        questionBack.onClick.AddListener(() => {

            next.GetComponent<Button>().interactable = true;


        });
        inputFields[0].text = inputData[0].answers[0];
      
    }

    private void OnEnable()
    {
       
        loadImage.OnEventRaise += QuestionImageLoad;
        questionUpdate.OnEventRaise += UpdateQuestion;

    }

    private void OnDisable()
    {
        
        loadImage.OnEventRaise -= QuestionImageLoad;
        questionUpdate.OnEventRaise += UpdateQuestion;


    }

    public void UpdateQuestion() 
    {
        questionCounter = 1;
        questionSlot.sprite = Manager.Instance.imageList[0];
        questionCounterTxt.text = questionCounter.ToString();

        
        questionThreshHold = Manager.Instance.questionThresh;

        for (int i = 0; i < inputData.Length; i++) 
        {
            if (inputData[i].answers[0] == null) 
            {
                inputFields[0].text = "";
            
            }
            else
            {
                inputFields[0].text = inputData[0].answers[0];


            }

        }

        if (questionCounter < questionThreshHold)
        {
            next.GetComponent<Button>().interactable = true;

        }

           

    }

   

    public void QuestionImageLoad() 
    {
        Debug.Log("Image loaded");
        LoadImage(currentImageIndex);

    }

    private void LoadImage(int index)
    {                         
       questionCounter++;
       questionCounterTxt.text = questionCounter.ToString();
       questionSlot.sprite = Manager.Instance.imageList[index];
                   
    }

    public void LoadNextImage()
    {
        if (questionCounter < Manager.Instance.questionThresh) 
        {
            currentImageIndex++;

            if (currentImageIndex >= Manager.Instance.imageList.Count)
            {
                currentImageIndex = 0;
            }

            LoadImage(currentImageIndex);

            inputFields[0].text = inputData[questionCounter-1].answers[0];

            for (int i = 0; i < togles.Length; i++)
            {
                togles[i].isOn = false;

            }

        }
        else
        {
            next.GetComponent<Button>().interactable = false;

        }


    }

   
    void OnInputFieldValueChanged(int inputFieldIndex)
    {
      
        string currentInput = inputFields[inputFieldIndex].text;
        inputData[questionCounter-1].answers[inputFieldIndex] = currentInput;


    }

   

    void OnTogleValueChanged(int index) 
    {
        
        if (togles[index].isOn)
        {
            if (index == 1)
            {
                togles[0].isOn = false;

            }
            else 
            {
                togles[1].isOn = false;
            
            }
            inputData[currentImageIndex].rightAnswer = index;

        }
        else 
        {
            
            inputData[currentImageIndex].rightAnswer = index;

        }
    }



}
