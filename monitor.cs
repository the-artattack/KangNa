using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class monitor : MonoBehaviour
{
    
    public void prevScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
