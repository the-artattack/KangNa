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

    // Start is called before the first frame update
    void Start()
    {
        questionBox.SetActive(false);
        seaRiseAnimation.SetActive(false);
        blackTransparency.SetActive(false);
        SimulateParameters.onSeaRiseTrigger += enableSeaRise;
    }

    private void enableSeaRise()
    {
        seaRiseAnimation.SetActive(true);
        seaRise.SetBool("isSea", true);
        questionTrigger();
    }

    private void disableSeaRise()
    {
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);
        seaRiseAnimation.SetActive(false);
        seaRise.SetBool("isSea", false);
        SimulationManager.onSeaRiseTrigger -= enableSeaRise;
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
}
