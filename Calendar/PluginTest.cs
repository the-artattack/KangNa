using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PluginTest : MonoBehaviour
{
    const string pluginName = "com.example.androidlibrary.MyPlugin";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;
    public Text logtext;

    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
                AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
                _pluginClass.SetStatic<AndroidJavaObject>("mainActivity", activity);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {
        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        showCalendar();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void showCalendar()
    {
        Debug.Log("Enter showCalendar()");
        if (Application.platform == RuntimePlatform.Android)
            PluginInstance.Call("showCalendar");
        Debug.LogWarning("Wrong Platform");
    }
}
