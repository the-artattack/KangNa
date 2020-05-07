using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeaRiseAnimation : MonoBehaviour
{        
    public GameObject seaRiseAnimation;
    public Animator seaRise;
    public bool close;

    public static event onSeaRiseEvent onSeaRise;
    public delegate void onSeaRiseEvent(bool closeWay);

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {       
        seaRiseAnimation.SetActive(false); 
    }

    public void enable(SimulateParameters parameters)
    {
        seaRiseAnimation.SetActive(true);
        seaRise.SetBool("isSea", true);
        seaRiseSolution(parameters);
    }

    public void disable()
    {       
        seaRiseAnimation.SetActive(false);
        seaRise.SetBool("isSea", false);        
    }    

    public void seaRiseSolution(SimulateParameters parameters)
    {        
        if (!close)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            onParameterUpdateTrigger?.Invoke(parameters);
        }
    }
}
