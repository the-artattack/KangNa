using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Animation Trigger */
public class Animation : MonoBehaviour
{
    public InsectAnimation insect;
    public RainAnimation rain;

    public void InsectEnable(SimulateParameters parameter)
    {
        insect.enable(parameter);
    }
    public void RainEnable(SimulateParameters parameters, TMD_class.Forecast forecast)
    {
        rain.enable(parameters, forecast);
    }
}
