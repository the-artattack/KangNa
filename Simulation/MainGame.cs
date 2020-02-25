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
    public static event OnEventTrigger onRainTrigger;
    public static event OnEventTrigger onNotRainTrigger;
    public static event OnEventTrigger onSeaRiseTrigger;
    public static event OnEventTrigger onDroughtTrigger;
    public static event OnEventTrigger onDiseaseTrigger;
    public static event OnEventTrigger onFloodTrigger;
    public static event OnEventTrigger onRainForecastTrigger;
    public delegate void OnEventTrigger();

    // Start is called before the first frame update
    void Start()
    {
        parameterInstance = SimulateParameters.parameterInstance;
        if (parameterInstance == null)
        {            
            oldTurn = TurnControl.turnInstance.turn;

            onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);

            eventHandler.upCommingRaining();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InsectAnimation.onUseInsecticide += InsectSolution;
        FloodAnimation.onFlooding += FloodSolution;
        SeaRiseAnimation.onSeaRise += seaRiseSolution;
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
                        parameterInstance.WaterVolume = eventHandler.Rainning(parameterInstance.WaterVolume, forecast.data.cond);
                        if (parameterInstance.UseReservoir)
                        {
                            parameterInstance.ReservoirVolume = eventHandler.Rainning(parameterInstance.ReservoirVolume, forecast.data.cond) / 100 * 40 * 20;
                        }
                        Debug.Log("Rainning");
                        parameterInstance.IsRain = true;
                    }
                    else
                    {
                        parameterInstance.IsRain = false;
                        eventHandler.notRaining();
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
                        eventHandler.Insect();
                        Debug.Log("Insect");
                    }
                }

                foreach (TMD_class.Forecast forecast in parameterInstance.diseaseForecast)
                {
                    diseaseTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Disease
                        eventHandler.Disease();
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
                            eventHandler.SeaRise();
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
                        eventHandler.Flooding();
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
                        eventHandler.LackOfWater();
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

    public void InsectSolution(bool useInsecticide)
    {
        parameterInstance.useInsecticide = useInsecticide;
        if (useInsecticide)
        {
            parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 2);
        }
        else if (!useInsecticide)
        {
            parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 5);
        }
        else
            parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 10);
    }

    public void FloodSolution(bool useDrain)
    {
        parameterInstance.useDrain = useDrain;
        parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 5);
    }

    public void seaRiseSolution(bool close)
    {
        parameterInstance.closeWaterWay = close;
        if (!close)
        {
            parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 2);
        }
    }
}
