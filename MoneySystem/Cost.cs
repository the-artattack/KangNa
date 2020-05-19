using System;

[Serializable]
public class Cost 
{
    public string topic;
    public float cost;

    public Cost(string topic, float cost)
    {
        this.topic = topic;
        this.cost = cost;
    }
}
