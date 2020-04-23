using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnControl : MonoBehaviour
{
    public static TurnControl turnInstance { get; private set; }
    public Text TurnDisplay;
    public DateTime startDate;
    public int turn = 5;
    public DateTime gameDate = DateTime.Now;
    public int day = 0;
    public Text dateDisplay;

    public static event onRicePhaseHandler onRicePhase;
    public delegate void onRicePhaseHandler(int day);
    private void Start()
    {
        dateDisplay.text = ConvertThaiDate(gameDate);
        onRicePhase?.Invoke(day);
        if (turnInstance == null)
        {
            turnInstance = this;
            startDate = gameDate;       
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        TimeControl.OnTimeAdvance += Advance;
    }
    private void OnDisable()
    {
        TimeControl.OnTimeAdvance -= Advance;
    }
    public void Advance()
    {
        turn++;
        turn = TimeManagement(turn);
        TurnDisplay.text = string.Format("{0}:00 น.", turn);
    }

    private int TimeManagement(int time)
    {
        if(time % 24 != 0)
        {
            return time;
        }
        else
        {
            gameDate = gameDate.AddDays(1);
            day++;
            dateDisplay.text = ConvertThaiDate(gameDate);
            Debug.Log("Day: " + gameDate.Date);
            onRicePhase?.Invoke(day);
            return time %= 24;
        }
    }

    public string ConvertThaiDate (DateTime time)
    {
        switch (time.Month)
        {
            case 1:
                return time.Day + " " + "ม.ค." + " " + (time.Year + 543);
            case 2:
                return time.Day + " " + "ก.พ." + " " + (time.Year + 543);
            case 3:
                return time.Day + " " + "มี.ค." + " " + (time.Year + 543);
            case 4:
                return time.Day + " " + "เม.ย." + " " + (time.Year + 543);
            case 5:
                return time.Day + " " + "พ.ค." + " " + (time.Year + 543);
            case 6:
                return time.Day + " " + "มิ.ย." + " " + (time.Year + 543);
            case 7:
                return time.Day + " " + "ก.ค." + " " + (time.Year + 543);
            case 8:
                return time.Day + " " + "ส.ค." + " " + (time.Year + 543);
            case 9:
                return time.Day + " " + "ก.ย." + " " + (time.Year + 543);
            case 10:
                return time.Day + " " + "ต.ค." + " " + (time.Year + 543);
            case 11:
                return time.Day + " " + "พ.ย." + " " + (time.Year + 543);
            case 12:
                return time.Day + " " + "ธ.ค." + " " + (time.Year + 543);
            default:
                return "Error";
        }
    }
    public int Day
    {
        get { return day; }
    }
}
