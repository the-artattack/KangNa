using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloodAnimation : MonoBehaviour
{
    public GameObject questionBox;
    public GameObject blackTransparency;
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
        questionBox.SetActive(false);
        floodAnimation.SetActive(false);
        blackTransparency.SetActive(false);
        MainGame.onFloodTrigger += enableFlood;
    }

    private void enableFlood(SimulateParameters parameters)
    {
        floodAnimation.SetActive(true);
        flood.SetBool("isFlood", true);        
        questionTrigger();
        FloodSolution(parameters);
    }

    private void disableFlood()
    {
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
        floodAnimation.SetActive(false);
        flood.SetBool("isFlood", false);
        MainGame.onSeaRiseTrigger -= enableFlood;
    }

    private void questionTrigger()
    {        
        questionBox.SetActive(true);
        blackTransparency.SetActive(true);

        Button btnA1 = questionBox.transform.Find("btnA1").GetComponent<Button>();
        Button btnA2 = questionBox.transform.Find("btnA2").GetComponent<Button>();
        btnA1.onClick.AddListener(drain);
        btnA2.onClick.AddListener(notDrain);
    }

    private void drain()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        isDrain = true;
        onFlooding?.Invoke(isDrain);
        disableFlood();
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
    }

    private void notDrain()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        isDrain = false;
        onFlooding?.Invoke(isDrain);
        disableFlood();
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
    }

    public void FloodSolution(SimulateParameters parameters)
    {
        parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        onParameterUpdateTrigger?.Invoke(parameters);
    }
}
