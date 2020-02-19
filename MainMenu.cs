using UnityEngine;
using UnityEngine.UI;
/** Main menu for calling login function and quiting application */
public class MainMenu : MonoBehaviour
{
    public AuthController auth;

    public void AnnonymousLogin()
    {
        auth.AnonymousLogin();
    }
    public void GoogleLogin()
    {
        auth.SignInWithGoogle();
    }

    public void ExitApp()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
