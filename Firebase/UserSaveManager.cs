using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;

public class UserSaveManager : MonoBehaviour
{
    void Start()
    {
        SceneChanger.onSceneChanged += SavePlayer;
    }

    public void writeNewUser(string userId, string username, string loginType)
    {
        //User user = new User();
        User user = new User(userId, username, loginType);
        string jsonData = JsonUtility.ToJson(user);

        /* Generate unique ID of user */
        FirebaseInit.Instance._database.RootReference
            .Child("Users").Child(userId)
            .SetRawJsonValueAsync(jsonData);
        Debug.Log("Write new user");
    }

    public void SavePlayer()
    {
        Debug.Log("Saved scene!!!");
        float balance = FirebaseInit.Instance.CurrentMoney;
        int scene = FirebaseInit.Instance.CurrentScene;
        UserBoard entry = new UserBoard(balance, scene);
        Dictionary<string, System.Object> entryValues = entry.ToDictionary();
        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>
        {
            ["/Users/" + FirebaseInit.Instance.auth.CurrentUser.UserId + "/State"] = entryValues
        };

        FirebaseInit.Instance._database.RootReference
            .UpdateChildrenAsync(childUpdates);

        Debug.Log("Save player: " + FirebaseInit.Instance.auth.CurrentUser.UserId);
        Debug.Log("With current scene: " + scene + " and balance " + balance);
    }
    
}
