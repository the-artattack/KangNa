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
    public static event OnEventTrigger onSummaryTrigger;
    public static event OnEventTrigger onWaterTabTrigger;
    public delegate void OnEventTrigger(SimulateParameters parameters);

    public static event OnRainTrigger onRainTrigger;
    public delegate void OnRainTrigger(SimulateParameters parameters, TMD_class.Forecast forecast);

    // Start is called before the first frame update
    void Start()
    {
        WeatherAPI.onWeatherTrigger += onWeatherComplete;
    }

    private void onWeatherComplete()
    {
        parameterInstance = new SimulateParameters();

        oldTurn = TurnControl.turnInstance.turn;

        onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);

        Debug.Log("First Rain: " + parameterInstance.rainForecast[0]);

        upCommingRaining();
        
        onWaterTabTrigger?.Invoke(parameterInstance);
        WaterTab.onParameterUpdateTrigger += getParameterUpdate;

        RiceTab.onHarvest += createSummary;

        Debug.Log("MainGame: Created.");
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

                foreach (TMD_class.Forecast forecast in WeatherAPI.AllForecast)
                {
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        RainMonitor(forecast);
                        SeaRiseMonitor(forecast);

                        CanalMonitor();
                        WaterMonitor();

                        //Disease
                        BacterialBlight(forecast);
                        RiceBlast(forecast);
                        SheathBlight(forecast);
                        RaggedStunt();
                        DirtyPanicle(forecast);
                        BrownSpot(forecast);

                        //Bug
                        Thrips(forecast);
                        WhiteBackedPlantHopper();
                        WhiteLeafHopper();
                        LeafFolder();
                        BrownPlantHopper(forecast);
                        RiceBlackBug(forecast);
                        RiceGallMidges(forecast);
                        RiceCaseWorm();
                        PossessedBug();
                    }
                }

                //Normal Situation
                parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 1);
            }
        }
        oldTurn = TurnControl.turnInstance.turn;
    }

    #region Monitors
    void RainMonitor(TMD_class.Forecast forecast)
    {
        if (forecast.data.cond >= 5 && forecast.data.cond <= 8)
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

    void SeaRiseMonitor(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        seaRiseTrigger = false;
        if (parameterInstance.UseCanal)
        {
            number = rnd.Next(0, 7);
            if(number == 3)
            {
                //Trigger Sea rise
                SeaRise();
                Debug.Log("Sea rise");
            }
            
        }
    }

    void CanalMonitor()
    {
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
    }

    void WaterMonitor()
    {
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
    }

    void BacterialBlight(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (forecast.data.cond >= 7 && forecast.data.cond <= 8)
        {
            number = rnd.Next(0, 3);
            if (number == 1)
            {
                //Trigger Bacterial Blight
                Debug.Log("Bacterial Blight");
            }
        }
    }

    void RiceBlast(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (forecast.data.cond == 5)
        {
            if (forecast.data.rh >= 80)
            {
                number = rnd.Next(0, 3);
                if (number == 1)
                {
                    //Trigger Rice Blast
                    Debug.Log("Rice Blast");
                }
            }
        }
    }

    void SheathBlight(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        double temperature = (forecast.data.tc_max + forecast.data.tc_min) / 2;
        if (temperature >= 28 && temperature <= 32)
        {
            if (forecast.data.cond >= 3 && forecast.data.cond <= 4)
            {
                number = rnd.Next(0, 3);
                if (number == 1)
                {
                    //Trigger Sheath Blight
                    Debug.Log("Sheath Blight");
                }
            }
        }
    }

    void RaggedStunt()
    {
        //After Brown hopper occur
    }

    void DirtyPanicle(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (forecast.data.ws10m >= 6)
        {
            number = rnd.Next(0, 3);
            if (number == 1)
            {
                //Trigger Dirty Panicle
                Debug.Log("Dirty Panicle");
            }
        }
    }

    void BrownSpot(TMD_class.Forecast forecast)
    {
        DirtyPanicle(forecast);
        //Trigger Brown Spot
        Debug.Log("Brown Spot");
    }

    void Thrips(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (TurnControl.turnInstance.day >= 14 && TurnControl.turnInstance.day <= 20)
        {
            if (forecast.data.cond == 1 || forecast.data.cond == 12)
            {
                if (droughtTrigger)
                {
                    number = rnd.Next(0, 3);
                    if (number == 1)
                    {
                        //Trigger Thrips
                        Debug.Log("Thrips");
                    }
                }
            }
        }
    }

    void WhiteBackedPlantHopper()
    {
        System.Random rnd = new System.Random();
        int number;

        if (TurnControl.turnInstance.day >= 20 && TurnControl.turnInstance.day <= 30)
        {
            number = rnd.Next(0, 7);
            if (number == 3)
            {
                //Trigger White Backed Plant Hopper
                Debug.Log("White Backed Plant Hopper");
            }
        }
    }

    void WhiteLeafHopper()
    {
        System.Random rnd = new System.Random();
        int number;

        if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 9)
        {
            number = rnd.Next(0, 7);
            if (number == 3)
            {
                //Trigger White Leaf Hopper
                Debug.Log("White Leaf Hopper");
            }
        }
    }

    void LeafFolder()
    {
        //After Brown Hopper occur
    }

    void BrownPlantHopper(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (!droughtTrigger)
        {
            if (forecast.data.rh >= 60)
            {
                number = rnd.Next(0, 7);
                if (number == 3)
                {
                    //Trigger Brown Plant Hopper
                    Debug.Log("Brown Plant Hopper");
                }
            }            
        }
    }

    void RiceBlackBug(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        double temperature = (forecast.data.tc_max + forecast.data.tc_min) / 2;
        if (temperature <= 29)
        {
            if (forecast.data.cond >= 2 && forecast.data.cond <= 4)
            {
                number = rnd.Next(0, 7);
                if (number == 3)
                {
                    //Trigger Rice Black Bug
                    Debug.Log("Rice Black Bug");
                }
            }
        }
    }

    void RiceGallMidges(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 9)
        {
            if (forecast.data.rh >= 80)
            {
                if (forecast.data.cond >= 3 && forecast.data.cond <= 4)
                {
                    number = rnd.Next(0, 7);
                    if (number == 3)
                    {
                        //Trigger Rice Gall Midges
                        Debug.Log("Rice Gall Midges");
                    }
                }
            }
        }
    }

    void RiceCaseWorm()
    {
        System.Random rnd = new System.Random();
        int number;

        if (!droughtTrigger)
        {
            number = rnd.Next(0, 7);
            if (number == 3)
            {
                //Trigger Rice Case Worm
                Debug.Log("Rice Case Worm");
            }
        }
    }

    void PossessedBug()
    {
        System.Random rnd = new System.Random();
        int number;

        if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 7)
        {
            number = rnd.Next(0, 7);
            if (number == 3)
            {
                //Trigger Possessed Bug
                Debug.Log("Possessed Bug");
            }
        }
    }
    #endregion

    public void createSummary()
    {
        onSummaryTrigger?.Invoke(parameterInstance);
    }

    private void UpdateParameters()
    {
        RainAnimation.onParameterUpdateTrigger += getParameterUpdate;
        NotRainAnimation.onParameterUpdateTrigger += getParameterUpdate;
        InsectAnimation.onParameterUpdateTrigger += getParameterUpdate;
        FloodAnimation.onParameterUpdateTrigger += getParameterUpdate;
        SeaRiseAnimation.onParameterUpdateTrigger += getParameterUpdate;
        DiseaseAnimation.onParameterUpdateTrigger += getParameterUpdate;

        WaterTab.onParameterUpdateTrigger += getParameterUpdate;
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
