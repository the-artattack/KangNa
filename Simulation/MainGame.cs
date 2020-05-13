using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    private SimulateParameters parameterInstance;
    public MoneyList moneyList;

    private int oldTurn;

    public static event onWeatherHandler onDateChanges;
    public delegate void onWeatherHandler(DateTime dateChanged);
    
    public static event OnEventTrigger onNotRainTrigger;
    public static event OnEventTrigger onSeaRiseTrigger;
    public static event OnEventTrigger onDroughtTrigger;    
    public static event OnEventTrigger onFloodTrigger;
    public static event OnEventTrigger onRainForecastTrigger;
    public static event OnEventTrigger onSummaryTrigger;
    public static event OnEventTrigger onWaterTabTrigger;
    public delegate void OnEventTrigger(SimulateParameters parameters);

    public static event OnInsectTrigger onInsectTrigger;
    public delegate void OnInsectTrigger(string insect, SimulateParameters parameters);

    public static event OnDiseaseTrigger onDiseaseTrigger;
    public delegate void OnDiseaseTrigger(string disease, SimulateParameters parameters);

    public static event OnRainTrigger onRainTrigger;
    public delegate void OnRainTrigger(SimulateParameters parameters, TMD_class.Forecast forecast);

    // Start is called before the first frame update
    void Start()
    {
        WeatherAPI.onWeatherTrigger += onWeatherComplete;
        UpdateParameters();
    }

    private void onWeatherComplete()
    {
        parameterInstance = new SimulateParameters();

        oldTurn = TurnControl.turnInstance.turn;

        onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);

        //Debug.Log("First Rain: " + parameterInstance.rainForecast[0].time);

        upCommingRaining();
        
        onWaterTabTrigger?.Invoke(parameterInstance);
        WaterTab.onParameterUpdateTrigger += getParameterUpdate;

        RiceTab.onHarvest += updateParameters;
        Debug.Log("MainGame: Created.");
    }

    // Update is called once per frame
    void Update()
    {        
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

                        //Insect
                        Thrips(forecast);
                        WhiteBackedPlantHopper();
                        GreenLeafHopper();
                        LeafFolder();
                        BrownPlantHopper(forecast);
                        RiceBlackBug(forecast);
                        RiceGallMidges(forecast);
                        RiceCaseWorm();
                        StinkBug();
                    }
                }

                //Normal Situation
                switch (TurnControl.turnInstance.gameDate.Month)
                {
                    case 1:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.49);
                        break;
                    case 2:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 4.13);
                        break;
                    case 3:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 4.66);
                        break;
                    case 4:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 4.83);
                        break;
                    case 5:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 4.22);
                        break;
                    case 6:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.90);
                        break;
                    case 7:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.85);
                        break;
                    case 8:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.64);
                        break;
                    case 9:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.61);
                        break;
                    case 10:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.63);
                        break;
                    case 11:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.68);
                        break;
                    case 12:
                        parameterInstance.WaterVolume = eventHandler.WaterReduction(parameterInstance.WaterVolume, 3.51);
                        break;
                }
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
            //Debug.Log("Rainning");
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
        if (parameterInstance.WaterVolume > parameterInstance.WaterBaseLine + 30)
        {         
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
                //Debug.Log("Flooding");
                Flooding();
            }
            parameterInstance.day7Count++;
        }

        else if (parameterInstance.WaterVolume < parameterInstance.WaterBaseLine - 30)
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
                //Debug.Log("Lack of Water");
                LackOfWater();
            }
            parameterInstance.day7Count++;
        }

        else
        {
            parameterInstance.day7Count = 0;
        }
    }
    #endregion

    #region Disease
    void BacterialBlight(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะต้นกล้า" || RiceTab.RicePhase == "ระยะแตกกอ" || RiceTab.RicePhase == "ระยะตั้งท้อง")
        {
            if (forecast.data.cond >= 7 && forecast.data.cond <= 8)
            {
                number = rnd.Next(0, 3);
                if (number == 1)
                {
                    //Trigger Bacterial Blight
                    Debug.Log("โรคขอบใบแห้ง");
                    onDiseaseTrigger?.Invoke("โรคขอบใบแห้ง", parameterInstance);
                }
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
                number = rnd.Next(0, 24);
                if (number == 4)
                {
                    //Trigger Rice Blast
                    Debug.Log("โรคไหม้");
                    onDiseaseTrigger?.Invoke("โรคไหม้", parameterInstance);
                }
            }
        }
    }

    void SheathBlight(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะแตกกอ" || RiceTab.RicePhase == "ระยะตั้งท้อง")
        {
            double temperature = (forecast.data.tc_max + forecast.data.tc_min) / 2;
            if (temperature >= 28 && temperature <= 32)
            {
                if (forecast.data.cond >= 3 && forecast.data.cond <= 4)
                {
                    number = rnd.Next(0, 14);
                    if (number == 6)
                    {
                        //Trigger Sheath Blight
                        Debug.Log("โรคกาบใบแห้ง");
                        onDiseaseTrigger?.Invoke("โรคกาบใบแห้ง", parameterInstance);
                    }
                }
            }
        }            
    }

    void RaggedStunt()
    {        
        if (Events.BrownPlantHopper == true)
        {
            if (RiceTab.RicePhase == "ระยะต้นกล้า" || RiceTab.RicePhase == "ระยะแตกกอ" || RiceTab.RicePhase == "ระยะตั้งท้อง")
            {
                Debug.Log("โรคใบหงิก");
                onDiseaseTrigger?.Invoke("โรคใบหงิก", parameterInstance);
            }
        }        
    }

    void DirtyPanicle(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะออกรวง")
        {
            if (forecast.data.ws10m >= 6)
            {
                number = rnd.Next(0, 24);
                if (number == 3)
                {
                    //Trigger Dirty Panicle
                    Debug.Log("โรคเมล็ดด่าง");
                    onDiseaseTrigger?.Invoke("โรคเมล็ดด่าง", parameterInstance);
                }
            }
        }        
    }

    void BrownSpot(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะเก็บเก็ยว")
        {
            if (forecast.data.ws10m >= 6)
            {
                number = rnd.Next(0, 24);
                if (number == 8)
                {
                    //Trigger Brown Spot
                    Debug.Log("โรคใบจุดสีน้ำตาล");
                    onDiseaseTrigger?.Invoke("โรคใบจุดสีน้ำตาล", parameterInstance);
                }
            }
        }               
    }
    #endregion

    #region Insect
    void Thrips(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะต้นกล้า" && TurnControl.turnInstance.day >= 14)
        {
            if (forecast.data.cond == 1 || forecast.data.cond == 12)
            {
                if (Events.Drought)
                {
                    number = rnd.Next(0, 14);
                    if (number == 1)
                    {
                        //Trigger Thrips
                        Debug.Log("เพลี้ยไฟ");
                        onInsectTrigger?.Invoke("เพลี้ยไฟ", parameterInstance);
                    }
                }
            }
        }        
    }

    void WhiteBackedPlantHopper()
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase != "ระยะต้นกล้า" && TurnControl.turnInstance.day <= 30)
        {
            number = rnd.Next(0, 24);
            if (number == 3)
            {
                //Trigger White Backed Plant Hopper
                Debug.Log("เพลี้ยกระโดดหลังขาว");
                onInsectTrigger?.Invoke("เพลี้ยกระโดดหลังขาว", parameterInstance);
            }
        }        
    }

    void GreenLeafHopper()
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะแตกกอ")
        {
            if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 9)
            {
                number = rnd.Next(0, 24);
                if (number == 9)
                {
                    //Trigger White Leaf Hopper
                    Debug.Log("เพลี้ยจักจั่นสีเขียว");
                    onInsectTrigger?.Invoke("เพลี้ยจักจั่นสีเขียว", parameterInstance);
                }
            }
        }        
    }

    void LeafFolder()
    {        
        if (Events.BrownPlantHopper == true)
        {
            if (RiceTab.RicePhase == "ระยะแตกกอ")
            {
                Debug.Log("หนอนห่อใบข้าว");
                onInsectTrigger?.Invoke("หนอนห่อใบข้าว", parameterInstance);
            }
        }        
    }

    void BrownPlantHopper(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะแตกกอ" || RiceTab.RicePhase == "ระยะออกรวง")
        {
            if (!Events.Drought)
            {
                if (forecast.data.rh >= 60)
                {
                    number = rnd.Next(0, 24);
                    if (number == 11)
                    {
                        //Trigger Brown Plant Hopper
                        Debug.Log("เพลี้ยกระโดดสีน้ำตาล");
                        onInsectTrigger?.Invoke("เพลี้ยกระโดดสีน้ำตาล", parameterInstance);
                    }
                }
            }
        }
    }

    void RiceBlackBug(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase != "ระยะต้นกล้า")
        {
            double temperature = (forecast.data.tc_max + forecast.data.tc_min) / 2;
            if (temperature <= 29)
            {
                if (forecast.data.cond >= 2 && forecast.data.cond <= 4)
                {
                    number = rnd.Next(0, 14);
                    if (number == 2)
                    {
                        //Trigger Rice Black Bug
                        Debug.Log("แมลงหล่า");
                        onInsectTrigger?.Invoke("แมลงหล่า", parameterInstance);
                    }
                }
            }
        }        
    }

    void RiceGallMidges(TMD_class.Forecast forecast)
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะแตกกอ")
        {
            if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 9)
            {
                if (forecast.data.rh >= 80)
                {
                    if (forecast.data.cond >= 3 && forecast.data.cond <= 4)
                    {
                        number = rnd.Next(0, 14);
                        if (number == 13)
                        {
                            //Trigger Rice Gall Midges
                            Debug.Log("แมลงบั่ว");
                            onInsectTrigger?.Invoke("แมลงบั่ว", parameterInstance);
                        }
                    }
                }
            }
        }        
    }

    void RiceCaseWorm()
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะแตกกอ")
        {
            if (!Events.Drought)
            {
                number = rnd.Next(0, 24);
                if (number == 14)
                {
                    //Trigger Rice Case Worm
                    Debug.Log("หนอนปลอกข้าว");
                    onInsectTrigger?.Invoke("หนอนปลอกข้าว", parameterInstance);
                }
            }
        }        
    }

    void StinkBug()
    {
        System.Random rnd = new System.Random();
        int number;

        if (RiceTab.RicePhase == "ระยะออกรวง")
        {
            if (TurnControl.turnInstance.gameDate.Month >= 6 && TurnControl.turnInstance.gameDate.Month <= 7)
            {
                number = rnd.Next(0, 24);
                if (number == 16)
                {
                    //Trigger Possessed Bug
                    Debug.Log("แมลงสิง");
                    onInsectTrigger?.Invoke("แมลงสิง", parameterInstance);
                }
            }
        }        
    }
    #endregion

    #region Summary    

    private void updateParameters(DateTime gameDate, string riceType)
    {
        Parameters.parameters = parameterInstance;
        Parameters.userMoneyList = MoneyController.moneyList;
        Parameters.moneyList = moneyList;
        Parameters.date = gameDate;
        Parameters.riceType = riceType;

        //Parameters.print();
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
        Parameters.parameters = parameters;
    }
    #endregion

    #region Events
    public void upCommingRaining()
    {
        onRainForecastTrigger?.Invoke(parameterInstance);
    }            

    public void Flooding()
    {
        onFloodTrigger?.Invoke(parameterInstance);
        //Trigger Flooding        
        Debug.Log("Flooding");
    }

    public void SeaRise()
    {
        onSeaRiseTrigger?.Invoke(parameterInstance);
        //Trigger Sea rise
        Debug.Log("Sea rise");
    }

    public void Rainning(TMD_class.Forecast forecast)
    {
        onRainTrigger?.Invoke(parameterInstance, forecast);
        Debug.Log("Raining");
    }

    public void notRaining()
    {
        onNotRainTrigger?.Invoke(parameterInstance);
        Debug.Log("Not raining");
    }

    public void LackOfWater()
    {
        onDroughtTrigger?.Invoke(parameterInstance);
        Debug.Log("Drought");
        //Trigger Low water
        parameterInstance.RiceQuantity = eventHandler.RiceReduction(parameterInstance.RiceQuantity, 8);
    }
}
#endregion