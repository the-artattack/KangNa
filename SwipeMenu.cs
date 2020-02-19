using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeMenu : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;
    // Start is called before the first frame update
    void Start()
    {
        nextButton = nextButton.GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (nextButton.onClick != null)
        {
            nextButton.onClick.AddListener(NextSwipe);
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 3) && scroll_pos > pos[i] - (distance / 3))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
    }
    
    private void NextSwipe()
    {
        scroll_pos = scrollbar.GetComponent<Scrollbar>().value;        
            
    }
        /*for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < pos.Length; j++)
                {
                    transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
            }
        }*/
 

}
