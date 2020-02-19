using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherTab : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject weather;
    private List<GameObject> objs = new List<GameObject>();

    public Text todayCondition;
    public Text todayTemp;
    public Image todayWeatherIcon;

    public Sprite cond1;
    public Sprite cond2;
    public Sprite cond3;
    public Sprite cond4;
    public Sprite cond5;
    public Sprite cond6;
    public Sprite cond7;
    public Sprite cond8;
    public Sprite cond9;
    public Sprite cond10;
    public Sprite cond11;
    public Sprite cond12;

    void Start()
    {
        SimulateParameters.onDateChanges += getWeatherForecast;
    }

    // Update is called once per frame
    void Update()
    {
        getTodayWeather();
    }

    public void getTodayWeather()
    {
        foreach (TMD_class.Forecast forecast in SimulateParameters.allForecast)
        {
            if (TurnControl.turnInstance.gameDate.Date == forecast.time)
            {
                todayCondition.text = TMD_api.Conditions(forecast.data.cond);
                todayTemp.text = string.Format("{0} ํC", Convert.ToInt32((forecast.data.tc_max + forecast.data.tc_min) / 2).ToString());
                todayWeatherIcon.sprite = GetWeatherIcon(forecast.data.cond);
            }            
        }
    }

    public void generateText(string date, string minTemp, string maxTemp, int cond)
    {
        GameObject scrollObj = Instantiate(weather);
        scrollObj.transform.SetParent(scrollContent.transform, false);
        scrollObj.transform.Find("date").gameObject.GetComponent<Text>().text = date;
        scrollObj.transform.Find("maxTemp").gameObject.GetComponent<Text>().text = string.Format("{0} ํC", maxTemp);
        scrollObj.transform.Find("minTemp").gameObject.GetComponent<Text>().text = string.Format("{0} ํC", minTemp);
        scrollObj.transform.Find("Image").gameObject.GetComponent<Image>().sprite = GetWeatherIcon(cond); ;
        objs.Add(scrollObj);
    }

    private void getWeatherForecast(DateTime date)
    {
        int count = 0;

        foreach (TMD_class.Forecast forecast in SimulateParameters.allForecast)
        {
            //Debug.Log(forecast.time);
            if (count == 7) break;
            else if (date.AddDays(1).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                            Convert.ToInt32(forecast.data.tc_min).ToString(),
                            Convert.ToInt32(forecast.data.tc_max).ToString(),
                            forecast.data.cond);
                count++;
            }
            else if (date.AddDays(2).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                        Convert.ToInt32(forecast.data.tc_min).ToString(),
                        Convert.ToInt32(forecast.data.tc_max).ToString(),
                        forecast.data.cond);
                count++;
            }
            else if (date.AddDays(3).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                        Convert.ToInt32(forecast.data.tc_min).ToString(),
                        Convert.ToInt32(forecast.data.tc_max).ToString(),
                        forecast.data.cond);
                count++;
            }
            else if (date.AddDays(4).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                         Convert.ToInt32(forecast.data.tc_min).ToString(),
                         Convert.ToInt32(forecast.data.tc_max).ToString(),
                         forecast.data.cond);
                count++;
            }
            else if (date.AddDays(5).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                        Convert.ToInt32(forecast.data.tc_min).ToString(),
                        Convert.ToInt32(forecast.data.tc_max).ToString(),
                        forecast.data.cond);
                count++;
            }
            else if (date.AddDays(6).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                        Convert.ToInt32(forecast.data.tc_min).ToString(),
                        Convert.ToInt32(forecast.data.tc_max).ToString(),
                        forecast.data.cond);
                count++;
            }
            else if (date.AddDays(7).Date == forecast.time)
            {
                generateText(TurnControl.turnInstance.ConvertThaiDate(forecast.time),
                        Convert.ToInt32(forecast.data.tc_min).ToString(),
                        Convert.ToInt32(forecast.data.tc_max).ToString(),
                        forecast.data.cond);
                count++;
            }
        }
        OnDestroy();
    }

    public void OnDestroy()
    {
        Destroy(objs[0]);
        while (objs.Count > 7)
        {
            if (objs[0] != null)
                Destroy(objs[0].gameObject);

            objs.RemoveAt(0);
        }
    }

    public Sprite GetWeatherIcon(int cond)
    {
        if (cond == 1)
        {
            return cond1;
        }
        else if (cond == 2)
        {
            return cond2;
        }
        else if (cond == 3)
        {
            return cond3;
        }
        else if (cond == 4)
        {
            return cond4;
        }
        else if (cond == 5)
        {
            return cond5;
        }
        else if (cond == 6)
        {
            return cond6;
        }
        else if (cond == 7)
        {
            return cond7;
        }
        else if (cond == 8)
        {
            return cond8;
        }
        else if (cond == 9)
        {
            return cond9;
        }
        else if (cond == 10)
        {
            return cond10;
        }
        else if (cond == 11)
        {
            return cond11;
        }
        else
        {
            return cond12;
        }        
    }
}
