using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionDisplay : MonoBehaviour
{
    public Text desciptionText;
    public Button choiceA;
    public Button choiceB;
    public GameObject blackTransparency;

    public GameObject questionWindow;
    public Instruction instructionDisplay;

    private Question activeQuestion;
    private DiseaseQuestion diseaseQuestion;
    private InsectQuestion insectQuestion;

    private void Start()
    {
        choiceA.onClick.AddListener(SelectChoiceA);
        choiceB.onClick.AddListener(SelectChoiceB);
    }
    /** Trigger question when event occur */
    public void OpenQuestionWindow(Question question)
    {
        activeQuestion = question;
        questionWindow.SetActive(true);
        blackTransparency.SetActive(true);
        instructionDisplay.disable();
        desciptionText.text = question.description;
        choiceA.GetComponentInChildren<Text>().text = question.choiceA;
        choiceB.GetComponentInChildren<Text>().text = question.choiceB;
        question.Print();
    }

    public void CloseQuestionWindow()
    {
        questionWindow.SetActive(false);
        blackTransparency.SetActive(false);
    }

    public void printSelectedChoice()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        string selectedChoice;
        if (buttonValue == "btnA")
        {
            selectedChoice = activeQuestion.choiceA;            
        }
        else
        {
            selectedChoice = activeQuestion.choiceB;
        }
        Debug.Log(activeQuestion.topic + ": " + selectedChoice);

    }

    public void SelectChoiceA()
    {
        printSelectedChoice();
        if (activeQuestion.topic == "SeaRise") //ปิดทางน้ำเข้านา
        {
            //Do something
        }      
        else if(activeQuestion.topic == "Flood") //ต้องการระบายน้ำ
        {
            //Do something
        }
        else if (activeQuestion.topic == "Drought") //ใช้น้ำคลอง
        {
            //Do something
        }
        else //For insect and disease case
        {
            if (activeQuestion.topic.StartsWith("Disease"))
            {
                diseaseQuestion.solutionA(activeQuestion);
            }
            else if(activeQuestion.topic.StartsWith("Insect"))
            {
                insectQuestion.solutionA(activeQuestion);
            }
        }
    }

    public void SelectChoiceB()
    {
        printSelectedChoice();
        if (activeQuestion.topic == "SeaRise") //ไม่ปิดทางน้ำเข้านา
        {
            //Do something
        }
        else if (activeQuestion.topic == "Flood") //ไม่ต้องการระบายน้ำ
        {
            //Do something
        }
        else if (activeQuestion.topic == "Drought") //ใช้น้ำฝนต่อ
        {
            //Do something
        }
        else  //For insect and disease case
        {
            if (activeQuestion.topic.StartsWith("Disease"))
            {
                diseaseQuestion.solutionB(activeQuestion);
            }
            else if (activeQuestion.topic.StartsWith("Insect"))
            {
                insectQuestion.solutionB(activeQuestion);
            }
        }
    }
}
