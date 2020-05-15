using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static event onSceneChange onSceneChanged;
    public delegate void onSceneChange();

    public static void nextScene(int SceneIndex)
    {
        FirebaseInit.Instance.CurrentScene = SceneIndex;
        Debug.Log("Current scene:" + FirebaseInit.Instance.CurrentScene);        
        SceneManager.LoadScene(SceneIndex);
        if(SceneIndex > 2)
        { 
            onSceneChanged?.Invoke();
        }
    }

    public void PreviousScene()
    {
        FirebaseInit.Instance.CurrentScene--;
        Debug.Log("Current scene:" + FirebaseInit.Instance.CurrentScene);        
        SceneManager.LoadScene(FirebaseInit.Instance.CurrentScene);
        onSceneChanged?.Invoke();
    }
    
}
