using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Change rice field in each phase */
public class RicePhaseAnimation : MonoBehaviour
{
    public Image riceField;
    public RiceTab riceTab;
    public Sprite phase1; //ระยะเริ่มปลูก
    public Sprite phase2; //ระยะกล้า
    public Sprite phase3; //ระยะแตกกอ
    public Sprite phase4; //ระยะตั้งท้อง
    public Sprite phase5; //ระยะออกรวง
    public Sprite phase6; //ระยะใกล้เก็บเกี่ยว
    public Sprite phase7; //ระยะเก็บเกี่ยว

    private void Start()
    {
        TurnControl.onRicePhase += RicePhaseImageUpdate;
    }

    private void Update()
    {
    }

    public void RicePhaseImageUpdate(int day, DateTime gameDate)
    {
        if (day <= 4)
        {
            riceField.sprite = phase1;
        }
        else if(day == riceTab.getHarvestDay)
        {
            riceField.sprite = phase7;
        }
        else
        {
            if (RiceTab.RicePhase == "ระยะต้นกล้า")
            {
                riceField.sprite = phase2;
            }
            else if (RiceTab.RicePhase == "ระยะแตกกอ")
            {
                riceField.sprite = phase3;
            }
            else if (RiceTab.RicePhase == "ระยะตั้งท้อง")
            {
                riceField.sprite = phase4;
            }
            else if (RiceTab.RicePhase == "ระยะออกรวง")
            {
                riceField.sprite = phase5;
            }
            else if (RiceTab.RicePhase == "ระยะเก็บเกี่ยว")
            {
                riceField.sprite = phase6;
            }            
        }
    }
}
