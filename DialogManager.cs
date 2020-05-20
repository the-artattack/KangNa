using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogManager : MonoBehaviour
{
    public Animator animator;
    public Text dialogueText;
    private string fullText = "";
    public int SceneIndex;
    private Queue<string> sentences;
    private string currentSceneName;

    public static event OnMillTrigger onMillTrigger;
    public delegate void OnMillTrigger();
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void Update()
    {
        /* Get current active scene */
        Scene m_Scene = SceneManager.GetActiveScene();
        currentSceneName = m_Scene.name;

        /* When user clicked mouse then show full text */
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text.Length < fullText.Length)
            {
                StopAllCoroutines();
                dialogueText.text = fullText;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialog dialogue)
    {
        //Debug.Log("Start conversation with " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        fullText = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
    void EndDialogue()
    {
        if (currentSceneName.Equals("3RiceIntroduction"))
        {
            //animator.SetBool("isEndConversation", true);
            //animator.SetBool("isBook", true);
            SceneChanger.nextScene(SceneIndex);
        }
        else if (currentSceneName.Equals("4SelectArea"))
        {
            animator.SetBool("isEnd", true);
        }
        else if (currentSceneName.Equals("5SelectRice"))
        {
            animator.SetBool("isEnd", true);
        }
        else if (currentSceneName.Equals("7ShopRice"))
        {
            animator.SetBool("isEnd", true);
        }
        else if (currentSceneName.Equals("8Calendar"))
        {
            animator.SetBool("isEnd", true);
        }
        else if(currentSceneName.Equals("9StartPlant"))
        {
            updateBalance();
            SceneChanger.nextScene(SceneIndex);
        }
        else if (currentSceneName.Equals("11Mill"))
        {
            animator.SetBool("isEnd", true);
            onMillTrigger?.Invoke();
        }
        else
        {
            Debug.Log("next scene");
            SceneChanger.nextScene(SceneIndex);
        }
    }
    private void updateBalance()
    {
        FirebaseInit.Instance._database.RootReference
        .Child("Users").Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
        .Child("State")
        .Child("balance")
        .SetValueAsync(FirebaseInit.Instance.CurrentMoney);
        Debug.Log(FirebaseInit.Instance.auth.CurrentUser.UserId);
        Debug.Log("save new balance");
    }
}
