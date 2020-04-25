using Newtonsoft.Json;
using Proyecto26;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeatherAPI : MonoBehaviour
{
    private TMD_class myObject = new TMD_class();
    private string lat = "13.655091";
    private string lon = "100.494908";
    private static DateTime date = DateTime.Now;
    private int duration;
    private string fields = "rain,cond,tc_min,tc_max";

    private static List<TMD_class.Forecast> allForecast = new List<TMD_class.Forecast>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        duration = 30 - date.Day;

        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImUzMzlkYTEzZWQ0ZDc3NTZjZDM2YmNmZGI0MDVhZTI0OGMwZmUzZGVhOTI0NjVjZjMyYmY1MTQ1OGI4NWNjYmZlN2VhOWNmNDE4YTVhZmRhIn0.eyJhdWQiOiIyIiwianRpIjoiZTMzOWRhMTNlZDRkNzc1NmNkMzZiY2ZkYjQwNWFlMjQ4YzBmZTNkZWE5MjQ2NWNmMzJiZjUxNDU4Yjg1Y2NiZmU3ZWE5Y2Y0MThhNWFmZGEiLCJpYXQiOjE1ODA5Nzg5NDUsIm5iZiI6MTU4MDk3ODk0NSwiZXhwIjoxNjEyNjAxMzQ1LCJzdWIiOiI3MDEiLCJzY29wZXMiOltdfQ.nFSWISVdU7QPGjgL4FVUam0tQfo1s5Sm287mnYUPrNgss8asxGWKj1jIUsn840BDzVgOfdhpgQcAQ7ynxbCFz9BvmuvQnWcZ1_JeC7LJ29bye9TIg4BZ9z3mEXYdk4QwXYIBSuiVW4bY1lFO9LvThbdyrQuISOI43pon69qUzeL7x06WjoAl0EOetCg1OG15uSiZFUYowNOFr7jDF4_fx-6ftb0EKT9gsrPCkEVyo5VG-O6rfjoBZYx-rzJBSrWKGldzunMUua38T71ZVfuuGZn_7paqOZd4Dn_OZvN7EbE56jeC2GvFJEGaJqJmCwJB3I9RlG06BvkiLX5YV9C8f08IlON2smGufW-N_-rSJ2sX0pFsKvESwWJeicdQQb_byY0M2IVVdjIno_rV8nQIpqJztx3WLSZAnjunmVvDJnQ1m7dP1RZqXaM9osYa_JWgqFrSdp-A8u-BoiyFd_OIaxgdXn928L6AnYmIm9dWwEaokZHcUIxpviP-P1ruaM4-GrwmXCEcRZ7sqepRcgnGwkJxENolgWYJxEF0A4QpOSUUvHYk_I0414PX1Tmrktb9Ynzz5RrFHrZTmBEmEYNdRi31PLoWdAYWHeVMmog6vJ79Agscv4Iv90hjk-jsQ3eu9-NJbMwvUcQwuKF2rVs6DVX6g9d_qc2vpMfh6dPbMjU";
        RestClient.DefaultRequestParams["ContentType"] = "application/json";

        RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 30).ToString())
                .Then(response =>
                {

                    myObject = JsonConvert.DeserializeObject<TMD_class>(response.Text);

                    foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                    {
                        allForecast.Add(forecast);
                    }

                    RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 60).ToString())
                        .Then(res1 =>
                        {

                            myObject = JsonConvert.DeserializeObject<TMD_class>(res1.Text);

                            foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                            {
                                allForecast.Add(forecast);
                            }

                            RestClient.Get("https://data.tmd.go.th/nwpapi/v1/forecast/location/daily/at?lat=" + lat + "&lon=" + lon + "&fields=" + fields + "&date=" + date.ToString("yyyy-MM-dd") + "&duration=" + (duration + 90).ToString())
                                .Then(res2 =>
                                {

                                    myObject = JsonConvert.DeserializeObject<TMD_class>(res2.Text);

                                    foreach (TMD_class.Forecast forecast in myObject.WeatherForecasts[0].forecasts)
                                    {
                                        allForecast.Add(forecast);
                                    }

                                }).Catch(err => Debug.Log(err.Message));
                        }).Catch(err => Debug.Log(err.Message));
                }).Catch(err => Debug.Log(err.Message));
    }

    public static List<TMD_class.Forecast> AllForecast
    {
        get { return allForecast; }
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

    public static DateTime CurrentDate
    {
        get { return date; }
        set { date = value; }
    }
}
