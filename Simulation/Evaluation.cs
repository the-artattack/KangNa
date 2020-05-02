using UnityEngine;

public class Evaluation : MonoBehaviour
{
    public int score = 0;
    public int level;

    public static void increaseScore(int num)
    {
        score += num;
    }

    /** Return level from evaluation
     * Level 1: แย่ (0-4 points)
     * Level 2: ปานกลาง (5-9 points)
     * Level 3: พอใช้ (10-14 points)
     * Level 4: ดี (15-18 points)
     * max score = 18 */

    public int getEvaluation()
    {        
        if(score >= 15) 
        {
            level = 4;
        }
        else if(score > 9 &&  score < 15) 
        {
            level = 3;
        }
        else if(score > 4 && score < 10) 
        {
            level = 2;
        }
        else 
        {
            level = 1;
        }
        return level;
    }

    public string getEvaluationString()
    {
        string evalutionLevel;
        if(level == 1)
        {
            evalutionLevel = "แย่";
        }
        else if (level == 2)
        {
            evalutionLevel = "ปานกลาง";
        }
        else if (level == 3)
        {
            evalutionLevel = "พอใช้";
        }
        else
        {
            evalutionLevel = "ดี";
        }
        return evalutionLevel;
    }
}
