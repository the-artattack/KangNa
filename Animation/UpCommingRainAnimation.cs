using UnityEngine;
using UnityEngine.UI;

public class UpCommingRainAnimation : MonoBehaviour
{
    public Text rainDate;
    public Text rainCount;
    public TurnControl turnControl;
    public GameObject notificationBox;

    public void enable(SimulateParameters parameters)
    {        
        Debug.Log("First rain: " + parameters.rainForecast[0].time);        
        if(getDayCount(parameters) != 0)
        {
            string date = turnControl.ConvertThaiDate(parameters.rainForecast[0].time);
            notificationBox.SetActive(true);
            rainDate.text = string.Format("(วันที่ {0})", date);
            rainCount.text = getDayCount(parameters).ToString();
            Invoke("closeBox", 10.0f);
        }        
    }

    private int getDayCount(SimulateParameters parameters)
    {
        int dayCount;
        dayCount = (parameters.rainForecast[0].time.Date - turnControl.getGameDate.Date).Days;
        return dayCount;
    }

    private void closeBox()
    {
        notificationBox.SetActive(false);
    }
}
