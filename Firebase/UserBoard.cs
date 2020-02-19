using System;
using System.Collections.Generic;

[Serializable]
public class UserBoard
{
    public int balance;
    public int scene;

    public UserBoard(int balance, int scene)
    {
        this.balance = balance;
        this.scene = scene;
    }

    public Dictionary<string, Object> ToDictionary()
    {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["balance"] = balance;
        result["scene"] = scene;

        return result;
    }
}
