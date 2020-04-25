using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class CalendarActivity : MonoBehaviour
{
    public Text month;

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollTextPrefeb;
    private List<GameObject> objs = new List<GameObject>();

    private List<TMD_class.Forecast> currentMonthForecast = new List<TMD_class.Forecast>();
    private List<TMD_class.Forecast> nextMonthForecast = new List<TMD_class.Forecast>();
    private List<TMD_class.Forecast> next2MonthForecast = new List<TMD_class.Forecast>();

    const string pluginName = "com.example.androidlibrary.MyPlugin";

    class DatepickerCallback : AndroidJavaProxy
    {
        private System.Action<int> datepickerHandler;

        public DatepickerCallback(System.Action<int> datepickerHandlerIn) : base(pluginName + "$DatepickerCallback")
        {
            datepickerHandler = datepickerHandlerIn;
        }

        public void onDateSelected(int day, int month, int year)
        {
            Debug.Log("Unity Date Selected: " + day + "-" + month + "-" + year);
            WeatherAPI.CurrentDate = new DateTime(year, month, day);
        }
    }

    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

    private int currentMonth;
    private int selectedMonth;

    private int currentMonthCount;
    private int nextMonthCount;
    private int next2MonthCount;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        CalendarTrigger((int obj) =>
        {
            Debug.Log("Local Handler called: " + obj);
        });
        CurrentMonth();
    }

    private void Init()
    {
        currentMonth = WeatherAPI.CurrentDate.Month;
        selectedMonth = currentMonth;
        foreach (TMD_class.Forecast forecast in WeatherAPI.AllForecast)
        {
            if (currentMonth == forecast.time.Month)
            {
                currentMonthForecast.Add(forecast);
            }
            else if (currentMonth + 1 == forecast.time.Month)
            {
                nextMonthForecast.Add(forecast);
            }
            else if (currentMonth + 2 == forecast.time.Month)
            {
                next2MonthForecast.Add(forecast);
            }
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

    public void CurrentMonth()
    {
        if (selectedMonth == currentMonth)
        {
            if (currentMonthForecast != null && currentMonthForecast.Any())
            {
                foreach (TMD_class.Forecast i in currentMonthForecast)
                {
                    string forecast = i.time.Day + " " + MonthString(i.time.Month) + " - " + WeatherAPI.Conditions(i.data.cond);
                    generateText(forecast);
                }

                Debug.Log("Current month");
            }
            else
                generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
        }
    }

    public void nextMonth()
    {
        OnDestroy();
        if (selectedMonth - currentMonth >= 0 && selectedMonth - currentMonth < 2)
        {
            month.text = MonthString(selectedMonth + 1);
            selectedMonth++;
            selectedMonth %= 12;

            if (selectedMonth == currentMonth + 1)
            {
                if (nextMonthForecast != null && nextMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in nextMonthForecast)
                    {
                        string forecast = i.time.Day + " " + MonthString(i.time.Month) + " - " + WeatherAPI.Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Next 1 month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }

            else if (selectedMonth == currentMonth + 2)
            {
                if (next2MonthForecast != null && next2MonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in next2MonthForecast)
                    {
                        string forecast = i.time.Day + " " + MonthString(i.time.Month) + " - " + WeatherAPI.Conditions(i.data.cond);
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
        if (selectedMonth - currentMonth < 3 && selectedMonth - currentMonth > 0)
        {
            month.text = MonthString(selectedMonth - 1);
            selectedMonth--;
            selectedMonth %= 12;

            if (selectedMonth == currentMonth)
            {
                if (currentMonthForecast != null && currentMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in currentMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + WeatherAPI.Conditions(i.data.cond);
                        generateText(forecast);
                    }

                    Debug.Log("Current month");
                }
                else
                    generateText("เดือนนี้ไม่มีฝนตก ดีจังเลย~");
            }

            else if (selectedMonth == currentMonth + 1)
            {
                if (nextMonthForecast != null && nextMonthForecast.Any())
                {
                    foreach (TMD_class.Forecast i in nextMonthForecast)
                    {
                        string[] temp = i.time.ToString("yyyy-MM-dd").Split('-');
                        int temp2 = Int32.Parse(temp[1]);
                        string forecast = temp[2] + " " + MonthString(temp2) + " - " + WeatherAPI.Conditions(i.data.cond);
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
            if (objs[0] != null)
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

    public void CalendarTrigger(System.Action<int> handler = null)
    {
        if (Application.platform == RuntimePlatform.Android)
            PluginInstance.Call("showCalendar", new object[] { new DatepickerCallback(handler) });
    }

}
