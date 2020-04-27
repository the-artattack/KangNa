using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingAnimation : MonoBehaviour
{
    public GameObject sow;
    public GameObject truck;
    
    public void enable(string selectedChoice)
    {
        if(selectedChoice == "หว่านเอง")
        {
            sow.SetActive(true);
            Invoke("disable", 10.0f); //show animation for 5 seconds then disable
        }
        else
        {
            truck.SetActive(true);
            Invoke("disable", 10.0f);
        }        
    }

    public void disable()
    {
        sow.SetActive(false);
        truck.SetActive(false);
    }
}
