using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestAnimation : MonoBehaviour
{
    public GameObject scythe;
    public GameObject truck;

    public void enable(string selectedChoice)
    {
        if (selectedChoice == "เก็บเกี่ยวเอง")
        {
            scythe.SetActive(true);
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
        scythe.SetActive(false);
        truck.SetActive(false);
        SceneChanger.nextScene(FirebaseInit.Instance.CurrentScene+1); //go to next scene
    }
}
