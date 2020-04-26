using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** Template for select methods */
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string topic;
    public string description;
    public string choiceA;
    public Sprite aImage;
    public Sprite bImage;
    public string choiceB;

    public void Print()
    {
        Debug.Log("Card topic: "+ topic);
    }
}
