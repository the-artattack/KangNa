using Newtonsoft.Json;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SimulateParameters : MonoBehaviour
{
    public static SimulateParameters parameterInstance { get; private set; }
    public bool insectTrigger = false;
    public bool rainTrigger = false;
    public bool upCommingRain = false;
    public bool diseaseTrigger = false;
    public bool floodTrigger = false;
    public bool seaRiseTrigger = false;
    public bool droughtTrigger = false;

    private static double riceQuantity = 5000; //kg
    public double waterVolume = 50; //cm
    private int waterBaseLine = 50; //cm
    private bool useReservoir = false;
    private double reservoirVolume = 0; //ลบ.ม.
    public bool useCanal = false;
    private bool isRain = false;
    private int day7Count = 0;
    private int rainCount = 0;
    private int limitInsect = 3;
    private int limitDisease = 3;
    private int limitSeaRise = 3;

    public bool useInsecticide = false;
    public bool useHerbicide = false;
    public bool closeWaterWay = false; //for sea rise event
    public bool useDrain = false;

    private static TMD_class myObject = new TMD_class();
    string lat = "13.655091";
    string lon = "100.494908";
    private DateTime date = DateTime.Now;
    private int duration;
    string fields = "rain,cond,tc_min,tc_max";

    public static List<TMD_class.Forecast> allForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> rainForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> floodForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> noRainForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> insectForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> diseaseForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> seaRiseForecast = new List<TMD_class.Forecast>();

    List<int> chkDuplicate = new List<int>();

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

    /*public DateTime MyDate
    {
        get
        {
            Debug.Log("Current date: " + date);
            return date;
        }
        set
        {
            if (date == value) return;
            date = value;
            Debug.Log("Current date: " + date);
            onDateChanges?.Invoke(date);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        InsectAnimation.onUseInsecticide += InsectSolution;
        FloodAnimation.onFlooding += FloodSolution;
        SeaRiseAnimation.onSeaRise += seaRiseSolution;
        if (parameterInstance == null)
        {
            parameterInstance = this;
            duration = 30 - date.Day;
            RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImUzMzlkYTEzZWQ0ZDc3NTZjZDM2YmNmZGI0MDVhZTI0OGMwZmUzZGVhOTI0NjVjZjMyYmY1MTQ1OGI4NWNjYmZlN2VhOWNmNDE4YTVhZmRhIn0.eyJhdWQiOiIyIiwianRpIjoiZTMzOWRhMTNlZDRkNzc1NmNkMzZiY2ZkYjQwNWFlMjQ4YzBmZTNkZWE5MjQ2NWNmMzJiZjUxNDU4Yjg1Y2NiZmU3ZWE5Y2Y0MThhNWFmZGEiLCJpYXQiOjE1ODA5Nzg5NDUsIm5iZiI6MTU4MDk3ODk0NSwiZXhwIjoxNjEyNjAxMzQ1LCJzdWIiOiI3MDEiLCJzY29wZXMiOltdfQ.nFSWISVdU7QPGjgL4FVUam0tQfo1s5Sm287mnYUPrNgss8asxGWKj1jIUsn840BDzVgOfdhpgQcAQ7ynxbCFz9BvmuvQnWcZ1_JeC7LJ29bye9TIg4BZ9z3mEXYdk4QwXYIBSuiVW4bY1lFO9LvThbdyrQuISOI43pon69qUzeL7x06WjoAl0EOetCg1OG15uSiZFUYowNOFr7jDF4_fx-6ftb0EKT9gsrPCkEVyo5VG-O6rfjoBZYx-rzJBSrWKGldzunMUua38T71ZVfuuGZn_7paqOZd4Dn_OZvN7EbE56jeC2GvFJEGaJqJmCwJB3I9RlG06BvkiLX5YV9C8f08IlON2smGufW-N_-rSJ2sX0pFsKvESwWJeicdQQb_byY0M2IVVdjIno_rV8nQIpqJztx3WLSZAnjunmVvDJnQ1m7dP1RZqXaM9osYa_JWgqFrSdp-A8u-BoiyFd_OIaxgdXn928L6AnYmIm9dWwEaokZHcUIxpviP-P1ruaM4-GrwmXCEcRZ7sqepRcgnGwkJxENolgWYJxEF0A4QpOSUUvHYk_I0414PX1Tmrktb9Ynzz5RrFHrZTmBEmEYNdRi31PLoWdAYWHeVMmog6vJ79Agscv4Iv90hjk-jsQ3eu9-NJbMwvUcQwuKF2rVs6DVX6g9d_qc2vpMfh6dPbMjU";
            RestClient.DefaultRequestParams["ContentType"] = "application/json";

            RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 30).ToString())
                .Then(response =>
                {

                    myObject = JsonConvert.DeserializeObject<TMD_class>(response.Text);

                    foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                    {
                        allForecast.Add(forecast);
                    }

                    RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 60).ToString())
                        .Then(res1 =>
                        {

                            myObject = JsonConvert.DeserializeObject<TMD_class>(res1.Text);

                            foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                            {
                                allForecast.Add(forecast);
                            }

                            RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 90).ToString())
                                .Then(res2 =>
                                {

                                    myObject = JsonConvert.DeserializeObject<TMD_class>(res2.Text);

                                    foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                                    {
                                        allForecast.Add(forecast);
                                    }

                                    foreach (TMD_class.Forecast forecast in allForecast)
                                    {
                                        if (forecast.data.cond >= 5 && forecast.data.cond <= 8)
                                        {
                                            rainForecast.Add(forecast);
                                            if (rainCount > 4 && day7Count > 4)
                                            {
                                                floodForecast.Add(forecast);
                                            }
                                            rainCount++;
                                        }
                                        else
                                        {
                                        //Debug.Log("No rain.");
                                        noRainForecast.Add(forecast);
                                        }

                                        day7Count++;
                                        day7Count %= 7;
                                    }

                                    GenerateInsect(limitInsect);
                                    GenerateDisease(limitDisease);
                                    GenerateSeaRise(limitSeaRise);

                                    oldTurn = TurnControl.turnInstance.turn;

                                    day7Count = 0;

                                    Debug.Log("First rain: " + rainForecast[0].time.Date);
                                    upCommingRaining();
                                }).Catch(err => Debug.Log(err.Message));
                        }).Catch(err => Debug.Log(err.Message));
                    onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);
                }).Catch(err => Debug.Log(err.Message));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(oldTurn != TurnControl.turnInstance.turn)
        {            
            if (TurnControl.turnInstance.turn % 24 == 0)
            {
                onDateChanges?.Invoke(TurnControl.turnInstance.gameDate);               
                Debug.Log("Water: " + waterVolume);
                Debug.Log("Rice: " + riceQuantity);
                
                foreach (TMD_class.Forecast forecast in rainForecast)
                {
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Rain
                        waterVolume = Rainning(waterVolume, forecast.data.cond);
                        if (useReservoir)
                        {
                            reservoirVolume = Rainning(reservoirVolume, forecast.data.cond)/100 * 40 * 20;
                        }
                        Debug.Log("Rainning");
                        isRain = true;
                    }
                    else
                    {
                        isRain = false;
                        notRaining();
                    }
                }

                if (useCanal && !isRain)
                {
                    waterVolume = 50;
                }

                else if (useCanal && isRain)
                {
                    if (waterVolume > 50)
                    {
                        waterVolume -= 0.5;
                    }
                }

                foreach (TMD_class.Forecast forecast in insectForecast)
                {
                    insectTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Insect invade
                        Insect();
                        Debug.Log("Insect");
                    }                    
                }

                foreach (TMD_class.Forecast forecast in diseaseForecast)
                {
                    diseaseTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        //Trigger Disease
                        Disease();
                        Debug.Log("Disease");
                    }
                }

                foreach (TMD_class.Forecast forecast in seaRiseForecast)
                {
                    seaRiseTrigger = false;
                    if (forecast.time.Date == TurnControl.turnInstance.gameDate.Date)
                    {
                        if (useCanal)
                        {
                            //Trigger Sea rise
                            SeaRise();
                            Debug.Log("Sea rise");
                        }
                    }
                }

                if (waterVolume > waterBaseLine + 10)
                {
                    //Trigger Flooding
                    floodTrigger = false;
                    if (useReservoir)
                    {
                        if (reservoirVolume <= 2000)
                        {
                            reservoirVolume += (waterVolume - 50) * 40 * 20;
                            waterVolume = 50;
                        }
                        else
                            reservoirVolume = 2000;
                    }

                    if (day7Count > 4)
                    {
                        Debug.Log("Flooding");
                        Flooding();
                    }
                    day7Count++;
                }

                else if (waterVolume < waterBaseLine - 10)
                {
                    //Trigger low water

                    if (useReservoir)
                    {
                        if (reservoirVolume >= 0)
                        {
                            reservoirVolume -= (50 - waterVolume) * 40 * 20;
                            waterVolume = 50;
                        }
                        else
                            reservoirVolume = 0;
                    }

                    if (day7Count > 4)
                    {
                        Debug.Log("Lack of Water");
                        LackOfWater();
                    }
                    day7Count++;
                }

                else
                {
                    day7Count = 0;
                }

                //Normal Situation
                waterVolume = WaterReduction(waterVolume, 1);
            }            
        }        
        oldTurn = TurnControl.turnInstance.turn;
    }

    public int intNotDuplicated(List<int> chkList)
    {
        int number;
        while (true)
        {
            number = UnityEngine.Random.Range(0, noRainForecast.Count);

            if (!chkList.Any())
                return number;
            else
            {
                foreach (int check in chkList)
                {
                    if (check == number)
                    {
                        number = UnityEngine.Random.Range(0, noRainForecast.Count);
                        break;
                    }
                    else
                    {
                        chkDuplicate.Add(number);
                        return number;
                    }                        
                }
            }
            
        }
    }
    public void GenerateInsect (int limit)
    {
        for(int i = 0; i < limit; i++)
        {
            insectForecast.Add(noRainForecast[intNotDuplicated(chkDuplicate)]);                        
        }
    }

    public void GenerateDisease(int limit)
    {
        for (int i = 0; i < limit; i++)
        {
            diseaseForecast.Add(noRainForecast[intNotDuplicated(chkDuplicate)]);
        }
    }

    public void GenerateSeaRise(int limit)
    {
        for (int i = 0; i < limit; i++)
        {
            seaRiseForecast.Add(noRainForecast[intNotDuplicated(chkDuplicate)]);
        }
    }

    #region Trigger

    public void upCommingRaining()
    {
        upCommingRain = true;
        onRainForecastTrigger?.Invoke();
    }
    public void Insect()
    {
        insectTrigger = true;
        onInsectTrigger?.Invoke();
        //Trigger Insect        
    }

    public void InsectSolution(bool useInsecticide)
    {
        this.useInsecticide = useInsecticide;
        if (useInsecticide)
        {
            riceQuantity = RiceReduction(riceQuantity, 2);
        }
        else if (!useInsecticide)
        {
            riceQuantity = RiceReduction(riceQuantity, 5);
        }
        else
            riceQuantity = RiceReduction(riceQuantity, 10);
    }

    public void Disease()
    {
        diseaseTrigger = true;
        onDiseaseTrigger?.Invoke();
    }

    public void diseaseSolution(bool useHerbicide)
    {
        this.useHerbicide = useHerbicide;

        //Trigger Disease
        if (useHerbicide)
        {
            riceQuantity = RiceReduction(riceQuantity, 2);
        }
        else if (!useHerbicide)
        {
            riceQuantity = RiceReduction(riceQuantity, 5);
        }
        else
            riceQuantity = RiceReduction(riceQuantity, 10);
    }
    public void Flooding()
    {
        floodTrigger = true;
        onFloodTrigger?.Invoke();
        //Trigger Flooding        
    }

    public void FloodSolution(bool useDrain)
    {
        this.useDrain = useDrain;
        riceQuantity = RiceReduction(riceQuantity, 5);
    }

    public void LackOfWater()
    {
        droughtTrigger = true;
        onDroughtTrigger?.Invoke();
        //Trigger Low water
        riceQuantity = RiceReduction(riceQuantity, 8);
        
    }

    public void SeaRise()
    {
        seaRiseTrigger = true;
        onSeaRiseTrigger?.Invoke();
        //Trigger Sea rise

    }

    public void seaRiseSolution(bool close)
    {
        closeWaterWay = close;
        if(!close)
        {
            riceQuantity = RiceReduction(riceQuantity, 2);
        }            
    }

    public double Rainning (double volume, int rainVolume)
    {
        rainTrigger = true;
        onRainTrigger?.Invoke();
        switch (rainVolume)
        {
            case 5:
                return volume + 2;
            case 6:
                return volume + 4;
            case 7:
                return volume + 8;
            case 8:
                return volume + 12;
            default:
                return 0;
        }
    }

    public void notRaining()
    {
        onNotRainTrigger?.Invoke();
    }

    #endregion

    #region rice parameters
    public double RiceReduction (double quantity, double damage)
    {
        return quantity - (quantity * damage/100);
    }

    public double WaterReduction (double volume, double reductRatio)
    {
        return volume - (volume * reductRatio/100);
    }
    #endregion
    public void ToggleCanal(bool use)
    {
        useCanal = use;
        Debug.Log("Use canel: " + useCanal);
    }

    public void makeReservior()
    {
        useReservoir = true;
    }

    public static double RiceQuantity
    {
        get{ return riceQuantity; }
    }
}
