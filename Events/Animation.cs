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
    public PlantingAnimation planting;
    public HarvestAnimation harvest;

    #region Enable Animation
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
    public void PlantingEnable(string selectedChoice)
    {
        planting.enable(selectedChoice);
    }
    public void HarvestEnable(string selectedChoice)
    {
        harvest.enable(selectedChoice);
    }
    #endregion

    #region Disable Animation
    public void InsectDisable(string insect)
    {
        this.insect.disable(insect);
    }
    public void DiseaseDisable(string disease)
    {
        this.disease.disable(disease);
    }
    public void SeaRiseDisable()
    {
        seaRise.disable();
    }
    public void FloodDisable()
    {
        flood.disable();
    }
    #endregion
}
