using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Animation Trigger */
public class Animation : MonoBehaviour
{
    public InsectAnimation insect;
    public DiseaseAnimation disease;
    public RainAnimation rain;
    public SeaRiseAnimation seaRise;
    public FloodAnimation flood;
    public DroughtAnimation drought;
    public NotRainAnimation notRain;
    public UpCommingRainAnimation upCommingRain;

    public void InsectEnable(string insect, SimulateParameters parameter)
    {
        this.insect.enable(insect, parameter);
    }
    public void DiseaseEnable(string disease, SimulateParameters parameter)
    {
        this.disease.enable(disease, parameter);
    }
    public void RainEnable(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        rain.enable(parameters, forecast);
    }
    public void SeaRiseEnable(SimulateParameters parameters)
    {
        seaRise.enable(parameters);
    }
    public void FloodEnable(SimulateParameters parameters)
    {
        flood.enable(parameters);
    }
    public void DroughtEnable(SimulateParameters parameters)
    {
        drought.enable(parameters);
    }
    public void NotRainEnable(SimulateParameters parameters)
    {
        notRain.enable(parameters);
    }

    public void UpCommingRainEnable(SimulateParameters parameters)
    {
        upCommingRain.enable(parameters);
    }
}
