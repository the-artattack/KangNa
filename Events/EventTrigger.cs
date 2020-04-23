using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/** This class is main class for handle question/animation on main game */
public class EventTrigger : MonoBehaviour
{
    public QuestionDisplay questionDisplay;
    public Instruction instructionDisplay;
    private string header;
    private GameObject instructionObject;

    /** List of disease and insect instruction*/
    public List<GameObject> diseases;
    public List<GameObject> insects;
    public GameObject flood;
    public GameObject raining;

    /** question in each events*/
    public Question insectQuestion;
    public Question diseaseQuestion;
    public Question upCommingRainQuestion;
    public Question rainingQuestion;    
    public Question floodQuestion;
    public Question notRainQuestion;
    public Question seaRiseQuestion;

    public new Animation animation;

    public delegate void onTimeControl();
    public static event onTimeControl OnTimeControl;

    public void Start()
    {
        MainGame.onInsectTrigger += InsectTrigger;
        MainGame.onDiseaseTrigger += DiseaseTrigger;
        MainGame.onRainForecastTrigger += UpCommingRainTrigger;
        MainGame.onRainTrigger += RainingTrigger;
        MainGame.onFloodTrigger += FloodingTrigger;
        MainGame.onNotRainTrigger += NotRainTrigger;
        MainGame.onSeaRiseTrigger += SeaRiseTrigger;        
    }    

    private void InsectTrigger(SimulateParameters parameters)
    {
        insectQuestion.isActive = true;
        GameObject temp = insects.Where(obj => obj.name == "เพลี้ยไฟ").SingleOrDefault();
        instructionDisplay.createInstruction("เพลี้ยไฟ", temp);        
        animation.InsectEnable(parameters);
    }

    private void DiseaseTrigger(SimulateParameters parameters)
    {
        GameObject temp = diseases.Where(obj => obj.name == "โรคไหม้").SingleOrDefault();
        diseaseQuestion.isActive = true;
    }

    private void UpCommingRainTrigger(SimulateParameters parameters)
    {
        //upCommingRainQuestion.isActive = true;
    }

    private void RainingTrigger(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        Debug.Log("Raining");
        OnTimeControl?.Invoke();
        rainingQuestion.isActive = true;
        animation.RainEnable(parameters, forecast);
        header = "ฝนตก";
        instructionObject = raining;
        Invoke("showInstruction", 3.0f);                
    }    

    private void FloodingTrigger(SimulateParameters parameters)
    {
        OnTimeControl?.Invoke();
        floodQuestion.isActive = true;
        header = "น้ำท่วม";
        instructionObject = flood;
        Invoke("showInstruction", 3.0f);
    }

    private void NotRainTrigger(SimulateParameters parameters)
    {        
        notRainQuestion.isActive = true;
    }

    private void SeaRiseTrigger(SimulateParameters parameters)
    {
        seaRiseQuestion.isActive = true;
    }

    private void showInstruction()
    {
        instructionDisplay.createInstruction(header, instructionObject);
    }

    /** Trigger question when user complete read the instruction and click solve button */
    public void QuestionTrigger()
    {
        if (insectQuestion.isActive)
        {
            questionDisplay.OpenQuestionWindow(insectQuestion);
        }
        else if (diseaseQuestion.isActive)
        {
            questionDisplay.OpenQuestionWindow(diseaseQuestion);
        }
        else if (upCommingRainQuestion.isActive) //maybe not necessary to have question?
        {
            questionDisplay.OpenQuestionWindow(upCommingRainQuestion);
        }
        else if (rainingQuestion.isActive) //maybe not necessary to have question?
        {
            questionDisplay.OpenQuestionWindow(rainingQuestion);
        }
        else if (floodQuestion.isActive)
        {
            questionDisplay.OpenQuestionWindow(floodQuestion);
        }
        else if (notRainQuestion.isActive)
        {
            questionDisplay.OpenQuestionWindow(notRainQuestion);
        }
        else
        {
            questionDisplay.OpenQuestionWindow(seaRiseQuestion);
        }
    }
}
