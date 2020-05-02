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
    public InstructionDisplay instructionDisplay;

    private Question activeQuestion;
    private DiseaseSolution diseaseSolution;
    private InsectSolution insectSolution;

    public delegate void onTimeControl();
    public static event onTimeControl OnTimeControl;

    private SimulateParameters parameters;
    private void Start()
    {
        choiceA.onClick.AddListener(SelectChoiceA);
        choiceB.onClick.AddListener(SelectChoiceB);
    }
    /** Trigger question when event occur */
    public void OpenQuestionWindow(Question question, SimulateParameters parameters)
    {
        activeQuestion = question;
        questionWindow.SetActive(true);
        blackTransparency.SetActive(true);
        instructionDisplay.disable();
        question.isActive = true;
        desciptionText.text = question.description;
        this.parameters = parameters;
        choiceA.GetComponentInChildren<Text>().text = question.choiceA;
        choiceB.GetComponentInChildren<Text>().text = question.choiceB;
        question.Print();
    }  

    private void CloseQuestionWindow()
    {
        questionWindow.SetActive(false);
        blackTransparency.SetActive(false);
    }

    private void printSelectedChoice()
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
        OnTimeControl?.Invoke(); //Resume 

        if (activeQuestion.topic == "SeaRise") //ปิดทางน้ำเข้านา +1
        {
            //Do something
            parameters.UseCanal = false;
            Evaluation.increaseScore(1);
        }      
        else if(activeQuestion.topic == "Flood") //ต้องการระบายน้ำ +1
        {
            //Do something
            parameters.UseCanal = true;
            Evaluation.increaseScore(1);
        }
        else if (activeQuestion.topic == "Drought") //ใช้น้ำคลอง +1
        {
            //Do something
            if (parameters.UseReservoir != true)
            {
                parameters.UseCanal = true;
                Evaluation.increaseScore(1);
            }            
        }
        else //For insect and disease case
        {
            if (activeQuestion.topic.StartsWith("Disease"))
            {
                diseaseSolution.solutionA(activeQuestion, parameters);
            }
            else if(activeQuestion.topic.StartsWith("Insect"))
            {
                insectSolution.solutionA(activeQuestion, parameters);
            }
        }
        CloseQuestionWindow();
    }

    public void SelectChoiceB()
    {
        printSelectedChoice();
        OnTimeControl?.Invoke(); //Resume 

        if (activeQuestion.topic == "SeaRise") //ไม่ปิดทางน้ำเข้านา +0
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
        }
        else if (activeQuestion.topic == "Flood") //ไม่ต้องการระบายน้ำ +0
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);
        }
        else if (activeQuestion.topic == "Drought") //ใช้น้ำฝนต่อ +0
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        }
        else  //For insect and disease case
        {
            if (activeQuestion.topic.StartsWith("Disease"))
            {
                diseaseSolution.solutionB(activeQuestion, parameters);
            }
            else if (activeQuestion.topic.StartsWith("Insect"))
            {
                insectSolution.solutionB(activeQuestion, parameters);
            }
        }
        CloseQuestionWindow();
    }
}
