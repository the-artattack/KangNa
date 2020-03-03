using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterTab : MonoBehaviour
{
    public Text waterVolumn;
    private SimulateParameters parameterInstance;

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    public void Start()
    {
        MainGame.onWaterTabTrigger += createWaterTab;
    }

    private void createWaterTab(SimulateParameters parameters)
    {
        parameterInstance = parameters;        
    }

    private void Update()
    {
       if (Convert.ToInt32(parameterInstance.WaterVolume) > 60)
        {
            waterVolumn.text = "มากเกินไป";
        }
        else if (Convert.ToInt32(parameterInstance.WaterVolume) < 40)
        {
            waterVolumn.text = "ไม่เพียงพอต่อการใช้งาน";
        }
        else
        {
            waterVolumn.text = "เพียงพอต่อการใช้งาน";
        }
    }

    public void useCanel(bool use)
    {
        parameterInstance.ToggleCanal(use);
        onParameterUpdateTrigger?.Invoke(parameterInstance);
    }

}
