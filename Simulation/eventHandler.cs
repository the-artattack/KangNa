using System.Collections.Generic;
using System.Linq;

public static class eventHandler
{
    #region Trigger

    public static void upCommingRaining()
    {
        upCommingRain = true;
        onRainForecastTrigger?.Invoke();
    }
    public static void Insect()
    {
        insectTrigger = true;
        onInsectTrigger?.Invoke();
        //Trigger Insect        
    }

    public static void Disease()
    {
        diseaseTrigger = true;
        onDiseaseTrigger?.Invoke();
    }

    public static void diseaseSolution(bool useHerbicide)
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
    public static void Flooding()
    {
        floodTrigger = true;
        onFloodTrigger?.Invoke();
        //Trigger Flooding        
    }

    public static void LackOfWater()
    {
        droughtTrigger = true;
        onDroughtTrigger?.Invoke();
        //Trigger Low water
        riceQuantity = RiceReduction(riceQuantity, 8);

    }

    public static void SeaRise()
    {
        seaRiseTrigger = true;
        onSeaRiseTrigger?.Invoke();
        //Trigger Sea rise

    }

    public static double Rainning(double volume, int rainVolume)
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

    public static void notRaining()
    {
        onNotRainTrigger?.Invoke();
    }

    #endregion

    #region rice parameters
    public static double RiceReduction(double quantity, double damage)
    {
        return quantity - (quantity * damage / 100);
    }

    public static double WaterReduction(double volume, double reductRatio)
    {
        return volume - (volume * reductRatio / 100);
    }
    #endregion
    public static void ToggleCanal(bool use)
    {
        useCanal = use;
        Debug.Log("Use canel: " + useCanal);
    }

    public static void makeReservior()
    {
        useReservoir = true;
    }

    public static double RiceQuantity
    {
        get { return riceQuantity; }
    }

    public static implicit operator SimulateParameters(MainGame v)
    {
        throw new NotImplementedException();
    }
}