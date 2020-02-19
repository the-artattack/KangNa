using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Google;
public class AuthController : MonoBehaviour
{
    public UserSaveManager databaseHandler;

    /** For google sign in*/
    //public Text infoText;
    private string webClientId = "1049796260071-i9j9edffi33q08m1cgmlunoaubm929uf.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;

    /*#region Facebook 
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    void Update()
    {

    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // stop game
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            // Initialize not complete
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    public void FacebookLogin()
    {
        var perms = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(perms, onLogIn);
    }
    public void FacebookLogout()
    {
        FB.LogOut();
    }

    private void onLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var token = AccessToken.CurrentAccessToken;
            accessToken(token); //invoke auth method
        }
        else
            Debug.Log("log failed");
    }
    public void accessToken(AccessToken accessToken)
    {
        var credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken.TokenString);
        facebookAuth = FirebaseAuth.DefaultInstance;

        if (!FB.IsLoggedIn)
        {
            return;
        }

        facebookAuth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            databaseHandler.writeNewUser(newUser.UserId, newUser.DisplayName, "Facebook");
        });
    }
    #endregion
*/

    #region Google
    private void Start()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
    }
    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            AddToInformation("Google ID Token = " + task.Result.IdToken);
            AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        FirebaseInit.Instance.auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                AddToInformation("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                AddToInformation("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            AddToInformation("Sign In Successful.");
            FirebaseInit.Instance.user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                FirebaseInit.Instance.user.DisplayName, FirebaseInit.Instance.user.UserId);
            AddToInformation("After signed in: " + FirebaseInit.Instance.user.DisplayName + "\n" + FirebaseInit.Instance.user.UserId);
            databaseHandler.writeNewUser(FirebaseInit.Instance.user.UserId, FirebaseInit.Instance.user.DisplayName, "Google");

        });
    }
    private void AddToInformation(string str) {
        //infoText.text += "\n" + str; 
        Debug.Log(str);
    }

    #endregion

    #region Anonymous
    public void AnonymousLogin()
    {
        FirebaseInit.Instance.auth.SignInAnonymouslyAsync().ContinueWith((task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e =
                task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e =
                task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsCompleted)
            {
                print("User is logged in");
                FirebaseInit.Instance.user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    FirebaseInit.Instance.user.DisplayName, FirebaseInit.Instance.user.UserId);
                databaseHandler.writeNewUser(FirebaseInit.Instance.user.UserId, "Anonymous User", "Anonymous");
            }
        }));
    }
    #endregion
    /*private Text emailInput, passwordInput;
    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text).ContinueWith((task =>
            {
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsCompleted)
                {
                    print("User is logged in");
                }
            }));
    }
    
    public void RegisterUser()
    {
        if (emailInput.text.Equals("") && passwordInput.text.Equals(""))
        {
            print("Please enter an email and password to register");
            return;
        }

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith((task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e =
                task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e =
                task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsCompleted)
            {
                print("Register complete");
            }
        }));
    }

    public void Logout()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }*/

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
        /*switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentrials:
                break;
            case AuthError.MissingPassword:
                break;
            case AuthError.WrongPassword:
                break;
        }*/
        print(msg);
    }
}
