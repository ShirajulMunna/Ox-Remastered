using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameAnswerHandler : MonoBehaviour
{
    public static InGameAnswerHandler Instance;
    public TextMeshProUGUI[] text;
    public InputDataSO[] inputData;
    public Button[] answerButtons;
    [SerializeField] public EventChannelSO eventChannleSO;
    public int currentQuestionIndex;
    public Image[] resultBanner;
    public Image blackBg,clock,yes,no;
    public EventChannelSO questionFinished;
    public GameObject mainResultPanel,giftPanel,sorryPanel,mainGamePage;
    public int score;
    public AudioChannelSO audioChannelSO;
    public bool isButtonPressed;
    public int questionCounter;
    public TextMeshProUGUI questionCounterTxt;
    public Image answerBanner,questionBanner;
    public TextMeshProUGUI answerTxt;


    private void Start()
    {
        Instance = this;
        currentQuestionIndex = Manager.Instance.currentImageIndex;
        questionCounter = 1;
        questionCounterTxt.text = questionCounter.ToString();


        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(delegate { CheckAnswer(index); });
        }
    }

    private void OnEnable() 
    {
       
       // questionFinished.OnEventRaise += TriggerMainResultPanel;
    }

    private void OnDisable()
    {
       
       // questionFinished.OnEventRaise -= TriggerMainResultPanel;

    }

    public void CheckAnswer(int index) 
    {
        isButtonPressed = true;
        TimeManager.Instacne.isCountDownStart = false;
        int winningNumber = inputData[currentQuestionIndex].rightAnswer;


        if (winningNumber == index)
        {
            Debug.Log("win" + index);

            WinResultBanner(0);
            score++;

        }
        else 
        {      
            ResultBanner(1);
          
        }

    }

    public void WinResultBanner(int index) 
    {
        blackBg.gameObject.SetActive(true);


        resultBanner[index].GetComponent<RectTransform>().DOLocalMoveX(10.00f, 0.1f).SetEase(Ease.InElastic).OnComplete(() => 
        {
            StartCoroutine(AnswerWinPanel(index));



        });
        

        clock.gameObject.SetActive(false);
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);

    }

    public IEnumerator AnswerWinPanel(int index) 
    {
        yield return new WaitForSeconds(1);

        
        blackBg.gameObject.SetActive(false);

        resultBanner[index].GetComponent<RectTransform>().DOLocalMoveX(-916f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            // StartCoroutine(AnswerWinPanel(index));
            LoadWinNext(index);




        });



        clock.gameObject.SetActive(true);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);




    }

    public void ResultBanner(int index) 
    {
        clock.gameObject.SetActive(false);
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);

        blackBg.gameObject.SetActive(true);
      
        resultBanner[index].GetComponent<RectTransform>().DOLocalMoveX(10f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {
            
            StartCoroutine(AnswerPanel(index));



        });
        questionBanner.gameObject.SetActive(false);
       
        answerBanner.gameObject.SetActive(true);
        answerTxt.text = inputData[currentQuestionIndex].answers[0];


       
    }

    public IEnumerator AnswerPanel(int index) 
    {
        yield return new WaitForSeconds(1);

        clock.gameObject.SetActive(true);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);

        blackBg.gameObject.SetActive(false);
        
        resultBanner[index].GetComponent<RectTransform>().DOLocalMoveX(-916f, 0.1f).SetEase(Ease.InElastic).OnComplete(() =>
        {


            LoadNext(index);



        });
        answerBanner.gameObject.SetActive(false);
        questionBanner.gameObject.SetActive(true);


    }

    public void LoadNext(int index) 
    {
        StartCoroutine(LoadNextQuestion(index));

    }

    IEnumerator LoadNextQuestion(int index) 
    {
       
        if (Manager.Instance.currentImageIndex != Manager.Instance.questionThresh-1 && isButtonPressed)
        {
            yield return new WaitForSeconds(0.1f);
         
            Manager.Instance.LoadNextImage();
            questionCounter++;
            questionCounterTxt.text = questionCounter.ToString();
            TimeManager.Instacne.isCountDownStart = true;
            TimeManager.Instacne.StartTime();
            isButtonPressed = false;

        }

        else 
        {
            
            TriggerMainResultPanel();


        }
          
    }

    public void LoadWinNext(int index)
    {
       
        StartCoroutine(LoadWinNextQuestion(index));

    }

    IEnumerator LoadWinNextQuestion(int index)
    {

        if (Manager.Instance.currentImageIndex != Manager.Instance.questionThresh - 1 && isButtonPressed)
        {
            yield return new WaitForSeconds(0.1f);

            Manager.Instance.LoadNextImage();
            questionCounter++;
            questionCounterTxt.text = questionCounter.ToString();
            TimeManager.Instacne.isCountDownStart = true;
            TimeManager.Instacne.StartTime();
            isButtonPressed = false;

        }

        else
        {
           
            TriggerMainResultPanel();

        }


    }

    public void TriggerMainResultPanel() 
    {
        Debug.Log("Open Gift/ Fail Panel");
        mainGamePage.SetActive(false);
        mainResultPanel.SetActive(true);
       // Manager.Instance.gameStat.SetActive(true);
        //Manager.Instance.OpenStatBox();
        Manager.Instance.ParticleActivate();
              
    
    }

    
}
