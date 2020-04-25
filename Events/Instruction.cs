using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    public Text header;
    public Transform container;
    public GameObject instructionWindow;
    private GameObject itemObject;
    private bool isCreated = false;

    public void createInstruction(string header, GameObject instructionObj)
    {
        if (!isCreated)
        {
            isCreated = true;
            instructionWindow.SetActive(true);
            this.header.text = header;
            itemObject = Instantiate(instructionObj, container);
            RectTransform transform = itemObject.transform.GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(0, 0);
        }
    }
    
    public void disable()
    {
        instructionWindow.SetActive(false);
        Destroy(itemObject, 1.0f);
        isCreated = false;
    }
}
