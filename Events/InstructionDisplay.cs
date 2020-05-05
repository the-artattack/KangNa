using UnityEngine;
using UnityEngine.UI;

public class InstructionDisplay : MonoBehaviour
{
    public Text header;
    public Transform container;
    public GameObject instructionWindow;
    public GameObject blackTranparency;
    private GameObject itemObject;
    private bool isCreated = false;
   
    public void createInstruction(Instruction instruction)
    {
        if (!isCreated)
        {
            isCreated = true;
            instructionWindow.SetActive(true);
            blackTranparency.SetActive(true);
            this.header.text = instruction.topic;
            itemObject = Instantiate(instruction.description, container);
            RectTransform transform = itemObject.transform.GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(0, 0);
        }
    }
    
    public void disable()
    {
        instructionWindow.SetActive(false);
        blackTranparency.SetActive(false);
        Destroy(itemObject, 1.0f);
        isCreated = false;
    }   
}
