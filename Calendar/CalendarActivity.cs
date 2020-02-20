using Proyecto26;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class TMD_api : MonoBehaviour
{
    public Text month;

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollTextPrefeb;
    private List<GameObject> objs = new List<GameObject>();

    List<TMD_class.Forecast> currentMonthForecast = new List<TMD_class.Forecast>();
    List<TMD_class.Forecast> nextMonthForecast = new List<TMD_class.Forecast>();
    List<TMD_class.Forecast> next2MonthForecast = new List<TMD_class.Forecast>();

    const string pluginName = "com.example.androidlibrary.MyPlugin";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

    // Start is called before the first frame update
    void Start()
    {

            if (monthTemp == currentMonth)
            {
                if (currentMonthForecast != null && currentMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in currentMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Current month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }
    }

    private void Init()
    {
        int currentMonth = WeatherAPI.AllForecast[0].time.month;
        foreach (TMD_class.Forecast forecast in WeatherAPI.AllForecast)
        {
            
        }
    }

    public string MonthString(int month)
    {
        switch (month)
        {
            case 1:
                return "มกราคม";
            case 2:
                return "กุมภาพันธ์";
            case 3:
                return "มีนาคม";
            case 4:
                return "เมษายน";
            case 5:
                return "พฤษภาคม";
            case 6:
                return "มิถุนายน";
            case 7:
                return "กรกฎาคม";
            case 8:
                return "สิงหาคม";
            case 9:
                return "กันยายน";
           case 10:
                return "ตุลาคม";
            case 11:
                return "พฤศจิกายน";
            case 12:
                return "ธันวาคม";
            default:
                return "Error";
        }
    }

    public void nextMonth()
    {
        OnDestroy();
        if (monthTemp - currentMonth >= 0 && monthTemp - currentMonth < 2)
        {
            month.text = MonthString(monthTemp + 1);
            monthTemp++;
            monthTemp %= 12;

            if (monthTemp == currentMonth + 1)
            {
                if (nextMonthForecast != null && nextMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in nextMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Next 1 month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }

            else if (monthTemp == currentMonth + 2)
            {
                if (next2MonthForecast != null && next2MonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in next2MonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Next 2 months");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }
        }       
    }

    public void prevMonth()
    {
        OnDestroy();
        if (monthTemp - currentMonth < 3 && monthTemp - currentMonth > 0)
        {
            month.text = MonthString(monthTemp - 1);
            monthTemp--;
            monthTemp %= 12;

            if (monthTemp == currentMonth)
            {
                if (currentMonthForecast != null && currentMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in currentMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Current month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }

            else if (monthTemp == currentMonth + 1)
            {
                if (nextMonthForecast != null && nextMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in nextMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Next 1 month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }
        }            
    }

    public void generateText(string forecast)
    {
        if (forecast != null)
        {
            GameObject scrollObj = Instantiate(scrollTextPrefeb);
            scrollObj.transform.SetParent(scrollContent.transform, false);
            scrollObj.transform.gameObject.GetComponent<Text>().text = forecast.ToString();
            objs.Add(scrollObj);
        }
    }

    public void OnDestroy()
    {
        Debug.Log("cur count: " + currentMonthCount);
        Debug.Log("cur count: " + nextMonthCount);
        Debug.Log("cur count: " + next2MonthCount);
        while (objs.Count > currentMonthCount)
        {
            if(objs[0]!=null)
                Destroy(objs[0].gameObject);

            objs.RemoveAt(0);
        }        
    }

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
                _pluginClass.SetStatic<AndroidJavaObject>("mainActivity", activity);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    public void CalendarTrigger()
    {
        if (Application.platform == RuntimePlatform.Android)
            PluginInstance.Call("showCalendar");
    }

}
