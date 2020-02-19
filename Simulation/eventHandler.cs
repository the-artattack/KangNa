public class eventHandler
{
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
        if (!close)
        {
            riceQuantity = RiceReduction(riceQuantity, 2);
        }
    }

    public double Rainning(double volume, int rainVolume)
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
    public double RiceReduction(double quantity, double damage)
    {
        return quantity - (quantity * damage / 100);
    }

    public double WaterReduction(double volume, double reductRatio)
    {
        return volume - (volume * reductRatio / 100);
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
        get { return riceQuantity; }
    }
}