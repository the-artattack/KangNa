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
    public Question floodQuestion;
    public Question seaRiseQuestion;
    public Question droughtQuestion;

    private bool isInsect;
    private bool isDisease;
    private string insectName;
    private string diseaseName;
    public new Animation animation;

    public delegate void onTimeControl();
    public static event onTimeControl OnTimeControl;

    public void Start()
    {
        isInsect = false;
        isDisease = false;
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
        isInsect = true;
        insectName = insect;
        GameObject temp = insects.Where(obj => obj.name == insect).SingleOrDefault();
        instructionDisplay.createInstruction(insect, temp);        
        animation.InsectEnable(insect, parameters);
    }

    private void DiseaseTrigger(string disease, SimulateParameters parameters)
    {
        isDisease = true;
        diseaseName = disease;
        GameObject temp = diseases.Where(obj => obj.name == disease).SingleOrDefault();
        instructionDisplay.createInstruction(disease, temp);
        animation.DiseaseEnable(disease, parameters);
    }

    private void UpCommingRainTrigger(SimulateParameters parameters)
    {
        Events.UpCommingRain = true;
        animation.UpCommingRainEnable(parameters);        
    }

    private void RainingTrigger(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        Events.Rain = true;
        animation.RainEnable(parameters, forecast);
        header = "ฝนตก";
        instructionObject = raining;
        //Invoke("showInstruction", 3.0f);                
    }    

    private void FloodingTrigger(SimulateParameters parameters)
    {
        Events.Flood = true;
        floodQuestion.isActive = true;
        animation.FloodEnable(parameters);
        header = "น้ำท่วม";
        instructionObject = flood;
        Invoke("showInstruction", 3.0f);
    }

    private void NotRainTrigger(SimulateParameters parameters)
    {
        Events.Rain = false;
        animation.NotRainEnable(parameters);
    }

    private void SeaRiseTrigger(SimulateParameters parameters)
    {
        Events.SeaRise = true;
        seaRiseQuestion.isActive = true;
        animation.SeaRiseEnable(parameters);
        header = "น้ำทะเลหนุน";
        instructionObject = seaRise;
        Invoke("showInstruction", 3.0f);
    }

    private void DroughtTrigger(SimulateParameters parameters)
    {
        Events.Drought = true;
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
        if (isInsect)
        {
            questionDisplay.OpenQuestionWindow(isInsect, insectName);
            isInsect = false;
        }
        else if (isDisease)
        {
            questionDisplay.OpenQuestionWindow(!isInsect, diseaseName);
            isDisease = false;
        }
        else
        {
            if (floodQuestion.isActive)
            {
                questionDisplay.OpenQuestionWindow(floodQuestion);
            }            
            else if(seaRiseQuestion.isActive)
            {
                questionDisplay.OpenQuestionWindow(seaRiseQuestion);
            }
            else
            {
                //do nothing
            }
        }
    }
}
