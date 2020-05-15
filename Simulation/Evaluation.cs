using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public static int scoreMax = 0;
    public static int score = 0;
    public static int level;

    /** Return level from evaluation
     * Level 1: พอใช้ (0-25%)
     * Level 2: ปานกลาง (26-50%)
     * Level 3: ดี (51-75%)
     * Level 4: ดีมาก (76-100%)
     * max score = 18 */

    public static int getEvaluation()
    {
        //Evaluate level of educate by percentage from score you get divide by max score
        float scorePercentage = (score / scoreMax) * 100;

        //76-100%
        if(scorePercentage > 75) 
        {
            level = 4; 
        }
        //51-75%
        else if(scorePercentage > 50 && scorePercentage < 76) 
        {
            level = 3;
        }
        //26-50%
        else if(scorePercentage > 25 && scorePercentage < 51) 
        {
            level = 2;
        }
        //0-25%
        else 
        {
            level = 1;
        }
        return level;
    }

    public static string getEvaluationString()
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
