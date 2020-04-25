using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSolution : MonoBehaviour
{ 
    public void solutionA(Question activeQuestion)
    {
        //ใช้แสงไฟฟ้าล่อแมลงและกำจัด +1
        if (activeQuestion.topic == "แมลงหล่า")
        {
            //Do something
            Events.RiceBlackBug = false;
        }
        //ปล่อยให้ตัวมวนเขียวดูดไข่จัดการ +1
        else if (activeQuestion.topic == "เพลี้ยกระโดดสีน้ำตาล")
        {
            //Do something
            Events.BrownPlantHopper = false;
        }
        //จับโดยกับดักแสงไฟ +1
        else if (activeQuestion.topic == "เพลี้ยจักจั่นสีเขียว")
        {
            //Do something
            Events.GreenLeafHopper = false;
        }
        //ใส่ปุ๋ยไนโตรเจน 10 กก./ไร่ +0
        else if (activeQuestion.topic == "หนอนห่อใบข้าว")
        {
            //Do something
            Events.LeafFolder = false;
        }
        //กำจัดวัชพืช +1
        else if (activeQuestion.topic == "แมลงบั่ว")
        {
            //Do something
            Events.RiceGallMidges = false;
        }
        //ปล่อยน้ำเข้าแปลงนา +0
        else if (activeQuestion.topic == "หนอนปลอกข้าว")
        {
            //Do something
            Events.RiceCaseWorm = false;
        }
        //ไขน้ำท่วมยอดข้าวทิ้งไว้ 1-2 วัน +1
        else if (activeQuestion.topic == "เพลี้ยไฟ")
        {
            //Do something
            Events.Thrips = false;
        }
        //ใช้ยาฆ่าแมลง +1
        else if (activeQuestion.topic == "แมลงสิง")
        {
            //Do something
            Events.StinkBug = false;
        }
        //ไขน้ำเข้าแปลงนา +0
        else if (activeQuestion.topic == "เพลี้ยกระโดดหลังขาว")
        {
            //Do something
            Events.WhiteBackedPlantHopper = false;
        }
    }

    public void solutionB(Question activeQuestion)
    {
        //ปล่อยน้ำเข้าแปลง +0
        if (activeQuestion.topic == "แมลงหล่า")
        {
            //Do something
            Events.RiceBlackBug = false;
        }
        //กำจัดตัวมวนเขียวดูดไข่ +0
        else if (activeQuestion.topic == "เพลี้ยกระโดดสีน้ำตาล")
        {
            //Do something
            Events.BrownPlantHopper = false;
        }
        //ใชยาฆ่าแมลง +0
        else if (activeQuestion.topic == "เพลี้ยจักจั่นสีเขียว")
        {
            //Do something
            Events.GreenLeafHopper = false;
        }
        //กำจัดพืชอาศัย +1
        else if (activeQuestion.topic == "หนอนห่อใบข้าว")
        {
            //Do something
            Events.LeafFolder = false;
        }
        //ใช้ยาฆ่าแมลง +0
        else if (activeQuestion.topic == "แมลงบั่ว")
        {
            //Do something
            Events.RiceGallMidges = false;
        }
        //ระบายน้ำออกจากแปลงนา +1
        else if (activeQuestion.topic == "หนอนปลอกข้าว")
        {
            //Do something
            Events.RiceCaseWorm = false;
        }
        //ใช้ยาฆ่าแมลง +0
        else if (activeQuestion.topic == "เพลี้ยไฟ")
        {
            //Do something
            Events.Thrips = false;
        }
        //ใช้สวิงจับแมลงสิง +1
        else if (activeQuestion.topic == "แมลงสิง")
        {
            //Do something
            Events.StinkBug = false;
        }
        //ไขน้ำออกจากแปลงนา +1
        else if (activeQuestion.topic == "เพลี้ยกระโดดหลังขาว")
        {
            //Do something
            Events.WhiteBackedPlantHopper = false;
        }
    }
}
