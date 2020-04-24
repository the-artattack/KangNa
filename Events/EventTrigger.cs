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
    public GameObject seaRise;
    public GameObject drought;

    /** question in each events*/
    public Question insectQuestion;
    public Question diseaseQuestion;    
    //public Question rainingQuestion;    
    public Question floodQuestion;
    public Question notRainQuestion;
    public Question seaRiseQuestion;
    public Question droughtQuestion;

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
        MainGame.onDroughtTrigger += DroughtTrigger;
    }    

    private void InsectTrigger(string insect, SimulateParameters parameters)
    {
        insectQuestion.isActive = true;
        GameObject temp = insects.Where(obj => obj.name == insect).SingleOrDefault();
        instructionDisplay.createInstruction(insect, temp);        
        animation.InsectEnable(insect, parameters);
    }

    private void DiseaseTrigger(string disease, SimulateParameters parameters)
    {
        GameObject temp = diseases.Where(obj => obj.name == disease).SingleOrDefault();
        diseaseQuestion.isActive = true;
        instructionDisplay.createInstruction(disease, temp);
        animation.DiseaseEnable(disease, parameters);
    }

    private void UpCommingRainTrigger(SimulateParameters parameters)
    {               
        animation.UpCommingRainEnable(parameters);        
    }

    private void RainingTrigger(SimulateParameters parameters, TMD_class.Forecast forecast)
    {     
        //rainingQuestion.isActive = true;
        animation.RainEnable(parameters, forecast);
        header = "ฝนตก";
        instructionObject = raining;
        Invoke("showInstruction", 3.0f);                
    }    

    private void FloodingTrigger(SimulateParameters parameters)
    {        
        floodQuestion.isActive = true;
        animation.FloodEnable(parameters);
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
        animation.SeaRiseEnable(parameters);
        header = "น้ำทะเลหนุน";
        instructionObject = seaRise;
        Invoke("showInstruction", 3.0f);
    }

    private void DroughtTrigger(SimulateParameters parameters)
    {
        droughtQuestion.isActive = true;
        animation.DroughtEnable(parameters);
        header = "แห้งแล้ง";
        instructionObject = drought;
        Invoke("showInstruction", 3.0f);
    }

    private void showInstruction()
    {
        OnTimeControl?.Invoke(); //pause
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
        /*else if (upCommingRainQuestion.isActive) //maybe not necessary to have question?
        {
            questionDisplay.OpenQuestionWindow(upCommingRainQuestion);
        }
        else if (rainingQuestion.isActive) //maybe not necessary to have question?
        {
            questionDisplay.OpenQuestionWindow(rainingQuestion);
        }*/
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
