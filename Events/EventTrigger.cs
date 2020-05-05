using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/** This class is main class for handle question/animation on main game */
public class EventTrigger : MonoBehaviour
{
    public InstructionDisplay instructionDisplay;
    private string header;
    private Instruction instruction;
    public InstructionList instructionList;
    /** List of disease and insect instruction*/
    public List<Instruction> diseases;
    public List<Instruction> insects;
    public Instruction flood;
    public Instruction raining;
    public Instruction seaRise;
    public Instruction drought;

    /** question in each events*/      
    public Question floodQuestion;
    public Question seaRiseQuestion;
    public Question droughtQuestion;
    public QuestionList questionList;

    private bool isInsect;
    private bool isDisease;
    private string insectName;
    private string diseaseName;
    public new Animation animation;

    public delegate void onTimeControl();
    public static event onTimeControl OnTimeControl;

    private SimulateParameters parameters;
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
        isDisease = false;
        insectName = insect;
        this.parameters = parameters;
        Instruction temp = insects.Where(obj => obj.name == insect).SingleOrDefault();
        InstructionTrigger(temp);
        animation.InsectEnable(insect, parameters);
    }

    private void DiseaseTrigger(string disease, SimulateParameters parameters)
    {
        isDisease = true;
        isInsect = false;
        diseaseName = disease;
        this.parameters = parameters;
        Instruction temp = diseases.Where(obj => obj.name == disease).SingleOrDefault();
        InstructionTrigger(temp);
        animation.DiseaseEnable(disease, parameters);
    }

    private void UpCommingRainTrigger(SimulateParameters parameters)
    {
        Events.UpCommingRain = true;
        this.parameters = parameters;
        animation.UpCommingRainEnable(parameters);        
    }

    private void RainingTrigger(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        Events.Rain = true;
        animation.RainEnable(parameters, forecast);
        header = "ฝนตก";
        instruction = raining;
    }    

    private void FloodingTrigger(SimulateParameters parameters)
    {
        Events.Flood = true;
        this.parameters = parameters;
        animation.FloodEnable(parameters);
        InstructionTrigger(flood);        
    }

    private void NotRainTrigger(SimulateParameters parameters)
    {
        Events.Rain = false;
        this.parameters = parameters;
        animation.NotRainEnable(parameters);
    }

    private void SeaRiseTrigger(SimulateParameters parameters)
    {
        Events.SeaRise = true;
        this.parameters = parameters;
        animation.SeaRiseEnable(parameters);
        InstructionTrigger(seaRise);
    }

    private void DroughtTrigger(SimulateParameters parameters)
    {
        Events.Drought = true;
        this.parameters = parameters;
        animation.DroughtEnable(parameters);
        InstructionTrigger(drought);
    }

    private void InstructionTrigger(Instruction instruction)
    {
        OnTimeControl?.Invoke(); //pause        
        instructionList.addInstruction(instruction);
    }

    /** Trigger question when user complete read the instruction and click solve button */
    public void QuestionTrigger()
    {
        if (isInsect)
        {
            Debug.Log("EventTrigger: insect triggered");
            questionList.addQuestion(isInsect, insectName, parameters);
            isInsect = false;
        }
        else if (isDisease)
        {
            Debug.Log("EventTrigger: disease triggered");
            questionList.addQuestion(!isInsect, diseaseName, parameters);
            isDisease = false;
        }
        else
        {
            if (floodQuestion.isActive)
            {
                questionList.addQuestion(floodQuestion, parameters);
            }            
            else if(seaRiseQuestion.isActive)
            {
                questionList.addQuestion(seaRiseQuestion, parameters);
            }
            else
            {
                //do nothing
            }
        }
    }
}
