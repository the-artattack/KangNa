﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseAnimation : MonoBehaviour
{
    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void enable(string diseaseType, SimulateParameters parameters)
    {
        diseaseSolution(parameters);
    }

    public void disable(string diseaseType)
    {
        
    }

    public void diseaseSolution(SimulateParameters parameters)
    {
        //Trigger Disease
        if (parameters.useHerbicide)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
        }
        else if (!parameters.useHerbicide)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        }
        else
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 10);

        onParameterUpdateTrigger?.Invoke(parameters);
    }

}
