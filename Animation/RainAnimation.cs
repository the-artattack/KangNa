using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RainAnimation : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite rainBackground;

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    public void enable(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        backgroundImage.sprite = rainBackground;
        parameters.WaterVolume = Rainning(parameters.WaterVolume, forecast.data.cond);
        if (parameters.UseReservoir)
        {
            parameters.ReservoirVolume = Rainning(parameters.ReservoirVolume, forecast.data.cond) / 100 * 40 * 20;
        }

        parameters.IsRain = true;
        onParameterUpdateTrigger?.Invoke(parameters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public double Rainning(double volume, int rainVolume)
    {
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
}
