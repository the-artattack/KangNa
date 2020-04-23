using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Template for event pop-up to make decision for solution*/
[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{
    public bool isActive;

    public string topic;
    public string description;
    public string choiceA;
    public string choiceB;

    public void Print()
    {
        Debug.Log("Card topic: "+ topic);
    }
}
