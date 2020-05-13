using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Money", menuName = "Money")]
public class Money : ScriptableObject
{
    public string topic;
    public float cost;
}
