using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloodAnimation : MonoBehaviour
{        
    public GameObject floodAnimation;
    public Animator flood;
    public bool isDrain;

    public static event onFloodEvent onFlooding;
    public delegate void onFloodEvent(bool drain);

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {        
        floodAnimation.SetActive(false);  
    }

    public void enable(SimulateParameters parameters)
    {
        floodAnimation.SetActive(true);
        flood.SetBool("isFlood", true);   
        FloodSolution(parameters);
    }

    public void disable()
    {                
        floodAnimation.SetActive(false);
        flood.SetBool("isFlood", false);        
    }    

    public void FloodSolution(SimulateParameters parameters)
    {
        parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        onParameterUpdateTrigger?.Invoke(parameters);
    }
}
