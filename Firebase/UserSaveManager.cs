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
        int balance = 100000;
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

    private void SaveScene(int scene)
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(FirebaseInit.Instance.user.UserId)
            .ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            FirebaseInit.Instance._database.RootReference
                .Child("Users").Child(FirebaseInit.Instance.user.UserId).SetValueAsync(FirebaseInit.Instance.CurrentScene);
            Debug.Log("Write current scene");
        }
    }

}
