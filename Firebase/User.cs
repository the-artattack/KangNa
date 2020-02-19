﻿using System;

[Serializable]
public class User
{
    public string userId;
    public string username;
    public string loginType;
    public int balance;
    public int scene;
    
    public User(string userId, string username, string loginType)
    {
        this.userId = userId;
        this.username = username;
        this.loginType = loginType;
    }
}
