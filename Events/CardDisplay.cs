using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text desciptionText;
    public Text choiceAText;
    public Text choiceBText;

    // Start is called before the first frame update
    void Start()
    {
        desciptionText.text = card.description;
        choiceAText.text = card.choiceA;
        choiceBText.text = card.choiceB;
        card.Print();
    }
        
}
