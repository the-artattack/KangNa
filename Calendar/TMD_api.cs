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
    private static TMD_class myObject = new TMD_class();
    private int currentMonth;
    private static int monthTemp = 0;
    string lat = "13.655091";
    string lon = "100.494908";
    string date = "2020-02-08";
    string duration = "60";
    string fields = "rain,cond";
    public Text month;

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollTextPrefeb;
    private List<GameObject> objs = new List<GameObject>();
    private int currentMonthCount = 0;
    private int nextMonthCount = 0;
    private int next2MonthCount = 0;

    List<TMD_class.Forecast> currentMonthForecast = new List<TMD_class.Forecast>();
    List<TMD_class.Forecast> nextMonthForecast = new List<TMD_class.Forecast>();
    List<TMD_class.Forecast> next2MonthForecast = new List<TMD_class.Forecast>();

    const string pluginName = "com.example.androidlibrary.MyPlugin";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

    // Start is called before the first frame update
    void Start()
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImUzMzlkYTEzZWQ0ZDc3NTZjZDM2YmNmZGI0MDVhZTI0OGMwZmUzZGVhOTI0NjVjZjMyYmY1MTQ1OGI4NWNjYmZlN2VhOWNmNDE4YTVhZmRhIn0.eyJhdWQiOiIyIiwianRpIjoiZTMzOWRhMTNlZDRkNzc1NmNkMzZiY2ZkYjQwNWFlMjQ4YzBmZTNkZWE5MjQ2NWNmMzJiZjUxNDU4Yjg1Y2NiZmU3ZWE5Y2Y0MThhNWFmZGEiLCJpYXQiOjE1ODA5Nzg5NDUsIm5iZiI6MTU4MDk3ODk0NSwiZXhwIjoxNjEyNjAxMzQ1LCJzdWIiOiI3MDEiLCJzY29wZXMiOltdfQ.nFSWISVdU7QPGjgL4FVUam0tQfo1s5Sm287mnYUPrNgss8asxGWKj1jIUsn840BDzVgOfdhpgQcAQ7ynxbCFz9BvmuvQnWcZ1_JeC7LJ29bye9TIg4BZ9z3mEXYdk4QwXYIBSuiVW4bY1lFO9LvThbdyrQuISOI43pon69qUzeL7x06WjoAl0EOetCg1OG15uSiZFUYowNOFr7jDF4_fx-6ftb0EKT9gsrPCkEVyo5VG-O6rfjoBZYx-rzJBSrWKGldzunMUua38T71ZVfuuGZn_7paqOZd4Dn_OZvN7EbE56jeC2GvFJEGaJqJmCwJB3I9RlG06BvkiLX5YV9C8f08IlON2smGufW-N_-rSJ2sX0pFsKvESwWJeicdQQb_byY0M2IVVdjIno_rV8nQIpqJztx3WLSZAnjunmVvDJnQ1m7dP1RZqXaM9osYa_JWgqFrSdp-A8u-BoiyFd_OIaxgdXn928L6AnYmIm9dWwEaokZHcUIxpviP-P1ruaM4-GrwmXCEcRZ7sqepRcgnGwkJxENolgWYJxEF0A4QpOSUUvHYk_I0414PX1Tmrktb9Ynzz5RrFHrZTmBEmEYNdRi31PLoWdAYWHeVMmog6vJ79Agscv4Iv90hjk-jsQ3eu9-NJbMwvUcQwuKF2rVs6DVX6g9d_qc2vpMfh6dPbMjU";
        RestClient.DefaultRequestParams["ContentType"] = "application/json";

        

        RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat="+ lat +"&lon="+ lon +"&fields="+ fields +"&date="+ date +"&duration="+ duration)
            .Then(response => {
                //EditorUtility.DisplayDialog("Response", response.Text, "Ok");
                //Debug.Log("response: " + response.Text);
                myObject = JsonConvert.DeserializeObject<TMD_class>(response.Text);
                //Debug.Log(myObject.WeatherForecasts[0].forecasts[0].data.cond);

                string[] yyyymmdd = date.Split('-');
                currentMonth = Int32.Parse(yyyymmdd[1]);
                //Debug.Log("currentMonth: " + currentMonth);

                foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                {
                    //Debug.Log("original: " + forecast.time);
                    string[] temp = forecast.time.ToString("yyyy-MM-dd").Split('-');
                    int month = Int32.Parse(temp[1]);
                    foreach (string word in temp)
                    {
                        //Debug.Log("temp: " + word);
                    }

                    if (currentMonth == month && forecast.data.cond >= 5 && forecast.data.cond <= 8)
                    {
                        currentMonthCount++;
                        currentMonthForecast.Add(forecast);
                    }
                    else if (currentMonth + 1 == month && forecast.data.cond >= 5 && forecast.data.cond <= 8)
                    {
                        nextMonthCount++;
                        nextMonthForecast.Add(forecast);
                    }
                    else if (currentMonth + 2 == month && forecast.data.cond >= 5 && forecast.data.cond <= 8)
                    {
                        next2MonthCount++;
                        next2MonthForecast.Add(forecast);
                    }
                    else
                        Debug.Log("No rain.");
                }

                foreach (TMD_class.Forecast forecast in currentMonthForecast)
                {
                    //Debug.Log("currentMonthForecast: " + forecast.time);
                }

                foreach (TMD_class.Forecast forecast in nextMonthForecast)
                {
                    //Debug.Log("nextMonthForecast: " + forecast.time);
                }

                foreach (TMD_class.Forecast forecast in next2MonthForecast)
                {
                    //Debug.Log("next2MonthForecast: " + forecast.time);
                }
                monthTemp = currentMonth;
                month.text = MonthString(monthTemp);
                
            }).Catch(err => Debug.Log(err.Message));

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

    // Update is called once per frame
    void Update()
    {        

       
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

    public static string Conditions(int cond)
    {
        switch (cond)
        {
            case 1:
                return "ท้องฟ้าแจ่มใส";
            case 2:
                return "มีเมฆบางส่วน";
            case 3:
                return "เมฆเป็นส่วนมาก";
            case 4:
                return "มีเมฆมาก";
            case 5:
                return "ฝนตกเล็กน้อย";
            case 6:
                return "ฝนปานกลาง";
            case 7:
                return "ฝนตกหนัก";
            case 8:
                return "ฝนฟ้าคะนอง";
            case 9:
                return "อากาศหนาวจัด";
            case 10:
                return "อากาศหนาว";
            case 11:
                return "อากาศเย็น";
            case 12:
                return "อากาศร้อนจัด";
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
