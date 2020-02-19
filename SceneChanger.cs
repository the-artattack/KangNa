using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{      
    public static void nextScene(int SceneIndex)
    {
        FirebaseInit.Instance.CurrentScene = SceneIndex;
        Debug.Log("Current scene:" + FirebaseInit.Instance.CurrentScene);
        SceneManager.LoadScene(SceneIndex);
    }

    public void PreviousScene()
    {
        FirebaseInit.Instance.CurrentScene--;
        Debug.Log("Current scene:" + FirebaseInit.Instance.CurrentScene);
        SceneManager.LoadScene(FirebaseInit.Instance.CurrentScene);
    }

}
