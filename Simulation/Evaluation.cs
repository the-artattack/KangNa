using Firebase.Database;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Evaluation : MonoBehaviour
{
    public int scoreMax = 0;
    public int score = 0;
    public int level;
    private EvaluationManager evaluationManager;

    private void Awake()
    {
        evaluationManager = GameObject.FindObjectOfType<EvaluationManager>();
    }

    public void increaseScore()
    {
        score++;
        Debug.Log("Current score: " + score);
    }
    public void updateMaxScore()
    {
        scoreMax++;
        Debug.Log("Max score: " + scoreMax);
    }

    /** Return level from evaluation
     * Level 1: พอใช้ (0-25%)
     * Level 2: ปานกลาง (26-50%)
     * Level 3: ดี (51-75%)
     * Level 4: ดีมาก (76-100%)
     * max score = 18 */
    public int getEvaluation(float scorePercentage)
    {
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
        Debug.Log("Level of evaluation: " + level);
        return level;
    }

    public string getEvaluationString()
    {
        string evalutionLevel;
        if(level == 1)
        {
            evalutionLevel = "พอใช้";
        }
        else if (level == 2)
        {
            evalutionLevel = "ปานกลาง";
        }
        else if (level == 3)
        {
            evalutionLevel = "ดี";
        }
        else if(level == 4)
        {
            evalutionLevel = "ดีมาก";
        }
        else
        {
            evalutionLevel = "";
        }
        return evalutionLevel;
    }

    public void LoadScore()
    {
        StartCoroutine(LoadScoreEvaluation());
    }

    private IEnumerator WaitTask(Task task)
    {
        while (task.IsCompleted == false)
        {
            yield return null;
        }
        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
    private IEnumerator LoadScoreEvaluation()
    {
        Task<DataSnapshot> score = FirebaseDatabase.DefaultInstance.GetReference("Education")
            .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("Evaluation")
            .Child("score")
            .GetValueAsync();
        yield return WaitTask(score);
        if (score.Result.Value == null)
        {
            this.score = 0;
        }
        else
        {
            this.score = Int32.Parse(score.Result.Value.ToString());
        }
        Debug.Log("score: " + this.score);

        Task<DataSnapshot> maxScore = FirebaseDatabase.DefaultInstance.GetReference("Education")
            .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("Evaluation")
            .Child("maxScore")
            .GetValueAsync();
        yield return WaitTask(maxScore);
        if (maxScore.Result.Value == null)
        {
            scoreMax = 0;
        }
        else
        {
            scoreMax = Int32.Parse(maxScore.Result.Value.ToString());
        }

        Debug.Log("maxScore: " + scoreMax);

        //Evaluate level of educate by percentage from score you get divide by max score
        float scorePercentage = ((float)this.score / scoreMax) * 100.0f;
        Debug.Log("Score Percent: " + scorePercentage + "%");
        getEvaluation(scorePercentage);
        evaluationManager.showStars();
    }
}
