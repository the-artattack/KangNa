using UnityEngine;
using UnityEngine.UI;

public class UpCommingRainAnimation : MonoBehaviour
{
    public Text rainDate;
    public Text rainCount;
    public TurnControl turnControl;
    public Button close;
    public GameObject notificationBox;

    public void enable(SimulateParameters parameters)
    {
        notificationBox.SetActive(true);
        Debug.Log("First rain: " + parameters.rainForecast[0].time);
        string date = turnControl.ConvertThaiDate(parameters.rainForecast[0].time);
        string rainCount = (parameters.rainForecast[0].time.Date - turnControl.getGameDate.Date).Days.ToString();
        rainDate.text = string.Format("(วันที่ {0})", date);
        this.rainCount.text = rainCount;
    }

    public void closeBox()
    {
        notificationBox.SetActive(false);
    }
}
