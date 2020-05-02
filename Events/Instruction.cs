using UnityEngine;

/** Template for instruction pop-up to make decision for solution*/
[CreateAssetMenu(fileName = "New Instruction", menuName = "Instruction")]
public class Instruction : ScriptableObject
{
    public bool isActive;

    public string topic;
    public GameObject description;
    public void Print()
    {
        Debug.Log("Instruction topic: " + topic);
    }
}
