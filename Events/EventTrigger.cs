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

    private Evaluation evaluation;

    /** which situation is current occur
     * 0 : nothing
     * 1 : insect
     * 2 : disease
     * 3 : flood
     * 4 : sea rise
     * 5 : drought */
    public int which;
    private string insectName;
    private string diseaseName;
    public new Animation animation;

    public delegate void onTimeControl();
    public static event onTimeControl OnTimeControl;

    private SimulateParameters parameters;
    public void Start()
    {
        which = 0;
        evaluation = GameObject.FindObjectOfType<Evaluation>();
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
        which = 1;
        insectName = insect;
        this.parameters = parameters;
        Instruction temp = insects.Where(obj => obj.name == insect).SingleOrDefault();
        TriggerBasedOnMode(temp);
        animation.InsectEnable(insect, parameters);
    }

    private void DiseaseTrigger(string disease, SimulateParameters parameters)
    {
        which = 2;
        diseaseName = disease;
        this.parameters = parameters;
        Instruction temp = diseases.Where(obj => obj.name == disease).SingleOrDefault();
        TriggerBasedOnMode(temp);
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
        which = 3;
        Events.Flood = true;
        flood.isActive = true;
        this.parameters = parameters;
        animation.FloodEnable(parameters);
        TriggerBasedOnMode(flood);        
    }

    private void NotRainTrigger(SimulateParameters parameters)
    {
        Events.Rain = false;
        this.parameters = parameters;
        animation.NotRainEnable(parameters);
    }

    private void SeaRiseTrigger(SimulateParameters parameters)
    {
        which = 4;
        Events.SeaRise = true;
        seaRiseQuestion.isActive = true;
        this.parameters = parameters;
        animation.SeaRiseEnable(parameters);
        TriggerBasedOnMode(seaRise);
    }

    private void DroughtTrigger(SimulateParameters parameters)
    {
        which = 5;
        Events.Drought = true;
        drought.isActive = true;
        this.parameters = parameters;
        animation.DroughtEnable(parameters);
        TriggerBasedOnMode(drought);
    }

    private void TriggerBasedOnMode(Instruction instruction)
    {
        //increse evaluation score for counting the number of event trigger
        evaluation.updateMaxScore();

        //beginner mode
        if(FirebaseInit.Instance.mode == 0)
        {
            OnTimeControl?.Invoke(); //pause        
            instructionList.addInstruction(instruction);
        }
        //expert mode
        else
        {
            
            OnTimeControl?.Invoke(); //pause       
            QuestionTrigger(); //show question
        }
    }

    /** Trigger question when user complete read the instruction and click solve button */
    public void QuestionTrigger()
    {        
        if (which == 1) //insect
        {
            which = 0; //reset

            Debug.Log("EventTrigger: insect triggered");
            questionList.addInsectQuestion(insectName, parameters);
            animation.InsectDisable(insectName);
        }
        else if (which == 2) //disease
        {
            which = 0; //reset

            Debug.Log("EventTrigger: disease triggered");
            questionList.addDiseaseQuestion(diseaseName, parameters);
            animation.DiseaseDisable(diseaseName);
        }
        else if (which == 3) //flood
        {
            which = 0; //reset

            questionList.addQuestion(floodQuestion, parameters);
            animation.FloodDisable();
        }
        else if (which == 4) //sea rise
        {
            which = 0; //reset

            questionList.addQuestion(seaRiseQuestion, parameters);
            animation.SeaRiseDisable();
        }
        else if (which == 5) //drought
        {
            which = 0; //reset

            questionList.addQuestion(droughtQuestion, parameters);
        }
        else
        {
            // do nothing
        }        
    }
}
