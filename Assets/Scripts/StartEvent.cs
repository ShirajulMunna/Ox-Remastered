using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEvent : MonoBehaviour
{
    [SerializeField] private EventChannelSO onStart,onTimeStart;
    [SerializeField] private Button start;
    void Start()
    {
        start.onClick.AddListener(StartGame);
        start.onClick.AddListener(TimeStart);

    }

   
    void StartGame()
    {
        onStart.RaiseEvent();
        Manager.Instance.isGameOn = true;
      
    }

    void TimeStart() 
    {
        onTimeStart.RaiseEvent();
    }
}
