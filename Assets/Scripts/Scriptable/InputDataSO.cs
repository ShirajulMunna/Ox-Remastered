using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputData", menuName = "ScriptableObjects/InputData", order = 1)]
public class InputDataSO : ScriptableObject
{
    public string[] answers;
    public int rightAnswer;
}
