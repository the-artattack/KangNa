using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaterTab : MonoBehaviour
{
    public Text waterVolumn;
    private void Update()
    {
        
        if(Convert.ToInt32(SimulateParameters.parameterInstance.waterVolume) > 60)
        {
            waterVolumn.text = "มากเกินไป";
        }
        else if(Convert.ToInt32(SimulateParameters.parameterInstance.waterVolume) < 40)
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
        SimulateParameters.parameterInstance.ToggleCanal(use);
    }


}
