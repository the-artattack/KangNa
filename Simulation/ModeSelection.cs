using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    public Button beginner;
    public Button expert;

    private void Start()
    {
        beginner.onClick.AddListener(beginnerTrigger);
        expert.onClick.AddListener(expertTrigger);
    }
    private void beginnerTrigger()
    {
        FirebaseInit.Instance.mode = 0;
        goToNextScene();
    }
    private void expertTrigger()
    {
        FirebaseInit.Instance.mode = 1;
        goToNextScene();
    }
    private void goToNextScene()
    {        
        SceneChanger.nextScene(3);
    }

    private void saveModeToFirebase()
    {
        FirebaseInit.Instance._database.RootReference
            .Child("Users").Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("Mode")
            .SetValueAsync(getMode(FirebaseInit.Instance.mode));
        Debug.Log("Save mode: " + FirebaseInit.Instance.mode);
    }
    private string getMode(int mode)
    {
        if (mode == 1)
        {
            return "expert";
        }
        else
        {
            return "beginner";
        }
    }
}
