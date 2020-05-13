using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** For pass values to another scene:
    - SimulateParameters
    - List of money (that used in simulation)
*/

public class Parameters : MonoBehaviour
{
    public static SimulateParameters parameters;
    public static List<Money> userMoneyList = new List<Money>();
    public static MoneyList moneyList;
    public static DateTime date;
    public static string riceType;

    public static void print()
    {
        Debug.Log("Rice quantity: " + parameters.RiceQuantity);
        Debug.Log("Water volume: " + parameters.WaterVolume);
        Debug.Log("List of money count: " + userMoneyList.Count);
    }
}
