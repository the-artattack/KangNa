using System;
using System.Collections.Generic;

[Serializable]
public class EvaluationBoard
{
    public int score;
    public int maxScore;
    public EvaluationBoard(int score, int maxScore)
    {
        this.score = score;
        this.maxScore = maxScore;
    }

    public Dictionary<string, Object> ToDictionary()
    {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["score"] = score;
        result["maxScore"] = maxScore;

        return result;
    }
}
