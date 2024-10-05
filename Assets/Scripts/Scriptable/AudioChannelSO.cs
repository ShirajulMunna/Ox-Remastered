using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio Channel")]

public class AudioChannelSO : ScriptableObject
{
    public UnityAction audioEventSO;

    public void RaiseEvent() 
    {
        audioEventSO?.Invoke();
    
    }
}
