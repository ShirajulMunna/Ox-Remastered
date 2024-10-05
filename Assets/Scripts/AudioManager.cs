using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioChannelSO audioChannelSO;
    public AudioClip[] audioClips;
    private AudioSource audioSource,audioSource_2;
    public Button[] allButtons;


    private void Start()
    {
      Instance = this;
      audioSource=GetComponent<AudioSource>();
      audioSource_2 = GetComponent<AudioSource>();

      for (int i = 0; i < allButtons.Length; i++) 
      {
        allButtons[i].onClick.AddListener(TriggerButtonSound);
        
      }
    }
    private void OnEnable()
    {
        audioChannelSO.audioEventSO += PlayMusic;
    }

    private void OnDisable()
    {
        audioChannelSO.audioEventSO -= PlayMusic;   
    }

    public void PlayMusic() 
    {        
       audioSource.PlayOneShot(audioClips[0]);                 
    
    }

    public void PlayLastPageSound() 
    {
        audioSource.Stop();
        audioSource_2.PlayOneShot(audioClips[1]);
    
    }

    public void TriggerButtonSound() 
    {
        audioChannelSO.RaiseEvent();
    
    }
}
