using UnityEngine;
using UnityEngine.UI;
public class SimulationManager : MonoBehaviour
{
    public static event OnEventTrigger onInsectTrigger;
    public static event OnEventTrigger onRainTrigger;
    public static event OnEventTrigger onSeaRiseTrigger;
    public static event OnEventTrigger onDroughtTrigger;
    public static event OnEventTrigger onDiseaseTrigger;
    public static event OnEventTrigger onFloodTrigger;
    public static event OnEventTrigger onRainForecastTrigger;
    public delegate void OnEventTrigger();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SimulateParameters.parameterInstance.rainTrigger)
        {
            onRainTrigger?.Invoke();
        }
        else if (SimulateParameters.parameterInstance.seaRiseTrigger)
        {
            onSeaRiseTrigger?.Invoke();
        }
        else if(SimulateParameters.parameterInstance.insectTrigger)
        {
            onInsectTrigger?.Invoke();
        }
        else if(SimulateParameters.parameterInstance.droughtTrigger)
        {
            onDroughtTrigger?.Invoke();
        }
        else if (SimulateParameters.parameterInstance.diseaseTrigger)
        {
            onDiseaseTrigger?.Invoke();
        }
        else if (SimulateParameters.parameterInstance.floodTrigger)
        {
            onFloodTrigger?.Invoke();
        }
        else if (SimulateParameters.parameterInstance.upCommingRain)
        {
            onRainForecastTrigger?.Invoke();
        }
    }    
}
