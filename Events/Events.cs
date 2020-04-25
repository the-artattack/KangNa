using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    /** Diseases */
    public static bool BacterialBlight;
    public static bool RiceBlast;
    public static bool SheathBlight;
    public static bool RaggedStunt;
    public static bool DirtyPanicle;
    public static bool BrownSpot;

    /** Insects */
    public static bool Thrips;
    public static bool WhiteBackedPlantHopper;
    public static bool GreenLeafHopper;
    public static bool LeafFolder;
    public static bool BrownPlantHopper;
    public static bool RiceBlackBug;
    public static bool RiceGallMidges;
    public static bool RiceCaseWorm;
    public static bool StinkBug;

    /** Other events */
    public static bool Rain;
    public static bool UpCommingRain;
    public static bool Flood;
    public static bool SeaRise;
    public static bool Drought;

    private void Start()
    {
        BacterialBlight = false;
        RiceBlast = false;
        SheathBlight = false;
        RaggedStunt = false;
        DirtyPanicle = false;
        BrownSpot = false;

        /** Insects */
        Thrips = false;
        WhiteBackedPlantHopper = false;
        GreenLeafHopper = false;
        LeafFolder = false;
        BrownPlantHopper = false;
        RiceBlackBug = false;
        RiceGallMidges = false;
        RiceCaseWorm = false;
        StinkBug = false;

        /** Other events */
        Rain = false;
        UpCommingRain = false;
        Flood = false;
        SeaRise = false;
        Drought = false;
    }

    public static void DiseaseTrigger(string name)
    {
        if(name == "โรคขอบใบแห้ง")
        {
            BacterialBlight = true;
        }
        else if (name == "โรคใบจุดสีน้ำตาล")
        {
            BrownSpot = true;
        }
        else if (name == "โรคเมล็ดด่าง")
        {
            DirtyPanicle = true;
        }
        else if (name == "โรคใบหงิก")
        {
            RaggedStunt = true;
        }
        else if (name == "โรคไหม้")
        {
            RiceBlast = true;
        }
        else if (name == "โรคกาบใบแห้ง")
        {
            SheathBlight = true;
        }
    }

    public static void InsectTrigger(string name)
    {
        if (name == "เพลี้ยไฟ")
        {
            Thrips = true;
        }
        else if (name == "เพลี้ยกระโดดหลังขาว")
        {
            WhiteBackedPlantHopper = true;
        }
        else if (name == "เพลี้ยจักจั่นสีเขียว")
        {
            GreenLeafHopper = true;
        }
        else if (name == "หนอนห่อใบข้าว")
        {
            LeafFolder = true;
        }
        else if (name == "เพลี้ยกระโดดสีน้ำตาล")
        {
            BrownPlantHopper = true;
        }
        else if (name == "แมลงหล่า")
        {
            RiceBlackBug = true;
        }
        else if (name == "แมลงบั่ว")
        {
            RiceGallMidges = true;
        }
        else if (name == "หนอนปลอกข้าว")
        {
            RiceCaseWorm = true;
        }
        else if (name == "แมลงสิง")
        {
            StinkBug = true;
        }
    }
}
