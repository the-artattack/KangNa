using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SimulateParameters : MonoBehaviour
{
    public static SimulateParameters parameterInstance { get; private set; }

    //Parameters
    private double riceQuantity = 5000; //kg
    private double waterVolume = 50; //cm
    private int waterBaseLine = 50; //cm    
    private double reservoirVolume = 0; //ลบ.ม.
    
    private bool isRain = false;
    public int day7Count = 0;
    private int rainCount = 0;
    private int limitInsect = 3;
    private int limitDisease = 3;
    private int limitSeaRise = 3;

    //Options
    private bool useReservoir = false;
    private bool useCanal = false;
    public bool useInsecticide = false;
    public bool useHerbicide = false;
    public bool useDrain = false;

    //Situation Forecast
    public List<TMD_class.Forecast> rainForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> floodForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> noRainForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> insectForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> diseaseForecast = new List<TMD_class.Forecast>();
    public List<TMD_class.Forecast> seaRiseForecast = new List<TMD_class.Forecast>();

    List<int> chkDuplicate = new List<int>();

    public SimulateParameters()
    {
        foreach (TMD_class.Forecast forecast in WeatherAPI.AllForecast)
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
                noRainForecast.Add(forecast);
            }

            day7Count++;
            day7Count %= 7;
        }

        day7Count = 0;

        GenerateInsect(limitInsect);
        GenerateDisease(limitDisease);
        GenerateSeaRise(limitSeaRise);
    }

    public void GenerateInsect(int limit)
    {
        for (int i = 0; i < limit; i++)
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

    public int intNotDuplicated(List<int> chkList)
    {
        Random rnd = new Random();
        int number;
        while (true)
        {
            number = rnd.Next(0, noRainForecast.Count);

            if (!chkList.Any())
                return number;
            else
            {
                foreach (int check in chkList)
                {
                    if (check == number)
                    {
                        number = rnd.Next(0, noRainForecast.Count);
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

    public void ToggleCanal(bool use)
    {
        useCanal = use;
    }

    #region getter setter
    public double RiceQuantity
    {
        get { return riceQuantity; }
        set { riceQuantity = value; }
    }

    public double WaterVolume
    {
        get { return waterVolume; }
        set { waterVolume = value; }
    }

    public int WaterBaseLine
    {
        get { return waterBaseLine; }
    }

    public bool UseReservoir
    {
        get { return useReservoir; }
        set { useReservoir = value; }
    }

    public double ReservoirVolume
    {
        get { return reservoirVolume; }
        set { reservoirVolume = value; }
    }

    public bool IsRain
    {
        get { return isRain; }
        set { isRain = value; }
    }

    public bool UseCanal
    {
        get { return useCanal; }
        set { useCanal = value; }
    }
    #endregion
}
