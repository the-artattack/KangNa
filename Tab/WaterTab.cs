using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterTab : MonoBehaviour
{
    public Text waterVolumn;
    public Text waterVolumnText;
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
        waterVolumn.text = string.Format("ระดับน้ำอยู่ที่ {0} ซม.",parameterInstance.waterVolume.ToString());
       if (Convert.ToInt32(parameterInstance.WaterVolume) > 80)
        {
            waterVolumnText.text = "มากเกินไป";
        }
        else if (Convert.ToInt32(parameterInstance.WaterVolume) < 20)
        {
            waterVolumnText.text = "ไม่เพียงพอต่อการใช้งาน";
        }
        else
        {
            waterVolumnText.text = "เพียงพอต่อการใช้งาน";
        }
    }

    public void useCanel(bool use)
    {
        parameterInstance.ToggleCanal(use);
        onParameterUpdateTrigger?.Invoke(parameterInstance);
    }

}
