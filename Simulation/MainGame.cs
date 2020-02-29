using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    private SimulateParameters parameterInstance;
    public bool insectTrigger = false;
    public bool rainTrigger = false;
    public bool upCommingRain = false;
    public bool diseaseTrigger = false;
    public bool floodTrigger = false;
    public bool seaRiseTrigger = false;
    public bool droughtTrigger = false;

    private int oldTurn;

    public static event onWeatherHandler onDateChanges;
    public delegate void onWeatherHandler(DateTime dateChanged);

    public static event OnEventTrigger onInsectTrigger;
    public static event OnEventTrigger onNotRainTrigger;
    public static event OnEventTrigger onSeaRiseTrigger;
    public static event OnEventTrigger onDroughtTrigger;
    public static event OnEventTrigger onDiseaseTrigger;
    public static event OnEventTrigger onFloodTrigger;
    public static event OnEventTrigger onRainForecastTrigger;
    public delegate void OnEventTrigger(SimulateParameters parameters);

    public static event OnRainTrigger onRainTrigger;
    public delegate void OnRainTrigger(SimulateParameters parameters, TMD_class.Forecast forecast);

    // Start is called before the first frame update
    void Start()
    {
        parameterInstance = SimulateParameters.parameterInstance;
        if (parameterInstance == null)
        {            
            oldTurn = TurnControl.turnInstance.turn;

            onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);

            upCommingRaining();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateParameters();
        if (oldTurn != TurnControl.turnInstance.turn)
        {
            if (TurnControl.turnInstance.turn % 24 == 0)
            {
                onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);
                Debug.Log("Water: " + parameterInstance.WaterVolume);
                Debug.Log("Rice: " + parameterInstance.RiceQuantity);

                foreach (TMD_class.Forecast forecast in parameterInstance.rainForecast)
                {
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Rain
                        Rainning(forecast);
                        Debug.Log("Rainning");
                    }
                    else
                    {
                        notRaining();
                    }
                }

                if (parameterInstance.UseCanal && !parameterInstance.IsRain)
                {
                    parameterInstance.WaterVolume = 50;
                }

                else if (parameterInstance.UseCanal && parameterInstance.IsRain)
                {
                    if (parameterInstance.WaterVolume > 50)
                    {
                        parameterInstance.WaterVolume -= 0.5;
                    }
                }

                foreach (TMD_class.Forecast forecast in parameterInstance.insectForecast)
                {
                    insectTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Insect invade
                        Insect();
                        Debug.Log("Insect");
                    }
                }

                foreach (TMD_class.Forecast forecast in parameterInstance.diseaseForecast)
                {
                    diseaseTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Disease
                        Disease();
                        Debug.Log("Disease");
                    }
                }

                foreach (TMD_class.Forecast forecast in parameterInstance.seaRiseForecast)
                {
                    seaRiseTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        if (parameterInstance.UseCanal)
                        {
                            //Trigger Sea rise
                            SeaRise();
                            Debug.Log("Sea rise");
                        }
                    }
                }

                if (parameterInstance.WaterVolume > parameterInstance.WaterBaseLine + 10)
                {
                    //Trigger Flooding
                    floodTrigger = false;
                    if (parameterInstance.UseReservoir)
                    {
                        if (parameterInstance.ReservoirVolume <= 2000)
                        {
                            parameterInstance.ReservoirVolume += (parameterInstance.WaterVolume - 50) * 40 * 20;
                            parameterInstance.WaterVolume = 50;
                        }
                        else
                            parameterInstance.ReservoirVolume = 2000;
                    }

                    if (parameterInstance.day7Count > 4)
                    {
                        Debug.Log("Flooding");
                        Flooding();
                    }
                    parameterInstance.day7Count++;
                }

                else if (parameterInstance.WaterVolume < parameterInstance.WaterBaseLine - 10)
                {
                    //Trigger low water

                    if (parameterInstance.UseReservoir)
                    {
                        if (parameterInstance.ReservoirVolume >= 0)
                        {
                            parameterInstance.ReservoirVolume -= (50 - parameterInstance.WaterVolume) * 40 * 20;
                            parameterInstance.WaterVolume = 50;
                        }
                        else
                            parameterInstance.ReservoirVolume = 0;
                    }

                    if (parameterInstance.day7Count > 4)
                    {
                        Debug.Log("Lack of Water");
                        LackOfWater();
                    }
                    parameterInstance.day7Count++;
                }

                else
                {
                    parameterInstance.day7Count = 0;
                }

                //Normal Situation
                parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 1);
            }
        }
        oldTurn = TurnControl.turnInstance.turn;
    }

    private void UpdateParameters()
    {
        RainAnimation.onParameterUpdateTrigger += getParameterUpdate;
        NotRainAnimation.onParameterUpdateTrigger += getParameterUpdate;
        InsectAnimation.onParameterUpdateTrigger += getParameterUpdate;
        FloodAnimation.onParameterUpdateTrigger += getParameterUpdate;
        SeaRiseAnimation.onParameterUpdateTrigger += getParameterUpdate;
        DiseaseAnimation.onParameterUpdateTrigger += getParameterUpdate;
    }

    private void getParameterUpdate(SimulateParameters parameters)
    {
        parameterInstance = parameters;
    }

    public void upCommingRaining()
    {
        upCommingRain = true;
        onRainForecastTrigger?.Invoke(parameterInstance);
    }
    public void Insect()
    {
        insectTrigger = true;
        onInsectTrigger?.Invoke(parameterInstance);
        //Trigger Insect        
    }

    public void Disease()
    {
        diseaseTrigger = true;
        onDiseaseTrigger?.Invoke(parameterInstance);
    }

    public void Flooding()
    {
        floodTrigger = true;
        onFloodTrigger?.Invoke(parameterInstance);
        //Trigger Flooding        
    }

    public void SeaRise()
    {
        seaRiseTrigger = true;
        onSeaRiseTrigger?.Invoke(parameterInstance);
        //Trigger Sea rise
    }

    public void Rainning(TMD_class.Forecast forecast)
    {
        rainTrigger = true;
        onRainTrigger?.Invoke(parameterInstance, forecast);
    }

    public void notRaining()
    {
        onNotRainTrigger?.Invoke(parameterInstance);
    }

    public void LackOfWater()
    {
        droughtTrigger = true;
        onDroughtTrigger?.Invoke(parameterInstance);
        //Trigger Low water
        parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 8);
    }
}
