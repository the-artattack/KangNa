using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public static SimulateParameters parameters;

    public static void print()
    {
        Debug.Log("Rice quantity: " + parameters.RiceQuantity);
        Debug.Log("Water volume: " + parameters.WaterVolume);
    }
}
