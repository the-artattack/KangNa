using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeaRiseAnimation : MonoBehaviour
{
    public GameObject questionBox;
    public GameObject blackTransparency;
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
        questionBox.SetActive(false);
        seaRiseAnimation.SetActive(false);
        blackTransparency.SetActive(false);
        MainGame.onSeaRiseTrigger += enableSeaRise;
    }

    private void enableSeaRise(SimulateParameters parameters)
    {
        seaRiseAnimation.SetActive(true);
        seaRise.SetBool("isSea", true);
        questionTrigger();
        seaRiseSolution(parameters);
    }

    private void disableSeaRise()
    {
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
        seaRiseAnimation.SetActive(false);
        seaRise.SetBool("isSea", false);
        Mai.onSeaRiseTrigger -= enableSeaRise;
    }
    private void questionTrigger()
    {
        questionBox.SetActive(true);
        blackTransparency.SetActive(true);

        Button btnA1 = questionBox.transform.Find("btnA1").GetComponent<Button>();
        Button btnA2 = questionBox.transform.Find("btnA2").GetComponent<Button>();
        btnA1.onClick.AddListener(closeWay);
        btnA2.onClick.AddListener(notCloseWay);
    }

    private void notCloseWay()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        close = true;
        onSeaRise?.Invoke(close);
        disableSeaRise();
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
    }

    private void closeWay()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        close = false;
        onSeaRise?.Invoke(close);
        disableSeaRise();
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
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
