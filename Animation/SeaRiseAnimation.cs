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
        MainGame.onSeaRiseTrigger += enableSeaRise;
    }

    private void enableSeaRise(SimulateParameters parameters)
    {
        seaRiseAnimation.SetActive(true);
        seaRise.SetBool("isSea", true);
        seaRiseSolution(parameters);
    }

    private void disableSeaRise()
    {       
        seaRiseAnimation.SetActive(false);
        seaRise.SetBool("isSea", false);
        MainGame.onSeaRiseTrigger -= enableSeaRise;
    }    

    private void notCloseWay()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        close = true;
        onSeaRise?.Invoke(close);
        disableSeaRise();        
    }

    private void closeWay()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        close = false;
        onSeaRise?.Invoke(close);
        disableSeaRise();        
    }

    public void seaRiseSolution(SimulateParameters parameters)
    {
        parameters.closeWaterWay = close;
        if (!close)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            onParameterUpdateTrigger?.Invoke(parameters);
        }
    }
}
