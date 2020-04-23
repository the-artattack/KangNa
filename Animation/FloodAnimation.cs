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
        MainGame.onFloodTrigger += enableFlood;
    }

    private void enableFlood(SimulateParameters parameters)
    {
        floodAnimation.SetActive(true);
        flood.SetBool("isFlood", true);   
        FloodSolution(parameters);
    }

    private void disableFlood()
    {                
        floodAnimation.SetActive(false);
        flood.SetBool("isFlood", false);
        MainGame.onSeaRiseTrigger -= enableFlood;
    }    

    private void drain()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        isDrain = true;
        onFlooding?.Invoke(isDrain);
        disableFlood();               
    }

    private void notDrain()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        isDrain = false;
        onFlooding?.Invoke(isDrain);
        disableFlood();        
    }

    public void FloodSolution(SimulateParameters parameters)
    {
        parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        onParameterUpdateTrigger?.Invoke(parameters);
    }
}
