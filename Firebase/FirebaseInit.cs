using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Unity.Editor;
using System;
using System.Threading.Tasks;

public class FirebaseInit : MonoBehaviour
{
    public static FirebaseInit Instance { get; private set; }
    public UnityEvent OnFirebaseInitialized = new UnityEvent();
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;
    public FirebaseDatabase _database;

    public int CurrentScene;
    public float CurrentMoney;

    public string riceType;
    public string area;

    //0 beginner, 1 expert
    public int mode;

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
        StartCoroutine(LoadSceneAndBalance());
    }

    private IEnumerator WaitTask(Task task)
    {
        while (task.IsCompleted == false)
        {
            yield return null;
        }
        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
    private IEnumerator LoadSceneAndBalance()
    {
        Task<DataSnapshot> balance = FirebaseDatabase.DefaultInstance.GetReference("Users")
            .Child(user.UserId)
            .Child("State")
            .Child("balance")
            .GetValueAsync();
        yield return WaitTask(balance);
        if (balance.Result.Value == null)
        {
            CurrentMoney = 100000;
        }
        else
        {
            CurrentMoney = Int32.Parse(balance.Result.Value.ToString());            
        }
        Debug.Log("balance: " + CurrentMoney);

        Task<DataSnapshot> mode = FirebaseDatabase.DefaultInstance.GetReference("Users")
            .Child(user.UserId)
            .Child("Mode")
            .GetValueAsync();
        yield return WaitTask(mode);
        if (mode.Result.Value == null)
        {
            this.mode = 0;
        }
        else
        {
            if(mode.Result.Value.ToString() == "expert")
            {
                this.mode = 1;
            }
            else
            {
                this.mode = 0;
            }
        }
        Debug.Log("mode: " + this.mode);

        Task<DataSnapshot> scene = FirebaseDatabase.DefaultInstance.GetReference("Users")
            .Child(user.UserId)
            .Child("State")
            .Child("scene")
            .GetValueAsync();
        yield return WaitTask(scene);
        int sceneIndex;
        if (scene.Result.Value == null)
        {
            sceneIndex = 2;
        }
        else
        {
            sceneIndex = Int32.Parse(scene.Result.Value.ToString());
        }
        
        Debug.Log("scene: " + sceneIndex);
        SceneChanger.nextScene(sceneIndex);
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
            DataSnapshot data = e.Snapshot;
            if (data.Child("TypeOfRice").Value != null)
            {
                riceType = data.Child("TypeOfRice").Value.ToString();                
            }
            foreach (var child in data.Child("TypeOfLand").Children)
            {
                area = child.Value.ToString();
            }            
        }
    }    

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}
