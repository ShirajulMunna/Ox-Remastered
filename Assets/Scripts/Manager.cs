using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
   
    public EventChannelSO onLoadImage;
    public static Manager Instance;
    public string imageFolderPath = "Images";
    public string itemFolderPath = "Gift Items";
    public int currentImageIndex = 0;
    public bool isGameOn;
    public bool isQuestionFinish;
    public int questionThresh;
    public int paricleValue;
    public Image giftItem;
    public Image GiftBox;
    public GameObject gameStat;
    public List<Sprite> imageList;
    public List<Sprite> itemList;

    [SerializeField] private GameObject adminPanel;
    [SerializeField] private Image imageSlot;
    [SerializeField] private Button next;

    public Button[] allGiftButtons;
    public Button restart,openGiftBoxButton;

    public GameObject[] particles;
    public Sprite[] perfect;
    public Image perfectImage;
    public TextMeshProUGUI questionNumber,questionAnswered;
    void Start()
    {
        questionThresh = PlayerPrefs.GetInt("QuestionSet", 5);
        paricleValue = PlayerPrefs.GetInt("Particle", 0);
        Instance = this;
        LoadImagesFromProject();
        if (imageList.Count > 0)
        {
            LoadImage(currentImageIndex);
        }
        else
        {
            Debug.LogWarning("No images found in the specified folder.");
        }

        for (int i = 0; i < allGiftButtons.Length; i++) 
        {
            allGiftButtons[i].onClick.AddListener(OpenGiftBox);
        
        }

        next.onClick.AddListener(LoadNextImage);
        restart.onClick.AddListener(RestartGame);
       


    }

    public void SetQuestionThreshHold(int threshHold) 
    {
        this.questionThresh = threshHold;
    
    }

    public void SetparticleValue(int value) 
    {
        this.paricleValue= value;

    
    }

    private void Update()
    {
        if (!isGameOn) 
        {

            if (Input.GetKeyDown(KeyCode.F12))
            {
                adminPanel.SetActive(true);               

            }

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Application.Quit();

        }

    }
       
    
    private void LoadImagesFromProject()
    {
        
        Sprite[] loadedImages = Resources.LoadAll<Sprite>(imageFolderPath);
        Sprite[] loadedItems = Resources.LoadAll<Sprite>(itemFolderPath);


        
        if (loadedImages.Length > 0)
        {
            imageList = new List<Sprite>(loadedImages);
            itemList = new List<Sprite>(loadedItems);
            Debug.Log("Loaded " + imageList.Count + " images from the folder.");
            onLoadImage.RaiseEvent();


        }
        else
        {
            Debug.LogWarning("No images found in the specified folder: " + imageFolderPath);
        }
    }

    private void LoadImage(int index)
    {
        if (index < questionThresh)
        {
            imageSlot.sprite = imageList[index];

        }
        else 
        {
            Debug.Log("error");
        
        }
    
    }

   
    public void LoadNextImage()
    {
        if (currentImageIndex != imageList.Count - 1)
        {
            currentImageIndex++;

            InGameAnswerHandler.Instance.currentQuestionIndex = currentImageIndex;
            if (currentImageIndex >= imageList.Count)
            {
                currentImageIndex = 0;
            }


            LoadImage(currentImageIndex);

         //   InGameAnswerHandler.Instance.AnswerHandle();

        }
       
       
    }

    public void OpenStatBox() 
    {
       
        if (questionThresh == InGameAnswerHandler.Instance.score)
        {
            perfectImage.GetComponent<Image>().sprite = perfect[0];
        }
        else 
        {
            perfectImage.GetComponent<Image>().sprite = perfect[1];

        }

        questionNumber.text = questionThresh.ToString();
        questionAnswered.text = InGameAnswerHandler.Instance.score.ToString();

    }
    public void OpenGiftBox()
    {
        AudioManager.Instance.PlayLastPageSound();
        GiftBox.gameObject.SetActive(true);
        giftItem.GetComponent<Image>().sprite = itemList[UnityEngine.Random.Range(0,itemList.Count)];
    
    }

    public void ParticleActivate() 
    {
        particles[paricleValue].SetActive(true);

    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(0);
    
    }
}
 