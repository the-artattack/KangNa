using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System;

public class FirebaseInit : MonoBehaviour
{
    public static FirebaseInit Instance { get; private set; }
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;
    public FirebaseDatabase _database;

    public int CurrentScene;
    public string riceType;

    //This will set up all initialize
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentScene = 0;

            //Initialize firebase authenticaton
            InitializeFirebaseAuth();
            InitializeFirebaseDatabase();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void InitializeFirebaseDatabase()
    {
        // Set this before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://kangna.firebaseio.com/");
        _database = FirebaseDatabase.DefaultInstance;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError("Failed to initialize Firebase with {task.Exception}");
                return;
            }
        });
    }

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebaseAuth()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                LoadUser();
            }
        }
    }

    public void LoadUser()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Education").Child(user.UserId)
            .ValueChanged += LoadData;
        FirebaseDatabase.DefaultInstance.GetReference("Users").Child(user.UserId)
            .ValueChanged += LoadScene;
    }
    private void LoadData(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot.Child("TypeOfRice");
            if(data.Value != null)
            {
                riceType = data.Value.ToString();
            }            
        }
    }

    private void LoadScene(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot.Child("State").Child("scene");
            if (data.Value != null)
            {
                int scene = Int32.Parse(data.Value.ToString());
                Debug.Log("scene : " + scene);
                SceneChanger.nextScene(scene);
            }
            else
            { 
                SceneChanger.nextScene(2); 
            }
        }
    }
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
