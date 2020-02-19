using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneTransitions : MonoBehaviour
{
    public Animator Anim;
    public Animator ButtonAnim;
    public Image Img;
    public bool isOpenApp = false;
    private float delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (isOpenApp)
        {
            StartCoroutine(LoadLevelAfterDelay(delay));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextSceneButton(int SceneIndex)
    {
        StartCoroutine(ButtonTransition(SceneIndex));
    }
    public void nextSceneFade(int SceneIndex)
    {
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(Fade(SceneIndex));
    }

    private IEnumerator ButtonTransition(int SceneIndex)
    {
        ButtonAnim.SetBool("isClick", true);
        SceneChanger.nextScene(SceneIndex);
        yield return null;
    }
    private IEnumerator Fade(int SceneIndex)
    {
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneChanger.nextScene(SceneIndex);
    }

    private IEnumerator LoadLevelAfterDelay(float delay)
    {
        Anim.SetBool("Open", true);
        yield return new WaitForSeconds(delay);
        Anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1);
        SceneChanger.nextScene(1);
    }
}
