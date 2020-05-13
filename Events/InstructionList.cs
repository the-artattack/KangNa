using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class InstructionList : MonoBehaviour
{
    private Queue<Instruction> instructions = new Queue<Instruction>();
    public InstructionDisplay instructionDisplay;   

    private Instruction activeInstruction;
    private float waitTime = 3.0f;
    private Coroutine trainingCoroutine = null;

    public void addInstruction(Instruction instruction)
    {
        instruction.isActive = false;
        instructions.Enqueue(instruction);
        Debug.Log(instruction.topic + " Added");
        Debug.Log("Number of instruction: " + instructions.Count);
        if (trainingCoroutine == null)
        {
            Debug.Log("Start Instruction Coroutine");
            trainingCoroutine = StartCoroutine(showActiveInstruction());
        }
    }
    IEnumerator showActiveInstruction()
    {
        WaitForSeconds time = new WaitForSeconds(waitTime);
        while (instructions.Count > 0)
        {
            yield return time;
            activeInstruction = instructions.Dequeue();
            activeInstruction.isActive = true;
            if (activeInstruction.isActive)
            {
                Debug.Log("Instruction: " + activeInstruction.topic);
                instructionDisplay.createInstruction(activeInstruction);                
            }            
        }
        trainingCoroutine = null;
    }
}
